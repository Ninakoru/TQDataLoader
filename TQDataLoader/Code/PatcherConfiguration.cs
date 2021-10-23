using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TQDataLoader.Code
{
    public class PatcherConfiguration
    {
        private const string newFilesFolder = "\\new";

        private string _configPath;
        private string _sourcePath;
        private string _destinationPath;
        private bool _delDestination;
        private IList<PatcherConfigAction> _rawConfig;
        private IList<PatcherConfigAction> _config;

        public IList<PatcherConfigAction> Configuration 
        { 
            get { return _config; } 
        }

        public string NewFilesConfigPath { get { return _configPath + newFilesFolder; } }

        public string SourcePath { get { return _sourcePath; } }

        public string DestinationPath { get { return _destinationPath; } }

        public int TotalFiles { get { return _config.Select(x=> x.TargetFile.Trim().ToLower()).Distinct().ToList().Count; } }

        public PatcherConfiguration(string configPath, string sourcePath, string destinationPath, bool deleteDestination)
        {
            _configPath = configPath;
            _sourcePath = sourcePath;
            _destinationPath = destinationPath;
            _delDestination = deleteDestination;
        }

        public PatcherConfigResult LoadConfiguration()
        {
            PatcherConfigResult result = new PatcherConfigResult();

            CheckAllPaths(result);

            if (result.HasFailures) { return result; }

            if (_delDestination)
            {
                DeleteDestinationFiles(result);
            }

            if (result.HasFailures) { return result; }

            LoadRawConfiguration(result);

            if (result.HasFailures) { return result; }

            if (!_rawConfig.Any())
            {
                result.AddFailureMessage("No configuration data found in config directory.");
                return result;
            }

            ArrangeConfiguration(result);

            return result;
        }

        private void CheckAllPaths(PatcherConfigResult result)
        {
            if (!FileUtils.DirectoryExists(_configPath))
            {
                result.AddFailureMessage("Configuration directory does not exist.");
            }

            if (!FileUtils.DirectoryExists(_sourcePath))
            {
                result.AddFailureMessage("Source directory does not exist.");
            }

            if (!FileUtils.DirectoryExists(_destinationPath))
            {
                result.AddFailureMessage("Destination directory does not exist.");
            }        
        }

        private void DeleteDestinationFiles(PatcherConfigResult result)
        {
            try
            {
                FileUtils.DeleteAllInDir(_destinationPath);
            }
            catch(Exception ex)
            {
                result.AddFailureMessage(ex.Message);
            }            
        }

        private void LoadRawConfiguration(PatcherConfigResult result)
        {
            try
            {
                var rawconfig = new List<PatcherConfigAction>();

                var files = FileUtils.GetAllFilesRecursive(_configPath, "txt");

                if (!files.Any())
                {
                    throw new Exception("No txt files inside configuration directory.");
                }

                files.ToList().ForEach(x => rawconfig.AddRange(GetConfigActions(x)));

                _rawConfig = rawconfig;
            }
            catch (Exception ex)
            {
                result.AddFailureMessage(ex.Message);
            }   

        }

        private void ArrangeConfiguration(PatcherConfigResult result)
        {
            try
            {
                _config = new List<PatcherConfigAction>();

                // Discover all target files, create a config action for each file, merge same file actions.
                foreach (var cfg in _rawConfig)
                {
                    foreach (var dir in cfg.TempDirectories)
                    {
                        var fullPath = FileUtils.GetCombinedPath(_sourcePath, dir);

                        var allFiles = FileUtils.GetAllMatchingPaths(fullPath);

                        if (!allFiles.Any())
                        {
                            throw new Exception(string.Format("Error in target files, path not found for '{0}'", fullPath));
                        }

                        foreach (var matchedFile in allFiles)
                        {
                            var tempCfgAction = _config.FirstOrDefault(x => x.TargetFile == matchedFile);
                            if (tempCfgAction == null)
                            { 
                                tempCfgAction = new PatcherConfigAction();
                                tempCfgAction.TargetFile = matchedFile;
                            }
                            tempCfgAction.LoadActions(cfg.TempActions);

                            _config.Add(tempCfgAction);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.AddFailureMessage(ex.Message);
            }

        }

        private IList<PatcherConfigAction> GetConfigActions(string filePath)
        {
            const string DIRECTORY_ACTION_SEPARATOR = "----";

            var configActions = new List<PatcherConfigAction>();

            var fileData = FileUtils.ReadAllLines(filePath);
            
            // Empty Files...
            if (fileData.Length <= 1)
            {
                return configActions;
            }

            bool isDirectory = true;
            int lineCount = 1;
            var tempConfig = new PatcherConfigAction();

            foreach (var line in fileData)
            {
                if (IsEmptyOrCommentLine(line))
                {
                    lineCount++;
                    continue;
                }

                if (line.StartsWith(DIRECTORY_ACTION_SEPARATOR))
                {
                    ValidateDirectories(tempConfig, filePath, lineCount);

                    if (!isDirectory)
                    {
                        ValidateActions(tempConfig, filePath, lineCount);

                        configActions.Add(tempConfig);
                        tempConfig = new PatcherConfigAction();
                    }

                    isDirectory = !isDirectory;
                }
                else
                {
                    if (isDirectory)
                    {
                        ValidateDirectoryPath(line, lineCount); 
                            
                        tempConfig.TempDirectories.Add(line);
                    }
                    else
                    {
                        tempConfig.TempActions.Add(line);
                    }                    
                }

                lineCount++;
            }

            //Finished loading lines.
            ValidateActions(tempConfig, filePath, lineCount);

            configActions.Add(tempConfig);

            return configActions;
        }

        private bool IsEmptyOrCommentLine(string line)
        {
            return string.IsNullOrWhiteSpace(line) || line.Trim().StartsWith("//");
        }

        private void ValidateDirectoryPath(string filePath, int line)
        {
            if (!filePath.ToLower().StartsWith("records"))
            {
                var message = string.Format(
                "Error in directory on '{0}'. Directory not starting with 'records'. Line: {1}",
                filePath, line - 1);
                throw new Exception(message);
            }
        }

        private void ValidateDirectories(PatcherConfigAction config, string filePath, int line)
        {
            if (!config.TempDirectories.Any())
            {
                var message = string.Format(
                    "Error in secuence on '{0}'. No directories before action separator. Line: {1}",
                    filePath, line - 1);
                throw new Exception(message);
            }        
        }

        private void ValidateActions(PatcherConfigAction config, string filePath, int line)
        {
            const string REGEX_SEVEN_PLUS_DIGITS = "(\\.)[0-9]{7}";

            if (!config.TempActions.Any())
            {
                var message = string.Format(
                    "Error in secuence on '{0}'. Directory without actions associated. Line: {1}",
                    filePath, line - 1);
                throw new Exception(message);
            }

            foreach (var action in config.TempActions)
            {
                int numberOfCommas = action.Where(x => (x == GlobalConstants.COMMA_CHAR)).Count();

                if ((numberOfCommas > 2) || ((numberOfCommas == 1) && action.EndsWith(GlobalConstants.COMMA_STRING)))
                {
                    var message = string.Format(
                        "Invalid number of commas in action: line text '{0}'. Line: {1}, File: '{2}'",
                        action, line - 1, filePath);
                    throw new Exception(message);                    
                }

                if (action.EndsWith(GlobalConstants.SEMICOLON_STRING))
                {
                    var message = string.Format(
                        "Action ends in semicolon: line text '{0}'. Line: {1}, File: '{2}'",
                        action, line - 1, filePath);
                    throw new Exception(message);                   
                }

                if (System.Text.RegularExpressions.Regex.IsMatch(action, REGEX_SEVEN_PLUS_DIGITS))
                {
                    var message = string.Format(
                        "Too many digits: line text '{0}'. Line: {1}, File: '{2}'",
                        action, line - 1, filePath);
                    throw new Exception(message);                
                }
            }

        }
    }
}
