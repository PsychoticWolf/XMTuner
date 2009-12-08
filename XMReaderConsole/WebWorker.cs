using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Web;

namespace XMReaderConsole
{
    class WebWorker
    {
        NameValueCollection config = new NameValueCollection();
        XMTuner myTuner;
        public WebWorker(XMTuner xmTuner)
        {
            myTuner = xmTuner;

            //Set up configuration values
            config = new NameValueCollection();
            config.Add("isMMS", myTuner.isMMS.ToString());
            config.Add("bitrate", myTuner.bitrate);
            config.Add("hostname", myTuner.hostname);
        }

        public NameValueCollection parseStreamURL(string methodURL)
        {
            NameValueCollection collection = new NameValueCollection();
            String streamtype = null;
            String[] args = methodURL.Split('/');
            if (args.Length > 0)
            {
                String ChanNum = args[0];
                String bitrate = "";
                if (args.Length > 1)
                {
                    if (args[1].ToLower().Equals("mp3"))
                    {
                        streamtype = "mp3";
                        if (args.Length > 2)
                        {
                            bitrate = args[2];
                        }
                    }
                    else
                    {
                        bitrate = args[1];
                    }
                }
                collection.Add("num", ChanNum);
                collection.Add("bitrate", bitrate);
                collection.Add("streamtype", streamtype); // Null (default) or mp3 for transcoded version

                return collection;
            }
            return null;

        }

        private String getBitrateDesc(String bitrate)
        {
            String bitrate_desc = "";
            if (bitrate.Equals("high"))
            {
                bitrate_desc = "128 Kbps";
            }
            else if (bitrate.Equals("low"))
            {
                bitrate_desc = "32 Kbps";
            }
            return bitrate_desc;
        }

        private String getHostName(String serverHost)
        {
            String hostname;
            if (config.Get("hostname") != null && config.Get("hostname").Contains(":"))
            {
                hostname = config["hostname"];
            }
            else
            {
                hostname = serverHost;
            }
            return hostname;
        }

        public NameValueCollection DoStream(NameValueCollection streamParams, String fullurl, String serverHost)
        {
            NameValueCollection streamCollection = new NameValueCollection();
            String msg;
            String tversityHost = myTuner.tversityHost;
            String isErr = "false";
            int ChanNum = Convert.ToInt32(streamParams["num"]);
            String bitrate = TheConstructor.getBitRate(streamParams, config);
            serverHost = getHostName(serverHost);
            if (streamParams.Get("streamtype") != null)
            {
                fullurl = fullurl.Replace("/mp3","");
                String redirectURL = HttpUtility.UrlEncode("rtsp://" + serverHost + fullurl);
                myTuner.output("MP3 Request forwarding to" + redirectURL, "debug");
                msg = "http://" + tversityHost + "/geturl/stream.mp3?type=audio/x-ms-wma&ttype=audio/mpeg&url=" + redirectURL+"&ext=.mp3";
                myTuner.output(msg, "debug");
            }
            else
            {
                msg = myTuner.play(ChanNum, bitrate);
            }
            if (msg == null)
            {
                isErr = "true";
                msg = "<html><body><h1>Service Unavailable</h1> " +
                      "<p>Stream Error: Unable to fetch XM stream URL. (XMRO Not Logged In or Down)</p>" +
                      "</body></html>";
            }
            streamCollection.Add("msg", msg);
            streamCollection.Add("isErr", isErr);
            return streamCollection;
        }

        public MemoryStream DoFeed(string methodURL, NameValueCollection URLparams, String useragent, String serverHost)
        {
            String bitrate_desc = getBitrateDesc(TheConstructor.getBitRate(URLparams, config));
            serverHost = getHostName(serverHost);
            myTuner.output("Incoming Feed Request: XM Channels (All - " + bitrate_desc + ")", "info");
            List<XMChannel> list = myTuner.getChannels();
            MemoryStream OutputStream = CreateXMFeed(list, URLparams, serverHost, useragent);

            return OutputStream;
        }

