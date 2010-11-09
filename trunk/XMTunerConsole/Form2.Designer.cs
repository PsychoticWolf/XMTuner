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
            this.tGeneral = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.boxNetwork = new System.Windows.Forms.ComboBox();
            this.tServer = new System.Windows.Forms.TabPage();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tTVersity = new System.Windows.Forms.TabPage();
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
            this.tAdvanced = new System.Windows.Forms.TabPage();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.bUpdateLineup = new System.Windows.Forms.Button();
            this.tRemote = new System.Windows.Forms.TabPage();
            this.chkAutoServer = new System.Windows.Forms.CheckBox();
            this.chkBitrate = new System.Windows.Forms.CheckBox();
            this.channelListStyle = new System.Windows.Forms.ComboBox();
            this.numRecent = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.chkOnTop = new System.Windows.Forms.CheckBox();
            this.chkShowNote = new System.Windows.Forms.CheckBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtHostname = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.maskedTextBox1 = new System.Windows.Forms.MaskedTextBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tGeneral.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tServer.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.tTVersity.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tversityBox2.SuspendLayout();
            this.tAdvanced.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tRemote.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRecent)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 45);
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
            this.bSave.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bSave.Location = new System.Drawing.Point(0, 327);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(435, 23);
            this.bSave.TabIndex = 6;
            this.bSave.Text = "Save Configuration";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(77, 50);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(138, 20);
            this.txtUser.TabIndex = 1;
            this.toolTip1.SetToolTip(this.txtUser, "Username for your Sirius/XM radio online account.");
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(73, 42);
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
            this.chkAutologin.Location = new System.Drawing.Point(7, 11);
            this.chkAutologin.Margin = new System.Windows.Forms.Padding(2);
            this.chkAutologin.Name = "chkAutologin";
            this.chkAutologin.Size = new System.Drawing.Size(169, 17);
            this.chkAutologin.TabIndex = 5;
            this.chkAutologin.Text = "Login Automatically on Startup";
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
            this.tabControl1.Controls.Add(this.tGeneral);
            this.tabControl1.Controls.Add(this.tServer);
            this.tabControl1.Controls.Add(this.tTVersity);
            this.tabControl1.Controls.Add(this.tRemote);
            this.tabControl1.Controls.Add(this.tAdvanced);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(435, 327);
            this.tabControl1.TabIndex = 1;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tGeneral
            // 
            this.tGeneral.BackColor = System.Drawing.Color.Transparent;
            this.tGeneral.Controls.Add(this.label11);
            this.tGeneral.Controls.Add(this.channelListStyle);
            this.tGeneral.Controls.Add(this.numRecent);
            this.tGeneral.Controls.Add(this.label10);
            this.tGeneral.Controls.Add(this.chkOnTop);
            this.tGeneral.Controls.Add(this.chkShowNote);
            this.tGeneral.Controls.Add(this.label1);
            this.tGeneral.Controls.Add(this.txtUser);
            this.tGeneral.Controls.Add(this.chkAutologin);
            this.tGeneral.Controls.Add(this.groupBox2);
            this.tGeneral.Location = new System.Drawing.Point(4, 22);
            this.tGeneral.Margin = new System.Windows.Forms.Padding(2);
            this.tGeneral.Name = "tGeneral";
            this.tGeneral.Padding = new System.Windows.Forms.Padding(2);
            this.tGeneral.Size = new System.Drawing.Size(427, 301);
            this.tGeneral.TabIndex = 0;
            this.tGeneral.Text = "General";
            this.tGeneral.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.boxNetwork);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.txtPassword);
            this.groupBox2.Location = new System.Drawing.Point(4, 33);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox2.Size = new System.Drawing.Size(301, 81);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Login";
            // 
            // boxNetwork
            // 
            this.boxNetwork.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.boxNetwork.FormattingEnabled = true;
            this.boxNetwork.Items.AddRange(new object[] {
            "XM",
            "SIRIUS"});
            this.boxNetwork.Location = new System.Drawing.Point(217, 42);
            this.boxNetwork.Name = "boxNetwork";
            this.boxNetwork.Size = new System.Drawing.Size(67, 21);
            this.boxNetwork.TabIndex = 3;
            this.toolTip1.SetToolTip(this.boxNetwork, "Which network to log in to. (Default: XM)");
            // 
            // tServer
            // 
            this.tServer.BackColor = System.Drawing.Color.Transparent;
            this.tServer.Controls.Add(this.groupBox6);
            this.tServer.Controls.Add(this.chkAutoServer);
            this.tServer.Controls.Add(this.groupBox4);
            this.tServer.Location = new System.Drawing.Point(4, 22);
            this.tServer.Margin = new System.Windows.Forms.Padding(2);
            this.tServer.Name = "tServer";
            this.tServer.Padding = new System.Windows.Forms.Padding(2);
            this.tServer.Size = new System.Drawing.Size(427, 301);
            this.tServer.TabIndex = 1;
            this.tServer.Text = "Server";
            this.tServer.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkBitrate);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.chkMMS);
            this.groupBox4.Controls.Add(this.txtPort);
            this.groupBox4.Location = new System.Drawing.Point(10, 36);
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
            // tTVersity
            // 
            this.tTVersity.BackColor = System.Drawing.Color.Transparent;
            this.tTVersity.Controls.Add(this.groupBox1);
            this.tTVersity.Location = new System.Drawing.Point(4, 22);
            this.tTVersity.Margin = new System.Windows.Forms.Padding(2);
            this.tTVersity.Name = "tTVersity";
            this.tTVersity.Padding = new System.Windows.Forms.Padding(2);
            this.tTVersity.Size = new System.Drawing.Size(427, 301);
            this.tTVersity.TabIndex = 2;
            this.tTVersity.Text = "TVersity";
            this.tTVersity.UseVisualStyleBackColor = true;
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
            // tAdvanced
            // 
            this.tAdvanced.BackColor = System.Drawing.Color.Transparent;
            this.tAdvanced.Controls.Add(this.groupBox9);
            this.tAdvanced.Controls.Add(this.groupBox8);
            this.tAdvanced.Controls.Add(this.groupBox5);
            this.tAdvanced.Controls.Add(this.groupBox3);
            this.tAdvanced.Location = new System.Drawing.Point(4, 22);
            this.tAdvanced.Name = "tAdvanced";
            this.tAdvanced.Padding = new System.Windows.Forms.Padding(3);
            this.tAdvanced.Size = new System.Drawing.Size(427, 301);
            this.tAdvanced.TabIndex = 3;
            this.tAdvanced.Text = "Advanced";
            this.tAdvanced.UseVisualStyleBackColor = true;
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.bUpdateLineup);
            this.groupBox3.Location = new System.Drawing.Point(7, 5);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox3.Size = new System.Drawing.Size(332, 81);
            this.groupBox3.TabIndex = 14;
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
            // 
            // tRemote
            // 
            this.tRemote.Controls.Add(this.checkBox2);
            this.tRemote.Location = new System.Drawing.Point(4, 22);
            this.tRemote.Name = "tRemote";
            this.tRemote.Padding = new System.Windows.Forms.Padding(3);
            this.tRemote.Size = new System.Drawing.Size(427, 301);
            this.tRemote.TabIndex = 4;
            this.tRemote.Text = "Remote";
            this.tRemote.UseVisualStyleBackColor = true;
            // 
            // chkAutoServer
            // 
            this.chkAutoServer.AutoSize = true;
            this.chkAutoServer.Enabled = false;
            this.chkAutoServer.Location = new System.Drawing.Point(18, 14);
            this.chkAutoServer.Name = "chkAutoServer";
            this.chkAutoServer.Size = new System.Drawing.Size(114, 17);
            this.chkAutoServer.TabIndex = 15;
            this.chkAutoServer.Text = "Enable Webserver";
            this.chkAutoServer.UseVisualStyleBackColor = true;
            // 
            // chkBitrate
            // 
            this.chkBitrate.AutoSize = true;
            this.chkBitrate.Location = new System.Drawing.Point(34, 65);
            this.chkBitrate.Name = "chkBitrate";
            this.chkBitrate.Size = new System.Drawing.Size(186, 17);
            this.chkBitrate.TabIndex = 16;
            this.chkBitrate.Text = "Default to High Bandwidth Stream";
            this.toolTip1.SetToolTip(this.chkBitrate, "Prefer Sirius/XM\'s 128k \"Premium\" stream (if available), over the standard 32k st" +
                    "ream for channels. (Recommended)");
            this.chkBitrate.UseVisualStyleBackColor = true;
            // 
            // channelListStyle
            // 
            this.channelListStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.channelListStyle.FormattingEnabled = true;
            this.channelListStyle.Items.AddRange(new object[] {
            "All Channels",
            "Favorite Channels",
            "By Category"});
            this.channelListStyle.Location = new System.Drawing.Point(110, 192);
            this.channelListStyle.Name = "channelListStyle";
            this.channelListStyle.Size = new System.Drawing.Size(121, 21);
            this.channelListStyle.TabIndex = 17;
            // 
            // numRecent
            // 
            this.numRecent.Location = new System.Drawing.Point(44, 163);
            this.numRecent.Name = "numRecent";
            this.numRecent.Size = new System.Drawing.Size(45, 20);
            this.numRecent.TabIndex = 16;
            this.numRecent.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(10, 166);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(238, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Keep                   entries in recently played history";
            // 
            // chkOnTop
            // 
            this.chkOnTop.AutoSize = true;
            this.chkOnTop.Location = new System.Drawing.Point(13, 143);
            this.chkOnTop.Name = "chkOnTop";
            this.chkOnTop.Size = new System.Drawing.Size(229, 17);
            this.chkOnTop.TabIndex = 14;
            this.chkOnTop.Text = "Keep XMTuner on top of other applications";
            this.toolTip1.SetToolTip(this.chkOnTop, "Make XMTuner stay on top of other applications.");
            this.chkOnTop.UseVisualStyleBackColor = true;
            // 
            // chkShowNote
            // 
            this.chkShowNote.AutoSize = true;
            this.chkShowNote.Checked = true;
            this.chkShowNote.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowNote.Location = new System.Drawing.Point(13, 119);
            this.chkShowNote.Name = "chkShowNote";
            this.chkShowNote.Size = new System.Drawing.Size(192, 17);
            this.chkShowNote.TabIndex = 13;
            this.chkShowNote.Text = "Show Notification on Song Change";
            this.toolTip1.SetToolTip(this.chkShowNote, "XMTuner, by default, shows a notification box when a song changes, if you\'d prefe" +
                    "r not to have these notifications appear, uncheck this box.");
            this.chkShowNote.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 195);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(94, 13);
            this.label11.TabIndex = 18;
            this.label11.Text = "Channel List View:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.txtHostname);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(7, 90);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox5.Size = new System.Drawing.Size(332, 98);
            this.groupBox5.TabIndex = 15;
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
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Enabled = false;
            this.checkBox2.Location = new System.Drawing.Point(9, 18);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(220, 17);
            this.checkBox2.TabIndex = 0;
            this.checkBox2.Text = "Enable Remote Control (Requires Server)";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label12);
            this.groupBox6.Controls.Add(this.textBox1);
            this.groupBox6.Enabled = false;
            this.groupBox6.Location = new System.Drawing.Point(10, 130);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(328, 79);
            this.groupBox6.TabIndex = 16;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Access Control";
            // 
            // textBox1
            // 
            this.textBox1.Enabled = false;
            this.textBox1.Location = new System.Drawing.Point(11, 32);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(142, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.UseSystemPasswordChar = true;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Enabled = false;
            this.label12.Location = new System.Drawing.Point(8, 16);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(177, 13);
            this.label12.TabIndex = 1;
            this.label12.Text = "Password protect the web interface:";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.maskedTextBox1);
            this.groupBox8.Controls.Add(this.label13);
            this.groupBox8.Enabled = false;
            this.groupBox8.Location = new System.Drawing.Point(7, 193);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(332, 47);
            this.groupBox8.TabIndex = 16;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Error Reporting";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Enabled = false;
            this.label13.Location = new System.Drawing.Point(7, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(73, 13);
            this.label13.TabIndex = 0;
            this.label13.Text = "NSCA Server:";
            // 
            // maskedTextBox1
            // 
            this.maskedTextBox1.Enabled = false;
            this.maskedTextBox1.Location = new System.Drawing.Point(83, 17);
            this.maskedTextBox1.Name = "maskedTextBox1";
            this.maskedTextBox1.Size = new System.Drawing.Size(190, 20);
            this.maskedTextBox1.TabIndex = 1;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.label14);
            this.groupBox9.Controls.Add(this.textBox2);
            this.groupBox9.Enabled = false;
            this.groupBox9.Location = new System.Drawing.Point(9, 247);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(330, 48);
            this.groupBox9.TabIndex = 17;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Advanced Networking";
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(81, 22);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(190, 20);
            this.textBox2.TabIndex = 0;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Enabled = false;
            this.label14.Location = new System.Drawing.Point(8, 25);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(67, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Proxy Server";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(435, 350);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.bSave);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.tabControl1.ResumeLayout(false);
            this.tGeneral.ResumeLayout(false);
            this.tGeneral.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tServer.ResumeLayout(false);
            this.tServer.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.tTVersity.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tversityBox2.ResumeLayout(false);
            this.tversityBox2.PerformLayout();
            this.tAdvanced.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.tRemote.ResumeLayout(false);
            this.tRemote.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRecent)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
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
        private System.Windows.Forms.TabPage tGeneral;
        private System.Windows.Forms.TabPage tServer;
        private System.Windows.Forms.TabPage tTVersity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtTversity;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.LinkLabel linkTversity;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
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
        private System.Windows.Forms.TabPage tAdvanced;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bUpdateLineup;
        private System.Windows.Forms.TabPage tRemote;
        private System.Windows.Forms.CheckBox chkAutoServer;
        private System.Windows.Forms.CheckBox chkBitrate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox channelListStyle;
        private System.Windows.Forms.NumericUpDown numRecent;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkOnTop;
        private System.Windows.Forms.CheckBox chkShowNote;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtHostname;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.MaskedTextBox maskedTextBox1;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox textBox2;
    }
}