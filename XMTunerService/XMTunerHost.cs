using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Net;
using System.Timers;

namespace XMTuner
{
    public class XMTunerHost
    {
        Core c;
        XMTuner self;
        Log logging;
        Config cfg = new Config(true);


        EventLog EventLog;
        public int err = 0;
        public Boolean started = false;

         public XMTunerHost(EventLog log)
        {
            this.EventLog = log;
            logging = new Log();

            c = new Core(logging);

            //Complaint to Event Log for Missing Configuration
            if (c.cfg.loaded == false)
            {
                EventLog.WriteEntry("Missing Configuration", System.Diagnostics.EventLogEntryType.Error);
                logging.output("No Configuration", LogLevel.Error);
                return;
            }
            start(); //GO!
        }

        private void start()
        {
            EventLog.WriteEntry("XMTuner Service initializing", System.Diagnostics.EventLogEntryType.Information);
            logging.output("Please wait... logging in", LogLevel.Info);
            c.tick += new Core.TickHandler(coreEvent_Do);
            c.Start();
            EventLog.WriteEntry("XMTuner Service started", System.Diagnostics.EventLogEntryType.Information);
            logging.output("XMTuner Service started", LogLevel.Info);
        }

        public void stop()
        {
            c.Stop();
        }

        private void coreEvent_Do(Core c, XMTunerEventArgs e)
        {
            self = c.tuner;
            switch (e.source)
            {
                case "xmtuner":
                    switch (e.data)
                    {
                        /*case "isLoggedIn":
                            break;
                        case "isLoggedOut":
                            break;
                         */
                        case "isError":
                            EventLog.WriteEntry("Failed to login. Check the serivce log for details (Err 1)", EventLogEntryType.Error);
                            err = 1;
                            //return; //XXX!
                            break;
                    }
                    break;
                case "server":
                    switch (e.data)
                    {
                        case "isRunning":
                            started = true;
                            break;
                        case "isStopped":
                            started = false;
                            GC.Collect();
                            break;
                        case "isError":
                            EventLog.WriteEntry("Server failed to start. (Err 2)", System.Diagnostics.EventLogEntryType.Error);
                            logging.output("Server failed to start.", LogLevel.Error);
                            err = 2;
                            //return; //XXX!
                            break;
                    }
                    break;
            }
        }
    }
}
