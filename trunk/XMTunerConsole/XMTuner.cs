using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Web;

namespace XMTuner
{
    class XMTuner
    {
        //Flags
        protected bool isLive = true;

        //Config options...
        protected String user;
        protected String password;
        public String network = "XM";
        public int numItems;

        //Objects
        public Config cfg;
        Log logSvc;
        public CacheManager cache;
        protected String cookies;
        protected List<XMChannel> channels = new List<XMChannel>();
        public List<String> recentlyPlayed = new List<String>();
        public Favorites favorites = new Favorites();

        //Runtime Flags
        public Boolean isLoggedIn;
        public Boolean tryingLogin = false;
        protected Boolean firstLogin = false;
        public Boolean loadedChannelMetadata = false;
        Boolean loadedChannelMetadataCache = false;
        Boolean useProgramGuide = true;
        Boolean isProgramDataCurrent = false;

        public Boolean preloadImageRunning = false;
        Boolean preloadedImages1R = false;
        Boolean preloadedImages = false;
        public Boolean preloadImagesUpdated = false;
        int preloadImageTimeout = 500;

        //General Globals
        protected int cookieCount = 0;
        public int lastChannelPlayed;
        public DateTime lastLoggedIn;
        public DateTime lastWhatsOnUpdate;
        int attempts = 1;
        System.Timers.Timer pulseTimer = new System.Timers.Timer(1800000);

        protected virtual String baseurl
        {
            get
            {
                return "http://www.xmradio.com";
            }
        }

        public XMTuner(Config cfg, Log logging) : this(cfg, logging, null) {}
        public XMTuner(Config cfg, Log logging, String netw)
        {
            this.cfg = cfg;
            this.user = cfg.username;
            this.password = cfg.password;
            this.numItems = cfg.numRecentHistory;

            logSvc = logging;

            if (netw != null) { network = netw; }

            cache = new CacheManager(logSvc, network);
#if !DEBUG
            isLive = true;
#endif
            if (!isLive) useProgramGuide = false;
            tryingLogin = true;
            handleLogin(login());
     
        }
        private void handleLogin(Boolean result)
        {
            int maxattempts = 5;
            int timeout = 10000;
            if (Form1.isService)
            {
                maxattempts = -1;
                timeout = 30000;
            }

            if (result == false)
            {
                if (maxattempts != -1 && attempts >= maxattempts)
                {
                    output("Login attempts exhausted, giving up...", LogLevel.Error);
                    attempts = 1;
                    tryingLogin = false;
                    return;
                }
                attempts++;
                output("Waiting to retry login... (Attempt " + attempts + " of "+maxattempts+")", LogLevel.Info);
                System.Threading.Thread.Sleep(timeout);
                handleLogin(login());
                return;
            }
            tryingLogin = false;

            attempts = 1;
        }

