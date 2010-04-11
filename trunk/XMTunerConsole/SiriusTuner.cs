using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Xml;
using System.Security.Cryptography;
using System.Text;


namespace XMTuner
{
    class SiriusTuner : XMTuner
    {
        protected override String baseurl
        {
            get
            {
                return "http://www.sirius.com";
            }
        }


        public SiriusTuner(String username, String passw, Log logging)
            : base(username, passw, logging, "SIRIUS")
        {
        }

        protected override Boolean login()
        {
            Boolean loginResult = true;
            output("Logging into Sirius Internet Radio", "info");

            String captchaResponse;
            String captchaID;
            CookieCollection playerCookies;

            getSiriusCaptcha(out captchaResponse, out captchaID, out playerCookies);

            // Do Actual Login
            String SiriusLoginURL = "http://www.sirius.com/player/login/siriuslogin.action";
            if (!isLive)
            {
                SiriusLoginURL = "http://users.pcfire.net/~wolf/XMReader/test.php";
            }

            output("Connecting to: " + SiriusLoginURL, "debug");
            String data = "userName=" + HttpUtility.UrlEncode(user) + "&password=" + HttpUtility.UrlEncode(getMD5Hash(password)) + "&__checkbox_remember=true&remember=true&captchaEnabled=true&captchaID=" + HttpUtility.UrlEncode(captchaID) + "&timeNow=null&captcha_response=" + captchaResponse;
            URL loginURL = new URL(SiriusLoginURL);
            loginURL.setRequestHeader("Cookie", cookies);
            loginURL.setCookieContainer(playerCookies);
            loginURL.fetch(data);

            output("Server Response: " + loginURL.getStatusDescription(), "debug");

            if (loginURL.isSuccess)
            {
                CookieCollection loginCookies = loginURL.getCookies();
                loginCookies.Add(playerCookies);
                cookies = setCookies(loginCookies);

                output("Number of Cookies: " + cookieCount.ToString(), "debug");

                if (cookieCount > 0)
                {

                    if (cookieCount <= 12)
                    {
                        output("Login failed: Bad Username or Password", "error");
                        loginResult = false;
                    }
                    else
                    {
                        output("Logged in as " + user, "info");
                        /* For the purposes of quickLogin, we want to just do the actual login step
                           and let the normal data rebuilding occur incrementally on its own.*/
                        if (firstLogin == true)
                        {
                            isLoggedIn = true;
                            return true;
                        }
                        Boolean cd = loadChannelData();
                        Boolean ecd = false;
                        if (cd)
                        {
                            ecd = loadSiriusChannelGuide();
                        }
                        if (cd && ecd)
                        {
                            //We're logged in and have valid channel information, set login flag to true
                            isLoggedIn = true;
                            lastLoggedIn = DateTime.Now;

                            //Attempt to preload channel metadata
                            loadChannelMetadata(true);

                            //Continue to preloading whatsOn data
                            doWhatsOn(true);

                        }
                        else
                        {
                            //If we don't have [complete] chanData, consider ourselves not-logged-in
                            isLoggedIn = false;
                            output("Login failed: Unable to retrieve channel data.", "error");
                            loginResult = false;
                        }

                    }

                }
            }
            else
            {
                output("Login Failed: " + loginURL.getStatusDescription(), "error");
                loginResult = false;
            }
            loginURL.close();
            return loginResult;
        }

        private Boolean getSiriusCaptcha(out String captchaResponse, out String captchaID, out CookieCollection playerCookies)
        {
            //Prefetch
            String SiriusPlayerURL = "http://www.sirius.com/player/home/siriushome.action";
            URL playerURL = new URL(SiriusPlayerURL);
            output("Connecting to: " + SiriusPlayerURL, "debug");
            playerURL.setCookieContainer();
            playerURL.fetch();
            output("Server Response: " + playerURL.getStatusDescription(), "debug");
            if (playerURL.isSuccess == false)
            {
                captchaResponse = null;
                captchaID = null;
                playerCookies = null;
                return false;
            }
            
            //Continue, we have a page to work with...

            playerCookies = playerURL.getCookies();
            //Add JS controlled required cookies
            playerCookies.Add(new Cookie("sirius_consumer_type", "sirius_online_subscriber", "", "www.sirius.com"));
            playerCookies.Add(new Cookie("sirius_login_type", "subscriber", "", "www.sirius.com"));
            cookies = setCookies(playerCookies);

            output("Number of Cookies: " + cookieCount.ToString(), "debug");

            String data = playerURL.response();

            int start;
            try
            {
                start = data.IndexOf("<!-- CAPTCHA:BEGIN -->");
                int end = data.IndexOf("<!-- CAPTCHA:END -->");
                if (start == -1 || end == -1)
                {
                    data = "";
                }
                else
                {
                    data = data.Substring(start, end - start);
                }
                start = data.IndexOf("/mp/captcha/image/");
                String _captchaNum = data.Substring(start, 25);
                String[] _captchaNumA = _captchaNum.Split('_');
                int captchaNum = Convert.ToInt32(_captchaNumA[1]);

                captchaResponse = getCaptchaResponse(captchaNum);

                start = data.IndexOf("name=\"captchaID\"");
                captchaID = data.Trim().Substring(start, 35);
                String[] _captchaID = captchaID.Split(new String[] { "value=\"" }, StringSplitOptions.None);
                captchaID = _captchaID[1].Trim().Replace("\">", "");
            }
            catch (ArgumentOutOfRangeException)
            {
                output("Failed to get Sirius captcha.", "error");
                captchaID = null;
                captchaResponse = null;
                return false;
            }
            return true;
        }
        

