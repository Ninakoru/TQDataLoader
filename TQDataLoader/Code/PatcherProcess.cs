using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TQDataLoader.Code
{
    public class PatcherProcess
    {
        private const int numFilesToNotify = 100;

        private PatcherConfigResult configResult;
        private PatcherConfiguration config;
        private TextBox logCtrl;
        private DbrTemplateManager dbrTemplateManager;
        private int actionPatchCounter;

        public PatcherProcess(PatcherConfiguration patcherConfiguration, TextBox logControl, DbrTemplateManager templateManager)
        {
            logCtrl = logControl;
            config = patcherConfiguration;
            dbrTemplateManager = templateManager;
        }

        public IList<string> DoProcess()
        {
            var result = new List<string>();

            configResult = config.LoadConfiguration();
            if (configResult.HasFailures)
            {
                return configResult.FailureMessages;
            }
            else
            {
                dbrTemplateManager.ParseTemplates(GetAllTemplatesFromTargetFiles());

                var failureProperties = VerifyAllConfigPropertiesAreValid();

                if (failureProperties.Any())
                {
                    return failureProperties;
                }
            }

            printNotification(string.Format("Configuration Loaded Successfully. Total of {0} elements to patch.", config.TotalFiles));

            new Thread(delegate()
            {
                GeneratePatchAndCopyNewFilesThread();
            }).Start();


            return result;
        }

        private IList<string> VerifyAllConfigPropertiesAreValid()
        {
            var result = new List<string>();

            foreach (var actionConfig in config.Configuration)
            {
                foreach (var action in actionConfig.Actions)
                {
                    if (!dbrTemplateManager.IsValidPropertyName(action.Property))
                    {
                        result.Add(action.Property);
                    }
                }
            }

            if (result.Count > 0)
            {
                return result.Distinct().Select(x => string.Format("Property not in Templates: '{0}'", x)).ToList();
            }

            return result;
        }

        private void GeneratePatchAndCopyNewFilesThread()
        {
            GeneratePatch();

            CopyNewFiles();           
        }

        private void printNotification(string message)
        {
            AddTextLineToControl(logCtrl, message);
        }

        // We must invoke the control due to thread restrictions.
        delegate void SetTextOnControl(Control controlToChange, string message);

        private void AddTextLineToControl(Control controlToChange, string message)
        {
            if (controlToChange.InvokeRequired)
            {
                SetTextOnControl del1 = new SetTextOnControl(AddTextLineToControl);
                controlToChange.Invoke(del1, controlToChange, message);
            }
            else
            {
                controlToChange.Text = message + Environment.NewLine + controlToChange.Text;
            }
        }

        private IList<string> GetAllTemplatesFromTargetFiles()
        {
            var result = new List<string>();

            foreach (var actionConfig in config.Configuration)
            {
                var fileData = FileUtils.ReadAllLines(actionConfig.TargetFile);

                var templateLine = fileData.Where(x => x.StartsWith(GlobalConstants.TEMPLATE_KEYNAME)).FirstOrDefault();

                if (templateLine != null)
                {
                    var template = templateLine
                        .Replace(GlobalConstants.TEMPLATE_KEYNAME + GlobalConstants.COMMA_STRING, string.Empty)
                        .Replace(GlobalConstants.COMMA_STRING, string.Empty)
                        .ToLower();

                    if (!string.IsNullOrWhiteSpace(template))
                    {
                        result.Add(template);
                    }
                }

            }

            result = result.Distinct().ToList();

            return result;
        }

        private void GeneratePatch()
        {
            actionPatchCounter = 0;

            foreach (var actionConfig in config.Configuration)
            {
                var fileData = FileUtils.ReadFile(actionConfig.TargetFile);

                foreach (var action in actionConfig.Actions)
                {
                    fileData = ReplaceOrAdd(fileData, action);
                }

                var resultFilePath = actionConfig.TargetFile.Replace(config.SourcePath, config.DestinationPath);

                if (FileUtils.SaveStringFile(fileData, resultFilePath))
                {
                    actionPatchCounter++;
                }

                if ((actionPatchCounter > 0) && ((actionPatchCounter % numFilesToNotify) == 0))
                {
                    printNotification(string.Format("Running... Patched {0} Files.", actionPatchCounter));
                }
            }

            printNotification(string.Format("Finished. Patched {0} Files.", actionPatchCounter));
        }

        private void CopyNewFiles()
        {
            var filesCopied = FileUtils.CopyAllFiles(config.NewFilesConfigPath, config.DestinationPath);
            printNotification(string.Format("Copied {0} original files.", filesCopied));
        }

        private string ReplaceOrAdd(string data, PatcherAction action)
        {
            var result = data;

            if (data.Contains(action.Property + GlobalConstants.COMMA_STRING))
            {
                var stringToReplace = data.Substring(data.IndexOf(action.Property + GlobalConstants.COMMA_STRING));

                if (stringToReplace.Contains(Environment.NewLine))
                {
                    stringToReplace = stringToReplace.Substring(0, stringToReplace.IndexOf(Environment.NewLine));
                }

                if (stringToReplace.EndsWith(GlobalConstants.COMMA_STRING))
                {
                    stringToReplace = stringToReplace.Remove(stringToReplace.Length - 1);
                }

                result = data.Replace(stringToReplace, action.FullValue);
            }
            else
            {
                result = action.FullValueWithComma + Environment.NewLine + data;
            }

            return result;
        }
    }
}
