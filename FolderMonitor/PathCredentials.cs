using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Security;
using System.Text;
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

        public string UNC_IPC { get
            {
                return  @"\\" + System.IO.Path.Combine(GetHostNameOfUNCPath(), "IPC$");
            }
        }
        public void ConnectToUNC(bool throwException)
        {
            try
            {


                if (!IsUNC)
                    throw new Exception("You can't connect to local, it must be a UNC path such as \\\\fileserver\\sharename");
                if (IsPathHasUserName)
                    Network.Host.ConnectTo(Path , new System.Net.NetworkCredential(UserName, Password, Domain), false, false, false);
            }
            catch (Exception er)
            {
                if (throwException)
                    throw new Exception (er.InnerMessages ()+Environment.NewLine + "*trying to connect to " + Path );
            }
        }

        public void DisconnectFromUNC()
        {
            try
            {
                Network.Host.DisconnectFrom(Path , true, false);

            }
            catch
            {

                
            }

        }
        public bool CheckAccessiblity()
        {
            if (IsPathHasUserName && !IsUNC) // if path has a user name then check if we can run a robocopy over that username.
                if (!CanRunProcess())
                    throw new Exception("Can't run Robocopy using the provided parameters.");
            if (IsPathHasUserName && IsUNC)

            {
                bool exist = false;
                Network.Host.ConnectTo(Path, new System.Net.NetworkCredential(UserName, Password, Domain), false, false, false);
                exist= Directory.Exists(Path);
                Network.Host.DisconnectFrom(Path, true, false);
                if(!exist)
                    throw new Exception("Can't access to path:"+ Environment.NewLine + Path + Environment.NewLine + "*Note that, connect to above path using user '"+ UserName + "' was succeeded.");

                return exist; 
            }
            else
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

                if (IsPathHasUserName & !IsUNC)
                {
                    using (Tools.Impersonator i = new Tools.Impersonator(UserName, Domain, Password))
                    {
                        return Directory.Exists(Path);
                    }
                }
                else if (IsPathHasUserName & IsUNC)
                  ConnectToUNC(throwException);
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

            if (IsPathHasUserName & !IsUNC)
            {
                using (Tools.Impersonator i = new Tools.Impersonator(UserName, Domain, Password))
                {
                    return Directory.GetFileSystemEntries(Path, searchPattern, searchOption);
                }
            }
            else if (IsPathHasUserName & IsUNC)
                ConnectToUNC(false );
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
        public bool IsEnabled { get;  set; }

        public PathFromAndTo()
        {
            From = new PathCredentials ();
            To = new PathCredentials ();
            IsEnabled = true;
        }
        public PathFromAndTo(PathCredentials from,PathCredentials to)
        {
            From = from;
            To = to;
            IsEnabled = true;
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
            cc.IsEnabled = IsEnabled;
            return cc;
        }
    }


   

}
