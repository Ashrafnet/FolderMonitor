using ConnectUNCWithCredentials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Text.RegularExpressions;


namespace FolderMonitor
{
    [Serializable ]
    public class PathCredentials:ICloneable
    {
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        [ResourceExposure(ResourceScope.None)]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        internal static extern bool PathIsUNC([MarshalAsAttribute(UnmanagedType.LPWStr), In] string pszPath);
        public PathCredentials()
        {
            ExcludedFiles = new List<string>();
            ExcludedFolders = new List<string>();
        }
        private string _pass = null;
        /// <summary>
        /// The full path of folder.
        /// </summary>
         [Description("The full path of folder.")]
        public string Path { get; set; }
        /// <summary>
        /// Username used to gain access for this path
        /// </summary>
        [Category("Security")]
        [Description("Username used to gain access for this path.")]
        public string UserName { get; set; }
        /// <summary>
        /// Password used to gain access for this path.
        /// </summary>
        [Category("Security")]
        [Description("Password used to gain access for this path.")]
        [PasswordPropertyText(true)]
        public string Password { get
            {
                return _pass;
            }
            set
            {
                _pass = value;
            }
        }
        /// <summary>
        /// Domain used to gain access for this path
        /// </summary>
        [Category("Security")]
        [Description("Domain used to gain access for this path.")]
        public string Domain { get; set; }

        /// <summary>
        /// Indicate if this path is a UNC resource or not.
        /// </summary>
        public bool IsUNC
        {
            get
            {
                return PathIsUNC(Path);
            }
        }

        public bool CheckAccessiblity()
        {
            if (IsPathHasUserName && !IsUNC) // if path has a user name then check if we can run a robocopy over that username.
                if (!CanRunProcess())
                    throw new Exception("Can't run Robocopy using the provided parameters.");
            if (IsPathHasUserName && IsUNC)

            {
                //\\RemoteServerName\IPC$
                string xx =@"\\"+ System.IO.Path.Combine(GetHostNameOfUNCPath(), "IPC$");
                var unc_from = new UNCAccessWithCredentials() { AutoDispose = false };
                if ((!unc_from.NetUseWithCredentials(xx, UserName, Domain, Password) && unc_from.LastError != 1219))
                {
                    if (!IsPathUNCStartswithIP)
                        throw new Exception("Failed to connect to " + Path + "\r\nYou have to point to remote share folder using IP address instead of DNS name, So Remote Share folder should look like:\\\\10.10..\\SharefolderName.");
                    else
                        throw new Exception("Failed to connect to " + Path + "\r\nLastError = " + unc_from.LastError.ToString());
                }
                unc_from.Dispose();
            }
            return PathExists(true);
        }

        private string GetHostNameOfUNCPath()
        {
            Uri uri = new Uri(Path );
            string[] segs = uri.Segments; 
            return uri.Host;
                
        }

        bool CanRunProcess()
        {
            var process = new Process();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.FileName = "ROBOCOPY.exe";

            if (IsPathHasUserName)
            {
                process.StartInfo.UserName = UserName;
                process.StartInfo.Password = Password.ToSecureString();
                if (!string.IsNullOrWhiteSpace(Domain))
                    process.StartInfo.Domain = Domain;
            }
            process.Start();
            process.Kill();
            return true;
        }

        /// <summary>
        /// Indicate if this path has a username or not.
        /// </summary>
        public bool IsPathHasUserName
        {
            get
            {
                return !string.IsNullOrWhiteSpace(UserName);
            }
        }

        /// <summary>
        /// Indicate if this path is a UNC path and starts with IP address or not.
        /// </summary>
        public bool IsPathUNCStartswithIP
        {
            get
            {
                if(IsUNC)
                {
                    if (Path.StartsWith(@"\\"))
                    {
                        Regex ip = new Regex(@"\b\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}\b");
                        MatchCollection result = ip.Matches(Path);
                        if (result!=null && result.Count > 0)
                            return true;
                    }
                }
                return false;
                
            }
        }

        public List<string> ExcludedFiles { get;  set; }
        public List<string> ExcludedFolders { get;  set; }


        public override string ToString()
        {
            return Path;
        }

