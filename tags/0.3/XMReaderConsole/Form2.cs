using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace XMReaderConsole
{
    public partial class Form2 : Form
    {
        public String user;
        public String pass;
        public String port;
        public bool bitrate;
        public bool autologin;
        public bool isMMS;
        public String tversityHost;
        public String hostname;
        bool isLoggedIn = false;
        bool isDebug = false;

        public Form2(String usertxt, String userpass, String usrport, bool bitRate, bool autolog, bool MMSON, String tTversityHost, String tHostname, Boolean tLoggedIn, Boolean isDebug)
        {
            user = usertxt;
            pass = userpass;
            port = usrport;
            isMMS = MMSON;
            bitrate = bitRate;
            autologin = autolog;
            tversityHost = tTversityHost;
            hostname = tHostname;
            isLoggedIn = tLoggedIn;
            this.isDebug = isDebug;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtUser.Text = user;
            txtPassword.Text = pass;
            txtPort.Text = port;
            chkBitrate.Checked = bitrate;
            chkAutologin.Checked = autologin;
            chkMMS.Checked = isMMS;
            txtHostname.Text = hostname;
            txtTversity.Text = tversityHost;
            if (isLoggedIn)
            {
                bUpdateLineup.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            configMan configuration = new configMan(isDebug);
            NameValueCollection configOut = new NameValueCollection();
            configOut.Add("username", txtUser.Text);
            configOut.Add("password", txtPassword.Text);
            configOut.Add("port", txtPort.Text);
            configOut.Add("bitrate", chkBitrate.Checked.ToString());
            configOut.Add("autologin", chkAutologin.Checked.ToString());
            configOut.Add("isMMS", chkMMS.Checked.ToString());
            configOut.Add("Tversity", txtTversity.Text);
            configOut.Add("hostname", txtHostname.Text);
                        
            configuration.writeConfig(configOut);

            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://tversity.com/");
        }

        private void bUpdateLineup_Click(object sender, EventArgs e)
        {
            String path = @"channellineup.cache";
            //File.Delete(path);
            File.SetLastWriteTime(path, new DateTime(1985, 1, 1));
            bUpdateLineup.Enabled = false;
            bUpdateLineup.Text = "Update Pending...";
        }
    }
}
