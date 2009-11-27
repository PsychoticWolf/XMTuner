using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMReaderConsole
{
    public partial class Form2 : Form
    {
        public String user;
        public String pass;
        public String port;
        public bool bitrate;
        public bool autologin;

        public Form2(String usertxt, String userpass, String usrport, bool bitRate, bool autolog)
        {
            user = usertxt;
            pass = userpass;
            port = usrport;
            bitrate = bitRate;
            autologin = autolog;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            txtUser.Text = user;
            txtPassword.Text = pass;
            txtPort.Text = port;
            chkBitrate.Checked = bitrate;
            chkAutologin.Checked = autologin;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            configMan configuration = new configMan();
            configuration.writeConfig(txtUser.Text, txtPassword.Text, txtPort.Text, chkBitrate.Checked, chkAutologin.Checked);
            Close();
        }

        

        private void txtBitrate_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