        public String DoNowPlaying(String serverHost, NameValueCollection URLparams)
        {
            Boolean UseMMS = myTuner.isMMS;
            serverHost = getHostName(serverHost);
            String bitrate = TheConstructor.getBitRate(URLparams, config);

            int nowPlayingNum = myTuner.lastChannelPlayed;
            List<XMChannel> list = myTuner.getChannels();


            String NowPlayingPage = "<!DOCTYPE html PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html401/loose.dtd\">" +
                                    "<html>\n<head>\n\t<title>XM Tuner - What's On</title>\n" +
                                    "<link rel=\"alternate\" type=\"application/rss+xml\" title=\"RSS\" href=\"/feeds/\" />\n" +
                                    "</head>\n<body style=\"margin: 0px; font-family: Arial; font-size: 10pt;\">\n" +
                                    "<style type=\"text/css\">a , a:visited { color: blue; text-decoration: none; } a:hover { color: orange; }</style>\n";
            //NowPlaying
            XMChannel npChannel = myTuner.Find(nowPlayingNum);
            if (npChannel.album == null) { npChannel.album = ""; }

            NowPlayingPage += "<div style=\"float: right;\">\n<table style=\"min-width: 300px; border: 1px solid #666; margin: 5px; padding: 0px 3px; -moz-border-radius: 10px;\">" +
                              "<tr><td style=\"border-bottom: 1px solid blue; font-size: 18pt; font-weight: bold;\" colspan=\"2\">Now Playing<br></td></tr>\n";
            if (npChannel.num != 0)
            {
                NowPlayingPage += "<tr><td style=\"height: 1em; padding-left: 5px;\">XM " + npChannel.num + " - " + npChannel.name + "</td>";
                    
                        if (npChannel.logo != null) {
                            NowPlayingPage += "<td rowspan=3 width=\"138\"><a href=\""+npChannel.url+"\" target=\"_blank\"><img src=\"" + npChannel.logo + "\" border=0 alt=\"\" width=\"138\" height=\"50\" align=\"right\"></a></td>";
                        }
                        NowPlayingPage += "</tr>" +
                                  "<tr><td style=\"padding-left: 5px;\" valign=\"top\">" + npChannel.artist + " - " + npChannel.song + "</td></tr>";
                if (!npChannel.album.Equals(""))
                {
                    NowPlayingPage += "<tr><td style=\"padding-left: 25px; color: #666;\">" + npChannel.album + "</td></tr>\n";
                }
            }
            else
            {
                NowPlayingPage += "<tr><td colspan=\"2\" style=\"color: #666; text-align: center;\"><p>Nothing Yet... Play a Channel</p></td></tr>\n";
            }
            NowPlayingPage += "</table>\n</div>" +
                              "<h1 style=\"margin: 0px; padding: 25px; font-size: 26pt;\">XM Tuner - What's On</h1>";

            //Full Channel List
            NowPlayingPage += "<table align=\"center\" style=\"width: 95%; border: 1px solid black;\">" +
                                "<tr><td colspan=6 style=\"border-bottom: 1px solid blue; font-size: 18pt; font-weight: bold;\">" +
                                "<div style=\"float: right; font-size: 10pt;\">" +
                                "<a style=\"text-decoration: none;\" href=\"/feeds/\" title=\"RSS 2.0 Feed for TVersity Media Server\"><img border=\"0\" width=\"16\" height=\"16\" alt=\"\" src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAKOSURBVHjadJNNSFRRGIbfc+6dO6OOOplDykSZaRCtnKRc+ANRUBFEm0gicxG0bGoVhERRYIvIjRAtW+UmW5QQQS6qRWQSWJRaUcjkT0LiVWfm/pzTd869M5LShe+eufC973m+n2Fj55KJymTt9ZgVy3AGMHoxvvEs/qZgDL4AlhbsAfv34g1TiRPxRMZbWwU3GQzO9clNhGcYBpkY4UlGiWRd5scnMvx5tUWano9IMoXy1pOUBPgz4xC/xv8r1hSexNKiB1Nhq6RIbQoVhy6i+MiCDffDEPyJITBvZZPYLwiQFFzVZ0Q45Moc8q8fwP34FGJ5FixaCevgBZT1DMNs7Nok9iikL8GyfWkZ3VgvhbljP6yOy2BbmzWR/+o2vM/PSmLfkchTaAIlsHa1ourSS5Sfvg8r3Q38mYb7+DzE9Ig2MDquAan2kth3AgKu0JSBapLCNranEWnPIHp2GDzZDEE3+1OBiXW4D4LHtViZCB/rBHJuHPmHp+CO3oK0Zyk7DvP4IGR1EwrPb673JX0mELslgvVRYW0O8usIvCe9AboyOdIPn1Ug9+Kupihr69ZiZSJFsYRwztbRO4icGARv6IT3ZkCTsKp68J2dyE2MQuZtsFgllbZHTyMsAVqsaldCVt8CtvsYPHsZzvtH+tZIc5e+sfBtLOhFYytRFAlYuGGED2clGNn8lO62m53U37y6XmM7M8E3onEIIpCSLp/vPyC31Bp6SUSsjmIbnO9jpVEZqbROzn15F5RDZs5CFs58FmaNBTZ5Ze+9hn11Genl/1mS0qjChumaCVvo2iViNXHkuD1g9Daxt7lVEQPMNpcSdah1pQb5kqho4yVXQc2iacHiMMpNFPia/jv/FWAAUTVTOunExzkAAAAASUVORK5CYII%3D\">" +
                                "&nbsp;RSS Feed</a></div>" +
                                "XM Channel Guide" +

                                "</td></tr>\n" +
                                "<tr><th></th><th>Channel</th><th>Artist</th><th>Song</th><th>Album</th><th></th></tr>\n";
            String mediaurl = "";
            int i = 0;
            foreach (XMChannel channel in list)
            {
                String row_color;
                if (i % 2 == 0) { row_color = "#FFFFC0"; } else { row_color = "#FFFFFF"; }
                mediaurl = TheConstructor.buildLink("stream", serverHost, URLparams, null, channel.num, config);
                if (nowPlayingNum == channel.num)
                {
                    NowPlayingPage += "<tr style=\"background-color: #FFFF00; border-bottom: 1px solid black;\">\n";
                }
                else
                {
                    NowPlayingPage += "<tr bgcolor=\"" + row_color + "\" onMouseOver=\"this.bgColor = '#CCE3E9'\" onMouseOut =\"this.bgColor = '" + row_color + "'\">\n";
                }
                NowPlayingPage += "\t<td style=\"text-align: center;\" nowrap><a href=\"" + channel.url + "\" target=\"_blank\">";
                if (channel.category.ToLower().Contains("talk") || channel.category.ToLower().Contains("sports"))
                {
                    NowPlayingPage += "<div style=\"float: left;\"><img src=\"" + channel.logo_small + "\" border=\"0\" width=\"45\" height=\"40\"></div>";
                }
                else
                {
                    NowPlayingPage += "<div style=\"overflow: hidden; height: 25px; float: left;\"><img src=\"" + channel.logo_small + "\" border=\"0\" width=\"45\" height=\"40\" style=\"position: relative; top: -5px;\"></div>";
                }
                NowPlayingPage += "<span style=\"position: relative; top: 6px; font-size: 8pt;\">" + channel.num + "</span></a></td>\n" +
                                    "\t<td title=\""+channel.desc+"\">" + channel.name + "</td>\n" +
                                    "\t<td>" + channel.artist + "</td>\n" +
                                    "\t<td>" + channel.song + "</td>\n" +
                                    "\t<td>" + channel.album + "</td>\n" +
                                    "\t<td><strong><a href=\"" + mediaurl + "\">Play!</a></strong></td>\n" +
                                    "</tr>\n";
                i++;
            }
            NowPlayingPage += "</table>" +
                                "<hr noshade>\n" +
                                "<p style=\"text-align: right; margin: 0px; color: #666;\">XMTuner</p>"+
                                "</body>\n</html>";
            return (NowPlayingPage);
        }

