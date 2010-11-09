using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;

namespace XMTuner
{
    public partial class Form1 : Form
    {
        Core c;
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
        bool autologin = false;
        bool showNotification;
        bool onTop;
        int numRecentHistory;

        int i = 0;

        String runTime = "";
        String ip = "";

        #region Form1 Core
        public Form1()
        {
            InitializeComponent();
            initPlayer();

            Microsoft.Win32.SystemEvents.PowerModeChanged += new
            Microsoft.Win32.PowerModeChangedEventHandler(powerModeChanged);    
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            logging = new Log(ref outputbox);
            aVersion.Text = configMan.version;
            outputbox.AppendText("XMTuner "+configMan.version+"\n");

            //Load config...
            refreshConfig();
            lblClock.Text = "0:00:00";
#if !DEBUG
            Updater update = new Updater(outputbox);
#endif
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaint(e);
            AeroLoad();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //If we have a configuration, and they want autologin, do it now.
            if (isConfigurationLoaded && autologin)
            {
                start();
            }
        }

        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        public static Boolean isService
        {
            get
            {
                return false;
            }
        }

        private void powerModeChanged(System.Object sender, Microsoft.Win32.PowerModeChangedEventArgs e)
        {
            //Handle the PowerModes we care about... (Resume, Suspend)
            if (e.Mode == Microsoft.Win32.PowerModes.Suspend)
            {
                //If the server isn't running, we don't need to do any of this...
                if (serverRunning == false) { return; }

                //We're going to sleep.. Stop.
                output("System is going to sleep, stopping server.", "info");
                stop();
            }
            else if (e.Mode == Microsoft.Win32.PowerModes.Resume)
            {
                //XXX This will probably unconditionally start the server regardless of it if was up before...

                //We're waking up, resume server.
                output("System has resumed, starting server.", "info");
                start();

                //If we were playing, resume.
                playerNum = sleepPlayerNum;
                if (playerNum > 0)
                {
                    output("Resuming playback...", "info");
                    play(playerNum);
                }
            }
        }
        #endregion

        #region Start/Stop
        //Start / Login
        private void bStart_Click(object sender, EventArgs e)
        {
            bStart.Text = "Wait...";
            bStart.Enabled = false;
            start();
            if (bStop.Enabled == true)
            {
                bStart.Text = "Start";
            }
        }
        private void start()
        {
            output("Please wait... logging in", "info");
            c = new Core(logging);
            c.tick += new Core.TickHandler(coreEvent_Do);
            c.Start();
            output("XMTuner Ready...", "info");
        }

        private void coreEvent_Do(Core c, XMTunerEventArgs e)
        {
            self = c.tuner;
            switch (e.source)
            {
                case "xmtuner":
                    switch(e.data) 
                    {
                        case "isLoggedIn":
                            timer2.Enabled = true;
                            loggedIn = true;
                            if (loggedIn)
                            {
                                bStart.Enabled = false;
                                bStop.Enabled = true;
                                channelBox.Enabled = true;
                            }
                            syncStatusLabel();
                            loadChannels();

                            break;
                        case "isLoggedOut":
                            loggedIn = false;
                            timer2.Enabled = false;
                            bStart.Enabled = true;
                            bStop.Enabled = false;

                            unloadChannels();
                            recentlyPlayedBox.Clear();
                            syncStatusLabel();

                            break;
                    }
                    break;
                case "server":
                    switch (e.data)
                    {
                        case "isRunning":
                            serverRunning = true;
                            viewServerToolStripMenuItem.Enabled = true;
                            loginToolStripMenuItem.Enabled = false;
                            timer1.Enabled = true;

                            linkServer.Text = "Server is Running...";
                            linkServer.Enabled = true;


                            output("XMTuner Ready...", "info");
                            break;
                        case "isStopped":
                            serverRunning = false;
                            linkServer.Text = "Server is Stopped";
                            output("Server Uptime was " + runTime, "info");
                            linkServer.Enabled = false;
                            timer1.Enabled = false;
                            lblClock.Text = "0:00:00";
                            break;
                    }
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double sec = 0;
            double minute = 0;
            double hour = 0;
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
            self.doWhatsOn();
        }

        //Stop
        private void bStop_Click(object sender, EventArgs e)
        {
            stop();
        }
        private void stop()
        {
            c.Stop();
            shutdownPlayer();
        }

        private void linkServer_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://" + configMan.getLocalIP() + ":" + port);
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

            //Set alwaysOnTop...
            this.TopMost = onTop; //Make XMTuner always on top

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
            //alwaysOnTop, showURLBuilder, showNotice (refreshed directly as they're Form1 elements)

            if (xmServer == null || self == null || loggedIn == false) { return; }
            //username, password, port [, network] = require restart
            if (updatedvalues.Contains("username") || updatedvalues.Contains("password") ||
                updatedvalues.Contains("port") || updatedvalues.Contains("network"))
            {
                output("Your configuration update requires XMTuner to restart the server to take effect. Restarting now...", "info");
                restart();
            }
            //numRecentHistory
            self.numItems = numRecentHistory;

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
            ip = configMan.getLocalIP();
            username = config["username"];
            password = config["password"];
            port = config["port"];
            autologin = cfg.getConfigItemAsBoolean("autologin");
            network = config["network"];
            showNotification = cfg.getConfigItemAsBoolean("showNotice");
            onTop = cfg.getConfigItemAsBoolean("alwaysOnTop");
            numRecentHistory = Convert.ToInt32(config["numRecentHistory"]);
            setChannelsListStyle(config["channelListStyle"]);
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
            System.Diagnostics.Process.Start("http://" + configMan.getLocalIP() + ":" + port);
        }
        #endregion

