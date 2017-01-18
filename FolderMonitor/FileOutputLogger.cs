using System;
using System.IO;
using System.Reflection;

namespace Ashrafnet.FileSync
{

    public class FileOutputLogger : ILogger
    {
        public string FileOutputPath { get; set; }
        public void LogWrite(string logMessage, logType logType= logType.information  )
        {

            FileOutputPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var logFile = Path.Combine(FileOutputPath, ServiceConfig.FolderMonitorLogFilename );
            try
            {
                if (!File.Exists(logFile))
                    File.CreateText(logFile).Close ();
                using (StreamWriter w = File.AppendText(logFile))
                {
                    Log(logMessage, w, logType);
                    w.Close();
                }
            }
            catch
            {
            }
        }
        void Log(string logMessage, TextWriter txtWriter, logType logtype)
        {
            try
            {
                if (LogLevel == logType.All || LogLevel == logtype)
                {
                    var lognameType = Enum.GetName(typeof( logType), logtype);
                    txtWriter.Write(string.Format("\r\n{0} Entry : ",lognameType));
                    txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());
                    txtWriter.WriteLine("  {0}: ", logMessage.Replace ("\n\r",Environment.NewLine ));
                    txtWriter.WriteLine("-------------------------------");
                }
            }
            catch (Exception ex)
            {
            }
        }

        public logType LogLevel { get; set; }
        /// <summary>
        /// Triggers on <c>FileSync.Changed</c> events
        /// </summary>
        /// <param name="source">The sender</param>
        /// <param name="path">Path to the destination file, that was changed</param>
        public void OnSyncChanged(object source, string path)
        {
            
            LogWrite("Changed: " + path);
        }

        /// <summary>
        /// Triggers on <c>FileSync.Created</c> events
        /// </summary>
        /// <param name="source">The sender</param>
        /// <param name="path">Path to the destination file, that was changed</param>
        public void OnSyncCreated(object source, string path)
        {
            LogWrite("Created: " + path);
        }
        /// <summary>
        /// Triggers on <c>FileSync.Deleted</c> events
        /// </summary>
        /// <param name="source">The sender</param>
        /// <param name="path">Path to the destination file, that was changed</param>
        public void OnSyncDeleted(object source, string path)
        {
            LogWrite("Deleted: " + path);
        }
        /// <summary>
        /// Triggers on <c>FileSync.Renamed</c> events
        /// </summary>
        /// <param name="source">The sender</param>
        /// <param name="oldpath">Old Path to the destination file, that was changed</param>
        /// <param name="newpath">New Path to the destination file, that was changed</param>
        public void OnSyncRenamed(object source, string oldpath, string newpath)
        {
            LogWrite("Renamed From: " + oldpath +Environment.NewLine + "        To: " + newpath);
        }



        public void OnErrorOccure(object source, Exception exception)
        {
            LogWrite("Exception " + exception.Message, logType.Error );
        }
    }

    public enum logType
    {
        Error, information, Warning, Tip, All

    }
}
