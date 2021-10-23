using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TQDataLoader.Code
{
    public static class FileUtils
    {
        private static string _executingDirectory;

        // UTF-8 without bit order mark.
        private static Encoding utf8noBom = new UTF8Encoding(false);

        private static string ExecutingDirectory
        {
            get
            {
                if (_executingDirectory == null)
                {
                    var file_info = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    _executingDirectory = file_info.Directory.ToString();                
                }

                return _executingDirectory;           
            }
        }

        public static string OutputDirectory
        {
            get { return TryFindDirectory(ConfigUtils.OutputFilesFolder); }
        }

        public static string DbrTemplatesDirectory
        {
            get { return TryFindDirectory(ConfigUtils.DbrTemplatesDirectory); }
        }

        public static string ExcludedKeysConfigFile
        {
            get { return TryFindFile(ConfigUtils.ExcludedKeysConfigFile); }
        }

        public static string DataExtractionConfigFile
        {
            get { return TryFindFile(ConfigUtils.DataExtractionConfigFile); }
        }

        public static string PatcherPresetsConfigFile
        {
            get { return TryFindFile(ConfigUtils.PatcherPresetsConfigFile); }
        }

        public static string TextResourcesFile
        {
            get { return TryFindFile(ConfigUtils.TextResourcesFile); }
        }

        public static string TextResourcesOutputDirectory
        {
            get { return CombinePathCustom(FileUtils.OutputDirectory, ConfigUtils.TextResourcesOutputDirectory); }
        }
    
        private static string TryFindFile(string path)
        {
            string testPath = string.Empty;

            if (File.Exists(path))
            {
                return path;
            }

            testPath = GetFileFullPathFromExecutingDir(path);

            if (File.Exists(testPath))
            {
                return testPath;
            }

            testPath = CombinePathCustom(OutputDirectory, path);

            if (File.Exists(testPath))
            {
                return testPath;
            }

            return string.Empty;
        }


        private static string TryFindDirectory(string path)
        {
            string testPath = string.Empty;

            if (Directory.Exists(path))
            {
                return path;
            }

            testPath = GetFileFullPathFromExecutingDir(path);

            if (Directory.Exists(testPath))
            {
                return testPath;
            }

            testPath = CombinePathCustom(OutputDirectory, path);

            if (Directory.Exists(testPath))
            {
                return testPath;
            }

            return string.Empty;
        }

        private static string CombinePathCustom(string path1, string path2)
        {
            return Path.Combine(path1, GetPathNoAbsolute(path2));
        }

        private static string GetPathNoAbsolute(string path)
        {
            return path.StartsWith(GlobalConstants.DIRECTORY_SEPARATOR) ? path.Substring(1, path.Length - 1) : path;
        }

        public static string CsvOutputFilePath(string fileName)
        {
            return OutputDirectory + GlobalConstants.DIRECTORY_SEPARATOR + GetPathNoAbsolute(fileName) + GlobalConstants.CSV_FILE_EXTENSION;
        }

        public static string[] GetDbrFiles(string sourceDir, string fileSearchPattern = GlobalConstants.EMPTY_STRING)
        {
            var usedFsp = string.IsNullOrWhiteSpace(fileSearchPattern) ? ConfigUtils.DefaultFileSearchPattern : fileSearchPattern;

            if (!usedFsp.ToUpper().EndsWith(GlobalConstants.DBR_FILE_EXTENSION.ToUpper()))
            {
                throw new Exception(string.Format("FileUtils.GetDbrFiles: The search pattern '{0}' doesn't contain DBR file extension.", usedFsp));
            }

            return Directory.GetFiles(sourceDir, usedFsp);
        }

        public static string[] GetDirectories(string sourceDir)
        {
            return Directory.GetDirectories(sourceDir);
        }

        public static string GetDbrDirectoryPath(string partialPath)
        {
            return ConfigUtils.DbrRecordsSourcePath + GlobalConstants.DIRECTORY_SEPARATOR + partialPath;
        }

        public static string ReadFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new Exception(ConfigUtils.Messages.FileNameEmpty);
            }
            return File.ReadAllText(filePath, Encoding.UTF8);
        }

        public static string[] ReadAllLines(string fileName)         
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new Exception(ConfigUtils.Messages.FileNameEmpty);
            }
            var allTextString = File.ReadAllText(fileName);
            return allTextString.Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
        }

        public static void SaveOutputFile(string[] fileLines, string fileName = GlobalConstants.EMPTY_STRING)
        {
            var usingFileName = string.IsNullOrWhiteSpace(fileName) ? ConfigUtils.OutputFileName : fileName;

            if (!Directory.Exists(OutputDirectory))
            {
                Directory.CreateDirectory(OutputDirectory);
            }

            var outputFile = CsvOutputFilePath(usingFileName);

            if (File.Exists(outputFile))
            {
                File.Delete(outputFile);
            }

            File.AppendAllLines(outputFile, fileLines, Encoding.UTF8);
        }

        public static bool SaveStringFile(string fileData, string filePath)
        {
            var outputDirectory = new FileInfo(filePath).Directory.FullName;

            if (!Directory.Exists(outputDirectory))
            {
                Directory.CreateDirectory(outputDirectory);
            }

            if (File.Exists(filePath))
            {
                var oldFileText = File.ReadAllText(filePath, utf8noBom);

                if (fileData.Equals(oldFileText))
                {
                    return false;
                }
            }

            File.WriteAllText(filePath, fileData, utf8noBom);

            return true;
        }

        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        public static void DeleteAllInDir(string directoryPath)
        {
            var di = new DirectoryInfo(directoryPath);
            di.GetFiles().ToList().ForEach(x => x.Delete());
            di.GetDirectories().ToList().ForEach(x => x.Delete(true));
        }

        public static IList<string> GetAllFilesRecursive(string path, string extension = "*")
        {
            var result = new List<string>();

            var dir = new DirectoryInfo(path);

            IList<string> files = dir.GetFiles().Select(x => x.FullName).ToList();

            if (extension != "*")
            {
                files = files.Where(x => x.EndsWith("." + extension)).ToList();
            }

            result.AddRange(files);

            IList<string> directories = dir.GetDirectories().Select(x => x.FullName).ToList();

            if (directories.Any())
            {
                directories.ToList().ForEach(x => result.AddRange(GetAllFilesRecursive(x, extension)));
            }

            return result;
        }

        public static IList<string> GetAllMatchingPaths(string pattern)
        {
            char separator = Path.DirectorySeparatorChar;
            string[] parts = pattern.Split(separator);

            if (parts[0].Contains('*') || parts[0].Contains('?'))
                throw new Exception("path root must not have a wildcard");

            return GetAllMatchingPathsInternal(String.Join(separator.ToString(), parts.Skip(1)), parts[0]).ToList();
        }

        public static string GetCombinedPath(string path1, string path2)
        {
            return Path.Combine(path1, path2);
        }

        private static string GetFileFullPathFromExecutingDir(string configPath)
        {
            return ExecutingDirectory + GlobalConstants.DIRECTORY_SEPARATOR + GetPathNoAbsolute(configPath);
        }

        private static IEnumerable<string> GetAllMatchingPathsInternal(string pattern, string root)
        {
            char separator = Path.DirectorySeparatorChar;
            string[] parts = pattern.Split(separator);

            for (int i = 0; i < parts.Length; i++)
            {
                // if this part of the path is a wildcard that needs expanding
                if (parts[i].Contains('*') || parts[i].Contains('?'))
                {
                    // create an absolute path up to the current wildcard and check if it exists
                    var combined = root + separator + String.Join(separator.ToString(), parts.Take(i));
                    if (!Directory.Exists(combined))
                        return new string[0];

                    if (i == parts.Length - 1) // if this is the end of the path (a file name)
                    {
                        return Directory.EnumerateFiles(combined, parts[i], SearchOption.TopDirectoryOnly);
                    }
                    else // if this is in the middle of the path (a directory name)
                    {
                        var directories = Directory.EnumerateDirectories(combined, parts[i], SearchOption.TopDirectoryOnly);
                        var paths = directories.SelectMany(dir =>
                            GetAllMatchingPathsInternal(String.Join(separator.ToString(), parts.Skip(i + 1)), dir));
                        return paths;
                    }
                }
            }

            // if pattern ends in an absolute path with no wildcards in the filename
            var absolute = root + separator + String.Join(separator.ToString(), parts);
            if (File.Exists(absolute))
                return new[] { absolute };

            return new string[0];
        }

        internal static int CopyAllFiles(string sourcePath, string destinationPath)
        {
            int result = 0;

            if (!Directory.Exists(sourcePath) || !Directory.Exists(destinationPath)) return result;
            
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*", SearchOption.AllDirectories))
            {
                var destPath = newPath.Replace(sourcePath, destinationPath);
                var pathDir = Path.GetDirectoryName(destPath);
                if (!Directory.Exists(pathDir))
                {
                    Directory.CreateDirectory(pathDir);
                }
                if (!File.Exists(destPath) || !FileTextEquals(newPath, destPath))
                {
                    File.Copy(newPath, destPath, true);
                    result++;
                }
            }

            return result;
        }

        static bool FileTextEquals(string path1, string path2)
        {
            return File.ReadAllText(path1, utf8noBom) == File.ReadAllText(path2, utf8noBom);
        }
    }
}
