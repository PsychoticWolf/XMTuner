using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.Specialized;

namespace XMTuner
{
    static class TheConstructor
    {
        public static String buildLink(String linkType, String serverHost, NameValueCollection URLparams, String useragent, Int32 num, Config cfg)
        {
            String link;
            //Linktype: stream or feed
            if (linkType.Equals("stream"))
            {
                String type = getStreamType(useragent, URLparams, cfg);
                String bitrate = getBitRate(URLparams, cfg);
                String format = getFormat(URLparams);
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
                if (format != null)
                {
                    type = "http";
                    format = "/" + format;
                }
                if (num == 0)
                {
                    num = Convert.ToInt32(URLparams["num"]);
                }

                //if needed it'll be "streamtype/"
                link = type + "://" + serverHost + "/streams/" + num + "/" + streamtype + bitrate + format;

            }
            else if (linkType.Equals("playlist"))
            {
                String type = URLparams["type"].ToLower(); //XXX should be getPlaylistType()
                String bitrate = getBitRate(URLparams, cfg);
                link = "http://" + serverHost + "/playlists/"+"?type=" + type + "&bitrate=" + bitrate;
            }
            else
            //Feeds
            {
                String category = ""; //XXX not implemented yet.
                String type = getStreamType(null, URLparams, cfg);
                String bitrate = getBitRate(URLparams, cfg);
                link = "http://" + serverHost + "/feeds/" + category + "?type=" + type + "&bitrate=" + bitrate;
            }
            return link;
        }

        public static String getBitRate(NameValueCollection URLparams, Config cfg)
        {
            //Default Bitrate from Config
            String bitrate = cfg.bitrate;

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

        private static String getStreamType(String useragent, NameValueCollection URLparams, Config cfg)
        {
            //Possible return values: rtsp, http, mms, mp3
            if (useragent == null) { useragent = ""; } //support non-web calls
            String type;

            //Do we default to mms in the absense of any overrides?
            if (cfg.useMMS)
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
                else if (URLparams.Get("type").ToLower().Equals("wav"))
                {
                    return "wav";
                }
                else if (URLparams.Get("type").ToLower().Equals("m3u"))
                {
                    return "m3u";
                }
                else if (URLparams.Get("type").ToLower().Equals("asx"))
                {
                    return "asx";
                }
            }
            return type;
        }

        private static String getFormat(NameValueCollection URLparams)
        {
            String format = null;
            if (URLparams["type"] != null)
            {
                if (URLparams["type"].ToLower().Equals("asx") ||
                    URLparams["type"].ToLower().Equals("m3u"))
                {
                    format = URLparams["type"];
                }

            }

            if (URLparams["format"] != null)
            {
                if (URLparams["format"].ToLower().Equals("m3u") ||
                    URLparams["format"].ToLower().Equals("asx"))
                {
                    format = URLparams["format"];
                }
            }
            return format;
        }

    }
}
