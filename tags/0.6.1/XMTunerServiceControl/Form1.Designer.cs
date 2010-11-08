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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblServiceStat = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btnSerRestart = new System.Windows.Forms.Button();
            this.btnSerStop = new System.Windows.Forms.Button();
            this.btnSerStart = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.btnSerInstall = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.lblServiceInst = new System.Windows.Forms.Label();
            this.btnSerUninstall = new System.Windows.Forms.Button();
            this.bConfig = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tServiceControl = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tLog = new System.Windows.Forms.TabPage();
            this.rtbServiceLog = new System.Windows.Forms.RichTextBox();
            this.logContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tServiceControl.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tLog.SuspendLayout();
            this.logContextMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblServiceStat);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.btnSerRestart);
            this.groupBox2.Controls.Add(this.btnSerStop);
            this.groupBox2.Controls.Add(this.btnSerStart);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(257, 100);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Service Control";
            // 
            // lblServiceStat
            // 
            this.lblServiceStat.AutoSize = true;
            this.lblServiceStat.Location = new System.Drawing.Point(52, 33);
            this.lblServiceStat.Name = "lblServiceStat";
            this.lblServiceStat.Size = new System.Drawing.Size(53, 13);
            this.lblServiceStat.TabIndex = 4;
            this.lblServiceStat.Text = "Unknown";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Status:";
            // 
            // btnSerRestart
            // 
            this.btnSerRestart.Location = new System.Drawing.Point(170, 71);
            this.btnSerRestart.Name = "btnSerRestart";
            this.btnSerRestart.Size = new System.Drawing.Size(75, 23);
            this.btnSerRestart.TabIndex = 2;
            this.btnSerRestart.Text = "Restart";
            this.btnSerRestart.UseVisualStyleBackColor = true;
            this.btnSerRestart.Click += new System.EventHandler(this.btnSerRestart_Click);
            // 
            // btnSerStop
            // 
            this.btnSerStop.Location = new System.Drawing.Point(88, 71);
            this.btnSerStop.Name = "btnSerStop";
            this.btnSerStop.Size = new System.Drawing.Size(75, 23);
            this.btnSerStop.TabIndex = 1;
            this.btnSerStop.Text = "Stop";
            this.btnSerStop.UseVisualStyleBackColor = true;
            this.btnSerStop.Click += new System.EventHandler(this.btnSerStop_Click);
            // 
            // btnSerStart
            // 
            this.btnSerStart.Location = new System.Drawing.Point(6, 71);
            this.btnSerStart.Name = "btnSerStart";
            this.btnSerStart.Size = new System.Drawing.Size(75, 23);
            this.btnSerStart.TabIndex = 0;
            this.btnSerStart.Text = "Start";
            this.btnSerStart.UseVisualStyleBackColor = true;
            this.btnSerStart.Click += new System.EventHandler(this.btnSerStart_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 27);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(53, 13);
            this.label14.TabIndex = 2;
            this.label14.Text = "Service is";
            // 
            // btnSerInstall
            // 
            this.btnSerInstall.Location = new System.Drawing.Point(6, 55);
            this.btnSerInstall.Name = "btnSerInstall";
            this.btnSerInstall.Size = new System.Drawing.Size(75, 23);
            this.btnSerInstall.TabIndex = 0;
            this.btnSerInstall.Text = "Install";
            this.btnSerInstall.UseVisualStyleBackColor = true;
            this.btnSerInstall.Click += new System.EventHandler(this.btnSerInstall_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.lblServiceInst);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.btnSerUninstall);
            this.groupBox3.Controls.Add(this.btnSerInstall);
            this.groupBox3.Location = new System.Drawing.Point(6, 113);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(257, 84);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Installation";
            // 
            // lblServiceInst
            // 
            this.lblServiceInst.AutoSize = true;
            this.lblServiceInst.Location = new System.Drawing.Point(56, 27);
            this.lblServiceInst.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblServiceInst.Name = "lblServiceInst";
            this.lblServiceInst.Size = new System.Drawing.Size(65, 13);
            this.lblServiceInst.TabIndex = 3;
            this.lblServiceInst.Text = "UNKNOWN";
            // 
            // btnSerUninstall
            // 
            this.btnSerUninstall.Location = new System.Drawing.Point(88, 55);
            this.btnSerUninstall.Name = "btnSerUninstall";
            this.btnSerUninstall.Size = new System.Drawing.Size(75, 23);
            this.btnSerUninstall.TabIndex = 1;
            this.btnSerUninstall.Text = "Uninstall";
            this.btnSerUninstall.UseVisualStyleBackColor = true;
            this.btnSerUninstall.Click += new System.EventHandler(this.btnSerUninstall_Click);
            // 
            // bConfig
            // 
            this.bConfig.Location = new System.Drawing.Point(32, 39);
            this.bConfig.Name = "bConfig";
            this.bConfig.Size = new System.Drawing.Size(80, 23);
            this.bConfig.TabIndex = 4;
            this.bConfig.Text = "Configure";
            this.bConfig.UseVisualStyleBackColor = true;
            this.bConfig.Click += new System.EventHandler(this.bConfig_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tServiceControl);
            this.tabControl1.Controls.Add(this.tLog);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(437, 256);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 5;
            this.tabControl1.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl1_Selected);
            // 
            // tServiceControl
            // 
            this.tServiceControl.BackColor = System.Drawing.SystemColors.Control;
            this.tServiceControl.Controls.Add(this.progressBar1);
            this.tServiceControl.Controls.Add(this.groupBox1);
            this.tServiceControl.Controls.Add(this.groupBox2);
            this.tServiceControl.Controls.Add(this.groupBox3);
            this.tServiceControl.Location = new System.Drawing.Point(4, 22);
            this.tServiceControl.Name = "tServiceControl";
            this.tServiceControl.Padding = new System.Windows.Forms.Padding(3);
            this.tServiceControl.Size = new System.Drawing.Size(429, 230);
            this.tServiceControl.TabIndex = 0;
            this.tServiceControl.Text = "Controls";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bConfig);
            this.groupBox1.Location = new System.Drawing.Point(269, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 100);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Configuration";
            // 
            // tLog
            // 
            this.tLog.BackColor = System.Drawing.SystemColors.Control;
            this.tLog.Controls.Add(this.rtbServiceLog);
            this.tLog.Location = new System.Drawing.Point(4, 22);
            this.tLog.Name = "tLog";
            this.tLog.Padding = new System.Windows.Forms.Padding(3);
            this.tLog.Size = new System.Drawing.Size(429, 230);
            this.tLog.TabIndex = 1;
            this.tLog.Text = "Service Log";
            // 
            // rtbServiceLog
            // 
            this.rtbServiceLog.BackColor = System.Drawing.Color.White;
            this.rtbServiceLog.ContextMenuStrip = this.logContextMenu;
            this.rtbServiceLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbServiceLog.Location = new System.Drawing.Point(3, 3);
            this.rtbServiceLog.Name = "rtbServiceLog";
            this.rtbServiceLog.ReadOnly = true;
            this.rtbServiceLog.Size = new System.Drawing.Size(423, 224);
            this.rtbServiceLog.TabIndex = 0;
            this.rtbServiceLog.Text = "";
            // 
            // logContextMenu
            // 
            this.logContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
            this.logContextMenu.Name = "logContextMenu";
            this.logContextMenu.Size = new System.Drawing.Size(114, 26);
            // 
            // refreshToolStripMenuItem
            // 
            this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
            this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.refreshToolStripMenuItem.Text = "Refresh";
            this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            this.fileSystemWatcher1.Created += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            this.fileSystemWatcher1.Changed += new System.IO.FileSystemEventHandler(this.fileSystemWatcher1_Changed);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(6, 206);
            this.progressBar1.MarqueeAnimationSpeed = 5;
            this.progressBar1.Maximum = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(417, 23);
            this.progressBar1.Step = 2;
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 6;
            // 
            // timer2
            // 
            this.timer2.Interval = 1500;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(437, 256);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(445, 290);
            this.Name = "Form1";
            this.Text = "XMTuner Service Control";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tServiceControl.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tLog.ResumeLayout(false);
            this.logContextMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblServiceStat;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnSerRestart;
        private System.Windows.Forms.Button btnSerStop;
        private System.Windows.Forms.Button btnSerStart;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button btnSerInstall;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblServiceInst;
        private System.Windows.Forms.Button btnSerUninstall;
        private System.Windows.Forms.Button bConfig;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tServiceControl;
        private System.Windows.Forms.TabPage tLog;
        private System.Windows.Forms.RichTextBox rtbServiceLog;
        private System.Windows.Forms.ContextMenuStrip logContextMenu;
        private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Timer timer2;
    }
}

