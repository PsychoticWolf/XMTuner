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
        System.Timers.Timer theTimer = new System.Timers.Timer(30000);

        public XMReader()
        {
            
        }

        public void run()
        {

            if (refreshConfig())
            {
                if (highbit) { bitrate = "high"; } else { bitrate = "low"; }
                String hostport = "";
                if (hostname != "") { hostport = hostname + ":" + port; }
                self = new XMTuner(user, password, bitrate, isMMS, tversityHost, hostport);
                if (self.isLoggedIn == false)
                {
                    //Not logged in successfully.. Bail!
                    return;
                }
                //self.OutputData = outputbox.Text + self.OutputData;
                i = 0;
                loggedIn = true;
                xmServer = new WebListner(self, port);
                serverRunning = true;
                xmServer.start();
                if (xmServer.isRunning == false)
                {
                    serverRunning = false;
                    //Server failed to start.
                    return;
                }
                log();

                theTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
                theTimer.AutoReset = true;
                theTimer.Enabled = true;

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
            String directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
            String file = "XMReader.log";
            String path;
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            path = directory + "\\" + file;
            
            
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

        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            if (loggedIn)
            {
                self.doWhatsOn();
            }

            log();
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
