using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.IO;

namespace XMTuner
{
    class configMan
    {
        public bool useLocalDatapath = false;
        private bool isConfig = false;
        //collection to store configuration variables
        NameValueCollection config = new NameValueCollection();

        //path to Application Data folder
        String path;
        public configMan()
        {
#if DEBUG
            useLocalDatapath = true;
#endif
            path = getConfigPath(useLocalDatapath);
        }

        public Boolean loaded
        {
            get {
                if (config.Count == 0)
                {
                    readConfig();
                }
                return isConfig;
            }
        }

        private String getConfigPath() { return getConfigPath(false); }
        private String getConfigPath(Boolean useLocalDatapath)
        {
            String file = "config.txt";
            String directory = "";
            if (useLocalDatapath == false)
            {
                directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
                if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
                directory += "\\";
            }
            String path = directory + file;
            return path;
        }

        private void readConfig()
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

        public NameValueCollection getConfig()
        {
            if (config.Count == 0)
            {
                readConfig();
            }
            return config;
        }
        public NameValueCollection getConfig(Boolean interpreted)
        {
            NameValueCollection sConfig = getConfig();
            if (Convert.ToBoolean(sConfig["bitrate"])) { sConfig["bitrate"] = "high"; } else { sConfig["bitrate"] = "low"; }
            if (sConfig["hostname"] != "") { sConfig["hostname"] = sConfig["hostname"] + ":" + sConfig["port"]; }
            return sConfig;
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

        public String getConfigItem(NameValueCollection config, String item)
        {
            NameValueCollection defaultConfig = new NameValueCollection();
                defaultConfig.Add("ConfigVer", "0.3");
                defaultConfig.Add("username", "");
                defaultConfig.Add("password", "");
                defaultConfig.Add("port", "19081");
                defaultConfig.Add("bitrate", "True");
                defaultConfig.Add("autologin", "False");
                defaultConfig.Add("isMMS", "False");
                defaultConfig.Add("TVersity", "");
                defaultConfig.Add("hostname", "");
                defaultConfig.Add("network", "XM");

                if (config.Get(item) == null)
                {
                    return defaultConfig[item];
                }
                else
                {
                    return config[item];
                }
        }
    }
}
