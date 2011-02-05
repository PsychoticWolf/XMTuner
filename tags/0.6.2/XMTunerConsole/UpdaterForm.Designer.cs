namespace XMTuner
{
    partial class UpdaterForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdaterForm));
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dnldStatus = new System.Windows.Forms.Label();
            this.btnStartDownload = new System.Windows.Forms.Button();
            this.btnNo = new System.Windows.Forms.Button();
            this.btnAbort = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblNewVersion = new System.Windows.Forms.Label();
            this.lblCurrentVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblDetails = new System.Windows.Forms.Label();
            this.lnkMoreInfo = new System.Windows.Forms.LinkLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(17, 7);
            this.progressBar1.MarqueeAnimationSpeed = 20;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(303, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // dnldStatus
            // 
            this.dnldStatus.AutoSize = true;
            this.dnldStatus.Location = new System.Drawing.Point(17, 37);
            this.dnldStatus.Name = "dnldStatus";
            this.dnldStatus.Size = new System.Drawing.Size(24, 13);
            this.dnldStatus.TabIndex = 1;
            this.dnldStatus.Text = "Idle";
            // 
            // btnStartDownload
            // 
            this.btnStartDownload.Location = new System.Drawing.Point(123, 146);
            this.btnStartDownload.Name = "btnStartDownload";
            this.btnStartDownload.Size = new System.Drawing.Size(75, 23);
            this.btnStartDownload.TabIndex = 2;
            this.btnStartDownload.Text = "Yes";
            this.btnStartDownload.UseVisualStyleBackColor = true;
            this.btnStartDownload.Click += new System.EventHandler(this.btnStartDownload_Click);
            // 
            // btnNo
            // 
            this.btnNo.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.btnNo.Location = new System.Drawing.Point(205, 146);
            this.btnNo.Name = "btnNo";
            this.btnNo.Size = new System.Drawing.Size(75, 23);
            this.btnNo.TabIndex = 3;
            this.btnNo.Text = "No";
            this.btnNo.UseVisualStyleBackColor = true;
            this.btnNo.Click += new System.EventHandler(this.btnNo_Click);
            // 
            // btnAbort
            // 
            this.btnAbort.Enabled = false;
            this.btnAbort.Location = new System.Drawing.Point(326, 8);
            this.btnAbort.Name = "btnAbort";
            this.btnAbort.Size = new System.Drawing.Size(56, 22);
            this.btnAbort.TabIndex = 4;
            this.btnAbort.Text = "Cancel";
            this.btnAbort.UseVisualStyleBackColor = true;
            this.btnAbort.Click += new System.EventHandler(this.btnAbort_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "A new version of XMTuner is available.";
            // 
            // lblNewVersion
            // 
            this.lblNewVersion.AutoSize = true;
            this.lblNewVersion.Location = new System.Drawing.Point(38, 25);
            this.lblNewVersion.Name = "lblNewVersion";
            this.lblNewVersion.Size = new System.Drawing.Size(70, 13);
            this.lblNewVersion.TabIndex = 6;
            this.lblNewVersion.Text = "New Version:";
            // 
            // lblCurrentVersion
            // 
            this.lblCurrentVersion.AutoSize = true;
            this.lblCurrentVersion.Location = new System.Drawing.Point(38, 41);
            this.lblCurrentVersion.Name = "lblCurrentVersion";
            this.lblCurrentVersion.Size = new System.Drawing.Size(82, 13);
            this.lblCurrentVersion.TabIndex = 7;
            this.lblCurrentVersion.Text = "Current Version:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(90, 130);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(222, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Would you like to download the new version?";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.dnldStatus);
            this.panel1.Controls.Add(this.btnAbort);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 173);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(403, 53);
            this.panel1.TabIndex = 9;
            // 
            // lblDetails
            // 
            this.lblDetails.AutoEllipsis = true;
            this.lblDetails.Location = new System.Drawing.Point(39, 58);
            this.lblDetails.Name = "lblDetails";
            this.lblDetails.Size = new System.Drawing.Size(352, 70);
            this.lblDetails.TabIndex = 10;
            this.lblDetails.Text = "Details";
            // 
            // lnkMoreInfo
            // 
            this.lnkMoreInfo.AutoSize = true;
            this.lnkMoreInfo.Enabled = false;
            this.lnkMoreInfo.Location = new System.Drawing.Point(314, 157);
            this.lnkMoreInfo.Name = "lnkMoreInfo";
            this.lnkMoreInfo.Size = new System.Drawing.Size(86, 13);
            this.lnkMoreInfo.TabIndex = 11;
            this.lnkMoreInfo.TabStop = true;
            this.lnkMoreInfo.Text = "More Information";
            this.lnkMoreInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // UpdaterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(403, 226);
            this.Controls.Add(this.lnkMoreInfo);
            this.Controls.Add(this.lblDetails);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblCurrentVersion);
            this.Controls.Add(this.lblNewVersion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnNo);
            this.Controls.Add(this.btnStartDownload);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(419, 264);
            this.Name = "UpdaterForm";
            this.Text = "New version available!";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.UpdaterForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label dnldStatus;
        private System.Windows.Forms.Button btnStartDownload;
        private System.Windows.Forms.Button btnNo;
        private System.Windows.Forms.Button btnAbort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblNewVersion;
        private System.Windows.Forms.Label lblCurrentVersion;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblDetails;
        private System.Windows.Forms.LinkLabel lnkMoreInfo;
    }
}