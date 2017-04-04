using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FolderMonitor
{
    class RoboCopyAgent:IDisposable
    {
        public RoboCopyAgent(PathFromAndTo task)
        {
            fromPath = task.From;
            toPath = task.To;
            Schduletask = task.ScheduleTask;
            if (this.fromPath.Path.Last() != '\\')
            {
                this.fromPath.Path += "\\";
            }
            if (this.toPath.Path.Last() != '\\')
            {
                this.toPath.Path += "\\";
            }
            if (!string.IsNullOrWhiteSpace(task.RoboCopy_Options))
                RoboOptions = task.RoboCopy_Options;

            var switches = new StringBuilder();
            if (task.From.ExcludedFiles.Count > 0)
            {
                switches.Append(" /xf");
                foreach (string file in task.From.ExcludedFiles)
                    switches.AppendPathOrWildcard( task.From.Path, file);
            }

            if (task.From.ExcludedFolders.Count > 0)
            {
                switches.Append(" /xd");
                foreach (string folder in task.From.ExcludedFolders)
                    switches. AppendPathOrWildcard( task.From.Path, folder);
            }

            if (!string.IsNullOrEmpty(task.ExtendedAttributes))
            {
                switches.Append(" /copy:dat");
                switches.Append(task.ExtendedAttributes);
            }

            RoboOptions += switches.ToString();
        }

        /// <summary>
        /// Destructor
        /// </summary>
        ~RoboCopyAgent()
        {
            Stop();

        }
        public Process process { get; set; }
      //  private Task backupTask;

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
        private Timer timer1;
        long TIME_INTERVAL_IN_MILLISECONDS = 10 * 1000;
        public void Start()
        {

            try
            {

                TimerCallback tmrCallBack = new TimerCallback(timer1_Tick);
                timer1 = new Timer(tmrCallBack);
                // have the timer starts in 1 second
                timer1.Change(1, Timeout.Infinite );

                

            }
            catch (Exception er)
            {
                ErrorOccured?.Invoke(this, er);
            }
        }
        private void timer1_Tick(object sender)
        {
            try
            {
                var task = ServiceConfig.Default.GetTask(fromPath.Path, toPath.Path );
                if(task!=null)
                {
                    if (!task.IsEnabled) //first check if task become disabled
                    {
                        Stop();
                        IsRunning = false;
                        return;
                    }

                    fromPath = task.From;
                    toPath = task.To;
                    Schduletask = task.ScheduleTask;

                  
                }
                bool CanRunNow = false, MustStop = false;
                if (Schduletask == null || !Schduletask.IsEnabled)
                {
                    if (IsRunning)
                        return;
                    CanRunNow = true;
                }
                else if (Schduletask != null && Schduletask.IsEnabled)
                {
                    if (Schduletask.EndTime.HasValue)// if schduled enabled  then check the end time
                    {
                        switch (Schduletask.EndTime_Type)
                        {
                            case EndOnType.SameStartTime:
                                if(!LastRunOn.HasValue && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay) //didn't run yet
                                    MustStop = true;
                                else  if (LastRunOn.HasValue && DateTime.Now.Date >= LastRunOn.Value.Date && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay)
                                    MustStop = true;
                                break;
                            case EndOnType.NextDay:
                                if (!LastRunOn.HasValue && DateTime.Now.Date >= Schduletask.EndTime.Value.Date .AddDays(1) && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay) //didn't run yet
                                    MustStop = true;
                                else
                               if (LastRunOn.HasValue && DateTime.Now.Date >= LastRunOn.Value.Date.AddDays(1) && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay)
                                    MustStop = true;
                                break;

                            case EndOnType.After_2_days:
                                if (!LastRunOn.HasValue && DateTime.Now.Date >= Schduletask.EndTime.Value.Date.AddDays(2) && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay) //didn't run yet
                                    MustStop = true;
                                else
                               if (LastRunOn.HasValue && DateTime.Now.Date >= LastRunOn.Value.Date.AddDays(2) && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay)
                                    MustStop = true;
                                break;

                            case EndOnType.After_3_days:
                                if (!LastRunOn.HasValue && DateTime.Now.Date >= Schduletask.EndTime.Value.Date.AddDays(3) && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay) //didn't run yet
                                    MustStop = true;
                                else
                               if (LastRunOn.HasValue && DateTime.Now.Date >= LastRunOn.Value.Date.AddDays(3) && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay)
                                    MustStop = true;
                                break;
                            case EndOnType.After_5_days:
                                if (!LastRunOn.HasValue && DateTime.Now.Date >= Schduletask.EndTime.Value.Date.AddDays(5) && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay) //didn't run yet
                                    MustStop = true;
                                else
                               if (LastRunOn.HasValue && DateTime.Now.Date >= LastRunOn.Value.Date.AddDays(5) && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay)
                                    MustStop = true;
                                break;
                            case EndOnType.After_7_days:
                                if (!LastRunOn.HasValue && DateTime.Now.Date >= Schduletask.EndTime.Value.Date.AddDays(7) && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay) //didn't run yet
                                    MustStop = true;
                                else
                               if (LastRunOn.HasValue && DateTime.Now.Date >= LastRunOn.Value.Date.AddDays(7) && Schduletask.EndTime.Value.TimeOfDay <= DateTime.Now.TimeOfDay)
                                    MustStop = true;
                                break;
                            default:
                                break;
                        }
                        if (MustStop)
                        {
                            Stop();
                            return;
                        }
                    }

                    switch (Schduletask.Triggertype)
                    {
                        case TriggerType.Daily:
                            if (DateTime.Now >= Schduletask.StartTime)
                                if (!LastRunOn.HasValue)
                                    CanRunNow = true;
                                else if (LastRunOn.HasValue && LastRunOn.Value.Date != DateTime.Now.Date)
                                    CanRunNow = true;
                            break;
                        case TriggerType.Weekly:
                            if (DateTime.Now.DayOfWeek == Schduletask.StartTime.DayOfWeek &&
                                (DateTime.Now.Hour >= Schduletask.StartTime.Hour || (DateTime.Now.Hour == Schduletask.StartTime.Hour && DateTime.Now.Minute >= Schduletask.StartTime.Minute)))
                                CanRunNow = true;
                            break;
                        case TriggerType.Monthly:
                            if (DateTime.Now.Day == Schduletask.StartTime.Day &&
                               (DateTime.Now.Hour >= Schduletask.StartTime.Hour || (DateTime.Now.Hour == Schduletask.StartTime.Hour && DateTime.Now.Minute >= Schduletask.StartTime.Minute)))
                                CanRunNow = true;
                            break;
                        default:
                            break;
                    }

                  


                }
                if (CanRunNow && !IsRunning)
                {
                    thrd = new Thread(RunRoboCopyProcess);
                    thrd.Start();
                }
            }
            catch (Exception er)
            {
                ErrorOccured?.Invoke(this, er);
            }
            finally
            {
                // have the timer starts in 1 second, and then fire once every min
                timer1.Change(TIME_INTERVAL_IN_MILLISECONDS, Timeout.Infinite );
            }
        }

        bool IsRunning = false;
        DateTime? LastRunOn = null;
        Thread thrd;
        private void RunRoboCopyProcess()
        {

            try
            {
                IsRunning = true;
                LastRunOn = DateTime.Now;
                if (fromPath.IsUNC)
                    fromPath.ConnectToUNC(false);

                if (toPath.IsUNC)
                    toPath.ConnectToUNC(false);

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
               IsRunning = process.ExitCode == 0;
            }
            catch (Exception er)
            {
                IsRunning = false;
                LastRunOn = null;
                ErrorOccured?.Invoke(this, er);
            }
            finally
            {
                Stop();
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

        public ScheduleTime Schduletask { get; private set; }

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




        public void Stop()
        {
            try
            {
                fromPath.DisconnectFromUNC();
                toPath.DisconnectFromUNC();


                if (process != null && !process.HasExited)
                {
                    process.Kill();
                   
                }
            }
            catch (Exception er)
            {
                ErrorOccured?.Invoke(this, er);
            }
            finally
            {
               // IsRunning = false;
            }
        }

        public void Dispose()
        {
            try
            {
                timer1.Change(Timeout.Infinite, Timeout.Infinite);
                timer1.Dispose();
                timer1 = null;
                thrd.Abort();
                thrd = null;
                Stop();
                if (process != null)
                {
                    process.Dispose();
                    process = null;
                }
                if (DataReceivedOccured != null)
                    DataReceivedOccured = null;

                if (ErrorOccured != null)
                    ErrorOccured = null;
            }
            catch(Exception er)
            {
            }

        }
    }
}
