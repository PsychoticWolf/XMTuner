using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace XMTuner
{
    public class XMTunerService : ServiceBase
    {
        private System.ComponentModel.IContainer components;
        
        XMReader reader;
        bool serviceStarted = false;
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

            this.CanHandlePowerEvent = true;
            this.CanHandleSessionChangeEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            this.CanStop = true;

            //XMReader reader = new XMReader();
            //reader = new XMReader();
            //reader.run();
        }

        /// <summary>

        /// The Main Thread: This is where your Service is Run.

        /// </summary>

        static void Main()
        {
            ServiceBase.Run(new XMTunerService());
            //XMReader.run();
            
        }

        /// <summary>

        /// Dispose of objects that need it here.

        /// </summary>

        /// <param name="disposing">Whether

        ///    or not disposing is going on.</param>

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        /// <summary>

        /// OnStart(): Put startup code here

        ///  - Start threads, get inital data, etc.

        /// </summary>

        /// <param name="args"></param>

        protected override void OnStart(string[] args)
        {
            
            //base.OnStart(args);
            //System.Threading.ThreadStart job = new System.Threading.ThreadStart(runReader);
            //System.Threading.Thread thread = new System.Threading.Thread(job);
            //thread.Start();
            //runReader();
            ThreadStart st = new ThreadStart(runReader);
            workerThread = new Thread(st);

            // set flag to indicate worker thread is active
            serviceStarted = true;

            // start the thread
            workerThread.Start();

            
            
        }

        /// <summary>

        /// OnStop(): Put your stop code here

        /// - Stop threads, set final data, etc.

        /// </summary>

        public void runReader()
        {
            EventLog.WriteEntry("Opening XMTuner",
         System.Diagnostics.EventLogEntryType.Information);

            reader = new XMReader();
            reader.run();


        }

        protected override void OnStop()
        {
            base.OnStop();
            reader.log();
        }

        /// <summary>

        /// OnPause: Put your pause code here

        /// - Pause working threads, etc.

        /// </summary>

        protected override void OnPause()
        {
            base.OnPause();
        }

        /// <summary>

        /// OnContinue(): Put your continue code here

        /// - Un-pause working threads, etc.

        /// </summary>

        protected override void OnContinue()
        {
            base.OnContinue();
        }

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

        private void InitializeComponent()
        {
            // 
            // XMTunerService
            // 
            this.ServiceName = "XMTuner";

        }

        private void notifyIcon1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

    }
}
