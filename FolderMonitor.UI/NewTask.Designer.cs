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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.endon_time = new System.Windows.Forms.DateTimePicker();
            this.endon_type = new System.Windows.Forms.ComboBox();
            this.endon_enabled = new System.Windows.Forms.CheckBox();
            this.intervalComboBox = new System.Windows.Forms.ComboBox();
            this.Starttime = new System.Windows.Forms.DateTimePicker();
            this.Startdate = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.schedul_enable = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // targetLabel
            // 
            this.targetLabel.AutoSize = true;
            this.targetLabel.Location = new System.Drawing.Point(40, 123);
            this.targetLabel.Name = "targetLabel";
            this.targetLabel.Size = new System.Drawing.Size(74, 13);
            this.targetLabel.TabIndex = 12;
            this.targetLabel.Text = "Target folder:";
            // 
            // sourceLabel
            // 
            this.sourceLabel.AutoSize = true;
            this.sourceLabel.Location = new System.Drawing.Point(40, 16);
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
            this.sourceFolderTextBox.Location = new System.Drawing.Point(43, 40);
            this.sourceFolderTextBox.MaxLength = 1000;
            this.sourceFolderTextBox.Name = "sourceFolderTextBox";
            this.sourceFolderTextBox.Size = new System.Drawing.Size(386, 20);
            this.sourceFolderTextBox.TabIndex = 0;
            this.sourceFolderTextBox.TextChanged += new System.EventHandler(this.sourceFolderTextBox_TextChanged);
            // 
            // targetFolderTextBox
            // 
            this.targetFolderTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.targetFolderTextBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.targetFolderTextBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.targetFolderTextBox.Location = new System.Drawing.Point(43, 147);
            this.targetFolderTextBox.MaxLength = 1000;
            this.targetFolderTextBox.Name = "targetFolderTextBox";
            this.targetFolderTextBox.Size = new System.Drawing.Size(386, 20);
            this.targetFolderTextBox.TabIndex = 3;
            this.targetFolderTextBox.TextChanged += new System.EventHandler(this.targetFolderTextBox_TextChanged);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(40, 63);
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
            this.label1.Location = new System.Drawing.Point(5, 106);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(472, 2);
            this.label1.TabIndex = 18;
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(40, 170);
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
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Enabled = false;
            this.button4.Image = global::FolderMonitor.UI.Properties.Resources.data_forbidden;
            this.button4.Location = new System.Drawing.Point(295, 66);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(135, 23);
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
            this.button3.Location = new System.Drawing.Point(289, 417);
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
            this.button2.Location = new System.Drawing.Point(370, 417);
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
            this.button1.Location = new System.Drawing.Point(451, 417);
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
            this.pictureBox2.Location = new System.Drawing.Point(5, 118);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(24, 24);
            this.pictureBox2.TabIndex = 14;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::FolderMonitor.UI.Properties.Resources.data24;
            this.pictureBox1.Location = new System.Drawing.Point(5, 11);
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
            this.browseSourceFolderButton.Location = new System.Drawing.Point(433, 37);
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
            this.browseTargetFolderButton.Location = new System.Drawing.Point(435, 145);
            this.browseTargetFolderButton.Name = "browseTargetFolderButton";
            this.browseTargetFolderButton.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.browseTargetFolderButton.Size = new System.Drawing.Size(42, 23);
            this.browseTargetFolderButton.TabIndex = 4;
            this.browseTargetFolderButton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.browseTargetFolderButton.UseVisualStyleBackColor = true;
            this.browseTargetFolderButton.Click += new System.EventHandler(this.browseTargetFolderButton_Click);
            // 
            // robocopySwitchesCheckBox
            // 
            this.robocopySwitchesCheckBox.AutoSize = true;
            this.robocopySwitchesCheckBox.Location = new System.Drawing.Point(47, 298);
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
            this.robocopySwitchesTextBox.Location = new System.Drawing.Point(47, 321);
            this.robocopySwitchesTextBox.MaxLength = 1000;
            this.robocopySwitchesTextBox.Name = "robocopySwitchesTextBox";
            this.robocopySwitchesTextBox.Size = new System.Drawing.Size(382, 20);
            this.robocopySwitchesTextBox.TabIndex = 21;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(7, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(472, 2);
            this.label2.TabIndex = 22;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::FolderMonitor.UI.Properties.Resources.history32;
            this.pictureBox3.Location = new System.Drawing.Point(5, 229);
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
            this.groupBox1.Location = new System.Drawing.Point(51, 234);
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
            this.checkBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(16, 406);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(86, 17);
            this.checkBox1.TabIndex = 25;
            this.checkBox1.Text = "Don\'t validat";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(519, 387);
            this.tabControl1.TabIndex = 26;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.sourceLabel);
            this.tabPage1.Controls.Add(this.targetFolderTextBox);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.browseTargetFolderButton);
            this.tabPage1.Controls.Add(this.pictureBox3);
            this.tabPage1.Controls.Add(this.sourceFolderTextBox);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.browseSourceFolderButton);
            this.tabPage1.Controls.Add(this.robocopySwitchesCheckBox);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.robocopySwitchesTextBox);
            this.tabPage1.Controls.Add(this.pictureBox2);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.targetLabel);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.linkLabel1);
            this.tabPage1.Controls.Add(this.linkLabel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(511, 361);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Task Properties";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.schedul_enable);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(511, 361);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Scheduler";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.endon_time);
            this.panel1.Controls.Add(this.endon_type);
            this.panel1.Controls.Add(this.endon_enabled);
            this.panel1.Controls.Add(this.intervalComboBox);
            this.panel1.Controls.Add(this.Starttime);
            this.panel1.Controls.Add(this.Startdate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(40, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 148);
            this.panel1.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 6);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Trigger Type";
            // 
            // endon_time
            // 
            this.endon_time.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.endon_time.CustomFormat = "hh:mm tt";
            this.endon_time.Enabled = false;
            this.endon_time.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.endon_time.Location = new System.Drawing.Point(238, 99);
            this.endon_time.Name = "endon_time";
            this.endon_time.ShowUpDown = true;
            this.endon_time.Size = new System.Drawing.Size(101, 20);
            this.endon_time.TabIndex = 12;
            // 
            // endon_type
            // 
            this.endon_type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.endon_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.endon_type.Enabled = false;
            this.endon_type.FormattingEnabled = true;
            this.endon_type.Items.AddRange(new object[] {
            "Same day of start",
            "Next day of start",
            "After 2 days of start",
            "After 3 days of start",
            "After 5 days of start",
            "After 7 days of start"});
            this.endon_type.Location = new System.Drawing.Point(77, 98);
            this.endon_type.Name = "endon_type";
            this.endon_type.Size = new System.Drawing.Size(155, 21);
            this.endon_type.TabIndex = 11;
            // 
            // endon_enabled
            // 
            this.endon_enabled.AutoSize = true;
            this.endon_enabled.Location = new System.Drawing.Point(77, 75);
            this.endon_enabled.Name = "endon_enabled";
            this.endon_enabled.Size = new System.Drawing.Size(80, 17);
            this.endon_enabled.TabIndex = 10;
            this.endon_enabled.Text = "Kill Task On";
            this.endon_enabled.UseVisualStyleBackColor = true;
            this.endon_enabled.CheckedChanged += new System.EventHandler(this.checkBox3_CheckedChanged);
            // 
            // intervalComboBox
            // 
            this.intervalComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.intervalComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.intervalComboBox.FormattingEnabled = true;
            this.intervalComboBox.Items.AddRange(new object[] {
            "Daily",
            "Weekly",
            "Monthly"});
            this.intervalComboBox.Location = new System.Drawing.Point(77, 3);
            this.intervalComboBox.Name = "intervalComboBox";
            this.intervalComboBox.Size = new System.Drawing.Size(262, 21);
            this.intervalComboBox.TabIndex = 6;
            // 
            // Starttime
            // 
            this.Starttime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Starttime.CustomFormat = "hh:mm tt";
            this.Starttime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.Starttime.Location = new System.Drawing.Point(238, 34);
            this.Starttime.Name = "Starttime";
            this.Starttime.ShowUpDown = true;
            this.Starttime.Size = new System.Drawing.Size(101, 20);
            this.Starttime.TabIndex = 9;
            // 
            // Startdate
            // 
            this.Startdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Startdate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.Startdate.Location = new System.Drawing.Point(77, 34);
            this.Startdate.Name = "Startdate";
            this.Startdate.Size = new System.Drawing.Size(155, 20);
            this.Startdate.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Starts On";
            // 
            // schedul_enable
            // 
            this.schedul_enable.AutoSize = true;
            this.schedul_enable.Location = new System.Drawing.Point(27, 32);
            this.schedul_enable.Name = "schedul_enable";
            this.schedul_enable.Size = new System.Drawing.Size(130, 17);
            this.schedul_enable.TabIndex = 5;
            this.schedul_enable.Text = "Enable task scheduler";
            this.schedul_enable.UseVisualStyleBackColor = true;
            this.schedul_enable.CheckedChanged += new System.EventHandler(this.schedul_enable_CheckedChanged);
            // 
            // NewTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(538, 452);
            this.ControlBox = false;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
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
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox intervalComboBox;
        private System.Windows.Forms.DateTimePicker Starttime;
        private System.Windows.Forms.DateTimePicker Startdate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox schedul_enable;
        private System.Windows.Forms.DateTimePicker endon_time;
        private System.Windows.Forms.ComboBox endon_type;
        private System.Windows.Forms.CheckBox endon_enabled;
        private System.Windows.Forms.Label label4;
    }
}

