/*
 * XMTuner: Copyright (C) 2009-2012 Chris Crews and Curtis M. Kularski.
 * 
 * This file is part of XMTuner.

 * XMTuner is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * XMTuner is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with XMTuner.  If not, see <http://www.gnu.org/licenses/>.
 */

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
        
        XMTuner self;
        Log logging;
        EventLog EventLog;
        WebListner xmServer;
        String username;
        String password;
        String network;
        String port;
        Boolean isConfigurationLoaded = false;
        Timer theTimer = new System.Timers.Timer(30000);
        public int err = 0;
        public Boolean started = false;
        DateTime serverStarted;
        String runTime;

        public XMTunerHost(EventLog log)
        {
            this.EventLog = log;
            logging = new Log();

            //Load config...
 	  	  	refreshConfig();

            //Complaint to Event Log for Missing Configuration
            if (isConfigurationLoaded == false)
            {
                EventLog.WriteEntry("Missing Configuration", System.Diagnostics.EventLogEntryType.Error);
                logging.output("No Configuration", "error");
                return;
            }
            start(); //GO!
        }

        private void start()
        {
            EventLog.WriteEntry("XMTuner Service initializing", System.Diagnostics.EventLogEntryType.Information);
            logging.output("Please wait... logging in", "info");

            if (network.ToUpper().Equals("SIRIUS"))
            {
                self = new SiriusTuner(username, password, logging);
            }
            else
            {
                self = new XMTuner(username, password, logging);
            }

            if (self.isLoggedIn == false)
            {
                //Not logged in successfully.. Bail!
                EventLog.WriteEntry("Failed to login. Check the serivce log for details (Err 1)", EventLogEntryType.Error);
                err = 1;
                return;
            }

            xmServer = new WebListner(self, port);
            xmServer.start();
            if (xmServer.isRunning == false)
            {
                EventLog.WriteEntry("Server failed to start. (Err 2)", System.Diagnostics.EventLogEntryType.Error);
                logging.output("Server failed to start.", "error");
                err = 2;
                return;
            }
            serverStarted = DateTime.Now;

            theTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
            theTimer.AutoReset = true;
            theTimer.Enabled = true;

            EventLog.WriteEntry("XMTuner Service started", System.Diagnostics.EventLogEntryType.Information);
            logging.output("XMTuner Service started", "info");
            started = true;
        }

        public void stop()
        {
            if (xmServer != null)
            {
                xmServer.stop();
            }
            theTimer.Stop();
            started = false;
            runTime = (DateTime.Now - serverStarted).ToString().Split('.')[0];
            logging.output("Server Uptime was " + runTime, "info"); 
            self = null;
            xmServer = null;
            GC.Collect();
        }

        private bool refreshConfig()
        {
            configMan configuration = new configMan();
            if (configuration.loaded == false)
            {
                isConfigurationLoaded = false;
                return false;
            }

            //Get configuration from configMan
            NameValueCollection config = configuration.getConfig(true);

            //Set config values using new config
            setConfig(configuration, config);
            isConfigurationLoaded = true;
            return true;
        }

        private void setConfig(configMan cfg, NameValueCollection config)
        {
            username = config["username"];
            password = config["password"];
            port = config["port"];
            network = config["network"];
        }
        
        public void OnTimedEvent(object source, ElapsedEventArgs e)
        {
            self.doWhatsOn();
        }
    }
}
