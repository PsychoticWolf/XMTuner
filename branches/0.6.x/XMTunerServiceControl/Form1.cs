using System;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Collections.Specialized;
using System.Net;
using System.IO;
using System.Drawing;
using System.Runtime.InteropServices;


namespace XMTuner
{
    public partial class Form1 : Form
    {
        ServiceController serviceControl = new ServiceController();
        String serviceName = "XMTunerService";
        String logFile = "XMTunerService.log";
        TimeSpan delay = TimeSpan.FromSeconds(10);

        #region Aero
        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth;
            public int cxRightWidth;
            public int cyTopHeight;
            public int cyBottomHeight;
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(
               IntPtr hWnd,
               ref MARGINS pMarInset
               );
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int en);

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            AeroLoad();
        }
        
        private void AeroLoad()
        {
            if (System.Environment.OSVersion.Version.Major >= 6)  //make sure you are not on a legacy OS 
            {
                int en = 0;
                DwmIsCompositionEnabled(ref en);  //check if the desktop composition is enabled
                if (en > 0)
                {                    
                    this.BackColor = Color.Gainsboro;
                    this.TransparencyKey = Color.Gainsboro;


                    MARGINS margins = new MARGINS();

                    margins.cxLeftWidth = 0;
                    margins.cxRightWidth = 0;
                    margins.cyTopHeight = 80;
                    margins.cyBottomHeight = 0;

                    IntPtr hWnd = this.Handle;
                    int result = DwmExtendFrameIntoClientArea(hWnd, ref margins);
                }
                else
                {
                    this.TransparencyKey = Color.Empty;
                    this.BackColor = SystemColors.Control;
                }
            }
        }

        #endregion

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
            serviceControl.Refresh();
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
            setProgressValue();
            if (start())
            {
                setProgressValue(10);
                service_button_reset();
            }
            

        }

        private void btnSerStop_Click(object sender, EventArgs e)
        {
            setProgressValue();
            if (stop())
            {
                setProgressValue(10);
                service_button_reset();
            }

        }

        private void btnSerRestart_Click(object sender, EventArgs e)
        {
            setProgressValue();
            if (stop())
            {
                setProgressValue(5);
                setProgressValue(6);
                if (start())
                {
                    setProgressValue(10);
                    service_button_reset();
                }
            }
        }

        private void btnSerInstall_Click(object sender, EventArgs e)
        {
            setProgressValue();
            servicemanager sm = new servicemanager(serviceName, "Provides Sirius|XM Satelite Radio Streams to DLNA Compatible Media Players", "XMTuner");
            sm.addDependency(new String[1] { "tcpip" });
            bool success = sm.Install(ServiceStartMode.Automatic);
            setProgressValue(10);
            service_button_reset();
        }

        private void btnSerUninstall_Click(object sender, EventArgs e)
        {
            setProgressValue(5);
            servicemanager sm = new servicemanager(serviceName);
            bool success = sm.Uninstall();
            service_button_reset();
            setProgressValue(10);
        }

        private Boolean stop()
        {
            try
            {
                serviceControl.Stop();
                serviceControl.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped, delay);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Failed to Stop Service", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resetProgressValue();
                return false;
            }
            catch (System.ServiceProcess.TimeoutException)
            {
                MessageBox.Show("Timed out waiting for service to stop.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resetProgressValue();
                return false;
            }
            return true;
        }

        private Boolean start()
        {
            try
            {
                serviceControl.Start();
                serviceControl.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running, delay);
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Failed to Start Service", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resetProgressValue();
                return false;
            }
            catch (System.ServiceProcess.TimeoutException)
            {
                MessageBox.Show("Timed out waiting for service to start.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                resetProgressValue();
                return false;
            }
            return true;
        }

        private void setProgressValue()
        {
            setProgressValue(1);
        }
        private void setProgressValue(int value)
        {
            progressBar1.Value = value;
            if (progressBar1.Maximum == value)
            {
                timer2.Start();
            }
        }
        private void resetProgressValue()
        {
            progressBar1.Value = 0;
        }
        #endregion

        #region Configuration
        private void bConfig_Click(object sender, EventArgs e)
        {
            //Store current configuration for comparison test
            NameValueCollection currentconfig = new configMan().getConfig();

            Form2 form2 = new Form2(null, false, configMan.getLocalIP());
            form2.ShowDialog();
            if (DialogResult.Yes ==
                MessageBox.Show("The service must be restarted for your changes to take effect.\n Restart service now?", "Updated configuration saved", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
            {
                btnSerRestart_Click(sender, e);
            }
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
            catch (IOException) {}
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            service_button_reset();
            loadServiceLog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            resetProgressValue();
        }
    }
}
