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
            if (level.Equals("debug") && !isDebug)
            {
                return;
            }
            DateTime currentTime = DateTime.Now;
            output = currentTime.ToString("%H:") + currentTime.ToString("mm:") + currentTime.ToString("ss") + "  " + output + "\n";
            theLog = theLog + output;

            //Tell the Form to write to the messagebox in the UI
            if (outputbox != null) {
                Form1.output(output, level, ref outputbox);
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

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(fs);

            DateTime datetime = DateTime.Now;
            String header = "XMTuner Output\n";
            header = header + "Build: "+version+" \n";
            header += datetime.ToString() + "\n\n";
            if (outputbox != null)
            {
                textOut.Write(header + outputbox.Text);
            }
            else
            {
                textOut.Write(header + theLog);
            }
            

            textOut.Close();
        }
    }
}
