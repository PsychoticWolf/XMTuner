using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace XMTuner
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

#if !DEBUG

            if (serviceRunning() == false)
            {
                MessageBox.Show("XMTuner cannot start because the XMTuner service is running.\nStop the XMTuner service and try again.", "XMTuner Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
                return;
            }
#endif

            Application.Run(new Form1());
        }

        private static Boolean serviceRunning()
        {
            String lockFile = "xmtunerservice.lock";
            String path = configMan.datapath + "\\" + lockFile;

            if (System.IO.File.Exists(path))
            {
                int pid = Convert.ToInt32(CacheManager.getFileS(path));

                try
                {
                    if (System.Diagnostics.Process.GetProcessById(pid).ProcessName.ToLower().Contains("xmtunerservice"))
                    {
                        return true;
                    }
                }
                catch (ArgumentException) { }
                catch (InvalidOperationException) { }

            }
            return false;
        }
    }
}
