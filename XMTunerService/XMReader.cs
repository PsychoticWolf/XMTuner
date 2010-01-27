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
        bool loggedIn = false;
        String user;
        String password;
        String port = "";
        Boolean useLocalDatapath = false;
        Boolean serverRunning = false; 
        bool highbit = true;
        String hostname = "";
        String tversityHost = "";
        String ip = "";
        public String output;
        bool autologin = false;
        bool isMMS = false;
        public int i = 0;
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
                self = new XMTuner(user, password, logging, useLocalDatapath);
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
                addOutput("No Configuration");
                
                logging.log(i);

            }
        }
        public void addOutput(String outtxt)
        {
            output = output + "\n" + outtxt;
            logging.output(outtxt, "message");
        }

        public String test()
        {
            return "Test reached";
        }
        private bool refreshConfig()
        {
            configMan configuration = new configMan();
            configuration.readConfig();
            if (configuration.isConfig)
            {
                NameValueCollection configIn = configuration.getConfig();
                user = configIn.Get("username");
                password = configIn.Get("password");
                port = configIn.Get("port");
                highbit = Convert.ToBoolean(configIn.Get("bitrate"));
                autologin = Convert.ToBoolean(configIn.Get("autologin"));
                isMMS = Convert.ToBoolean(configIn.Get("isMMS"));
                tversityHost = configIn.Get("Tversity"); ;
                hostname = configIn.Get("hostname"); ;
                if (hostname.Equals("")) { hostname = ip; }
                return true;
            }
            else
            {
                if (port.Equals(""))
                {
                    port = "19081";
                }

                //outputbox.AppendText("No Configuration\n");
                return false;
            }
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
                addOutput(e.Message);
            }
            if (localIP == null)
            {
                localIP = "localhost";
            }
            return localIP;
        }
    }
}
