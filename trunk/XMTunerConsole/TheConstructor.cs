using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace XMTuner
{
    static class TheConstructor
    {
        public static String buildLink(String linkType, String serverHost, NameValueCollection URLparams, String useragent, Int32 num, NameValueCollection config)
        {
            String link;
            //Linktype: stream or feed
            if (linkType.Equals("stream"))
            {
                String type = getStreamType(useragent, URLparams, config);
                String bitrate = getBitRate(URLparams, config);
                if (type.Equals("mp3") || type.Equals("wav"))
                {
                    URLparams["streamtype"] = type; //turn on transcoder
                    type = "http"; //Force http type.
                }
                String streamtype = "";
                if (URLparams.Get("streamtype") != null)
                {
                    streamtype = URLparams["streamtype"] + "/";
                }
                if (num == 0)
                {
                    num = Convert.ToInt32(URLparams["num"]);
                }

                //if needed it'll be "streamtype/"
                link = type + "://" + serverHost + "/streams/" + num + "/" + streamtype + bitrate;

            }
            else if (linkType.Equals("playlist"))
            {
                String type = URLparams["type"].ToLower(); //XXX should be getPlaylistType()
                String bitrate = getBitRate(URLparams, config);
                link = "http://" + serverHost + "/playlists/"+"?type=" + type + "&bitrate=" + bitrate;
            }
            else
            {
                String category = ""; //XXX not implemented yet.
                String type = getStreamType(null, URLparams, config);
                String bitrate = getBitRate(URLparams, config);
                link = "http://" + serverHost + "/feeds/" + category + "?type=" + type + "&bitrate=" + bitrate;
            }
            return link;
        }

        public static String getBitRate(NameValueCollection URLparams, NameValueCollection config)
        {
            //Default Bitrate from Config
            String bitrate = config["bitrate"];

            //Process Override Bitrate (validate)
            if (URLparams.Get("bitrate") != null)
            {
                if (URLparams["bitrate"].ToLower().Equals("high") ||
                    URLparams["bitrate"].ToLower().Equals("low"))
                {
                    bitrate = URLparams["bitrate"].ToLower();
                }
            }

            return bitrate;
        }

        private static String getStreamType(String useragent, NameValueCollection URLparams, NameValueCollection config)
        {
            //Possible return values: rtsp, http, mms, mp3
            if (useragent == null) { useragent = ""; } //support non-web calls
            String type;

            //Do we default to mms in the absense of any overrides?
            Boolean UseMMS = Convert.ToBoolean(config["isMMS"]);
            if (UseMMS)
            {
                type = "mms";
            }
            else
            {
                type = "http";
            }

            //RTSP (always for TVersity only)
            if (useragent.Contains("TVersity"))
            {
                return "rtsp";
            }

            //We're told a type.. validate it...
            if (URLparams.Get("type") != null)
            {
                if (URLparams.Get("type").ToLower().Equals("mms"))
                {
                    return "mms";
                }
                else if (URLparams.Get("type").ToLower().Equals("http"))
                {
                    return "http";
                }
                else if (URLparams.Get("type").ToLower().Equals("mp3"))
                {
                    return "mp3";
                }
            }
            return type;
        }

    }
}
