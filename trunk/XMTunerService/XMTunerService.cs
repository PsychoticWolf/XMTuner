using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;
using System.IO;

namespace XMTuner
{
    public class XMTunerService : ServiceBase
    {
        Core c;
        Thread workerThread;

        /// <summary>

        /// Public Constructor for WindowsService.
       
        /// - Put all of your Initialization code here.

        /// </summary>

        public XMTunerService()
        {
            InitializeComponent();

            this.ServiceName = "XMTunerService";
            this.EventLog.Log = "Application";
           
            // These Flags set whether or not to handle that specific
            //  type of event. Set to true if you need it, false otherwise.

            this.CanHandlePowerEvent = false;
            this.CanHandleSessionChangeEvent = false;
            this.CanPauseAndContinue = false;
            this.CanShutdown = false;
            this.CanStop = true;
        }

#region Service Events
        /// <summary>

        /// The Main Thread: This is where your Service is Run.

        /// </summary>

        static void Main()
        {
            ServiceBase.Run(new XMTunerService());
        }

        /// <summary>

        /// OnStart(): Put startup code here

        ///  - Start threads, get inital data, etc.

        /// </summary>

        /// <param name="args"></param>

        protected override void OnStart(string[] args)
        {
            
            //base.OnStart(args);
            ThreadStart st = new ThreadStart(runXMTuner);
            workerThread = new Thread(st);

            // set flag to indicate worker thread is active
            //serviceStarted = true;

            // start the thread
            workerThread.Start();

            writePID();
            
        }

        /// <summary>

        /// OnStop(): Put your stop code here

        /// - Stop threads, set final data, etc.

        /// </summary>

        protected override void OnStop()
        {
            base.OnStop();
            if (c != null)
            {
                c.Stop();
            }
            removePID();
        }
        /*
         /// <summary>

        /// OnShutdown(): Called when the System is shutting down

        /// - Put code here when you need special handling

        ///   of code that deals with a system shutdown, such

        ///   as saving special data before shutdown.

        /// </summary>

        protected override void OnShutdown()
        {
            base.OnShutdown();
        }

        /// <summary>

        /// OnCustomCommand(): If you need to send a command to your

        ///   service without the need for Remoting or Sockets, use

        ///   this method to do custom methods.

        /// </summary>

        /// <param name="command">Arbitrary Integer between 128 & 256</param>

        protected override void OnCustomCommand(int command)
        {
            //  A custom command can be sent to a service by using this method:

            //#  int command = 128; //Some Arbitrary number between 128 & 256

            //#  ServiceController sc = new ServiceController("NameOfService");

            //#  sc.ExecuteCommand(command);


            base.OnCustomCommand(command);
        }

        /// <summary>

        /// OnPowerEvent(): Useful for detecting power status changes,

        ///   such as going into Suspend mode or Low Battery for laptops.

        /// </summary>

        /// <param name="powerStatus">The Power Broadcast Status

        /// (BatteryLow, Suspend, etc.)</param>

        protected override bool OnPowerEvent(PowerBroadcastStatus powerStatus)
        {
            return base.OnPowerEvent(powerStatus);
        }

        /// <summary>

        /// OnSessionChange(): To handle a change event

        ///   from a Terminal Server session.

        ///   Useful if you need to determine

        ///   when a user logs in remotely or logs off,

        ///   or when someone logs into the console.

        /// </summary>

        /// <param name="changeDescription">The Session Change

        /// Event that occured.</param>

        protected override void OnSessionChange(
                  SessionChangeDescription changeDescription)
        {
            base.OnSessionChange(changeDescription);
        }
        
                /// <summary>

        /// Dispose of objects that need it here.

        /// </summary>

        /// <param name="disposing">Whether or not disposing is going on.</param>

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
         
        */

        private void InitializeComponent()
        {
            // 
            // XMTunerService
            // 
            this.ServiceName = "XMTuner";

        }
#endregion

        public void runXMTuner()
        {
            EventLog.WriteEntry("Opening XMTuner", System.Diagnostics.EventLogEntryType.Information);
            c = new Core(new Log());
            c.tick += new Core.TickHandler(coreEvent_Do);
            c.Start();
        }

        private void writePID()
        {
            String lockFile = "xmtunerservice.lock";

            int pid = Process.GetCurrentProcess().Id;

            String directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
            if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            String path = directory + "\\" + lockFile;

            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(fs);
            textOut.Write(pid);
            textOut.Close();

        }

        private void removePID()
        {
            String lockFile = "xmtunerservice.lock";
            String directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
            String path = directory + "\\" + lockFile;

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        private void coreEvent_Do(Core c, XMTunerEventArgs e)
        {
            switch (e.source)
            {
                case XMTunerEventSource.Configuration:
                    switch (e.data)
                    {
                        case XMTunerEventData.isNotLoaded:
                            EventLog.WriteEntry("Missing Configuration", System.Diagnostics.EventLogEntryType.Error);
                            c.logging.output("No Configuration", LogLevel.Error);
                            break;
                    }
                    break;
                case XMTunerEventSource.Tuner:
                    switch (e.data)
                    {
                        case XMTunerEventData.isStarting:
                            EventLog.WriteEntry("XMTuner Service initializing", System.Diagnostics.EventLogEntryType.Information);
                            c.logging.output("Please wait... logging in", LogLevel.Info);
                            break;
                        case XMTunerEventData.isReady:
                            EventLog.WriteEntry("XMTuner Service started", System.Diagnostics.EventLogEntryType.Information);
                            c.logging.output("XMTuner Service started", LogLevel.Info);
                            break;
                        case XMTunerEventData.isError:
                            EventLog.WriteEntry("Failed to login. Check the serivce log for details (Err 1)", EventLogEntryType.Error);
                            this.ExitCode = 1;
                            this.Stop();
                            break;
                    }
                    break;
                case XMTunerEventSource.Server:
                    switch (e.data)
                    {
                        case XMTunerEventData.isError:
                            EventLog.WriteEntry("Server failed to start. (Err 2)", System.Diagnostics.EventLogEntryType.Error);
                            c.logging.output("Server failed to start.", LogLevel.Error);
                            this.ExitCode = 2;
                            this.Stop();
                            break;
                    }
                    break;
            }
        }

    }
}
