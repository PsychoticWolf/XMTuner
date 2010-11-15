using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace XMTuner
{
    public enum channelListStyles { All, Category, Favorites };

    public class Config
    {
        //Internal values
        private configMan cm;
        private NameValueCollection config;

        //Configuration Values (properties)
        public Boolean autologin;
        public String username;
        public String password;
        public String network;

        public Boolean showNotification;
        public Boolean onTop;
        public Int32 numRecentHistory;
        public channelListStyles channelListStyle;

        public Boolean enableServer;
        public String port;
        public Boolean useMMS;
        public String bitrate;
        //public String serverPassword;

        public String tversityServer;

        //public Boolean enableRemoteControl;

        public String hostname;
        //public String nscaServer;
        //public String proxyServer;

        //Additional Values
        public String ip;

        public Boolean loaded
        {
            get
            {
                if (cm == null)
                {
                    return false;
                }
                return cm.loaded;
            }
        }

        // Config Update Event
        public event ConfigUpdateHandler update;
        public delegate void ConfigUpdateHandler(Config c, ConfigUpdateEventArgs e);

        public Config()
        {
            cm = new configMan();
            loadConfig();
        }

        public Config(Boolean skipConfigLoading)
        {
        }

        private void loadConfig()
        {
            NameValueCollection config = cm.getConfig(true);
            ip = configMan.getLocalIP();

            autologin = cm.getConfigItemAsBoolean("autologin");
            username = config["username"];
            password = config["password"];
            network = config["network"];

            showNotification = cm.getConfigItemAsBoolean("showNotice");
            onTop = cm.getConfigItemAsBoolean("alwaysOnTop");
            numRecentHistory = Convert.ToInt32(config["numRecentHistory"]);

            //Set channelListStyle...
            switch (config["channelListStyle"].ToLower()) {
                case "all":
                    channelListStyle = channelListStyles.All;
                    break;
                case "category":
                    channelListStyle = channelListStyles.Category;
                    break;
                case "favorites":
                    channelListStyle = channelListStyles.Favorites;
                    break;
            }

            enableServer = cm.getConfigItemAsBoolean("enableServer");
            port = config["port"];
            useMMS = cm.getConfigItemAsBoolean("useMMS");
            bitrate = config["bitrate"];

            tversityServer = config["tversityServer"];

            hostname = config["hostname"];

            this.config = config;
        }

        public Boolean reload()
        {
            if (config == null || cm == null) { return false; }
            NameValueCollection prevconfig = config;
            cm = new configMan();
            loadConfig();

            //Get list of updated values if we're updating config
            if (prevconfig != null)
            {
                String updated_string;
                List<String> updatedvalues = compareConfig(prevconfig, config, out updated_string);
                if (updatedvalues != null)
                {
                    //Notify interested parties that we're reloading the config
                    update(this, new ConfigUpdateEventArgs(updatedvalues, updated_string));
                }
            }
            return true;
        }

        private List<String> compareConfig(NameValueCollection prevconfig, NameValueCollection config, out String updatedvalues)
        {
            List<String> changedValues = new List<String>();
            foreach (String value in config.AllKeys)
            {
                if (config[value] != prevconfig[value])
                {
                    changedValues.Add(value.ToLower());
                }
            }
            if (changedValues.Count > 0)
            {
                updatedvalues = "";
                for (int i = 0; i < changedValues.Count; i++)
                    updatedvalues += changedValues[i] + " ";
                return changedValues;
            }
            updatedvalues = null;
            return null;
        }
    }

    public class ConfigUpdateEventArgs : EventArgs
    {
        public List<String> data;
        public String details;

        public ConfigUpdateEventArgs(List<String> data, String details)
        {
            this.data = data;
            this.details = details;
        }
    }
}