        #region About Tab
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.xmtuner.net/");
        }
        #endregion

        #region Log Tab / Outputbox
        private void output(String output, String level)
        {
            logging.output(output, level);
        }

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
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
            if (level.ToLower().Equals("notice"))
            {
                color = Color.OrangeRed;
            }
            if (level.ToLower().Contains("player"))
            {
                color = Color.Green;
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

        private void loadChannels()
        {
            if (self == null) { return; }
            if (channelBox.Tag == null) { channelBox.Tag = ""; }

            //Channel ListView
            output("Channels Tab: Loading...", "debug");
            channelBox.BeginUpdate();
            channelBox.Clear();

            //Columns for Item and Subitems
            channelBox.Columns.AddRange(new ColumnHeader[] { new ColumnHeader(), new ColumnHeader() });

            //Set up ImageList (Construct, Link, Declare Size)
            ImageList imagelist = new ImageList();
            channelBox.LargeImageList = imagelist;
            imagelist.ImageSize = new Size(45, 40);
            Image defaultImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("XMTuner.xmtuner64.png"));

            //Add Groups
            channelBox.Groups.Add("cbGroupFavorite", "Favorite Channels");
            channelBox.Groups.Add("cbGroupNormal", network.ToUpper() + " Channels");

            Int32 imagenum = 0;
            //Iterate through the channels (using a copy of the List)
            foreach (XMChannel chan in self.getChannels())
            {
                //First, we deal with the Logo and adding it to the ImageList linked to the ListView
                Image image = null;

                //If the logo is defined, load it...
                if (chan.logo_small_image != null)
                {
                    image = chan.logo_small_image;
                    imagelist.Images.Add(image);

                }
                else
                {
                    //Default Icon
                    if (imagelist.Images.Count.Equals(0) && image == null)
                    {
                        imagelist.ImageSize = new Size(30, 30);
                    }
                    imagelist.Images.Add(defaultImage);
                    output("Channels Tab: Using default logo for " + chan.ToString(), "debug");
                }
                //End Logo

                //Construct ListViewItem to Add to the ListView
                ListViewItem item = new ListViewItem(new String[] {chan.ToString(), chan.desc});
                item.Name = chan.num.ToString();
                item.ImageIndex = imagenum;
                item.ToolTipText = chan.ToString()+"\n" + chan.desc+"\n";

                    //Groups (Assign the Channel to its group)
                    /* The General Group (For All Modes)
                     *   (Favorites and Category Views will override this as needed.) */
                    item.Group = channelBox.Groups["cbGroupNormal"];

                    /* Create and set category groups as we build, if we're building in category mode */
                    if (channelBox.Tag.Equals("category"))
                    {
                        //If Category isn't yet a group, create it...
                        if (channelBox.Groups.IndexOf(channelBox.Groups[chan.category]) == -1)
                        {
                            channelBox.Groups.Add(chan.category, chan.category.ToUpperFirstLetter());
                        }
                        //Add Item to its Category Group
                        item.Group = channelBox.Groups[chan.category];
                    }

                    /* Mark Favorites for Favorites and Category Modes */
                    if (!channelBox.Tag.Equals("") && self.favorites.isFavorite(chan.num))
                    {
                        item.Group = channelBox.Groups["cbGroupFavorite"];
                    }

                channelBox.Items.Add(item);
                imagenum++;
            }

            channelBox.EndUpdate();
            output("Channels Tab: Complete", "debug");
            timerCB.Enabled = true;
            timerCB.Tag = 0;
        }

        private void updateChannels()
        {
            if (self.preloadImageRunning == false && self.preloadImagesUpdated == true)
            {
                output("Channels Tab: New Images in Cache Detected, Updating...", "debug");
                loadChannels();
                self.preloadImagesUpdated = false;
            }

            //Update What's On Data in the Channel Box
            int i = 0;
            int c = channelBox.Items.Count - 1;
            foreach (XMChannel chan in self.getChannels())
            {
                if (chan.artist != null && chan.song != null && i <= c)
                {
                    channelBox.Items[i].SubItems[1].Text = chan.artist + " - " + chan.song;
                }
                i++;
            }
        }

        private void unloadChannels()
        {
            timerCB.Enabled = false;
            channelBox.Clear();
            channelBox.LargeImageList.Dispose();
        }


        private void channelBox_DoubleClick(object sender, EventArgs e)
        {
            if (channelBox.SelectedItems.Count < 1)
            {
                return;
            }
            ListViewItem item = channelBox.SelectedItems[0];
            if (item.Name.Equals("")) { return; }
            int num = Convert.ToInt32(item.Name);
            play(num);
        }

