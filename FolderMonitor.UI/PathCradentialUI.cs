using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FolderMonitor.UI
{
    public partial class PathCradentialUI : Form
    {
        public PathCradentialUI()
        {
            InitializeComponent();
            Pathcredentials = new FolderMonitor.PathCredentials();
        }
        public PathCredentials Pathcredentials { get; set; }

        private void PathCradentialUI_Load(object sender, EventArgs e)
        {
            sourceFolderTextBox.Text = Pathcredentials.Path;
            textBox1.Text = Pathcredentials.UserName;
            textBox2.Text = Pathcredentials.Password ;
            textBox3.Text = Pathcredentials.Domain ;
            checkBox1.Checked  = Pathcredentials.IsPathHasUserName;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pathcredentials.UserName = textBox1.Text;
            Pathcredentials.Password  = textBox2.Text;
            Pathcredentials.Domain = textBox3.Text;
            DialogResult = DialogResult.OK ;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            groupBox1.Enabled = checkBox1.Checked;
            if (!checkBox1.Checked)
            {
                textBox1.Text = textBox2.Text = textBox3.Text = 
                Pathcredentials.UserName = Pathcredentials.Password = Pathcredentials.Domain = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
              
                if (Pathcredentials.CheckAccessiblity())
                    MessageBox.Show("All Good, you can GO.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("This Path is not exist or inaccessible.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }finally
            {
                Cursor = Cursors.Default ;
            }
        }
      

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Pathcredentials.UserName = textBox1.Text;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Pathcredentials.Password  = textBox2.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            Pathcredentials.Domain  = textBox3.Text;
        }
    }
}
