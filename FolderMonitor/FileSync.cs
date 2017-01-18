using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using ConnectUNCWithCredentials;
using FolderMonitor;

namespace Ashrafnet.FileSync
{
    /// <summary>
    /// String Extensions
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Compares the string against a given pattern.
        /// </summary>
        /// <param name="str">The string.</param>
        /// <param name="wildcard">The wildcard, where "*" means any sequence of characters, and "?" means any single character.</param>
        /// <returns><c>true</c> if the string matches the given pattern; otherwise <c>false</c>.</returns>
        public static bool GlobMatch(this string str, string wildcard)
        {
            return new Regex(
                "^" + Regex.Escape(wildcard).Replace(@"\*", ".*").Replace(@"\?", ".") + "$",
                RegexOptions.IgnoreCase | RegexOptions.Singleline
            ).IsMatch(str);
        }
    }


    /// <summary>
    /// Syncs files from one folder to another folder using the <c>FileSystemWatcher</c>
    /// </summary>
    public class FileSync
    {
        /// <summary>
        /// Path to listen for changes
        /// </summary>
        public  PathCredentials fromPath = null ;
        /// <summary>
        /// Path, where to apply the changes
        /// </summary>
        public  PathCredentials toPath = null  ;

        /// <summary>
        /// Watcher for the <c>fromPath</c>
        /// </summary>
        protected FileSystemWatcher fromWatcher = null;
        /// <summary>
        /// Watcher for the <c>toPath</c>, just set when using 2 way sync
        /// </summary>
        protected FileSystemWatcher toWatcher = null;

        /// <summary>
        /// Delegate for renamed events
        /// </summary>
        /// <param name="sender">Object, where the event occured</param>
        /// <param name="oldFileName">the old filename</param>
        /// <param name="newFileName">the new filename</param>
        public delegate void FileRenamedHandler(object sender, string oldFileName, string newFileName);
        /// <summary>
        /// Delegate for file creations, changes or deletions
        /// </summary>
        /// <param name="sender">Object, where the event occured</param>
        /// <param name="fileName">the filename</param>
        public delegate void FileChangeHandler(object sender, string fileName);

        /// <summary>
        /// Delegate for errors occurs wile filesync working
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="exception"></param>
        public delegate void ErrorHandler(object sender, Exception exception);


        /// <summary>
        /// Destination file was renamed
        /// </summary>
        public event FileRenamedHandler Renamed;
        /// <summary>
        /// Destination file was created
        /// </summary>
        public event FileChangeHandler Created;
        /// <summary>
        /// Destination file was deleted
        /// </summary>
        public event FileChangeHandler Deleted;
        /// <summary>
        /// Destination file was changed
        /// </summary>
        public event FileChangeHandler Changed;

        public event ErrorHandler ErrorOccured;

        /// <summary>
        /// Is the <c>FileSync</c> <c>Running</c>?
        /// </summary>
        public bool Running
        {
            get
            {
                return fromWatcher != null;
            }
            private set { }
        }

        /// <summary>
        /// Is the <c>FileSync</c> <c>Running</c> in 2 way mode?
        /// </summary>
        public bool IsTwoWaySyncRunning
        {
            get
            {
                return fromWatcher != null && toWatcher != null;
            }
            private set { }
        }

        /// <summary>
        /// Glob filters, that should be ignored
        /// </summary>
        /// <example> 
        /// This sample shows how to use the <see cref="IgnoreGlobFilters"/> property.
        /// <code>
        /// FileSync sync = new FileSync(@"c:\testdir1\", @"c:\testdir2\");
        /// sync.IgnoreGlobFilters = new string[] {
        ///    "*/tmp/*", // ignore all tmp directories
        ///    "*.exe",   // ignore all exe files
        /// };
        /// sync.Start();
        /// </code>
        /// </example>
        public string[] IgnoreGlobFilters = new string[] { };

        /// <summary>
        /// The Constructor
        /// </summary>
        /// <param name="fromPath">Source path, where to listen for changes</param>
        /// <param name="toPath">Destination path, where to apply changes</param>
        public FileSync(PathCredentials fromPath, PathCredentials toPath)
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
        ~FileSync()
        {
            if (Running)
            {
                Stop();
            }

        }

        /// <summary>
        /// Attaches a logger to the FileSsync
        /// </summary>
        /// <param name="logger">the logger</param>
        public void AttachLogger(ILogger logger)
        {
            this.Deleted += logger.OnSyncDeleted;
            this.Changed += logger.OnSyncChanged;
            this.Created += logger.OnSyncCreated;
            this.Renamed += logger.OnSyncRenamed;
            this.ErrorOccured += logger.OnErrorOccure;
        }

        /// <summary>
        /// Start the file synchronization
        /// </summary>
        /// <param name="twoWaySync">when <c>true</c>, apply changes in the destination path back to the source path. when <c>false</c>, just apply any source changes to the destination</param>
        public void Start(bool twoWaySync = false)
        {
            if (Running)
            {
                var err = new FileSyncException("Filesync is already running.");
                if (ErrorOccured != null) ErrorOccured(this, err);
                return;
            }
            UNCAccessWithCredentials unc = null;
            if (toPath.IsUNC )
            {
                unc = new UNCAccessWithCredentials() { AutoDispose = false };
                if (toPath.IsPathHasUserName && ( !unc.NetUseWithCredentials(toPath.Path.Trim().TrimEnd('\\'), toPath.UserName, toPath.Domain, toPath.Password) && unc.LastError != 1219))
                {
                    if (!toPath.Path.StartsWith(@"\\10.10."))
                        if (ErrorOccured != null) ErrorOccured(this, new Exception("Failed to connect to " + toPath + "\r\nYou have to point to remote share folder using IP address instead of DNS name, So Remote Share folder should look like:\\\\10.10..\\SharefolderName."));
                    else
                            if (ErrorOccured != null) ErrorOccured(this, new Exception("Failed to connect to " + toPath + "\r\nLastError = " + unc.LastError.ToString()));
                   // return;
                }
            }

            UNCAccessWithCredentials unc_from = null;
            if (fromPath.IsUNC )
            {
                unc_from = new UNCAccessWithCredentials() { AutoDispose = false };
                if (fromPath.IsPathHasUserName &&( !unc_from.NetUseWithCredentials(fromPath.Path.Trim().TrimEnd('\\'), fromPath.UserName, fromPath.Domain, fromPath.Password) && unc.LastError != 1219))
                {
                    if (!fromPath.Path.StartsWith(@"\\10.10."))
                        if (ErrorOccured != null) ErrorOccured(this, new Exception("Failed to connect to " + fromPath + "\r\nYou have to point to remote share folder using IP address instead of DNS name, So Remote Share folder should look like:\\\\10.10..\\SharefolderName."));
                    else
                            if (ErrorOccured != null) ErrorOccured(this, new Exception("Failed to connect to " + fromPath + "\r\nLastError = " + unc_from.LastError.ToString()));                        
                   // return ;
                }
            }
            fromWatcher = new FileSystemWatcher(fromPath.Path );
            fromWatcher.InternalBufferSize = 65536;
            fromWatcher.IncludeSubdirectories = true;
            fromWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
            // Add event handlers.
            fromWatcher.Changed += new FileSystemEventHandler(OnChanged);
            fromWatcher.Created += new FileSystemEventHandler(OnChanged);
            fromWatcher.Deleted += new FileSystemEventHandler(OnChanged);
            fromWatcher.Renamed += new RenamedEventHandler(OnRenamed);
            fromWatcher.Error += fromWatcher_Error;
            // start watching
            fromWatcher.EnableRaisingEvents = true;

            if (twoWaySync)
            {
                toWatcher = new FileSystemWatcher(toPath.Path );
                toWatcher.InternalBufferSize = 65536;
                toWatcher.IncludeSubdirectories = true;
                toWatcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.LastWrite | NotifyFilters.CreationTime;
                // Add event handlers.
                toWatcher.Changed += new FileSystemEventHandler(OnChanged);
                toWatcher.Created += new FileSystemEventHandler(OnChanged);
                toWatcher.Deleted += new FileSystemEventHandler(OnChanged);
                toWatcher.Renamed += new RenamedEventHandler(OnRenamed);
                toWatcher.Error += toWatcher_Error;

                // start watching
                toWatcher.EnableRaisingEvents = true;
            }
        }

        void toWatcher_Error(object sender, ErrorEventArgs e)
        {
            if (ErrorOccured != null) ErrorOccured(sender, e.GetException());
        }

        void fromWatcher_Error(object sender, ErrorEventArgs e)
        {
            if (ErrorOccured != null) ErrorOccured(sender, e.GetException());
        }

        /// <summary>
        /// Stop the file synchronization
        /// </summary>
        public void Stop()
        {
            if (!Running)
            {
                var err=new FileSyncException("Filesync is not running.");
                if (ErrorOccured != null) ErrorOccured(this, err);
                return;
            }
            if (fromWatcher != null)
            {
                fromWatcher.EnableRaisingEvents = false;
                fromWatcher = null;
            }
            if (toWatcher != null)
            {
                toWatcher.EnableRaisingEvents = false;
                toWatcher = null;
            }
        }

        /// <summary>
        /// Trys to set normal file attributes
        /// </summary>
        /// <param name="fullNewPath">The path to the file</param>
        protected void tryToSetNormalPermissions(string fullNewPath)
        {
            try
            {
                File.SetAttributes(fullNewPath, FileAttributes.Normal);
            }
            catch { }
        }

        /// <summary>
        /// Method for the the <c>FileSystemEventHandler</c>
        /// </summary>
        /// <param name="source">Object where the event occured</param>
        /// <param name="e">Event data</param>
        protected void OnChanged(object source, FileSystemEventArgs e)
        {
            // any excludes?
            foreach (string filter in IgnoreGlobFilters)
            {
                if (e.FullPath.GlobMatch(filter))
                {
                    return;
                }
            }

            string fullNewPath = "";
            if (e.FullPath.StartsWith(fromPath.Path))
            {
                fullNewPath = toPath + e.FullPath.Substring(fromPath.Path .Length);
            }
            else if (e.FullPath.StartsWith(toPath.Path))
            {
                fullNewPath = fromPath + e.FullPath.Substring(toPath.Path.Length);
            }
            else
            {
                return;
            }


            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Created:
                    try
                    {
                        if ((File.GetAttributes(e.FullPath) & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            if (!Directory.Exists(fullNewPath))
                            {
                                Directory.CreateDirectory(fullNewPath);
                                Created(this, fullNewPath);
                            }

                        }
                        else
                        {
                            try
                            {
                                FileInfo fromFile = new FileInfo(e.FullPath);
                                FileInfo toFile = new FileInfo(fullNewPath);
                                if (fromFile.LastWriteTime >= toFile.LastWriteTime.AddSeconds(2) && fromFile.Length != toFile.Length)
                                {
                                    tryToSetNormalPermissions(fullNewPath);
                                    File.Copy(e.FullPath, fullNewPath, true);
                                    Changed(this, fullNewPath);
                                }

                            }
                            catch
                            {
                                File.Copy(e.FullPath, fullNewPath, true);
                                tryToSetNormalPermissions(fullNewPath);
                                Created(this, fullNewPath);
                            }
                        }
                    }
                    catch (Exception er) { if (ErrorOccured != null) ErrorOccured(source, new Exception(string.Format("On Created\n\rFrom:{0}\n\rTo:{1}\n\rError:{2}", fromPath, toPath, er.Message))); }
                    break;
                case WatcherChangeTypes.Changed:
                    try
                    {
                        if ((File.GetAttributes(e.FullPath) & FileAttributes.Directory) != FileAttributes.Directory)
                        {
                            try
                            {
                                FileInfo fromFile = new FileInfo(e.FullPath);
                                FileInfo toFile = new FileInfo(fullNewPath);
                                if (fromFile.LastWriteTime >= toFile.LastWriteTime.AddSeconds(2) && fromFile.Length != toFile.Length)
                                {
                                    tryToSetNormalPermissions(fullNewPath);
                                    File.Copy(e.FullPath, fullNewPath, true);
                                    Changed(this, fullNewPath);
                                }
                            }
                            catch
                            {
                                tryToSetNormalPermissions(fullNewPath);
                                File.Copy(e.FullPath, fullNewPath, true);
                                Created(this, fullNewPath);
                            }
                        }
                    }
                    catch (Exception er) { if (ErrorOccured != null) ErrorOccured(source, new Exception(string.Format("On Changed\n\rFrom:{0}\n\rTo:{1}\n\rError:{2}", fromPath, toPath, er.Message))); }
                    break;
                case WatcherChangeTypes.Deleted:
                    try
                    {
                        if ((File.GetAttributes(fullNewPath) & FileAttributes.Directory) == FileAttributes.Directory)
                        {
                            if (Directory.Exists(fullNewPath))
                            {
                                Directory.Delete(fullNewPath,true  );
                                Deleted(this, fullNewPath);
                            }

                        }
                        else
                        {
                            if (File.Exists(fullNewPath))
                            {
                                tryToSetNormalPermissions(fullNewPath);
                                File.Delete(fullNewPath);
                                Deleted(this, fullNewPath);
                            }
                        }

                    }
                    catch (Exception er) { if (ErrorOccured != null) ErrorOccured(source, new Exception(string.Format("On Delete\n\rFrom:{0}\n\rTo:{1}\n\rError:{2}", fromPath, toPath, er.Message))); }
                    break;

            }

            //Console.WriteLine("File: " + e.FullPath + " " + e.ChangeType);
            //Console.WriteLine("  ==> " + fullNewPath);
        }

        /// <summary>
        /// Method for the the <c>RenamedEventHandler</c> 
        /// </summary>
        /// <param name="source">Object where the event occured</param>
        /// <param name="e">Event data</param>
        protected void OnRenamed(object source, RenamedEventArgs e)
        {
            // any excludes?
            foreach (string filter in IgnoreGlobFilters)
            {
                if (e.OldFullPath.GlobMatch(filter) || e.FullPath.GlobMatch(filter))
                {
                    return;
                }
            }

            string oldFullPath = "";
            string newFullPath = "";
            if (e.OldFullPath.StartsWith(fromPath.Path))
            {
                oldFullPath = toPath + e.OldFullPath.Substring(fromPath.Path.Length);
            }
            else if (e.OldFullPath.StartsWith(toPath.Path))
            {
                oldFullPath = fromPath + e.OldFullPath.Substring(toPath.Path.Length);
            }
            else
            {
                return;
            }

            if (e.FullPath.StartsWith(fromPath.Path))
            {
                newFullPath = toPath + e.FullPath.Substring(fromPath.Path.Length);
            }
            else if (e.FullPath.StartsWith(toPath.Path))
            {
                newFullPath = fromPath + e.FullPath.Substring(toPath.Path.Length);
            }
            else
            {
                return;
            }

            try
            {
                if ((File.GetAttributes(oldFullPath) & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    Directory.Move(oldFullPath, newFullPath);
                    Renamed(this, oldFullPath, newFullPath);
                }
                else
                {
                    if (File.Exists(oldFullPath))
                    {
                        File.Move(oldFullPath, newFullPath);
                        Renamed(this, oldFullPath, newFullPath);
                    }
                    else
                    {
                        File.Copy(e.FullPath, newFullPath, true);
                        tryToSetNormalPermissions(newFullPath);
                        Created(this, newFullPath);

                    }
                }
            }
            catch (Exception er) {if(ErrorOccured!=null ) ErrorOccured(source, new Exception(string.Format("On Renamed\n\r\tFrom: {0}\n\r\tTo: {1}\n\rError: {2}", fromPath, toPath, er.Message))); }

        }

    }

}
