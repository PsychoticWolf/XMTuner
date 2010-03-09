using System;
using System.Windows.Forms;
using System.ServiceProcess;
using System.Collections.Specialized;
using System.Net;
using System.IO;

namespace XMTuner
{
    public partial class Form1 : Form
    {
        ServiceController serviceControl = new ServiceController();
        String logFile = "XMTunerService.log";

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

            Form2 form2 = new Form2(null, false, getLocalIP());
            form2.ShowDialog();
            btnSerRestart_Click(sender, e);
        }

        private String getLocalIP()
        {
            String localIP = null;
            IPAddress[] IP;
            try
            {
                IP = Dns.GetHostAddresses("");
                foreach (IPAddress ip in IP)
                {
                    if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        localIP = ip.ToString();
                        break;
                    }
                }
            }
            catch (System.Net.Sockets.SocketException)
            {
            }
            if (localIP == null)
            {
                localIP = "localhost";
            }
            return localIP;
        }
        #endregion

        #region Service Log
        private void loadServiceLog()
        {
            String directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
                if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            String path = directory + "\\" + logFile;

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
    }
}
