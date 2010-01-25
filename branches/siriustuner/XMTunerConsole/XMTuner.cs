using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Web;
using System.Xml;

namespace XMTuner
{
    class XMTuner
    {
        //Flags
        bool isLive = true;

        //Config options...
        String user;
        String password;
        Boolean useLocalDatapath = false;

        List<XMChannel> channels = new List<XMChannel>();
        Log log;
        String cookies;
        public String network = "SIRIUS";
        public int lastChannelPlayed;
        public bool isLoggedIn;
        public Boolean loadedExtendedChannelData = false;
        Boolean loadedChannelMetadataCache = false;
        Boolean isProgramDataCurrent = false;
        int cookieCount = 0;
        public List<String> recentlyPlayed = new List<String>();


        public XMTuner()
        {
        }

        public XMTuner(String username, String passw, Log logging, Boolean pUseLocalDatapath)
        {
            user = username;
            password = passw;
            log = logging;
            useLocalDatapath = pUseLocalDatapath;
#if !DEBUG
            isLive = true;
#endif
            login();
          
        }

        private void login()
        {
            //Prefetch
            String SiriusPlayerURL = "http://www.sirius.com/player/home/siriushome.action";
            URL playerURL = new URL(SiriusPlayerURL);
            output("Connecting to: " + SiriusPlayerURL, "debug");
            playerURL.setCookieContainer();
            playerURL.fetch();
            output("Server Response: " + playerURL.getStatus().ToString(), "debug");
            CookieCollection playerCookies = playerURL.getCookies();
            //Add JS controlled required cookies
            playerCookies.Add(new Cookie("sirius_consumer_type", "sirius_online_subscriber", "", "www.sirius.com"));
            playerCookies.Add(new Cookie("sirius_login_type", "subscriber", "", "www.sirius.com"));
            cookies = setCookies(playerCookies);

            output("Number of Cookies: " + cookieCount.ToString(), "debug");

            String data = playerURL.response();

            int start = data.IndexOf("<!-- CAPTCHA:BEGIN -->");
            int end = data.IndexOf("<!-- CAPTCHA:END -->");

            data = data.Substring(start, end-start);

            start = data.IndexOf("/mp/captcha/image/");
            String _captchaNum = data.Substring(start, 25);
            String[] _captchaNumA = _captchaNum.Split('_');
            int captchaNum = Convert.ToInt32(_captchaNumA[1]);

            start = data.IndexOf("name=\"captchaID\"");
            String captchaID = data.Trim().Substring(start, 35);
            String[] _captchaID = captchaID.Split(new String[] { "value=\"" }, StringSplitOptions.None);
            captchaID = _captchaID[1].Trim().Replace("\">", "");

            String captchaResponse = getCaptchaResponse(captchaNum);


            // Do Actual Login
            String SiriusLoginURL;
            if (isLive) 
            {
                SiriusLoginURL = "http://www.sirius.com/player/login/siriuslogin.action";
            }
            else
            {
                SiriusLoginURL = "http://users.pcfire.net/~wolf/XMReader/test.php";
            }

            output("Connecting to: "+SiriusLoginURL, "debug");
            
            data = "userName="+HttpUtility.UrlEncode(user)+"&password="+HttpUtility.UrlEncode(password)+"&__checkbox_remember=true&captchaEnabled=true&captchaID="+HttpUtility.UrlEncode(captchaID)+"&timeNow=null&captcha_response="+captchaResponse;
            URL loginURL = new URL(SiriusLoginURL);
            loginURL.setRequestHeader("Cookie", cookies);
            loginURL.setCookieContainer(playerCookies);
            loginURL.fetch(data);

            int responseCode = loginURL.getStatus();
            output("Server Response: " + responseCode.ToString(), "debug");
            
            if (loginURL.getStatus() > 0 && loginURL.getStatus() < 400)
            {
                CookieCollection loginCookies = loginURL.getCookies();
                loginCookies.Add(playerCookies);
                cookies = setCookies(loginCookies);

                output("Number of Cookies: " + cookieCount.ToString(), "debug");

                if (cookieCount > 0)
                {
                    
                    if (cookieCount <= 3)
                    {
                        output("Login failed: Bad Username or Password", "error");
                    }
                    else
                    {
                        output("Logged in as " + user, "info");
                        Boolean cd = loadChannelData();
                        if (cd)
                        {
                            //We're logged in and have valid channel information, set login flag to true
                            isLoggedIn = true;

                            loadSiriusChannelGuide();

                            //Attempt to preload channel metadata
                            loadChannelMetadata(true);

                            //Continue to preloading whatsOn data
                            doWhatsOn();
                        }
                        else
                        {
                            //If we don't have chanData, consider ourselves not-logged-in
                            isLoggedIn = false;
                            output("Login failed: Unable to retrieve channel data.", "error");
                        }

                    }
                    
                }
            }
            else 
            {
                output("Login Failed: " + loginURL.getStatus(), "error"); 
            }
            loginURL.close();
        }

