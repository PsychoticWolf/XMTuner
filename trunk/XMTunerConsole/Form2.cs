using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace XMTuner
{
    public partial class Form2 : Form
    {
        bool isLoggedIn = false;
        private CacheManager cache;

        public Form2(CacheManager cache, Boolean tLoggedIn)
        {
            isLoggedIn = tLoggedIn;
            this.cache = cache;

            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            LoadConfig();
            if (isLoggedIn)
            {
                bUpdateLineup.Enabled = true;
            }
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            writeConfig();
            Close();
        }

        private void linkTversity_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://tversity.com/");
        }

        private void bUpdateLineup_Click(object sender, EventArgs e)
        {
            cache.purgeCache();

            bUpdateLineup.Enabled = false;
            bUpdateLineup.Text = "Update Pending...";
        }

        private void LoadConfig()
        {
            // Get configuration and set the form fields as needed:
            NameValueCollection config = new configMan().getConfig();
            txtUser.Text = config["username"];
            txtPassword.Text = config["password"];
            txtPort.Text = config["port"];
            chkBitrate.Checked = Convert.ToBoolean(config["bitrate"]);
            chkAutologin.Checked = Convert.ToBoolean(config["autologin"]);
            chkMMS.Checked = Convert.ToBoolean(config["isMMS"]);
            txtTversity.Text = config["Tversity"];
            txtHostname.Text = config["hostname"];
            boxNetwork.SelectedItem = config["network"];

            //Defaults
            if (boxNetwork.SelectedItem == null) { boxNetwork.SelectedItem = "XM"; }
        }

        private void writeConfig()
        {
            configMan configuration = new configMan();
            NameValueCollection config = new NameValueCollection();
            config.Add("username", txtUser.Text);
            config.Add("password", txtPassword.Text);
            config.Add("port", txtPort.Text);
            config.Add("bitrate", chkBitrate.Checked.ToString());
            config.Add("autologin", chkAutologin.Checked.ToString());
            config.Add("isMMS", chkMMS.Checked.ToString());
            config.Add("Tversity", txtTversity.Text);
            config.Add("hostname", txtHostname.Text);
            config.Add("network", boxNetwork.SelectedItem.ToString());

            configuration.writeConfig(config);
        }
    }
}
