


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Threading;
using System.Windows.Forms;

namespace FolderMonitor.UI
{
    public partial class MainForm : Form
    {

        public bool _savestate
        {
            get
            {
                return __savestate;
            }
            set
            {
                __savestate = value;
                btnsave.Enabled = !value;
                if (!value)
                    Text = "Folder Monitor GUI *";
                else
                    Text = Text.Trim().TrimEnd('*');
            }
        }
        bool __savestate = true;
        static ServiceManager _service = null;


        public MainForm()
        {
            InitializeComponent();



            // select the first item
            if (listView1.Items.Count > 0)
                listView1.SelectedIndices.Add(0);
        }





        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

            editButton.Enabled = removeButton.Enabled = propertyGrid1.Enabled = propertyGrid2.Enabled =
                listView1.SelectedIndices.Count > 0;

            if (listView1.SelectedIndices.Count > 0)
            {
                propertyGrid1.SelectedObject = ((PathFromAndTo)listView1.SelectedItems[0].Tag).From;
                propertyGrid2.SelectedObject = ((PathFromAndTo)listView1.SelectedItems[0].Tag).To;


                var tag = (PathFromAndTo)listView1.SelectedItems[0].Tag;
                var log = Path.GetDirectoryName(FolderMonitorServicePath);
                var foldername = tag.From.Path.GetFolderName();
                log = Path.Combine(log, foldername + ".log");

                txtlogs.Text = ReadFileAsString(log);
                txtlogs.Tag = log;

            }
            else
            {

                var log = Path.Combine(Path.GetDirectoryName(FolderMonitorServicePath), ServiceConfig.FolderMonitorLogFilename);
                txtlogs.Text = ReadFileAsString(log);
                txtlogs.Tag = log;
            }

        }

        string ReadFileAsString(string fpath)
        {
            var sw = Stopwatch.StartNew();

            try
            {
                linkLabel1.Text = fpath;
                linkLabel1.Tag = fpath;
                if (File.Exists(fpath))
                {
                    //if (checkBox1.Checked)
                    //    return ReadasLines(fpath);
                    var fs = new FileStream(fpath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    using (var sr = new StreamReader(fs))
                    {
                        var rr = sr.ReadToEnd() + "";
                        fs.Close(); fs.Dispose(); sr.Dispose();
                        if (rr.Length > 1024 * 1000)
                            return rr.Substring(rr.Length - 1024 * 1000);
                        else
                            return rr;

                    }
                }
                else
                {
                    return "Logfile does not exist yet!" + Environment.NewLine + fpath;
                }
            }
            finally
            {
                sw.Stop();
                toolstatus.Text = sw.ElapsedMilliseconds + " ms.";
            }

        }


        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            // simulate a click on the edit button when an item is double-clicked
            editButton.PerformClick();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            // simulate a click on the remove button when del or backspace is pressed
            if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                removeButton.PerformClick();
        }



