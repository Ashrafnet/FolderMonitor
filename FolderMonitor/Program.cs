using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using ConnectUNCWithCredentials;
using Ashrafnet.FileSync;
using System.Linq;


namespace FolderMonitor
{
    static class Program
    {
       

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

            //var ss = new Service1();
            //ss.startservice();

           

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new Service1() 
			};
            ServiceBase.Run(ServicesToRun);
        }
       static  void  sync_ErrorOccured(object sender, Exception exception)
        {

         

        }
      
    }
}
