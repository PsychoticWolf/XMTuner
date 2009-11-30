using System;
using System.Collections.Generic;
//using System.Timers;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
//using System.Drawing;

namespace XMReaderConsole
{
    class XMTuner
    {
        String user;
        String password;
        RichTextBox outputbox;
        public bool isMMS = false;
        String cookies;
        public int lastChannelPlayed;
        bool isLoggedIn;
        bool isDebug = false;
        bool isLive = true;
        public String OutputData = "";
        public String theLog = "";
        public int cookieCount = 0;
        
        public String bitrate = "high";
        List<XMChannel> channels = new List<XMChannel>();

        public XMTuner()
        {
        }

        public XMTuner(String username, String passw, ref RichTextBox box1, String rbitrate, bool MMSON)
        {

            user = username;
            password = passw;
            outputbox = box1;
            bitrate = rbitrate;
            isMMS = MMSON;
           
            System.Timers.Timer loginTimer = new System.Timers.Timer();
            loginTimer.Interval  = 3600;
            loginTimer.Start();
            login();
          
        }

        public void login()
        {
            String XMURL;
            if (isLive) 
            {
                XMURL = "http://www.xmradio.com/player/login/xmlogin.action";
                
            }
            else
            {
                XMURL = "http://users.pcfire.net/~wolf/XMReader/test.php";
            }

            output("Connecting to: "+XMURL, "debug");
            
            String data = "playerToLaunch=xm&encryptPassword=true&userName="+user+"&password="+password;
            URL loginURL = new URL(XMURL);
            loginURL.fetch(data);

            int responseCode = loginURL.getStatus();
            output("Server Response: " + responseCode.ToString(), "debug");



            
            if (loginURL.getStatus() > 0 && loginURL.getStatus() < 400)
            {
                cookies = setCookies(loginURL.getCookies());

                output("Number of Cookies: " + cookieCount.ToString(), "debug");

                if (cookieCount > 0)
                {
                    
                    if (cookieCount <= 1)
                    {
                        output("Login failed: Bad Us/Ps", "error");
                    }
                    else { output("Logged in as " + user, "info"); }
                    isLoggedIn = true;
                    loadChannelData();
                    doWhatsOn();
                }
            }
            else 
            {
                output("Login Failed: " + loginURL.getStatus(), "error"); 
            }
            loginURL.close();
        }

        //public bool isLoggedIn()
        //{
            //if (cookies != null && cookieCount > 1)
            //{
                //return true;
            //}
            //else
            //{
                //return false;
            //}
        //}

        public bool loadChannelData()
        {
            output("Loading channel lineup...", "info");
            bool goodData = false;
            int j;
            for (j = 0; j < 60; j++)
            {
                string url;
                if (isLive)
                {
                   url = "http://www.xmradio.com/player/channel/ajax.action?reqURL=player/2ft/channelData.jsp?remote=true&all_channels=true";
                }
                else
                {
                    url = "http://users.pcfire.net/~wolf/XMReader/channeldata.jsp";
                }

                URL channelURL = new URL(url);
                channelURL.setRequestHeader("Cookie", cookies);
                channelURL.fetch();
                String resultStr = channelURL.response();
                if (channelURL.getStatus() >= 200 && channelURL.getStatus() < 300 && resultStr.IndexOf(":[],") == -1)
                {
                    goodData = true;
                    setChannelData(resultStr);
                }
                if (goodData)
                {
                    output("Channel lineup loaded successfully.", "info");
                    return true;
                }
            }
            output("Failed to load channel lineup", "error");
            return false;
        }

