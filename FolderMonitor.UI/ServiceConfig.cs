using FolderMonitor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace FolderMonitor
{
    internal sealed partial class ServiceConfig
    {
        public static string _configFile = "";
        private static ServiceConfig defaultInstance = (new ServiceConfig());

        public static ServiceConfig Default
        {
            get
            {

                return defaultInstance;
            }
        }


        public ServiceConfig()
        {

        }

        public static string RoboCopyOptions = "/E /FFT /Z /W:20 /R:100  /ETA /MON:1"; // /MT causes issues
        public static string ServiceFilename = "FolderMonitor.exe";
        public static string ServiceConfigFilename = "FolderMonitor.exe.conf";

        public static string FolderMonitorLogFilename = "FolderMonitor.log";

        public void SaveChanges(List<PathFromAndTo> paths)
        {
            var items = new List<PathFromAndTo>(paths.Select(x => (PathFromAndTo)x.Clone()));
            foreach (var item in items)
            {
                if (!string.IsNullOrWhiteSpace(item.From.Password))
                    item.From.Password = item.From.Password.Encrypts();

                if (!string.IsNullOrWhiteSpace(item.To.Password))
                    item.To.Password = item.To.Password.Encrypts();
            }
            XmlSerializer xs = new XmlSerializer(typeof(List<PathFromAndTo>));
            TextWriter tw = new StreamWriter(_configFile);
            xs.Serialize(tw, items);
            tw.Close();
            tw.Dispose();
        }

        public static string RobocopyPath
        {
            get
            {
                var r = Environment.GetFolderPath(Environment.SpecialFolder.System);
                r = Path.Combine(r, "robocopy.exe");
                return r;
            }
        }
        public static bool DoesRoboCopExist()
        {

            return File.Exists(RobocopyPath);


        }

        public List<PathFromAndTo> GetAllTasks(bool EnabaledOnly)
        {

            List<PathFromAndTo> items = new List<PathFromAndTo>();
            if (!File.Exists(_configFile))
                return items;
            using (var sr = new FileStream(_configFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                XmlSerializer xs = new XmlSerializer(typeof(List<PathFromAndTo>));
                var xitems = (List<PathFromAndTo>)xs.Deserialize(sr);

                foreach (var item in xitems)
                {
                    if (EnabaledOnly && !item.IsEnabled) continue;
                    if (!string.IsNullOrWhiteSpace(item.From.Password))
                        item.From.Password = item.From.Password.Decrypts();

                    if (!string.IsNullOrWhiteSpace(item.To.Password))
                        item.To.Password = item.To.Password.Decrypts();
                    items.Add(item);
                }
                sr.Close();

                return items;
            }
        }

        public PathFromAndTo GetTask(string from_path,string to_path)
        {

          return   GetAllTasks(false ).FirstOrDefault(x => 
                   from_path.ToLower().Trim().Trim("\\".ToCharArray()) == x.From.Path.ToLower().Trim().Trim("\\".ToCharArray()) &&
                   x.To .Path.ToLower().Trim().Trim("\\".ToCharArray()) == to_path.ToLower().Trim().Trim("\\".ToCharArray()
                   ));
        }


    }
}
