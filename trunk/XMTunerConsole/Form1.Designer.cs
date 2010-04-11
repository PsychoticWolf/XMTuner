namespace XMTuner
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Recently Played", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Nothing Played Yet...");
            this.allChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoriteChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bStart = new System.Windows.Forms.Button();
            this.bConfigure = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblClock = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitXMTunerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bStop = new System.Windows.Forms.Button();
            this.tabcontrol1 = new System.Windows.Forms.TabControl();
            this.tLog = new System.Windows.Forms.TabPage();
            this.outputbox = new System.Windows.Forms.RichTextBox();
            this.tChannels = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.channelBox = new System.Windows.Forms.ListView();
            this.channelContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.uRLBuilderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disabledToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.bitRateBox = new System.Windows.Forms.ComboBox();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.cpyToClip = new System.Windows.Forms.Button();
            this.protocolBox = new System.Windows.Forms.ComboBox();
            this.txtChannel = new System.Windows.Forms.TextBox();
            this.tHistory = new System.Windows.Forms.TabPage();
            this.recentlyPlayedBox = new System.Windows.Forms.ListView();
            this.tAbout = new System.Windows.Forms.TabPage();
            this.bUpdate = new System.Windows.Forms.Button();
            this.aBuildDate = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.aVersion = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.pLabel6 = new System.Windows.Forms.Label();
            this.pLabel5 = new System.Windows.Forms.Label();
            this.pLabel4 = new System.Windows.Forms.Label();
            this.pLabel3 = new System.Windows.Forms.Label();
            this.pLabel2 = new System.Windows.Forms.Label();
            this.pLabel1 = new System.Windows.Forms.Label();
            this.pLogoBox = new System.Windows.Forms.PictureBox();
            this.timerUpdater = new System.Windows.Forms.Timer(this.components);
            this.pTimer = new System.Windows.Forms.Timer(this.components);
            this.playerPanel = new System.Windows.Forms.Panel();
            this.pStatusLabel = new System.Windows.Forms.Label();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.pHoverTimer = new System.Windows.Forms.Timer(this.components);
            this.timerCB = new System.Windows.Forms.Timer(this.components);
            this.linkServer = new System.Windows.Forms.LinkLabel();
            this.timerTest = new System.Windows.Forms.Timer(this.components);
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.trayIconContextMenu.SuspendLayout();
            this.tabcontrol1.SuspendLayout();
            this.tLog.SuspendLayout();
            this.tChannels.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.channelContextMenu.SuspendLayout();
            this.tHistory.SuspendLayout();
            this.tAbout.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pLogoBox)).BeginInit();
            this.playerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allChannelsToolStripMenuItem,
            this.favoriteChannelsToolStripMenuItem,
            this.byCategoryToolStripMenuItem});
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            viewToolStripMenuItem.Text = "View";
            // 
            // allChannelsToolStripMenuItem
            // 
            this.allChannelsToolStripMenuItem.Name = "allChannelsToolStripMenuItem";
            this.allChannelsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.allChannelsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.allChannelsToolStripMenuItem.Text = "All Channels";
            this.allChannelsToolStripMenuItem.Click += new System.EventHandler(this.allChannelsToolStripMenuItem_Click);
            // 
            // favoriteChannelsToolStripMenuItem
            // 
            this.favoriteChannelsToolStripMenuItem.Name = "favoriteChannelsToolStripMenuItem";
            this.favoriteChannelsToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.favoriteChannelsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.favoriteChannelsToolStripMenuItem.Text = "Favorite Channels";
            this.favoriteChannelsToolStripMenuItem.Click += new System.EventHandler(this.favoriteChannelsToolStripMenuItem_Click);
            // 
            // byCategoryToolStripMenuItem
            // 
            this.byCategoryToolStripMenuItem.Name = "byCategoryToolStripMenuItem";
            this.byCategoryToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.byCategoryToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.byCategoryToolStripMenuItem.Text = "By Category";
            this.byCategoryToolStripMenuItem.Click += new System.EventHandler(this.byCategoryToolStripMenuItem_Click);
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(3, 0);
            this.bStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(60, 23);
            this.bStart.TabIndex = 3;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bConfigure
            // 
            this.bConfigure.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.bConfigure.Location = new System.Drawing.Point(132, 0);
            this.bConfigure.Margin = new System.Windows.Forms.Padding(4);
            this.bConfigure.Name = "bConfigure";
            this.bConfigure.Size = new System.Drawing.Size(60, 23);
            this.bConfigure.TabIndex = 10;
            this.bConfigure.Text = "Configure";
            this.bConfigure.UseVisualStyleBackColor = true;
            this.bConfigure.Click += new System.EventHandler(this.bConfigure_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tag = "Server Uptime Counter";
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = true;
            this.lblClock.Location = new System.Drawing.Point(508, 9);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(49, 13);
            this.lblClock.TabIndex = 12;
            this.lblClock.Text = "00:00:00";
            // 
            // timer2
            // 
            this.timer2.Interval = 30000;
            this.timer2.Tag = "Update What\'s On timer";
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.trayIconContextMenu;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "XMTuner";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // trayIconContextMenu
            // 
            this.trayIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.viewServerToolStripMenuItem,
            this.restoreToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitXMTunerToolStripMenuItem});
            this.trayIconContextMenu.Name = "contextMenuStrip1";
            this.trayIconContextMenu.Size = new System.Drawing.Size(145, 98);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Enabled = false;
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.bStart_Click);
            // 
            // viewServerToolStripMenuItem
            // 
            this.viewServerToolStripMenuItem.Enabled = false;
            this.viewServerToolStripMenuItem.Name = "viewServerToolStripMenuItem";
            this.viewServerToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.viewServerToolStripMenuItem.Text = "What\'s On...";
            this.viewServerToolStripMenuItem.Click += new System.EventHandler(this.viewServerToolStripMenuItem_Click);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // exitXMTunerToolStripMenuItem
            // 
            this.exitXMTunerToolStripMenuItem.Name = "exitXMTunerToolStripMenuItem";
            this.exitXMTunerToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.exitXMTunerToolStripMenuItem.Text = "Exit XMTuner";
            this.exitXMTunerToolStripMenuItem.Click += new System.EventHandler(this.exitXMTunerToolStripMenuItem_Click);
            // 
            // bStop
            // 
            this.bStop.Enabled = false;
            this.bStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bStop.Location = new System.Drawing.Point(67, 0);
            this.bStop.Margin = new System.Windows.Forms.Padding(2);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(60, 23);
            this.bStop.TabIndex = 15;
            this.bStop.Text = "Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // tabcontrol1
            // 
            this.tabcontrol1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.tabcontrol1.Controls.Add(this.tLog);
            this.tabcontrol1.Controls.Add(this.tChannels);
            this.tabcontrol1.Controls.Add(this.tHistory);
            this.tabcontrol1.Controls.Add(this.tAbout);
            this.tabcontrol1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabcontrol1.HotTrack = true;
            this.tabcontrol1.Location = new System.Drawing.Point(0, 0);
            this.tabcontrol1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 50);
            this.tabcontrol1.Name = "tabcontrol1";
            this.tabcontrol1.SelectedIndex = 0;
            this.tabcontrol1.Size = new System.Drawing.Size(560, 239);
            this.tabcontrol1.TabIndex = 16;
            this.tabcontrol1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabcontrol1_Selected);
            // 
            // tLog
            // 
            this.tLog.BackColor = System.Drawing.Color.Transparent;
            this.tLog.Controls.Add(this.outputbox);
            this.tLog.Location = new System.Drawing.Point(4, 22);
            this.tLog.Margin = new System.Windows.Forms.Padding(2);
            this.tLog.Name = "tLog";
            this.tLog.Padding = new System.Windows.Forms.Padding(3);
            this.tLog.Size = new System.Drawing.Size(552, 213);
            this.tLog.TabIndex = 0;
            this.tLog.Text = "Log";
            this.tLog.UseVisualStyleBackColor = true;
            // 
            // outputbox
            // 
            this.outputbox.BackColor = System.Drawing.Color.White;
            this.outputbox.Cursor = System.Windows.Forms.Cursors.Default;
            this.outputbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputbox.Location = new System.Drawing.Point(3, 3);
            this.outputbox.Margin = new System.Windows.Forms.Padding(2);
            this.outputbox.Name = "outputbox";
            this.outputbox.ReadOnly = true;
            this.outputbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.outputbox.Size = new System.Drawing.Size(546, 207);
            this.outputbox.TabIndex = 6;
            this.outputbox.Text = "";
            this.outputbox.WordWrap = false;
            this.outputbox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.Link_Clicked);
            this.outputbox.Layout += new System.Windows.Forms.LayoutEventHandler(this.outputbox_Layout);
            this.outputbox.TextChanged += new System.EventHandler(this.outputbox_TextChanged_1);
            // 
            // tChannels
            // 
            this.tChannels.BackColor = System.Drawing.SystemColors.Control;
            this.tChannels.Controls.Add(this.splitContainer1);
            this.tChannels.Location = new System.Drawing.Point(4, 22);
            this.tChannels.Margin = new System.Windows.Forms.Padding(2);
            this.tChannels.Name = "tChannels";
            this.tChannels.Padding = new System.Windows.Forms.Padding(3);
            this.tChannels.Size = new System.Drawing.Size(552, 210);
            this.tChannels.TabIndex = 1;
            this.tChannels.Text = "Channels";
            // 
            // splitContainer1
            // 
            this.splitContainer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.channelBox);
            this.splitContainer1.Panel1MinSize = 150;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.splitContainer1.Panel2.ContextMenuStrip = this.channelContextMenu;
            this.splitContainer1.Panel2.Controls.Add(this.label1);
            this.splitContainer1.Panel2.Controls.Add(this.typeBox);
            this.splitContainer1.Panel2.Controls.Add(this.bitRateBox);
            this.splitContainer1.Panel2.Controls.Add(this.addressBox);
            this.splitContainer1.Panel2.Controls.Add(this.cpyToClip);
            this.splitContainer1.Panel2.Controls.Add(this.protocolBox);
            this.splitContainer1.Panel2.Controls.Add(this.txtChannel);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(546, 204);
            this.splitContainer1.SplitterDistance = 171;
            this.splitContainer1.SplitterIncrement = 5;
            this.splitContainer1.SplitterWidth = 2;
            this.splitContainer1.TabIndex = 14;
            // 
            // channelBox
            // 
            this.channelBox.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.channelBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.channelBox.ContextMenuStrip = this.channelContextMenu;
            this.channelBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelBox.FullRowSelect = true;
            this.channelBox.Location = new System.Drawing.Point(0, 0);
            this.channelBox.Margin = new System.Windows.Forms.Padding(0);
            this.channelBox.MultiSelect = false;
            this.channelBox.Name = "channelBox";
            this.channelBox.ShowItemToolTips = true;
            this.channelBox.Size = new System.Drawing.Size(544, 169);
            this.channelBox.TabIndex = 11;
            this.channelBox.TileSize = new System.Drawing.Size(515, 30);
            this.channelBox.UseCompatibleStateImageBehavior = false;
            this.channelBox.View = System.Windows.Forms.View.Tile;
            this.channelBox.SelectedIndexChanged += new System.EventHandler(this.makeAddress);
            this.channelBox.DoubleClick += new System.EventHandler(this.channelBox_DoubleClick);
            // 
            // channelContextMenu
            // 
            this.channelContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.playToolStripMenuItem,
            this.addToFavoritesToolStripMenuItem,
            this.copyURLToolStripMenuItem,
            this.toolStripSeparator2,
            viewToolStripMenuItem,
            this.uRLBuilderToolStripMenuItem});
            this.channelContextMenu.Name = "contextMenuStrip2";
            this.channelContextMenu.Size = new System.Drawing.Size(210, 120);
            // 
            // playToolStripMenuItem
            // 
            this.playToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.playToolStripMenuItem.Name = "playToolStripMenuItem";
            this.playToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.playToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.playToolStripMenuItem.Text = "Play";
            this.playToolStripMenuItem.Click += new System.EventHandler(this.channelBox_DoubleClick);
            // 
            // addToFavoritesToolStripMenuItem
            // 
            this.addToFavoritesToolStripMenuItem.Name = "addToFavoritesToolStripMenuItem";
            this.addToFavoritesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.addToFavoritesToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.addToFavoritesToolStripMenuItem.Text = "Add to Favorites...";
            this.addToFavoritesToolStripMenuItem.Click += new System.EventHandler(this.addToFavoritesToolStripMenuItem_Click);
            // 
            // copyURLToolStripMenuItem
            // 
            this.copyURLToolStripMenuItem.Name = "copyURLToolStripMenuItem";
            this.copyURLToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.copyURLToolStripMenuItem.Text = "Copy URL";
            this.copyURLToolStripMenuItem.Click += new System.EventHandler(this.cpyToClip_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(206, 6);
            // 
            // uRLBuilderToolStripMenuItem
            // 
            this.uRLBuilderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enabledToolStripMenuItem,
            this.disabledToolStripMenuItem});
            this.uRLBuilderToolStripMenuItem.Name = "uRLBuilderToolStripMenuItem";
            this.uRLBuilderToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.uRLBuilderToolStripMenuItem.Text = "URL Builder";
            // 
            // enabledToolStripMenuItem
            // 
            this.enabledToolStripMenuItem.Name = "enabledToolStripMenuItem";
            this.enabledToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.enabledToolStripMenuItem.Text = "Enabled";
            this.enabledToolStripMenuItem.Click += new System.EventHandler(this.enabledToolStripMenuItem_Click);
            // 
            // disabledToolStripMenuItem
            // 
            this.disabledToolStripMenuItem.Name = "disabledToolStripMenuItem";
            this.disabledToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.disabledToolStripMenuItem.Text = "Disabled";
            this.disabledToolStripMenuItem.Click += new System.EventHandler(this.disabledToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "URL Builder";
            // 
            // typeBox
            // 
            this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Items.AddRange(new object[] {
            "Channel",
            "Feed",
            "Playlist"});
            this.typeBox.Location = new System.Drawing.Point(3, 21);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(64, 21);
            this.typeBox.TabIndex = 12;
            this.typeBox.SelectedIndexChanged += new System.EventHandler(this.updateTypeList);
            // 
            // bitRateBox
            // 
            this.bitRateBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bitRateBox.FormattingEnabled = true;
            this.bitRateBox.Items.AddRange(new object[] {
            "Bitrate:",
            "High",
            "Low"});
            this.bitRateBox.Location = new System.Drawing.Point(139, 21);
            this.bitRateBox.Margin = new System.Windows.Forms.Padding(2);
            this.bitRateBox.Name = "bitRateBox";
            this.bitRateBox.Size = new System.Drawing.Size(64, 21);
            this.bitRateBox.TabIndex = 10;
            this.bitRateBox.SelectedIndexChanged += new System.EventHandler(this.makeAddress);
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(206, 22);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(260, 20);
            this.addressBox.TabIndex = 3;
            // 
            // cpyToClip
            // 
            this.cpyToClip.Location = new System.Drawing.Point(469, 21);
            this.cpyToClip.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.cpyToClip.Name = "cpyToClip";
            this.cpyToClip.Size = new System.Drawing.Size(42, 23);
            this.cpyToClip.TabIndex = 8;
            this.cpyToClip.Text = "Copy";
            this.cpyToClip.UseVisualStyleBackColor = true;
            this.cpyToClip.Click += new System.EventHandler(this.cpyToClip_Click);
            // 
            // protocolBox
            // 
            this.protocolBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protocolBox.FormattingEnabled = true;
            this.protocolBox.Items.AddRange(new object[] {
            "Protocol:",
            "HTTP",
            "MMS"});
            this.protocolBox.Location = new System.Drawing.Point(71, 21);
            this.protocolBox.Name = "protocolBox";
            this.protocolBox.Size = new System.Drawing.Size(64, 21);
            this.protocolBox.TabIndex = 5;
            this.protocolBox.SelectedIndexChanged += new System.EventHandler(this.makeAddress);
            // 
            // txtChannel
            // 
            this.txtChannel.Enabled = false;
            this.txtChannel.Location = new System.Drawing.Point(515, 22);
            this.txtChannel.Name = "txtChannel";
            this.txtChannel.Size = new System.Drawing.Size(27, 20);
            this.txtChannel.TabIndex = 2;
            this.txtChannel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChannel_KeyPress);
            // 
            // tHistory
            // 
            this.tHistory.Controls.Add(this.recentlyPlayedBox);
            this.tHistory.Location = new System.Drawing.Point(4, 22);
            this.tHistory.Name = "tHistory";
            this.tHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tHistory.Size = new System.Drawing.Size(552, 210);
            this.tHistory.TabIndex = 4;
            this.tHistory.Text = "History";
            this.tHistory.UseVisualStyleBackColor = true;
            // 
            // recentlyPlayedBox
            // 
            this.recentlyPlayedBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.recentlyPlayedBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recentlyPlayedBox.FullRowSelect = true;
            listViewGroup2.Header = "Recently Played";
            listViewGroup2.Name = "listViewGroup1";
            this.recentlyPlayedBox.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup2});
            this.recentlyPlayedBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem2.Group = listViewGroup2;
            this.recentlyPlayedBox.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.recentlyPlayedBox.Location = new System.Drawing.Point(3, 3);
            this.recentlyPlayedBox.Margin = new System.Windows.Forms.Padding(0);
            this.recentlyPlayedBox.MultiSelect = false;
            this.recentlyPlayedBox.Name = "recentlyPlayedBox";
            this.recentlyPlayedBox.Size = new System.Drawing.Size(546, 204);
            this.recentlyPlayedBox.TabIndex = 7;
            this.recentlyPlayedBox.TileSize = new System.Drawing.Size(507, 30);
            this.recentlyPlayedBox.UseCompatibleStateImageBehavior = false;
            this.recentlyPlayedBox.View = System.Windows.Forms.View.Tile;
            // 
            // tAbout
            // 
            this.tAbout.BackColor = System.Drawing.SystemColors.Control;
            this.tAbout.Controls.Add(this.bUpdate);
            this.tAbout.Controls.Add(this.aBuildDate);
            this.tAbout.Controls.Add(this.label8);
            this.tAbout.Controls.Add(this.label9);
            this.tAbout.Controls.Add(this.label10);
            this.tAbout.Controls.Add(this.label11);
            this.tAbout.Controls.Add(this.label12);
            this.tAbout.Controls.Add(this.aVersion);
            this.tAbout.Controls.Add(this.label13);
            this.tAbout.Controls.Add(this.groupBox1);
            this.tAbout.Location = new System.Drawing.Point(4, 22);
            this.tAbout.Margin = new System.Windows.Forms.Padding(2);
            this.tAbout.Name = "tAbout";
            this.tAbout.Padding = new System.Windows.Forms.Padding(2);
            this.tAbout.Size = new System.Drawing.Size(552, 213);
            this.tAbout.TabIndex = 2;
            this.tAbout.Text = "About";
            // 
            // bUpdate
            // 
            this.bUpdate.Location = new System.Drawing.Point(415, 181);
            this.bUpdate.Name = "bUpdate";
            this.bUpdate.Size = new System.Drawing.Size(115, 23);
            this.bUpdate.TabIndex = 17;
            this.bUpdate.Text = "Check for Updates...";
            this.bUpdate.UseVisualStyleBackColor = true;
            this.bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // aBuildDate
            // 
            this.aBuildDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aBuildDate.ForeColor = System.Drawing.Color.Black;
            this.aBuildDate.Location = new System.Drawing.Point(365, 73);
            this.aBuildDate.Name = "aBuildDate";
            this.aBuildDate.ReadOnly = true;
            this.aBuildDate.Size = new System.Drawing.Size(62, 19);
            this.aBuildDate.TabIndex = 17;
            this.aBuildDate.Text = "20100411";
            this.aBuildDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(300, 77);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Build Date:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(140, 111);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 13);
            this.label9.TabIndex = 15;
            this.label9.Text = "Curtis M. Kularski";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(140, 94);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "Chris Crews";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(122, 77);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 13;
            this.label11.Text = "Developers:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(120, 35);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(132, 31);
            this.label12.TabIndex = 12;
            this.label12.Text = "XMTuner";
            // 
            // aVersion
            // 
            this.aVersion.ForeColor = System.Drawing.Color.Black;
            this.aVersion.Location = new System.Drawing.Point(365, 43);
            this.aVersion.Name = "aVersion";
            this.aVersion.ReadOnly = true;
            this.aVersion.Size = new System.Drawing.Size(62, 20);
            this.aVersion.TabIndex = 11;
            this.aVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(311, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 13);
            this.label13.TabIndex = 10;
            this.label13.Text = "Version: ";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Controls.Add(this.linkLabel1);
            this.groupBox1.Location = new System.Drawing.Point(109, 22);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(330, 154);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(87, 139);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(125, 13);
            this.linkLabel1.TabIndex = 18;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.xmtuner.net/";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // pLabel6
            // 
            this.pLabel6.ForeColor = System.Drawing.Color.White;
            this.pLabel6.Location = new System.Drawing.Point(506, 69);
            this.pLabel6.Name = "pLabel6";
            this.pLabel6.Size = new System.Drawing.Size(54, 15);
            this.pLabel6.TabIndex = 11;
            this.pLabel6.Text = "00:00:00";
            this.pLabel6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.pLabel6.UseMnemonic = false;
            // 
            // pLabel5
            // 
            this.pLabel5.AutoSize = true;
            this.pLabel5.ForeColor = System.Drawing.Color.White;
            this.pLabel5.Location = new System.Drawing.Point(2, 68);
            this.pLabel5.Name = "pLabel5";
            this.pLabel5.Size = new System.Drawing.Size(38, 13);
            this.pLabel5.TabIndex = 10;
            this.pLabel5.Text = "Ready";
            this.pLabel5.UseMnemonic = false;
            // 
            // pLabel4
            // 
            this.pLabel4.AutoEllipsis = true;
            this.pLabel4.ForeColor = System.Drawing.Color.White;
            this.pLabel4.Location = new System.Drawing.Point(133, 53);
            this.pLabel4.Name = "pLabel4";
            this.pLabel4.Size = new System.Drawing.Size(415, 13);
            this.pLabel4.TabIndex = 9;
            this.pLabel4.Text = "Album:";
            this.pLabel4.UseMnemonic = false;
            // 
            // pLabel3
            // 
            this.pLabel3.AutoEllipsis = true;
            this.pLabel3.ForeColor = System.Drawing.Color.White;
            this.pLabel3.Location = new System.Drawing.Point(133, 37);
            this.pLabel3.Name = "pLabel3";
            this.pLabel3.Size = new System.Drawing.Size(258, 13);
            this.pLabel3.TabIndex = 8;
            this.pLabel3.Text = "Artist:";
            this.pLabel3.UseMnemonic = false;
            this.pLabel3.TextChanged += new System.EventHandler(this.pLabel2_TextChanged);
            // 
            // pLabel2
            // 
            this.pLabel2.AutoEllipsis = true;
            this.pLabel2.ForeColor = System.Drawing.Color.White;
            this.pLabel2.Location = new System.Drawing.Point(133, 22);
            this.pLabel2.Name = "pLabel2";
            this.pLabel2.Size = new System.Drawing.Size(258, 13);
            this.pLabel2.TabIndex = 7;
            this.pLabel2.Text = "Title:";
            this.pLabel2.UseMnemonic = false;
            this.pLabel2.TextChanged += new System.EventHandler(this.pLabel2_TextChanged);
            // 
            // pLabel1
            // 
            this.pLabel1.AutoEllipsis = true;
            this.pLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pLabel1.ForeColor = System.Drawing.Color.White;
            this.pLabel1.Location = new System.Drawing.Point(133, 3);
            this.pLabel1.Name = "pLabel1";
            this.pLabel1.Size = new System.Drawing.Size(258, 17);
            this.pLabel1.TabIndex = 6;
            this.pLabel1.Text = "Channel:";
            this.pLabel1.UseMnemonic = false;
            // 
            // pLogoBox
            // 
            this.pLogoBox.BackColor = System.Drawing.Color.Transparent;
            this.pLogoBox.InitialImage = null;
            this.pLogoBox.Location = new System.Drawing.Point(3, 9);
            this.pLogoBox.Name = "pLogoBox";
            this.pLogoBox.Size = new System.Drawing.Size(128, 50);
            this.pLogoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pLogoBox.TabIndex = 3;
            this.pLogoBox.TabStop = false;
            // 
            // timerUpdater
            // 
            this.timerUpdater.Enabled = true;
            this.timerUpdater.Interval = 86400000;
            this.timerUpdater.Tag = "Updater check";
            this.timerUpdater.Tick += new System.EventHandler(this.timerUpdater_Tick);
            // 
            // pTimer
            // 
            this.pTimer.Interval = 1000;
            this.pTimer.Tag = "Player timer for WMP counter";
            this.pTimer.Tick += new System.EventHandler(this.pTimer_Tick);
            // 
            // playerPanel
            // 
            this.playerPanel.BackColor = System.Drawing.Color.Black;
            this.playerPanel.Controls.Add(this.pStatusLabel);
            this.playerPanel.Controls.Add(this.axWindowsMediaPlayer1);
            this.playerPanel.Controls.Add(this.pLabel6);
            this.playerPanel.Controls.Add(this.pLabel5);
            this.playerPanel.Controls.Add(this.pLogoBox);
            this.playerPanel.Controls.Add(this.pLabel4);
            this.playerPanel.Controls.Add(this.pLabel1);
            this.playerPanel.Controls.Add(this.pLabel3);
            this.playerPanel.Controls.Add(this.pLabel2);
            this.playerPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.playerPanel.Location = new System.Drawing.Point(0, 265);
            this.playerPanel.Name = "playerPanel";
            this.playerPanel.Size = new System.Drawing.Size(560, 84);
            this.playerPanel.TabIndex = 17;
            // 
            // pStatusLabel
            // 
            this.pStatusLabel.AutoSize = true;
            this.pStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pStatusLabel.ForeColor = System.Drawing.Color.White;
            this.pStatusLabel.Location = new System.Drawing.Point(40, 52);
            this.pStatusLabel.Name = "pStatusLabel";
            this.pStatusLabel.Size = new System.Drawing.Size(51, 16);
            this.pStatusLabel.TabIndex = 12;
            this.pStatusLabel.Text = "Status";
            this.pStatusLabel.UseMnemonic = false;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(394, 2);
            this.axWindowsMediaPlayer1.Margin = new System.Windows.Forms.Padding(0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(165, 35);
            this.axWindowsMediaPlayer1.TabIndex = 4;
            this.axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.axWindowsMediaPlayer1_PlayStateChange);
            this.axWindowsMediaPlayer1.StatusChange += new System.EventHandler(this.axWindowsMediaPlayer1_StatusChange);
            this.axWindowsMediaPlayer1.ErrorEvent += new System.EventHandler(this.axWindowsMediaPlayer1_ErrorEvent);
            this.axWindowsMediaPlayer1.MouseMoveEvent += new AxWMPLib._WMPOCXEvents_MouseMoveEventHandler(this.axWindowsMediaPlayer1_MouseMoveEvent);
            // 
            // pHoverTimer
            // 
            this.pHoverTimer.Interval = 5000;
            this.pHoverTimer.Tag = "Hover timer for WMP control UI flip";
            this.pHoverTimer.Tick += new System.EventHandler(this.pHoverTimer_Tick);
            // 
            // timerCB
            // 
            this.timerCB.Interval = 10000;
            this.timerCB.Tag = "Update channelbox data";
            this.timerCB.Tick += new System.EventHandler(this.timerCB_Tick);
            // 
            // linkServer
            // 
            this.linkServer.AutoSize = true;
            this.linkServer.Enabled = false;
            this.linkServer.Location = new System.Drawing.Point(408, 9);
            this.linkServer.Name = "linkServer";
            this.linkServer.Size = new System.Drawing.Size(100, 13);
            this.linkServer.TabIndex = 18;
            this.linkServer.TabStop = true;
            this.linkServer.Text = "Server is Stopped...";
            this.linkServer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkServer_LinkClicked);
            // 
            // timerTest
            // 
            this.timerTest.Interval = 1800000;
            this.timerTest.Tick += new System.EventHandler(this.timerTest_Tick);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabcontrol1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer2.Panel2.Controls.Add(this.bStart);
            this.splitContainer2.Panel2.Controls.Add(this.bConfigure);
            this.splitContainer2.Panel2.Controls.Add(this.bStop);
            this.splitContainer2.Panel2.Controls.Add(this.linkServer);
            this.splitContainer2.Panel2.Controls.Add(this.lblClock);
            this.splitContainer2.Size = new System.Drawing.Size(560, 265);
            this.splitContainer2.SplitterDistance = 239;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 19;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(560, 349);
            this.Controls.Add(this.splitContainer2);
            this.Controls.Add(this.playerPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(576, 2048);
            this.MinimumSize = new System.Drawing.Size(576, 387);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "XMTuner";
            this.TransparencyKey = System.Drawing.Color.Gainsboro;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.trayIconContextMenu.ResumeLayout(false);
            this.tabcontrol1.ResumeLayout(false);
            this.tLog.ResumeLayout(false);
            this.tChannels.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.channelContextMenu.ResumeLayout(false);
            this.tHistory.ResumeLayout(false);
            this.tAbout.ResumeLayout(false);
            this.tAbout.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pLogoBox)).EndInit();
            this.playerPanel.ResumeLayout(false);
            this.playerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bConfigure;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button bStop;
        private System.Windows.Forms.TabControl tabcontrol1;
        private System.Windows.Forms.TabPage tLog;
        private System.Windows.Forms.TabPage tChannels;
        private System.Windows.Forms.RichTextBox outputbox;
        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.ContextMenuStrip trayIconContextMenu;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitXMTunerToolStripMenuItem;
        private System.Windows.Forms.ComboBox protocolBox;
        private System.Windows.Forms.ToolStripMenuItem viewServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button cpyToClip;
        private System.Windows.Forms.TabPage tAbout;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox aBuildDate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox aVersion;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox bitRateBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button bUpdate;
        private System.Windows.Forms.Timer timerUpdater;
        private System.Windows.Forms.TabPage tHistory;
        private System.Windows.Forms.TextBox txtChannel;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.Label pLabel4;
        private System.Windows.Forms.Label pLabel3;
        private System.Windows.Forms.Label pLabel2;
        private System.Windows.Forms.Label pLabel1;
        private System.Windows.Forms.PictureBox pLogoBox;
        private System.Windows.Forms.Label pLabel5;
        private System.Windows.Forms.Timer pTimer;
        private System.Windows.Forms.Label pLabel6;
        private System.Windows.Forms.Panel playerPanel;
        private System.Windows.Forms.Timer pHoverTimer;
        private System.Windows.Forms.Label pStatusLabel;
        private System.Windows.Forms.Timer timerCB;
        private System.Windows.Forms.ListView channelBox;
        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.LinkLabel linkServer;
        private System.Windows.Forms.Timer timerTest;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ContextMenuStrip channelContextMenu;
        private System.Windows.Forms.ToolStripMenuItem uRLBuilderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disabledToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListView recentlyPlayedBox;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.ToolStripMenuItem allChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoriteChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToFavoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem byCategoryToolStripMenuItem;
    }
}