        public void setChannelData(String rawchanneldata)
        {
            XMChannel tempChannel;

            //digest info into usable bits
            rawchanneldata = rawchanneldata.Replace("xms.sendRPCDone(\"allchannels\",[{\"channels\":[{", "");
            rawchanneldata = rawchanneldata.Replace("}])", "}]");
            rawchanneldata = rawchanneldata.Replace("{\"channels\":[{", "\n");

            //Break Raw Data into Grouped Neighborhoods...
            String [] rChanNeighborhoods = rawchanneldata.Split('\n');

            String tChanNeigh;
            String neighborhood;
            String[] tmp;
            String[] rChannelsList;
            String tChannel;
            String[] channelData;
            int num;
            int k = 0;
            foreach (String rChanNeigh in rChanNeighborhoods)
            {
                tChanNeigh = rChanNeigh.Replace("},", "");
                //Split Neighborhood Name from Raw Channels
                tmp = tChanNeigh.Split(new string[] { ",\"nhood\":" }, StringSplitOptions.None);
                neighborhood = tmp[1].Replace("\"", "");
                neighborhood = neighborhood.Replace("}]","");
                rChannelsList = tmp[0].Split('{');

                foreach (String rChannel in rChannelsList)
                {
                    k++;
                    tChannel = rChannel.Replace("\"enabled\":\"", "");
                    tChannel = tChannel.Replace("\",\"name\"", "");
                    tChannel = tChannel.Replace("\",\"num\"", "");
                    tChannel = tChannel.Replace(",\"desc\"", "");
                    tChannel = tChannel.Replace("\",\"xl\":\"true\"", "");
                    tChannel = tChannel.Replace("\",\"xl\":\"false\"", "");
                    tChannel = tChannel.Replace("\"", "");
                    tChannel = tChannel.Replace("}]", "");
                    tChannel = tChannel.Replace("\\", "");

                    channelData = tChannel.Split(':');
                    if (channelData[0] != "false")
                    {
                        num = Convert.ToInt32(channelData[2]);
                        tempChannel = new XMChannel(neighborhood, num, channelData[1], channelData[3]);
                        output(neighborhood + " " + num + " " + channelData[1] + " " + channelData[3], "debug");
                        channels.Add(tempChannel);
                    }

                }
            }
        }

        public List<XMChannel> getChannels()
        {
            channels.Sort();
            channels.Reverse();
            return channels;
        }

        public string checkChannel(int num)
        {
            return Find(num).name;
        }

        public void doWhatsOn()
        {
            if (isLoggedIn == false)
            {
                output("Not logged in. Reconnecting...", "info");
                login();
                return;
            }

            MethodInvoker simpleDelegate = new MethodInvoker(loadWhatsOn);
            simpleDelegate.BeginInvoke(null, null);
        }

        public void loadWhatsOn()
        {
            output("Update What's On Data...", "debug");
            String whatsOnURL;
            if (isLive)
            {
                whatsOnURL = "http://www.xmradio.com/padData/pad_data_servlet.jsp?all_channels=true&remote=true";
            }
            else
            {
                whatsOnURL = "http://users.pcfire.net/~wolf/XMReader/all_data.js";
            }
            URL whatsOn = new URL(whatsOnURL);
            output("Fetching: "+whatsOnURL, "debug");
            whatsOn.setRequestHeader("Cookie", cookies);
            whatsOn.fetch();

            int responseCode = whatsOn.getStatus();
            output("Server Response: " + responseCode.ToString(), "debug");
            setWhatsonData(whatsOn.response());
            whatsOn.close();
        }