        private String getMD5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }


        private Boolean loadSiriusChannelGuide()
        {
            Boolean fromCache = false;
            output("Loading Sirius Extended Channel Data...", "info");
            String data;
            String file = "channellineupsirius.cache";
            cache.addCacheFile(file, "sirius channel metadata", -1);
            if (cache.isCached(file))
            {
                data = cache.getFile(file);
                fromCache = true;
            }
            else
            {
                String URL = "http://www.sirius.com/servlet/ContentServer?pagename=Sirius/XML/ChannelGuideXML&c=ChannelLineup&cid=1218563499691&pid=SIR_AUD_EVT_SXM&catid=all"; //&pid=SIR_IP_EVT&catid=all";
                URL channelGuideURL = new URL(URL);
                channelGuideURL.fetch();
                if (channelGuideURL.isSuccess)
                {
                    data = channelGuideURL.response().Trim();
                }
                else
                {
                    return false;
                }
            }
            XmlDocument xmldoc = new XmlDocument();
            try
            {
                xmldoc.LoadXml(data);
            }
            catch (XmlException)
            {
                output("Failed to load Sirius Extended Channel Data...", "error");
                return false;
            }
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

            if (fromCache == false)
            {
                output("Sirius Extended Channel Data loaded successfully...", "info");
                cache.saveFile("channellineupsirius.cache", data);
            }
            else
            {
                output("Sirius Extended Channel Data loaded successfully... (from cache)", "info");
            }
            return true;
        }

        public string getCaptchaResponse(int captchaNum)
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

        protected XMChannel Find(String channame)
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

        public override string play(int channelnum, String speed)
        {
            return play(channelnum, speed, true);
        }


        protected override string playChannel(String address)
        {
            URL url = new URL(address);
            url.setRequestHeader("Cookie", cookies);
            url.fetch();
            output("Server Response: " + url.getStatusDescription(), "debug");
            if (url.isSuccess == false)
            {
                output("Play Error: " + url.getStatusDescription(), "error");
                return null;
            }

            String contentURL = null;
            String pattern = "<PARAM (NAME|name)=\"FileName\" (VALUE|value)=\"(.*?)\">";
            String data = url.response();
            String m = Regex.Match(data, pattern).ToString();
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
                if (data.ToLower().Contains("access denied"))
                {
                    output("SIRIUS Internet Radio Error - Not Logged In", "error");
                    isLoggedIn = false;
                }
                else
                {
                    output("SIRIUS Internet Radio Error - Unknown Error", "error");
                    output("See playchannel.err for raw data", "debug");
                }
                cache.saveFile("playchannel.err", data);
                contentURL = null;
            }
            else
            {
                output(contentURL, "debug");
            }

            return (contentURL);
        }

        private String playSiriusChannel(String url)
        {
            url = baseurl + url;
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

        protected override Boolean setChannelMetadata(String rawData)
        {
            foreach (String _value in setChannelMetadataHelper(rawData))
            {
                String[] value = _value.Replace("\\", "").Split(new string[] { "\"," }, StringSplitOptions.None);

                String[] newdata = new String[4];
                String name = value[0].Replace("\"", "");
                newdata[0] = value[2].Replace("\"", ""); //XM's Num
                newdata[2] = baseurl + value[6].Replace("\"", ""); // Logo (45x40)
                newdata[3] = baseurl + value[10].Replace("\"", ""); // Logo (138x50)

                /* Because Sirius/XM have to be unique, this channel doesn't have the same name and won't
                 *  be correctly cross-referenced without being special-cased, for now. */
                if (name.ToUpper().Equals("NPR NOW"))
                {
                    name = "NPR";
                }

                Find(name.ToUpper()).addChannelMetadataS(newdata);
            }
            return true;
        }

        protected override String getChannelsNums()
        {
            String channels = "";
            foreach (XMChannel chan in getChannels())
            {
                channels += chan.xmxref + ",";
            }
            return channels;
        }
    }
}