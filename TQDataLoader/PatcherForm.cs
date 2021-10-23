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
    public partial class PatcherForm : Form
    {
        private ErrorHandler errorHandler;
        private PatcherFormPresetList presetsList;
        private DescriptionManager descriptionManager;

        #region private methods

        public PatcherForm()
        {
            InitializeComponent();
        }

        private void LoadConfiguration()
        {
            errorHandler = new ErrorHandler();

            descriptionManager = new DescriptionManager();
           
            var xmlText = FileUtils.ReadFile(FileUtils.PatcherPresetsConfigFile);
            presetsList = XmlUtils.Deserialize(xmlText, typeof(PatcherFormPresetList)) as PatcherFormPresetList;
        }

        private void LoadDynamicControls()
        {
            if (presetsList != null)
            {
                CmbPresets.Items.AddRange(presetsList.Items.Select(x => x.PresetName).ToArray());
                CmbPresets.SelectedItem = CmbPresets.Items[0];
            }
            else 
            {
                CmbPresets.Enabled = false;
            }
        }

        private string DirectoryFullPath
        {
            get
            {
                return FileUtils.GetDbrDirectoryPath(txtSourceDir.Text);
            }
        }

        private void directorySourceFolderBrowser_HelpRequest(object sender, EventArgs e)
        {
            // not used.
        }

        private void printNotification(string message)
        {
            txtNotifications.Text += Environment.NewLine + message;
        }

        private void checkPrintNotification(bool result, ErrorTypeEnum errorEnum)
        {
            if (result)
            {
                var errorItem = errorHandler.GetError(errorEnum);
                printNotification(errorItem.ErrorMessage);
            }
        }

        private bool checkPathEmptyAndExists(string path) {

            var result = string.IsNullOrWhiteSpace(path) || !FileUtils.DirectoryExists(path);

            checkPrintNotification(result, ErrorTypeEnum.NotValidDirectoryPath);

            return result;  
        }

        private void CleanNotifications()
        {
            txtNotifications.Text = string.Empty;
        }

        private void btnSetFolder_Click(object sender, EventArgs e)
        {
            ManageSetDirectory(txtSourceDir);
        }


        private void btnSetDestinationFolder_Click(object sender, EventArgs e)
        {
            ManageSetDirectory(txtDestinationDir);
        }

        private void btnSetConfigFolder_Click(object sender, EventArgs e)
        {
            ManageSetDirectory(txtConfigDir);
        }

        private void ManageSetDirectory(TextBox control)
        {
            directorySourceFolderBrowser.SelectedPath = control.Text;

            DialogResult result = directorySourceFolderBrowser.ShowDialog();

            if (result == DialogResult.OK)
            {
                if (checkPathEmptyAndExists(directorySourceFolderBrowser.SelectedPath)) return;

                control.Text = directorySourceFolderBrowser.SelectedPath;

                printNotification(ConfigUtils.Messages.DirectorySetSuccess);
            }
        }

        #endregion

        #region Form Events

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadConfiguration();

            LoadDynamicControls();
        }


        private void btnCreatePatch_Click(object sender, EventArgs e)
        {
            if (!chkPreserveLog.Checked)
            {
                CleanNotifications();
            }

            var templateManager = new DbrTemplateManager(FileUtils.DbrTemplatesDirectory);

            var patcherConfig = new PatcherConfiguration(txtConfigDir.Text, txtSourceDir.Text, txtDestinationDir.Text, chkDeleteDestination.Checked);

            var process = new PatcherProcess(patcherConfig, txtNotifications, templateManager);

            var processErrors = process.DoProcess();

            if (processErrors.Any())
            {
                printNotification("--Error in patching execution--");
                processErrors.ToList().ForEach(x => printNotification(x));
            }
            else
            {
                printNotification("--Patch process executed successfully--");
            }
        }

        private void BtnDataLoader_Click(object sender, EventArgs e)
        {
            this.Hide();
            var loaderForm = new MainForm();
            loaderForm.ShowDialog();
            this.Close();
        }

        private void CmbPresets_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedName = CmbPresets.SelectedItem as string;

            if (!string.IsNullOrWhiteSpace(selectedName))
            {
                var selectedPreset = presetsList.Items.FirstOrDefault(x => x.PresetName == selectedName);

                if (selectedPreset != null)
                {
                    txtSourceDir.Text = selectedPreset.SourceDir;
                    txtDestinationDir.Text = selectedPreset.DestinationDir;
                    txtConfigDir.Text = selectedPreset.ConfigurationDir;
                    chkDeleteDestination.Checked = selectedPreset.DeleteDestination;
                }
            }
        }

        #endregion
    }
}
