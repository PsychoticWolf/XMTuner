namespace XMTuner
{
    partial class Form3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form3));
            this.typeBox = new System.Windows.Forms.ComboBox();
            this.bitRateBox = new System.Windows.Forms.ComboBox();
            this.addressBox = new System.Windows.Forms.TextBox();
            this.cpyToClip = new System.Windows.Forms.Button();
            this.protocolBox = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bClose = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // typeBox
            // 
            this.typeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.typeBox.FormattingEnabled = true;
            this.typeBox.Items.AddRange(new object[] {
            "Channel",
            "Feed",
            "Playlist"});
            this.typeBox.Location = new System.Drawing.Point(16, 50);
            this.typeBox.Name = "typeBox";
            this.typeBox.Size = new System.Drawing.Size(64, 21);
            this.typeBox.TabIndex = 18;
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
            this.bitRateBox.Location = new System.Drawing.Point(152, 50);
            this.bitRateBox.Margin = new System.Windows.Forms.Padding(2);
            this.bitRateBox.Name = "bitRateBox";
            this.bitRateBox.Size = new System.Drawing.Size(64, 21);
            this.bitRateBox.TabIndex = 17;
            this.bitRateBox.SelectedIndexChanged += new System.EventHandler(this.makeAddress);
            // 
            // addressBox
            // 
            this.addressBox.Location = new System.Drawing.Point(220, 51);
            this.addressBox.Name = "addressBox";
            this.addressBox.Size = new System.Drawing.Size(278, 20);
            this.addressBox.TabIndex = 14;
            // 
            // cpyToClip
            // 
            this.cpyToClip.Location = new System.Drawing.Point(503, 50);
            this.cpyToClip.Margin = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.cpyToClip.Name = "cpyToClip";
            this.cpyToClip.Size = new System.Drawing.Size(42, 23);
            this.cpyToClip.TabIndex = 16;
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
            "HTTP"});
            this.protocolBox.Location = new System.Drawing.Point(84, 50);
            this.protocolBox.Name = "protocolBox";
            this.protocolBox.Size = new System.Drawing.Size(64, 21);
            this.protocolBox.TabIndex = 15;
            this.protocolBox.SelectedIndexChanged += new System.EventHandler(this.makeAddress);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bClose);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(559, 146);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "URL Builder";
            // 
            // bClose
            // 
            this.bClose.Location = new System.Drawing.Point(236, 111);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(75, 23);
            this.bClose.TabIndex = 20;
            this.bClose.Text = "Close";
            this.bClose.UseVisualStyleBackColor = true;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // Form3
            // 
            this.AcceptButton = this.bClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 146);
            this.Controls.Add(this.typeBox);
            this.Controls.Add(this.bitRateBox);
            this.Controls.Add(this.addressBox);
            this.Controls.Add(this.cpyToClip);
            this.Controls.Add(this.protocolBox);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form3";
            this.Text = "URL Builder";
            this.Load += new System.EventHandler(this.makeAddress);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox typeBox;
        private System.Windows.Forms.ComboBox bitRateBox;
        private System.Windows.Forms.TextBox addressBox;
        private System.Windows.Forms.Button cpyToClip;
        private System.Windows.Forms.ComboBox protocolBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bClose;
    }
}