        private void loadSiriusChannelGuide()
        {
            //String URL = "http://users.pcfire.net/~wolf/XMReader/sirius/ContentServer";
            String URL = "http://www.sirius.com/servlet/ContentServer?pagename=Sirius/XML/ChannelGuideXML&c=ChannelLineup&cid=1218563499691&pid=SIR_AUD_EVT_SXM&catid=all"; //&pid=SIR_IP_EVT&catid=all";
            URL channelGuideURL = new URL(URL);
            channelGuideURL.fetch();
            String data = channelGuideURL.response().Trim();
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(data);
            XmlNodeList list = xmldoc.GetElementsByTagName("channel");

            foreach (XmlNode channel in list)
            {
                Int32 chanNum = Convert.ToInt32(channel.ChildNodes[4].InnerText);
                String chanKey = channel.Attributes["key"].Value;
                String chanURL = channel.ChildNodes[8].InnerText;

                String[] details = new String[2];
                details[0] = chanURL;
                details[1] = chanKey;

                XMChannel c = Find(chanNum);
                c.addChannelData(details);
            }

        }

        private void logout()
        {
            cookieCount = 0;
            cookies = null;
            channels.Clear();
            loadedExtendedChannelData = false;
            loadedChannelMetadataCache = false;
            isProgramDataCurrent = false;
            lastChannelPlayed = 0;
            isLoggedIn = false;
        }

        private void relogin()
        {
            logout();
            login();
        }

