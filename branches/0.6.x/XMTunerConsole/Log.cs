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
using System.IO;
using System.Windows.Forms;

namespace XMTuner
{
    public class Log
    {
        RichTextBox outputbox;
        String theLog = "";
        String logFile = "application.log";
        Boolean isDebug = false;
        Boolean useLocalDatapath = false;
        String version = configMan.version;

        public Log()
        {
#if DEBUG
            isDebug = true;
#endif
            logFile = "XMTunerService.log";
        }

        public Log(ref RichTextBox box1)
        {
#if DEBUG
            isDebug = true;
#endif
            logFile = "XMTuner.log";
            outputbox = box1;
            useLocalDatapath = new configMan().useLocalDatapath;
        }

        public void output(String output, String level)
        {
            if ((level.Contains("debug") || level.Equals("notice")) && !isDebug)
            {
                return;
            }
            DateTime currentTime = DateTime.Now;
            output = currentTime.ToString("%H:") + currentTime.ToString("mm:") + currentTime.ToString("ss") + "  " + output + "\r\n";

            //Tell the Form to write to the messagebox in the UI
            if (outputbox != null)
            {
                Form1.output(output, level, ref outputbox);
            }
            else
            {
                theLog = theLog + output;
            }

            log();
        }

        private void log()
        {
            String path = "";
            if (useLocalDatapath == false)
            {
                String directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
                if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
                path += directory + "\\";
            }
            path += logFile;
            FileStream fs;
            try
            {
                fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            }
            catch (IOException)
            {
                return;
            }
            StreamWriter textOut = new StreamWriter(fs);

            DateTime datetime = DateTime.Now;
            String header = "XMTuner Log\r\n";
            header = header + "Build: "+version+" \r\n";
            header += datetime.ToString() + "\r\n\r\n";
            if (outputbox != null)
            {
                if (outputbox.InvokeRequired)
                {
                    GetTextCallback d = new Log.GetTextCallback(GetText);
                    String text = outputbox.Invoke(d, new object[] { outputbox }) as String;
                    textOut.Write(header + text);
                }
                else
                {
                    textOut.Write(header + outputbox.Text);
                }
            }
            else
            {
                textOut.Write(header + theLog);
            }
            

            textOut.Close();
        }
        public delegate String GetTextCallback(ref RichTextBox outputbox);
        private static String GetText(ref RichTextBox outputbox)
        {
            return outputbox.Text;
        }
    }
}
