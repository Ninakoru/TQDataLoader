namespace TQDataLoader
{
    partial class PatcherForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSourceDir = new System.Windows.Forms.Label();
            this.txtSourceDir = new System.Windows.Forms.TextBox();
            this.lblDestinationDir = new System.Windows.Forms.Label();
            this.txtDestinationDir = new System.Windows.Forms.TextBox();
            this.lblConfigDir = new System.Windows.Forms.Label();
            this.txtConfigDir = new System.Windows.Forms.TextBox();
            this.btnSetSourceFolder = new System.Windows.Forms.Button();
            this.btnSetDestinationFolder = new System.Windows.Forms.Button();
            this.btnSetConfigFolder = new System.Windows.Forms.Button();
            this.directorySourceFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.btnCreatePatch = new System.Windows.Forms.Button();
            this.lblLogTitle = new System.Windows.Forms.Label();
            this.txtNotifications = new System.Windows.Forms.TextBox();
            this.BtnDataLoader = new System.Windows.Forms.Button();
            this.chkDeleteDestination = new System.Windows.Forms.CheckBox();
            this.CmbPresets = new System.Windows.Forms.ComboBox();
            this.LblPresets = new System.Windows.Forms.Label();
            this.chkPreserveLog = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // lblSourceDir
            // 
            this.lblSourceDir.AutoSize = true;
            this.lblSourceDir.Location = new System.Drawing.Point(18, 67);
            this.lblSourceDir.Name = "lblSourceDir";
            this.lblSourceDir.Size = new System.Drawing.Size(86, 13);
            this.lblSourceDir.TabIndex = 2;
            this.lblSourceDir.Text = "Source Directory";
            // 
            // txtSourceDir
            // 
            this.txtSourceDir.Location = new System.Drawing.Point(21, 83);
            this.txtSourceDir.Name = "txtSourceDir";
            this.txtSourceDir.Size = new System.Drawing.Size(490, 20);
            this.txtSourceDir.TabIndex = 0;
            this.txtSourceDir.Text = "D:\\Dev\\Titan Quest Workshop\\AtlantisDB_2_9";
            // 
            // lblDestinationDir
            // 
            this.lblDestinationDir.AutoSize = true;
            this.lblDestinationDir.Location = new System.Drawing.Point(18, 115);
            this.lblDestinationDir.Name = "lblDestinationDir";
            this.lblDestinationDir.Size = new System.Drawing.Size(105, 13);
            this.lblDestinationDir.TabIndex = 10;
            this.lblDestinationDir.Text = "Destination Directory";
            // 
            // txtDestinationDir
            // 
            this.txtDestinationDir.Location = new System.Drawing.Point(21, 131);
            this.txtDestinationDir.Name = "txtDestinationDir";
            this.txtDestinationDir.Size = new System.Drawing.Size(490, 20);
            this.txtDestinationDir.TabIndex = 11;
            this.txtDestinationDir.Text = "D:\\Documents\\My Games\\Titan Quest - Immortal Throne\\Working\\CustomMaps\\Itemizatio" +
    "n-Quest\\database";
            // 
            // lblConfigDir
            // 
            this.lblConfigDir.AutoSize = true;
            this.lblConfigDir.Location = new System.Drawing.Point(18, 165);
            this.lblConfigDir.Name = "lblConfigDir";
            this.lblConfigDir.Size = new System.Drawing.Size(114, 13);
            this.lblConfigDir.TabIndex = 13;
            this.lblConfigDir.Text = "Configuration Directory";
            // 
            // txtConfigDir
            // 
            this.txtConfigDir.Location = new System.Drawing.Point(21, 181);
            this.txtConfigDir.Name = "txtConfigDir";
            this.txtConfigDir.Size = new System.Drawing.Size(490, 20);
            this.txtConfigDir.TabIndex = 14;
            this.txtConfigDir.Text = "D:\\Documents\\Visual Studio 2012\\Projects\\TQDataLoader\\Configuration";
            // 
            // btnSetSourceFolder
            // 
            this.btnSetSourceFolder.Location = new System.Drawing.Point(517, 83);
            this.btnSetSourceFolder.Name = "btnSetSourceFolder";
            this.btnSetSourceFolder.Size = new System.Drawing.Size(79, 19);
            this.btnSetSourceFolder.TabIndex = 1;
            this.btnSetSourceFolder.Text = "Set Folder";
            this.btnSetSourceFolder.UseVisualStyleBackColor = true;
            this.btnSetSourceFolder.Click += new System.EventHandler(this.btnSetFolder_Click);
            // 
            // btnSetDestinationFolder
            // 
            this.btnSetDestinationFolder.Location = new System.Drawing.Point(517, 132);
            this.btnSetDestinationFolder.Name = "btnSetDestinationFolder";
            this.btnSetDestinationFolder.Size = new System.Drawing.Size(79, 19);
            this.btnSetDestinationFolder.TabIndex = 12;
            this.btnSetDestinationFolder.Text = "Set Folder";
            this.btnSetDestinationFolder.UseVisualStyleBackColor = true;
            this.btnSetDestinationFolder.Click += new System.EventHandler(this.btnSetDestinationFolder_Click);
            // 
            // btnSetConfigFolder
            // 
            this.btnSetConfigFolder.Location = new System.Drawing.Point(517, 181);
            this.btnSetConfigFolder.Name = "btnSetConfigFolder";
            this.btnSetConfigFolder.Size = new System.Drawing.Size(79, 19);
            this.btnSetConfigFolder.TabIndex = 15;
            this.btnSetConfigFolder.Text = "Set Folder";
            this.btnSetConfigFolder.UseVisualStyleBackColor = true;
            this.btnSetConfigFolder.Click += new System.EventHandler(this.btnSetConfigFolder_Click);
            // 
            // directorySourceFolderBrowser
            // 
            this.directorySourceFolderBrowser.HelpRequest += new System.EventHandler(this.directorySourceFolderBrowser_HelpRequest);
            // 
            // btnCreatePatch
            // 
            this.btnCreatePatch.Location = new System.Drawing.Point(623, 147);
            this.btnCreatePatch.Name = "btnCreatePatch";
            this.btnCreatePatch.Size = new System.Drawing.Size(149, 25);
            this.btnCreatePatch.TabIndex = 4;
            this.btnCreatePatch.Text = "Create Patch Files";
            this.btnCreatePatch.UseVisualStyleBackColor = true;
            this.btnCreatePatch.Click += new System.EventHandler(this.btnCreatePatch_Click);
            // 
            // lblLogTitle
            // 
            this.lblLogTitle.AutoSize = true;
            this.lblLogTitle.Location = new System.Drawing.Point(18, 237);
            this.lblLogTitle.Name = "lblLogTitle";
            this.lblLogTitle.Size = new System.Drawing.Size(25, 13);
            this.lblLogTitle.TabIndex = 6;
            this.lblLogTitle.Text = "Log";
            // 
            // txtNotifications
            // 
            this.txtNotifications.BackColor = System.Drawing.SystemColors.Info;
            this.txtNotifications.Location = new System.Drawing.Point(21, 253);
            this.txtNotifications.Multiline = true;
            this.txtNotifications.Name = "txtNotifications";
            this.txtNotifications.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtNotifications.Size = new System.Drawing.Size(751, 154);
            this.txtNotifications.TabIndex = 5;
            // 
            // BtnDataLoader
            // 
            this.BtnDataLoader.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BtnDataLoader.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnDataLoader.Location = new System.Drawing.Point(631, 12);
            this.BtnDataLoader.Name = "BtnDataLoader";
            this.BtnDataLoader.Size = new System.Drawing.Size(141, 23);
            this.BtnDataLoader.TabIndex = 16;
            this.BtnDataLoader.Text = "Jump to Data Loader";
            this.BtnDataLoader.UseVisualStyleBackColor = false;
            this.BtnDataLoader.Click += new System.EventHandler(this.BtnDataLoader_Click);
            // 
            // chkDeleteDestination
            // 
            this.chkDeleteDestination.AutoSize = true;
            this.chkDeleteDestination.Checked = true;
            this.chkDeleteDestination.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDeleteDestination.Location = new System.Drawing.Point(444, 18);
            this.chkDeleteDestination.Name = "chkDeleteDestination";
            this.chkDeleteDestination.Size = new System.Drawing.Size(152, 17);
            this.chkDeleteDestination.TabIndex = 17;
            this.chkDeleteDestination.Text = "Delete Files on Destination";
            this.chkDeleteDestination.UseVisualStyleBackColor = true;
            // 
            // CmbPresets
            // 
            this.CmbPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CmbPresets.FormattingEnabled = true;
            this.CmbPresets.Location = new System.Drawing.Point(21, 29);
            this.CmbPresets.Name = "CmbPresets";
            this.CmbPresets.Size = new System.Drawing.Size(273, 21);
            this.CmbPresets.TabIndex = 18;
            this.CmbPresets.SelectedIndexChanged += new System.EventHandler(this.CmbPresets_SelectedIndexChanged);
            // 
            // LblPresets
            // 
            this.LblPresets.AutoSize = true;
            this.LblPresets.Location = new System.Drawing.Point(18, 13);
            this.LblPresets.Name = "LblPresets";
            this.LblPresets.Size = new System.Drawing.Size(82, 13);
            this.LblPresets.TabIndex = 19;
            this.LblPresets.Text = "Patcher Presets";
            // 
            // chkPreserveLog
            // 
            this.chkPreserveLog.AutoSize = true;
            this.chkPreserveLog.Checked = true;
            this.chkPreserveLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPreserveLog.Location = new System.Drawing.Point(49, 236);
            this.chkPreserveLog.Name = "chkPreserveLog";
            this.chkPreserveLog.Size = new System.Drawing.Size(89, 17);
            this.chkPreserveLog.TabIndex = 20;
            this.chkPreserveLog.Text = "Preserve Log";
            this.chkPreserveLog.UseVisualStyleBackColor = true;
            // 
            // PatcherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 419);
            this.Controls.Add(this.chkPreserveLog);
            this.Controls.Add(this.LblPresets);
            this.Controls.Add(this.CmbPresets);
            this.Controls.Add(this.chkDeleteDestination);
            this.Controls.Add(this.BtnDataLoader);
            this.Controls.Add(this.btnSetConfigFolder);
            this.Controls.Add(this.txtConfigDir);
            this.Controls.Add(this.lblConfigDir);
            this.Controls.Add(this.btnSetDestinationFolder);
            this.Controls.Add(this.txtDestinationDir);
            this.Controls.Add(this.lblDestinationDir);
            this.Controls.Add(this.lblLogTitle);
            this.Controls.Add(this.txtNotifications);
            this.Controls.Add(this.btnCreatePatch);
            this.Controls.Add(this.lblSourceDir);
            this.Controls.Add(this.btnSetSourceFolder);
            this.Controls.Add(this.txtSourceDir);
            this.Name = "PatcherForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "File Patcher";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog directorySourceFolderBrowser;
        private System.Windows.Forms.TextBox txtSourceDir;
        private System.Windows.Forms.Button btnSetSourceFolder;
        private System.Windows.Forms.Label lblSourceDir;
        private System.Windows.Forms.Button btnCreatePatch;
        private System.Windows.Forms.TextBox txtNotifications;
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.Label lblDestinationDir;
        private System.Windows.Forms.TextBox txtDestinationDir;
        private System.Windows.Forms.Button btnSetDestinationFolder;
        private System.Windows.Forms.Label lblConfigDir;
        private System.Windows.Forms.TextBox txtConfigDir;
        private System.Windows.Forms.Button btnSetConfigFolder;
        private System.Windows.Forms.Button BtnDataLoader;
        private System.Windows.Forms.CheckBox chkDeleteDestination;
        private System.Windows.Forms.ComboBox CmbPresets;
        private System.Windows.Forms.Label LblPresets;
        private System.Windows.Forms.CheckBox chkPreserveLog;
    }
}

