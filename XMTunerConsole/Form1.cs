﻿using System;
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

        int playerNum;
        int p;

        public Form1()
        {
#if DEBUG
            useLocalDatapath = true;
#endif
            InitializeComponent();
            initPlayer();
        }

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
                outputbox.Invoke (d, new object[] {outputbox, output, color});
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
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
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
                button1.Enabled = false;
                button5.Enabled = true;
                channelBox.Enabled = true;
                if (!tversityHost.Equals("")) { protocolBox.Items.Add("MP3"); }
            }
            syncStatusLabel();
            loadChannels();
        }



        private void outputbox_TextChanged(object sender, EventArgs e)
        {
            outputbox.SelectionStart = outputbox.Text.Length;
            outputbox.ScrollToCaret();
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
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
                username = configIn.Get("username") ;
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
                if (!serverRunning) {
                    button1.Enabled = true;
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

                button1.Enabled = false;
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
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            if (logging == null) { return; }
            logging.log(i);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (loggedIn)
            {
                self.doWhatsOn();
            }
            logging.log(i);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serviceControl.ServiceName = "XMTunerService";

            service_button_reset();
            
            if (refreshConfig() && autologin && !serviceRunning)
            {
                button1_Click(sender, e);
            }
            lblClock.Text = "0:00:00";

            if (!useLocalDatapath) 
            {
                Updater update = new Updater(outputbox);
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            xmServer.stop();
            serverRunning = false;
            timer2.Enabled = false;
            button1.Enabled = true;
            button5.Enabled = false;
            
        }

        private void loadChannels()
        {
            int i = 0;
            foreach (XMChannel chan in self.getChannels())
            {
                channelBox.Items.Add(chan.ToSimpleString());
                if (i == 0) { channelBox.SelectedItem = chan.ToSimpleString(); }
                i++;
            }
            if (isMMS) { protocolBox.SelectedItem = "MMS"; } else { protocolBox.SelectedItem = "HTTP"; }
            if (bitrate.Equals("high")) { bitRateBox.SelectedItem = "High"; } else { bitRateBox.SelectedItem = "Low"; }
            addressBox.Text = getChannelAddress((String)channelBox.SelectedItem, (String)protocolBox.SelectedItem, (String)bitRateBox.SelectedItem);
            
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

        private void makeAddress(object sender, EventArgs e)
        {
            String protocol;
            String channel;
            String altBitrate; 
            protocol = (String) protocolBox.SelectedItem;
            channel = (String)channelBox.SelectedItem;
            altBitrate = (String)bitRateBox.SelectedItem;
            addressBox.Text = getChannelAddress(channel, protocol, altBitrate);
        }

        private void channelBox_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            System.Diagnostics.Process.Start("http://"+getLocalIP()+":" + port);
        }

        private void cpyToClip_Click(object sender, EventArgs e)
        {
            if (addressBox.Text != "")
            {
                Clipboard.SetText(addressBox.Text);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.pcfire.net/XMTuner/"); 
        }

        private void tabcontrol1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Equals(tabPage1))
            {
                outputbox.Focus();
                outputbox.SelectionStart = outputbox.Text.Length;
                outputbox.ScrollToCaret();
            }
        }

        private void outputbox_Layout(object sender, LayoutEventArgs e)
        {
            outputbox.Focus();
        }

        private void outputbox_TextChanged_1(object sender, EventArgs e)
        {
            //outputbox.Focus();
            outputbox.ScrollToCaret();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            serviceControl.Stop();
            serviceControl.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped);
            service_button_reset();

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {

            servicemanager sm = new servicemanager("XMTunerService", "Provides XMRO to Devices", "XM Tuner");
            bool sucess = sm.Uninstall();
            service_button_reset();
            MessageBox.Show("If you wish to reinstall the service, please restart XMTuner");
            btnSerUninstall.Enabled = false;
            btnSerStart.Enabled = false;
            

        }

        private void btnSerStart_Click(object sender, EventArgs e)
        {
            serviceControl.Start();
            serviceControl.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running);
            service_button_reset();

        }

        protected void Link_Clicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
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

        private void button2_Click(object sender, EventArgs e)
        {
            //do...
            Updater update = new Updater();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Updater update = new Updater(outputbox);
        }

        private void updateNowPlayingData(Boolean useDefault, Int32 num)
        {
            if (useDefault == true)
            {
                pLogoBox.ImageLocation = "";
                showLogo();
                syncStatusLabel();
                pLabel1.Text = ""; //"Channel:";
                pLabel2.Text = ""; //"Title:";
                pLabel3.Text = ""; //"Artist:";
                pLabel4.Text = ""; //"Album:";
                pLabel5.Text = "";
                pLabel6.Text = "";
            }
            else
            {
                if (num == 0)
                {
                    num = playerNum;
                }

                XMChannel npChannel = self.Find(num);

                syncStatusLabel();
                
                if (pLogoBox.ImageLocation.Equals(""))
                {
                    //pLogoBox.SizeMode = PictureBoxSizeMode.CenterImage;
                    pLogoBox.ClientSize = new Size(128, 50);
                    pLogoBox.ImageLocation = npChannel.logo;
                }
                pLabel1.Text = "XM " + npChannel.num + " - " + npChannel.name;
                pLabel2.Text = npChannel.song;
                pLabel3.Text = npChannel.artist;
                //pLabel5 is Player status (Handled in axWindowsMediaPlayer1_StatusChange)
                pLabel6.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString; //pLabel6 is Player timer

                //pLabel4: Tri-mode Artist/Program Now/Next Text
                if (p <= 5)
                {
                    if (npChannel.album.Equals("")) { p = 5; p++; return; }
                    pLabel4.Text = npChannel.album;
                }
                else if (p <= 10 && p > 5)
                {
                    String[] program = self.getCurrentProgram(npChannel.programData);
                    if (program == null) { p = 10; p++; return; }
                    pLabel4.Text = "Now: " + program[2];
                }
                else if (p > 10)
                {
                    String[] nextProgram = self.getNextProgram(npChannel.programData);
                    if (nextProgram == null) { p = 0; p++; return; }
                    pLabel4.Text = "Next: " + DateTime.Parse(nextProgram[4]).ToShortTimeString() + ": " + nextProgram[2];
                    if (p >= 15) { p = 0; }
                }
                p++;
            }
        }

        private void syncStatusLabel()
        {
            pStatusLabel.Visible = true;
            if (loggedIn == false)
            {
                pStatusLabel.Text = "Log in to begin...";
            }
            else
            {
                if (playerNum != 0)
                {
                    pStatusLabel.Text = "";
                    pStatusLabel.Visible = false;
                }
                else
                {

                    pStatusLabel.Text = "Select a channel...";
                }
            }
        }

        private void bTune_Click(object sender, EventArgs e)
        {
            updateNowPlayingData(true, 0);

            int num = Convert.ToInt32(txtChannel.Text);


            axWindowsMediaPlayer1.URL = self.play(num, "high");
            playerNum = num;
            updateNowPlayingData(false, num);

            updateRecentlyPlayedBox();
        }

        private void axWindowsMediaPlayer1_StatusChange(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsReady &&
                axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsStopped)
            {
                String status = axWindowsMediaPlayer1.status;
                if (status.Contains("Playing"))
                {
                    String[] temp = status.Replace("reflector:", "").Split(':');
                    status = "Playing (" + temp[1].Trim()+")";
                }
                pLabel5.Visible = true;
                pLabel5.Text = status;
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // If Windows Media Player is in the playing state, enable the data update timer. 
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.enableContextMenu = true;
                pTimer.Enabled = true;
                showWMPPlayerUI();
            }
            else
            {
                pTimer.Enabled = false;
            }

            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                axWindowsMediaPlayer1.enableContextMenu = false;
                //Tell the app we're done playing so history stops being built.
                playerNum = 0;
                self.lastChannelPlayed = 0;
                updateNowPlayingData(true, 0);
            }

        }

        private void pTimer_Tick(object sender, EventArgs e)
        {
            updateNowPlayingData(false, 0);
        }

        private void txtChannel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return) {
                bTune_Click(sender, e);
            }
        }

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

        private void pLabel2_TextChanged(object sender, EventArgs e)
        {
            if (pLabel2.Text.Equals("Title:") || pLabel2.Text.Equals(""))
            {
                return;
            }
            String dummy = pLabel2.Text;
            updateRecentlyPlayedBox();
            doNotification();
        }

        private void doNotification()
        {
            if (playerNum == 0) { return; } //Bail early if we have no work to do.
            XMChannel npChannel = self.Find(playerNum);
            String title = "XM "+npChannel.num+" - "+npChannel.name;
            String nptext = npChannel.artist + " - " + npChannel.song;
            NotifyWindow nw = new NotifyWindow(title, "Now Playing:\n"+nptext);
            nw.TitleFont = new Font("Tahoma", 8.25F, FontStyle.Bold);
            nw.Font = new Font("Tahoma", 10F);
            nw.TextColor = Color.White;
            nw.BackColor = Color.Black;
            nw.SetDimensions(300, 120);
            nw.WaitTime = 5000;
            nw.Notify();
        }

        private void button3_Click_2(object sender, EventArgs e)
        {
            doNotification();
        }

        private void initPlayer()
        {
            showLogo();
            axWindowsMediaPlayer1.uiMode = "none";

            pLabel5.Visible = false;
            updateNowPlayingData(true, 0);

            syncStatusLabel();

        }

        private void showLogo()
        {
            Image xmtunerLogo = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("XMTuner.xmtuner64.png"));
            pLogoBox.ClientSize = new Size(64, 64);
            pLogoBox.Image = xmtunerLogo;
        }

        private void axWindowsMediaPlayer1_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {
            if (axWindowsMediaPlayer1.uiMode.Equals("mini") || axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsPlaying)
            {
                return;
            }
            showWMPPlayerUI();
        }

        private void showWMPPlayerUI()
        {
            axWindowsMediaPlayer1.uiMode = "mini";
            axWindowsMediaPlayer1.Size = new Size(165, 35);
            pHoverTimer.Start();
        }

        private void pHoverTimer_Tick(object sender, EventArgs e)
        {
            pHoverTimer.Stop();
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.Size = new Size(165,50);
        }
    }
}
