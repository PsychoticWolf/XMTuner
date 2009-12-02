using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Threading;

namespace XMTunerService
{
    public class XMTunerService : ServiceBase
    {
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitXMTunerToolStripMenuItem;
        public System.Windows.Forms.NotifyIcon notifyIcon1;
        
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMTunerService));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitXMTunerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.viewServerToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.restoreToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitXMTunerToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(149, 120);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Enabled = false;
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.loginToolStripMenuItem.Text = "Login";
            // 
            // viewServerToolStripMenuItem
            // 
            this.viewServerToolStripMenuItem.Enabled = false;
            this.viewServerToolStripMenuItem.Name = "viewServerToolStripMenuItem";
            this.viewServerToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.viewServerToolStripMenuItem.Text = "What\'s On...";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.aboutToolStripMenuItem.Text = "About...";
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(145, 6);
            // 
            // exitXMTunerToolStripMenuItem
            // 
            this.exitXMTunerToolStripMenuItem.Name = "exitXMTunerToolStripMenuItem";
            this.exitXMTunerToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.exitXMTunerToolStripMenuItem.Text = "Exit XMTuner";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "XMTuner";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "XMTuner";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // XMTunerService
            // 
            this.ServiceName = "XMTuner";
            this.contextMenuStrip1.ResumeLayout(false);

        }

        private void notifyIcon1_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

    }
}
