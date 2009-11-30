﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Xml;
using System.IO;
using System.Text;

namespace XMReaderConsole
{
    class WebListner
    {
        private static System.Threading.AutoResetEvent listenForNextRequest = new System.Threading.AutoResetEvent(false);
        String port;
        HttpListener theServer = new HttpListener();
        String prefix;
        XMTuner myTuner;
 
        String serverHost;
        
        public bool isRunning = false;


        public WebListner(XMTuner tuner, String newport)
        {
            if (Convert.ToInt32(newport) < 2)
            {
                port = "19081";
            }
            else
            {
                port = newport;
            }

            myTuner = tuner;
            prefix = "http://*:" + port + "/";
        }

        public void start()
        {
            myTuner.output("Starting server...", "info");
            theServer.Prefixes.Clear();
            theServer.Prefixes.Add(prefix);
            try
            {
                theServer.Start();
                isRunning = true;
                myTuner.output("Server started", "info");
                myTuner.output("Listening on port " + port, "info");
            }
            catch (HttpListenerException e)
            {
                isRunning = false;
                myTuner.output("Server failed to start (Port already in use?)", "error");
                myTuner.output("Check your settings or close the other application using the port", "error");
                myTuner.output("Error " + e.ErrorCode + ": " + e.Message, "debug");
                return;
            }
            System.Threading.ThreadPool.QueueUserWorkItem(listen);
        }

        public void stop()
        {
            theServer.Close();
            isRunning = false;
            myTuner.output("Server stopped", "info");
        }

        private void listen(object state)
        {
            while (theServer.IsListening)
            {
                IAsyncResult result = theServer.BeginGetContext(new AsyncCallback(ListenerCallback), theServer);
                listenForNextRequest.WaitOne();
            }
        }

        private void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener) result.AsyncState;

            if (listener == null)
            {
                return;
            }

            HttpListenerContext context = null;
            try
            {
                context = listener.EndGetContext(result);
                listenForNextRequest.Set();
            }
            catch (ObjectDisposedException)
            {
                listenForNextRequest.Set();
                return;
            }

            if (context == null)
            {
                return;
            }

