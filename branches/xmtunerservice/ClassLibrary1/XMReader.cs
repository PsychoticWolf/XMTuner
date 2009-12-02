using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Net;

namespace XMTunerService
{
    public class XMReader
    {
        
        XMTuner self;
        WebListner xmServer;
        bool loggedIn = false;
        bool serverRunning = false;
        String user;
        String password;
        String port = "";
        String bitrate = "high";
        String hostname = "";
        String tversityHost = "";
        String ip = "";
        public String output;
        bool highbit = true;
        bool autologin = false;
        bool isMMS = false;
        int i = 0;
        double sec = 0;
        double minute = 0;
        double hour = 0;
        String runTime = "";
        

        public XMReader()
        {
            
        }

        public void run()
        {

            if (refreshConfig())
            {
                self = new XMTuner(user, password, bitrate, isMMS);
                xmServer = new WebListner(self, port);
                //self.OutputData = output + self.OutputData;
                addOutput(self.OutputData);
                xmServer.start();
                loggedIn = true;
                //output = self.OutputData;
                
                log();

            }
            else
            {
                addOutput("No Configuration");
                
                log();

            }
        }
        public void addOutput(String outtxt)
        {
            output = output + "\n" + outtxt;
        }

        public void log()
        {
            string path = @"XMReader.log";
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(fs);
            textOut.Write(output);
            textOut.Close();
            return;
        }

        public String test()
        {
            return "Test reached";
        }
        private bool refreshConfig()
        {
            configMan configuration = new configMan();
            configuration.readConfig();
            //outputbox.SelectionColor = Color.Blue;
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