        public bool PathExists(bool throwException=false )
        {
            try
            {

                if (IsPathHasUserName)
                {
                    using (Tools.Impersonator i = new Tools.Impersonator(UserName, Domain, Password))
                    {
                        return Directory.Exists(Path);
                    }
                }
                else
                    return Directory.Exists(Path);

            }
            catch (Exception er)
            {
                if (throwException)
                    throw;
                else
                    return false;
            }
        }
        public string[] GetFileSystemEntries(string searchPattern = "*.*", SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {

            if (IsPathHasUserName)
            {
                using (Tools.Impersonator i = new Tools.Impersonator(UserName, Domain, Password))
                {
                    return Directory.GetFileSystemEntries(Path, searchPattern, searchOption);
                }
            }
            else
                return Directory.GetFileSystemEntries(Path, searchPattern, searchOption);
        }

        public object Clone()
        {
           return  MemberwiseClone();
        }
    }
    [Serializable ]
    public class PathFromAndTo:ICloneable
    {
        public string  RoboCopy_Options { get; set; }
        public PathCredentials From { get; set; }
        public PathCredentials To { get; set; }
        public string ExtendedAttributes { get;  set; }

        public PathFromAndTo()
        {
            From = new PathCredentials ();
            To = new PathCredentials ();
        }
        public PathFromAndTo(PathCredentials from,PathCredentials to)
        {
            From = from;
            To = to;
        }
      

        public override string ToString()
        {
            var r= string.Format("{1}|{2}|{3}|{4};{5}|{6}|{7}|{8}", "<string>", From.Path, From.UserName, From.Password.Encrypts (), From.Domain, To.Path, To.UserName, To.Password.Encrypts (), To.Domain, "</string>");
            return r;
        }
        public  string ToString(bool WithXmlNode)
        {
            var r = string.Format("{0}{1}|{2}|{3}|{4};{5}|{6}|{7}|{8}{9}", "<string>", From.Path, From.UserName, From.Password.Encrypts (), From.Domain, To.Path, To.UserName, To.Password.Encrypts (), To.Domain, "</string>");
            return r;
        }

        public object Clone()
        {
            var cc = new PathFromAndTo();
            cc.From =(PathCredentials ) From.Clone();
            cc.To =(PathCredentials) To.Clone();
            cc.RoboCopy_Options = RoboCopy_Options;
            cc.ExtendedAttributes = ExtendedAttributes;
            return cc;
        }
    }


    public static class MyExtensions
    {
        /// <summary>
        /// Returns a Secure string from the source string
        /// </summary>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static SecureString ToSecureString(this string Source)
        {
            if (string.IsNullOrWhiteSpace(Source))
                return null;
            else
            {
                SecureString Result = new SecureString();
                foreach (char c in Source.ToCharArray())
                    Result.AppendChar(c);
                return Result;
            }
        }
        /// <summary>
        /// Encloses a path in double-quotes if it contains spaces and
        /// makes sure it can be used as Robocopy command-line argument.
        /// </summary>
        public static  string QuoteForRobocopy(this string path, bool force = false)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            path = Quote(path, force);

            //// \" is interpreted as escaped double-quote, so if the path ends with \, we need to escape the backslash => \\"
            if (path.EndsWith("\\\"", StringComparison.Ordinal))
                path = path.Substring(0, path.Length - 2) + "\\\\\"";

            return path;
        }
        /// <summary>
        /// Encloses a path in double-quotes if it contains spaces.
        /// </summary>
        public static  string Quote(string path, bool force = false)
        {
            if (string.IsNullOrEmpty(path))
                return path;

            if (!force && path.IndexOf(' ') < 0)
                return path;

            return string.Concat('"', path, '"');
        }

        public static string GetFolderName( this string path)
        {
            var end = -1;
            path += "";
            path = path.Trim();
            if (!path.EndsWith("\\"))
                path += "\\";
            for (var i = path.Length; --i >= 0;)
            {
                var ch = path[i];
                if (ch == System.IO.Path.DirectorySeparatorChar ||
                    ch == System.IO.Path.AltDirectorySeparatorChar ||
                    ch == System.IO.Path.VolumeSeparatorChar)
                {
                    if (end > 0)
                    {
                        return path.Substring(i + 1, end - i - 1);
                    }

                    end = i;
                }
            }

            if (end > 0)
            {
                return path.Substring(0, end);
            }

            return path;
        }

        public static string InnerMessages(this Exception er)
        {
            var msg = er.Message ;

            while (er.InnerException !=null  )
            {

                er = er.InnerException;
                msg += Environment.NewLine +"InnerException: " + er.Message;
            }
            return msg;
        }

        public static string Encrypts(this string str)
        {
           return  Encryption.Encrypt(str);
        }
        public static string Decrypts(this string str)
        {
            return Encryption.Decrypt (str);
        }
    }

}
