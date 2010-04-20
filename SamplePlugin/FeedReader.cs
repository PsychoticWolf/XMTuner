using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using XMTuner;
using MediaMallTechnologies.Plugin;

namespace XMTunerPlugin
{
    class FeedReader
    {

        public List<XMChannel> channels = new List<XMChannel>();
        private IPlayOnHost p;

        private String host = "localhost";
        private String port = "19081";

        public FeedReader(IPlayOnHost p)
        {
            this.p = p;
            if (p.Properties["hostname"] != null)
                host = p.Properties["hostname"];

            if (p.Properties["port"] != null)
                port = p.Properties["port"];

            //Fetch feed and turn it into an XMChannel Object...
            String url = "http://" + host + ":" + port + "/feeds/";
            output("Get XMTuner Feed...", "debug");
            URL feedURL = new URL(url);
            output("Fetching: " + url, "debug");
            feedURL.fetch();

            output("Server Response: " + feedURL.getStatusDescription(), "debug");
            if (feedURL.isSuccess)
            {
                setChannels(feedURL.response());
            }
            feedURL.close();
            output("XMTuner Feed Load Complete", "debug");
        }

        private void output(String data, String level)
        {
            p.LogMessage(data);
        }

        private void setChannels(String data)
        {
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.LoadXml(data);
            }
            catch (XmlException)
            {
                output("Failed to load XMTuner Feed...", "error");
                return;
            }
            XmlNodeList list = xmldoc.GetElementsByTagName("item");

            foreach (XmlNode channel in list)
            {
                String rawTitle = channel.ChildNodes[0].InnerText;
                    String[] rawT = rawTitle.Split('-');
                    String name = rawT[1].Trim();
                    rawT = rawT[0].Trim().Split(' ');
                    String network = rawT[0];
                    Int32 num = Convert.ToInt32(rawT[1]);
                    
                String link = channel.ChildNodes[1].InnerText;
                String description = channel.ChildNodes[3].InnerText;


                XMChannel tempChannel = new XMChannel("", num, name, description, network, link);
                channels.Add(tempChannel);
            }
        }
    }
}