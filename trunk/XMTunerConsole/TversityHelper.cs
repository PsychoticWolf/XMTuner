using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;


namespace XMTuner
{
    class TversityHelper
    {
        String tversityURL = "";
        Int32 id;
        Boolean feedAdded;
        String lastMessage;
        public Int32 maxitemsperfeed;

        public TversityHelper(String tvURL)
        {
            tversityURL = tvURL;
            feedAdded = getID();
        }

        public String message
        {
            get { return lastMessage; }
        }


        private String getXMTunerURL()
        {
            String ip = "192.168.1.104";
            String port = "19081";

            String url = "http://" + ip + ":" + port + "/feeds/";

            return url;
        }

        public Boolean addFeed()
        {
            //http://192.168.1.102:41952/mediasource/add?transcodingwhen=&featured=Yes&public=No&lookforaudio=true&menus=&tags=&title=XMTest&url=http://192.168.1.104:19081/feeds/&type=audfeed&menuroot=TVersity%20Custom&id=
            String url = "http://" + tversityURL + "/mediasource/add?";
                url += "transcodingwhen=&";
                url += "featured=Yes&";
                url += "public=No&";
                url += "lookforaudio=true&";
                url += "menus=&tags=&";
                url += "title=XMTest&";
                url += "url=" + getXMTunerURL() + "&";
                url += "type=audfeed&";
                url += "menuroot=TVersity%20Custom&";
                url += "&id=";

            URL tAdd = new URL(url);
            tAdd.fetch();
            String reply = tAdd.response();

            return parseResponse(reply);
        }

        public Boolean getID()
        {
            String url = "http://" + tversityURL + "/mediasource/fetchlist?type=audfeed";
            URL tFetch = new URL(url);
            tFetch.fetch();
            String data = tFetch.response();

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(data);
            XmlNodeList list = xmldoc.GetElementsByTagName("result");

            foreach (XmlNode result in list)
            {
                String rURL = result.Attributes["url"].Value;
                if (rURL == getXMTunerURL())
                {
                    Int32 rID = Convert.ToInt32(result.Attributes["id"].Value);
                    id = rID;
                    return true;
                }
            }
            return false;
        }

        public Boolean refresh()
        {
            if (id == 0) { return false; }

            String url = "http://" + tversityURL + "/mediasource/refresh?id=" + id;

            URL tFetch = new URL(url);
            tFetch.fetch();
            String data = tFetch.response();

            return parseResponse(data);
        }

        private bool parseResponse(string data)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(data);
            XmlNodeList list = xmldoc.GetElementsByTagName("response");
            XmlNode response = list[0];

            String status = response.Attributes["status"].Value;
            String message = response.Attributes["message"].Value;

            reportMessage(message);
            if (status.Equals("success"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void reportMessage(string message)
        {
            lastMessage = message;
        }

        public Boolean validate()
        {
            return getSettings();
        }

        public Boolean getSettings()
        {
            String url = "http://" + tversityURL + "/settings/fetch";

            URL tFetch = new URL(url);
            tFetch.fetch();
            String data = tFetch.response();

            if (parseResponse(data) == false) { return false; }

            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(data);
            XmlNodeList list = xmldoc.GetElementsByTagName("result");
            XmlNode config = list[0];

            maxitemsperfeed = Convert.ToInt32(config.Attributes["maxItemsPerFeed"].Value);

            return true;
        }

        public Boolean feed()
        {
            if (id == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
