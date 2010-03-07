using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections.Specialized;
using System.ServiceProcess;
using System.Reflection;

namespace XMTuner
{
    public partial class Form1 : Form
    {
        XMTuner self;
        Log logging;
        WebListner xmServer;
        Boolean isConfigurationLoaded = false;
        bool loggedIn = false;
        bool serverRunning = false;
        String network;
        String username = "";
        String password = "";
        String port = "";
        String bitrate;
        bool autologin = false;
        bool isMMS = false;
        String tversityHost;
        String hostname;
        ServiceController serviceControl = new ServiceController();
        bool serviceRunning = false;

        int i = 0;
        double sec = 0;
        double minute = 0;
        double hour = 0;
        String runTime = "";
        String ip = "";

        Boolean cbIconsLoaded;

        #region Form1 Core
        public Form1()
        {
            InitializeComponent();
            initPlayer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logging = new Log(ref outputbox);
            aVersion.Text = configMan.version;
            outputbox.AppendText("XMTuner "+configMan.version+"\n");
            serviceControl.ServiceName = "XMTunerService";

            service_button_reset();
            if (autologin && serviceRunning)
            {
                output("Autologin skipped - Service already running\n", "error");
            }

            if (refreshConfig() && autologin && !serviceRunning)
            {
                bStart_Click(sender, e);
            }
            lblClock.Text = "0:00:00";
#if !DEBUG
            Updater update = new Updater(outputbox);
#endif

        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (logging == null) { return; }
            logging.log(i);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }
        #endregion

        #region Start/Stop
        //Start / Login
        private void bStart_Click(object sender, EventArgs e)
        {
            start();
        }
        private void start()
        {
            output("Please wait... logging in", "info");
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
                return;
            }
            i = 0;

            xmServer = new WebListner(self, port);
            serverRunning = true;
            xmServer.start();
            if (xmServer.isRunning == false)
            {
                serverRunning = false;
                //Server failed to start.
                return;
            }
            viewServerToolStripMenuItem.Enabled = true;
            loginToolStripMenuItem.Enabled = false;
            timer2.Enabled = true;
            linkServer.Text = "Server is Running...";
            linkServer.Enabled = true;

            timerTest.Enabled = true;

