using System;
using System.IO;
using System.Windows.Forms;

namespace XMTuner
{
    public class Log
    {
        RichTextBox outputbox;
        String theLog = "";
        String logFile = "applicaiton.log";
        Boolean isDebug = true;
        Boolean useLocalDatapath = false;

        public Log()
        {
            logFile = "XMTunerService.log";
        }

        public Log(ref RichTextBox box1, Boolean bUseLocalDataPath)
        {
            logFile = "XMTuner.log";
            outputbox = box1;
            useLocalDatapath = bUseLocalDataPath;
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

        }

        public void log(int i)
        {
            String directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            String file = logFile;
            String path;
            //output(directory, "message");
            path = directory + "\\" + file;
            //path = file;

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(fs);

            DateTime datetime = DateTime.Now;
            String header = "XMTuner Output\n";
            header = header + "Build: 0.4 \n";
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
