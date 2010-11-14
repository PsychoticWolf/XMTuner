using System;
using System.Collections.Generic;
using System.Timers;

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
        public Config cfg;
        private Log logging;

        //Event
        public event TickHandler tick;
        public XMTunerEventArgs e = null;
        public delegate void TickHandler(Core c, XMTunerEventArgs e);

        //Flags
        public Boolean isLoggedIn
        {
            get
            {
                if (tuner == null)
                {
                    return false;
                }
                return tuner.isLoggedIn;
            }
        }
        public Boolean isServerRunning
        {
            get
            {
                if (server == null)
                {
                    return false;
                }
                return server.isRunning;
            }
        }

        //Timer (for polling...)
        Timer pollTimer = new System.Timers.Timer(30000);


        public Core(Log log)
        {
            cfg = new Config();
            this.logging = log;
        }

        public void Start()
        {
            if (cfg.network.ToUpper().Equals("SIRIUS"))
            {
                tuner = new SiriusTuner(cfg, logging);
            }
            else
            {
                tuner = new XMTuner(cfg, logging);
            }
            if (tuner.isLoggedIn == false)
            {
                //Not logged in successfully.. Bail!
                tick(this, new XMTunerEventArgs("xmtuner", "isError"));
                return;
            }
            tick(this, new XMTunerEventArgs("xmtuner", "isLoggedIn"));

            //Start the polling timer.
            pollTimer.Elapsed += new ElapsedEventHandler(OnPollTimerEvent);
            pollTimer.AutoReset = true;
            pollTimer.Enabled = true;

            //Starting the server is optional...
            if (cfg.enableServer == true)
            {
                server = new WebListner(tuner, cfg.port);

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
            if (server != null)
            {
                server.stop();
                e = new XMTunerEventArgs("server", "isStopped");
                tick(this, e);
                server = null;
            }

            e = new XMTunerEventArgs("xmtuner", "isLoggedOut");
            tick(this, e);
            tuner.logout();
            pollTimer.Stop();
            tuner = null;

            GC.Collect();
        }

        public void OnPollTimerEvent(object source, ElapsedEventArgs e)
        {
            tuner.doWhatsOn();
        }
    }
}
