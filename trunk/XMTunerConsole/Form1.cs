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

        bool useLocalDatapath = false;

        XMTuner self;
        Log logging;
        WebListner xmServer;
        bool loggedIn = false;
        bool serverRunning = false;
        String username = "";
        String password = "";
        String port = "";
        String bitrate;
        bool highbit = true;
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
#if DEBUG
            useLocalDatapath = true;
#endif
            InitializeComponent();
            initPlayer();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serviceControl.ServiceName = "XMTunerService";

            service_button_reset();

            if (refreshConfig() && autologin && !serviceRunning)
            {
                bStart_Click(sender, e);
            }
            lblClock.Text = "0:00:00";

            if (!useLocalDatapath)
            {
                Updater update = new Updater(outputbox);
            }

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
            outputbox.AppendText("Please wait... logging in\n");
            outputbox.Refresh();
            logging = new Log(ref outputbox, useLocalDatapath);
            self = new XMTuner(username, password, logging);
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

            loggedIn = true;
            if (loggedIn) {
                bStart.Enabled = false;
                bStop.Enabled = true;
                channelBox.Enabled = true;
                if (!tversityHost.Equals("")) { protocolBox.Items.Add("MP3"); }
            }
            syncStatusLabel();
            loadChannels();
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
            shutdownPlayer();
            xmServer.stop();
            serverRunning = false;
            lblClock.Text = "0:00:00";
            i = 0;
            timer2.Enabled = false;
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
        #endregion

        #region Configuration
        private void bConfigure_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(username, password, port, highbit, autologin, isMMS, tversityHost, hostname, loggedIn, useLocalDatapath);
            form2.ShowDialog();
            refreshConfig();
        }

        private bool refreshConfig()
        {
            configMan configuration = new configMan(useLocalDatapath);
            configuration.readConfig();
            ip = getLocalIP();
            if (configuration.isConfig)
            {
                NameValueCollection configIn = configuration.getConfig();
                username = configIn.Get("username");
                password = configIn.Get("password");
                port = configIn.Get("port");
                highbit = Convert.ToBoolean(configIn.Get("bitrate"));
                if (highbit) { bitrate = "high"; } else { bitrate = "low"; }
                autologin = Convert.ToBoolean(configIn.Get("autologin"));
                if (autologin && serviceRunning)
                {
                    outputbox.AppendText("Autologin skipped - Service running\n");
                }
                isMMS = Convert.ToBoolean(configIn.Get("isMMS"));
                tversityHost = configIn.Get("Tversity"); ;
                hostname = configIn.Get("hostname"); ;
                //if (hostname.Equals("")) { hostname = ip; }
                if (!serverRunning)
                {
                    bStart.Enabled = true;
                }
                loginToolStripMenuItem.Enabled = true;

                DateTime currentTime = DateTime.Now;
                String ct = currentTime.ToString("%H:") + currentTime.ToString("mm:") + currentTime.ToString("ss");
                outputbox.AppendText(ct + "  Configuration Loaded\n");
                return true;
            }
            else
            {
                if (port.Equals(""))
                {
                    port = "19081";
                }

                bStart.Enabled = false;
                outputbox.AppendText("No Configuration\nClick Configure.\n");
                return false;
            }

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
        public delegate void SetTextCallback(ref RichTextBox outputbox, string text, Color color);

        public static void output(String output, String level, ref RichTextBox outputbox)
        {
            Color color = Color.Black;
            if (level.ToLower().Equals("debug") || level.ToLower().Equals("error"))
            {
                color = Color.Red;
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
            System.Diagnostics.Process.Start(e.LinkText);
        }
        #endregion

        #region Channels Tab
        private Image getImageFromURL(String url)
        {
            URL imageURL = new URL(url);
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
            Boolean setDefaultImage = false;
            Image defaultImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("XMTuner.xmtuner64.png"));
            channelBox.Columns.AddRange(new ColumnHeader[] { new ColumnHeader(), new ColumnHeader()});

            foreach (XMChannel chan in self.getChannels())
            {
                Int32 imagenum;
                if (chan.logo_small != null)
                {
                    Image image = getImageFromURL(chan.logo_small);
                    if (image != null)
                    {
                        imagelist.Images.Add(image);
                    }
                    //if (channel.category.ToLower().Contains("talk") || channel.category.ToLower().Contains("sports"))
                    // then the default image height is too small.. 
                    imagenum = i;
                }
                else
                {
                    //Default Icon
                    if (setDefaultImage == false)
                    {
                        imagelist.ImageSize = new Size(30, 30);
                        imagelist.Images.Add(defaultImage);
                    }
                    cbIconsLoaded = false;
                    imagenum = 0;
                    setDefaultImage = true;
                }
                ListViewItem item = new ListViewItem(new String[] {chan.ToSimpleString(), chan.desc});
                item.ImageIndex = imagenum;
                item.Name = chan.num.ToString();
                channelBox.Items.Add(item);
                i++;
            }

            channelBox.EndUpdate();
            timerCB.Enabled = true;
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
                channelBox.Clear();
                loadChannels();
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
            if (channelString == null) {
                return "";
            }

            String[] tmp1 = channelString.Split('-');
            String channelNum = tmp1[0].Replace("XM","").Trim();
            String channelAddress = protocol.ToLower()+"://"+hostname+":"+port+"/streams/"+channelNum+"/"+bitrate;

            if (bitRateBox.SelectedIndex == -1) { altBitrate = bitrate; }
            NameValueCollection collectionForAdd = new NameValueCollection();
            collectionForAdd.Add("type", protocol.ToLower());
            collectionForAdd.Add("bitrate", altBitrate.ToLower());

            NameValueCollection config = new NameValueCollection();
            config.Add("bitrate", bitrate);
            config.Add("isMMS", isMMS.ToString());
            String useHost;
            if (hostname.Equals("")) { useHost = ip; } else { useHost = hostname; }
            useHost = useHost + ":" + port;
            String address1 = TheConstructor.buildLink("stream", useHost, collectionForAdd, null, Convert.ToInt32(channelNum), config);
            
            return address1;
        }

        private String getFeedAddress(String protocol, String altBitrate)
        {
            NameValueCollection collectionForAdd = new NameValueCollection();
            collectionForAdd.Add("type", protocol.ToLower());
            collectionForAdd.Add("bitrate", altBitrate.ToLower());
            NameValueCollection config = new NameValueCollection();
            config.Add("bitrate", bitrate);
            config.Add("isMMS", isMMS.ToString());

            String host;
            if (hostname.Equals("")) { host = ip; } else { host = hostname; }
            host = host + ":" + port;

            if (bitRateBox.SelectedIndex == -1) { altBitrate = bitrate; }

            String address = TheConstructor.buildLink("feed", host, collectionForAdd, null, 0, config);

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
            else
            {
                addressBox.Text = getChannelAddress(channel, protocol, altBitrate);
            }
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
    }
}