        protected virtual Boolean login()
        {
            CookieCollection startupCookies = new CookieCollection();
            //sirius_user_name=****@****.net;
            startupCookies.Add(new Cookie("sirius_user_name", user, "", "www.xmradio.com"));
            //sirius_password=******;
            startupCookies.Add(new Cookie("sirius_password", password, "", "www.xmradio.com"));
            //playerType=xm;
            startupCookies.Add(new Cookie("playerType", "xm", "", "www.xmradio.com"));
            //pad_user=yes;
            startupCookies.Add(new Cookie("pad_user", "yes", "", "www.xmradio.com"));
            //playspeed=high;
            startupCookies.Add(new Cookie("playspeed", "high", "", "www.xmradio.com"));
            //ep=XMROUS;
            startupCookies.Add(new Cookie("ep", "XMROUS", "", "www.xmradio.com"));
            //uiType=generic;
            startupCookies.Add(new Cookie("uiType", "generic", "", "www.xmradio.com"));
            //sirius_campaign_code=default;
            startupCookies.Add(new Cookie("sirius_campaign_code", "default", "", "www.xmradio.com"));
            //sirius_consumer_type=XMROUS;
            startupCookies.Add(new Cookie("sirius_consumer_type", "XMROUS", "", "www.xmradio.com"));
            //sirius_mp_bitrate_entitlement_cookie=high;
            startupCookies.Add(new Cookie("sirius_mp_bitrate_entitlement_cookie", "high", "", "www.xmradio.com"));
            //sirius_mp_bitrate_button_status_cookie=high
            startupCookies.Add(new Cookie("sirius_mp_bitrate_button_status_cookie", "high", "", "www.xmradio.com"));
            setCookies(startupCookies);

            Boolean loginResult = true;
            output("Logging into XM Radio Online", LogLevel.Info);

            String XMURL = "http://www.xmradio.com/player/channel/fwrd.action?pageName=category&categoryKey=&genreKey=";
            if (!isLive)
            {
                XMURL = "http://test.xmtuner.net/test.php";
            }

            //String data = "playerToLaunch=xm&encryptPassword=false&userName="+user+"&password="+password;
            URL loginURL = new URL(XMURL);
            output("Connecting to: " + XMURL + " ("+loginURL.getIP()+")", LogLevel.Debug);
            loginURL.setRequestHeader("Cookie", cookies);
            loginURL.setCookieContainer(startupCookies);
            loginURL.fetch(); //loginURL.fetch(data);

            int responseCode = loginURL.getStatus();
            output("Server Response: " + loginURL.getStatusDescription(), LogLevel.Debug);

            if (loginURL.isSuccess)
            {
                CookieCollection loginCookies = loginURL.getCookies();
                loginCookies.Add(startupCookies);
                cookies = setCookies(loginCookies);

                output("Number of Cookies: " + cookieCount.ToString(), LogLevel.Debug);

                if (cookieCount > 0)
                {
                    
                    if (cookieCount <= 1)
                    {
                        output("Login failed: Bad Username or Password", LogLevel.Error);
                        loginResult = false;
                    }
                    else
                    {
                        output("Logged in as " + user, LogLevel.Info);
                        /* For the purposes of quickLogin, we want to just do the actual login step
                           and let the normal data rebuilding occur incrementally on its own.*/
                        if (firstLogin == true)
                        {
                            isLoggedIn = true;
                            return true;
                        }
                        Boolean cd = loadChannelData();
                        if (cd)
                        {
                            //We're logged in and have valid channel information, set login flag to true
                            isLoggedIn = true;
                            lastLoggedIn = DateTime.Now;

                            //Attempt to preload channel metadata
                            loadChannelMetadata(true);

                            //Continue to preloading whatsOn data
                            doWhatsOn(true);

                            //Pulse Timer
                            startPulseTimer();
                        }
                        else
                        {
                            //If we don't have chanData, consider ourselves not-logged-in
                            isLoggedIn = false;
                            output("Login failed: Unable to retrieve channel data.", LogLevel.Error);
                            loginResult = false;
                        }

                    }
                    
                }
            }
            else 
            {
                output("Login Failed: " + loginURL.getStatusDescription(), LogLevel.Error);
                loginResult = false;
            }
            loginURL.close();
            if (loginResult == true)
            {
                firstLogin = true;
            }
            return loginResult;
        }

        public void logout()
        {
            cookieCount = 0;
            cookies = null;
            channels.Clear();
            loadedChannelMetadata = false;
            loadedChannelMetadataCache = false;
            isProgramDataCurrent = false;
            lastChannelPlayed = 0;
            isLoggedIn = false;
            firstLogin = false;
            pulseTimer.Stop();
        }

        private void relogin()
        {
            logout();
            login();
        }

        private void reloginQuick()
        {
            cookieCount = 0;
            cookies = null;
            isLoggedIn = false;
            if (login())
            {
                output("Relogin Completed.", LogLevel.Info);
            }
            else
            {
                output("Relogin unsuccessful, will keep trying...", LogLevel.Info);
            }
        }

