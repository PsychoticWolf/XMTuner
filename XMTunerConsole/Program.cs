/*
 * XMTuner: Copyright (C) 2009-2012 Chris Crews and Curtis M. Kularski.
 * 
 * This file is part of XMTuner.

 * XMTuner is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * XMTuner is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with XMTuner.  If not, see <http://www.gnu.org/licenses/>.
 */

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
