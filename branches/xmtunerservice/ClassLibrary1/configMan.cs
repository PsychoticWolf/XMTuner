using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace XMTunerService
{
    class configMan
    {
        //String[] config = new String[10];
        NameValueCollection config = new NameValueCollection();
        //static String[] newConfig;
        String path = "config.txt";
        public bool isConfig = false;
        public bool protocolMMS = false;

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
