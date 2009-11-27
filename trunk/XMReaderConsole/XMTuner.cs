using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace XMReaderConsole
{
    class XMTuner
    {
        String user;
        String password;
        String cookies;
        String[] cookieBox;
        String contentURL;
        bool isDebug = true;
        bool isLive = false;
        public String OutputData = "";
        public String theLog = "";
        public int cookieCount = 0;
        
        public String bitrate = "high";
        List<XMChannel> channels = new List<XMChannel>();

        public XMTuner()
        {
        }

        public XMTuner(String username, String passw)
        {

            user = username;
            password = passw;
           
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

            if (isDebug)
            {
                output("Connecting to: "+XMURL);
            }
            
            String data = "playerToLaunch=xm&encryptPassword=true&userName="+user+"&password="+password;
            URL loginURL = new URL(XMURL);
            loginURL.fetch(data);

            int responseCode = loginURL.response();
            output("Server Response: "+responseCode.ToString());



            
            if (loginURL.response() > 0 && loginURL.response() < 400)
            {
                //CookieCollection cookieJar = loginURL.getCookies();
                cookies = setCookies(loginURL.getHeader("Set-Cookie"));
                cookieBox = loginURL.getHeader("Set-Cookie");
                cookieCount = cookieBox.Length;

                if (cookieCount > 0)
                {
                    
                    if (cookieCount <= 1)
                    {
                        output("Login failed: Bad Us/Ps");
                    }
                    else { output("Logged in as " + user+" ("+cookieCount+")"); }

                    //loadConfig();
                    loadChannelData();
                }
                else {output("Number of Cookies: "+cookieCount.ToString()); }
            }
            else 
            { 
                output("Login Failed: " + loginURL.response()); 
            }
              
        }

        public void loadConfig()
        {
            URL configURL = new URL("http://www.xmradio.com/player/listen/playerShell.action");
            configURL.setRequestHeader("Cookie", cookies);
            configURL.fetch();

            if (configURL.response() > 0 && configURL.response() < 400)
            {
                //String config = configURL.result().Split("</script>");
            }
        }

        public bool isLoggedIn()
        {
            if (cookies != null && cookieCount > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void loadChannelData()
        {
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
                String resultStr = channelURL.result();
                if (channelURL.response() >= 200 && channelURL.response() < 300 && resultStr.IndexOf(":[],") == -1)
                {
                    goodData = true;
                    setChannelData(resultStr);
                }
                if (goodData)
                {
                    break;
                }
            }
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
                        if (isDebug)
                        {
                            output(neighborhood + " " + num + " " + channelData[1] + " " + channelData[3]);
                        }
                        channels.Add(tempChannel);
                    }

                }
            }
        }

        public List<XMChannel> getChannels()
        {
            return channels;
        }

        public void loadWhatsOn()
        {
            URL whatsOnURL = new URL("http://www.xmradio.com/padData/pad_data_servlet.jsp?all_channels=true&remote=true");
            whatsOnURL.setRequestHeader("Cookie", cookies);
            whatsOnURL.fetch();
        }

        public String setCookies(CookieCollection cookies)
        {
            int cookieCount = cookies.Count;
            String[] cookieStr = new String[cookieCount];
            String cookieJar = "";
            int i = 0;
            foreach (Cookie cookie in cookies)
            {
                cookieStr[i] = cookie.ToString();
                cookieStr[i].Substring(0, cookieStr[i].IndexOf(";"));
                cookieJar = cookieJar + cookieStr[i] + "; ";
                
                i++;

            }
            return cookieJar;
          
        }

        public String setCookies(String[] cookiesArr)
        {
            int cookieCount = cookiesArr.Length;
            String cookieJar = "";
            int i = 0;
            foreach (String cookie in cookiesArr)
            {
                output(cookiesArr[i]);
                cookiesArr[i].Substring(0, cookiesArr[i].IndexOf(";"));
                cookieJar = cookieJar + cookiesArr[i] + "; ";
                i++;

            }
            return cookieJar;

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
            return URL;
         }

        public string playChannel(URL url)
        {
            String pattern = "<PARAM NAME=\"FileName\" VALUE=\"(.*?)\">";
            String m = Regex.Match(url.result(),pattern).ToString();
            m = m.Replace("<PARAM NAME=\"FileName\" VALUE=\"", "");
            m = m.Replace("\">", "");

            if (m != null)
            {
                contentURL = m.ToString();
                if (isDebug)
                {
                    output(contentURL);
                }
                else
                {
                    output("Write witty output for normal run");
                }
            }
            else 
            {
                login(); //play again
                log("Relogin");
            }

            return (contentURL);
        }

        public void output(String output)
        {
            OutputData = OutputData + output + "\n";
            log(output);
            //Form1.refreshOutput(output);
        }

        public void log(String logentry)
        {
            theLog = theLog + logentry + "\n";
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
