﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Xml;
using System.Web;

namespace XMTuner
{
    class WebWorker
    {
        public Config cfg;
        XMTuner tuner;
        String network = "";

        public WebWorker(XMTuner tuner)
        {
            this.tuner = tuner;
            network = tuner.network;
            cfg = tuner.cfg;
        }

        public NameValueCollection parseStreamURL(string methodURL)
        {
            NameValueCollection collection = new NameValueCollection();
            String[] args = methodURL.Split('/');
            foreach (String arg in args)
            {
                if (collection["num"] == null)
                {
                    int n;
                    if (int.TryParse(arg, out n) == true)
                    {
                        collection.Add("num", arg);
                    }
                }
                if (collection["bitrate"] == null)
                {
                    if (arg.ToLower().Equals("high") || arg.ToLower().Equals("low"))
                    {
                        collection.Add("bitrate", arg);
                    }
                }
                // Null (default) or mp3 for transcoded version
                if (collection["streamtype"] == null)
                {
                    if (arg.ToLower().Equals("mp3") || arg.ToLower().Equals("wav"))
                    {
                        collection.Add("streamtype", arg);
                    }
                }

                //Type: Playlist container type, or null for none.
                if (collection["type"] == null)
                {
                    if (arg.ToLower().Equals("m3u") || arg.ToLower().Equals("asx"))
                    {
                        collection.Add("type", arg.ToUpper());
                    }
                }

            }
            if (args.Length > 0)
            {
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
            if (cfg.hostname != null && cfg.hostname.Contains(":"))
            {
                hostname = cfg.hostname;
            }
            else
            {
                hostname = serverHost;
            }
            return hostname;
        }

        public NameValueCollection DoStream(NameValueCollection streamParams, String serverHost)
        {
            NameValueCollection streamCollection = new NameValueCollection();
            String msg;
            String isErr = "false";
            int ChanNum = Convert.ToInt32(streamParams["num"]);
            String bitrate = TheConstructor.getBitRate(streamParams, cfg);
            String type = streamParams["type"];

            if (streamParams.Get("streamtype") != null)
            {
                msg = buildTranscoderURL(streamParams, serverHost);
            }
            else
            {
                msg = tuner.play(ChanNum, bitrate);
            }

            //Playlist Container for Single Channel...
            if (msg != null && type != null) 
            {
                String mimetype = null;
                if (type.Equals("ASX"))
                {
                    mimetype = "audio/x-ms-wax";
                }
                else if (type.Equals("M3U"))
                {
                    mimetype = "audio/x-mpegurl";
                }

                msg = buildPlaylistContainer(ChanNum, type, msg);
                streamCollection.Add("playlist", mimetype);
            }
            if (msg == null)
            {
                isErr = "true";
                msg = "<html><body><h1>Service Unavailable</h1> " +
                      "<p>Stream Error: Unable to fetch " + network + " stream URL. (Not Logged In or Down)</p>" +
                      "</body></html>";
            }
            streamCollection.Add("msg", msg);
            streamCollection.Add("isErr", isErr);
            return streamCollection;
        }

        public MemoryStream DoFeed(string methodURL, NameValueCollection URLparams, String useragent, String serverHost)
        {
            String bitrate_desc = getBitrateDesc(TheConstructor.getBitRate(URLparams, cfg));
            serverHost = getHostName(serverHost);
            tuner.output("Incoming Feed Request: " + network + " Channels (All - " + bitrate_desc + ")", LogLevel.Info);
            List<XMChannel> list = tuner.getChannels();
            MemoryStream OutputStream = CreateXMFeed(list, URLparams, serverHost, useragent);

            return OutputStream;
        }

        public String DoNowPlaying(String serverHost, NameValueCollection URLparams)
        {
            Boolean UseMMS = cfg.useMMS;
            serverHost = getHostName(serverHost);
            String bitrate = TheConstructor.getBitRate(URLparams, cfg);

            int nowPlayingNum = tuner.lastChannelPlayed;
            List<XMChannel> list = tuner.getChannels();


            String NowPlayingPage = "<!DOCTYPE html PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html401/loose.dtd\">" +
                                    "<html>\n<head>\n\t<title>XM Tuner - What's On</title>\n" +
                                    "<link rel=\"alternate\" type=\"application/rss+xml\" title=\"RSS\" href=\"/feeds/\" />\n" +
                                    "<link rel=\"shortcut icon\" href=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACpUlEQVR42n2TX0hTURzHf9fNud3SHprNzIc0BbVpGGaUpqv5YJSzSSKFScMeHMJE6qHCoMKn8DXpuRH0kkX0lIRaKkilsB4qnBNbpnO7M7fp7r9zb79zU9FSf/C958A553N+v+/vHgbW4qnXy9ba7RdMJraQKITheeGnf2rq08jIhy/d3d0q7BAM/QwPv8/LL8gfNRqNWYqigEIUIHRUCESjS8Hp6cCbeDx2v/Vay+K2gK/fvj/LyTl0lcgyMCkpQAhBKSBJIkQ4DgKBGTiSl5fQ6XQ9AwNvezs9HrIF8Hli8vXR4qJ6QRRBlmRIQQgvCLiiQjgchvT0DG2zSNdlecjv9zsbHPW/NwCP+560tLa2eBmGwVslSCRWIM1gAEVVIR6PA5amZbUGoFmOj42N2tpcLl4DXKxvYDwez6uTFeUOUZRAp9eBIIgwv7AA+zIywGBI1Uqih3meByyFetRjLS66x6zXct3VZqo7X/e8pvqMg26gyc3O/gAVs1BVPIwZ7GFZMKEELA/LnENADrPZ0YZLjczlpqau0hLrA7N5/17akb9mStrN+tRUDUZkQoFzZcdKtwLWo7n5ittaYu07Z7dD9sEsYBjsDLZUay/RDhMEusqPl3n/A9hsZx049KIUNDX7REXFnVOnKzEbkhtbXjagkUEEvOhwt/s2uvBvVFfXdGCNt3H6MRrl3D6fL4kw2kuCnqzgSEV2A7gR8BA3+wOB6bvBYJC6SvvOoZZQ8V0BVZVVj3R6/S2cvpucnHDGYrHDODehImuABEreFlBUWJhmsWSNYwdyQ4uhLo7jciORyCAuzaB+oSTqz5ZfeXM0Ohtrk8nkTZZlC/pf9nfaamw3BocG23EptONr3ByZmZnG1dVVA4vvmotGQ5YDFvP8wnwYdog/nl9LIIQqVnIAAAAASUVORK5CYII=\" type=\"image/x-icon\">"+
                                    "</head>\n<body style=\"margin: 0px; font-family: Arial; font-size: 10pt;\">\n" +
                                    "<style type=\"text/css\">a , a:visited { color: blue; text-decoration: none; } a:hover { color: orange; }</style>\n";
            //NowPlaying
            XMChannel npChannel = tuner.Find(nowPlayingNum);
            if (npChannel.album == null) { npChannel.album = ""; }

            NowPlayingPage += "<div style=\"float: right;\">\n<table id=\"nowplaying\" style=\"min-width: 442px; border: 1px solid #666; margin: 5px; padding: 0px 3px; -moz-border-radius: 10px; -webkit-border-radius: 10px;\">" +
                              "<tr><td style=\"border-bottom: 1px solid blue; font-size: 18pt; font-weight: bold;\" colspan=\"2\">"+
                                  "<div style=\"min-width: 442px; height: 1px;\"></div>"+ //Did I mention that webkit sucks?
                                  "<div id=\"rpHeader\" style=\"text-align:center; float: right; font-size: 8pt; cursor: pointer;\" onclick=\"runAnimation(recentlyPlayed, this)\"><a>Recently Played</a></div>" +
                                  "Now Playing<br>"+
                              "</td></tr>\n";
            if (npChannel.num != 0)
            {
                NowPlayingPage += "<tr><td style=\"height: 1em; padding-left: 5px;\">" + npChannel.ToString() + "</td>";
                    
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
            NowPlayingPage += "</table>\n" + CreateRecentlyPlayedPanel() + "</div>" +
                              "<h1 style=\"margin: 0px; padding: 25px 10px; font-size: 26pt;\">"+
                              "<img width=\"48\" height=\"48\" alt=\"\" src=\"data:image/png;base64,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%3D\" style=\"padding-right: 5px;\">" +
                              "XM Tuner - What's On</h1>";

            //Full Channel List
            NowPlayingPage += "<table align=\"center\" style=\"width: 98%; border: 1px solid black;\">" +
                                "<tr><td colspan=6 style=\"border-bottom: 1px solid blue; font-size: 18pt; font-weight: bold;\">" +
                                "<div style=\"float: right; font-size: 10pt;\">" +
                                "<a style=\"text-decoration: none;\" href=\"/feeds/\" title=\"RSS 2.0 Feed for TVersity Media Server\"><img border=\"0\" width=\"16\" height=\"16\" alt=\"\" src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAAABGdBTUEAAK/INwWK6QAAABl0RVh0U29mdHdhcmUAQWRvYmUgSW1hZ2VSZWFkeXHJZTwAAAKOSURBVHjadJNNSFRRGIbfc+6dO6OOOplDykSZaRCtnKRc+ANRUBFEm0gicxG0bGoVhERRYIvIjRAtW+UmW5QQQS6qRWQSWJRaUcjkT0LiVWfm/pzTd869M5LShe+eufC973m+n2Fj55KJymTt9ZgVy3AGMHoxvvEs/qZgDL4AlhbsAfv34g1TiRPxRMZbWwU3GQzO9clNhGcYBpkY4UlGiWRd5scnMvx5tUWano9IMoXy1pOUBPgz4xC/xv8r1hSexNKiB1Nhq6RIbQoVhy6i+MiCDffDEPyJITBvZZPYLwiQFFzVZ0Q45Moc8q8fwP34FGJ5FixaCevgBZT1DMNs7Nok9iikL8GyfWkZ3VgvhbljP6yOy2BbmzWR/+o2vM/PSmLfkchTaAIlsHa1ourSS5Sfvg8r3Q38mYb7+DzE9Ig2MDquAan2kth3AgKu0JSBapLCNranEWnPIHp2GDzZDEE3+1OBiXW4D4LHtViZCB/rBHJuHPmHp+CO3oK0Zyk7DvP4IGR1EwrPb673JX0mELslgvVRYW0O8usIvCe9AboyOdIPn1Ug9+Kupihr69ZiZSJFsYRwztbRO4icGARv6IT3ZkCTsKp68J2dyE2MQuZtsFgllbZHTyMsAVqsaldCVt8CtvsYPHsZzvtH+tZIc5e+sfBtLOhFYytRFAlYuGGED2clGNn8lO62m53U37y6XmM7M8E3onEIIpCSLp/vPyC31Bp6SUSsjmIbnO9jpVEZqbROzn15F5RDZs5CFs58FmaNBTZ5Ze+9hn11Genl/1mS0qjChumaCVvo2iViNXHkuD1g9Daxt7lVEQPMNpcSdah1pQb5kqho4yVXQc2iacHiMMpNFPia/jv/FWAAUTVTOunExzkAAAAASUVORK5CYII%3D\">" +
                                "&nbsp;RSS Feed</a></div>" +
                                network+" Channel Guide" +

                                "</td></tr>\n" +
                                "<tr><th></th><th>Channel</th><th>Artist</th><th>Song</th><th>Album</th><th></th></tr>\n";
            String mediaurl = "";
            int i = 0;
            foreach (XMChannel channel in list)
            {
                String row_color;
                if (i % 2 == 0) { row_color = "#FFFFC0"; } else { row_color = "#FFFFFF"; }
                mediaurl = TheConstructor.buildLink("stream", serverHost, URLparams, null, channel.num, cfg);
                if (nowPlayingNum == channel.num)
                {
                    NowPlayingPage += "<tr style=\"background-color: #FFFF00; border-bottom: 1px solid black;\">\n";
                }
                else
                {
                    NowPlayingPage += "<tr bgcolor=\"" + row_color + "\" onMouseOver=\"this.bgColor = '#CCE3E9'\" onMouseOut =\"this.bgColor = '" + row_color + "'\">\n";
                }
                NowPlayingPage += "\t<td style=\"text-align: center;\" nowrap>";
                if (channel.url != null) { NowPlayingPage += "<a href=\"" + channel.url + "\" target=\"_blank\">"; }
                String int_logo_url = "/logo/" + channel.num;
                if (channel.category.ToLower().Contains("talk") || channel.category.ToLower().Contains("sports"))
                {
                    NowPlayingPage += "<div style=\"float: left;\"><img src=\"" + int_logo_url + "\" border=\"0\" width=\"45\" height=\"40\"></div>";
                }
                else
                {
                    NowPlayingPage += "<div style=\"overflow: hidden; height: 25px; float: left;\"><img src=\"" + int_logo_url + "\" border=\"0\" width=\"45\" height=\"40\" style=\"position: relative; top: -5px;\"></div>";
                }

                String[] currentProgram = tuner.getCurrentProgram(channel.programData);
                String[] nextProgram = tuner.getNextProgram(channel.programData);

                String program = ""; 
                if (currentProgram != null) {
                    program = "<div style=\"padding-left: 0px; font-size: 8pt; color: #999;\" title=\"";
                    program += "On Now: " + HttpUtility.HtmlEncode(currentProgram[2]) + " (" + DateTime.Parse(currentProgram[4]).ToShortTimeString() + " - " + DateTime.Parse(currentProgram[5]).ToShortTimeString() + ")";

                    if (nextProgram != null)
                    {
                        program += "\n | Next: " + HttpUtility.HtmlEncode(nextProgram[2]) + " (" + DateTime.Parse(nextProgram[4]).ToShortTimeString() + " - " + DateTime.Parse(nextProgram[5]).ToShortTimeString() + ")";
                    }
                    program += "\">Now: " + currentProgram[2] + "</div>";
                }
                NowPlayingPage += "<span style=\"position: relative; top: 6px; font-size: 8pt;\">" + channel.num + "</span></a></td>\n" +
                                    "\t<td title=\"" + channel.desc + "\"><a href=\"/info/"+channel.num+"\">" + channel.name + "</a>" + program + "</td>\n" +
                                    "\t<td>" + channel.artist + "</td>\n" +
                                    "\t<td>" + channel.song + "</td>\n" +
                                    "\t<td>" + channel.album + "</td>\n" +
                                    //"\t<td>" + channel.channelKey + " :: " + channel.xmkey + " ("+ channel.xmxref +")</td>\n" + //Temp for Testing
                                    "\t<td><strong><a href=\"" + mediaurl + "\">Play!</a></strong></td>\n" +
                                    "</tr>\n";

                i++;
            }

            String lastLoggedIn = tuner.lastLoggedIn.ToString("F");
            String lastDataUpdate = tuner.lastWhatsOnUpdate.ToString("F");

            NowPlayingPage += "</table>" +
                                "<hr noshade>\n" +
                                "<p style=\"text-align: right; margin: 0px; padding-right: 10px; color: #666;\">Last Update: "+ lastDataUpdate+" | Logged in: "+lastLoggedIn+" | XMTuner "+configMan.version+"</p>"+
                                "</body>\n</html>";
            return (NowPlayingPage);
        }

        private String CreateRecentlyPlayedPanel()
        {
            if (tuner.recentlyPlayed == null ||
                tuner.recentlyPlayed.AsReadOnly().Count == 0) { return ""; }


            String animationJavascript = "function AnimationFrame(left, top, width, height, time) {\n" +
                  "this.Left = left;   this.Top = top;   this.Width = width;   this.Height = height;   this.Time = time;\n" +
                  "this.Copy = function(frame)  {    this.Left = frame.Left; this.Top = frame.Top;    this.Width = frame.Width;    this.Height = frame.Height;    this.Time = frame.Time;  }\n" +
                  "this.Apply = function(element)  {    element.style.left = Math.round(this.Left) + 'px';    element.style.top = Math.round(this.Top) + 'px';    element.style.width = Math.round(this.Width) + 'px';    element.style.height = Math.round(this.Height) + 'px';  }\n" +
                "}\n" +

                "function AnimationObject(element) {\n" +
                  "if(typeof(element) == \"string\") element = document.getElementById(element); var frames = null; var timeoutID = -1; var running = 0; var currentFI = 0; var currentData = null; var lastTick = -1;  var callback = null;  var prevDir = 0;\n" +
                  "this.AddFrame = function(frame)  {    frames.push(frame);  }\n" +
                  "this.SetCallback = function(cb)  {    callback = cb;  }\n" +
                  "this.ClearFrames = function()  {    if(running != 0)  this.Stop();    frames = new Array();    frames.push(new AnimationFrame(0,0,0,0,0));    frames[0].Time = 0;    frames[0].Left = parseInt(element.style.left);    frames[0].Top = parseInt(element.style.top);    frames[0].Width = parseInt(element.style.width);    frames[0].Height = parseInt(element.style.height);    currentFI = 0;    prevDir = 0;    currentData = new AnimationFrame(0,0,0,0,0);     }\n" +
                  "this.ResetToStart = function()  {   if(running != 0)  this.Stop();   currentFI = 0;   prevDir = 0;   currentData = new AnimationFrame(0,0,0,0,0);   frames[0].Apply(element);  }\n" +
                  "this.ResetToEnd = function()  {    if(running != 0)      this.Stop();    currentFI = 0;    prevDir = 0;    currentData = new AnimationFrame(0,0,0,0,0);    frames[frames.length - 1].Apply(element);  }\n" +
                  "this.Stop = function()  {    if(running == 0)      return;    if(timeoutID != -1)      clearTimeout(timeoutID);    prevDir = running;    running = 0;  }\n" +
                  "this.RunForward = function()  {    if(running == 1)  return;    if(running == -1)      this.Stop();    if(frames.length == 1 || element == null)      return;   lastTick = new Date().getTime();  if(prevDir == 0)   {      currentFI = 1;      currentData.Time = 0;      currentData.Left = parseInt(element.style.left);      currentData.Top = parseInt(element.style.top);      currentData.Width = parseInt(element.style.width);      currentData.Height = parseInt(element.style.height);      frames[0].Copy(currentData);    } else if(prevDir != 1)    {      currentFI++;      currentData.Time =          frames[currentFI].Time - currentData.Time;    }  running = 1;    animate();  }\n" +
                  "this.RunBackward = function()  {  if(running == -1)  return; if(running == 1)  this.Stop(); if(frames.length == 1 || element == null)  return;  lastTick = new Date().getTime(); if(prevDir == 0) {  currentFI = frames.length-2;  currentData.Left = parseInt(element.style.left);  currentData.Top = parseInt(element.style.top);  currentData.Width = parseInt(element.style.width);  currentData.Height = parseInt(element.style.height);  currentData.Time = frames[frames.length-1].Time; frames[frames.length-1].Copy(currentData);  currentData.Time = 0; } else if(prevDir != -1)  {  currentData.Time = frames[currentFI].Time - currentData.Time;  currentFI--;  }  running = -1;  animate(); }\n" +

                  "function animate() {\n" +
                    "if(running == 0)  return;  var curTick = new Date().getTime();  var tickCount = curTick - lastTick;   lastTick = curTick; var timeLeft =  frames[((running == -1) ? currentFI+1 : currentFI)].Time  - currentData.Time;\n" +
                    "while(timeLeft <= tickCount) {  currentData.Copy(frames[currentFI]);  currentData.Time = 0;  currentFI += running; if(currentFI>= frames.length || currentFI <0) {  currentData.Apply(element);  lastTick = -1;  running = 0;  prevDir = 0;  if(callback != null)  callback();  return; } tickCount = tickCount - timeLeft;  timeLeft = frames[((running == -1) ? currentFI+1 : currentFI)].Time- currentData.Time; }\n" +
                    "if(tickCount != 0)  {  currentData.Time += tickCount; var ratio = currentData.Time/ frames[((running == -1) ? currentFI+1 : currentFI)].Time;  currentData.Left = frames[currentFI-running].Left + (frames[currentFI].Left - frames[currentFI-running].Left) * ratio;      currentData.Top = frames[currentFI-running].Top + (frames[currentFI].Top - frames[currentFI-running].Top)  * ratio;      currentData.Width = frames[currentFI-running].Width + (frames[currentFI].Width  - frames[currentFI-running].Width)  * ratio;      currentData.Height = frames[currentFI-running].Height + (frames[currentFI].Height - frames[currentFI-running].Height)  * ratio;    } currentData.Apply(element); timeoutID = setTimeout(animate, 33);\n" +
                  "}\n" +

                  "this.ClearFrames();\n" +
                "}";

            String panel;
            panel = "<script type=\"text/javascript\">"+ animationJavascript +"</script>" +

                "<div id=\"rpPanelParent\" style=\"position:relative; width: auto; /* height:250px; */ top:0px; left:0px; z-index: 1;\">" +
                    "<div id=\"rpPanel\" style=\"position:absolute; width:440px; height:0px; top:-8px; left:5px; background:#FFF; border: 0px solid black; overflow:hidden; -moz-border-radius: 0px 0px 10px 10px; -webkit-border-bottom-left-radius: 10px; -webkit-border-bottom-right-radius: 10px;\">" +
                        "<table id=\"rpTable\" border=0 cellspacing=2 cellpadding=0 width=\"100%\">\n<tr><td colspan=2 style=\"font-size: 12pt; font-weight: bold; border-bottom: 1px solid black;\">Recently Played:</td></tr>\n";
                        int i = 0;
                        foreach (String _item in tuner.recentlyPlayed.AsReadOnly()) {
                            i++;
                            String row_color;
                            if (i % 2 == 0) { row_color = "#EEEEEE"; } else { row_color = "#FFFFFF"; }

                            String[] item = _item.Split(new char[]{'-'}, 2);
                            panel += "<tr bgcolor=\"" + row_color + "\"><td valign=\"top\" nowrap>" + item[0] + "</td><td style=\"padding-left: 2px;\">" + item[1] + "</td></tr>\n";
                        }
                        panel += "</table></div>"+
                "</div>";

            panel += "<script type=\"text/javascript\">"+
                "var recentlyPlayed = new AnimationObject('rpPanel');"+
                "var width = 440;"+
                "if (document.getElementById('nowplaying').clientWidth > 440) {"+
                    "width = document.getElementById('nowplaying').clientWidth; "+
                    "document.getElementById('rpPanel').style.width = document.getElementById('nowplaying').offsetWidth-2+\"px\"; " +
                "}"+
                //Pos X, Y, Width, Height, Duration of Animation in MS
                "recentlyPlayed.AddFrame(new AnimationFrame(5, -8, width, document.getElementById('rpTable').clientHeight+5, 500));" +

                "function runAnimation(animation, header)"+
                "{"+
                  "if(header.animationObject)"+
                  "{"+
                    "animation.RunBackward();"+
                    "header.animationObject = null;"+
                    "setTimeout(\"updateElements(1)\", 500);" +
                  "}"+
                  "else"+
                  "{"+
                    "setTimeout(\"updateElements(0)\", 25);" +
                    "animation.RunForward();"+
                    "header.animationObject = animation;"+
                  "}"+
                "}\n"+
                "function updateElements(header) {"+
                 "if(header) {"+
                    "d = document.getElementById('nowplaying');" +
                    "e = document.getElementById('rpPanel');" +
                    "d.style.width=\"\";" +
                    "d.style.MozBorderRadiusBottomleft=\"10px\";" +
                    "d.style.MozBorderRadiusBottomright=\"10px\";" +
                    "e.style.border='0px solid #FFF';" +
                   "}"+
                  "else"+
                  "{"+
                    "d = document.getElementById('nowplaying');" +
                    "e = document.getElementById('rpPanel');"+
                    "d.style.MozBorderRadiusBottomleft=\"0px\";" +
                    "d.style.MozBorderRadiusBottomright=\"0px\";" +
                    "d.style.webkitBorderBottomLeftRadius=\"0px\";" +
                    "d.style.webkitBorderBottomRightRadius=\"0px\";" +
                    "e.style.border='1px solid #666';" +
                    "e.style.borderTop='0px solid #FFF';" +
                  "}"+
                "}" +
            "</script>";

            return panel;
        }

        private MemoryStream CreateXMFeed(List<XMChannel> list, NameValueCollection URLparams, String serverHost, String useragent)
        {
            String link = TheConstructor.buildLink("feed", serverHost, URLparams, useragent, 0, cfg);
            String bitrate_desc = getBitrateDesc(TheConstructor.getBitRate(URLparams, cfg));
            
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
            writer.WriteAttributeString("xmlns:tversity", "http://tversity.com/schemas/podcast-1.0.xsd");

            //<channel>
            writer.WriteStartElement("channel");

            //Write the title element.
            //<title>XM Channels (" + bitrate_desc + ")</title>
            writer.WriteStartElement("title");
            writer.WriteString(network + " Channels (" + bitrate_desc + ")");
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
                String media = TheConstructor.buildLink("stream", serverHost, URLparams, useragent, chan.num, cfg);

                //<item>
                writer.WriteStartElement("item");

                //<title>XM " + chan.num + " - " + chan.name + "</title>
                writer.WriteStartElement("title");
                writer.WriteString(chan.ToString());
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
                writer.WriteAttributeString("type", "video/x-ms-asf");
                writer.WriteAttributeString("medium", "audio");
                writer.WriteAttributeString("audioCodec", "WMAV2");
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

        public String DoBuildPlaylist(string methodURL, NameValueCollection URLparams, String serverHost)
        {
            String type = URLparams["type"].ToUpper();
            List<XMChannel> ChannelList = tuner.getChannels();
            String media = "";
            String playlist = "";
            int i = 0; 
            int sizeofList = ChannelList.Count;

            if (type == "PLS")
            {
                playlist += "[playlist]\r\n";
                playlist += "NumberOfEntries=" + sizeofList.ToString() + "\r\n";
                playlist += "\r\n";

                foreach (XMChannel channel in ChannelList)
                {
                    i++;
                    media = TheConstructor.buildLink("stream", serverHost, URLparams, null, channel.num, cfg);
                    playlist += "File" + i + "=" + media + "\r\n";
                    playlist += "Title" + i + "=" + channel.name + "\r\n";
                    playlist += "Length=-1\r\n";
                    playlist += "\r\n";
                }

                playlist += "Version=2";
            }
            else if (type == "ASX")
            {
                playlist += "<asx version=\"3.0\">\r\n";
                playlist += "\t<title>XM Tuner</title>\r\n";
                playlist += "\r\n";

                foreach (XMChannel channel in ChannelList)
                {
                    media = TheConstructor.buildLink("stream", serverHost, URLparams, null, channel.num, cfg);

                    playlist += "\t<entry>\r\n";
                    playlist += "\t\t<title>"+channel.name+"</title>\r\n";
                    playlist += "\t\t<ref href=\""+media+"\"/>\r\n";
                    playlist += "\t</entry>\r\n";
                }
                playlist += "</asx>";
            }
            else if (type == "M3U")
            {
                playlist += "#EXTM3U\r\n";
                foreach (XMChannel channel in ChannelList)
                {
                    media = TheConstructor.buildLink("stream", serverHost, URLparams, null, channel.num, cfg);

                    playlist += "#EXTINF:-1,"+channel.name+"\r\n";
                    playlist += media + "\r\n";
                }
            }
            return playlist;
        }

        private String buildPlaylistContainer(int num, String type, String media)
        {
            String playlist = null;
            XMChannel channel = tuner.Find(num);

            if (type == "ASX")
            {
                playlist += "<asx version=\"3.0\">\r\n";
                playlist += "\t<title>XM Tuner</title>\r\n";
                playlist += "\r\n";

                    playlist += "\t<entry>\r\n";
                    playlist += "\t\t<title>" + channel.name + "</title>\r\n";
                    playlist += "\t\t<ref href=\"" + media + "\"/>\r\n";
                    playlist += "\t</entry>\r\n";
                playlist += "</asx>";
            }
            else if (type == "M3U")
            {
                playlist += "#EXTM3U\r\n";
                    playlist += "#EXTINF:-1," + channel.name + "\r\n";
                    playlist += media + "\r\n";
            }
            return playlist;
        }

        private String buildTranscoderURL(NameValueCollection streamParams, String serverHost)
        {
            if (cfg.tversityServer == null) { return null; }

            String streamtype = streamParams["streamtype"].ToLower();
            String mimetype = null;
            streamParams.Remove("streamtype");
            serverHost = getHostName(serverHost);

            tuner.output("Stream using transcoder. Output type: " + streamtype, LogLevel.Info);
            if (streamtype.Equals("mp3"))
            {
                mimetype = "audio/mpeg";
            }
            else if (streamtype.Equals("wav"))
            {
                mimetype = "audio/wav";
            }

            int num = Convert.ToInt32(streamParams["num"]);
            String redirectURL = TheConstructor.buildLink("stream", serverHost, streamParams, "TVersity", num, cfg);
            tuner.output("RedirectURL: " + redirectURL, LogLevel.Debug);

            String msg = "http://" + cfg.tversityServer + "/geturl/stream."+streamtype+"?type=audio/x-ms-wma&ttype="+mimetype+"&url=" + HttpUtility.UrlEncode(redirectURL) + "&ext=."+streamtype;
            tuner.output(msg, LogLevel.Debug);
            return msg;
        }

        public String GetMimeType(System.Drawing.Image i)
        {
            foreach (System.Drawing.Imaging.ImageCodecInfo codec in System.Drawing.Imaging.ImageCodecInfo.GetImageDecoders())
            {
                if (codec.FormatID == i.RawFormat.Guid)
                    return codec.MimeType;
            }

            return "image/unknown";
        }

        public String DoChannelInfo(int num)
        {
            String page;
            XMChannel npChannel = tuner.Find(num);

            // Page Preamble
            page = "<!DOCTYPE html PUBLIC \"-//W3C//DTD HTML 4.01 Transitional//EN\" \"http://www.w3.org/TR/html401/loose.dtd\">" +
                   "<html>\n<head>\n" +
                   "<title>XMTuner - Channel Info :: " + npChannel.ToString() + "</title>\n" +
                   "<link rel=\"shortcut icon\" href=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAACpUlEQVR42n2TX0hTURzHf9fNud3SHprNzIc0BbVpGGaUpqv5YJSzSSKFScMeHMJE6qHCoMKn8DXpuRH0kkX0lIRaKkilsB4qnBNbpnO7M7fp7r9zb79zU9FSf/C958A553N+v+/vHgbW4qnXy9ba7RdMJraQKITheeGnf2rq08jIhy/d3d0q7BAM/QwPv8/LL8gfNRqNWYqigEIUIHRUCESjS8Hp6cCbeDx2v/Vay+K2gK/fvj/LyTl0lcgyMCkpQAhBKSBJIkQ4DgKBGTiSl5fQ6XQ9AwNvezs9HrIF8Hli8vXR4qJ6QRRBlmRIQQgvCLiiQjgchvT0DG2zSNdlecjv9zsbHPW/NwCP+560tLa2eBmGwVslSCRWIM1gAEVVIR6PA5amZbUGoFmOj42N2tpcLl4DXKxvYDwez6uTFeUOUZRAp9eBIIgwv7AA+zIywGBI1Uqih3meByyFetRjLS66x6zXct3VZqo7X/e8pvqMg26gyc3O/gAVs1BVPIwZ7GFZMKEELA/LnENADrPZ0YZLjczlpqau0hLrA7N5/17akb9mStrN+tRUDUZkQoFzZcdKtwLWo7n5ittaYu07Z7dD9sEsYBjsDLZUay/RDhMEusqPl3n/A9hsZx049KIUNDX7REXFnVOnKzEbkhtbXjagkUEEvOhwt/s2uvBvVFfXdGCNt3H6MRrl3D6fL4kw2kuCnqzgSEV2A7gR8BA3+wOB6bvBYJC6SvvOoZZQ8V0BVZVVj3R6/S2cvpucnHDGYrHDODehImuABEreFlBUWJhmsWSNYwdyQ4uhLo7jciORyCAuzaB+oSTqz5ZfeXM0Ohtrk8nkTZZlC/pf9nfaamw3BocG23EptONr3ByZmZnG1dVVA4vvmotGQ5YDFvP8wnwYdog/nl9LIIQqVnIAAAAASUVORK5CYII=\" type=\"image/x-icon\">" +
                   "</head>\n<body style=\"margin: 0px; font-family: Arial; font-size: 10pt;\">\n";

            page += "<h1 style=\"margin: 0px; padding: 25px 10px; font-size: 26pt;\">" +
                              "<img width=\"48\" height=\"48\" alt=\"\" src=\"data:image/png;base64,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%3D\" style=\"padding-right: 5px;\">" +
                              "XM Tuner - Channel Info</h1>";

            //Title
            page += "<div id=\"channelTitle\" style=\"font-size: 18pt;\"><a href=\"" + npChannel.url + "\" target=\"_blank\"><img src=\"" + npChannel.logo + "\" border=0 alt=\"\" width=\"138\" height=\"50\"></a> "+npChannel.ToString()+"</div>";

            //Details
            page += "<div id=\"channelDetails\" style=\"border-top: 1px solid #666; margin-left: 45px; margin-right: 20px; padding: 5px 10px;\">";
            page += "<span style=\"font-size: 10pt; text-transform: uppercase;\">"+npChannel.desc+"</span><br>\n";

            //On Air Now
            page += "<table><tr><td style=\"font-size: 11pt; font-weight: bold;\">On Now:</td></tr>" +
                    "<tr><td style=\"padding-left: 5px;\" valign=\"top\">" + npChannel.artist + " - " + npChannel.song + "</td></tr>";
            if (!npChannel.album.Equals(""))
            {
                page += "<tr><td style=\"padding-left: 25px; color: #666;\">" + npChannel.album + "</td></tr>\n";
            }

            String[] currentProgram = tuner.getCurrentProgram(npChannel.programData);
            if (currentProgram != null)
            {
                page += "<tr><td style=\"padding-left: 25px; color: #666;\">" + HttpUtility.HtmlEncode(currentProgram[2]) + " (" + DateTime.Parse(currentProgram[4]).ToShortTimeString() + " - " + DateTime.Parse(currentProgram[5]).ToShortTimeString() + ")</td></tr>";
            }
            page += "</table>";

            page += "</div>";
            page += "<div style=\"margin-left: 45px; margin-right: 20px; padding-top: 15px;\">";

            //Script
            page += "<script type=\"text/javascript\">\n" +
                        "function showPG() { " +
                        "var songDiv = document.getElementById(\"songs\");\n" +
                        "var pgDiv = document.getElementById(\"programs\");\n" +
                        "var songButton = document.getElementById(\"songButton\");\n" +
                        "var pgButton = document.getElementById(\"programButton\");\n" +
                        "songDiv.style.display = 'none';" +
                        "pgDiv.style.display = 'block';" +
                        "songButton.style.backgroundColor = '#666666';" +
                        "pgButton.style.backgroundColor = '#3399FF';" +
                        "}" +

                        "function showRS() { " +
                        "var songDiv = document.getElementById(\"songs\");\n" +
                        "var pgDiv = document.getElementById(\"programs\");\n" +
                        "var songButton = document.getElementById(\"songButton\");\n" +
                        "var pgButton = document.getElementById(\"programButton\");\n" +
                        "songDiv.style.display = 'block';" +
                        "pgDiv.style.display = 'none';" +
                        "songButton.style.backgroundColor = '#3399FF';" +
                        "pgButton.style.backgroundColor = '#666666';" +
                        "}"+
                     "</script>";

            page += "<div id=\"songButton\"    onclick=\"javascript:showRS()\" style=\"width: 90px; display: inline; padding: 5px; background-color: #39F; height: 35px; border: 1px solid #333; color: white; cursor: pointer;\">Recent Songs</div>\n";
            page += "<div id=\"programButton\" onclick=\"javascript:showPG()\" style=\"width: 90px; display: inline; padding: 5px; background-color: #666; height: 35px; border: 1px solid #333; color: white; cursor: pointer;\">Program Guide</div>\n";

            //Recently Played Songs
            page += "<div id=\"songs\" style=\"border: 1px solid #666; padding: 5px;\">";
            page += "<h2>Recently Played Songs</h2>\n";
            page += "<table style=\"font-size: 11pt; width: 80%;\" cellpadding=\"5\" cellspacing=\"4\">";
            int i = 0;
            if (npChannel.history.Count == 0)
            {
                page += "Not available yet...<br>";
            }
            else
            {

                foreach (String _item in npChannel.history.AsReadOnly()) {

                    String row_color;
                    if (i % 2 == 0) { row_color = "#FFFFC0"; } else { row_color = "#FFFFFF"; }

                    String[] item = _item.Split(new Char[1]{'-'}, 3);

                    page += "<tr bgcolor=\"" + row_color + "\"><td style=\"width: 170px;\">" + item[0] + "</td><td>" + item[1] + "</td><td>" + item[2] + "</td></tr>\n";
                    i++;
                }
            }
            page += "</table>";
            page += "</div>\n";

            //Program Guide
            page += "<div id=\"programs\" style=\"display: none; border: 1px solid #666; padding: 5px;\">";
            page += "<h2>Program Guide</h2>\n";
            page += "<table style=\"font-size: 11pt; width: 80%;\" cellpadding=\"5\" cellspacing=\"4\">";
            i = 0;
            foreach (String[] program in npChannel.programData)
            {
                String row_color;
                if (i % 2 == 0) { row_color = "#FFFFC0"; } else { row_color = "#FFFFFF"; }

                page += "<tr bgcolor=\"" + row_color + "\"><td style=\"width: 170px;\">" + DateTime.Parse(program[4]).ToShortTimeString() + " - " + DateTime.Parse(program[5]).ToShortTimeString() + "</td><td>" + HttpUtility.HtmlEncode(program[2]) + "</td></tr>\n";
                i++;
            }
            page += "</table>";
            page += "</div>\n";

            page += "</div>\n";
            page += "</body>\n</html>";
            return page;
        }
    }
}