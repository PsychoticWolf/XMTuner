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
            System.Windows.Forms.NotifyIcon trayIcon;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            System.Windows.Forms.ContextMenuStrip trayIconContextMenu;
            System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            System.Windows.Forms.ToolStripMenuItem exitXMTunerToolStripMenuItem;
            System.Windows.Forms.ContextMenuStrip channelContextMenu;
            System.Windows.Forms.ToolStripMenuItem playToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem addToFavoritesToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem uRLBuilderToolStripMenuItem;
            System.Windows.Forms.ToolStripMenuItem copyURLToolStripMenuItem;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
            System.Windows.Forms.ContextMenuStrip historyContextMenu;
            System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
            System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
            System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
            System.Windows.Forms.Timer timerUpdater;
            System.Windows.Forms.TabPage tPlayer;
            System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Recently Played", System.Windows.Forms.HorizontalAlignment.Left);
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("Nothing Played Yet...");
            System.Windows.Forms.TabPage tChannels;
            System.Windows.Forms.Panel pChannels;
            System.Windows.Forms.Panel pStatusPanel;
            System.Windows.Forms.Button bManageFavorites;
            System.Windows.Forms.Label pStatusBarLabel;
            System.Windows.Forms.TabPage tFavorites;
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("No Favorites");
            System.Windows.Forms.ColumnHeader columnHeader5;
            System.Windows.Forms.Panel pFavoritesBase;
            System.Windows.Forms.GroupBox groupBox5;
            System.Windows.Forms.GroupBox groupBox4;
            System.Windows.Forms.Label lAddPreNum;
            System.Windows.Forms.GroupBox groupBox3;
            System.Windows.Forms.GroupBox groupBox2;
            System.Windows.Forms.Label lAddFavChanNum;
            System.Windows.Forms.Panel pLogMain;
            System.Windows.Forms.Panel pLogBase;
            System.Windows.Forms.Button bConfigure;
            System.Windows.Forms.TabPage tAbout;
            System.Windows.Forms.Button bUpdate;
            System.Windows.Forms.TextBox aBuildDate;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label13;
            System.Windows.Forms.GroupBox groupBox1;
            System.Windows.Forms.LinkLabel linkLabel1;
            this.allChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.favoriteChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byCategoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.playerSplitContainer = new System.Windows.Forms.SplitContainer();
            this.axWindowsMediaPlayer1 = new AxWMPLib.AxWindowsMediaPlayer();
            this.playerPanel1 = new System.Windows.Forms.Panel();
            this.pStatusLabel = new System.Windows.Forms.Label();
            this.pLabel4 = new System.Windows.Forms.Label();
            this.pLabel5 = new System.Windows.Forms.Label();
            this.pLabel3 = new System.Windows.Forms.Label();
            this.pLabel2 = new System.Windows.Forms.Label();
            this.pLabel6 = new System.Windows.Forms.Label();
            this.txtChannelNum = new System.Windows.Forms.TextBox();
            this.panelPresets = new System.Windows.Forms.Panel();
            this.presetsPanelContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.manageFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.jumpToPreviousChannelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pLogoBox = new System.Windows.Forms.PictureBox();
            this.pLabel1 = new System.Windows.Forms.Label();
            this.recentlyPlayedBox = new System.Windows.Forms.ListView();
            this.channelBox = new System.Windows.Forms.ListView();
            this.favoritesListView = new System.Windows.Forms.ListView();
            this.bRemovePreset = new System.Windows.Forms.Button();
            this.addPresetNum = new System.Windows.Forms.NumericUpDown();
            this.bAddPreset = new System.Windows.Forms.Button();
            this.bRemoveFavorite = new System.Windows.Forms.Button();
            this.addFavChNum = new System.Windows.Forms.NumericUpDown();
            this.bAddFavorite = new System.Windows.Forms.Button();
            this.outputbox = new System.Windows.Forms.RichTextBox();
            this.linkServer = new System.Windows.Forms.LinkLabel();
            this.lblClock = new System.Windows.Forms.Label();
            this.bStart = new System.Windows.Forms.Button();
            this.bStop = new System.Windows.Forms.Button();
            this.aVersion = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pTimer = new System.Windows.Forms.Timer(this.components);
            this.pHoverTimer = new System.Windows.Forms.Timer(this.components);
            this.timerCB = new System.Windows.Forms.Timer(this.components);
            this.pRetryTimer = new System.Windows.Forms.Timer(this.components);
            this.tabcontrol1 = new System.Windows.Forms.TabControl();
            this.tLog = new System.Windows.Forms.TabPage();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            trayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            trayIconContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            exitXMTunerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            channelContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            playToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            addToFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            uRLBuilderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            copyURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            historyContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            timerUpdater = new System.Windows.Forms.Timer(this.components);
            tPlayer = new System.Windows.Forms.TabPage();
            tChannels = new System.Windows.Forms.TabPage();
            pChannels = new System.Windows.Forms.Panel();
            pStatusPanel = new System.Windows.Forms.Panel();
            bManageFavorites = new System.Windows.Forms.Button();
            pStatusBarLabel = new System.Windows.Forms.Label();
            tFavorites = new System.Windows.Forms.TabPage();
            columnHeader5 = new System.Windows.Forms.ColumnHeader();
            pFavoritesBase = new System.Windows.Forms.Panel();
            groupBox5 = new System.Windows.Forms.GroupBox();
            groupBox4 = new System.Windows.Forms.GroupBox();
            lAddPreNum = new System.Windows.Forms.Label();
            groupBox3 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();
            lAddFavChanNum = new System.Windows.Forms.Label();
            pLogMain = new System.Windows.Forms.Panel();
            pLogBase = new System.Windows.Forms.Panel();
            bConfigure = new System.Windows.Forms.Button();
            tAbout = new System.Windows.Forms.TabPage();
            bUpdate = new System.Windows.Forms.Button();
            aBuildDate = new System.Windows.Forms.TextBox();
            label8 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            groupBox1 = new System.Windows.Forms.GroupBox();
            linkLabel1 = new System.Windows.Forms.LinkLabel();
            trayIconContextMenu.SuspendLayout();
            channelContextMenu.SuspendLayout();
            historyContextMenu.SuspendLayout();
            tPlayer.SuspendLayout();
            this.playerSplitContainer.Panel1.SuspendLayout();
            this.playerSplitContainer.Panel2.SuspendLayout();
            this.playerSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).BeginInit();
            this.playerPanel1.SuspendLayout();
            this.presetsPanelContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pLogoBox)).BeginInit();
            tChannels.SuspendLayout();
            pChannels.SuspendLayout();
            pStatusPanel.SuspendLayout();
            tFavorites.SuspendLayout();
            pFavoritesBase.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addPresetNum)).BeginInit();
            groupBox3.SuspendLayout();
            groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addFavChNum)).BeginInit();
            pLogMain.SuspendLayout();
            pLogBase.SuspendLayout();
            tAbout.SuspendLayout();
            groupBox1.SuspendLayout();
            this.tabcontrol1.SuspendLayout();
            this.tLog.SuspendLayout();
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
            // trayIcon
            // 
            trayIcon.ContextMenuStrip = trayIconContextMenu;
            trayIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("trayIcon.Icon")));
            trayIcon.Text = "XMTuner";
            trayIcon.Visible = true;
            trayIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // trayIconContextMenu
            // 
            trayIconContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.viewServerToolStripMenuItem,
            restoreToolStripMenuItem,
            toolStripSeparator1,
            exitXMTunerToolStripMenuItem});
            trayIconContextMenu.Name = "contextMenuStrip1";
            trayIconContextMenu.Size = new System.Drawing.Size(145, 98);
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
            restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            restoreToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            restoreToolStripMenuItem.Text = "Restore";
            restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(141, 6);
            // 
            // exitXMTunerToolStripMenuItem
            // 
            exitXMTunerToolStripMenuItem.Name = "exitXMTunerToolStripMenuItem";
            exitXMTunerToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            exitXMTunerToolStripMenuItem.Text = "Exit XMTuner";
            exitXMTunerToolStripMenuItem.Click += new System.EventHandler(this.exitXMTunerToolStripMenuItem_Click);
            // 
            // channelContextMenu
            // 
            channelContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            playToolStripMenuItem,
            addToFavoritesToolStripMenuItem,
            uRLBuilderToolStripMenuItem,
            copyURLToolStripMenuItem,
            toolStripSeparator2,
            viewToolStripMenuItem});
            channelContextMenu.Name = "contextMenuStrip2";
            channelContextMenu.Size = new System.Drawing.Size(210, 120);
            // 
            // playToolStripMenuItem
            // 
            playToolStripMenuItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            playToolStripMenuItem.Name = "playToolStripMenuItem";
            playToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            playToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            playToolStripMenuItem.Text = "Play";
            playToolStripMenuItem.Click += new System.EventHandler(this.channelBox_DoubleClick);
            // 
            // addToFavoritesToolStripMenuItem
            // 
            addToFavoritesToolStripMenuItem.Name = "addToFavoritesToolStripMenuItem";
            addToFavoritesToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            addToFavoritesToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            addToFavoritesToolStripMenuItem.Text = "Add to Favorites...";
            addToFavoritesToolStripMenuItem.Click += new System.EventHandler(this.addToFavoritesToolStripMenuItem_Click);
            // 
            // uRLBuilderToolStripMenuItem
            // 
            uRLBuilderToolStripMenuItem.Name = "uRLBuilderToolStripMenuItem";
            uRLBuilderToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            uRLBuilderToolStripMenuItem.Text = "Get Channel URL...";
            uRLBuilderToolStripMenuItem.Click += new System.EventHandler(this.uRLBuilderToolStripMenuItem_Click);
            // 
            // copyURLToolStripMenuItem
            // 
            copyURLToolStripMenuItem.Name = "copyURLToolStripMenuItem";
            copyURLToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            copyURLToolStripMenuItem.Text = "Copy URL";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new System.Drawing.Size(206, 6);
            // 
            // historyContextMenu
            // 
            historyContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            copyToolStripMenuItem,
            toolStripSeparator3,
            clearToolStripMenuItem});
            historyContextMenu.Name = "historyContextMenu";
            historyContextMenu.Size = new System.Drawing.Size(102, 54);
            // 
            // copyToolStripMenuItem
            // 
            copyToolStripMenuItem.Font = new System.Drawing.Font("Tahoma", 8.25F);
            copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            copyToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            copyToolStripMenuItem.Text = "Copy";
            copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new System.Drawing.Size(98, 6);
            // 
            // clearToolStripMenuItem
            // 
            clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            clearToolStripMenuItem.Text = "Clear";
            clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // timerUpdater
            // 
            timerUpdater.Enabled = true;
            timerUpdater.Interval = 86400000;
            timerUpdater.Tag = "Updater check";
            timerUpdater.Tick += new System.EventHandler(this.timerUpdater_Tick);
            // 
            // tPlayer
            // 
            tPlayer.BackColor = System.Drawing.Color.Transparent;
            tPlayer.Controls.Add(this.playerSplitContainer);
            tPlayer.Location = new System.Drawing.Point(4, 22);
            tPlayer.Name = "tPlayer";
            tPlayer.Size = new System.Drawing.Size(552, 327);
            tPlayer.TabIndex = 5;
            tPlayer.Text = "Player";
            // 
            // playerSplitContainer
            // 
            this.playerSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.playerSplitContainer.IsSplitterFixed = true;
            this.playerSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.playerSplitContainer.Margin = new System.Windows.Forms.Padding(0);
            this.playerSplitContainer.Name = "playerSplitContainer";
            this.playerSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // playerSplitContainer.Panel1
            // 
            this.playerSplitContainer.Panel1.BackColor = System.Drawing.Color.Black;
            this.playerSplitContainer.Panel1.Controls.Add(this.axWindowsMediaPlayer1);
            this.playerSplitContainer.Panel1.Controls.Add(this.playerPanel1);
            this.playerSplitContainer.Panel1.Controls.Add(this.txtChannelNum);
            this.playerSplitContainer.Panel1.Controls.Add(this.panelPresets);
            this.playerSplitContainer.Panel1.Controls.Add(this.pLogoBox);
            this.playerSplitContainer.Panel1.Controls.Add(this.pLabel1);
            // 
            // playerSplitContainer.Panel2
            // 
            this.playerSplitContainer.Panel2.Controls.Add(this.recentlyPlayedBox);
            this.playerSplitContainer.Size = new System.Drawing.Size(552, 327);
            this.playerSplitContainer.SplitterDistance = 194;
            this.playerSplitContainer.SplitterWidth = 1;
            this.playerSplitContainer.TabIndex = 17;
            // 
            // axWindowsMediaPlayer1
            // 
            this.axWindowsMediaPlayer1.Enabled = true;
            this.axWindowsMediaPlayer1.Location = new System.Drawing.Point(134, 20);
            this.axWindowsMediaPlayer1.Margin = new System.Windows.Forms.Padding(0);
            this.axWindowsMediaPlayer1.Name = "axWindowsMediaPlayer1";
            this.axWindowsMediaPlayer1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWindowsMediaPlayer1.OcxState")));
            this.axWindowsMediaPlayer1.Size = new System.Drawing.Size(284, 68);
            this.axWindowsMediaPlayer1.TabIndex = 4;
            this.axWindowsMediaPlayer1.PlayStateChange += new AxWMPLib._WMPOCXEvents_PlayStateChangeEventHandler(this.axWindowsMediaPlayer1_PlayStateChange);
            this.axWindowsMediaPlayer1.StatusChange += new System.EventHandler(this.axWindowsMediaPlayer1_StatusChange);
            this.axWindowsMediaPlayer1.ErrorEvent += new System.EventHandler(this.axWindowsMediaPlayer1_ErrorEvent);
            this.axWindowsMediaPlayer1.MouseMoveEvent += new AxWMPLib._WMPOCXEvents_MouseMoveEventHandler(this.axWindowsMediaPlayer1_MouseMoveEvent);
            // 
            // playerPanel1
            // 
            this.playerPanel1.Controls.Add(this.pStatusLabel);
            this.playerPanel1.Controls.Add(this.pLabel4);
            this.playerPanel1.Controls.Add(this.pLabel5);
            this.playerPanel1.Controls.Add(this.pLabel3);
            this.playerPanel1.Controls.Add(this.pLabel2);
            this.playerPanel1.Controls.Add(this.pLabel6);
            this.playerPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.playerPanel1.Location = new System.Drawing.Point(0, 96);
            this.playerPanel1.Name = "playerPanel1";
            this.playerPanel1.Size = new System.Drawing.Size(552, 73);
            this.playerPanel1.TabIndex = 13;
            // 
            // pStatusLabel
            // 
            this.pStatusLabel.AutoSize = true;
            this.pStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pStatusLabel.ForeColor = System.Drawing.Color.White;
            this.pStatusLabel.Location = new System.Drawing.Point(30, 28);
            this.pStatusLabel.Name = "pStatusLabel";
            this.pStatusLabel.Size = new System.Drawing.Size(51, 16);
            this.pStatusLabel.TabIndex = 12;
            this.pStatusLabel.Text = "Status";
            this.pStatusLabel.UseMnemonic = false;
            // 
            // pLabel4
            // 
            this.pLabel4.AutoEllipsis = true;
            this.pLabel4.ForeColor = System.Drawing.Color.White;
            this.pLabel4.Location = new System.Drawing.Point(99, 38);
            this.pLabel4.Name = "pLabel4";
            this.pLabel4.Size = new System.Drawing.Size(415, 13);
            this.pLabel4.TabIndex = 9;
            this.pLabel4.Text = "Album:";
            this.pLabel4.UseMnemonic = false;
            // 
            // pLabel5
            // 
            this.pLabel5.AutoSize = true;
            this.pLabel5.ForeColor = System.Drawing.Color.White;
            this.pLabel5.Location = new System.Drawing.Point(3, 57);
            this.pLabel5.Name = "pLabel5";
            this.pLabel5.Size = new System.Drawing.Size(38, 13);
            this.pLabel5.TabIndex = 10;
            this.pLabel5.Text = "Ready";
            this.pLabel5.UseMnemonic = false;
            // 
            // pLabel3
            // 
            this.pLabel3.AutoEllipsis = true;
            this.pLabel3.ForeColor = System.Drawing.Color.White;
            this.pLabel3.Location = new System.Drawing.Point(99, 21);
            this.pLabel3.Name = "pLabel3";
            this.pLabel3.Size = new System.Drawing.Size(415, 13);
            this.pLabel3.TabIndex = 8;
            this.pLabel3.Text = "Artist:";
            this.pLabel3.UseMnemonic = false;
            this.pLabel3.TextChanged += new System.EventHandler(this.pLabel2_TextChanged);
            // 
            // pLabel2
            // 
            this.pLabel2.AutoEllipsis = true;
            this.pLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pLabel2.ForeColor = System.Drawing.Color.White;
            this.pLabel2.Location = new System.Drawing.Point(99, 1);
            this.pLabel2.Name = "pLabel2";
            this.pLabel2.Size = new System.Drawing.Size(415, 16);
            this.pLabel2.TabIndex = 7;
            this.pLabel2.Text = "Title:";
            this.pLabel2.UseMnemonic = false;
            this.pLabel2.TextChanged += new System.EventHandler(this.pLabel2_TextChanged);
            // 
            // pLabel6
            // 
            this.pLabel6.ForeColor = System.Drawing.Color.White;
            this.pLabel6.Location = new System.Drawing.Point(497, 60);
            this.pLabel6.Name = "pLabel6";
            this.pLabel6.Size = new System.Drawing.Size(54, 15);
            this.pLabel6.TabIndex = 11;
            this.pLabel6.Text = "00:00:00";
            this.pLabel6.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.pLabel6.UseMnemonic = false;
            // 
            // txtChannelNum
            // 
            this.txtChannelNum.BackColor = System.Drawing.Color.Black;
            this.txtChannelNum.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtChannelNum.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChannelNum.ForeColor = System.Drawing.Color.White;
            this.txtChannelNum.Location = new System.Drawing.Point(524, 2);
            this.txtChannelNum.Name = "txtChannelNum";
            this.txtChannelNum.Size = new System.Drawing.Size(26, 15);
            this.txtChannelNum.TabIndex = 14;
            this.txtChannelNum.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtChannelNum_KeyPress);
            this.txtChannelNum.Enter += new System.EventHandler(this.txtChannelNum_Enter);
            // 
            // panelPresets
            // 
            this.panelPresets.BackColor = System.Drawing.Color.Transparent;
            this.panelPresets.ContextMenuStrip = this.presetsPanelContextMenu;
            this.panelPresets.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPresets.Location = new System.Drawing.Point(0, 169);
            this.panelPresets.Margin = new System.Windows.Forms.Padding(0);
            this.panelPresets.Name = "panelPresets";
            this.panelPresets.Size = new System.Drawing.Size(552, 25);
            this.panelPresets.TabIndex = 15;
            // 
            // presetsPanelContextMenu
            // 
            this.presetsPanelContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manageFavoritesToolStripMenuItem,
            this.jumpToPreviousChannelToolStripMenuItem});
            this.presetsPanelContextMenu.Name = "presetsPanelContextMenu";
            this.presetsPanelContextMenu.Size = new System.Drawing.Size(213, 48);
            // 
            // manageFavoritesToolStripMenuItem
            // 
            this.manageFavoritesToolStripMenuItem.Name = "manageFavoritesToolStripMenuItem";
            this.manageFavoritesToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.manageFavoritesToolStripMenuItem.Text = "Manage Favorites...";
            // 
            // jumpToPreviousChannelToolStripMenuItem
            // 
            this.jumpToPreviousChannelToolStripMenuItem.Name = "jumpToPreviousChannelToolStripMenuItem";
            this.jumpToPreviousChannelToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            this.jumpToPreviousChannelToolStripMenuItem.Text = "Jump to Previous Channel";
            // 
            // pLogoBox
            // 
            this.pLogoBox.BackColor = System.Drawing.Color.Transparent;
            this.pLogoBox.InitialImage = null;
            this.pLogoBox.Location = new System.Drawing.Point(3, 20);
            this.pLogoBox.Name = "pLogoBox";
            this.pLogoBox.Size = new System.Drawing.Size(128, 50);
            this.pLogoBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pLogoBox.TabIndex = 3;
            this.pLogoBox.TabStop = false;
            // 
            // pLabel1
            // 
            this.pLabel1.AutoEllipsis = true;
            this.pLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pLabel1.ForeColor = System.Drawing.Color.White;
            this.pLabel1.Location = new System.Drawing.Point(3, 0);
            this.pLabel1.Name = "pLabel1";
            this.pLabel1.Size = new System.Drawing.Size(421, 17);
            this.pLabel1.TabIndex = 6;
            this.pLabel1.Text = "Channel:";
            this.pLabel1.UseMnemonic = false;
            // 
            // recentlyPlayedBox
            // 
            this.recentlyPlayedBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.recentlyPlayedBox.ContextMenuStrip = historyContextMenu;
            this.recentlyPlayedBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recentlyPlayedBox.FullRowSelect = true;
            listViewGroup1.Header = "Recently Played";
            listViewGroup1.Name = "listViewGroup1";
            this.recentlyPlayedBox.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
            listViewGroup1});
            this.recentlyPlayedBox.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            listViewItem1.Group = listViewGroup1;
            this.recentlyPlayedBox.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.recentlyPlayedBox.Location = new System.Drawing.Point(0, 0);
            this.recentlyPlayedBox.Margin = new System.Windows.Forms.Padding(0);
            this.recentlyPlayedBox.Name = "recentlyPlayedBox";
            this.recentlyPlayedBox.Size = new System.Drawing.Size(552, 132);
            this.recentlyPlayedBox.TabIndex = 16;
            this.recentlyPlayedBox.TileSize = new System.Drawing.Size(507, 30);
            this.recentlyPlayedBox.UseCompatibleStateImageBehavior = false;
            this.recentlyPlayedBox.View = System.Windows.Forms.View.Tile;
            // 
            // tChannels
            // 
            tChannels.BackColor = System.Drawing.Color.Transparent;
            tChannels.Controls.Add(pChannels);
            tChannels.Controls.Add(pStatusPanel);
            tChannels.Location = new System.Drawing.Point(4, 22);
            tChannels.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
            tChannels.Name = "tChannels";
            tChannels.Size = new System.Drawing.Size(552, 327);
            tChannels.TabIndex = 1;
            tChannels.Text = "Channels";
            tChannels.UseVisualStyleBackColor = true;
            // 
            // pChannels
            // 
            pChannels.BackColor = System.Drawing.Color.Transparent;
            pChannels.Controls.Add(this.channelBox);
            pChannels.Dock = System.Windows.Forms.DockStyle.Fill;
            pChannels.Location = new System.Drawing.Point(0, 0);
            pChannels.Name = "pChannels";
            pChannels.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            pChannels.Size = new System.Drawing.Size(552, 299);
            pChannels.TabIndex = 13;
            // 
            // channelBox
            // 
            this.channelBox.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.channelBox.ContextMenuStrip = channelContextMenu;
            this.channelBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.channelBox.FullRowSelect = true;
            this.channelBox.Location = new System.Drawing.Point(0, 1);
            this.channelBox.Margin = new System.Windows.Forms.Padding(0);
            this.channelBox.MultiSelect = false;
            this.channelBox.Name = "channelBox";
            this.channelBox.ShowItemToolTips = true;
            this.channelBox.Size = new System.Drawing.Size(552, 298);
            this.channelBox.TabIndex = 11;
            this.channelBox.TileSize = new System.Drawing.Size(515, 30);
            this.channelBox.UseCompatibleStateImageBehavior = false;
            this.channelBox.View = System.Windows.Forms.View.Tile;
            this.channelBox.DoubleClick += new System.EventHandler(this.channelBox_DoubleClick);
            this.channelBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.channelBox_KeyPress);
            // 
            // pStatusPanel
            // 
            pStatusPanel.BackColor = System.Drawing.Color.Black;
            pStatusPanel.Controls.Add(bManageFavorites);
            pStatusPanel.Controls.Add(pStatusBarLabel);
            pStatusPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            pStatusPanel.Location = new System.Drawing.Point(0, 299);
            pStatusPanel.Name = "pStatusPanel";
            pStatusPanel.Size = new System.Drawing.Size(552, 28);
            pStatusPanel.TabIndex = 12;
            // 
            // bManageFavorites
            // 
            bManageFavorites.Location = new System.Drawing.Point(430, 2);
            bManageFavorites.Name = "bManageFavorites";
            bManageFavorites.Size = new System.Drawing.Size(111, 23);
            bManageFavorites.TabIndex = 1;
            bManageFavorites.Text = "Manage Favorites...";
            bManageFavorites.UseVisualStyleBackColor = true;
            // 
            // pStatusBarLabel
            // 
            pStatusBarLabel.AutoSize = true;
            pStatusBarLabel.ForeColor = System.Drawing.Color.White;
            pStatusBarLabel.Location = new System.Drawing.Point(6, 7);
            pStatusBarLabel.Name = "pStatusBarLabel";
            pStatusBarLabel.Size = new System.Drawing.Size(219, 13);
            pStatusBarLabel.TabIndex = 0;
            pStatusBarLabel.Text = "This space will house the Player Status Bar...";
            // 
            // tFavorites
            // 
            tFavorites.Controls.Add(this.favoritesListView);
            tFavorites.Controls.Add(pFavoritesBase);
            tFavorites.Location = new System.Drawing.Point(4, 22);
            tFavorites.Name = "tFavorites";
            tFavorites.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            tFavorites.Size = new System.Drawing.Size(552, 327);
            tFavorites.TabIndex = 4;
            tFavorites.Text = "Favorites";
            tFavorites.UseVisualStyleBackColor = true;
            // 
            // favoritesListView
            // 
            this.favoritesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            columnHeader5});
            this.favoritesListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.favoritesListView.ForeColor = System.Drawing.Color.Black;
            this.favoritesListView.FullRowSelect = true;
            listViewItem2.StateImageIndex = 0;
            this.favoritesListView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.favoritesListView.Location = new System.Drawing.Point(0, 1);
            this.favoritesListView.Margin = new System.Windows.Forms.Padding(0);
            this.favoritesListView.Name = "favoritesListView";
            this.favoritesListView.Size = new System.Drawing.Size(552, 248);
            this.favoritesListView.TabIndex = 0;
            this.favoritesListView.UseCompatibleStateImageBehavior = false;
            this.favoritesListView.View = System.Windows.Forms.View.Details;
            this.favoritesListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.favoritesListView_ItemSelectionChanged);
            // 
            // columnHeader5
            // 
            columnHeader5.Text = "Favorite Channels";
            columnHeader5.Width = 450;
            // 
            // pFavoritesBase
            // 
            pFavoritesBase.BackColor = System.Drawing.SystemColors.Control;
            pFavoritesBase.Controls.Add(groupBox5);
            pFavoritesBase.Controls.Add(groupBox4);
            pFavoritesBase.Controls.Add(groupBox3);
            pFavoritesBase.Controls.Add(groupBox2);
            pFavoritesBase.Dock = System.Windows.Forms.DockStyle.Bottom;
            pFavoritesBase.Location = new System.Drawing.Point(0, 249);
            pFavoritesBase.Margin = new System.Windows.Forms.Padding(0);
            pFavoritesBase.Name = "pFavoritesBase";
            pFavoritesBase.Size = new System.Drawing.Size(552, 78);
            pFavoritesBase.TabIndex = 2;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(this.bRemovePreset);
            groupBox5.Location = new System.Drawing.Point(423, 5);
            groupBox5.Margin = new System.Windows.Forms.Padding(0);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new System.Drawing.Size(125, 70);
            groupBox5.TabIndex = 3;
            groupBox5.TabStop = false;
            groupBox5.Text = "Remove from Presets";
            // 
            // bRemovePreset
            // 
            this.bRemovePreset.Enabled = false;
            this.bRemovePreset.Location = new System.Drawing.Point(12, 24);
            this.bRemovePreset.Name = "bRemovePreset";
            this.bRemovePreset.Size = new System.Drawing.Size(100, 23);
            this.bRemovePreset.TabIndex = 1;
            this.bRemovePreset.Text = "Remove Preset";
            this.bRemovePreset.UseVisualStyleBackColor = true;
            this.bRemovePreset.Click += new System.EventHandler(this.bRemovePreset_Click);
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(lAddPreNum);
            groupBox4.Controls.Add(this.addPresetNum);
            groupBox4.Controls.Add(this.bAddPreset);
            groupBox4.Location = new System.Drawing.Point(281, 5);
            groupBox4.Margin = new System.Windows.Forms.Padding(0);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new System.Drawing.Size(140, 70);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "Add to Presets";
            // 
            // lAddPreNum
            // 
            lAddPreNum.AutoSize = true;
            lAddPreNum.Enabled = false;
            lAddPreNum.Location = new System.Drawing.Point(10, 18);
            lAddPreNum.Name = "lAddPreNum";
            lAddPreNum.Size = new System.Drawing.Size(80, 13);
            lAddPreNum.TabIndex = 2;
            lAddPreNum.Text = "Preset Number:";
            // 
            // addPresetNum
            // 
            this.addPresetNum.Enabled = false;
            this.addPresetNum.Location = new System.Drawing.Point(90, 14);
            this.addPresetNum.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.addPresetNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.addPresetNum.Name = "addPresetNum";
            this.addPresetNum.Size = new System.Drawing.Size(40, 20);
            this.addPresetNum.TabIndex = 1;
            this.addPresetNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bAddPreset
            // 
            this.bAddPreset.Enabled = false;
            this.bAddPreset.Location = new System.Drawing.Point(22, 36);
            this.bAddPreset.Name = "bAddPreset";
            this.bAddPreset.Size = new System.Drawing.Size(100, 23);
            this.bAddPreset.TabIndex = 0;
            this.bAddPreset.Text = "Add Preset";
            this.bAddPreset.UseVisualStyleBackColor = true;
            this.bAddPreset.Click += new System.EventHandler(this.bAddPreset_Click);
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(this.bRemoveFavorite);
            groupBox3.Location = new System.Drawing.Point(149, 5);
            groupBox3.Margin = new System.Windows.Forms.Padding(0);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new System.Drawing.Size(130, 70);
            groupBox3.TabIndex = 1;
            groupBox3.TabStop = false;
            groupBox3.Text = "Remove from Favorites";
            // 
            // bRemoveFavorite
            // 
            this.bRemoveFavorite.Enabled = false;
            this.bRemoveFavorite.Location = new System.Drawing.Point(15, 24);
            this.bRemoveFavorite.Name = "bRemoveFavorite";
            this.bRemoveFavorite.Size = new System.Drawing.Size(100, 23);
            this.bRemoveFavorite.TabIndex = 0;
            this.bRemoveFavorite.Text = "Remove Favorite";
            this.bRemoveFavorite.UseVisualStyleBackColor = true;
            this.bRemoveFavorite.Click += new System.EventHandler(this.bRemoveFavorite_Click);
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(lAddFavChanNum);
            groupBox2.Controls.Add(this.addFavChNum);
            groupBox2.Controls.Add(this.bAddFavorite);
            groupBox2.Location = new System.Drawing.Point(2, 5);
            groupBox2.Margin = new System.Windows.Forms.Padding(0);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new System.Drawing.Size(145, 70);
            groupBox2.TabIndex = 0;
            groupBox2.TabStop = false;
            groupBox2.Text = "Add to Favorites";
            // 
            // lAddFavChanNum
            // 
            lAddFavChanNum.AutoSize = true;
            lAddFavChanNum.Location = new System.Drawing.Point(7, 18);
            lAddFavChanNum.Name = "lAddFavChanNum";
            lAddFavChanNum.Size = new System.Drawing.Size(89, 13);
            lAddFavChanNum.TabIndex = 2;
            lAddFavChanNum.Text = "Channel Number:";
            // 
            // addFavChNum
            // 
            this.addFavChNum.Enabled = false;
            this.addFavChNum.Location = new System.Drawing.Point(96, 14);
            this.addFavChNum.Maximum = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.addFavChNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.addFavChNum.Name = "addFavChNum";
            this.addFavChNum.Size = new System.Drawing.Size(42, 20);
            this.addFavChNum.TabIndex = 1;
            this.addFavChNum.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // bAddFavorite
            // 
            this.bAddFavorite.Enabled = false;
            this.bAddFavorite.Location = new System.Drawing.Point(27, 36);
            this.bAddFavorite.Name = "bAddFavorite";
            this.bAddFavorite.Size = new System.Drawing.Size(90, 23);
            this.bAddFavorite.TabIndex = 0;
            this.bAddFavorite.Text = "Add Favorite";
            this.bAddFavorite.UseVisualStyleBackColor = true;
            this.bAddFavorite.Click += new System.EventHandler(this.bAddFavorite_Click);
            // 
            // pLogMain
            // 
            pLogMain.BackColor = System.Drawing.Color.Transparent;
            pLogMain.Controls.Add(this.outputbox);
            pLogMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pLogMain.Location = new System.Drawing.Point(0, 1);
            pLogMain.Margin = new System.Windows.Forms.Padding(0);
            pLogMain.Name = "pLogMain";
            pLogMain.Size = new System.Drawing.Size(552, 295);
            pLogMain.TabIndex = 25;
            // 
            // outputbox
            // 
            this.outputbox.BackColor = System.Drawing.Color.White;
            this.outputbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputbox.Cursor = System.Windows.Forms.Cursors.Default;
            this.outputbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.outputbox.Location = new System.Drawing.Point(0, 0);
            this.outputbox.Name = "outputbox";
            this.outputbox.ReadOnly = true;
            this.outputbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.outputbox.Size = new System.Drawing.Size(552, 295);
            this.outputbox.TabIndex = 6;
            this.outputbox.Text = "";
            this.outputbox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.Link_Clicked);
            this.outputbox.Layout += new System.Windows.Forms.LayoutEventHandler(this.outputbox_Layout);
            this.outputbox.TextChanged += new System.EventHandler(this.outputbox_TextChanged_1);
            // 
            // pLogBase
            // 
            pLogBase.BackColor = System.Drawing.Color.PeachPuff;
            pLogBase.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            pLogBase.Controls.Add(this.linkServer);
            pLogBase.Controls.Add(this.lblClock);
            pLogBase.Controls.Add(this.bStart);
            pLogBase.Controls.Add(bConfigure);
            pLogBase.Controls.Add(this.bStop);
            pLogBase.Dock = System.Windows.Forms.DockStyle.Bottom;
            pLogBase.Location = new System.Drawing.Point(0, 296);
            pLogBase.Margin = new System.Windows.Forms.Padding(0);
            pLogBase.Name = "pLogBase";
            pLogBase.Size = new System.Drawing.Size(552, 31);
            pLogBase.TabIndex = 24;
            // 
            // linkServer
            // 
            this.linkServer.AutoSize = true;
            this.linkServer.Enabled = false;
            this.linkServer.Location = new System.Drawing.Point(388, 10);
            this.linkServer.Name = "linkServer";
            this.linkServer.Size = new System.Drawing.Size(100, 13);
            this.linkServer.TabIndex = 23;
            this.linkServer.TabStop = true;
            this.linkServer.Text = "Server is Stopped...";
            this.linkServer.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkServer_LinkClicked);
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = true;
            this.lblClock.Location = new System.Drawing.Point(488, 10);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(49, 13);
            this.lblClock.TabIndex = 21;
            this.lblClock.Text = "00:00:00";
            // 
            // bStart
            // 
            this.bStart.Location = new System.Drawing.Point(5, 4);
            this.bStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(60, 23);
            this.bStart.TabIndex = 19;
            this.bStart.Text = "Start";
            this.bStart.UseVisualStyleBackColor = true;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // bConfigure
            // 
            bConfigure.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            bConfigure.Location = new System.Drawing.Point(134, 4);
            bConfigure.Margin = new System.Windows.Forms.Padding(4);
            bConfigure.Name = "bConfigure";
            bConfigure.Size = new System.Drawing.Size(60, 23);
            bConfigure.TabIndex = 20;
            bConfigure.Text = "Configure";
            bConfigure.UseVisualStyleBackColor = true;
            bConfigure.Click += new System.EventHandler(this.bConfigure_Click);
            // 
            // bStop
            // 
            this.bStop.Enabled = false;
            this.bStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bStop.Location = new System.Drawing.Point(69, 4);
            this.bStop.Margin = new System.Windows.Forms.Padding(2);
            this.bStop.Name = "bStop";
            this.bStop.Size = new System.Drawing.Size(60, 23);
            this.bStop.TabIndex = 22;
            this.bStop.Text = "Stop";
            this.bStop.UseVisualStyleBackColor = true;
            this.bStop.Click += new System.EventHandler(this.bStop_Click);
            // 
            // tAbout
            // 
            tAbout.BackColor = System.Drawing.SystemColors.Control;
            tAbout.Controls.Add(bUpdate);
            tAbout.Controls.Add(aBuildDate);
            tAbout.Controls.Add(label8);
            tAbout.Controls.Add(label9);
            tAbout.Controls.Add(label10);
            tAbout.Controls.Add(label11);
            tAbout.Controls.Add(label12);
            tAbout.Controls.Add(this.aVersion);
            tAbout.Controls.Add(label13);
            tAbout.Controls.Add(groupBox1);
            tAbout.Location = new System.Drawing.Point(4, 22);
            tAbout.Margin = new System.Windows.Forms.Padding(2);
            tAbout.Name = "tAbout";
            tAbout.Padding = new System.Windows.Forms.Padding(2);
            tAbout.Size = new System.Drawing.Size(552, 327);
            tAbout.TabIndex = 2;
            tAbout.Text = "About";
            // 
            // bUpdate
            // 
            bUpdate.Location = new System.Drawing.Point(415, 181);
            bUpdate.Name = "bUpdate";
            bUpdate.Size = new System.Drawing.Size(115, 23);
            bUpdate.TabIndex = 17;
            bUpdate.Text = "Check for Updates...";
            bUpdate.UseVisualStyleBackColor = true;
            bUpdate.Click += new System.EventHandler(this.bUpdate_Click);
            // 
            // aBuildDate
            // 
            aBuildDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            aBuildDate.ForeColor = System.Drawing.Color.Black;
            aBuildDate.Location = new System.Drawing.Point(365, 73);
            aBuildDate.Name = "aBuildDate";
            aBuildDate.ReadOnly = true;
            aBuildDate.Size = new System.Drawing.Size(62, 19);
            aBuildDate.TabIndex = 17;
            aBuildDate.Text = "20110211";
            aBuildDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(300, 77);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(59, 13);
            label8.TabIndex = 16;
            label8.Text = "Build Date:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(140, 111);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(88, 13);
            label9.TabIndex = 15;
            label9.Text = "Curtis M. Kularski";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(140, 94);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(62, 13);
            label10.TabIndex = 14;
            label10.Text = "Chris Crews";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label11.Location = new System.Drawing.Point(122, 77);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(75, 13);
            label11.TabIndex = 13;
            label11.Text = "Developers:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            label12.Location = new System.Drawing.Point(120, 35);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(132, 31);
            label12.TabIndex = 12;
            label12.Text = "XMTuner";
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
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(311, 47);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(48, 13);
            label13.TabIndex = 10;
            label13.Text = "Version: ";
            // 
            // groupBox1
            // 
            groupBox1.BackColor = System.Drawing.SystemColors.Control;
            groupBox1.Controls.Add(linkLabel1);
            groupBox1.Location = new System.Drawing.Point(109, 22);
            groupBox1.Margin = new System.Windows.Forms.Padding(2);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new System.Windows.Forms.Padding(2);
            groupBox1.Size = new System.Drawing.Size(330, 154);
            groupBox1.TabIndex = 19;
            groupBox1.TabStop = false;
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new System.Drawing.Point(87, 139);
            linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new System.Drawing.Size(125, 13);
            linkLabel1.TabIndex = 18;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "http://www.xmtuner.net/";
            linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tag = "Server Uptime Counter";
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pTimer
            // 
            this.pTimer.Interval = 1000;
            this.pTimer.Tag = "Player timer for WMP counter";
            this.pTimer.Tick += new System.EventHandler(this.pTimer_Tick);
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
            // pRetryTimer
            // 
            this.pRetryTimer.Interval = 10000;
            this.pRetryTimer.Tick += new System.EventHandler(this.pRetryTimer_Tick);
            // 
            // tabcontrol1
            // 
            this.tabcontrol1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.tabcontrol1.Controls.Add(tPlayer);
            this.tabcontrol1.Controls.Add(tChannels);
            this.tabcontrol1.Controls.Add(tFavorites);
            this.tabcontrol1.Controls.Add(this.tLog);
            this.tabcontrol1.Controls.Add(tAbout);
            this.tabcontrol1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabcontrol1.HotTrack = true;
            this.tabcontrol1.Location = new System.Drawing.Point(0, 0);
            this.tabcontrol1.Name = "tabcontrol1";
            this.tabcontrol1.Padding = new System.Drawing.Point(7, 3);
            this.tabcontrol1.SelectedIndex = 0;
            this.tabcontrol1.Size = new System.Drawing.Size(560, 353);
            this.tabcontrol1.TabIndex = 16;
            this.tabcontrol1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabcontrol1_Selected);
            // 
            // tLog
            // 
            this.tLog.BackColor = System.Drawing.Color.Transparent;
            this.tLog.Controls.Add(pLogMain);
            this.tLog.Controls.Add(pLogBase);
            this.tLog.Location = new System.Drawing.Point(4, 22);
            this.tLog.Margin = new System.Windows.Forms.Padding(0);
            this.tLog.Name = "tLog";
            this.tLog.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
            this.tLog.Size = new System.Drawing.Size(552, 327);
            this.tLog.TabIndex = 0;
            this.tLog.Text = "Log";
            this.tLog.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(560, 353);
            this.Controls.Add(this.tabcontrol1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(576, 2048);
            this.MinimumSize = new System.Drawing.Size(576, 387);
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "XMTuner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            trayIconContextMenu.ResumeLayout(false);
            channelContextMenu.ResumeLayout(false);
            historyContextMenu.ResumeLayout(false);
            tPlayer.ResumeLayout(false);
            this.playerSplitContainer.Panel1.ResumeLayout(false);
            this.playerSplitContainer.Panel1.PerformLayout();
            this.playerSplitContainer.Panel2.ResumeLayout(false);
            this.playerSplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.axWindowsMediaPlayer1)).EndInit();
            this.playerPanel1.ResumeLayout(false);
            this.playerPanel1.PerformLayout();
            this.presetsPanelContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pLogoBox)).EndInit();
            tChannels.ResumeLayout(false);
            pChannels.ResumeLayout(false);
            pStatusPanel.ResumeLayout(false);
            pStatusPanel.PerformLayout();
            tFavorites.ResumeLayout(false);
            pFavoritesBase.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addPresetNum)).EndInit();
            groupBox3.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.addFavChNum)).EndInit();
            pLogMain.ResumeLayout(false);
            pLogBase.ResumeLayout(false);
            pLogBase.PerformLayout();
            tAbout.ResumeLayout(false);
            tAbout.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            this.tabcontrol1.ResumeLayout(false);
            this.tLog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pLogoBox;
        private System.Windows.Forms.Label pLabel2;
        private System.Windows.Forms.Label pLabel6;
        private System.Windows.Forms.Label pLabel3;
        private System.Windows.Forms.Label pLabel5;
        private System.Windows.Forms.Label pLabel1;
        private System.Windows.Forms.Label pLabel4;
        private System.Windows.Forms.Label pStatusLabel;
        private System.Windows.Forms.TextBox aVersion;
        private System.Windows.Forms.LinkLabel linkServer;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bStop;
        private AxWMPLib.AxWindowsMediaPlayer axWindowsMediaPlayer1;
        private System.Windows.Forms.ListView channelBox;
        private System.Windows.Forms.Timer pTimer;
        private System.Windows.Forms.Timer pHoverTimer;
        private System.Windows.Forms.Timer pRetryTimer;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TabControl tabcontrol1;
        private System.Windows.Forms.Timer timerCB;
        private System.Windows.Forms.TabPage tLog;
        private System.Windows.Forms.ToolStripMenuItem allChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem favoriteChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byCategoryToolStripMenuItem;
        public System.Windows.Forms.RichTextBox outputbox;
        private System.Windows.Forms.Panel playerPanel1;
        private System.Windows.Forms.TextBox txtChannelNum;
        private System.Windows.Forms.Panel panelPresets;
        private System.Windows.Forms.ListView recentlyPlayedBox;
        private System.Windows.Forms.SplitContainer playerSplitContainer;
        private System.Windows.Forms.ContextMenuStrip presetsPanelContextMenu;
        private System.Windows.Forms.ToolStripMenuItem manageFavoritesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem jumpToPreviousChannelToolStripMenuItem;
        private System.Windows.Forms.ListView favoritesListView;
        private System.Windows.Forms.Button bRemoveFavorite;
        private System.Windows.Forms.Button bRemovePreset;
        private System.Windows.Forms.Button bAddPreset;
        private System.Windows.Forms.Button bAddFavorite;
        private System.Windows.Forms.NumericUpDown addFavChNum;
        private System.Windows.Forms.NumericUpDown addPresetNum;


    }
}