        public void setWhatsonData(String rawdata)
        {
            rawdata = rawdata.Trim();
            rawdata = rawdata.Replace("\n", ""); // No new lines please
            rawdata = rawdata.Replace("\t", ""); // No tabs...
            rawdata = rawdata.Replace("xms.sendRPCDone(\"whatson\",[ {", ""); // Remove preamble
            rawdata = rawdata.Replace("}            ]);", ""); //suffix

            //$data = explode(" {   ", $rawData); // Turn our single line into an array of raw whatsOn data per channel
            String[] data = rawdata.Split(new string[] { " {   " }, StringSplitOptions.None);

            //foreach ($data as $rawChannel) {
            foreach (String _rawChannel in data)
            {
	            String sep = "::XMTUNER-SEPERATOR::";

	            //Clean up the string
	            String rawChannel = _rawChannel.Trim();
	            rawChannel = rawChannel.Replace("},", ""); //Remove Suffix
                rawChannel = rawChannel.Replace("num: ", ""); //Remove Label Num
                rawChannel = rawChannel.Replace(",artist: ", sep); //Remove Label Artist
                rawChannel = rawChannel.Replace(",song: ", sep); //Remove Label Song
                rawChannel = rawChannel.Replace(",album: ", sep); //Remove Label Album
                rawChannel = rawChannel.Replace("\"", ""); // Too many double-quotes
	            //String[] channel = explode($sep, $rawChannel); // num, artist, song, album
                String[] channel = rawChannel.Split(new string[] { sep }, StringSplitOptions.None);
            	
                //channels.Find(Find(Convert.ToInt32(channel[0])));
                Find(Convert.ToInt32(channel[0])).addPlayingInfo(channel);

                // XXX -- Need to write these values to some object (XMChannel?) so they're usable.


	            String[] whatson = channel;



            }

        }


        public String setCookies(CookieCollection cookies)
        {
            cookieCount = cookies.Count;
            String[] cookieStr = new String[cookieCount];
            String cookieJar = "";
            int i = 0;
            foreach (Cookie cookie in cookies)
            {
                cookieStr[i] = cookie.ToString();
                //cookieStr[i].Substring(0, cookieStr[i].IndexOf(";"));
                cookieJar = cookieJar + cookieStr[i] + "; ";
                
                i++;

            }
            return cookieJar;
          
        }

        public XMChannel Find(int channum)
        {
            XMChannel result = channels.Find(
            delegate(XMChannel chan)
            {
                return chan.num == channum;
            }
            );
            if (result != null)
            {
                return result;
            }
            else
            {
                XMChannel tmp = new XMChannel("", 0, "", "");
                return tmp;
            }
        }

        public string play(int channelnum, String speed)
        {
            String address;
            if (isLive)
            {
                address = "http://www.xmradio.com/player/listen/play.action?channelKey=" + channelnum + "&newBitRate=" + speed;
            }
            else
            {
                address = "http://users.pcfire.net/~wolf/XMReader/play.action";
            }
            Uri tmp = new Uri(address);
            URL playerURL = new URL(tmp);
            playerURL.setRequestHeader("Cookie", cookies);
            playerURL.fetch();
            string URL = playChannel(playerURL);
            //response is closed in playChannel();
            lastChannelPlayed = channelnum;
            return URL;
         }

        public string playChannel(URL url)
        {
            String contentURL;
            String pattern = "<PARAM NAME=\"FileName\" VALUE=\"(.*?)\">";
            String m = Regex.Match(url.response(),pattern).ToString();
            m = m.Replace("<PARAM NAME=\"FileName\" VALUE=\"", "");
            m = m.Replace("\">", "");

            if (!m.Equals(""))
            {
                contentURL = m.ToString();
                output(contentURL, "debug");
            }
            else 
            {
                output("XM Radio Online Error - Not Logged In", "error");
                isLoggedIn = false;
                contentURL = null;
            }

            return (contentURL);
        }

        public void output(String output, String level)
        {
            DateTime currentTime = DateTime.Now;
            output = currentTime.ToString("%H:") + currentTime.ToString("mm:") + currentTime.ToString("ss") + "  " + output + "\n";
            OutputData = OutputData + output;
            log(output);

            if (level.Equals("debug") && !isDebug)
            {
                return;
            }


            //Tell the Form to write to the messagebox in the UI
            Form1.output(output, level, ref outputbox);

        }

        public void log(String logentry)
        {
            theLog = theLog + logentry;
        }

        public void writeLog()
        {
            string path = @"log.txt";
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);

            StreamWriter textOut = new StreamWriter(fs);
            textOut.Write(theLog);

            textOut.Close();
        }
        




    }
}
