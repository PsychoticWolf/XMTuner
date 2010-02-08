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
        bool loadedTversityPanel = false;
        String ip;

        public Form2(CacheManager cache, Boolean tLoggedIn, String ip)
        {
            isLoggedIn = tLoggedIn;
            this.cache = cache;
            this.ip = ip;

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

        private void button1_Click(object sender, EventArgs e)
        {
            tbtnValidate.Enabled = false;
             syncTversityPanel();
        }

        private void syncTversityPanel()
        {
            if (txtTversity.Text.Equals("") || txtTversity.Text.Contains(":") == false)
            { return; }

            tlblEnabled.Text = "Validating...";
            tlblConfig.Text = "";
            tlblFeed.Text = "";

            tversityBox2.Visible = true;
            tversityBox2.Enabled = true;

            TversityHelper tvhelp = new TversityHelper(txtTversity.Text, ip, txtPort.Text);
            Boolean status = tvhelp.validate();

            if (status == true)
            {
                tlblEnabled.Text = "TVersity is running; XMTuner integration enabled.";
                tlblEnabled.ForeColor = System.Drawing.Color.Black;
                tlblEnabled.Enabled = true;

                if (tvhelp.feed())
                {
                    tlblFeed.Text = "XMTuner Feed in TVersity";
                    tlblFeed.ForeColor = System.Drawing.Color.Black;
                    tlblFeed.Enabled = true;

                    tbtnFeed.Text = "Refresh Feed";
                    tbtnFeed.Enabled = true;
                }
                else
                {
                    tlblFeed.Text = "XMTuner Feed Missing from TVersity";
                    tlblFeed.ForeColor = System.Drawing.Color.Red;
                    tlblFeed.Enabled = true;

                    tbtnFeed.Text = "Add Feed";
                    tbtnFeed.Enabled = true;

                    toolTip1.SetToolTip(tlblFeed, "In order to play channels through TVersity, XMTuner's RSS feed (also referred to as a\n"+
                                                  " podcast) must be added to TVersity. XMTuner can try to do this automatically for you,\n"+
                                                  "just click the \"Add Feed\" button.");
                }

                if (tvhelp.maxitemsperfeed == 0 || tvhelp.maxitemsperfeed > 120)
                {
                    tlblConfig.Text = "Configuration Check: Passed";
                    tlblConfig.ForeColor = System.Drawing.Color.Black;
                    tlblConfig.Enabled = true;
                }
                else
                {
                    tlblConfig.Text = "Configuration Check: Failed\nMaxItemsPerFeed value set too low.";
                    tlblConfig.ForeColor = System.Drawing.Color.Red;
                    tlblConfig.Enabled = true;

                    toolTip1.SetToolTip(tlblConfig, "TVersity includes a limit on the number of items it will display for a single feed.\n"+
                                                    "This limit, by default, is lower than the number of channels XMTuner's feed has.\n" +
                                                    "Therefore, to work correctly, this limit needs to be raised above (approx.) 130, or\n" +
                                                    "set to 0 to disable this limit (recommended).");
                }

            }
            else
            {
                tlblEnabled.Text = "XMTuner cannot find TVersity";
                tlblEnabled.ForeColor = System.Drawing.Color.Red;
                tlblEnabled.Enabled = true;
            }
            tbtnValidate.Enabled = true;
            loadedTversityPanel = true;
        }

        private void tbtnFeed_Click(object sender, EventArgs e)
        {
            tbtnFeed.Enabled = false;
            backgroundWorker1.RunWorkerAsync();
        }

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            if (e.TabPage.Equals(tabPage3))
            {
                enableTVersityValidateBtn();
                if(!loadedTversityPanel) {
                    syncTversityPanel();
                }
            }
        }

        private void enableTVersityValidateBtn()
        {
            if (txtTversity.Text.Equals("") || txtTversity.Text.Contains(":") == false)
            { return; }
            tbtnValidate.Enabled = true;
        }

        private void txtTversity_TextChanged(object sender, EventArgs e)
        {
            enableTVersityValidateBtn();
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            TversityHelper tvhelp = new TversityHelper(txtTversity.Text, ip, txtPort.Text);
            if (tbtnFeed.Text.Equals("Add Feed"))
            {
                tvhelp.addFeed();
            }
            else
            {
                tvhelp.refresh();
            }

            e.Result = tvhelp;
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            TversityHelper tvhelp = e.Result as TversityHelper;
            MessageBox.Show(tvhelp.message, "TVersity says...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            syncTversityPanel();
            tbtnFeed.Enabled = true;
        }
    }
}
