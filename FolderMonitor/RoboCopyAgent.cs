using ConnectUNCWithCredentials;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;


namespace FolderMonitor
{
    class RoboCopyAgent
    {
        public RoboCopyAgent(PathCredentials fromPath, PathCredentials toPath)
        {
            this.fromPath = fromPath;
            this.toPath = toPath;
            if (this.fromPath.Path.Last() != '\\')
            {
                this.fromPath.Path += "\\";
            }
            if (this.toPath.Path.Last() != '\\')
            {
                this.toPath.Path += "\\";
            }
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~RoboCopyAgent()
        {
            Stop();


        }
        public Process process { get; set; }
        private Task backupTask;

        /// <summary>
        /// Path to listen for changes
        /// </summary>
        public PathCredentials fromPath = null;
        /// <summary>
        /// Path, where to apply the changes
        /// </summary>
        public PathCredentials toPath = null;

        /// <summary>
        /// Delegate for errors occurs wile filesync working
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="exception"></param>
        public delegate void ErrorHandler(object sender, Exception exception);


        public event ErrorHandler ErrorOccured;
        public event DataReceivedEventHandler DataReceivedOccured;
        UNCAccessWithCredentials unc_from = null;
        UNCAccessWithCredentials unc_to = null;

        public void Start()
        {

            try
            {

          
            if (fromPath.IsUNC)
            {
                unc_from = new UNCAccessWithCredentials() { AutoDispose = false };
                if (fromPath.IsPathHasUserName && (!unc_from.NetUseWithCredentials(fromPath.Path.Trim().TrimEnd('\\'), fromPath.UserName, fromPath.Domain, fromPath.Password) && unc_from.LastError != 1219))
                {
                    if (!fromPath.IsPathUNCStartswithIP)
                        ErrorOccured?.Invoke(this, new Exception("Failed to connect to " + fromPath + "\r\nYou have to point to remote share folder using IP address instead of DNS name, So Remote Share folder should look like:\\\\10.10..\\SharefolderName."));
                    else
                        ErrorOccured?.Invoke(this, new Exception("Failed to connect to " + fromPath + "\r\nLastError = " + unc_from.LastError.ToString()));
                     return ;
                }
            }

            
            if (toPath.IsUNC)
            {
                unc_to = new UNCAccessWithCredentials() { AutoDispose = false };
                if (toPath.IsPathHasUserName && (!unc_to.NetUseWithCredentials(toPath.Path.Trim().TrimEnd('\\'), toPath.UserName, toPath.Domain, toPath.Password) && unc_to.LastError != 1219))
                {
                    if (!toPath.IsPathUNCStartswithIP)
                        ErrorOccured?.Invoke(this, new Exception("Failed to connect to " + toPath + "\r\nYou have to point to remote share folder using IP address instead of DNS name, So Remote Share folder should look like:\\\\10.10..\\SharefolderName."));
                    else
                        ErrorOccured?.Invoke(this, new Exception("Failed to connect to " + toPath + "\r\nLastError = " + unc_to.LastError.ToString()));
                     return;
                }
            }



            backupTask = Task.Factory.StartNew(() =>
            {
                process = new Process();
                process.StartInfo.WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.LoadUserProfile = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.FileName = "ROBOCOPY.exe";
                process.StartInfo.Arguments = GenerateParameters();
                process.OutputDataReceived += process_OutputDataReceived;
                process.ErrorDataReceived += process_ErrorDataReceived;

                if (fromPath.IsPathHasUserName && !fromPath.IsUNC && !toPath.IsUNC)
                {
                    process.StartInfo.UserName = fromPath.UserName;
                    process.StartInfo.Password = fromPath.Password.ToSecureString();
                    if (!string.IsNullOrWhiteSpace(fromPath.Domain))
                        process.StartInfo.Domain = fromPath.Domain;
                }
                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();
                process.WaitForExit();
            });

            backupTask.ContinueWith((continuation) =>
            {

                Stop();
            });

            }
            catch (Exception er)
            {
                ErrorOccured?.Invoke(this,er);              
            }
        }

        private void process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data)) return;
            ErrorOccured?.Invoke(sender,new Exception ( e.Data));
            // throw new NotImplementedException();
        }

        private void process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data)) return;
            DataReceivedOccured?.Invoke(sender, e);
        }
        string _options = "/E /FFT /Z /W:5 /R:3 /MON:1";
        public string RoboOptions
        {
            get
            {
                return _options;
            }
            set
            {
                _options = value;
            }
        }

        private string GenerateParameters()
        {
            //robocopy \\SourceServer\Share \\DestinationServer\Share /MIR /FFT /Z /W:5
            var parms = " ";
            var LogPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string lastFolderName = Path.GetFileName(Path.GetDirectoryName(fromPath.Path));
            //string DC = "";// "\"";
            LogPath += "\\" + lastFolderName + ".log";
            
            
             parms = string.Format("{0} {1} {2}", fromPath.Path.QuoteForRobocopy (),
                toPath.Path.QuoteForRobocopy(), RoboOptions + @" /LOG:" + LogPath);

            return parms;
        }

      

       
        public  void Stop()
        {
            try
            {

                if (unc_to != null)
                    unc_to.Dispose();

                if (unc_from != null)
                    unc_from.Dispose();

                if (process != null && !process.HasExited)
            {
                process.Kill();
                process.Dispose();
                process = null;
            }
            }
            catch (Exception er)
            {
                ErrorOccured?.Invoke(this, er);
            }
        }
    }
}
