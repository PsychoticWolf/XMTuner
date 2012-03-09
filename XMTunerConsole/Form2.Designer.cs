/*
 * XMTuner: Copyright (C) 2009-2012 Chris Crews and Curtis M. Kularski.
 * 
 * This file is part of XMTuner.

 * XMTuner is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * XMTuner is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with XMTuner.  If not, see <http://www.gnu.org/licenses/>.
 */

namespace XMTuner
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.bSave = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.chkAutologin = new System.Windows.Forms.CheckBox();
            this.chkMMS = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bUpdateLineup = new System.Windows.Forms.Button();
            this.chkBitrate = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.boxNetwork = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tversityBox2 = new System.Windows.Forms.GroupBox();
            this.tbtnFeed = new System.Windows.Forms.Button();
            this.tlblConfig = new System.Windows.Forms.Label();
            this.tlblFeed = new System.Windows.Forms.Label();
            this.tlblEnabled = new System.Windows.Forms.Label();
            this.tbtnValidate = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.linkTversity = new System.Windows.Forms.LinkLabel();
            this.txtTversity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.channelListStyle = new System.Windows.Forms.ComboBox();
            this.numRecent = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.chkShowURL = new System.Windows.Forms.CheckBox();
            this.chkOnTop = new System.Windows.Forms.CheckBox();
            this.chkShowNote = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.label11 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tversityBox2.SuspendLayout();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRecent)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Password: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Listen on Port:";
            this.toolTip1.SetToolTip(this.label3, "XMTuner\'s built-in webserver needs a port ot listen on, its default is 19081, if " +
                    "this is already in use on your system, or you would prefer a different port numb" +
                    "er, enter it here.");
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(128, 262);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(112, 23);
            this.bSave.TabIndex = 6;
            this.bSave.Text = "Save Configuration";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(75, 22);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(138, 20);
            this.txtUser.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtUser, "Username for your Sirius/XM radio online account.");
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(75, 54);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(138, 20);
            this.txtPassword.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtPassword, "Password for your Sirius/XM radio online account.");
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(86, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(117, 20);
            this.txtPort.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtPort, "XMTuner\'s built-in webserver needs a port ot listen on, its default is 19081, if " +
                    "this is already in use on your system, or you would prefer a different port numb" +
                    "er, enter it here.");
            // 
            // chkAutologin
            // 
            this.chkAutologin.AutoSize = true;
            this.chkAutologin.Location = new System.Drawing.Point(75, 100);
            this.chkAutologin.Margin = new System.Windows.Forms.Padding(2);
            this.chkAutologin.Name = "chkAutologin";
            this.chkAutologin.Size = new System.Drawing.Size(70, 17);
            this.chkAutologin.TabIndex = 5;
            this.chkAutologin.Text = "Autologin";
            this.toolTip1.SetToolTip(this.chkAutologin, "When XMTuner starts, automatically login without waiting for the start button to " +
                    "be pressed.");
            this.chkAutologin.UseVisualStyleBackColor = true;
            // 
            // chkMMS
            // 
            this.chkMMS.AutoSize = true;
            this.chkMMS.Location = new System.Drawing.Point(34, 43);
            this.chkMMS.Margin = new System.Windows.Forms.Padding(2);
            this.chkMMS.Name = "chkMMS";
            this.chkMMS.Size = new System.Drawing.Size(241, 17);
            this.chkMMS.TabIndex = 3;
            this.chkMMS.Text = "Use MMS (instead of HTTP) for Stream URLs";
            this.toolTip1.SetToolTip(this.chkMMS, resources.GetString("chkMMS.ToolTip"));
            this.chkMMS.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Location = new System.Drawing.Point(10, 11);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(348, 245);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.chkBitrate);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.txtPassword);
            this.tabPage1.Controls.Add(this.txtUser);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.chkAutologin);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage1.Size = new System.Drawing.Size(340, 219);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Login";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.bUpdateLineup);
            this.groupBox3.Location = new System.Drawing.Point(5, 134);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(332, 81);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lineup";
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(10, 18);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(317, 33);
            this.label5.TabIndex = 1;
            this.label5.Text = "The channel lineup is updated periodically automatically, if channels are missing" +
                " you can force an update of the lineup.";
            // 
            // bUpdateLineup
            // 
            this.bUpdateLineup.Enabled = false;
            this.bUpdateLineup.Location = new System.Drawing.Point(13, 50);
            this.bUpdateLineup.Margin = new System.Windows.Forms.Padding(2);
            this.bUpdateLineup.Name = "bUpdateLineup";
            this.bUpdateLineup.Size = new System.Drawing.Size(129, 23);
            this.bUpdateLineup.TabIndex = 0;
            this.bUpdateLineup.TabStop = false;
            this.bUpdateLineup.Text = "Update Channel Lineup";
            this.bUpdateLineup.UseVisualStyleBackColor = true;
            this.bUpdateLineup.Click += new System.EventHandler(this.bUpdateLineup_Click);
            // 
            // chkBitrate
            // 
            this.chkBitrate.AutoSize = true;
            this.chkBitrate.Location = new System.Drawing.Point(75, 78);
            this.chkBitrate.Name = "chkBitrate";
            this.chkBitrate.Size = new System.Drawing.Size(186, 17);
            this.chkBitrate.TabIndex = 4;
            this.chkBitrate.Text = "Default to High Bandwidth Stream";
            this.toolTip1.SetToolTip(this.chkBitrate, "Prefer Sirius/XM\'s 128k \"Premium\" stream (if available), over the standard 32k st" +
                    "ream for channels. (Recommended)");
            this.chkBitrate.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.boxNetwork);
            this.groupBox2.Location = new System.Drawing.Point(2, 5);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(335, 124);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            // 
            // boxNetwork
            // 
            this.boxNetwork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxNetwork.FormattingEnabled = true;
            this.boxNetwork.Items.AddRange(new object[] {
            "XM",
            "SIRIUS"});
            this.boxNetwork.Location = new System.Drawing.Point(218, 48);
            this.boxNetwork.Name = "boxNetwork";
            this.boxNetwork.Size = new System.Drawing.Size(67, 21);
            this.boxNetwork.TabIndex = 3;
            this.toolTip1.SetToolTip(this.boxNetwork, "Which network to log in to. (Default: XM)");
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.groupBox5);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage2.Size = new System.Drawing.Size(340, 219);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Server";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txtHostname);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(10, 119);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(328, 98);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "DNS";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label8.Location = new System.Drawing.Point(25, 72);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(148, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "(Example: www.example.com)";
            // 
            // txtHostname
            // 
            this.txtHostname.Location = new System.Drawing.Point(8, 48);
            this.txtHostname.Margin = new System.Windows.Forms.Padding(2);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(265, 20);
            this.txtHostname.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(319, 31);
            this.label4.TabIndex = 0;
            this.label4.Text = "If you would like a hostname to appear in generated feed and stream URLs instead " +
                "of your IP address, enter it below:";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.chkMMS);
            this.groupBox4.Controls.Add(this.txtPort);
            this.groupBox4.Location = new System.Drawing.Point(10, 5);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox4.Size = new System.Drawing.Size(328, 89);
            this.groupBox4.TabIndex = 14;
            this.groupBox4.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label9.Location = new System.Drawing.Point(207, 16);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "(Default: 19081)";
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(2);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(2);
            this.tabPage3.Size = new System.Drawing.Size(340, 219);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "TVersity";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tversityBox2);
            this.groupBox1.Controls.Add(this.tbtnValidate);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.linkTversity);
            this.groupBox1.Controls.Add(this.txtTversity);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.groupBox7);
            this.groupBox1.Location = new System.Drawing.Point(4, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(333, 215);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // tversityBox2
            // 
            this.tversityBox2.Controls.Add(this.tbtnFeed);
            this.tversityBox2.Controls.Add(this.tlblConfig);
            this.tversityBox2.Controls.Add(this.tlblFeed);
            this.tversityBox2.Controls.Add(this.tlblEnabled);
            this.tversityBox2.Enabled = false;
            this.tversityBox2.Location = new System.Drawing.Point(7, 90);
            this.tversityBox2.Name = "tversityBox2";
            this.tversityBox2.Size = new System.Drawing.Size(312, 107);
            this.tversityBox2.TabIndex = 5;
            this.tversityBox2.TabStop = false;
            this.tversityBox2.Text = "Status";
            this.tversityBox2.Visible = false;
            // 
            // tbtnFeed
            // 
            this.tbtnFeed.Enabled = false;
            this.tbtnFeed.Location = new System.Drawing.Point(243, 41);
            this.tbtnFeed.Name = "tbtnFeed";
            this.tbtnFeed.Size = new System.Drawing.Size(65, 20);
            this.tbtnFeed.TabIndex = 3;
            this.tbtnFeed.TabStop = false;
            this.tbtnFeed.Text = "Add Feed";
            this.tbtnFeed.UseVisualStyleBackColor = true;
            this.tbtnFeed.Click += new System.EventHandler(this.tbtnFeed_Click);
            // 
            // tlblConfig
            // 
            this.tlblConfig.AutoSize = true;
            this.tlblConfig.Enabled = false;
            this.tlblConfig.Location = new System.Drawing.Point(7, 63);
            this.tlblConfig.Name = "tlblConfig";
            this.tlblConfig.Size = new System.Drawing.Size(37, 13);
            this.tlblConfig.TabIndex = 2;
            this.tlblConfig.Text = "Config";
            // 
            // tlblFeed
            // 
            this.tlblFeed.AutoSize = true;
            this.tlblFeed.Enabled = false;
            this.tlblFeed.Location = new System.Drawing.Point(7, 41);
            this.tlblFeed.Name = "tlblFeed";
            this.tlblFeed.Size = new System.Drawing.Size(31, 13);
            this.tlblFeed.TabIndex = 1;
            this.tlblFeed.Text = "Feed";
            // 
            // tlblEnabled
            // 
            this.tlblEnabled.AutoSize = true;
            this.tlblEnabled.Enabled = false;
            this.tlblEnabled.Location = new System.Drawing.Point(7, 20);
            this.tlblEnabled.Name = "tlblEnabled";
            this.tlblEnabled.Size = new System.Drawing.Size(46, 13);
            this.tlblEnabled.TabIndex = 0;
            this.tlblEnabled.Text = "Enabled";
            // 
            // tbtnValidate
            // 
            this.tbtnValidate.Enabled = false;
            this.tbtnValidate.Location = new System.Drawing.Point(186, 41);
            this.tbtnValidate.Name = "tbtnValidate";
            this.tbtnValidate.Size = new System.Drawing.Size(53, 22);
            this.tbtnValidate.TabIndex = 3;
            this.tbtnValidate.Text = "Validate";
            this.tbtnValidate.UseVisualStyleBackColor = true;
            this.tbtnValidate.Click += new System.EventHandler(this.button1_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label7.Location = new System.Drawing.Point(20, 63);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(137, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "(Example: 127.0.0.1:41952)";
            // 
            // linkTversity
            // 
            this.linkTversity.AutoSize = true;
            this.linkTversity.Location = new System.Drawing.Point(264, 200);
            this.linkTversity.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkTversity.Name = "linkTversity";
            this.linkTversity.Size = new System.Drawing.Size(65, 13);
            this.linkTversity.TabIndex = 3;
            this.linkTversity.TabStop = true;
            this.linkTversity.Text = "Get TVersity";
            this.linkTversity.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTversity_LinkClicked);
            // 
            // txtTversity
            // 
            this.txtTversity.Location = new System.Drawing.Point(7, 43);
            this.txtTversity.Margin = new System.Windows.Forms.Padding(2);
            this.txtTversity.Name = "txtTversity";
            this.txtTversity.Size = new System.Drawing.Size(174, 20);
            this.txtTversity.TabIndex = 2;
            this.toolTip1.SetToolTip(this.txtTversity, "IP Address or Hostname and port number for TVersity running on your network.");
            this.txtTversity.TextChanged += new System.EventHandler(this.txtTversity_TextChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(4, 11);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(324, 28);
            this.label6.TabIndex = 0;
            this.label6.Text = "XMTuner can use TVersity to create MP3 streams, to enable, enter the address and " +
                "port to your TVersity server:";
            // 
            // groupBox7
            // 
            this.groupBox7.Location = new System.Drawing.Point(7, 90);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(312, 107);
            this.groupBox7.TabIndex = 5;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Details";
            // 
            // tabPage4
            // 
            this.tabPage4.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage4.Controls.Add(this.label11);
            this.tabPage4.Controls.Add(this.channelListStyle);
            this.tabPage4.Controls.Add(this.numRecent);
            this.tabPage4.Controls.Add(this.label10);
            this.tabPage4.Controls.Add(this.chkShowURL);
            this.tabPage4.Controls.Add(this.chkOnTop);
            this.tabPage4.Controls.Add(this.chkShowNote);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(340, 219);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "UI";
            // 
            // channelListStyle
            // 
            this.channelListStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.channelListStyle.FormattingEnabled = true;
            this.channelListStyle.Items.AddRange(new object[] {
            "All Channels",
            "Favorite Channels",
            "By Category"});
            this.channelListStyle.Location = new System.Drawing.Point(105, 122);
            this.channelListStyle.Name = "channelListStyle";
            this.channelListStyle.Size = new System.Drawing.Size(121, 21);
            this.channelListStyle.TabIndex = 6;
            // 
            // numRecent
            // 
            this.numRecent.Location = new System.Drawing.Point(40, 87);
            this.numRecent.Name = "numRecent";
            this.numRecent.Size = new System.Drawing.Size(45, 20);
            this.numRecent.TabIndex = 5;
            this.numRecent.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 90);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(238, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Keep                   entries in recently played history";
            // 
            // chkShowURL
            // 
            this.chkShowURL.AutoSize = true;
            this.chkShowURL.Checked = true;
            this.chkShowURL.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowURL.Location = new System.Drawing.Point(7, 64);
            this.chkShowURL.Name = "chkShowURL";
            this.chkShowURL.Size = new System.Drawing.Size(113, 17);
            this.chkShowURL.TabIndex = 4;
            this.chkShowURL.Text = "Show URL Builder";
            this.chkShowURL.UseVisualStyleBackColor = true;
            // 
            // chkOnTop
            // 
            this.chkOnTop.AutoSize = true;
            this.chkOnTop.Location = new System.Drawing.Point(7, 40);
            this.chkOnTop.Name = "chkOnTop";
            this.chkOnTop.Size = new System.Drawing.Size(229, 17);
            this.chkOnTop.TabIndex = 3;
            this.chkOnTop.Text = "Keep XMTuner on top of other applications";
            this.toolTip1.SetToolTip(this.chkOnTop, "Make XMTuner stay on top of other applications.");
            this.chkOnTop.UseVisualStyleBackColor = true;
            // 
            // chkShowNote
            // 
            this.chkShowNote.AutoSize = true;
            this.chkShowNote.Checked = true;
            this.chkShowNote.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowNote.Location = new System.Drawing.Point(7, 16);
            this.chkShowNote.Name = "chkShowNote";
            this.chkShowNote.Size = new System.Drawing.Size(192, 17);
            this.chkShowNote.TabIndex = 2;
            this.chkShowNote.Text = "Show Notification on Song Change";
            this.toolTip1.SetToolTip(this.chkShowNote, "XMTuner, by default, shows a notification box when a song changes, if you\'d prefe" +
                    "r not to have these notifications appear, uncheck this box.");
            this.chkShowNote.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "XMTuner Help";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(7, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 6;
            this.label11.Text = "Channel List View:";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 295);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.bSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tversityBox2.ResumeLayout(false);
            this.tversityBox2.PerformLayout();
            this.tabPage4.ResumeLayout(false);
            this.tabPage4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRecent)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.CheckBox chkAutologin;
        private System.Windows.Forms.CheckBox chkMMS;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.CheckBox chkBitrate;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTversity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel linkTversity;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bUpdateLineup;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtHostname;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox boxNetwork;
        private System.Windows.Forms.Button tbtnValidate;
        private System.Windows.Forms.GroupBox tversityBox2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label tlblEnabled;
        private System.Windows.Forms.Label tlblConfig;
        private System.Windows.Forms.Label tlblFeed;
        private System.Windows.Forms.Button tbtnFeed;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.NumericUpDown numRecent;
        private System.Windows.Forms.CheckBox chkShowURL;
        private System.Windows.Forms.CheckBox chkOnTop;
        private System.Windows.Forms.CheckBox chkShowNote;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox channelListStyle;
        private System.Windows.Forms.Label label11;
    }
}