        private MemoryStream CreateXMFeed(List<XMChannel> list, NameValueCollection URLparams, String serverHost, String useragent)
        {
            String link = TheConstructor.buildLink("feed", serverHost, URLparams, useragent, 0, config);
            //String link = "http://" + serverHost + "/feeds/?bitrate=" + bitrate;
            String bitrate_desc = getBitrateDesc(TheConstructor.getBitRate(URLparams, config));
            
            MemoryStream MemoryStream = new MemoryStream();

            //Create a writer to write XML
            XmlTextWriter writer = new XmlTextWriter(MemoryStream, System.Text.Encoding.UTF8);

            //Use indentation for readability.
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;

            //feed = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
            writer.WriteStartDocument();

            //Write an element (this one is the root).
            //feed += "<rss version=\"2.0\" xmlns:media=\"http://search.yahoo.com/mrss/\" xmlns:upnp=\"urn:schemas-upnp-org:metadata-1-0/upnp/\">\n";

            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteAttributeString("xmlns:media", "http://search.yahoo.com/mrss/");
            writer.WriteAttributeString("xmlns:atom", "http://www.w3.org/2005/Atom");
            writer.WriteAttributeString("xmlns:upnp", "urn:schemas-upnp-org:metadata-1-0/upnp/");

            //<channel>
            writer.WriteStartElement("channel");

            //Write the title element.
            //<title>XM Channels (" + bitrate_desc + ")</title>
            writer.WriteStartElement("title");
            writer.WriteString("XM Channels (" + bitrate_desc + ")");
            writer.WriteEndElement();

            //<link>" + link + "</link>
            writer.WriteStartElement("link");
            writer.WriteString(link);
            writer.WriteEndElement();

            writer.WriteStartElement("atom:link");
            writer.WriteAttributeString("href", link);
            writer.WriteAttributeString("rel", "self");
            writer.WriteAttributeString("type", "application/rss+xml");
            writer.WriteEndElement();

            //<description>English</description>
            writer.WriteStartElement("description");
            writer.WriteString("English");
            writer.WriteEndElement();

            //<language>en-us</language>
            writer.WriteStartElement("language");
            writer.WriteString("en-us");
            writer.WriteEndElement();

            //Channels
            foreach (XMChannel chan in list)
            {
                String media = TheConstructor.buildLink("stream", serverHost, URLparams, useragent, chan.num, config);

                //<item>
                writer.WriteStartElement("item");

                //<title>XM " + chan.num + " - " + chan.name + "</title>
                writer.WriteStartElement("title");
                writer.WriteString("XM " + chan.num + " - " + chan.name);
                writer.WriteEndElement();

                //<link>"+media+"</link>
                writer.WriteStartElement("link");
                writer.WriteString(media);
                writer.WriteEndElement();

                writer.WriteStartElement("guid");
                writer.WriteString(media);
                writer.WriteEndElement();

                //<description>" + chan.desc + "</description>
                writer.WriteStartElement("description");
                writer.WriteString(chan.desc);
                writer.WriteEndElement();

                //<media:content url=\""+media+"\" type=\"audio/x-ms-wma\" medium=\"audio\" />
                writer.WriteStartElement("media:content");
                writer.WriteAttributeString("url", media);
                writer.WriteAttributeString("type", "audio/x-ms-wma");
                writer.WriteAttributeString("medium", "audio");
                writer.WriteEndElement();

                //<upnp:region>United States</upnp:region>
                writer.WriteStartElement("upnp:region");
                writer.WriteString("United States");
                writer.WriteEndElement();

                //</item>
                writer.WriteEndElement();
            }

            //Close channel </channel>
            writer.WriteEndElement();

            //Write the close tag for the root element. </rss>
            writer.WriteEndElement();

            writer.Flush();
            MemoryStream OutputStream = new MemoryStream();
            MemoryStream.WriteTo(OutputStream);

            //Write the XML to file and close the writer.
            writer.Close();

            return OutputStream;
        }

    }
}