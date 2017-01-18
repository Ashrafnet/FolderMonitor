namespace FolderMonitor.UI
{
    partial class ExcludedItemsDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.filesGroupBox = new System.Windows.Forms.GroupBox();
            this.excludedFilesControl = new RoboMirror.GUI.ExcludedItemsControl();
            this.foldersGroupBox = new System.Windows.Forms.GroupBox();
            this.excludedFoldersControl = new RoboMirror.GUI.ExcludedItemsControl();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.filesGroupBox.SuspendLayout();
            this.foldersGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SteelBlue;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(12, 0, 56, 0);
            this.label1.Size = new System.Drawing.Size(583, 50);
            this.label1.TabIndex = 1004;
            this.label1.Text = "These files and folders will not be copied.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.SteelBlue;
            this.pictureBox1.Image = global::FolderMonitor.UI.Properties.Resources.data_forbidden32;
            this.pictureBox1.Location = new System.Drawing.Point(534, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(37, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1005;
            this.pictureBox1.TabStop = false;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.button2.ForeColor = System.Drawing.Color.Green;
            this.button2.Image = global::FolderMonitor.UI.Properties.Resources.check;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(415, 447);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 25);
            this.button2.TabIndex = 1006;
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
            this.button1.Location = new System.Drawing.Point(496, 447);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 25);
            this.button1.TabIndex = 1007;
            this.button1.Text = "Cancel";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.splitContainer.Location = new System.Drawing.Point(12, 53);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.splitContainer.Panel1.Controls.Add(this.filesGroupBox);
            this.splitContainer.Panel1MinSize = 100;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.splitContainer.Panel2.Controls.Add(this.foldersGroupBox);
            this.splitContainer.Panel2MinSize = 100;
            this.splitContainer.Size = new System.Drawing.Size(559, 370);
            this.splitContainer.SplitterDistance = 182;
            this.splitContainer.SplitterWidth = 3;
            this.splitContainer.TabIndex = 1008;
            // 
            // filesGroupBox
            // 
            this.filesGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filesGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.filesGroupBox.Controls.Add(this.excludedFilesControl);
            this.filesGroupBox.Location = new System.Drawing.Point(0, 0);
            this.filesGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.filesGroupBox.Name = "filesGroupBox";
            this.filesGroupBox.Size = new System.Drawing.Size(559, 173);
            this.filesGroupBox.TabIndex = 0;
            this.filesGroupBox.TabStop = false;
            this.filesGroupBox.Text = "Excluded files";
            // 
            // excludedFilesControl
            // 
            this.excludedFilesControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.excludedFilesControl.BaseFolder = null;
            this.excludedFilesControl.Font = this.Font;
            this.excludedFilesControl.Location = new System.Drawing.Point(14, 22);
            this.excludedFilesControl.Mode = RoboMirror.GUI.ExcludedItemsMode.Files;
            this.excludedFilesControl.Name = "excludedFilesControl";
            this.excludedFilesControl.Size = new System.Drawing.Size(491, 145);
            this.excludedFilesControl.TabIndex = 0;
            this.excludedFilesControl.Changed += new System.EventHandler(this.Control_Changed);
            // 
            // foldersGroupBox
            // 
            this.foldersGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.foldersGroupBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.foldersGroupBox.Controls.Add(this.excludedFoldersControl);
            this.foldersGroupBox.Location = new System.Drawing.Point(0, 7);
            this.foldersGroupBox.Margin = new System.Windows.Forms.Padding(0);
            this.foldersGroupBox.Name = "foldersGroupBox";
            this.foldersGroupBox.Size = new System.Drawing.Size(550, 178);
            this.foldersGroupBox.TabIndex = 0;
            this.foldersGroupBox.TabStop = false;
            this.foldersGroupBox.Text = "Excluded folders";
            // 
            // excludedFoldersControl
            // 
            this.excludedFoldersControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.excludedFoldersControl.BaseFolder = null;
            this.excludedFoldersControl.Location = new System.Drawing.Point(14, 22);
            this.excludedFoldersControl.Mode = RoboMirror.GUI.ExcludedItemsMode.Folders;
            this.excludedFoldersControl.Name = "excludedFoldersControl";
            this.excludedFoldersControl.Size = new System.Drawing.Size(482, 150);
            this.excludedFoldersControl.TabIndex = 0;
            this.excludedFoldersControl.Changed += new System.EventHandler(this.Control_Changed);
            // 
            // ExcludedItemsDialog
            // 
            this.AcceptButton = this.button2;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(228)))), ((int)(((byte)(248)))));
            this.CancelButton = this.button1;
            this.ClientSize = new System.Drawing.Size(583, 484);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label1);
            this.Name = "ExcludedItemsDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Excluded Items";
            this.Load += new System.EventHandler(this.ExcludedItemsDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.filesGroupBox.ResumeLayout(false);
            this.foldersGroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.GroupBox filesGroupBox;
        private RoboMirror.GUI.ExcludedItemsControl excludedFilesControl;
        private System.Windows.Forms.GroupBox foldersGroupBox;
        private RoboMirror.GUI.ExcludedItemsControl excludedFoldersControl;
    }
}