        protected void startPulseTimer()
        {
            pulseTimer.Elapsed += new System.Timers.ElapsedEventHandler(onPulse);
            pulseTimer.AutoReset = true;
            pulseTimer.Enabled = true;
        }

        private void onPulse(object source, System.Timers.ElapsedEventArgs e)
        {
            MethodInvoker simpleDelegate = new MethodInvoker(doPulse);
            simpleDelegate.BeginInvoke(null, null);
        }

        private Boolean isChannelDataCurrent()
        {
            return cache.isCached("channellineup.cache");
        }

        protected bool loadChannelData()
        {
            Boolean lineupLoaded;
            output("Loading channel lineup...", LogLevel.Info);
            cache.addCacheFile("channellineup.cache", "channel lineup", -1);
            if (isChannelDataCurrent())
            {
                //Load from file
                try
                {
                    String rawchanneldata = cache.getFile("channellineup.cache");
                    lineupLoaded = setChannelData(rawchanneldata);

                }
                catch
                {
                    lineupLoaded = false;
                }
                if (!lineupLoaded)
                {
                    //Force expiration of bad data. (Don't delete it in case something useful is in the file for debugging)
                    cache.invalidateFile("channellineup.cache");
                    output("Failed to load channel lineup.", LogLevel.Error);
                    return false;
                }
                output("Channel lineup loaded successfully (from cache).", LogLevel.Info);
                return true;
            }
            else
            {
                //Reload from server
                return dnldChannelData();
            }
        }

        protected Boolean dnldChannelData()
        {
            output("Downloading channel lineup...", LogLevel.Info);
            Boolean goodData = false;
            int j;
            //We try 5 times...
            for (j = 1; j <= 5; j++)
            {
                string url = baseurl + "/player/channel/ajax.action?reqURL=player/2ft/channelData.jsp?remote=true&all_channels=true";
                if (!isLive)
                {
                    url = baseurl + "/channeldata.jsp";
                }

                URL channelURL = new URL(url);
                channelURL.setRequestHeader("Cookie", cookies);
                channelURL.fetch();
                output("Server Response: " + channelURL.getStatusDescription(), LogLevel.Debug);

                String data = channelURL.response();
                if (channelURL.isSuccess && data.IndexOf(":[],") == -1)
                {
                    //Returns Bool; False if invalid (null) channel data, true on success
                    goodData = setChannelData(data);

                    //Try to catch the need to relogin because of dead channel data without retrying 5 times...
                    if (!goodData && isLoggedIn)
                    {
                        isLoggedIn = false;
                        output("Downloaded channel data had no stations. Scheduling relogin (Wait a few seconds...)", LogLevel.Info);
                        return false;
                    }

                    if (goodData)
                    {
                        cache.saveFile("channellineup.cache", data);
                        isLoggedIn = true;
                        output("Channel lineup loaded successfully.", LogLevel.Info);
                        return true;
                    }
                    else
                    {
                        isLoggedIn = false;
                        output("Downloaded channel data had no stations. Verify your subscription is active.", LogLevel.Error);
                        return false;
                    }
                }
                else
                {
                    isLoggedIn = false;
                    output("Error downloading channel data.. Will try again in 5 seconds (Attempt " + j + " of 5)", LogLevel.Error);
                    output("Error: " + channelURL.getStatusDescription() + "", LogLevel.Error);
                    System.Threading.Thread.Sleep(5000);
                }
            }

            output("Failed to load channel lineup", LogLevel.Error);
            return false;
        }

