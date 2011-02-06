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

            if (serviceRunning() == true)
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

    // string extension class
    public static class StringExtension
    {
        // string extension method ToUpperFirstLetter
        public static string ToUpperFirstLetter(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            // convert to char array of the string
            char[] letters = source.ToCharArray();
            // upper case the first char
            letters[0] = char.ToUpper(letters[0]);
            // return the array made of the new char array
            return new string(letters);
        }
    }
}