        string _FolderMonitorServicePath = "";
        public string FolderMonitorServicePath
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_FolderMonitorServicePath))
                {
                    _FolderMonitorServicePath = _service.ImagePath.Replace("\"", "");

                }
                return _FolderMonitorServicePath;
            }
        }


        string configFile = "";


        private void SaveTasksToConfigFile()
        {
            // List<string> ls = new List<string>();
            List<PathFromAndTo> paths = new List<PathFromAndTo>();
            foreach (ListViewItem item in listView1.Items)
            {
                var tag = (PathFromAndTo)item.Tag;
                // ls.Add(tag.ToString());
                paths.Add(tag);
            }
            //ServiceConfig.Default.PathToMonitor = ls;
            ServiceConfig.Default.SaveChanges(paths);

            _savestate = true;
        }






        protected override void OnClosing(CancelEventArgs e)
        {
            MainForm_FormClosing(this, (FormClosingEventArgs)e);
            //  base.OnClosing(e);
        }

        [DllImport("uxtheme.dll", ExactSpelling = true, CharSet = CharSet.Unicode)]
        private static extern int SetWindowTheme(IntPtr hwnd, string pszSubAppName, string pszSubIdList);

        private void MainForm_Load(object sender, EventArgs e)
        {
            SetWindowTheme(this.listView1.Handle, "Explorer", null);

            _service = new ServiceManager();
            SetServiceStatus();

            if (_service.ServiceInstalled)
            {
                configFile = Path.Combine(Path.GetDirectoryName(FolderMonitorServicePath), Path.GetFileName(FolderMonitorServicePath) + ".conf");
                ServiceConfig._configFile = configFile;
            }

        }

        private void LoadItems()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                listView1.Items.Clear();
                var ls = ServiceConfig.Default.GetAllTasks(false   );

                foreach (var path in ls)
                {
                    AddTaskToListView(path);

                }

                if (listView1.Items.Count > 0)
                    listView1.Items[0].Selected = true;
                _savestate = true;
                fileSystemWatcher1.Path = Path.GetDirectoryName(FolderMonitorServicePath);
                fileSystemWatcher1.EnableRaisingEvents = true;

            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        void EnableDisableAllControls(bool Enable)
        {
            mainPanel.Enabled = addButton.Enabled = historyButton.Enabled = scheduleButton.Enabled =
                   toolStripButton4.Enabled = toolStripButton1.Enabled = toolStripStatusLabel2.Enabled = Enable;

            if (!Enable)
                editButton.Enabled = removeButton.Enabled = false;
        }
        void SetServiceStatus()
        {
            try
            {

                _service.Refresh();
                switch (_service.Status)
                {

                    case ServiceControllerStatus.Paused:
                    case ServiceControllerStatus.PausePending:
                        statusInfo.Text = "Folder Monitor Service Status: Paused";
                        statusInfo.ForeColor = System.Drawing.Color.Green;
                        statusInfo.Image = Properties.Resources.warning;

                        startservice.Enabled = true;
                        stopservice.Enabled = false;
                        break;

                    case ServiceControllerStatus.Running:
                    case ServiceControllerStatus.StartPending:
                    case ServiceControllerStatus.ContinuePending:
                        statusInfo.Text = "Folder Monitor Service Status: Runing";
                        statusInfo.ForeColor = System.Drawing.Color.Green;
                        statusInfo.Image = Properties.Resources.check;

                        startservice.Enabled = false;
                        stopservice.Enabled = true;
                        break;
                    case ServiceControllerStatus.Stopped:
                    case ServiceControllerStatus.StopPending:
                        statusInfo.Text = "Folder Monitor Service Status: Stopped";
                        statusInfo.ForeColor = System.Drawing.Color.Red;
                        statusInfo.Image = Properties.Resources.delete;

                        startservice.Enabled = true;
                        stopservice.Enabled = false;
                        break;
                    default:
                        break;
                }
                EnableDisableAllControls(true);


                installservice.Tag = "installed";
                installservice.Text = "Uninstall Folder Monitor Service";
                installservice.Image = FolderMonitor.UI.Properties.Resources.data_copy_delete;

            }
            catch (Exception er)
            {
                statusInfo.Text = er.Message.Substring(0, 39);
                statusInfo.ForeColor = System.Drawing.Color.Red;
                statusInfo.Image = Properties.Resources.delete;
                statusInfo.ToolTipText = er.Message;

                EnableDisableAllControls(false);

                startservice.Enabled = stopservice.Enabled = false;
                installservice.Enabled = installservice.Visible = true;

                installservice.Tag = "not installed";
                installservice.Text = "Install Folder Monitor Service";
                installservice.Image = Properties.Resources.data_copy_add;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            SetServiceStatus();
        }

        private void startservice_Click(object sender, EventArgs e)
        {
            try
            {
                if (!CheckForRoboCopy()) return;
                Cursor = Cursors.WaitCursor;
                statusInfo.Text = "Starting..";
                statusInfo.Image = Properties.Resources.info_16;
                Enabled = false;
                SaveTasksToConfigFile();
                try
                {
                    _service.Start();
                }
                catch
                {
                    try
                    {
                        Thread.Sleep(500);
                        _service.Start();
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show(er.InnerMessages(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch (Exception er)
            {
                MessageBox.Show("You have to run folder monitor UI app in administrator mode" + Environment.NewLine + er.InnerMessages(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetServiceStatus();
                Enabled = true;
                Cursor = Cursors.Default;
            }

        }

        private void stopservice_Click(object sender, EventArgs e)
        {
            try
            {
                Enabled = false;
                Cursor = Cursors.WaitCursor;
                statusInfo.Text = "Stoping..";
                statusInfo.Image = Properties.Resources.info_16;
                try
                {
                    _service.Stop();
                }
                catch (Exception er)
                {
                    _service.Refresh();
                    if (_service.Status != ServiceControllerStatus.Stopped && _service.Status != ServiceControllerStatus.StopPending)
                        _service.Stop();
                }

            }
            catch (Exception er)
            {

                MessageBox.Show("You have to run folder monitor UI app in administrator mode" + Environment.NewLine + er.InnerMessages(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                SetServiceStatus();
                Enabled = true;
                Cursor = Cursors.Default;
            }

        }

        private void installservice_Click(object sender, EventArgs e)
        {

            try
            {
                Cursor = Cursors.WaitCursor;
                statusInfo.Text = "Installing..";


                listView1.Items.Clear();
                statusInfo.Image = Properties.Resources.info_16;
                Enabled = false;

                if (installservice.Tag + "" == "installed")
                {
                    statusInfo.Text = "Uninstalling..";
                    statusInfo.GetCurrentParent().Refresh();
                    ManagedInstallerClass.InstallHelper(new string[] { "/u", FolderMonitorServicePath });
                }
                else
                {
                    var app = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ServiceConfig.ServiceFilename);
                    if (!File.Exists(app))
                        File.WriteAllBytes(app, Properties.Resources.FolderMonitor);
                    if (!File.Exists(app))
                        throw new Exception("FolderMonitor Service File does not exist!");
                    ManagedInstallerClass.InstallHelper(new string[] { app });
                    _service = new ServiceManager();

                    if (_service.ServiceInstalled)
                    {
                        _FolderMonitorServicePath = "";
                        configFile = Path.Combine(Path.GetDirectoryName(FolderMonitorServicePath), Path.GetFileName(FolderMonitorServicePath) + ".conf");
                        ServiceConfig._configFile = configFile;
                        LoadItems();
                    }
                }
                SetServiceStatus();
            }
            catch (Exception er)
            {
                string req_files = string.Format("{0}{1}{0}", Environment.NewLine, ServiceConfig.ServiceFilename);
                MessageBox.Show("Please make sure that this files are located in the same folder of Folder Monitor UI:" + req_files + Environment.NewLine + Environment.NewLine + er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Enabled = true;
                Cursor = Cursors.Default;

            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (_service.ServiceInstalled)
            {
                LoadItems();
                CheckForRoboCopy();
            }



        }

        bool CheckForRoboCopy()
        {
            try
            {


                var xx = ServiceConfig.DoesRoboCopExist();
                if (!xx)
                {
                    if (MessageBox.Show("Looks like you don't have a robocopy tool in your system." + Environment.NewLine + "Would you like to install it now?" + Environment.NewLine + "", "Robocop", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var robopath = ServiceConfig.RobocopyPath;
                        File.WriteAllBytes(robopath, Properties.Resources.Robocopy);
                        return true;
                    }
                }
                return ServiceConfig.DoesRoboCopExist();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            Clipboard.SetData(_copyformat, null);
            if (!_savestate)
            {

                var rr = MessageBox.Show("You have unsaved tasks." + Environment.NewLine + "Would you like to save them before exist?", "Un-saved tasks", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (rr == DialogResult.Yes)
                {
                    SaveTasksToConfigFile();

                }
                else if (rr == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            NewTask x = new NewTask(true);
            if (x.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(x.MirrorTask.From.Path)) return;
                AddTaskToListView((PathFromAndTo)x.MirrorTask.Clone());
                //ListViewItem lvi = new ListViewItem(x.MirrorTask.From.Path, 0);
                //lvi.SubItems.Add(x.MirrorTask.To.Path);
                //lvi.Tag = (PathFromAndTo)x.MirrorTask.Clone();

                //listView1.Items.Add(lvi);
                _savestate = false;
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;
            var paths = (PathFromAndTo)listView1.SelectedItems[0].Tag;
            NewTask x = new NewTask(false);
            x.MirrorTask = (PathFromAndTo)paths.Clone();

            if (x.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(x.MirrorTask.From.Path)) return;
                ListViewItem lvi = listView1.SelectedItems[0];
                lvi.Text = x.MirrorTask.From.Path;
                lvi.SubItems[1].Text = x.MirrorTask.To.Path;
                lvi.Tag = (PathFromAndTo)x.MirrorTask.Clone();
                if (!x.MirrorTask.From.PathExists())
                    lvi.ImageIndex = 1;
                else
                    lvi.ImageIndex = x.MirrorTask.From.IsPathHasUserName || x.MirrorTask.To.IsPathHasUserName ? 3 : 0;

                _savestate = false;
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count == 0)
                return;
            foreach (ListViewItem listViewItem in listView1.SelectedItems)
                listViewItem.Remove();

            _savestate = false;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (!_savestate)
            {

                var rr = MessageBox.Show("You have unsaved tasks." + Environment.NewLine + "Would you like to save them before reload?", "Un-saved tasks", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (rr == DialogResult.Yes)
                {
                    SaveTasksToConfigFile();

                }
                else if (rr == DialogResult.Cancel)
                {
                    return;
                }
            }
            LoadItems();
            removeButton.Enabled = editButton.Enabled = false;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            var p = Path.GetDirectoryName(FolderMonitorServicePath);
            Process.Start(p);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            try
            {
                string serviceFolder = FolderMonitorServicePath;
                serviceFolder = Path.GetDirectoryName(serviceFolder);
                Process.Start("notepad.exe", Path.Combine(serviceFolder, ServiceConfig.ServiceConfigFilename));
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {

            Close();
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            try
            {
                //if (_savestate)
                //    return;

                SaveTasksToConfigFile();
                if (MessageBox.Show("Tasks has been saved, to take effect you have to restart the Folder Monitor Service." + Environment.NewLine + "Would you like to restart it Now?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    stopservice_Click(sender, e);
                    startservice_Click(sender, e);

                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => { fileSystemWatcher1_Changed(sender, e); }));
            }
            else
            {
                try
                {


                    if ((txtlogs.Tag + "").Trim().ToLower() != e.FullPath.Trim().ToLower())
                    {
                        txtlogs.AppendText(Environment.NewLine + "New Changes on file: " + e.FullPath);
                        return;
                    }
                    // txtlogs.SuspendLayout();
                    txtlogs.Clear();
                    txtlogs.AppendText(ReadFileAsString(e.FullPath));
                }
                catch (Exception er)
                {

                    txtlogs.AppendText(er.Message);
                }
                finally
                {
                    txtlogs.Refresh();
                    // txtlogs.ResumeLayout(true );
                }
            }

        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            _savestate = false;
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (listView1.Items.Count < 1)
                {
                    MessageBox.Show("No Tasks!");
                    return;
                }
                foreach (ListViewItem item in listView1.Items)
                {


                    CheckAccessibility(item);
                }
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }

        }
        void CheckAccessibility(ListViewItem item)
        {
            if (item == null) return;
            var tag = (PathFromAndTo)item.Tag;
            if (!tag.IsEnabled) return;
            var src = "The source folder of ";
            var trg = Environment.NewLine + "The target folder of ";
            item.ImageIndex = 2;
            item.ListView.Refresh();
            try
            {

                var from_acc = tag.From.CheckAccessiblity();

                if (from_acc)
                {

                    item.ToolTipText = src + "this task seems good.";
                    item.ImageIndex = 0;

                }
                else
                {
                    item.ToolTipText = src + "this task has an issue.";
                    item.ImageIndex = 1;
                }
            }
            catch (Exception er)
            {
                item.ToolTipText = src + er.Message;
                item.ImageIndex = 1;
            }

            try
            {

                var to_acc = tag.To.CheckAccessiblity();

                if (to_acc)
                {
                    item.ToolTipText += trg + "this task seems good.";
                    if (item.ImageIndex == 2) item.ImageIndex = 0;
                }
                else
                {
                    item.ToolTipText += trg + "this task has an issue.";
                    if (item.ImageIndex !=1) item.ImageIndex = 4;
                }
            }
            catch (Exception er)
            {
                item.ToolTipText += trg + er.InnerMessages();
                item.ImageIndex = 1;
            }
            finally
            {
                if (item.SubItems.Count <= 2) item.SubItems.Add("");
                item.SubItems[2].Text = item.ToolTipText;
                item.ListView.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.ColumnContent);
                if (item.ImageIndex ==1)
                    item.ForeColor = System.Drawing.Color.Red;
                else
                    item.ForeColor = System.Drawing.Color.Black;

                if (item.ImageIndex != 1 && (tag.From.IsPathHasUserName || tag.To.IsPathHasUserName))
                    item.ImageIndex = 3;

            }
        }
        private void validateAccessibilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
                CheckAccessibility(listView1.SelectedItems[0]);
        }

        private void robocopyLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1) return;
            var tag = (PathFromAndTo)listView1.SelectedItems[0].Tag;
            var log = Path.GetDirectoryName(FolderMonitorServicePath);
            var foldername = tag.From.Path.GetFolderName();
            log = Path.Combine(log, foldername + ".log");
            if (File.Exists(log))
                Process.Start("notepad", log);
            else
                MessageBox.Show("The log file is not exist yet!" + Environment.NewLine + log, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }



        string _copyformat = System.Windows.Forms.DataFormats.Serializable;


        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            dynamic dd = Clipboard.GetData(_copyformat) as PathCredentials;
            if (dd == null)
                dd = Clipboard.GetData(_copyformat) as PathFromAndTo;

            pastCradintialsToolStripMenuItem.Enabled = dd != null;

            enabletaskToolStripMenuItem.Enabled = disableTaskToolStripMenuItem.Enabled = contextMenuStrip1.Enabled = listView1.SelectedItems.Count > 0;
            openSourceFolderToolStripMenuItem.Enabled = openTargetFolderToolStripMenuItem.Enabled = duplicateToolStripMenuItem.Enabled = editToolStripMenuItem.Enabled = robocopyLogsToolStripMenuItem.Enabled = copyToolStripMenuItem.Enabled = listView1.SelectedItems.Count == 1;
            if (listView1.SelectedItems.Count > 0)
            {
                var src = ((PathFromAndTo)listView1.SelectedItems[0].Tag).From.IsPathHasUserName;
                var trg = ((PathFromAndTo)listView1.SelectedItems[0].Tag).To.IsPathHasUserName;
                copySourceCredentialsToolStripMenuItem.Enabled = src;
                copyTargetCredentialsToolStripMenuItem.Enabled = trg;
                copySourceAndTargetCredentialsToolStripMenuItem.Enabled = src && trg;

            }
        }

        private void duplicateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1) return;
            var item = listView1.SelectedItems[0];
            NewTask x = new NewTask(true);
            x.MirrorTask = (PathFromAndTo)((PathFromAndTo)item.Tag).Clone();

            if (x.ShowDialog() == DialogResult.OK)
            {
                AddTaskToListView(x.MirrorTask);
                _savestate = false;
            }
        }

        /// <summary>
        /// Creates an appropriate ListViewItem and adds it to the list view.
        /// </summary>
        bool AddTaskToListView(PathFromAndTo task)
        {
            if (string.IsNullOrWhiteSpace(task.From.Path)) return false;
            ListViewItem lvi = new ListViewItem(task.From.Path, 0);
            lvi.SubItems.Add(task.To.Path);
            lvi.SubItems.Add("");
            lvi.SubItems.Add("");
            lvi.Tag = task;// new PathFromAndTo(task.From, task.To);

            if (!task.From.PathExists())
            {
                lvi.ImageIndex = 1;
                lvi.ToolTipText = "Source folder does not exist or inaccessible.";
            }

            if (lvi.ImageIndex == 0 && (task.From.IsPathHasUserName || task.To.IsPathHasUserName))
                lvi.ImageIndex = 3;

            if (!task.IsEnabled)
            {
                lvi.ForeColor = System.Drawing.Color.Gray;
                lvi.Font = new System.Drawing.Font(lvi.Font, System.Drawing.FontStyle.Strikeout);
                lvi.SubItems[3].Text ="No";
                lvi.SubItems[3].BackColor = System.Drawing.Color.LightGray ;
                lvi.ToolTipText = "This Task is not active.";
                lvi.ImageIndex = 5;

            }
            else
            {
                // lvi.ForeColor = System.Drawing.Color.Black ;
                // lvi.Font = new System.Drawing.Font(lvi.Font, System.Drawing.FontStyle.Regular);
                lvi.SubItems[3].Text ="Yes";
                lvi.SubItems[3].BackColor = System.Drawing.Color.White;
            }
            listView1.Items.Add(lvi);
            return true;
        }
        private void openSourceFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1) return;
            var item = (PathFromAndTo)listView1.SelectedItems[0].Tag;
            if (item == null) return;
            var dir = "";
            if (((ToolStripMenuItem)sender).Tag + "" == "tgt") // target folder case            
                dir = item.To.Path;
            else
                dir = item.From.Path;
            if (Directory.Exists(dir))
                Process.Start(dir);
            else
                MessageBox.Show("Folder does not exist!" + Environment.NewLine + dir, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void clearLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                var f = txtlogs.Tag + "";// Path.Combine(Path.GetDirectoryName(FolderMonitorServicePath), "DirectoryMonitor.txt");
                if (File.Exists(f))
                {
                    var fname = Path.GetFileName(f);
                    if (MessageBox.Show("Are you sure you want to clear the log '" + fname + "'?", "Confirm?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        File.WriteAllText(f, null);
                }
            }
            catch (Exception er)
            {

                MessageBox.Show(er.InnerMessages(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        private void copySourceCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1) return;
            var tag = (PathFromAndTo)listView1.SelectedItems[0].Tag;

            Clipboard.SetData(_copyformat, tag.From);
        }

        private void copyTargetCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1) return;
            var tag = (PathFromAndTo)listView1.SelectedItems[0].Tag;

            Clipboard.SetData(_copyformat, tag.To);
        }

        private void pasteSourceCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dynamic dd = Clipboard.GetData(_copyformat) as PathCredentials;
            if (dd == null)
                dd = Clipboard.GetData(_copyformat) as PathFromAndTo;
            if (dd == null) return;

            var tag_t = ((ToolStripMenuItem)sender).Tag + "";
            foreach (ListViewItem item in listView1.SelectedItems)
            {



                var tag = item.Tag as PathFromAndTo;
                if (tag != null)
                {
                    if (dd is PathFromAndTo && tag_t == "both")
                    {
                        tag.From.UserName = dd.From.UserName;
                        tag.From.Password = dd.From.Password;
                        tag.From.Domain = dd.From.Domain;

                        tag.To.UserName = dd.To.UserName;
                        tag.To.Password = dd.To.Password;
                        tag.To.Domain = dd.To.Domain;
                        _savestate = false;
                        continue;
                    }
                    if (tag_t == "src" || tag_t == "both")
                    {
                        tag.From.UserName = dd.UserName;
                        tag.From.Password = dd.Password;
                        tag.From.Domain = dd.Domain;
                    }
                    if (tag_t == "trg" || tag_t == "both")
                    {
                        tag.To.UserName = dd.UserName;
                        tag.To.Password = dd.Password;
                        tag.To.Domain = dd.Domain;
                    }

                    _savestate = false;
                }

            }
        }

        private void copySourceAndTargetCredentialsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count < 1) return;
            var tag = (PathFromAndTo)listView1.SelectedItems[0].Tag;

            Clipboard.SetData(_copyformat, tag);
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            ConfigService x = new ConfigService(_service);
            if (x.ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var log = linkLabel1.Tag + "";
            if (File.Exists(log))
                Process.Start("notepad", log);
            else
                MessageBox.Show("The log file is not exist yet!" + Environment.NewLine + log, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void enabletaskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var tag_t = ((ToolStripMenuItem)sender).Tag + "";
            if (listView1.SelectedItems == null || listView1.SelectedItems.Count < 1) return;

            foreach (ListViewItem item in listView1.SelectedItems)
            {
                var i = (PathFromAndTo)item.Tag;
                if (tag_t == "1")
                {

                    i.IsEnabled = true;
                    item.ForeColor = System.Drawing.Color.Black;
                    item.Font = new System.Drawing.Font(item.Font, System.Drawing.FontStyle.Regular);
                    CheckAccessibility(item);
                    item.SubItems[3].Text = "Yes";
                }
                else if (tag_t == "0")
                {

                    i.IsEnabled = false;
                    item.ForeColor = System.Drawing.Color.Gray;
                    item.Font = new System.Drawing.Font(item.Font, System.Drawing.FontStyle.Strikeout);
                    item.ImageIndex = 5;
                    item.SubItems[3].Text = "No";
                }


            }
            _savestate = false;
        }
    }
}
