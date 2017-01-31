using System;


namespace FolderMonitor
{
    static class Program
    {


        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {

           

#if DEBUG

            var s = new Service1();
            s.StartMonitor();
            System.Windows.Forms.Application.Run();
            

#else


            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new Service1()
            };
            ServiceBase.Run(ServicesToRun);
#endif
        }
        static void sync_ErrorOccured(object sender, Exception exception)
        {



        }

    }

}
    

