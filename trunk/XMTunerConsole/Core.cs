using System;
using System.Collections.Generic;

namespace XMTuner
{
    class XMTunerEventArgs : EventArgs
    {
        public String source;
        public String data;

        public XMTunerEventArgs(String source, String data)
        {
            this.source = source;
            this.data = data;
        }
    }

    class Core
    {
        public XMTuner tuner;
        public WebListner server;
        private configMan config;
        private Log logging;

        private String username;
        private String password;
        private String network;
        private String port;

        //Event
        public event TickHandler tick;
        public XMTunerEventArgs e = null;
        public delegate void TickHandler(Core c, XMTunerEventArgs e);


        public Core(Log log)
        {
            loadConfig();
            this.logging = log;
        }

        private void loadConfig()
        {
            config = new configMan();
            username = config.getConfigItem("username");
            password = config.getConfigItem("password");
            network = config.getConfigItem("network");
            port = config.getConfigItem("port");

        }

        public void Start()
        {
            if (network.ToUpper().Equals("SIRIUS"))
            {
                tuner = new SiriusTuner(username, password, logging);
            }
            else
            {
                tuner = new XMTuner(username, password, logging);
            }
            if (tuner.isLoggedIn == false)
            {
                //Not logged in successfully.. Bail!
                tick(this, new XMTunerEventArgs("xmtuner", "isError"));
                return;
            }
            tick(this, new XMTunerEventArgs("xmtuner", "isLoggedIn"));

            //XXX Starting the server is optional...
            if (true)
            {
                server = new WebListner(tuner, port);

                server.start();
                if (server.isRunning == true)
                {
                    e = new XMTunerEventArgs("server", "isRunning");
                    tick(this, e);
                }
                else
                {
                    //Server failed to start.
                    e = new XMTunerEventArgs("server", "isError");
                    tick(this, e);
                    return;
                }
            }
        }

        public void Stop()
        {
            server.stop();
            e = new XMTunerEventArgs("server", "isStopped");
            tick(this, e);
            server = null;

            e = new XMTunerEventArgs("xmtuner", "isLoggedOut");
            tick(this, e);
            tuner.logout();
            tuner = null;

            GC.Collect();
        }
    }
}
