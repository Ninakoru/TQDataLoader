namespace TQDataLoader
{
    partial class MainForm
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
            this.directorySourceFolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.txtFolderValue = new System.Windows.Forms.TextBox();
            this.btnSetFolder = new System.Windows.Forms.Button();
            this.lblSourceDir = new System.Windows.Forms.Label();
            this.chkIncludeFoldersRecursive = new System.Windows.Forms.CheckBox();
            this.btnExtractData = new System.Windows.Forms.Button();
            this.txtNotifications = new System.Windows.Forms.TextBox();
            this.lblLogTitle = new System.Windows.Forms.Label();
            this.cmbExtrationList = new System.Windows.Forms.ComboBox();
            this.lblConfigExtractions = new System.Windows.Forms.Label();
            this.btnExtractConfigData = new System.Windows.Forms.Button();
            this.BtnPatcher = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // directorySourceFolderBrowser
            // 
            this.directorySourceFolderBrowser.HelpRequest += new System.EventHandler(this.directorySourceFolderBrowser_HelpRequest);
            // 
            // txtFolderValue
            // 
            this.txtFolderValue.Location = new System.Drawing.Point(21, 50);
            this.txtFolderValue.Name = "txtFolderValue";
            this.txtFolderValue.Size = new System.Drawing.Size(312, 20);
            this.txtFolderValue.TabIndex = 0;
            this.txtFolderValue.Text = "item\\equipmentweapon";
            // 
            // btnSetFolder
            // 
            this.btnSetFolder.Location = new System.Drawing.Point(339, 50);
            this.btnSetFolder.Name = "btnSetFolder";
            this.btnSetFolder.Size = new System.Drawing.Size(79, 19);
            this.btnSetFolder.TabIndex = 1;
            this.btnSetFolder.Text = "Set Folder";
            this.btnSetFolder.UseVisualStyleBackColor = true;
            this.btnSetFolder.Click += new System.EventHandler(this.btnSetFolder_Click);
            // 
            // lblSourceDir
            // 
            this.lblSourceDir.AutoSize = true;
            this.lblSourceDir.Location = new System.Drawing.Point(18, 34);
            this.lblSourceDir.Name = "lblSourceDir";
            this.lblSourceDir.Size = new System.Drawing.Size(148, 13);
            this.lblSourceDir.TabIndex = 2;
            this.lblSourceDir.Text = "Source Directory for operation";
            // 
            // chkIncludeFoldersRecursive
            // 
            this.chkIncludeFoldersRecursive.AutoSize = true;
            this.chkIncludeFoldersRecursive.Location = new System.Drawing.Point(24, 100);
            this.chkIncludeFoldersRecursive.Name = "chkIncludeFoldersRecursive";
            this.chkIncludeFoldersRecursive.Size = new System.Drawing.Size(162, 17);
            this.chkIncludeFoldersRecursive.TabIndex = 3;
            this.chkIncludeFoldersRecursive.Text = "Include Folders Recursively?";
            this.chkIncludeFoldersRecursive.UseVisualStyleBackColor = true;
            // 
            // btnExtractData
            // 
            this.btnExtractData.Location = new System.Drawing.Point(24, 146);
            this.btnExtractData.Name = "btnExtractData";
            this.btnExtractData.Size = new System.Drawing.Size(149, 25);
            this.btnExtractData.TabIndex = 4;
            this.btnExtractData.Text = "Extract Folder Data to Csv";
            this.btnExtractData.UseVisualStyleBackColor = true;
            this.btnExtractData.Click += new System.EventHandler(this.btnExtractData_Click);
            // 
            // txtNotifications
            // 
            this.txtNotifications.BackColor = System.Drawing.SystemColors.Info;
            this.txtNotifications.Location = new System.Drawing.Point(27, 208);
            this.txtNotifications.Multiline = true;
            this.txtNotifications.Name = "txtNotifications";
            this.txtNotifications.Size = new System.Drawing.Size(745, 154);
            this.txtNotifications.TabIndex = 5;
            // 
            // lblLogTitle
            // 
            this.lblLogTitle.AutoSize = true;
            this.lblLogTitle.Location = new System.Drawing.Point(24, 192);
            this.lblLogTitle.Name = "lblLogTitle";
            this.lblLogTitle.Size = new System.Drawing.Size(25, 13);
            this.lblLogTitle.TabIndex = 6;
            this.lblLogTitle.Text = "Log";
            // 
            // cmbExtrationList
            // 
            this.cmbExtrationList.FormattingEnabled = true;
            this.cmbExtrationList.Location = new System.Drawing.Point(513, 96);
            this.cmbExtrationList.Name = "cmbExtrationList";
            this.cmbExtrationList.Size = new System.Drawing.Size(259, 21);
            this.cmbExtrationList.TabIndex = 7;
            // 
            // lblConfigExtractions
            // 
            this.lblConfigExtractions.AutoSize = true;
            this.lblConfigExtractions.Location = new System.Drawing.Point(510, 80);
            this.lblConfigExtractions.Name = "lblConfigExtractions";
            this.lblConfigExtractions.Size = new System.Drawing.Size(122, 13);
            this.lblConfigExtractions.TabIndex = 8;
            this.lblConfigExtractions.Text = "Configurated Extractions";
            // 
            // btnExtractConfigData
            // 
            this.btnExtractConfigData.Location = new System.Drawing.Point(513, 136);
            this.btnExtractConfigData.Name = "btnExtractConfigData";
            this.btnExtractConfigData.Size = new System.Drawing.Size(149, 25);
            this.btnExtractConfigData.TabIndex = 9;
            this.btnExtractConfigData.Text = "Extract Config Data to Csv";
            this.btnExtractConfigData.UseVisualStyleBackColor = true;
            this.btnExtractConfigData.Click += new System.EventHandler(this.btnExtractConfigData_Click);
            // 
            // BtnPatcher
            // 
            this.BtnPatcher.BackColor = System.Drawing.SystemColors.ControlDark;
            this.BtnPatcher.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.BtnPatcher.Location = new System.Drawing.Point(631, 12);
            this.BtnPatcher.Name = "BtnPatcher";
            this.BtnPatcher.Size = new System.Drawing.Size(141, 23);
            this.BtnPatcher.TabIndex = 10;
            this.BtnPatcher.Text = "Jump to Patcher";
            this.BtnPatcher.UseVisualStyleBackColor = false;
            this.BtnPatcher.Click += new System.EventHandler(this.BtnPatcher_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(800, 381);
            this.Controls.Add(this.BtnPatcher);
            this.Controls.Add(this.btnExtractConfigData);
            this.Controls.Add(this.lblConfigExtractions);
            this.Controls.Add(this.cmbExtrationList);
            this.Controls.Add(this.lblLogTitle);
            this.Controls.Add(this.txtNotifications);
            this.Controls.Add(this.btnExtractData);
            this.Controls.Add(this.chkIncludeFoldersRecursive);
            this.Controls.Add(this.lblSourceDir);
            this.Controls.Add(this.btnSetFolder);
            this.Controls.Add(this.txtFolderValue);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Csv Generator";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog directorySourceFolderBrowser;
        private System.Windows.Forms.TextBox txtFolderValue;
        private System.Windows.Forms.Button btnSetFolder;
        private System.Windows.Forms.Label lblSourceDir;
        private System.Windows.Forms.CheckBox chkIncludeFoldersRecursive;
        private System.Windows.Forms.Button btnExtractData;
        private System.Windows.Forms.TextBox txtNotifications;
        private System.Windows.Forms.Label lblLogTitle;
        private System.Windows.Forms.ComboBox cmbExtrationList;
        private System.Windows.Forms.Label lblConfigExtractions;
        private System.Windows.Forms.Button btnExtractConfigData;
        private System.Windows.Forms.Button BtnPatcher;
    }
}