        protected Boolean setChannelData(String rawchanneldata)
        {
            if (rawchanneldata.Contains("\"allchannels\",null")) {
                //isLoggedIn = false;
                return false;
            }
            Boolean channelsReset = false;
            if (channels.Count > 0)
            {
                channels.Clear();
                channelsReset = true;
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
                        tempChannel = new XMChannel(neighborhood, num, channelData[1], channelData[3], this.network);
                        output(neighborhood + " " + num + " " + channelData[1] + " " + channelData[3], LogLevel.Debug);
                        channels.Add(tempChannel);
                    }

                }
            }
            if (channelsReset == true)
            {
                loadedChannelMetadata = false;
                loadedChannelMetadataCache = false;
                isProgramDataCurrent = false;
                loadChannelData_hook();
            }

            return true;
        }

        protected virtual void loadChannelData_hook()
        {
            /* The purpose of this method is to allow derived classes to 
             * do work when the channel data is (re)loaded.
             * For XM - no work is needed so this method is empty.
             */
            output("Load Channel Data Hook Called!", LogLevel.Notice);
        }

        private void doPulse()
        {
            if (!isLive) { return; }
            output("[Pulse] Checking session status...", LogLevel.Debug);

            string url = baseurl + "/player/channel/ajax.action?reqURL=player/2ft/channelData.jsp?remote=true&all_channels=true";

            URL channelURL = new URL(url);
            channelURL.setRequestHeader("Cookie", cookies);
            channelURL.fetch();

            String data = channelURL.response();
            if (channelURL.isSuccess && data.IndexOf(":[],") == -1)
            {
                //Returns Bool; False if invalid (null) channel data, true on success
                if (data.Contains("\"allchannels\",null"))
                {
                    output("[Pulse] Session dead Scheduling relogin...", LogLevel.Notice);
                    isLoggedIn = false;
                    return;
                }
                output("[Pulse] Session alive.", LogLevel.Debug);
                return;
            }
            return;
        }

        public List<XMChannel> getChannels()
        {
            List<XMChannel> channelsCopy = new List<XMChannel>(channels.AsReadOnly());
            channelsCopy.Sort();
            channelsCopy.Reverse();
            return channelsCopy;
        }

        public string checkChannel(int num)
        {
            return Find(num).name;
        }

        public void doWhatsOn()
        {
            /* Check for notification of login failure, and relogin as needed */
            if (isLoggedIn == false)
            {
                output(network.ToUpper()+" Session Timed-out. Reconnecting...", LogLevel.Info);
                reloginQuick();
                return;
            }

            //Update cached channel data
            if (cache.enabled == true && !isChannelDataCurrent())
            {
                dnldChannelData();
            }

            //Update "What's On" data...
            doWhatsOn(false);

            /* Do the rest of the periodic data checks */
            doWhatsOnExtra();

        }

        protected void doWhatsOn(Boolean atstartup)
        {
            //Update "What's On" data... (in background thread)
            MethodInvoker simpleDelegate = new MethodInvoker(loadWhatsOn);
            simpleDelegate.BeginInvoke(null, null);

            doWhatsOnExtra();
        }

        private void doWhatsOnExtra()
        {
            // "Extended Channel Data" -> Channel Metadata (URL, Logos, etc)
            if (loadedChannelMetadata == false)
            {
                MethodInvoker extendedChannelDataDelegate = new MethodInvoker(loadChannelMetadata);
                extendedChannelDataDelegate.BeginInvoke(null, null);
            }

            // Program Guide
            if (useProgramGuide == true && isProgramDataCurrent == false)
            {
                MethodInvoker extendedChannelDataDelegate = new MethodInvoker(loadProgramGuideData);
                extendedChannelDataDelegate.BeginInvoke(null, null);
            }

            //Image Preload (Async)
            if (preloadedImages == false)
            {
                doPreloadImages();
            }
        }

        private void loadWhatsOn()
        {
            output("Update What's On Data...", LogLevel.Debug);
            String whatsOnURL = "http://www.xmradio.com/padData/pad_data_servlet.jsp?all_channels=true&remote=true&rpc=XMROUS";
            if (!isLive)
            {
                whatsOnURL = "http://test.xmtuner.net/all_data.js";
            }
            URL whatsOn = new URL(whatsOnURL);
            output("Fetching: "+whatsOnURL, LogLevel.Debug);
            whatsOn.setRequestHeader("Cookie", cookies);
            whatsOn.fetch();

            output("Server Response: " + whatsOn.getStatusDescription(), LogLevel.Debug);
            if (whatsOn.isSuccess)
            {
                setWhatsonData(whatsOn.response());
                setRecentlyPlayed();
                lastWhatsOnUpdate = DateTime.Now;
            }
            whatsOn.close();
            output("What's On Update Complete", LogLevel.Debug);

        }

        protected void setWhatsonData(String rawdata)
        {
            if (rawdata.Equals("") || rawdata.Equals("xms.sendRPCDone(\"whatson\", [ ] );"))
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
            	
                Find(Convert.ToInt32(channel[0]), true).addPlayingInfo(channel);
            }

        }

        protected String setCookies(CookieCollection cookies)
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
            return Find(channum, false);
        }

        protected XMChannel Find(int channum, Boolean useXM)
        {
            XMChannel result = channels.Find(
            delegate(XMChannel chan)
            {
                if (useXM == true && network.Equals("XM") == false)
                {
                    return chan.xmxref == channum;
                }
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

        public virtual string play(int channelnum, String speed)
        {
            return play(channelnum, speed, false);
        }

        protected string play(int channelnum, String speed, Boolean useKey)
        {
            XMChannel cD = Find(channelnum);
            if (cD.num == 0)
            {
                output("Invalid Channel Number " + channelnum, LogLevel.Error);
                return null;
            }
            String channelKey = channelnum.ToString();
            if (useKey == true)
            {
                channelKey = cD.channelKey;
            }
            output("Playing " + cD.ToString(), LogLevel.Info);
            output("Cookies:" + cookies, LogLevel.Debug);
            String address = baseurl + "/player/listen/play.action?channelKey=" + channelKey + "&newBitRate=" + speed;
            if (!isLive)
            {
                address = baseurl + "/play.action";
            }
            String URL = playChannel(address);
            if (URL == null || URL.Contains("http") == false)
            {
                output("Error fetching stream for " + cD.ToString(), LogLevel.Error);
                return null;
            }
            lastChannelPlayed = channelnum;
            setRecentlyPlayed();
            return URL;
         }

        protected virtual string playChannel(String address)
        {
            URL url = new URL(address);
            output("Fetch: " + address + " ("+url.getIP()+")", LogLevel.Debug);
            url.setRequestHeader("Cookie", cookies);
            url.fetch();
            output("Server Response: " + url.getStatusDescription(), LogLevel.Debug);
            if (url.isSuccess == false) {
                output("Play Error: " + url.getStatusDescription(), LogLevel.Error);
                return null;
            }

            String data = url.response();
            String contentURL;
            String pattern = "<PARAM NAME=\"FileName\" VALUE=\"(.*?)\">";
            String m = Regex.Match(data,pattern).ToString();
            m = m.Replace("<PARAM NAME=\"FileName\" VALUE=\"", "");
            m = m.Replace("\">", "");

            if (!m.Equals(""))
            {
                contentURL = m.ToString();
                output(contentURL, LogLevel.Debug);
            }
            else 
            {
                if (data.ToLower().Contains("access denied"))
                {
                    output("XM Radio Online Error - Not Logged In", LogLevel.Error);
                    isLoggedIn = false;
                }
                else
                {
                    output("XM Radio Online Error - Unknown Error", LogLevel.Error);
                    output("See playchannel.err for raw data", LogLevel.Debug);
                }
                cache.saveFile("playchannel.err", data);
                contentURL = null;
            }
            return (contentURL);
        }

        //Helper method so we don't have to pass log around everywhere....
        public void output(String output, LogLevel level)
        {
            logSvc.output(output, level);
        }

        public String log
        {
            get
            {
                return logSvc.getLog;
            }
        }

        protected void loadChannelMetadata()
        {
            output("Loading extended channel data...", LogLevel.Info);
            cache.addCacheFile("channelmetadata.cache", "channel metadata", -5);
            if (cache.isCached("channelmetadata.cache"))
            {
                loadChannelMetadata(false);
            }
            else
            {
                dnldChannelMetadata();
                loadedChannelMetadata = true;
            }
        }
        protected void loadChannelMetadata(Boolean fastLoad)
        {
            if (fastLoad == true)
            {
                output("Loading extended channel data... (from cache)", LogLevel.Info);
            }
            //We only load cached data once (so if fastLoad succeeded.. 
            // Normal mode can return early
            if (loadedChannelMetadataCache == true)
            {
                output("Extended channel data is loaded and current, no update needed.", LogLevel.Info);
                if (fastLoad == false)
                {
                    loadedChannelMetadata = true; 
                }
                return;
            } 
            String file = "channelmetadata.cache";
            String data = cache.getFile(file);
            loadedChannelMetadataCache = setChannelMetadata(data);
            if (!loadedChannelMetadataCache)
            {
                if (cache.isInvalidatedFile(file))
                {
                    output("No cache data to load, skipping...", LogLevel.Info);
                }
                else
                {
                    //Force expiration of bad data. (Don't delete it in case something useful is in the file for debugging)
                    cache.invalidateFile(file);
                    output("Failed to load extended channel data (from cache).", LogLevel.Error);
                }
            }
            else
            {
                output("Extended channel data loaded successfully (from cache)", LogLevel.Info);
                if (fastLoad == false)
                {
                    loadedChannelMetadata = true;
                }
                preloadImages();
            }
        }

        private void dnldChannelMetadata()
        {
            output("Downloading extended channel data...", LogLevel.Info);
            String channelMetaURL = "http://www.siriusxm.com/programschedules";
            if (!isLive)
            {
                channelMetaURL = "http://test.xmtuner.net/epg/index.xmc";
            }
            URL channelMetaData = new URL(channelMetaURL);
            output("Fetching: " + channelMetaURL, LogLevel.Debug);
            channelMetaData.setRequestHeader("Cookie", cookies);
            channelMetaData.fetch();

            output("Server Response: " + channelMetaData.getStatusDescription(), LogLevel.Debug);
            if (channelMetaData.isSuccess)
            {
                String rawChannelMetaData = channelMetaData.response();
                loadedChannelMetadata = setChannelMetadata(rawChannelMetaData);

                if (loadedChannelMetadata == true)
                {
                    output("Extended channel data loaded successfully", LogLevel.Info);
                    cache.saveFile("channelmetadata.cache", rawChannelMetaData);

                    //Trigger the image preloader...
                    if (preloadedImages1R == false)
                    {
                        doPreloadImages();
                    }
                }
            }
            else
            {
                output("Error encountered loading extended channel data", LogLevel.Error);
            }
            channelMetaData.close();
        }

        protected virtual Boolean setChannelMetadata(String rawData)
        {
            if (rawData == null)
            {
                return false;
            }
            try
            {
                String baseurl = "http://www.siriusxm.com";

                foreach (String _value in setChannelMetadataHelper(rawData))
                {
                    String[] value = _value.Replace("\"", "").Split(new string[] { "::XMTUNER-SEPERATOR::" }, StringSplitOptions.None);

                    //                   0            1             2               3            4          5           6              7                  8             9           10          11            12           13
                    // rig.addChannel(genrekey, channelnumber, BestOfSirius, xmchannelnumber, BestOfXM, contentid, displayname, shortdescription, mediumdescription, vanityurl, channellogo, progtypekey, genretitle, genresortorder);
                    // BestofSirius and BestofXM args will be either "" or "*"

                    String[] newdata = new String[5];
                    newdata[0] = value[1]; //Num
                    int num;
                    try
                    {
                        if (network.ToUpper().Equals("SIRIUS"))
                        {
                            num = Convert.ToInt32(value[1]);
                        }
                        else
                        {
                            num = Convert.ToInt32(value[3]);
                        }
                    }
                    catch (FormatException) { continue; }

                    //XM Channel Number
                    if (value[3] != null && !value[3].Equals(""))
                    {
                        newdata[1] = value[3];
                    }

                    //Logo (138x50)
                    if (value[10] != null && !value[10].Equals(""))
                    {
                        newdata[2] = baseurl + value[10];
                    }
                    //channelKey
                    if (value[5] != null && !value[5].Equals(""))
                    {
                        newdata[3] = value[5];
                    }

                    //URL for channel
                    if (value[9] != null && !value[9].Equals(""))
                    {
                        newdata[4] = baseurl + value[9];
                    }

                    Find(Convert.ToInt32(num)).addChannelMetadata(newdata);
                }
            }
            catch (IndexOutOfRangeException)
            {
                return false;
            }
            return true;
        }
        protected String[] setChannelMetadataHelper(String rawData)
        {
            try
            {
                rawData = rawData.Replace("// rig.addChannel", "");
                int start = rawData.IndexOf("rig.addChannel") + 15;
                int length = rawData.IndexOf("//ENTRY") - start;
                rawData = rawData.Trim().Substring(start, length);
                rawData = rawData.Replace("    ", "");
                rawData = rawData.Replace(");", "");
                rawData = rawData.Replace("\r\n", "");
                rawData = rawData.Replace("\",", "::XMTUNER-SEPERATOR::");
            }
            catch (Exception)
            {
                return new String[0];
            }

            return rawData.Split(new string[] { "rig.addChannel(" }, StringSplitOptions.None);
        }

        protected void setRecentlyPlayed()
        {
            XMChannel npChannel = Find(lastChannelPlayed);
            if (npChannel.num == 0 || npChannel.song == null || npChannel.song.Equals(""))
            { 
                return;
            }
            String currentTime = DateTime.Now.ToString("T");
            String entry = network + " " + npChannel.num + " - " + npChannel.artist + " - " + npChannel.song;

            if (recentlyPlayed.Count > 0)
            {
                String[] lastEntry = recentlyPlayed.FirstOrDefault().Split(':');
                if (lastEntry[3].Trim() == entry) {
                    return;
                }
            }

            recentlyPlayed.Insert(0, currentTime + ": " + entry);
            if (recentlyPlayed.Count > numItems)
            {
                recentlyPlayed.RemoveRange(numItems, recentlyPlayed.Count - numItems);
            }
        }
        
        protected virtual String getChannelsNums()
        {
            String channels = "";
            foreach (XMChannel chan in getChannels())
            {
                channels += chan.num+",";
            }
            return channels;
        }

        private void loadProgramGuideData()
        {
            output("Loading program guide...", LogLevel.Info);

            String channums = HttpUtility.UrlEncode(getChannelsNums());

            //endDate	11122009030000 (date("dmYHis", time()+86400);)
            String enddate = DateTime.Now.AddDays(1).ToString("ddMMyyyyHHmmss");

            //startDate	11122009000000 (date("dmYHis", time());)
            String startdate = DateTime.Now.ToString("ddMMyyyyHHmmss");

            String programGuideURL = "http://www.xmradio.com/epg.program_schedules.xmc?channelNums=" + channums + "&endDate=" + enddate + "&startDate=" + startdate;
            if (!isLive)
            {
                programGuideURL = "http://test.xmtuner.net/epg/program_schedules.xmc";
            }
            URL programGuideData = new URL(programGuideURL);
            output("Fetching: " + programGuideURL, LogLevel.Debug);
            programGuideData.setRequestHeader("Cookie", cookies);
            programGuideData.fetch();

            output("Server Response: " + programGuideData.getStatusDescription(), LogLevel.Debug);
            if (programGuideData.isSuccess)
            {
                isProgramDataCurrent = setProgramGuideData(programGuideData.response());
            }
            else
            {
                output("Error: " + programGuideData.getStatusDescription(), LogLevel.Error);
            }
            if (isProgramDataCurrent)
            {
                output("Program guide loaded successfully.", LogLevel.Info);
            }
            else
            {
                output("Failed to load program guide data (will retry).", LogLevel.Error);
            }
            programGuideData.close();
        }

        protected Boolean setProgramGuideData(String rawData)
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
                if (PData.Length >= 10)
                {
                    //ChannelNum, Program ID, Program Name, Duration, Start Time, End Time
                    Int32 num = Convert.ToInt32(PData[8]);
                    XMChannel channel = Find(num, true);
                    String[] program = new String[6];
                    program[0] = PData[8];
                    program[1] = PData[0];
                    program[2] = PData[1];
                    program[3] = PData[7];
                    program[4] = PData[3];
                    program[5] = PData[9];

                    channel.addProgram(program);
                }
            }
            return true;
        }

        private String[] getProgram(List<String[]> _programs, Boolean getNext)
        {
            List<String[]> programs = new List<String[]>(_programs.AsReadOnly());
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

        private void doPreloadImages()
        {
            MethodInvoker simpleDelegate = new MethodInvoker(preloadImagesAsync);
            simpleDelegate.BeginInvoke(null, null);
        }

        private void preloadImagesAsync()
        {
            System.Threading.Thread.Sleep(500);
            preloadImages();
        }

        private void preloadImages()
        {
            //Don't run this more than once at a time... really.
            //Don't run this if there's nothing to work from at all...
            if ((loadedChannelMetadata == false && loadedChannelMetadataCache == false) || preloadImageRunning == true) { return; }
            int timeout = preloadImageTimeout;
            int errcnt = 0;
            preloadImageRunning = true;
            preloadedImages = true;
            Boolean result = false;
            output("Image Cache: Populate...", LogLevel.Debug);
            int n = 0;
            try
            {
                List<XMChannel> channelsCopy = new List<XMChannel>(channels.AsReadOnly());
                int i = 0;
                foreach (XMChannel rochan in channelsCopy)
                {
                    XMChannel chan = channels[i];
                    //Only load the image if it needs loading...
                    if (chan.logo_small_image == null)
                    {
                        if (chan.logo_small != null && chan.logo_small.Equals("") == false)
                        {
                            n = chan.num;
                            URL imageURL = new URL(chan.logo_small);
                            imageURL.setTimeout(timeout);
                            imageURL.fetch();
                            if (imageURL.isSuccess)
                            {
                                result = true;
                                output("Image Cache: Added Image for " + chan.ToString(), LogLevel.Debug);
                                chan.logo_small_image = imageURL.responseAsImage();
                                preloadImagesUpdated = true;
                            }
                            else
                            {
                                output("Image Cache: Error adding image for " + chan.ToString(), LogLevel.Notice);
                                preloadedImages = false;
                                if (preloadedImages1R == true)
                                {
                                    errcnt++;
                                    if (errcnt > 3)
                                    {
                                        timeout = timeout + 500;
                                        errcnt = 0;
                                    }
                                }
                            }
                        }
                        else
                        {
                            output("Image Cache: No logo found for " + chan.ToString(), LogLevel.Notice);
                        }
                    }
                    i++;
                }
            }
            catch (InvalidOperationException)
            {
                output("Image Cache: Fatal Error during operation. (" + n + ")", LogLevel.Error);
                preloadedImages = false;
            }
            output("Image Cache: Done.", LogLevel.Debug);
            preloadImageRunning = false;
            preloadedImages1R = true;
            if (result == false)
            {
                preloadedImages = result;
                preloadImageTimeout = timeout;
            }
        }
    }
}