        private void channelBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                channelBox_DoubleClick(sender, e);
            }
        }

        private void timerCB_Tick(object sender, EventArgs e)
        {
            updateChannels();
        }

        private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListViewItem item = channelBox.SelectedItems[0];
            if (item.Name.Equals("")) { return; }
            int num = Convert.ToInt32(item.Name);
            XMChannel ch = self.Find(num);
            if (self.favorites.isFavorite(num) == false)
            {
                //Add to Favorites...
                DialogResult result = MessageBox.Show("Add \"" + ch.ToString() + "\" to Favorites?", "Add to Favorites", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (self.favorites.addFavoriteChannel(num))
                    {
                        item.Group = channelBox.Groups["cbGroupFavorite"];
                    }
                }
            }
            else
            {
                //Already in favorites, offer to remove...
                DialogResult result = MessageBox.Show("Remove \"" + ch.ToString() + "\" from Favorites?", "Remove from Favorites", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    if (self.favorites.removeFavoriteChannel(num))
                    {
                        item.Group = channelBox.Groups["cbGroupNormal"];
                    }
                }
            }
        }

        private void setChannelsListStyle(String style)
        {
            favoriteChannelsToolStripMenuItem.Checked = false;
            allChannelsToolStripMenuItem.Checked = false;
            byCategoryToolStripMenuItem.Checked = false;

            if (style.ToLower().Equals("all"))
            {
                allChannelsToolStripMenuItem.Checked = true;
                channelBox.Tag = "";
            }
            else if (style.ToLower().Equals("category"))
            {
                byCategoryToolStripMenuItem.Checked = true;
                channelBox.Tag = "category";
            }
            else if (style.ToLower().Equals("favorites"))
            {
                favoriteChannelsToolStripMenuItem.Checked = true;
                channelBox.Tag = "favorites";
                
            }
        }

        private void allChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setChannelsListStyle("all");
            loadChannels();
        }

        private void favoriteChannelsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setChannelsListStyle("favorites");
            loadChannels();
        }

        private void byCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setChannelsListStyle("category");
            loadChannels();
        }

        private void uRLBuilderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3(Convert.ToInt32(channelBox.SelectedItems[0].Name));
            form3.ShowDialog();
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
            recentlyPlayedBox.BeginUpdate();
            recentlyPlayedBox.View = View.Tile;
            recentlyPlayedBox.Clear();
            recentlyPlayedBox.Columns.Add("Time");
            recentlyPlayedBox.Columns.Add("Channel");
            recentlyPlayedBox.Columns.Add("Song");

            //Image defaultImage = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("XMTuner.xmtuner64.png"));
            //ImageList imagelist = new ImageList();
            recentlyPlayedBox.LargeImageList = channelBox.LargeImageList;
            //imagelist.ImageSize = new Size(30, 30); //new Size(45, 40);
            //imagelist.Images.Add(defaultImage);

            if (self.recentlyPlayed.Count == 0)
            {
                recentlyPlayedBox.Items.Add(new ListViewItem("Nothing played yet...", recentlyPlayedBox.Groups[0]));
                return;
            }

            try
            {
                foreach (String entry in self.recentlyPlayed)
                {
                    String[] _entry = entry.Split(new Char[] {'-'}, 2);
                    String[] __entry = _entry[0].Split(new String[] {"M: "}, 2, StringSplitOptions.None);
                    ListViewItem item = new ListViewItem(new String[] {_entry[1].Trim(), _entry[0].Trim()});
                    item.ImageIndex = channelBox.FindItemWithText(__entry[1]).ImageIndex;
                    item.Group = recentlyPlayedBox.Groups[0];
                    recentlyPlayedBox.Items.Add(item);
                }
            }
            catch (InvalidOperationException) {}
            recentlyPlayedBox.EndUpdate();
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            String data = "";
            foreach (ListViewItem item in recentlyPlayedBox.SelectedItems)
            {
                if (data.Equals("") == false)
                {
                    data += Environment.NewLine;
                }

                data += item.Text;
            }
            Clipboard.SetText(data);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            self.recentlyPlayed.Clear();
            recentlyPlayedBox.Clear();
            updateRecentlyPlayedBox();
        }
        #endregion

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

        private void AeroLoad()
        {
            if (System.Environment.OSVersion.Version.Major >= 6)  //make sure you are not on a legacy OS 
            {
                int en = 0;
                DwmIsCompositionEnabled(ref en);  //check if the desktop composition is enabled
                if (en > 0)
                {
                    this.TransparencyKey = Color.Gainsboro;
                    this.BackColor = Color.Gainsboro;
                    splitContainer2.BackColor = SystemColors.Control;
                    splitContainer2.Panel1.BackColor = Color.Gainsboro;

                    MARGINS margins = new MARGINS();

                    margins.cxLeftWidth = 0;
                    margins.cxRightWidth = 0;
                    margins.cyTopHeight = -1;
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

    }
}