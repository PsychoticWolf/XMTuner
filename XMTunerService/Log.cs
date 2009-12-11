using System;
using System.IO;
using System.Windows.Forms;

namespace XMTuner
{
    public class Log
    {
        RichTextBox outputbox;
        Boolean isDebug = true;
        Boolean useLocalDatapath = false;

        public Log()
        {
        }

        public Log(ref RichTextBox box1, Boolean bUseLocalDataPath)
        {
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

            //Tell the Form to write to the messagebox in the UI
            if (outputbox != null) {
                Form1.output(output, level, ref outputbox);
            }

        }

        public void log(int i)
        {
            String directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            String file = "XMTuner.log";
            String path;
            path = directory + "\\" + file;
            if (!useLocalDatapath)
            {
                path = file;
            }

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(fs);

            DateTime datetime = DateTime.Now;
            String header = "XMTuner Output\n";
            header += datetime.ToString() + "\n\n";
            textOut.Write(header + outputbox.Text + "\nTime: " + i);

            textOut.Close();
        }
    }
}