            loggedIn = true;
            if (loggedIn) {
                bStart.Enabled = false;
                bStop.Enabled = true;
                channelBox.Enabled = true;
                if (!tversityHost.Equals("")) { protocolBox.Items.Add("MP3"); }
            }
            syncStatusLabel();
            loadChannels();
            output("XMTuner Ready...", "info");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serverRunning)
            {
                i++;
                sec = i;

                if (sec >= 60) { minute = sec / 60; minute = Math.Floor(minute); sec = sec - (minute * 60); }
                if (minute >= 60) { hour = minute / 60; hour = Math.Floor(hour); minute = minute - (hour * 60); }


                runTime = hour.ToString() + ":";
                if (minute < 10) { runTime += "0"+minute.ToString() + ":"; }
                else {runTime+=minute.ToString()+":";}

                if (sec < 10) { runTime += "0" + sec.ToString(); }
                else { runTime += sec.ToString(); }

                lblClock.Text = runTime;
            }
        }


        private void timer2_Tick(object sender, EventArgs e)
        {
            if (loggedIn)
            {
                self.doWhatsOn();
            }
            logging.log(i);
        }



        //Stop
        private void bStop_Click(object sender, EventArgs e)
        {
            stop();
        }
        private void stop()
        {
            shutdownPlayer();
            xmServer.stop();
            serverRunning = false;
            linkServer.Text = "Server is Stopped...";
            linkServer.Enabled = false;
            lblClock.Text = "0:00:00";
            i = 0;
            timer2.Enabled = false;
            timerTest.Enabled = false;
            bStart.Enabled = true;
            bStop.Enabled = false;
            unloadChannels();
            self = null;
            xmServer = null;
            loggedIn = false;
            recentlyPlayedBox.Clear();
            syncStatusLabel();
            GC.Collect();
        }

        private void linkServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://" + getLocalIP() + ":" + port);
        }

        private void restart()
        {
            stop();
            start();
        }
        #endregion

        #region Configuration
        private void bConfigure_Click(object sender, EventArgs e)
        {
            CacheManager cache = null;
            if (self != null) { cache = self.cache; }

            //Store current configuration for comparison test
            NameValueCollection currentconfig = new configMan().getConfig();

            Form2 form2 = new Form2(cache, loggedIn, ip);
            form2.ShowDialog();
            refreshConfig(currentconfig);
        }

        private List<String> compareConfig(NameValueCollection prevconfig, NameValueCollection config)
        {
            List<String> changedValues = new List<String>();
            foreach (String value in config.AllKeys)
            {
                if (config[value] != prevconfig[value])
                {
                    changedValues.Add(value.ToLower());
                }
            }
            if (changedValues.Count > 0)
            {
                String updatedvalues = "";
                for (int i = 0; i < changedValues.Count; i++)
                    updatedvalues += changedValues[i] + " ";
                    output("Updated Values: " + updatedvalues, "debug");

                return changedValues;
            }
            return null;
        }

        private bool refreshConfig() { return refreshConfig(null); }
        private bool refreshConfig(NameValueCollection prevconfig)
        {
            configMan configuration = new configMan();
            if (configuration.loaded == false)
            {
                bStart.Enabled = false;
                output("No Configuration\nClick Configure.", "error");
                isConfigurationLoaded = false;
                return false;
            }

            //Get configuration from configMan
            NameValueCollection config = configuration.getConfig(true);

            //Set config values using new config
            setConfig(configuration, config);

            //Get list of updated values if we're updating config
            if (loggedIn == true && prevconfig != null)
            {
                List<String> updatedvalues = compareConfig(prevconfig, config);
                if (updatedvalues != null)
                {
                    //Update downstream users dynamically or restart as needed
                    updateRunningConfig(config, updatedvalues);
                }
            }

            //Messages & Item Twiddling
            if (!serverRunning)
            {
                bStart.Enabled = true;
            }
            loginToolStripMenuItem.Enabled = true;

            if (isConfigurationLoaded == false)
            {
                output("Configuration Loaded", "info");
            }
            isConfigurationLoaded = true;
            syncStatusLabel();
            return true;
        }

        private void updateRunningConfig(NameValueCollection config, List<String> updatedvalues)
        {
            if (xmServer == null || self == null || loggedIn == false) { return; }
            //username, password, port [, network] = require restart
            if (updatedvalues.Contains("username") || updatedvalues.Contains("password") ||
                updatedvalues.Contains("port") || updatedvalues.Contains("network"))
            {
                output("Your configuration update requires XMTuner to restart the server to take effect. Restarting now...", "info");
                restart();
            }

            //bitrate, isMMS, hostname, TVersity = should be dynamically applied
            if (updatedvalues.Contains("bitrate") || updatedvalues.Contains("ismms") ||
                updatedvalues.Contains("hostname") || updatedvalues.Contains("tversity"))
            {
                output("Updating configuration of web server...", "debug");
                xmServer.worker.config = config;
            }
        }

        private void setConfig(configMan cfg, NameValueCollection config)
        {
            ip = getLocalIP();
            username = cfg.getConfigItem(config, "username");
            password = cfg.getConfigItem(config, "password");
            port = cfg.getConfigItem(config, "port");
            bitrate = cfg.getConfigItem(config, "bitrate");
            autologin = Convert.ToBoolean(cfg.getConfigItem(config, "autologin"));
            isMMS = Convert.ToBoolean(cfg.getConfigItem(config, "isMMS"));
            tversityHost = cfg.getConfigItem(config, "Tversity");
            hostname = cfg.getConfigItem(config, "hostname");
            network = cfg.getConfigItem(config, "network");

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
            catch (System.Net.Sockets.SocketException e)
            {
                outputbox.SelectionColor = Color.Red;
                outputbox.AppendText(e.Message);
            }
            if (localIP == null)
            {
                localIP = "localhost";
            }
            return localIP;
        }
        #endregion

        #region Taskbar Icon
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void exitXMTunerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void viewServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://" + getLocalIP() + ":" + port);
        }
        #endregion

        #region About Tab
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.pcfire.net/XMTuner/");
        }
        #endregion

        #region Log Tab / Outputbox
        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        private void output(String output, String level)
        {
            logging.output(output, level);
        }


        public delegate void SetTextCallback(ref RichTextBox outputbox, string text, Color color);
        public static void output(String output, String level, ref RichTextBox outputbox)
        {
            Color color = Color.Black;
            if (level.ToLower().Equals("error"))
            {
                color = Color.Red;
            }
            if (level.ToLower().Equals("debug"))
            {
                color = Color.CornflowerBlue;
            }

            //We can't talk to outputbox from the server thread...
            if (outputbox.InvokeRequired)
            {
                SetTextCallback d = new Form1.SetTextCallback(SetText);
                outputbox.Invoke(d, new object[] { outputbox, output, color });
            }
            else
            {
                outputbox.SelectionColor = color;
                outputbox.AppendText(output);
                outputbox.Refresh();
            }


        }

        // This method is passed in to the SetTextCallBack delegate
        // to set the Text property of textBox1.
        private static void SetText(ref RichTextBox outputbox, string text, Color color)
        {
            outputbox.SelectionColor = color;
            outputbox.AppendText(text);
            outputbox.Refresh();
        }

        public void refreshOutput(String output)
        {
            outputbox.Text = output;
            outputbox.Refresh();
        }

        private void outputbox_TextChanged(object sender, EventArgs e)
        {
            outputbox.SelectionStart = outputbox.Text.Length;
            outputbox.ScrollToCaret();
        }

        private void outputbox_Layout(object sender, LayoutEventArgs e)
        {
            outputbox.Focus();
        }

        private void outputbox_TextChanged_1(object sender, EventArgs e)
        {
            outputbox.ScrollToCaret();
        }

        private void tabcontrol1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Equals(tLog))
            {
                outputbox.Focus();
                outputbox.SelectionStart = outputbox.Text.Length;
                outputbox.ScrollToCaret();
            }
        }

        protected void Link_Clicked(object sender, LinkClickedEventArgs e)
        {
            if (e.LinkText.Contains("/xmtuner/update/")) {
                Updater update = new Updater();
            } else {
                System.Diagnostics.Process.Start(e.LinkText);
            }
        }
        #endregion

        #region Channels Tab
        private Image getImageFromURL(String url)
        {
            URL imageURL = new URL(url);
            imageURL.setTimeout(500);
            imageURL.fetch();
            Image image = imageURL.responseAsImage();
            return image; //Note, this can be null
        }

        private void loadChannels()
        {
            if (self == null || channelBox.Items.Count > 0) { return; }

            typeBox.SelectedItem = "Channel";
            if (isMMS) { protocolBox.SelectedItem = "MMS"; } else { protocolBox.SelectedItem = "HTTP"; }
            if (bitrate.Equals("high")) { bitRateBox.SelectedItem = "High"; } else { bitRateBox.SelectedItem = "Low"; }

            channelBox.BeginUpdate();
            channelBox.Clear();
            ImageList imagelist = new ImageList();
            channelBox.LargeImageList = imagelist;
            imagelist.ImageSize = new Size(45, 40);
            int i = 0;
            cbIconsLoaded = true;
            channelBox.Columns.AddRange(new ColumnHeader[] { new ColumnHeader(), new ColumnHeader()});

            Boolean setLogoImage = false;
            Image defaultImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("XMTuner.xmtuner64.png"));

            int errcnt = 0;
            foreach (XMChannel chan in self.getChannels())
            {
                Int32 imagenum;
                Image image = null;
                Boolean haveLogo = false;
                //If the logo is defined, attempt to load it...
                if (chan.logo_small != null)
                {
                    //Unless we've already had problems loading logos before...
                    if (errcnt <= 2)
                    {
                        image = getImageFromURL(chan.logo_small);
                    }
                    //We failed to get a logo: increase the error count
                    if (image == null)
                    {
                        errcnt++;
                    }
                    else
                    {
                        haveLogo = true;
                    }
                }

                //Evaluate if the logo is defined, so we catch it being declared invalid above
                if (haveLogo)
                {
                    //If we have the image, use it.
                    if (image != null)
                    {
                        imagelist.Images.Add(image);
                    }
                    setLogoImage = true;
                }
                else
                {
                    //Default Icon
                    if (setLogoImage == false && imagelist.Images.Count < 1)
                    {
                        imagelist.ImageSize = new Size(30, 30);
                        cbIconsLoaded = false;
                    }
                    imagelist.Images.Add(defaultImage);
                    
                }
                imagenum = i;
                ListViewItem item = new ListViewItem(new String[] {chan.ToSimpleString(), chan.desc});
                item.ImageIndex = imagenum;
                item.Name = chan.num.ToString();
                channelBox.Items.Add(item);
                i++;
            }
            if (errcnt >= 3) { 
                cbIconsLoaded = false;
            }

            channelBox.EndUpdate();
            timerCB.Enabled = true;
            timerCB.Tag = 0;
            txtChannel.Enabled = true;

        }

        private void unloadChannels()
        {
            timerCB.Enabled = false;
            channelBox.Clear();
            channelBox.LargeImageList.Dispose();
        }

        private void updateChannels()
        {
            if (cbIconsLoaded == false && self.loadedExtendedChannelData == true)
            {
                int j = (int)timerCB.Tag;
                if (j == 0 || j >= 12)
                {
                    channelBox.Clear();
                    loadChannels();
                    j = (int)timerCB.Tag;
                }
                j++;
                timerCB.Tag = j;
            }
            int i = 0;
            foreach (XMChannel chan in self.getChannels())
            {
                channelBox.Items[i].SubItems[1].Text = chan.artist + " - " + chan.song;
                i++;
            }
        }

        private String getChannelAddress(String channelString, String protocol, String altBitrate)
        {
            if (channelBox.SelectedItems.Count == 0) { return ""; }
            Int32 channelNum = Convert.ToInt32(channelBox.SelectedItems[0].Name);
            return getAddress("stream", protocol, altBitrate, channelNum);
        }

        private String getFeedAddress(String protocol, String altBitrate)
        {
            return getAddress("feed", protocol, altBitrate, 0);
        }

        private String getPlaylistAddress(string protocol, string altBitrate)
        {
            return getAddress("playlist", protocol, altBitrate, 0);
        }

        private String getAddress(String type, String protocol, String altBitrate, Int32 channelNum)
        {
            NameValueCollection collectionForAdd = new NameValueCollection();
            collectionForAdd.Add("type", protocol.ToLower());
            collectionForAdd.Add("bitrate", altBitrate.ToLower());
            NameValueCollection config = new NameValueCollection();
            config.Add("bitrate", bitrate);
            config.Add("isMMS", isMMS.ToString());
            if (bitRateBox.SelectedIndex == -1) { altBitrate = bitrate; }

            String host;
            if (hostname.Equals("")) { host = ip; } else { host = hostname; }
            host = host + ":" + port;

            String address = TheConstructor.buildLink(type, host, collectionForAdd, null, channelNum, config);
            return address;
        }

        private void makeAddress(object sender, EventArgs e)
        {
            
            if (channelBox.Items.Count == 0) { return; }

            String protocol;
            String channel;
            String altBitrate; 
            protocol = (String) protocolBox.SelectedItem;
            if (channelBox.SelectedItems.Count > 0)
            {

                channel = channelBox.SelectedItems[0].Name;
            }
            else
            {
                channel = channelBox.Items[0].Name;
            }
            altBitrate = (String)bitRateBox.SelectedItem;
            if (typeBox.SelectedItem.ToString().ToLower().Equals("feed"))
            {
                addressBox.Text = getFeedAddress(protocol, altBitrate);
            }
            else if (typeBox.SelectedItem.ToString().ToLower().Equals("playlist"))
            {
                addressBox.Text = getPlaylistAddress(protocol, altBitrate);
            }
            else
            {
                addressBox.Text = getChannelAddress(channel, protocol, altBitrate);
            }
        }

        private void updateTypeList(object sender, EventArgs e)
        {
            if (typeBox.SelectedItem.ToString().ToLower().Equals("playlist")) {
                protocolBox.Items.Clear();
                protocolBox.Items.Add("Type:");
                protocolBox.Items.Add("ASX");
                protocolBox.Items.Add("PLS");
                protocolBox.Items.Add("M3U");
                protocolBox.SelectedItem = "ASX";
            } else {
                protocolBox.Items.Clear();
                protocolBox.Items.Add("Protocol:");
                protocolBox.Items.Add("HTTP");
                protocolBox.Items.Add("MMS");
                if (isMMS) { protocolBox.SelectedItem = "MMS"; } else { protocolBox.SelectedItem = "HTTP"; }
            }
            makeAddress(sender, e);
        }



        private void cpyToClip_Click(object sender, EventArgs e)
        {
            if (addressBox.Text != "")
            {
                Clipboard.SetText(addressBox.Text);
            }
        }

        private void txtChannel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete) { return; }

            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtChannel.Text.Equals("")) { return; }
                int num = Convert.ToInt32(txtChannel.Text);
                play(num);
                return;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(e.KeyChar.ToString(), "\\d+"))
                e.Handled = true;
        }

        private void channelBox_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem item = channelBox.SelectedItems[0];
            if (item.Name.Equals("")) { return; }
            int num = Convert.ToInt32(item.Name);
            play(num);
        }

        private void timerCB_Tick(object sender, EventArgs e)
        {
            updateChannels();
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
                    serviceRunning = false;
                }
                else if (serviceControl.Status.ToString().ToLower().Equals("running"))
                {
                    btnSerInstall.Enabled = false;
                    btnSerUninstall.Enabled = true;
                    btnSerStart.Enabled = false;
                    btnSerStop.Enabled = true;
                    btnSerRestart.Enabled = true;
                    serviceRunning = true;
                }
                else
                {
                    btnSerInstall.Enabled = false;
                    btnSerUninstall.Enabled = true;
                    btnSerStart.Enabled = true;
                    btnSerStop.Enabled = false;
                    btnSerRestart.Enabled = false;
                    serviceRunning = false;
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

        #region Updater
        private void bUpdate_Click(object sender, EventArgs e)
        {
            //do...
            Updater update = new Updater();
        }

        private void timerUpdater_Tick(object sender, EventArgs e)
        {
            Updater update = new Updater(outputbox);
        }
        #endregion

        #region History Tab
        private void updateRecentlyPlayedBox()
        {
            recentlyPlayedBox.Clear();
            recentlyPlayedBox.SelectionFont = new Font(recentlyPlayedBox.SelectionFont, FontStyle.Bold);
            recentlyPlayedBox.SelectionColor = Color.Blue;
            recentlyPlayedBox.AppendText("Recently Played:\n");

            recentlyPlayedBox.SelectionColor = Color.Black;
            recentlyPlayedBox.SelectionFont = new Font(recentlyPlayedBox.SelectionFont, FontStyle.Regular);
            if (self.recentlyPlayed.Count == 0)
            {
                recentlyPlayedBox.AppendText("Nothing yet.. Play a channel...");
                return;
            }

            try
            {
                foreach (String entry in self.recentlyPlayed)
                {
                    recentlyPlayedBox.AppendText(entry + "\n");
                }
            }
            catch (InvalidOperationException)
            {
                recentlyPlayedBox.AppendText("Recently Played List Temporarily Not Available\n");
            }
        }
        #endregion

        private void timerTest_Tick(object sender, EventArgs e)
        {
            self.doTest();
        }
    }
}