using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new WindowsFormsApplication5.MDIParent1 ());
        }


        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            if (e.Exception is ArgumentOutOfRangeException ex &&
                ex.StackTrace != null &&
                ex.StackTrace.Contains("DataGridViewColumnCollection.Clear"))
            {
                // ignore this specific dispose-time bug
                return;
            }

            // show your normal error UI for other exceptions
            MessageBox.Show(e.Exception.ToString(), "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
