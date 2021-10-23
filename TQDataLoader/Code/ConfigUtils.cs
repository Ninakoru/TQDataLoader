using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TQDataLoader.Code
{
    public static class ConfigUtils
    {
        private const string CONFIG_KEY_OUTPUT_FILES_FOLDER = "OutputFilesFolder";
        private const string CONFIG_KEY_DATA_EXTRACTION_CONFIG_FILE = "DataExtractionConfigFile";
        private const string CONFIG_KEY_EXCLUDED_KEYS_CONFIG_FILE = "ExcludedKeysConfigFile";
        private const string CONFIG_KEY_OUTPUT_FILE_NAME = "DefaultOutputFileName";
        private const string CONFIG_KEY_DEFAULT_FILE_SEARCH_PATTERN = "DefaultFileSearchPattern";
        private const string CONFIG_KEY_DBR_RECORDS_SOURCE_PATH = "DbrRecordsSourcePath";
        private const string CONFIG_KEY_DBR_TEMPLATES_DIRECTORY = "DbrTemplatesDirectory";
        private const string CONFIG_KEY_EXTRACT_TEXT_RESOURCES = "ExtractTextResources";
        private const string CONFIG_KEY_TEXT_RESOURCES_FILE = "TextResourcesArcFile";
        private const string CONFIG_KEY_TEXT_RESOURCES_OUTPUT_DIRECTORY = "TextResourcesOutputDirectory";
        private const string CONFIG_KEY_PATCHER_PRESETS_CONFIG_FILE = "PatcherPresetsConfigFile";
        private const string CONFIG_KEY_MESSAGES_FILE_NAME_EMPTY = "Messages.FileNameEmpty";
        private const string CONFIG_KEY_MESSAGES_FILE_ITEMS_FOUND = "Messages.FileItemsFound";
        private const string CONFIG_KEY_MESSAGES_GENERATION_SUCCESS = "Messages.GenerationSuccess";
        private const string CONFIG_KEY_MESSAGES_DIRECTORY_SET_SUCCESS = "Messages.DirectorySetSuccess";

        public static string OutputFilesFolder
        {
            get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_OUTPUT_FILES_FOLDER); }
        }

        public static string DataExtractionConfigFile
        {
            get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_DATA_EXTRACTION_CONFIG_FILE); }
        }

        public static string ExcludedKeysConfigFile
        {
            get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_EXCLUDED_KEYS_CONFIG_FILE); }
        }

        public static string OutputFileName
        {
            get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_OUTPUT_FILE_NAME); }
        }

        public static string DefaultFileSearchPattern
        {
            get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_DEFAULT_FILE_SEARCH_PATTERN); }
        }

        public static string DbrRecordsSourcePath
        {
            get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_DBR_RECORDS_SOURCE_PATH); }
        }

        public static string DbrTemplatesDirectory
        {
            get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_DBR_TEMPLATES_DIRECTORY); }
        }

        public static string TextResourcesFile
        {
            get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_TEXT_RESOURCES_FILE); }
        }

        public static string TextResourcesOutputDirectory
        {
            get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_TEXT_RESOURCES_OUTPUT_DIRECTORY); }
        }

        public static string PatcherPresetsConfigFile
        {
            get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_PATCHER_PRESETS_CONFIG_FILE); }
        }

        public static bool ExtractTextResources
        {
            get 
            {
                bool extract = false;
                return bool.TryParse(ConfigurationManager.AppSettings.Get(CONFIG_KEY_EXTRACT_TEXT_RESOURCES), out extract); 
            }
        }

        public static class Messages
        {
            public static string FileNameEmpty
            {
                get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_MESSAGES_FILE_NAME_EMPTY); }
            }

            public static string FileItemsFound
            {
                get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_MESSAGES_FILE_ITEMS_FOUND); }
            }

            public static string GenerationSuccess
            {
                get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_MESSAGES_GENERATION_SUCCESS); }
            }

            public static string DirectorySetSuccess
            {
                get { return ConfigurationManager.AppSettings.Get(CONFIG_KEY_MESSAGES_DIRECTORY_SET_SUCCESS); }
            }
        }
    }
}
