using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace XMReaderConsole
{
    class configMan
    {
        //collection to store configuration variables
        NameValueCollection config = new NameValueCollection();

        //path to Application Data folder
        String directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
        String file = "config.txt";
        String path;
        public bool isConfig = false;
        public bool protocolMMS = false;

        public configMan()
        {
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); } 
            path = directory + "\\" + file;
        }

        public configMan(bool debug)
        {
            if (debug)
            {
                path = "config.txt";
            }
            else
            {
                if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
                path = directory + "\\" + file;
            }
        }
        public NameValueCollection getConfig()
        {
            //readConfig();
            return config;
        }
        public void writeConfig(NameValueCollection newConfig)
        {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(fs);
            String value;
            textOut.WriteLine("XMTuner Configuration");
            textOut.WriteLine("ConfigVer,0.3");
            foreach (String configKey in newConfig.AllKeys)
            {
                value = newConfig.Get(configKey);
                textOut.WriteLine(configKey + "," + value); 
            }

            textOut.Close();
        }
        public void readConfig()
        {
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader textIn = new StreamReader(fs);
            int i = 0;
            String newline = "";
            String[] parts = new String[2];
            String header = textIn.ReadLine();
            while(textIn.Peek() != -1)
            {
                
                newline = textIn.ReadLine();
                if (newline.Contains(","))
                {
                    isConfig = true;
                    parts = newline.Split(',');
                    config.Add(parts[0], parts[1]);
                }
                i++;
            }
            textIn.Close();
        }


    }
}