        private string getCaptchaResponse(int captchaNum)
        {
            String[] captchas = new String[101];
            captchas[1] = "wrQ2";
            captchas[2] = "LtFK";
            captchas[3] = "2bxh";
            captchas[4] = "Mf6D";
            captchas[5] = "fEXY";
            captchas[6] = "Wc46";
            captchas[7] = "fYP7";
            captchas[8] = "X6aw";
            captchas[9] = "nQQd";
            captchas[10] = "rt3k";
            captchas[11] = "kQhf";
            captchas[12] = "f2WG";
            captchas[13] = "aTLX";
            captchas[14] = "Qnaf";
            captchas[15] = "CA2T";
            captchas[16] = "cY36";
            captchas[17] = "xddQ";
            captchas[18] = "yaYf";
            captchas[19] = "4P67";
            captchas[20] = "7ekW";
            captchas[21] = "yZLN";
            captchas[22] = "RhLd";
            captchas[23] = "4eAc";
            captchas[24] = "bHKA";
            captchas[25] = "t4kw";
            captchas[26] = "AZQE";
            captchas[27] = "RWhN";
            captchas[28] = "7rPD";
            captchas[29] = "fYWP";
            captchas[30] = "7HCb";
            captchas[31] = "aR3L";
            captchas[32] = "TDkT";
            captchas[33] = "kf4Y";
            captchas[34] = "yfF2";
            captchas[35] = "eyDh";
            captchas[36] = "yWnK";
            captchas[37] = "NFWm";
            captchas[38] = "2n4d";
            captchas[39] = "634t";
            captchas[40] = "YnAH";
            captchas[41] = "MHPQ";
            captchas[42] = "N26M";
            captchas[43] = "Ra4C";
            captchas[44] = "dR4e";
            captchas[45] = "P6CZ";
            captchas[46] = "cnaW";
            captchas[47] = "W6Wm";
            captchas[48] = "Wm3y";
            captchas[49] = "mrdG";
            captchas[50] = "3KhR";
            captchas[51] = "p6fY";
            captchas[52] = "AGeh";
            captchas[53] = "ctDC";
            captchas[54] = "HDZY";
            captchas[55] = "WNKM";
            captchas[56] = "K72H";
            captchas[57] = "k627";
            captchas[58] = "PMW2";
            captchas[59] = "mWew";
            captchas[60] = "Y3YA";
            captchas[61] = "r67T";
            captchas[62] = "nDpE";
            captchas[63] = "Q7MQ";
            captchas[64] = "KLW2";
            captchas[65] = "pyDR";
            captchas[66] = "AQkH";
            captchas[67] = "wdfW";
            captchas[68] = "eWQh";
            captchas[69] = "ttEP";
            captchas[70] = "tn6r";
            captchas[71] = "P6yx";
            captchas[72] = "nRKW";
            captchas[73] = "eXEb";
            captchas[74] = "YwNZ";
            captchas[75] = "MHZt";
            captchas[76] = "f7mc";
            captchas[77] = "Rymy";
            captchas[78] = "MTPC";
            captchas[79] = "rc3k";
            captchas[80] = "Xebn";
            captchas[81] = "ffGH";
            captchas[82] = "6Y2D";
            captchas[83] = "mbKx";
            captchas[84] = "6nCH";
            captchas[85] = "tHyG";
            captchas[86] = "RtAE";
            captchas[87] = "hWE2";
            captchas[88] = "3F6D";
            captchas[89] = "dQpC";
            captchas[90] = "HACN";
            captchas[91] = "Ampy";
            captchas[92] = "mLEr";
            captchas[93] = "Mdt2";
            captchas[94] = "QGbL";
            captchas[95] = "PDQP";
            captchas[96] = "EEyC";
            captchas[97] = "MfmL";
            captchas[98] = "PQ3f";
            captchas[99] = "HPPc";
            captchas[100] = "pTXc";

            return captchas[captchaNum];

        }

        private String getDataPath(String file)
        {
            String directory = "";
            if (useLocalDatapath == false)
            {
                directory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "XMTuner");
                if (!Directory.Exists(directory)) { Directory.CreateDirectory(directory); }
            }
            if (file.Equals(""))
            {
                return directory;
            }
            else 
            {
                if (directory.Equals(""))
                {
                    return file;
                }
                else
                {
                    return directory + "\\" + file;
                }
            }
        }

        private Boolean isChannelDataCurrent()
        {
            return isDataCurrent("channellineup.cache", -1);
        }

