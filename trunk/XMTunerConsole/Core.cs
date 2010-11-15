using System;
using System.Collections.Generic;
using System.Timers;

namespace XMTuner
{
    public enum XMTunerEventSource {Configuration, Tuner, Server};
    public enum XMTunerEventData {isNotLoaded, isLoaded, isStarting, 
            isError, isLoggedIn, isLoggedOut, isRunning, isReady, isStopped};

    class XMTunerEventArgs : EventArgs
    {
        public XMTunerEventSource source;
        public XMTunerEventData data;
        public String details;

        public XMTunerEventArgs(XMTunerEventSource source, XMTunerEventData data) : this(source, data, null) { }
        public XMTunerEventArgs(XMTunerEventSource source, XMTunerEventData data, String details)
        {
            this.source = source;
            this.data = data;
            this.details = details;
        }
    }

    class Core
    {
        public XMTuner tuner;
        public WebListner server;
        public Config cfg = new Config(true);
        public Log logging;

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
            if (cfg.loaded == false)
            {
                tick(this, new XMTunerEventArgs(XMTunerEventSource.Configuration, XMTunerEventData.isNotLoaded));
                return;
            }
            tick(this, new XMTunerEventArgs(XMTunerEventSource.Configuration, XMTunerEventData.isLoaded));
        }

        public void Start()
        {
            if (cfg.loaded == false) { return; }

            tick(this, new XMTunerEventArgs(XMTunerEventSource.Tuner, XMTunerEventData.isStarting));

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
                tick(this, new XMTunerEventArgs(XMTunerEventSource.Tuner, XMTunerEventData.isError));
                return;
            }
            tick(this, new XMTunerEventArgs(XMTunerEventSource.Tuner, XMTunerEventData.isLoggedIn));

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
                    e = new XMTunerEventArgs(XMTunerEventSource.Server, XMTunerEventData.isRunning);
                    tick(this, e);
                }
                else
                {
                    //Server failed to start.
                    e = new XMTunerEventArgs(XMTunerEventSource.Server, XMTunerEventData.isError);
                    tick(this, e);
                    return;
                }
            }

            tick(this, new XMTunerEventArgs(XMTunerEventSource.Tuner, XMTunerEventData.isReady));
        }

        public void Stop()
        {
            if (server != null)
            {
                server.stop();
                e = new XMTunerEventArgs(XMTunerEventSource.Server, XMTunerEventData.isStopped);
                tick(this, e);
                server = null;
            }

            e = new XMTunerEventArgs(XMTunerEventSource.Tuner, XMTunerEventData.isLoggedOut);
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
