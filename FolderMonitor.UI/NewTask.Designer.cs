namespace FolderMonitor.UI
{
    partial class NewTask
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
            this.components = new System.ComponentModel.Container();
            this.targetLabel = new System.Windows.Forms.Label();
            this.sourceLabel = new System.Windows.Forms.Label();
            this.sourceFolderTextBox = new System.Windows.Forms.TextBox();
            this.targetFolderTextBox = new System.Windows.Forms.TextBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.browseSourceFolderButton = new System.Windows.Forms.Button();
            this.browseTargetFolderButton = new System.Windows.Forms.Button();
            this.robocopySwitchesCheckBox = new System.Windows.Forms.CheckBox();
            this.robocopySwitchesTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.noneRadioButton = new System.Windows.Forms.RadioButton();
            this.allRadioButton = new System.Windows.Forms.RadioButton();
            this.aclsOnlyRadioButton = new System.Windows.Forms.RadioButton();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // targetLabel
            // 
            this.targetLabel.AutoSize = true;
            this.targetLabel.Location = new System.Drawing.Point(47, 124);
            this.targetLabel.Name = "targetLabel";
            this.targetLabel.Size = new System.Drawing.Size(74, 13);
            this.targetLabel.TabIndex = 12;
            this.targetLabel.Text = "Target folder:";
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(47, 17);
            this.sourceLabel.Name = "sourceLabel";
            this.sourceLabel.Size = new System.Drawing.Size(75, 13);
            this.sourceLabel.TabIndex = 8;
            this.sourceLabel.Text = "Source folder:";
            // 
            // sourceFolderTextBox
            // 
            this.sourceFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sourceFolderTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.sourceFolderTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.sourceFolderTextBox.Location = new System.Drawing.Point(50, 41);
            this.sourceFolderTextBox.MaxLength = 1000;
            this.sourceFolderTextBox.Name = "sourceFolderTextBox";
            this.sourceFolderTextBox.Size = new System.Drawing.Size(322, 20);
            this.sourceFolderTextBox.TabIndex = 0;
            this.sourceFolderTextBox.TextChanged += new System.EventHandler(this.sourceFolderTextBox_TextChanged);
            // 
            // targetFolderTextBox
            // 
            this.targetFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetFolderTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.targetFolderTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.targetFolderTextBox.Location = new System.Drawing.Point(50, 148);
            this.targetFolderTextBox.MaxLength = 1000;
            this.targetFolderTextBox.Name = "targetFolderTextBox";
            this.targetFolderTextBox.Size = new System.Drawing.Size(322, 20);
            this.targetFolderTextBox.TabIndex = 3;
            this.targetFolderTextBox.TextChanged += new System.EventHandler(this.targetFolderTextBox_TextChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(47, 64);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(104, 13);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Authorize Required?";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(12, 107);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(408, 2);
            this.label1.TabIndex = 18;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(47, 171);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(104, 13);
            this.linkLabel2.TabIndex = 5;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Authorize Required?";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            this.errorProvider1.RightToLeft = true;
            // 
            // button4
            // 
            this.button4.Enabled = false;
            this.button4.Image = global::FolderMonitor.UI.Properties.Resources.data_forbidden;
            this.button4.Location = new System.Drawing.Point(249, 64);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(123, 23);
            this.button4.TabIndex = 19;
            this.button4.Text = "Excluded Items..";
            this.button4.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.button3.ForeColor = System.Drawing.Color.Blue;
            this.button3.Image = global::FolderMonitor.UI.Properties.Resources.text;
            this.button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button3.Location = new System.Drawing.Point(185, 367);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 25);
            this.button3.TabIndex = 6;
            this.button3.Text = "Test";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.Green;
            this.button2.Image = global::FolderMonitor.UI.Properties.Resources.check;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(266, 367);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 7;
            this.button2.Text = "Ok";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.button1.ForeColor = System.Drawing.Color.Red;
            this.button1.Image = global::FolderMonitor.UI.Properties.Resources.close;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(347, 367);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 8;
            this.button1.Text = "Cancel";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::FolderMonitor.UI.Properties.Resources.data_into24;
            this.pictureBox2.Location = new System.Drawing.Point(12, 119);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FolderMonitor.UI.Properties.Resources.data24;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(24, 24);
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // browseSourceFolderButton
            // 
            this.browseSourceFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseSourceFolderButton.Image = global::FolderMonitor.UI.Properties.Resources.folder_view;
            this.browseSourceFolderButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.browseSourceFolderButton.Location = new System.Drawing.Point(378, 38);
            this.browseSourceFolderButton.Name = "browseSourceFolderButton";
            this.browseSourceFolderButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.browseSourceFolderButton.Size = new System.Drawing.Size(44, 25);
            this.browseSourceFolderButton.TabIndex = 1;
            this.browseSourceFolderButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.browseSourceFolderButton.UseVisualStyleBackColor = true;
            this.browseSourceFolderButton.Click += new System.EventHandler(this.browseSourceFolderButton_Click);
            // 
            // browseTargetFolderButton
            // 
            this.browseTargetFolderButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.browseTargetFolderButton.Image = global::FolderMonitor.UI.Properties.Resources.folder_view;
            this.browseTargetFolderButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.browseTargetFolderButton.Location = new System.Drawing.Point(378, 146);
            this.browseTargetFolderButton.Name = "browseTargetFolderButton";
            this.browseTargetFolderButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.browseTargetFolderButton.Size = new System.Drawing.Size(44, 23);
            this.browseTargetFolderButton.TabIndex = 4;
            this.browseTargetFolderButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.browseTargetFolderButton.UseVisualStyleBackColor = true;
            this.browseTargetFolderButton.Click += new System.EventHandler(this.browseTargetFolderButton_Click);
            // 
            // robocopySwitchesCheckBox
            // 
            this.robocopySwitchesCheckBox.AutoSize = true;
            this.robocopySwitchesCheckBox.Location = new System.Drawing.Point(54, 299);
            this.robocopySwitchesCheckBox.Name = "robocopySwitchesCheckBox";
            this.robocopySwitchesCheckBox.Size = new System.Drawing.Size(161, 17);
            this.robocopySwitchesCheckBox.TabIndex = 20;
            this.robocopySwitchesCheckBox.Text = "Custom Robocopy switches:";
            this.robocopySwitchesCheckBox.UseVisualStyleBackColor = true;
            this.robocopySwitchesCheckBox.CheckedChanged += new System.EventHandler(this.robocopySwitchesCheckBox_CheckedChanged);
            // 
            // robocopySwitchesTextBox
            // 
            this.robocopySwitchesTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.robocopySwitchesTextBox.Enabled = false;
            this.robocopySwitchesTextBox.Location = new System.Drawing.Point(54, 322);
            this.robocopySwitchesTextBox.MaxLength = 1000;
            this.robocopySwitchesTextBox.Name = "robocopySwitchesTextBox";
            this.robocopySwitchesTextBox.Size = new System.Drawing.Size(322, 20);
            this.robocopySwitchesTextBox.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(14, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(408, 2);
            this.label2.TabIndex = 22;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::FolderMonitor.UI.Properties.Resources.history32;
            this.pictureBox3.Location = new System.Drawing.Point(12, 230);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(32, 32);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox3.TabIndex = 23;
            this.pictureBox3.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.noneRadioButton);
            this.groupBox1.Controls.Add(this.allRadioButton);
            this.groupBox1.Controls.Add(this.aclsOnlyRadioButton);
            this.groupBox1.Location = new System.Drawing.Point(58, 235);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(362, 58);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Copy extended NTFS attributes";
            // 
            // noneRadioButton
            // 
            this.noneRadioButton.AutoSize = true;
            this.noneRadioButton.Checked = true;
            this.noneRadioButton.Location = new System.Drawing.Point(10, 25);
            this.noneRadioButton.Name = "noneRadioButton";
            this.noneRadioButton.Size = new System.Drawing.Size(50, 17);
            this.noneRadioButton.TabIndex = 0;
            this.noneRadioButton.TabStop = true;
            this.noneRadioButton.Text = "None";
            this.noneRadioButton.UseVisualStyleBackColor = true;
            // 
            // allRadioButton
            // 
            this.allRadioButton.AutoSize = true;
            this.allRadioButton.Location = new System.Drawing.Point(188, 25);
            this.allRadioButton.Name = "allRadioButton";
            this.allRadioButton.Size = new System.Drawing.Size(36, 17);
            this.allRadioButton.TabIndex = 2;
            this.allRadioButton.Text = "All";
            this.allRadioButton.UseVisualStyleBackColor = true;
            // 
            // aclsOnlyRadioButton
            // 
            this.aclsOnlyRadioButton.AutoSize = true;
            this.aclsOnlyRadioButton.Location = new System.Drawing.Point(85, 25);
            this.aclsOnlyRadioButton.Name = "aclsOnlyRadioButton";
            this.aclsOnlyRadioButton.Size = new System.Drawing.Size(72, 17);
            this.aclsOnlyRadioButton.TabIndex = 1;
            this.aclsOnlyRadioButton.Text = "ACLs only";
            this.aclsOnlyRadioButton.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(12, 375);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 17);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "Don\'t validat";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // NewTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(434, 402);
            this.ControlBox = false;
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.robocopySwitchesCheckBox);
            this.Controls.Add(this.robocopySwitchesTextBox);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.targetLabel);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.sourceLabel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.browseSourceFolderButton);
            this.Controls.Add(this.sourceFolderTextBox);
            this.Controls.Add(this.browseTargetFolderButton);
            this.Controls.Add(this.targetFolderTextBox);
            this.Name = "NewTask";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add New Task";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.NewTask_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label targetLabel;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label sourceLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button browseSourceFolderButton;
        private System.Windows.Forms.TextBox sourceFolderTextBox;
        private System.Windows.Forms.Button browseTargetFolderButton;
        private System.Windows.Forms.TextBox targetFolderTextBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox robocopySwitchesCheckBox;
        private System.Windows.Forms.TextBox robocopySwitchesTextBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton noneRadioButton;
        private System.Windows.Forms.RadioButton allRadioButton;
        private System.Windows.Forms.RadioButton aclsOnlyRadioButton;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}

