using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMTuner
{
    public partial class Form3 : Form
    {
        Int32 channelNum = 0;

        String bitrate;
        bool isMMS = false;

        String hostname;
        String port;
        String ip;
        String tversityHost;


        public Form3()
        {
            InitializeComponent();
            setupURLBuilder();
        }

        public Form3(Int32 channel)
        {
            InitializeComponent();
            setupURLBuilder();
            if (channel != 0)
            {
                this.channelNum = channel;
            }
        }

        private void setupURLBuilder()
        {
            configMan cfg = new configMan();
            NameValueCollection config = cfg.getConfig();

            ip = configMan.getLocalIP();
            port = config["port"];
            bitrate = cfg.getConfigItem("bitrate");
            isMMS = cfg.getConfigItemAsBoolean("isMMS");
            tversityHost = config["Tversity"];
            hostname = config["hostname"];

            // URL Builder
            typeBox.SelectedItem = "Channel";
            if (isMMS) { protocolBox.SelectedItem = "MMS"; } else { protocolBox.SelectedItem = "HTTP"; }
            if (bitrate.Equals("high")) { bitRateBox.SelectedItem = "High"; } else { bitRateBox.SelectedItem = "Low"; }
        }

        private String getChannelAddress(String channelString, String protocol, String altBitrate)
        {
            if (channelNum == 0) { return ""; }
            return getAddress("stream", protocol, altBitrate, channelNum);
        }

        private String getFeedAddress(String protocol, String altBitrate)
        {
            return getAddress("feed", protocol, altBitrate, 0);
        }

        private String getPlaylistAddress(string protocol, string altBitrate)
        {
            return getAddress("playlist", protocol, altBitrate, 0);
        }

        private String getAddress(String type, String protocol, String altBitrate, Int32 channelNum)
        {
            NameValueCollection collectionForAdd = new NameValueCollection();
            collectionForAdd.Add("type", protocol.ToLower());
            collectionForAdd.Add("bitrate", altBitrate.ToLower());
            NameValueCollection config = new NameValueCollection();
            config.Add("bitrate", bitrate);
            config.Add("isMMS", isMMS.ToString());
            if (bitRateBox.SelectedIndex == -1) { altBitrate = bitrate; }

            String host;
            if (hostname.Equals("")) { host = ip; } else { host = hostname; }
            host = host + ":" + port;

            String address = TheConstructor.buildLink(type, host, collectionForAdd, null, channelNum, config);
            return address;
        }

        private void makeAddress(object sender, EventArgs e)
        {
            if (channelNum == 0) { return; }

            String protocol;
            String channel = channelNum.ToString();
            String altBitrate;
            protocol = (String)protocolBox.SelectedItem;
            altBitrate = (String)bitRateBox.SelectedItem;
            if (typeBox.SelectedItem.ToString().ToLower().Equals("feed"))
            {
                addressBox.Text = getFeedAddress(protocol, altBitrate);
            }
            else if (typeBox.SelectedItem.ToString().ToLower().Equals("playlist"))
            {
                addressBox.Text = getPlaylistAddress(protocol, altBitrate);
            }
            else
            {
                addressBox.Text = getChannelAddress(channel, protocol, altBitrate);
            }
        }

        private void updateTypeList(object sender, EventArgs e)
        {
            if (typeBox.SelectedItem.ToString().ToLower().Equals("playlist"))
            {
                protocolBox.Items.Clear();
                protocolBox.Items.Add("Type:");
                protocolBox.Items.Add("ASX");
                protocolBox.Items.Add("PLS");
                protocolBox.Items.Add("M3U");
                protocolBox.SelectedItem = "ASX";
            }
            else
            {
                protocolBox.Items.Clear();
                protocolBox.Items.Add("Protocol:");
                protocolBox.Items.Add("HTTP");
                protocolBox.Items.Add("MMS");
                protocolBox.Items.Add("ASX");
                protocolBox.Items.Add("M3U");

                if (!tversityHost.Equals(""))
                {
                    protocolBox.Items.Add("MP3");
                    protocolBox.Items.Add("WAV");
                }
                if (isMMS) { protocolBox.SelectedItem = "MMS"; } else { protocolBox.SelectedItem = "HTTP"; }
            }
            makeAddress(sender, e);
        }

        private void cpyToClip_Click(object sender, EventArgs e)
        {
            if (addressBox.Text != "")
            {
                Clipboard.SetText(addressBox.Text);
            }
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            Close();
        }

    }
}
