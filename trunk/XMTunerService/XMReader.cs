using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Net;
using System.Timers;

namespace XMTuner
{
    public class XMReader
    {
        
        XMTuner self;
        public Log logging;
        WebListner xmServer;
        String username;
        String password;
        String network;
        String port;
        Boolean loggedIn = false;
        Boolean serverRunning = false;
        int i = 0;
        System.Timers.Timer theTimer = new System.Timers.Timer(30000);

        public XMReader()
        {
            logging = new Log();
        }

        public void run()
        {
            logging.output("XMTuner Service started", "message");
            logging.log(i);
            if (refreshConfig())
            {
                logging.output("Reading configuration", "debug");
                logging.log(i);
                if (network.ToUpper().Equals("SIRIUS"))
                {
                    self = new SiriusTuner(username, password, logging);
                }
                else
                {
                    self = new XMTuner(username, password, logging);
                }
                logging.output("XMTuner created, Attempting Login", "debug");
                logging.log(i);

                if (self.isLoggedIn == false)
                {
                    //Not logged in successfully.. Bail!
                    logging.output("Failed Login", "message");
                    logging.log(i);
                    return;
                }

                i = 0;
                loggedIn = true;
                xmServer = new WebListner(self, port);
                serverRunning = true;
                xmServer.start();
                if (xmServer.isRunning == false)
                {
                    serverRunning = false;
                    logging.output("Server failed to start.", "message");
                    logging.log(i);
                    return;
                }

                logging.output("Services are Up!", "message");
                logging.log(i);

                theTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                theTimer.AutoReset = true;
                theTimer.Enabled = true;

            }
            else
            {
                logging.output("No Configuration", "error");
                
                logging.log(i);

            }
        }

        public String test()
        {
            return "Test reached";
        }

        private bool refreshConfig()
        {
            configMan configuration = new configMan();
            if (configuration.loaded == false)
            {
                return false;
            }

            //Get configuration from configMan
            NameValueCollection config = configuration.getConfig(true);

            //Set config values using new config
            setConfig(configuration, config);
            return true;
        }

        private void setConfig(configMan cfg, NameValueCollection config)
        {
            username = cfg.getConfigItem(config, "username");
            password = cfg.getConfigItem(config, "password");
            port = cfg.getConfigItem(config, "port");
            network = cfg.getConfigItem(config, "network");
        }
        
        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (loggedIn)
            {
                self.doWhatsOn();
            }

            logging.log(i);
        }

        private String getLocalIP()
        {
            String localIP = null;
            IPAddress[] IP;
            try
            {
                IP = Dns.GetHostAddresses("");
                foreach (IPAddress ip in IP)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
            }
            catch (System.Net.Sockets.SocketException e)
            {
                logging.output(e.Message, "error");
            }
            if (localIP == null)
            {
                localIP = "localhost";
            }
            return localIP;
        }
    }
}
