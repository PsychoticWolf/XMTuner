using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
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

        public Form2(String usertxt, String userpass, String usrport, bool bitRate, bool autolog, bool MMSON, String tTversityHost, String tHostname)
        {
            user = usertxt;
            pass = userpass;
            port = usrport;
            isMMS = MMSON;
            bitrate = bitRate;
            autologin = autolog;
            tversityHost = tTversityHost;
            hostname = tHostname;
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
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            configMan configuration = new configMan();
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

        

        private void txtBitrate_TextChanged(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://tversity.com/");
        }
    }
}
