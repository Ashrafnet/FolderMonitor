using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tools;

namespace FolderMonitor.UI
{
    public partial class NewTask : Form
    {
        string _AuthorizeRequired = "Authorize Required?";
        bool _NewTask = true;
        public NewTask(bool NewOrEditTask)
        {
            _NewTask = NewOrEditTask;
            InitializeComponent();
            MirrorTask = new PathFromAndTo();
            MirrorTask.RoboCopy_Options = ServiceConfig.RoboCopyOptions;
            if (!_NewTask)
                Text = "Edit Task*";
        }

        public PathFromAndTo MirrorTask { get; set; }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PathCradentialUI x = new PathCradentialUI();
            x.Pathcredentials = (PathCredentials)MirrorTask.From .Clone();
            x.Pathcredentials.Path = sourceFolderTextBox.Text;
            if (x.ShowDialog() == DialogResult.OK)
            {
                MirrorTask.From  = x.Pathcredentials;
                if (MirrorTask.From .IsPathHasUserName)
                    linkLabel1.Text = MirrorTask.From.UserName;
                else
                    linkLabel1.Text = _AuthorizeRequired;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PathCradentialUI x = new PathCradentialUI();
            x.Pathcredentials = (PathCredentials)MirrorTask.To.Clone();
            x.Pathcredentials.Path = targetFolderTextBox.Text;
            if (x.ShowDialog() == DialogResult.OK)
            {
                MirrorTask.To = x.Pathcredentials;
                if (MirrorTask.To.IsPathHasUserName)
                    linkLabel2.Text = MirrorTask.To.UserName;
                else
                    linkLabel2.Text = _AuthorizeRequired;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (MirrorTask.From != null)
            {
                sourceFolderTextBox.Text = MirrorTask.From.Path;
                if (MirrorTask.From.IsPathHasUserName)
                    linkLabel1.Text = MirrorTask.From.UserName;
                else
                    linkLabel1.Text = _AuthorizeRequired;
              
            }
            if (MirrorTask.To != null)
            {
                targetFolderTextBox.Text = MirrorTask.To.Path;

                if (MirrorTask.To.IsPathHasUserName)
                    linkLabel2.Text = MirrorTask.To.UserName;
                else
                    linkLabel2.Text = _AuthorizeRequired;
            }

            if (  !string.IsNullOrWhiteSpace(MirrorTask.RoboCopy_Options))            
                robocopySwitchesTextBox.Text = MirrorTask.RoboCopy_Options;          
                                                          
            robocopySwitchesCheckBox.Checked = !string.IsNullOrWhiteSpace(robocopySwitchesTextBox.Text);
            if (MirrorTask != null && !string.IsNullOrEmpty(MirrorTask.ExtendedAttributes))
            {
                if (MirrorTask.ExtendedAttributes == "S")
                    aclsOnlyRadioButton.Checked = true;
                else if (MirrorTask.ExtendedAttributes.Length == 3)
                    allRadioButton.Checked = true;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        string _lasterror = "";
        bool Test()
        {
            errorProvider1.Clear(); _lasterror = "";
            if (string.IsNullOrWhiteSpace(MirrorTask.From.Path))
            {
                _lasterror = "You must set the source folder first.";
                errorProvider1.SetError(sourceFolderTextBox, _lasterror);
                sourceFolderTextBox.SelectAll();
                sourceFolderTextBox.Focus();
                return false;
            }

            try
            {


                if (!MirrorTask.From.PathExists(true))
                {
                    _lasterror = "The source folder does not exist or inaccessible.";
                    errorProvider1.SetError(sourceFolderTextBox, _lasterror);
                    sourceFolderTextBox.SelectAll();
                    sourceFolderTextBox.Focus();
                    return false;
                }
            }
            catch (Exception er)
            {
                _lasterror = er.Message;
                errorProvider1.SetError(sourceFolderTextBox, _lasterror);
                sourceFolderTextBox.SelectAll();
                sourceFolderTextBox.Focus();
                return false;
            }

            try
            {
                if (!MirrorTask.To.PathExists(true))
                {
                    // in target case we don't care if folder exist or not, we only capture if user can access to the path or not.
                    return true;
                }
            }
            catch (Exception er)
            {
                _lasterror = er.Message;
                errorProvider1.SetError(targetFolderTextBox, _lasterror);
                targetFolderTextBox.SelectAll();
                targetFolderTextBox.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(targetFolderTextBox.Text))
            {
                _lasterror = "You must set the target folder first.";
                errorProvider1.SetError(targetFolderTextBox, _lasterror);
                targetFolderTextBox.SelectAll();
                targetFolderTextBox.Focus();
                return false;
            }

            try
            {
                MirrorTask. From.GetFileSystemEntries();//just check if source Path is accessiable and can read its entries or not.

            }
            catch (Exception er)
            {
                _lasterror = "This user name has no access to source folder." + Environment.NewLine + er.Message;
                errorProvider1.SetError(sourceFolderTextBox, _lasterror);
                sourceFolderTextBox.SelectAll();
                sourceFolderTextBox.Focus();
                return false;
            }



            try
            {
                MirrorTask. To.GetFileSystemEntries();//just check if source Path is accessiable and can read its entries or not.

            }
            catch (Exception er)
            {
                _lasterror = "This user name has no access to target folder." + Environment.NewLine + er.Message;
                errorProvider1.SetError(targetFolderTextBox, _lasterror);
                targetFolderTextBox.SelectAll();
                targetFolderTextBox.Focus();
                return false;
            }



            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            var cango = checkBox1.Checked;
            if (!cango)
                cango = Test();
            if (cango)
            {
                MirrorTask. From.Path = sourceFolderTextBox.Text;
                MirrorTask. To.Path = targetFolderTextBox.Text;

              

                if (robocopySwitchesCheckBox.Checked)
                    MirrorTask.RoboCopy_Options = robocopySwitchesTextBox.Text;
                else
                    MirrorTask.RoboCopy_Options = "";

                if (allRadioButton.Checked)
                    MirrorTask.ExtendedAttributes = "SOU";
                else if (aclsOnlyRadioButton.Checked)
                    MirrorTask.ExtendedAttributes = "S";
                else
                    MirrorTask.ExtendedAttributes = string.Empty;

                DialogResult = DialogResult.OK;
            }
            else
                MessageBox.Show(_lasterror, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void browseSourceFolderButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(sourceFolderTextBox.Text))
                folderBrowserDialog1.SelectedPath = sourceFolderTextBox.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                sourceFolderTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void browseTargetFolderButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(targetFolderTextBox.Text))
                folderBrowserDialog1.SelectedPath = targetFolderTextBox.Text;
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                targetFolderTextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!Test())
            {
                MessageBox.Show(_lasterror, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                MessageBox.Show("All is good, you can GO.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void sourceFolderTextBox_TextChanged(object sender, EventArgs e)
        {
            MirrorTask. From.Path = sourceFolderTextBox.Text;
            button4.Enabled =Directory.Exists ( sourceFolderTextBox.Text);
        }

        private void targetFolderTextBox_TextChanged(object sender, EventArgs e)
        {
            MirrorTask. To.Path = targetFolderTextBox.Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ExcludedItemsDialog d = new ExcludedItemsDialog(new PathFromAndTo(MirrorTask.From , MirrorTask. To));

            if (d.ShowDialog() == DialogResult.OK)
            {
                MirrorTask. From.ExcludedFiles = d.ExcludedFiles;
                MirrorTask. From.ExcludedFolders = d.ExcludedFolders;
            }
        }

        private void robocopySwitchesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            robocopySwitchesTextBox.Enabled = robocopySwitchesCheckBox.Checked;

            if (robocopySwitchesTextBox.Enabled && robocopySwitchesTextBox.TextLength == 0)
                robocopySwitchesTextBox.Text = ServiceConfig.RoboCopyOptions;


        }
    }
}
