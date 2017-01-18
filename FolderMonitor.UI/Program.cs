using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FolderMonitor.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException; ;
            ToolStripManager.Renderer = new Office2007Renderer.Office2007Renderer();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            ShowUnhandledExceptionMessageBox(e.Exception);
           // Application.Exit();
        }
        private static void ShowUnhandledExceptionMessageBox(Exception  e)
        {
            try
            {
                string msg =  e.InnerMessages ();
                MessageBox.Show(msg, "FolderMonitor GUI", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch { }
        }
    }
}
