using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace XMReaderConsole
{
    class configMan
    {
        String[] config = new String[10];
        //static String[] newConfig;
        String path = "config.txt";
        public bool isConfig = false;

        public String[] getConfig()
        {
            //readConfig();
            return config;
        }
        public void writeConfig(String username, String password, String port, bool bitrate, bool autologin)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(fs);
            textOut.WriteLine("XMReader");
            textOut.WriteLine(username);
            textOut.WriteLine(password);
            textOut.WriteLine(port);
            textOut.WriteLine(bitrate);
            textOut.WriteLine(autologin.ToString());
            textOut.Close();
        }
        public void readConfig()
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader textIn = new StreamReader(fs);
            int i = 0;
            while(textIn.Peek() != -1)
            {
                isConfig = true;
                config[i] = textIn.ReadLine();
                i++;
            }
            textIn.Close();
        }

    }
}
