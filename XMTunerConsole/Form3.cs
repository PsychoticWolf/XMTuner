using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace XMTuner
{
    public partial class Form3 : Form
    {
        Int32 channelNum = 0;

        String bitrate;
        Boolean isMMS = false;

        String hostname;
        String port;
        String ip;
        String tversityServer;


        public Form3(Int32 channel, Config cfg)
        {
            InitializeComponent();
            setupURLBuilder(cfg);
            if (channel != 0)
            {
                this.channelNum = channel;
            }
        }

        private void setupURLBuilder(Config cfg)
        {
            //Configuration
            ip = cfg.ip;
            port = cfg.port;
            bitrate = cfg.bitrate;
            isMMS = cfg.useMMS;
            tversityServer = cfg.tversityServer;
            hostname = cfg.hostname;

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

            Config cfg = new Config(true);
            cfg.bitrate = bitrate;
            cfg.useMMS = isMMS;

            if (bitRateBox.SelectedIndex == -1) { altBitrate = bitrate; }

            String host;
            if (hostname.Equals("")) { host = ip; } else { host = hostname; }
            host = host + ":" + port;

            String address = TheConstructor.buildLink(type, host, collectionForAdd, null, channelNum, cfg);
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

                if (!tversityServer.Equals(""))
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
