using System;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Drawing;
using CodeKing.Native;


namespace XMTuner
{
    public partial class Form1 : Form
    {
        ServiceController serviceControl = new ServiceController();
        String logFile = "XMTunerService.log";


                // defines how far we are extending the Glass margins
        private Win32.MARGINS margins;

        /// <summary>
        /// Override the onload, and define our Glass margins
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (!Win32.DwmIsCompositionEnabled())
            {
                //MessageBox.Show("This demo requires Vista, with Aero enabled.");
                //Application.Exit();
            }
            SetGlassRegion();
        }
        /// <summary>
        /// Use the form padding values to define a Glass margin
        /// </summary>
        private void SetGlassRegion()
        {
            // Set up the glass effect using padding as the defining glass region
            if (Win32.DwmIsCompositionEnabled())
            {
                margins = new Win32.MARGINS();
                margins.Top = Padding.Top;
                margins.Left = Padding.Left;
                margins.Bottom = Padding.Bottom;
                margins.Right = Padding.Right;
                Win32.DwmExtendFrameIntoClientArea(this.Handle, ref margins);
            }
        }
        /// <summary>
        /// Override the OnPaintBackground method, to draw the desired
        /// Glass regions black and display as Glass
        /// </summary>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (Win32.DwmIsCompositionEnabled())
            {
                e.Graphics.Clear(Color.Black);
                // put back the original form background for non-glass area
                Rectangle clientArea = new Rectangle(
                margins.Left,
                margins.Top,
                this.ClientRectangle.Width - margins.Left - margins.Right,
                this.ClientRectangle.Height - margins.Top - margins.Bottom);
                Brush b = new SolidBrush(this.BackColor);
                e.Graphics.FillRectangle(b, clientArea);
            }
        }

        /// <summary>
        /// On scroll change the padding value, which is mapped to the Glass region.
        /// </summary>
        /* private void trackBar1_Scroll(object sender, EventArgs e)
        {
            this.Padding = new Padding(this.trackBar1.Value);
            SetGlassRegion();
            Invalidate();
        } */

        #region Core
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serviceControl.ServiceName = "XMTunerService";

            service_button_reset();
        }
        #endregion

        #region Service Tab
        private void service_button_reset()
        {
            try
            {
                lblServiceStat.Text = serviceControl.Status.ToString();
                lblServiceInst.Text = "installed";
                if (serviceControl.Status.ToString().ToLower().Equals("stopped"))
                {
                    btnSerInstall.Enabled = false;
                    btnSerUninstall.Enabled = true;
                    btnSerStart.Enabled = true;
                    btnSerStop.Enabled = false;
                    btnSerRestart.Enabled = false;
                }
                else if (serviceControl.Status.ToString().ToLower().Equals("running"))
                {
                    btnSerInstall.Enabled = false;
                    btnSerUninstall.Enabled = true;
                    btnSerStart.Enabled = false;
                    btnSerStop.Enabled = true;
                    btnSerRestart.Enabled = true;
                }
                else
                {
                    btnSerInstall.Enabled = false;
                    btnSerUninstall.Enabled = true;
                    btnSerStart.Enabled = true;
                    btnSerStop.Enabled = false;
                    btnSerRestart.Enabled = false;
                }

            }
            catch
            {
                lblServiceStat.Text = "NOT INSTALLED";
                lblServiceInst.Text = "not installed";
                btnSerStop.Enabled = false;
                btnSerStart.Enabled = false;
                btnSerRestart.Enabled = false;
                btnSerUninstall.Enabled = false;
                btnSerInstall.Enabled = true;
            }

        }

        private void btnSerStart_Click(object sender, EventArgs e)
        {
            serviceControl.Start();
            serviceControl.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running);
            service_button_reset();

        }

        private void btnSerStop_Click(object sender, EventArgs e)
        {
            serviceControl.Stop();
            serviceControl.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped);
            service_button_reset();

        }

        private void btnSerRestart_Click(object sender, EventArgs e)
        {
            serviceControl.Stop();
            serviceControl.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped);
            serviceControl.Start();
            serviceControl.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running);
            service_button_reset();
        }

        private void btnSerInstall_Click(object sender, EventArgs e)
        {
            servicemanager sm = new servicemanager("XMTunerService", "Provides XMRO to Devices", "XM Tuner");
            bool sucess = sm.Install(ServiceStartMode.Automatic);
            service_button_reset();

        }

        private void btnSerUninstall_Click(object sender, EventArgs e)
        {
            servicemanager sm = new servicemanager("XMTunerService", "Provides XMRO to Devices", "XM Tuner");
            bool sucess = sm.Uninstall();
            service_button_reset();
            MessageBox.Show("If you wish to reinstall the service, please restart XMTuner");
            btnSerUninstall.Enabled = false;
            btnSerStart.Enabled = false;
        }
        #endregion

        #region Configuration
        private void bConfig_Click(object sender, EventArgs e)
        {
            //Store current configuration for comparison test
            NameValueCollection currentconfig = new configMan().getConfig();

            Form2 form2 = new Form2(null, false, configMan.getLocalIP());
            form2.ShowDialog();
            btnSerRestart_Click(sender, e);
        }
        #endregion

        #region Service Log
        private void loadServiceLog()
        {
            String directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
                if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            String path = directory + "\\" + logFile;
            fileSystemWatcher1.Path = directory;
            fileSystemWatcher1.Filter = logFile;
            try
            {
                FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                StreamReader textIn = new StreamReader(fs);
                String data = textIn.ReadToEnd();
                textIn.Close();
                rtbServiceLog.Text = data;
            }
            catch (FileNotFoundException)
            {
                rtbServiceLog.Text = "No XMTuner Service Log Found";
            }
            catch (IOException)
            {
                loadServiceLog();
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            loadServiceLog();
        }
        #endregion

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Equals(tLog))
            {
                loadServiceLog();

                rtbServiceLog.Focus();
                rtbServiceLog.SelectionStart = rtbServiceLog.Text.Length;
                rtbServiceLog.ScrollToCaret();
            }
        }

        private void fileSystemWatcher1_Changed(object sender, FileSystemEventArgs e)
        {
            loadServiceLog();
        }
    }
}