            ProcessRequest(context);
        }

        protected void ProcessRequest(HttpListenerContext context)
        {
            //do work
            HttpListenerRequest request = context.Request;
            serverHost = request.UserHostName;
            String requestURL = request.Url.PathAndQuery;
            myTuner.output("Incoming Request: (Source: " + request.RemoteEndPoint + ") " + request.HttpMethod + " - " + requestURL, "debug");

            char[] seperator = new char[] {'/'};
            String[] parsedURL = requestURL.Split(seperator, 2, StringSplitOptions.RemoveEmptyEntries);
            int sizeofURL = parsedURL.Length;

            String baseURL;
            String methodURL = "";
            if (sizeofURL == 0)
            {
                baseURL = "/";
            }
            else
            {
                baseURL = parsedURL[0];
                if (baseURL.StartsWith("?"))
                {
                    methodURL = baseURL;
                    baseURL = "/";
                }

                if (sizeofURL > 1)
                {
                    methodURL = parsedURL[1];
                }
            }

            if (baseURL.Equals("/"))
            {
                String responseString;
                NameValueCollection URLParams = request.QueryString;
                myTuner.output("Incoming 'What's On' Request", "info");
                responseString = DoNowPlaying(serverHost, URLParams);
                SendRequest(context, null, responseString, "text/html", false, HttpStatusCode.OK);

            }
            else  if (baseURL.Equals("streams"))
            {
                // Parse URL
                String[] streamParams = parseStreamURL(methodURL);

                //Validate Channel
                string chanName = myTuner.checkChannel(Convert.ToInt32(streamParams[0]));
                if (!chanName.Equals(""))
                {
                    myTuner.output("Incoming Stream Request for XM" + streamParams[0] + " - " + chanName + "", "info");
                    //Do Action for Stream
                    string responseString = DoStream(streamParams);
                    if (responseString == null)
                    {
                        String errorMsg = "<html><body><h1>Service Unavailable</h1> "+
                                          "<p>Stream Error: Unable to fetch XM stream URL. (XMRO Not Logged In or Down)</p>" +
                                          "</body></html>";
                        SendRequest(context, null, errorMsg, "text/html", false, HttpStatusCode.ServiceUnavailable);
                    }
                    else
                    {
                        SendRequest(context, null, responseString, "audio/x-ms-wma", true, HttpStatusCode.OK);
                    }
                }
                else
                {
                    myTuner.output("Incoming Stream Request for Unknown XM Channel", "error");
                    SendRequest(context, null, "Invalid Channel Stream Request", "text/plain", false, HttpStatusCode.OK);
                }
            }
            else if (baseURL.Equals("feeds"))
            {
                //Redirect legacy lineup.xml URL from uXM
                if (methodURL.StartsWith("lineup.xml"))
                {
                    String destinationurl = "/" + baseURL + "/";
                    SendRequest(context, null, destinationurl, "text/plain", true, HttpStatusCode.Found);
                    return;
                }
                //Do Action for Feeds
                NameValueCollection URLParams = request.QueryString;
                MemoryStream stream = DoFeed(methodURL, URLParams, request.UserAgent);
                SendRequest(context, stream, null, "text/xml;charset=UTF-8", false, HttpStatusCode.OK);
            }
            else
            {
                string responseString = "<HTML><BODY>Unknown Request</BODY></HTML>";
                SendRequest(context, null, responseString, "", false, HttpStatusCode.BadRequest);
            }
            myTuner.output("Incoming Request Completed", "debug");
        }

        private void SendRequest(HttpListenerContext context, MemoryStream input, String responseString,
            String contentType, bool redirect, HttpStatusCode status)
        {
            //Process data we're given and write to the outputstream for the client.
            // We accept either an memorystream or a responseString, not both.

            // Obtain a response object (and get its outputstream).
            HttpListenerResponse response = context.Response;
            Stream output = response.OutputStream;

            //Try to fetch the Range header...
            String range = context.Request.Headers.Get("Range");
            Int32 maxrange = -1;
            if (range != null)
            {
                String[] rangeGrp = range.Split('-');
                maxrange = Convert.ToInt32(rangeGrp[1]);
            }

            response.StatusCode = (int) status;

            //Set ContentType... (if provided)
            if (contentType != "")
            {
                response.ContentEncoding = System.Text.Encoding.UTF8;
                response.ContentType = contentType;
            }

            //If redirect is true, responseString is the destination to redirect to.
            // Do the redirect.
            if (redirect)
            {
                response.Redirect(responseString);
            }

            // Construct a response. (for the build from responseString case)
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("");
            if (responseString != null)
            {
                buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            }

            //Write to the output buffer and send it to the client..
            // Hopefully not crashing because the client ran away in the meantime
            try
            {
                if (responseString != null)
                {
                    response.ContentLength64 = buffer.Length;
                    // Get a response stream and write the response to it.
                    output.Write(buffer, 0, buffer.Length);
                }
                else if (input != null)
                {
                    if (maxrange < input.Length && maxrange != -1)
                    {

                    }
                    else
                    {
                        response.ContentLength64 = input.Length;
                        input.WriteTo(output);
                    }
                }
                // You must close the output stream.
                output.Close();

            }
            catch (HttpListenerException e)
            {
                response.Abort();
                myTuner.output("Error - Request Aborted Abnormally (" + e.Message + ")", "debug");
            }


        }

        public String[] parseStreamURL(string methodURL)
        {
            String[] args = methodURL.Split('/');
            if (args.Length > 0)
            {
                //int ChanNum = Convert.ToInt32(args[0]);
                String ChanNum = args[0];
                String bitrate = "";
                if (args.Length > 1)
                {
                    bitrate = args[1];
                }
                //return ChanNum, bitrate;
                String[] streamParams = new String[2];
                streamParams[0] = ChanNum;
                streamParams[1] = bitrate;

                return streamParams;
            }
            return null;

        }

        public string DoStream(string[] streamParams)
        {
            int ChanNum = Convert.ToInt32(streamParams[0]);
            string bitrate;
            if (streamParams[1].Length > 1)
            {
                bitrate = streamParams[1];
            }
            else
            {
                //Fetch default bitrate from config!
                bitrate = myTuner.bitrate;
            }

            string channelURL = myTuner.play(ChanNum, bitrate);
            return channelURL;
        }

        public MemoryStream DoFeed(string methodURL, NameValueCollection URLparams, String useragent)
        {
            Boolean UseMMS = myTuner.isMMS;
            Boolean UseRTSP = false;
            if (useragent.Contains("TVersity"))
            {
                UseRTSP = true;
            }

            if (URLparams.Get("type") != null)
            {
                if (URLparams.Get("type").ToLower().Equals("mms"))
                {
                    UseMMS = true;
                }
                else if (URLparams.Get("type").ToLower().Equals("http"))
                {
                    UseMMS = false;
                }
            }
            String type = "http";
            if (UseRTSP)
            {
                type = "rtsp";
            }
            else
            {
                if (UseMMS)
                {
                    type = "mms";
                }
            }

            String bitrate = myTuner.bitrate;
            if (URLparams.Get("bitrate") != null) 
            {
                if (URLparams["bitrate"].ToLower().Equals("high") ||
                    URLparams["bitrate"].ToLower().Equals("low"))
                {
                    bitrate = URLparams["bitrate"].ToLower();
                }
            }
            String bitrate_desc ="";
            if (bitrate.Equals("high"))
            {
                bitrate_desc = "128 Kbps";
            } else if (bitrate.Equals("low"))
            {
                bitrate_desc = "32 Kbps";
            }

            myTuner.output("Incoming Feed Request: XM Channels (All - "+bitrate_desc+")", "info");

            String link = "http://"+serverHost+"/feeds/?bandwidth="+bitrate;

            List <XMChannel> list = myTuner.getChannels();
            XMLWorker myLittleWorker = new XMLWorker();
            MemoryStream OutputStream = myLittleWorker.CreateXMFeed(list, bitrate, serverHost, type);

            return OutputStream;
        }

        public String DoNowPlaying(String serverHost, NameValueCollection URLparams)
        {
            Boolean UseMMS = myTuner.isMMS;
            String bitrate = myTuner.bitrate; //Get default bitrate
            int nowPlayingNum = myTuner.lastChannelPlayed;
            List<XMChannel> list = myTuner.getChannels();

            if (URLparams.Get("bitrate") != null)
            {
                if (URLparams["bitrate"].ToLower().Equals("high") ||
                    URLparams["bitrate"].ToLower().Equals("low"))
                {
                    bitrate = URLparams["bitrate"].ToLower();
                }
            }
            if (URLparams.Get("type") != null)
            {
                if (URLparams.Get("type").ToLower().Equals("mms"))
                {
                    UseMMS = true;
                }
                else if (URLparams.Get("type").ToLower().Equals("http"))
                {
                    UseMMS = false;
                }
            }
            String NowPlayingPage = "<!DOCTYPE html PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html401/loose.dtd\">";
            NowPlayingPage += "<html>\n<head>\n\t<title>XM Tuner - What's On</title>\n";
            NowPlayingPage += "<link rel=\"alternate\" type=\"application/rss+xml\" title=\"RSS\" href=\"/feeds/\" />\n";
            NowPlayingPage += "</head>\n<body style=\"margin: 0px; font-family: Arial; font-size: 10pt;\">\n";
            NowPlayingPage += "<style type=\"text/css\">a , a:visited { color: blue; text-decoration: none; } a:hover { color: orange; }</style>\n";
            //NowPlaying
            XMChannel npChannel = myTuner.Find(nowPlayingNum);

            NowPlayingPage += "<div style=\"float: right;\">\n<table style=\"min-width: 300px; border: 1px solid #666; margin: 5px; padding: 3px; -moz-border-radius: 10px;\">";
            NowPlayingPage += "<tr><td style=\"border-bottom: 1px solid blue; font-size: 18pt; font-weight: bold;\">Now Playing<br></td></tr>\n";
            if (npChannel.num != 0)
            {
                NowPlayingPage += "<tr><td style=\"padding-left: 5px;\">XM " + npChannel.num + " - " + npChannel.name + "</td></tr>";
                NowPlayingPage += "<tr><td style=\"padding-left: 5px;\">" + npChannel.artist + " - " + npChannel.song + "</td></tr>";
                if (!npChannel.album.Equals(""))
                {
                    NowPlayingPage += "<tr><td style=\"padding-left: 25px; color: #666;\">" + npChannel.album + "</td></tr>\n";
                }
            }
            else
            {
                NowPlayingPage += "<tr><td style=\"color: #666; text-align: center;\"><p>Nothing Yet... Play a Channel</p></td></tr>\n";
            }
            NowPlayingPage += "</table>\n</div>";

            NowPlayingPage += "<h1 style=\"margin: 0px; padding: 25px; font-size: 26pt;\">XM Tuner - What's On</h1>";

            //Full Channel List
            NowPlayingPage += "<table align=\"center\" style=\"width: 95%; border: 1px solid black;\">";
            NowPlayingPage += "<tr><td colspan=6 style=\"border-bottom: 1px solid blue; font-size: 18pt; font-weight: bold;\">";
            NowPlayingPage += "<div style=\"float: right; font-size: 10pt;\">";
            NowPlayingPage += "<a style=\"text-decoration: none;\" href=\"/feeds/\" title=\"RSS 2.0 Feed for TVersity Media Server\"><img border=\"0\" width=\"16\" height=\"16\" alt=\"\" src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAKOSURBVHjadJNNSFRRGIbfc+6dO6OOOplDykSZaRCtnKRc+ANRUBFEm0gicxG0bGoVhERRYIvIjRAtW+UmW5QQQS6qRWQSWJRaUcjkT0LiVWfm/pzTd869M5LShe+eufC973m+n2Fj55KJymTt9ZgVy3AGMHoxvvEs/qZgDL4AlhbsAfv34g1TiRPxRMZbWwU3GQzO9clNhGcYBpkY4UlGiWRd5scnMvx5tUWano9IMoXy1pOUBPgz4xC/xv8r1hSexNKiB1Nhq6RIbQoVhy6i+MiCDffDEPyJITBvZZPYLwiQFFzVZ0Q45Moc8q8fwP34FGJ5FixaCevgBZT1DMNs7Nok9iikL8GyfWkZ3VgvhbljP6yOy2BbmzWR/+o2vM/PSmLfkchTaAIlsHa1ourSS5Sfvg8r3Q38mYb7+DzE9Ig2MDquAan2kth3AgKu0JSBapLCNranEWnPIHp2GDzZDEE3+1OBiXW4D4LHtViZCB/rBHJuHPmHp+CO3oK0Zyk7DvP4IGR1EwrPb673JX0mELslgvVRYW0O8usIvCe9AboyOdIPn1Ug9+Kupihr69ZiZSJFsYRwztbRO4icGARv6IT3ZkCTsKp68J2dyE2MQuZtsFgllbZHTyMsAVqsaldCVt8CtvsYPHsZzvtH+tZIc5e+sfBtLOhFYytRFAlYuGGED2clGNn8lO62m53U37y6XmM7M8E3onEIIpCSLp/vPyC31Bp6SUSsjmIbnO9jpVEZqbROzn15F5RDZs5CFs58FmaNBTZ5Ze+9hn11Genl/1mS0qjChumaCVvo2iViNXHkuD1g9Daxt7lVEQPMNpcSdah1pQb5kqho4yVXQc2iacHiMMpNFPia/jv/FWAAUTVTOunExzkAAAAASUVORK5CYII%3D\">";
                NowPlayingPage += "&nbsp;RSS Feed</a></div>";
                NowPlayingPage +="XM Channel Guide";
                
                NowPlayingPage += "</td></tr>\n";
            NowPlayingPage += "<tr><th></th><th>Channel</th><th>Artist</th><th>Song</th><th>Album</th><th></th></tr>\n";
            String mediaurl = "";
            int i = 0;
            foreach (XMChannel channel in list)
            {
                String row_color;
                if (i % 2 == 0)  { row_color = "#FFFFC0"; } else { row_color = "#FFFFFF"; }
                //XXX need to respect the useMMS code here
                if (UseMMS)
                {
                    mediaurl = "mms://";
                }
                else
                {
                    mediaurl = "http://";
                }
                mediaurl += serverHost + "/streams/" + channel.num + "/" + bitrate;
                if (nowPlayingNum == channel.num)
                {
                    NowPlayingPage += "<tr style=\"background-color: #FFFF00; border-bottom: 1px solid black;\">\n";
                }
                else
                {
                    NowPlayingPage += "<tr bgcolor=\"" + row_color + "\" onMouseOver=\"this.bgColor = '#CCE3E9'\" onMouseOut =\"this.bgColor = '" + row_color + "'\">\n";
                }
                NowPlayingPage += "\t<td style=\"text-align: center;\" nowrap><a href=\"" + mediaurl + "\">XM " + channel.num + "</a></td>\n";
                NowPlayingPage += "\t<td>" + channel.name + "</td>\n";
                NowPlayingPage += "\t<td>" + channel.artist + "</td>\n";
                NowPlayingPage += "\t<td>" + channel.song + "</td>\n";
                NowPlayingPage += "\t<td>" + channel.album + "</td>\n";
                NowPlayingPage += "\t<td><strong><a href=\"" + mediaurl + "\">Play!</a></strong></td>\n";
                NowPlayingPage += "</tr>\n";
                i++;
            }
            NowPlayingPage += "</table>";

            NowPlayingPage += "<hr noshade>\n";
            NowPlayingPage += "<p style=\"text-align: right; margin: 0px; color: #666;\">XMTuner</p>";
            NowPlayingPage += "</body>\n</html>";
            return (NowPlayingPage);
        }

    }
}
