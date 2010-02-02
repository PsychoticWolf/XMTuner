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
            this.button1 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.linkTversity = new System.Windows.Forms.LinkLabel();
            this.txtTversity = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
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
            this.label2.TabIndex = 1;
            this.label2.Text = "Password: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Listen on Port:";
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(128, 262);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(112, 23);
            this.bSave.TabIndex = 3;
            this.bSave.Text = "Save Configuration";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.bSave_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(75, 22);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(138, 20);
            this.txtUser.TabIndex = 4;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(75, 54);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '•';
            this.txtPassword.Size = new System.Drawing.Size(138, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(86, 12);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(117, 20);
            this.txtPort.TabIndex = 6;
            // 
            // chkAutologin
            // 
            this.chkAutologin.AutoSize = true;
            this.chkAutologin.Location = new System.Drawing.Point(75, 100);
            this.chkAutologin.Margin = new System.Windows.Forms.Padding(2);
            this.chkAutologin.Name = "chkAutologin";
            this.chkAutologin.Size = new System.Drawing.Size(70, 17);
            this.chkAutologin.TabIndex = 8;
            this.chkAutologin.Text = "Autologin";
            this.chkAutologin.UseVisualStyleBackColor = true;
            // 
            // chkMMS
            // 
            this.chkMMS.AutoSize = true;
            this.chkMMS.Location = new System.Drawing.Point(34, 43);
            this.chkMMS.Margin = new System.Windows.Forms.Padding(2);
            this.chkMMS.Name = "chkMMS";
            this.chkMMS.Size = new System.Drawing.Size(241, 17);
            this.chkMMS.TabIndex = 12;
            this.chkMMS.Text = "Use MMS (instead of HTTP) for Stream URLs";
            this.chkMMS.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(10, 11);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(348, 245);
            this.tabControl1.TabIndex = 12;
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
            this.chkBitrate.TabIndex = 11;
            this.chkBitrate.Text = "Default to High Bandwidth Stream";
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
            this.boxNetwork.TabIndex = 0;
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
            this.label8.TabIndex = 15;
            this.label8.Text = "(Example: www.example.com)";
            // 
            // txtHostname
            // 
            this.txtHostname.Location = new System.Drawing.Point(8, 48);
            this.txtHostname.Margin = new System.Windows.Forms.Padding(2);
            this.txtHostname.Name = "txtHostname";
            this.txtHostname.Size = new System.Drawing.Size(265, 20);
            this.txtHostname.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(4, 15);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(319, 31);
            this.label4.TabIndex = 13;
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
            this.label9.TabIndex = 13;
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
            this.groupBox1.Controls.Add(this.button1);
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
            this.tversityBox2.Text = "Details";
            this.tversityBox2.Visible = false;
            // 
            // tbtnFeed
            // 
            this.tbtnFeed.Enabled = false;
            this.tbtnFeed.Location = new System.Drawing.Point(243, 41);
            this.tbtnFeed.Name = "tbtnFeed";
            this.tbtnFeed.Size = new System.Drawing.Size(65, 20);
            this.tbtnFeed.TabIndex = 3;
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(186, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(53, 22);
            this.button1.TabIndex = 4;
            this.button1.Text = "Validate";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.txtTversity.TabIndex = 1;
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
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 10000;
            this.toolTip1.InitialDelay = 500;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 100;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "XMTuner Help";
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
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox tversityBox2;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label tlblEnabled;
        private System.Windows.Forms.Label tlblConfig;
        private System.Windows.Forms.Label tlblFeed;
        private System.Windows.Forms.Button tbtnFeed;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}