using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TQDataLoader.Data;
using TQDataLoader.Data.Xml;
using TQDataLoader.Code.Error;
using TQDataLoader.Data.Error;
using TQDataLoader.Code;

namespace TQDataLoader
{
    public partial class MainForm : Form
    {
        private FileCheckResult lastFileCheckResult;
        private ErrorHandler errorHandler;
        private CsvGenerator csvGenerator;
        private DataExtractionConfigList deConfigList;
        private ExcludedKeyList excludedKeyList;
        private DescriptionManager descriptionManager;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadConfiguration();

            LoadDynamicControls();

            //TestSerialize();
        }

        private void LoadConfiguration()
        {
            errorHandler = new ErrorHandler();

            descriptionManager = new DescriptionManager();
           
            var xmlText = FileUtils.ReadFile(FileUtils.DataExtractionConfigFile);
            deConfigList = XmlUtils.Deserialize(xmlText, typeof(DataExtractionConfigList)) as DataExtractionConfigList;

            xmlText = FileUtils.ReadFile(FileUtils.ExcludedKeysConfigFile);
            excludedKeyList = XmlUtils.Deserialize(xmlText, typeof(ExcludedKeyList)) as ExcludedKeyList;
        }

        private void LoadDynamicControls()
        {
            if (deConfigList != null)
            {
                cmbExtrationList.Items.AddRange(deConfigList.Items.Select(x => x.Description).ToArray());
                cmbExtrationList.SelectedItem = cmbExtrationList.Items[0];
            }
        }

        private void TestSerialize()         
        {
            var dataConfig = new DataExtractionConfig();

            dataConfig.Description = "Description Test";
            dataConfig.TargetDirectories = new List<string>() {
                "item\\equipmentweapon",
                "xpack2\\item\\equipmentweapons",
                "xpack\\item\\equipmentweapons"
            };
            dataConfig.FileSearchMask = "pepe*.dbr";
            dataConfig.ResourceType = ResourceTypeEnum.CommonEquipment;

            dataConfig.ResourceTranslatedValues = new List<string>() {
                "tag001",
                "tag002"
            };

            var xmlStr = XmlUtils.SerializeObject(dataConfig, typeof(DataExtractionConfig));        
        }

        private string DirectoryFullPath
        {
            get
            {
                return FileUtils.GetDbrDirectoryPath(txtFolderValue.Text);
            }
        }

        private void directorySourceFolderBrowser_HelpRequest(object sender, EventArgs e)
        {
            // not used.
        }

        private void printNotification(string message)
        {
            txtNotifications.Text += Environment.NewLine + message;
            //MessageBox.Show(message);
        }

        private void checkPrintNotification(bool result, ErrorTypeEnum errorEnum)
        {
            if (result)
            {
                var errorItem = errorHandler.GetError(errorEnum);
                printNotification(errorItem.ErrorMessage);
            }
        }

        private bool checkAndNotifyIsEmpty(string path) {

            var result = string.IsNullOrWhiteSpace(path);

            checkPrintNotification(result, ErrorTypeEnum.NotValidDirectoryPath);

            return result;  
        }

        private FileCheckResult checkPathFileSystem(string path)
        {

            var dirs = FileUtils.GetDirectories(path);
            var dirCount = dirs.Length;

            var files = FileUtils.GetDbrFiles(path);
            var fileCount = files.Length;

            var isValid = (dirCount + fileCount) > 0;

            return new FileCheckResult(fileCount, dirCount, isValid);
        }

        private bool checkAndNotifyFilesOk()
        {
            lastFileCheckResult = checkPathFileSystem(DirectoryFullPath);
            
            var result = lastFileCheckResult.IsValid;

            checkPrintNotification(!result, ErrorTypeEnum.NoFilesDirectoriesFound);

            return result;
        }

        private void btnSetFolder_Click(object sender, EventArgs e)
        {
            directorySourceFolderBrowser.SelectedPath = DirectoryFullPath;

            DialogResult result = directorySourceFolderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {

                if (checkAndNotifyIsEmpty(directorySourceFolderBrowser.SelectedPath)) return;

                txtFolderValue.Text = directorySourceFolderBrowser.SelectedPath.Replace(ConfigUtils.DbrRecordsSourcePath + GlobalConstants.DIRECTORY_SEPARATOR, string.Empty);

                if (checkAndNotifyFilesOk())
                {
                    printNotification(string.Format(ConfigUtils.Messages.FileItemsFound, lastFileCheckResult.DirectoryCount.ToString(), lastFileCheckResult.FileCount.ToString()));
                }
            }
        }

        private void GenerateCsv(List<string> uniqueDataEntries, List<DbrDataItem> parsedItemList, ResourceTypeEnum rType, string fileName = GlobalConstants.EMPTY_STRING)
        {
            csvGenerator = new CsvGenerator(uniqueDataEntries, parsedItemList, rType);
            csvGenerator.GenerateCsvLines();
            csvGenerator.SaveFile(fileName);
            printNotification(ConfigUtils.Messages.GenerationSuccess);        
        }

        private void btnExtractData_Click(object sender, EventArgs e)
        {
            var isEmpty = checkAndNotifyIsEmpty(DirectoryFullPath);

            var isFilesOk = checkAndNotifyFilesOk();

            var config = new DataExtractionConfig()
            {
                IncludeSubfolders = chkIncludeFoldersRecursive.Checked
            };
            config.TargetDirectories.Add(txtFolderValue.Text);

            if (checkAndNotifyFilesOk())
            {
                var dbrLoader = new Code.DbrDataLoader(config, descriptionManager, excludedKeyList.Items);
                GenerateCsv(dbrLoader.uniqueDataEntries, dbrLoader.parsedItemList, config.ResourceType);
            }
        }

        private void btnExtractConfigData_Click(object sender, EventArgs e)
        {
            var currentConfigItem = deConfigList.Items.FirstOrDefault(x => x.Description == cmbExtrationList.SelectedItem.ToString());

            if (currentConfigItem == null)
            {
                printNotification("Configuration not Selected");
                return;
            }

            var dbrLoader = new Code.DbrDataLoader(currentConfigItem, descriptionManager, excludedKeyList.Items);
            var fileName = currentConfigItem.Description.ToLower().Replace(GlobalConstants.SPACE_STRING, string.Empty);
            GenerateCsv(dbrLoader.uniqueDataEntries, dbrLoader.parsedItemList, currentConfigItem.ResourceType, fileName);
        }

        private void BtnPatcher_Click(object sender, EventArgs e)
        {
            this.Hide();
            var patcherForm = new PatcherForm();
            patcherForm.ShowDialog();
            this.Close();
        }
    }
}
