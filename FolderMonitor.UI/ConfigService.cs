using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace FolderMonitor.UI
{
    public partial class ConfigService : Form
    {
        private ServiceManager _service;
        ServiceStartMode startmode ;
        string accountname = null;
        public ConfigService(ServiceManager service)
        {
            InitializeComponent();
            _service = service;
            drpAccount.Items.Add("Local Service");
            drpAccount.Items.Add("Local System");
            drpAccount.Items.Add("Network Service");
            drpAccount.Items.Add("User");

            //comboBox2.Items.Add("Automatic (Delayed Start)");//2
            drpStartupmode.Items.Add("Automatic");//2
            drpStartupmode.Items.Add("Manual");//3
            drpStartupmode.Items.Add("Disable");//4


            textBox1.Text = service.ServiceName ;
            textBox2.Text = service.ImagePath ;

            startmode= service.StartMode; 
              accountname =service.GetAccountName();

            switch (startmode)
            {
                case ServiceStartMode.Manual:
                    drpStartupmode.SelectedIndex = 1;
                    break;
                case ServiceStartMode.Automatic:
                    drpStartupmode.SelectedIndex = 0;
                    break;
                case ServiceStartMode.Disabled:
                    drpStartupmode.SelectedIndex = 2;
                    break;
                default:
                    break;
            }

            if (accountname == "LocalService" || accountname == "NT AUTHORITY\\LocalService")
                drpAccount.SelectedIndex = 0;
            else if (accountname == "LocalSystem" || accountname == "NT AUTHORITY\\LocalSystem")
                drpAccount.SelectedIndex = 1;
            else if (accountname == "NetworkService" || accountname == "NT AUTHORITY\\NetworkService")
                drpAccount.SelectedIndex = 2;
            else
            {
                drpAccount.SelectedIndex = 3;
               txtusername.Text = accountname;
                txtpass.Text = "password";
            }


        }

        private void ConfigService_Load(object sender, EventArgs e)
        {
            accountChanged = 
            startupChanged = false;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
        bool accountChanged = false;
        bool startupChanged = false;
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (accountChanged)
                {
                    
                    if (drpAccount.SelectedIndex == 3 && (string.IsNullOrWhiteSpace(txtusername.Text) || string.IsNullOrWhiteSpace(txtpass.Text)))
                    {
                        txtusername.Focus();
                        throw new Exception("Username and password must set!");
                    }



                    if (drpAccount.SelectedIndex == 3)
                        SetUserAccount(txtusername.Text, txtpass.Text);
                    else
                        _service.SetAccountName(drpAccount.Text.Replace(" ", ""));
                    MessageBox.Show("to take effects you must restart the service.", "Acknowledge", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (startupChanged)
                {
                    if (drpStartupmode.SelectedIndex == 0)
                        _service.StartMode = ServiceStartMode.Automatic;
                    else if (drpStartupmode.SelectedIndex == 1)
                        _service.StartMode = ServiceStartMode.Manual;
                    else if (drpStartupmode.SelectedIndex == 2)
                        _service.StartMode = ServiceStartMode.Disabled;
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void   SetUserAccount(string username,string password)
        {
            
            var cmd =string.Format ( "config {0} " ,ServiceManager._FolderMonitorServiceName);
            if (txtusername.Enabled)
            {
                cmd += "obj= " + username + " ";
                cmd += "password= " + password;
                var i = new Tools.Impersonator(username, null, password);
                i.Dispose();
            }else
                cmd += "obj= " + drpAccount.Text.Trim().Replace (" ","") ;
            var process = new Process();
          //  process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
            process.StartInfo.UseShellExecute = false;
           // process.StartInfo.LoadUserProfile = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = "sc";
            process.StartInfo.Arguments = cmd;
            //string sc_data = ""; string sc_error = "";
            //process.OutputDataReceived += (object sender, DataReceivedEventArgs e) => { sc_data = e.Data; };
            //process.ErrorDataReceived += (object sender, DataReceivedEventArgs e) => { sc_error = e.Data; };


           
            process.Start(); 
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit(2000);

            //if (!string.IsNullOrWhiteSpace(sc_error))
            //    throw new Exception(sc_error);

            //return sc_data;
        }

      

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           txtusername.Enabled = txtpass.Enabled  = drpAccount.SelectedIndex == 3;
            txtusername.Text = "";
            accountChanged = true;
        }

        private void txtusername_TextChanged(object sender, EventArgs e)
        {
            txtpass.Text = "";
            accountChanged = true;
        }

        private void drpStartupmode_SelectedIndexChanged(object sender, EventArgs e)
        {
            startupChanged = true;
        }

        private void txtpass_TextChanged(object sender, EventArgs e)
        {
            accountChanged = true;
        }
    }
}