        private Boolean isDataCurrent(String file, Double value)
        {
            String path = getDataPath(file);
            DateTime dt = File.GetLastWriteTime(path);
            DateTime maxage = DateTime.Now;
            maxage = maxage.AddDays(-1);
            if (dt > maxage)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool loadChannelData()
        {
            Boolean lineupLoaded;
            output("Loading channel lineup...", "info");
            if (isChannelDataCurrent())
            {
                //Load from file
                String path = getDataPath("channellineup.cache");
               
                try
                {
                    FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
                    StreamReader textIn = new StreamReader(fs);
                    String rawchanneldata = textIn.ReadToEnd();
                    textIn.Close();
                    lineupLoaded = setChannelData(rawchanneldata);

                }
                catch
                {
                    lineupLoaded = false;
                }
                if (!lineupLoaded)
                {
                    //Force expiration of bad data. (Don't delete it in case something useful is in the file for debugging)
                    File.SetLastWriteTime(path, new DateTime(1985, 1, 1)); 
                    output("Failed to load channel lineup.", "error");
                    return false;
                }
                output("Channel lineup loaded successfully (from cache).", "info");
                return true;
            }
            else
            {
                //Reload from server
                return dnldChannelData();
            }
        }

        private bool dnldChannelData()
        {
            output("Downloading channel lineup...", "info");
            Boolean goodData = false;
            int j;
            //We try 5 times...
            for (j = 1; j <= 5; j++)
            {
                string url;
                if (isLive)
                {
                   url = "http://www.sirius.com/player/channel/ajax.action?reqURL=player/2ft/channelData.jsp?remote=true&all_channels=true";
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
                    //Returns Bool; False if invalid (null) channel data, true on success
                    goodData = setChannelData(resultStr);

                    //Try to catch the need to relogin because of dead channel data without retrying 5 times...
                    if (!goodData && isLoggedIn)
                    {
                        isLoggedIn = false;
                        output("Downloaded channel data had no stations. Scheduling relogin (Wait a few seconds...)", "info");
                        return false;
                    }
                }
                if (goodData)
                {
                    saveChannelData(resultStr);
                    isLoggedIn = true;
                    output("Channel lineup loaded successfully.", "info");
                    return true;
                }
                isLoggedIn = false;
                output("Error downloading channel data.. Will try again in 5 seconds (Attempt " + j + " of 5, Error " + channelURL.getStatus().ToString() + ")", "error");
                System.Threading.Thread.Sleep(5000);
            }

            output("Failed to load channel lineup", "error");
            return false;
        }

        private Boolean setChannelData(String rawchanneldata)
        {
            if (rawchanneldata.Contains("\"allchannels\",null")) {
                //isLoggedIn = false;
                return false;
            }

            if (channels != null)
            {
                channels.Clear();
                loadedExtendedChannelData = false;
            }
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
            return true;
        }

        private void saveChannelData(String rawchanneldata)
        {
            String path = getDataPath("channellineup.cache");

            try {
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(fs);
            textOut.Write(rawchanneldata);
            textOut.Close();
            } catch (IOException e) {
                output("Error encountered saving channel lineup to cache. ("+e.Message+")", "error");
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
                output("XM Session Timed-out. Reconnecting...", "info");
                relogin();
                return;
            }

            if (!isChannelDataCurrent())
            {
                dnldChannelData();
            }

            if (loadedExtendedChannelData == false || isProgramDataCurrent == false)
            {
                MethodInvoker extendedChannelDataDelegate = new MethodInvoker(loadExtendedChannelData);
                extendedChannelDataDelegate.BeginInvoke(null, null);
            }
            
            MethodInvoker simpleDelegate = new MethodInvoker(loadWhatsOn);
            simpleDelegate.BeginInvoke(null, null);
            
        }

        private void loadExtendedChannelData()
        {
            loadChannelMetadata();
            if (isProgramDataCurrent == false)
            {
                loadProgramGuideData();
            }
        }

        private void loadWhatsOn()
        {
            output("Update What's On Data...", "debug");
            String whatsOnURL;
            if (isLive)
            {
                whatsOnURL = "http://www.xmradio.com/padData/pad_data_servlet.jsp?all_channels=true&remote=true&rpc=XMROUS";
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

            setRecentlyPlayed();
        }

        private void setWhatsonData(String rawdata)
        {
            if (rawdata.Equals(""))
            {
                return;
            }
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
                FindbyXM(Convert.ToInt32(channel[0])).addPlayingInfo(channel);
            }

        }


        private String setCookies(CookieCollection cookies)
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

        public XMChannel FindbyXM(int channum)
        {
            XMChannel result = channels.Find(
            delegate(XMChannel chan)
            {
                return chan.xmxref == channum;
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

        public XMChannel Find(String channame)
        {
            XMChannel result = channels.Find(
            delegate(XMChannel chan)
            {
                return chan.name == channame;
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
            String channelKey = Find(channelnum).channelKey;
            output("Playing stream for Sirius "+channelnum+" ("+channelKey+")", "debug");
            String address;
            if (isLive)
            {
                address = "http://www.sirius.com/player/listen/play.action?channelKey=" + channelKey + "&newBitRate=" + speed;
            }
            else
            {
                address = "http://users.pcfire.net/~wolf/XMReader/sirius/play.action";
            }
            Uri tmp = new Uri(address);
            URL playerURL = new URL(tmp);
            playerURL.setRequestHeader("Cookie", cookies);
            playerURL.fetch();
            string URL = playChannel(playerURL);
            //response is closed in playChannel();
            lastChannelPlayed = channelnum;
            setRecentlyPlayed();
            return URL;
         }

        private string playChannel(URL url)
        {
            String contentURL = null;
            String pattern = "<PARAM (NAME|name)=\"FileName\" (VALUE|value)=\"(.*?)\">";
            String data = url.response();
            String m = Regex.Match(data, pattern ).ToString();
            m = m.Replace("<PARAM NAME=\"FileName\" VALUE=\"", "");
            m = m.Replace("<PARAM name=\"FileName\" value=\"", "");
            m = m.Replace("\">", "");

            if (!m.Equals(""))
            {
                if (m.Contains("asxfwrd.action"))
                {
                    contentURL = playSiriusChannel(m);
                }
                else
                {
                    contentURL = m.ToString();
                }
            }

            if (contentURL == null)
            {
                output("Failed fetching stream for channel...", "error");
                //output("XM Radio Online Error - Not Logged In", "error");
                //isLoggedIn = false;
            }
            else
            {
                output(contentURL, "debug");
            }

            return (contentURL);
        }

        private String playSiriusChannel(String url)
        {
            url = "http://www.sirius.com" + url;
            //url = url.Replace("playasxpc", "playasxmac");
            URL asxFwrdURL = new URL(url);
            asxFwrdURL.setRequestHeader("Cookie", cookies);
            asxFwrdURL.fetch();
            url = asxFwrdURL.response();
            if (url.Equals("null"))
            {
                url = null;
            }
            return url;
        }

        //Helper method so we don't have to pass log around everywhere....
        public void output(String output, String level)
        {
            log.output(output, level);
        }

        private void loadChannelMetadata()
        {
            output("Loading extended channel data...", "info");
            if (isDataCurrent("channelmetadata.cache", -5))
            {
                loadChannelMetadata(false);
            }
            else
            {
                dnldChannelMetadata();
            }
        }
        private void loadChannelMetadata(Boolean fastLoad)
        {
            if (fastLoad == true)
            {
                output("Loading extended channel data... (from cache)", "info");
            }
            //We only load cached data once (so if fastLoad succeeded.. 
            // Normal mode can return early
            if (loadedChannelMetadataCache == true)
            {
                output("Extended channel data is loaded and current, no update needed.", "info");
                if (fastLoad == false)
                {
                    loadedExtendedChannelData = true; 
                }
                return;
            } 
            String file = "channelmetadata.cache";
            String path = getDataPath(file);
            FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader textIn = new StreamReader(fs);
            String rawchannelmetadata = textIn.ReadToEnd();
            textIn.Close();
            loadedChannelMetadataCache = setChannelMetadata(rawchannelmetadata);
            if (!loadedChannelMetadataCache)
            {
                //Force expiration of bad data. (Don't delete it in case something useful is in the file for debugging)
                File.SetLastWriteTime(path, new DateTime(1985, 1, 1));
                output("Failed to load extended channel data (from cache).", "error");
            }
            else
            {
                output("Extended channel data loaded successfully (from cache)", "info");
                if (fastLoad == false)
                {
                    loadedExtendedChannelData = true;
                }
            }
        }

        private void dnldChannelMetadata()
        {
            output("Downloading extended channel data...", "info");
            String channelMetaURL;
            if (isLive)
            {
                channelMetaURL = "http://www.xmradio.com/onxm/index.xmc";
            }
            else
            {
                channelMetaURL = "http://users.pcfire.net/~wolf/XMReader/epg/index.xmc";
            }
            URL channelMetaData = new URL(channelMetaURL);
            output("Fetching: " + channelMetaURL, "debug");
            channelMetaData.setRequestHeader("Cookie", cookies);
            channelMetaData.fetch();

            int responseCode = channelMetaData.getStatus();
            output("Server Response: " + responseCode.ToString(), "debug");
            String rawChannelMetaData = channelMetaData.response();
            loadedExtendedChannelData = setChannelMetadata(rawChannelMetaData);
            if (loadedExtendedChannelData == true)
            {
                output("Extended channel data loaded successfully", "info");
                saveChannelMetadata(rawChannelMetaData);
            }
            channelMetaData.close();
        }

        private Boolean setChannelMetadata(String rawData)
        {
            try
            {
                rawData = rawData.Replace("//rig.addChannel", "");
                int start = rawData.IndexOf("rig.addChannel") + 15;
                int length = rawData.IndexOf("loadPage();") - start;
                rawData = rawData.Trim().Substring(start, length);
                rawData = rawData.Replace("\t", "");
                rawData = rawData.Replace(");", "");
                rawData = rawData.Replace("\r\n", "");
                rawData = rawData.Replace("\\\"", "");
            }
            catch (Exception)
            {
               return false;
            }

            String[] data = rawData.Split(new string[] { "rig.addChannel(" }, StringSplitOptions.None);
            String baseurl = "http://www.sirius.com";
            foreach(String _value in data) {
                String[] value = _value.Replace("\\","").Split(new string[] { "\"," }, StringSplitOptions.None);
            	          
                String[] newdata = new String[4];
                String name = value[0].Replace("\"", "");
                newdata[0] = value[2].Replace("\"", ""); //XM's Num
                newdata[2] = baseurl + value[6].Replace("\"", ""); // Logo (45x40)
                newdata[3] = baseurl + value[10].Replace("\"", ""); // Logo (138x50)

                /* Because Sirius/XM have to be unique, this channel doesn't have the same name and won't
                 *  be correctly cross-referenced without being special-cased, for now. */
                if (name.ToUpper().Equals("NPR NOW")) {
                    name = "NPR";
                }


                Find(name.ToUpper()).addChannelMetadata(newdata);
            }
            return true;
        }

        private void saveChannelMetadata(String rawdata)
        {
            String path = getDataPath("channelmetadata.cache");

            try
            {
                FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
                StreamWriter textOut = new StreamWriter(fs);
                textOut.Write(rawdata);
                textOut.Close();
            }
            catch (IOException e)
            {
                output("Error encountered saving channel metadata to cache. (" + e.Message + ")", "error");
            }
        }

        private void setRecentlyPlayed()
        {
            XMChannel npChannel = Find(lastChannelPlayed);
            if (npChannel.num == 0 || npChannel.song == null || npChannel.song.Equals(""))
            { 
                return;
            }
            String currentTime = DateTime.Now.ToString("T");
            String entry = npChannel.ShortName() + " - " + npChannel.artist + " - " + npChannel.song;

            if (recentlyPlayed.Count > 0)
            {
                String[] lastEntry = recentlyPlayed.FirstOrDefault().Split(':');
                if (lastEntry[3].Trim() == entry) {
                    return;
                }
            }

            recentlyPlayed.Insert(0, currentTime + ": " + entry);
            if (recentlyPlayed.Count > 25)
            {
                recentlyPlayed.RemoveRange(25, recentlyPlayed.Count - 25);
            }
        }

        private String getChannelsNums()
        {
            String channels = "";
            foreach (XMChannel chan in getChannels())
            {
                channels += chan.xmxref+",";
            }
            return channels;
        }

        private void loadProgramGuideData()
        {
            output("Loading program guide...", "info");

            String channums = HttpUtility.UrlEncode(getChannelsNums());

            //endDate	11122009030000 (date("dmYHis", time()+86400);)
            String enddate = DateTime.Now.AddDays(1).ToString("ddMMyyyyHHmmss");

            //startDate	11122009000000 (date("dmYHis", time());)
            String startdate = DateTime.Now.ToString("ddMMyyyyHHmmss");

            String programGuideURL;
            if (isLive)
            {
                programGuideURL = "http://www.xmradio.com/epg.program_schedules.xmc?channelNums=" + channums + "&endDate=" + enddate + "&startDate=" + startdate;
            }
            else
            {
                programGuideURL = "http://users.pcfire.net/~wolf/XMReader/epg/program_schedules.xmc1";
            }
            URL programGuideData = new URL(programGuideURL);
            output("Fetching: " + programGuideURL, "debug");
            programGuideData.setRequestHeader("Cookie", cookies);
            programGuideData.fetch();

            int responseCode = programGuideData.getStatus();
            output("Server Response: " + responseCode.ToString(), "debug");
            if (responseCode.Equals(200))
            {
                isProgramDataCurrent = setProgramGuideData(programGuideData.response());
            }
            if (isProgramDataCurrent)
            {
                output("Program guide loaded successfully.", "info");
            }
            else
            {
                output("Failed to load program guide data (will retry).", "error");
            }
            programGuideData.close();
        }

        private Boolean setProgramGuideData(String rawData)
        {
            rawData = rawData.Replace("{\"programScheduleList\":[{","");
            rawData = rawData.Replace("\"repeat\":\"","");
            rawData = rawData.Replace("\",\"class\":\"com.xm.epg.domain.EpgSchedule\",\"scheduleId\":","");


            rawData = rawData.Replace("\"class\":\"com.xm.epg.domain.EpgProgram\",\"programId\":","");


            int start = rawData.IndexOf("\"epgProgram\":{")+14;

            String[] rawProgramData = rawData.Substring(start).Split(new string[] {"\"epgProgram\":{"}, StringSplitOptions.None);


            String sep = "::XMTUNER-SEPERATOR::";
            foreach(String _rPData in rawProgramData) {

	            //Program ID, Program Name, Unique ID, StartTime, Last Occurence, Unique ID, First Occurance, Duration, ChannelNum, EndTime, JUNK
	            String rPData = _rPData.Replace(",\"name\":\"",sep);
	            rPData = rPData.Replace("\",\"~unique-id~\":\"",sep);
	            rPData = rPData.Replace("\"},\"startTime\":\"",sep);
	            rPData = rPData.Replace("\",\"lastOccurence\":\"",sep);
	            rPData = rPData.Replace("\",\"firstOccurence\":\"",sep);
	            rPData = rPData.Replace("\",\"duration\":",sep);
	            rPData = rPData.Replace(",\"channelNum\":",sep);
	            rPData = rPData.Replace(",\"endTime\":\"",sep);
	            rPData = rPData.Replace("\"},{",sep); //Break off leftover junk
	            rPData = rPData.Replace("\"}],",sep); //Break off leftover junk (Final Line)
            	
	            String[] PData = rPData.Split(new String[] { sep }, StringSplitOptions.None);
            	
	            //ChannelNum, Program ID, Program Name, Duration, Start Time, End Time
	            Int32 num = Convert.ToInt32(PData[8]);
                XMChannel channel = FindbyXM(num);
                String[] program = new String[6];
                        program[0] = PData[8];
                        program[1] = PData[0];
                        program[2] = PData[1];
                        program[3] = PData[7];
                        program[4] = PData[3];
                        program[5] = PData[9];
                
                channel.addProgram(program);
            }
            return true;
        }

        private String[] getProgram(List<String[]> programs, Boolean getNext)
        {
            String[] program = null;
            Boolean foundFirstValidProgram = false;
            foreach (String[] _program in programs)
            {
                if (validateProgram(_program) == true)
                {
                    program = _program;
                    if (getNext == false)
                    {
                        break;
                    }
                    if (getNext && foundFirstValidProgram)
                    {
                        break;
                    }
                    foundFirstValidProgram = true;
                }
            }

            //We assume that if we can't find a valid program, we should reload the program data.
            //So we set the variable here to cause such a reload to be rescheduled.
            if (program == null)
            {
                isProgramDataCurrent = false;
            }

            return program;
        }

        public String[] getCurrentProgram(List<String[]> programs)
        {
            return getProgram(programs, false);
        }
        public String[] getNextProgram(List<String[]> programs)
        {
            return getProgram(programs, true);
        }

        private Boolean validateProgram(String[] program)
        {
            DateTime pEndDate = DateTime.Parse(program[5]);
            if (DateTime.Now > pEndDate)
            {
                return false;
                //Reconstruct program data...
            }
            return true;
        }
    }
}
