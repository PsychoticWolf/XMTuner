﻿namespace XMReaderConsole
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.button1 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblClock = new System.Windows.Forms.Label();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitXMTunerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button5 = new System.Windows.Forms.Button();
            this.tabcontrol1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.outputbox = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.bitRateBox = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cpyToClip = new System.Windows.Forms.Button();
            this.protocolBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.channelBox = new System.Windows.Forms.ComboBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblServiceInst = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.btnSerUninstall = new System.Windows.Forms.Button();
            this.btnSerInstall = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblServiceStat = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSerRestart = new System.Windows.Forms.Button();
            this.btnSerStop = new System.Windows.Forms.Button();
            this.btnSerStart = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.tabcontrol1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(17, 299);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 28);
            this.button1.TabIndex = 3;
            this.button1.Text = "Login";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(107, 299);
            this.button4.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(80, 28);
            this.button4.TabIndex = 10;
            this.button4.Text = "Configure";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblClock
            // 
            this.lblClock.AutoSize = true;
            this.lblClock.Location = new System.Drawing.Point(668, 306);
            this.lblClock.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClock.Name = "lblClock";
            this.lblClock.Size = new System.Drawing.Size(0, 17);
            this.lblClock.TabIndex = 12;
            // 
            // timer2
            // 
            this.timer2.Interval = 60000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "XMTuner";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
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
            this.contextMenuStrip1.Size = new System.Drawing.Size(166, 130);
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.Enabled = false;
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.loginToolStripMenuItem.Text = "Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.button1_Click);
            // 
            // viewServerToolStripMenuItem
            // 
            this.viewServerToolStripMenuItem.Enabled = false;
            this.viewServerToolStripMenuItem.Name = "viewServerToolStripMenuItem";
            this.viewServerToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.viewServerToolStripMenuItem.Text = "What\'s On...";
            this.viewServerToolStripMenuItem.Click += new System.EventHandler(this.viewServerToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            // 
            // restoreToolStripMenuItem
            // 
            this.restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            this.restoreToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.restoreToolStripMenuItem.Text = "Restore";
            this.restoreToolStripMenuItem.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(162, 6);
            // 
            // exitXMTunerToolStripMenuItem
            // 
            this.exitXMTunerToolStripMenuItem.Name = "exitXMTunerToolStripMenuItem";
            this.exitXMTunerToolStripMenuItem.Size = new System.Drawing.Size(165, 24);
            this.exitXMTunerToolStripMenuItem.Text = "Exit XMTuner";
            this.exitXMTunerToolStripMenuItem.Click += new System.EventHandler(this.exitXMTunerToolStripMenuItem_Click);
            // 
            // button5
            // 
            this.button5.Enabled = false;
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button5.Location = new System.Drawing.Point(195, 299);
            this.button5.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(95, 28);
            this.button5.TabIndex = 15;
            this.button5.Text = "Stop Server";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // tabcontrol1
            // 
            this.tabcontrol1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.tabcontrol1.Controls.Add(this.tabPage1);
            this.tabcontrol1.Controls.Add(this.tabPage2);
            this.tabcontrol1.Controls.Add(this.tabPage4);
            this.tabcontrol1.Controls.Add(this.tabPage3);
            this.tabcontrol1.HotTrack = true;
            this.tabcontrol1.Location = new System.Drawing.Point(13, 15);
            this.tabcontrol1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabcontrol1.Name = "tabcontrol1";
            this.tabcontrol1.SelectedIndex = 0;
            this.tabcontrol1.Size = new System.Drawing.Size(717, 283);
            this.tabcontrol1.TabIndex = 16;
            this.tabcontrol1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabcontrol1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.outputbox);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(709, 254);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            // 
            // outputbox
            // 
            this.outputbox.BackColor = System.Drawing.Color.White;
            this.outputbox.Cursor = System.Windows.Forms.Cursors.Default;
            this.outputbox.Location = new System.Drawing.Point(5, 6);
            this.outputbox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.outputbox.Name = "outputbox";
            this.outputbox.ReadOnly = true;
            this.outputbox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.outputbox.Size = new System.Drawing.Size(697, 242);
            this.outputbox.TabIndex = 6;
            this.outputbox.Text = "";
            this.outputbox.WordWrap = false;
            this.outputbox.Layout += new System.Windows.Forms.LayoutEventHandler(this.outputbox_Layout);
            this.outputbox.TextChanged += new System.EventHandler(this.outputbox_TextChanged_1);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.bitRateBox);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.cpyToClip);
            this.tabPage2.Controls.Add(this.protocolBox);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.addressBox);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.channelBox);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage2.Size = new System.Drawing.Size(709, 254);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Channels";
            // 
            // bitRateBox
            // 
            this.bitRateBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.bitRateBox.FormattingEnabled = true;
            this.bitRateBox.Items.AddRange(new object[] {
            "High",
            "Low"});
            this.bitRateBox.Location = new System.Drawing.Point(403, 66);
            this.bitRateBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.bitRateBox.Name = "bitRateBox";
            this.bitRateBox.Size = new System.Drawing.Size(288, 24);
            this.bitRateBox.TabIndex = 10;
            this.bitRateBox.SelectedIndexChanged += new System.EventHandler(this.makeAddress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(343, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 17);
            this.label7.TabIndex = 9;
            this.label7.Text = "Bitrate:";
            // 
            // cpyToClip
            // 
            this.cpyToClip.Location = new System.Drawing.Point(552, 185);
            this.cpyToClip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cpyToClip.Name = "cpyToClip";
            this.cpyToClip.Size = new System.Drawing.Size(139, 28);
            this.cpyToClip.TabIndex = 8;
            this.cpyToClip.Text = "Copy to Clipboard";
            this.cpyToClip.UseVisualStyleBackColor = true;
            this.cpyToClip.Click += new System.EventHandler(this.cpyToClip_Click);
            // 
            // protocolBox
            // 
            this.protocolBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protocolBox.FormattingEnabled = true;
            this.protocolBox.Items.AddRange(new object[] {
            "HTTP",
            "MMS"});
            this.protocolBox.Location = new System.Drawing.Point(89, 66);
            this.protocolBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.protocolBox.Name = "protocolBox";
            this.protocolBox.Size = new System.Drawing.Size(225, 24);
            this.protocolBox.TabIndex = 5;
            this.protocolBox.SelectedIndexChanged += new System.EventHandler(this.makeAddress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 70);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "Protocol:";
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(89, 103);
            this.addressBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(601, 22);
            this.addressBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 106);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Address:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 17);
            this.label3.TabIndex = 1;
            this.label3.Text = "Channel:";
            // 
            // channelBox
            // 
            this.channelBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.channelBox.Enabled = false;
            this.channelBox.FormattingEnabled = true;
            this.channelBox.Location = new System.Drawing.Point(89, 30);
            this.channelBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.channelBox.Name = "channelBox";
            this.channelBox.Size = new System.Drawing.Size(601, 24);
            this.channelBox.TabIndex = 0;
            this.channelBox.SelectionChangeCommitted += new System.EventHandler(this.makeAddress);
            this.channelBox.SelectedIndexChanged += new System.EventHandler(this.channelBox_SelectedIndexChanged);
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.groupBox3);
            this.tabPage4.Controls.Add(this.groupBox2);
            this.tabPage4.Location = new System.Drawing.Point(4, 25);
            this.tabPage4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage4.Size = new System.Drawing.Size(709, 254);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Service";
            this.tabPage4.Click += new System.EventHandler(this.tabPage4_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblServiceInst);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.btnSerUninstall);
            this.groupBox3.Controls.Add(this.btnSerInstall);
            this.groupBox3.Location = new System.Drawing.Point(9, 140);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox3.Size = new System.Drawing.Size(343, 103);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Installation";
            // 
            // lblServiceInst
            // 
            this.lblServiceInst.AutoSize = true;
            this.lblServiceInst.Location = new System.Drawing.Point(75, 33);
            this.lblServiceInst.Name = "lblServiceInst";
            this.lblServiceInst.Size = new System.Drawing.Size(81, 17);
            this.lblServiceInst.TabIndex = 3;
            this.lblServiceInst.Text = "UNKNOWN";
            this.lblServiceInst.Click += new System.EventHandler(this.label15_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(8, 33);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(69, 17);
            this.label14.TabIndex = 2;
            this.label14.Text = "Service is";
            // 
            // btnSerUninstall
            // 
            this.btnSerUninstall.Location = new System.Drawing.Point(117, 68);
            this.btnSerUninstall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSerUninstall.Name = "btnSerUninstall";
            this.btnSerUninstall.Size = new System.Drawing.Size(100, 28);
            this.btnSerUninstall.TabIndex = 1;
            this.btnSerUninstall.Text = "Uninstall";
            this.btnSerUninstall.UseVisualStyleBackColor = true;
            this.btnSerUninstall.Click += new System.EventHandler(this.button8_Click);
            // 
            // btnSerInstall
            // 
            this.btnSerInstall.Location = new System.Drawing.Point(8, 68);
            this.btnSerInstall.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSerInstall.Name = "btnSerInstall";
            this.btnSerInstall.Size = new System.Drawing.Size(100, 28);
            this.btnSerInstall.TabIndex = 0;
            this.btnSerInstall.Text = "Install";
            this.btnSerInstall.UseVisualStyleBackColor = true;
            this.btnSerInstall.Click += new System.EventHandler(this.btnSerInstall_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblServiceStat);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnSerRestart);
            this.groupBox2.Controls.Add(this.btnSerStop);
            this.groupBox2.Controls.Add(this.btnSerStart);
            this.groupBox2.Location = new System.Drawing.Point(9, 9);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox2.Size = new System.Drawing.Size(343, 123);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Service Control";
            // 
            // lblServiceStat
            // 
            this.lblServiceStat.AutoSize = true;
            this.lblServiceStat.Location = new System.Drawing.Point(69, 41);
            this.lblServiceStat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblServiceStat.Name = "lblServiceStat";
            this.lblServiceStat.Size = new System.Drawing.Size(66, 17);
            this.lblServiceStat.TabIndex = 4;
            this.lblServiceStat.Text = "Unknown";
            this.lblServiceStat.Click += new System.EventHandler(this.label2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 41);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Status:";
            // 
            // btnSerRestart
            // 
            this.btnSerRestart.Location = new System.Drawing.Point(227, 87);
            this.btnSerRestart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSerRestart.Name = "btnSerRestart";
            this.btnSerRestart.Size = new System.Drawing.Size(100, 28);
            this.btnSerRestart.TabIndex = 2;
            this.btnSerRestart.Text = "Restart";
            this.btnSerRestart.UseVisualStyleBackColor = true;
            this.btnSerRestart.Click += new System.EventHandler(this.btnSerRestart_Click);
            // 
            // btnSerStop
            // 
            this.btnSerStop.Location = new System.Drawing.Point(117, 87);
            this.btnSerStop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSerStop.Name = "btnSerStop";
            this.btnSerStop.Size = new System.Drawing.Size(100, 28);
            this.btnSerStop.TabIndex = 1;
            this.btnSerStop.Text = "Stop";
            this.btnSerStop.UseVisualStyleBackColor = true;
            this.btnSerStop.Click += new System.EventHandler(this.button3_Click_1);
            // 
            // btnSerStart
            // 
            this.btnSerStart.Location = new System.Drawing.Point(8, 87);
            this.btnSerStart.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSerStart.Name = "btnSerStart";
            this.btnSerStart.Size = new System.Drawing.Size(100, 28);
            this.btnSerStart.TabIndex = 0;
            this.btnSerStart.Text = "Start";
            this.btnSerStart.UseVisualStyleBackColor = true;
            this.btnSerStart.Click += new System.EventHandler(this.btnSerStart_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.linkLabel1);
            this.tabPage3.Controls.Add(this.textBox2);
            this.tabPage3.Controls.Add(this.label8);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label10);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.label12);
            this.tabPage3.Controls.Add(this.textBox1);
            this.tabPage3.Controls.Add(this.label13);
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabPage3.Size = new System.Drawing.Size(709, 254);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "About";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(255, 193);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(200, 17);
            this.linkLabel1.TabIndex = 18;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "http://www.pcfire.net/XMTuner/";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.Black;
            this.textBox2.Location = new System.Drawing.Point(481, 95);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(81, 22);
            this.textBox2.TabIndex = 17;
            this.textBox2.Text = "20091208";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(395, 100);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 17);
            this.label8.TabIndex = 16;
            this.label8.Text = "Build Date:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(181, 142);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 17);
            this.label9.TabIndex = 15;
            this.label9.Text = "Curtis M. Kularski";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(181, 121);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 17);
            this.label10.TabIndex = 14;
            this.label10.Text = "Chris Crews";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(157, 100);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(95, 17);
            this.label11.TabIndex = 13;
            this.label11.Text = "Developers:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(155, 48);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(164, 39);
            this.label12.TabIndex = 12;
            this.label12.Text = "XMTuner";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.ForeColor = System.Drawing.Color.Black;
            this.textBox1.Location = new System.Drawing.Point(481, 58);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(81, 22);
            this.textBox1.TabIndex = 11;
            this.textBox1.Text = "0.3";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(409, 63);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(64, 17);
            this.label13.TabIndex = 10;
            this.label13.Text = "Version: ";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox1.Location = new System.Drawing.Point(140, 32);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(440, 190);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(815, 263);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 17);
            this.label5.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(747, 334);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblClock);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tabcontrol1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "XMTuner";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tabcontrol1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblClock;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TabControl tabcontrol1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.RichTextBox outputbox;
        private System.Windows.Forms.ComboBox channelBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem restoreToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitXMTunerToolStripMenuItem;
        private System.Windows.Forms.ComboBox protocolBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ToolStripMenuItem viewServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button cpyToClip;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox bitRateBox;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnSerUninstall;
        private System.Windows.Forms.Button btnSerInstall;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblServiceStat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSerRestart;
        private System.Windows.Forms.Button btnSerStop;
        private System.Windows.Forms.Button btnSerStart;
        private System.Windows.Forms.Label lblServiceInst;
    }
}
