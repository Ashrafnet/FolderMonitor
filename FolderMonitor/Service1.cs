using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceProcess;
using System.IO;
using System.Threading;
using ICSharpCode.SharpZipLib.Zip;
using System.Reflection;
using Tools;
using System.Linq;
using Ashrafnet.FileSync;
using System.Xml.Serialization;
using System.Text;

namespace FolderMonitor
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        public void startservice()
        {
            OnStart(null );
        }
        protected override void OnStart(string[] args)
        {
            
              //  Thread.Sleep(10000);
            

            _ServiceName = this.ServiceName;
            ////if (!string.IsNullOrWhiteSpace(username))
            ////{
            ////    Impersonator x = new Impersonator(username, domain, password);
            ////}
           StartMonitor();
        }
        private Timer timer1;
        long TIME_INTERVAL_IN_MILLISECONDS = 60 * 1000;


      
        List<RoboCopyAgent> _watchers = new List<RoboCopyAgent>();
        FileOutputLogger _logger = new FileOutputLogger();
        void StartMonitor()
        {
            try
            {
                _logger.LogLevel = logType.All;
                _logger.LogWrite("Service Has been Started!");
                //var xx = ServiceConfig.DoesRoboCopExist();
                //if (xx)
                //    throw new Exception("");
                ServiceConfig._configFile =Path.Combine (AppDomain.CurrentDomain.BaseDirectory, ServiceConfig.ServiceConfigFilename );
                foreach (var copyOp in ServiceConfig.Default. GetAllTasks())
                {
                    try
                    {                       
                        
                        if (string.IsNullOrWhiteSpace(copyOp.From.Path )) continue;
                        if (string.IsNullOrWhiteSpace(copyOp.To.Path )) continue;
                        if (!copyOp.From.PathExists())
                        {

                            _logger.LogWrite("StartMonitor Function.\n" + "Dirctory '" + copyOp.From.Path + "' does not exist.", logType.Error);
                            continue;
                        }

                        var sync = new RoboCopyAgent(copyOp.From, copyOp.To );
                        if (!string.IsNullOrWhiteSpace(copyOp.RoboCopy_Options))
                            sync.RoboOptions = copyOp.RoboCopy_Options;
                        var switches = new StringBuilder();
                        if (copyOp.From .ExcludedFiles.Count > 0)
                        {
                            switches.Append(" /xf");
                            foreach (string file in copyOp.From.ExcludedFiles)
                                AppendPathOrWildcard(switches, copyOp.From.Path , file);
                        }

                        if (copyOp.From .ExcludedFolders.Count > 0)
                        {
                            switches.Append(" /xd");
                            foreach (string folder in copyOp.From.ExcludedFolders)
                                AppendPathOrWildcard(switches, copyOp.From.Path, folder);
                        }

                        if (!string.IsNullOrEmpty(copyOp.ExtendedAttributes))
                        {
                            switches.Append(" /copy:dat");
                            switches.Append(copyOp.ExtendedAttributes);
                        }

                        sync.RoboOptions += switches.ToString ();
                        sync.ErrorOccured += sync_ErrorOccured;
                        sync.DataReceivedOccured += Sync_DataReceivedOccured;
                        sync.Start();
                        if (_watchers.FirstOrDefault(x => x.fromPath.Path.ToLower().Trim() == copyOp.From.Path.ToLower().Trim()
                                                       && x.toPath.Path.ToLower().Trim() == copyOp.To .Path.ToLower().Trim()) == null)
                            _watchers.Add(sync);
                    }
                    catch (Exception er)
                    {

                        _logger.LogWrite("StartMonitor Function.\n" + er.InnerMessages(), logType.Error);

                    }
                }
            }
            catch (Exception er)
            {
                _logger.LogWrite("StartMonitor Function.\n" + er.InnerMessages(), logType.Error);
            }
            finally
            {
                TimerCallback tmrCallBack = new TimerCallback(timer1_Tick);
                timer1 = new Timer(tmrCallBack, null, TIME_INTERVAL_IN_MILLISECONDS, Timeout.Infinite);
            }

        }
        private static void AppendPathOrWildcard(StringBuilder arguments, string folder, string pathOrWildcard)
        {
            // paths begin with a directory separator character
            if (pathOrWildcard[0] == Path.DirectorySeparatorChar)
                pathOrWildcard = Path.Combine(folder, pathOrWildcard.Substring(1));

            arguments.Append(' ');

            // enforce enclosing double-quotes because if a volume shadow copy is used,
            // the source volume will be replaced by the mount point which may contain spaces
            arguments.Append(pathOrWildcard.QuoteForRobocopy ( force: true));
        }
        private void Sync_DataReceivedOccured(object sender, DataReceivedEventArgs e)
        {
            _logger.LogWrite("Sync_DataReceivedOccured Function: " + Environment.NewLine  + e.Data , logType.information );
        }

        void sync_ErrorOccured(object sender, Exception exception)
        {
            _logger.LogWrite("sync_ErrorOccured Function: " + Environment.NewLine + exception.InnerMessages(), logType.Error);                       
        }


        bool IsPathDirectory(string path)
        {
            // get the file attributes for file or directory
            FileAttributes attr = File.GetAttributes(path);

            //detect whether its a directory or file
            if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                return true;
            else
                return false;
        }
     

        public Queue<OperationInfo> _DoOperations { get; set; }
        bool ServiceStopping = false;
        protected override void OnStop()
        {
            ServiceStopping = true;

            foreach (var item in _watchers)
            {
                if (item == null) continue;
                item.Stop();
                    
            }
        }



        static void CompressFile(string strFile, string strzFile)
        {
          /*  string Element = strFile;
            string zElement = strzFile;
            string pass = "DumpUcasPass";
            string cmd = string.Format("a -p{0} {1} {2}", pass, zElement, Element);
            string rarPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "rar.exe");
            if (File.Exists(rarPath))
            {
                
                EventLog.WriteEntry(_ServiceName, "Rar Founded, and start compressing" + Environment.NewLine + cmd, EventLogEntryType.SuccessAudit);
                ProcessStartInfo p = new ProcessStartInfo(rarPath, cmd);
                p.UseShellExecute = false;
                p.WindowStyle = ProcessWindowStyle.Hidden;
                Process.Start(p).WaitForExit();
            }
            else
            {
                EventLog.WriteEntry(_ServiceName , "Rar Not Founded, and start compressing on Biult-In Zip" + Environment.NewLine + rarPath, EventLogEntryType.Warning);
                var z = ZipFile.Create(zElement);
                z.Password = pass;
                z.BeginUpdate();
                z.Add(Element, Path.GetFileName(Element));
                z.CommitUpdate();
                z.Close();
            }

            try
            {
                bool delAfter = false;
                delAfter = Config.Default.DeleteAfterCompression;
                if (delAfter)
                {
                    if (File.Exists(strFile))
                        File.Delete(strFile);
                }
            }
            catch { }*/
        }
     /*  public static void copyDirectory(string Src, string Dst)
        {
            try
            {


                if (string.IsNullOrWhiteSpace(Src)) return; if (Src.Trim().Length < 2) return;//at least should be 1 char
                if (string.IsNullOrWhiteSpace(Dst)) return; if (Dst.Trim().Length < 2) return;//at least should be 1 char
                Config.Default.Reload();
                bool IsCompression = Config.Default.EnableCompression;
                long MinSize = Config.Default.SizeBiggerThan;

                

                if (Dst[Dst.Length - 1] != Path.DirectorySeparatorChar)
                    Dst += Path.DirectorySeparatorChar;
               
                if (!Directory.Exists(Dst)) Directory.CreateDirectory(Dst);
                var Files = Directory.EnumerateFileSystemEntries(Src);
                foreach (var Element in Files)
                {
                    if (string.IsNullOrWhiteSpace(Element)) continue;
                    
                    // Sub directories
                    string dst = "";
                    if (Directory.Exists(Element))
                        copyDirectory(Element, Dst + Path.GetFileName(Element));


                    else // Files in directory
                    {
                        if (new FileInfo(Element).Length < MinSize) continue;
                        if (IsCompression)
                        {
                            dst = Dst + Path.GetFileName(Element);
                            if (Element.Trim().ToLower().EndsWith(".zip"))
                            {
                                if (File.Exists(dst) || File.Exists(Dst + Path.GetFileName(Element))) continue;
                                copyFile(Element, dst);
                                continue;
                            }


                            dst += ".zip";
                            string zElement = Element + ".zip";
                            if (File.Exists(dst) || File.Exists(Dst + Path.GetFileName(Element))) continue;
                            if (!File.Exists(dst) && !File.Exists(Dst + Path.GetFileName(Element)))
                            {

                                if (!File.Exists(zElement))
                                    CompressFile(Element, zElement);

                                copyFile(zElement, dst);
                                //File.Copy(zElement, dst, true);
                            }
                            else  //Check the Size of file
                            {
                                if (!File.Exists(zElement))
                                {
                                    CompressFile(Element, zElement);
                                }
                                if (!File.Exists(dst))
                                    copyFile(zElement, dst);
                                else // If exist , we have to check both Sizes
                                {
                                    FileInfo f1 = new FileInfo(dst);
                                    FileInfo f2 = new FileInfo(zElement);
                                    if (f1.Length != f2.Length)
                                    {
                                        dst = Dst + Path.GetFileName(Element);
                                        copyFile(zElement, dst);

                                        // File.Copy(zElement, dst, true);
                                    }
                                }
                            }
                        }
                        else
                        {
                            dst = Dst + Path.GetFileName(Element);
                            if (!File.Exists(dst))
                                copyFile(Element, dst);
                            else  //Check the Size of file
                            {
                                FileInfo f1 = new FileInfo(dst);
                                FileInfo f2 = new FileInfo(Element);
                                if (f1.Length != f2.Length)
                                {
                                    copyFile(Element, dst);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception er)
            {
                
                EventLog.WriteEntry(_ServiceName , "CopyDirectory Function[Error].\n\r" + er.Message, EventLogEntryType.Error);
            }
        }

        public static  void deleteRemotedirectories(string PathToMonitor, string CopyTo)
        {
            try
            {
               
                var srcFiles = Directory.EnumerateDirectories (PathToMonitor, "*.*", SearchOption.AllDirectories);
                var desFiles = Directory.EnumerateDirectories(CopyTo, "*.*", SearchOption.AllDirectories);
                foreach (var ditem in desFiles)
                {
                    bool founded = false;
                    foreach (var sitem in srcFiles)
                    {
                        string d = ditem.ToLower().Trim().Replace(CopyTo.ToLower(), PathToMonitor.ToLower());
                        if (d == sitem.ToLower().Trim())
                        {
                            founded = true;
                            break;
                        }
                    }
                    try
                    {
                        if (!founded)
                            DeleteFileSystemInfo(new DirectoryInfo(ditem));
                    }
                    catch (Exception er)
                    {
                        EventLog.WriteEntry(_ServiceName, "deleteRemotedirectories Function[Error].\n\rUnable to delete directory\n\r" + er.Message, EventLogEntryType.Error);
                    }
                }
            }
            catch (Exception er)
            {
                EventLog.WriteEntry(_ServiceName, "deleteRemotedirectories Function[Error].\n\r" + er.Message, EventLogEntryType.Error);
            }

        }
        private static void DeleteFileSystemInfo(FileSystemInfo fileSystemInfo)
        {
            if (fileSystemInfo.Exists) return;
            var directoryInfo = fileSystemInfo as DirectoryInfo;
            if (directoryInfo != null)
            {
                foreach (var childInfo in directoryInfo.GetFileSystemInfos())
                {
                    DeleteFileSystemInfo(childInfo);
                }
            }

            fileSystemInfo.Attributes = FileAttributes.Normal;
            fileSystemInfo.Delete();
        }

        public static  void deleteRemotefiles(string PathToMonitor, string CopyTo)
        {
            try
            {
               
                if (string.IsNullOrWhiteSpace(PathToMonitor)) return; if (PathToMonitor.Trim().Length < 2) return;//at least should be 1 char
                if (string.IsNullOrWhiteSpace(CopyTo)) return; if (CopyTo.Trim().Length < 2) return;//at least should be 1 char

                if (!Directory.Exists(PathToMonitor)) return;
                if (!Directory.Exists(CopyTo)) return;
                deleteRemotedirectories(PathToMonitor, CopyTo);
               
                var srcFiles = Directory.EnumerateFiles(PathToMonitor, "*.*", SearchOption.AllDirectories);
                var desFiles = Directory.EnumerateFiles(CopyTo, "*.*", SearchOption.AllDirectories);
                foreach (var ditem in desFiles)
                {
                    bool founded = false;
                    foreach (var sitem in srcFiles)
                    {
                        string d = ditem.ToLower().Trim().Replace(CopyTo.ToLower(), PathToMonitor.ToLower());
                        if (d == sitem.ToLower().Trim())
                        {
                            founded = true;
                            break;
                        }
                    }
                    try
                    {
                        if (!founded)
                        {
                            var ff = new FileInfo(ditem);
                            ff.Attributes = FileAttributes.Normal;
                            ff.Delete();
                         //   File.Delete(ditem);
                        }
                    }
                    catch (Exception er)
                    {
                        EventLog.WriteEntry(_ServiceName, "deleteRemotefiles Function[Error].\n\rUnable to delete file\n\r" + er.Message, EventLogEntryType.Error);
                    }
                }
            }
            catch (Exception er)
            {
                EventLog.WriteEntry(_ServiceName, "deleteRemotefiles Function[Error].\n\r" + er.Message, EventLogEntryType.Error);
            }

        }
        */
        //bool IsMovingFiles = false;
        //bool CheckFiles = true ;

        private void timer1_Tick(object x)
        {
            try
            {
              

                if (ServiceStopping) return;
                foreach (var item in _watchers)
                {
                    if (item == null) continue;
                    if (item.process!=null && item.process.HasExited)
                    {
                        item.Start();
                    }

                }
            /*

                if (!CheckFiles) return;
                if (IsMovingFiles) return;

                if (_watchers.Count > 0)
                {
                    foreach (var item in _watchers)
                    {
                        if (!CheckFiles) return;
                        var to_orginal = "";
                        var from = item.fromPath.Path; // d:\ashraf
                        if (from[from.Length - 1] != Path.DirectorySeparatorChar)
                            from += Path.DirectorySeparatorChar;
                        var to = to_orginal = item.toPath.Path;   // \\10.10.143.3\ashrafk$\kamal

                        if (string.IsNullOrWhiteSpace(from)) continue;
                        if (string.IsNullOrWhiteSpace(to)) continue;
                        if (!Directory.Exists(from)) continue;


                        //var Src_Dir = Path.GetFileName(Path.GetDirectoryName(from));
                        //to = Path.Combine(to, Src_Dir.Trim().Trim('$'));               // \\10.10.143.3\ashrafk$\ashraf

                        if (from[from.Length - 1] != Path.DirectorySeparatorChar)
                            from += Path.DirectorySeparatorChar;
                        if (to[to.Length - 1] != Path.DirectorySeparatorChar)
                            to += Path.DirectorySeparatorChar;

                        deleteRemotefiles(from, to);
                        Thread.Sleep(1000);
                        copyDirectory(from, to);


                    }

                }*/

            }
            catch (Exception er)
            {
                _logger.LogWrite("Timer Function[Error].\n" + er.Message + "\n\r--------------------------------" + er.StackTrace);
                
            }
            finally
            {
               // CheckFiles = false;
                timer1.Change(TIME_INTERVAL_IN_MILLISECONDS, Timeout.Infinite);
            }
        }


        int isdateloged = 0;
        static string _ServiceName = "";
       static   bool copyFile(string src, string des)
        {
            try
            {
                File.Copy(src, des, true);
                return true;
            }
            catch (Exception er)
            {
                EventLog.WriteEntry(_ServiceName, "copyFile Function[Error].\n" + er.Message + "\n--------------------------------" + er.StackTrace, EventLogEntryType.Error);
                return false;
            }
           
            /* if (!UNCAccessWithCredentials.PathIsUNC(src))
             {
                 File.Copy(src, des, true);
                 return true ;
             }
             using (UNCAccessWithCredentials unc = new UNCAccessWithCredentials())
             {
                 string uncpath = Path.GetDirectoryName(src);
               
                 if (unc.NetUseWithCredentials(uncpath, username, domain , pass ))
                 {
                     File.Copy(src,des,true );
                    unc.NetUseDelete();
                    return true;
                 }
                 else
                 {
                     // The connection has failed. Use the LastError to get the system error code
                     throw new Exception("Failed to connect to " + uncpath +
                                     "\r\nLastError = " + unc.LastError.ToString());                                    
                 }
                 // When it reaches the end of the using block, the class deletes the connection.
             }*/

        }

    }
}
