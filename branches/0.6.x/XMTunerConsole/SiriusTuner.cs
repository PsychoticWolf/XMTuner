/*
 * XMTuner: Copyright (C) 2009-2012 Chris Crews and Curtis M. Kularski.
 * 
 * This file is part of XMTuner.

 * XMTuner is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * XMTuner is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with XMTuner.  If not, see <http://www.gnu.org/licenses/>.
 */

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

            //String captchaResponse;
            //String captchaID;
            CookieCollection playerCookies = new CookieCollection();
            //sirius_user_name=***@****.net;
            playerCookies.Add(new Cookie("sirius_user_name", user, "", "www.sirius.com"));
            //sirius_password=********;
            playerCookies.Add(new Cookie("sirius_password", HttpUtility.UrlEncode(getMD5Hash(password)), "", "www.sirius.com"));
            //playerType=sirius;
            playerCookies.Add(new Cookie("playerType", "sirius", "", "www.sirius.com"));
            //sirius_consumer_type=sirius_online_subscriber;
            playerCookies.Add(new Cookie("sirius_consumer_type", "sirius_online_subscriber", "", "www.sirius.com"));
            //sirius_mp_bitrate_entitlement_cookie=high;
            playerCookies.Add(new Cookie("sirius_mp_bitrate_entitlement_cookie", "high", "", "www.sirius.com"));
            //sirius_mp_bitrate_button_status_cookie=high
            playerCookies.Add(new Cookie("sirius_mp_bitrate_button_status_cookie", "high", "", "www.sirius.com"));
            //sirius_mp_playertype=expand;
            playerCookies.Add(new Cookie("sirius_mp_playertype", "expand", "", "www.sirius.com"));
            //sirius_login_type=subscriber;
            playerCookies.Add(new Cookie("sirius_login_type", "subscriber", "", "www.sirius.com"));
            //sirius_login_attempts=0
            playerCookies.Add(new Cookie("sirius_login_attempts", "0", "", "www.sirius.com"));
            setCookies(playerCookies);

            /*Boolean result = getSiriusCaptcha(out captchaResponse, out captchaID, out playerCookies);
            if (result == false)
            {
                output("Error fetching Sirius Captcha for Login", "error");
                return false;
            }*/

            // Do Actual Login
            String SiriusLoginURL = "http://www.sirius.com/player/channel/fwrd.action?pageName=&categoryKey=&genreKey=";
            if (!isLive)
            {
                SiriusLoginURL = "http://test.xmtuner.net/test.php";
            }


            //String data = "userName=" + HttpUtility.UrlEncode(user) + "&password=" + HttpUtility.UrlEncode(getMD5Hash(password)) + "&__checkbox_remember=true&remember=true&captchaEnabled=true&captchaID=" + HttpUtility.UrlEncode(captchaID) + "&timeNow=null&captcha_response=" + captchaResponse;
            URL loginURL = new URL(SiriusLoginURL);
            output("Connecting to: " + SiriusLoginURL + " (" + loginURL.getIP() + ")", "debug");
            loginURL.setRequestHeader("Cookie", cookies);
            loginURL.setCookieContainer(playerCookies);
            loginURL.fetch(); //loginURL.fetch(data);

            output("Server Response: " + loginURL.getStatusDescription(), "debug");

            if (loginURL.isSuccess)
            {
                CookieCollection loginCookies = loginURL.getCookies();
                loginCookies.Add(playerCookies);
                cookies = setCookies(loginCookies);

                output("Number of Cookies: " + cookieCount.ToString(), "debug");

                if (cookieCount > 0)
                {

                    if (cookieCount < 12)
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
                output("Error: " + playerURL.getStatusDescription(), "error");
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
            output("Fetch: " + address + " (" + url.getIP() + ")", "debug");
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

        protected override String getChannelsNums()
        {
            String channels = "";
            foreach (XMChannel chan in getChannels())
            {
                channels += chan.xmxref + ",";
            }
            return channels;
        }

        /*private List<String[]> loadSiriusExtendedChannelDataObj()
        {
            List<String[]> list = new List<string[]>();
            list.Add(new String[] { "1", "siriushits1", "2" });
            list.Add(new String[] { "2", "starlite", "25" });
            list.Add(new String[] { "3", "siriuslove", "23" });
            list.Add(new String[] { "4", "8205", "4" });
            list.Add(new String[] { "5", "siriusgold", "5" });
            list.Add(new String[] { "6", "60svibrations", "6" });
            list.Add(new String[] { "7", "totally70s", "7" });
            list.Add(new String[] { "8", "big80s", "8" });
            list.Add(new String[] { "9", "8206", "9" });
            list.Add(new String[] { "10", "estreetradio", "58" });
            list.Add(new String[] { "11", "bbcradio1", "29" });
            list.Add(new String[] { "12", "thepulse", "26" });
            list.Add(new String[] { "13", "elvisradio", "18" });
            list.Add(new String[] { "14", "classicvinyl", "46" });
            list.Add(new String[] { "15", "classicrewind", "49" });
            list.Add(new String[] { "16", "thevault", "40" });
            list.Add(new String[] { "17", "8731", "56" });
            list.Add(new String[] { "18", "thespectrum", "45" });
            list.Add(new String[] { "19", "buzzsaw", "53" });
            list.Add(new String[] { "20", "octane", "48" });
            list.Add(new String[] { "21", "altnation", "47" });
            list.Add(new String[] { "22", "firstwave", "44" });
            list.Add(new String[] { "23", "hairnation", "41" });
            list.Add(new String[] { "24", "90salternative", "54" });
            list.Add(new String[] { "25", "undergroundgarage", "59" });
            list.Add(new String[] { "26", "leftofcenter", "43" });
            list.Add(new String[] { "27", "hardattack", "42" });
            list.Add(new String[] { "28", "faction", "52" });
            list.Add(new String[] { "29", "8207", "50" });
            list.Add(new String[] { "30", "coffeehouse", "51" });
            list.Add(new String[] { "31", "radiomargaritaville", "55" });
            list.Add(new String[] { "32", "gratefuldead", "57" });
            list.Add(new String[] { "33", "thebridge", "27" });
            list.Add(new String[] { "35", "chill", "84" });
            list.Add(new String[] { "36", "thebeat", "81" });
            list.Add(new String[] { "38", "area33", "38" });
            list.Add(new String[] { "39", "8124 (backspin)", "65" });
            list.Add(new String[] { "40", "hiphopnation", "67" });
            list.Add(new String[] { "45", "shade45", "66" });
            list.Add(new String[] { "50", "hotjamz", "68" });
            list.Add(new String[] { "51", "heartandsoul", "62" });
            list.Add(new String[] { "53", "soultown", "60" });
            list.Add(new String[] { "60", "newcountry", "16" });
            list.Add(new String[] { "61", "primecountry", "17" });
            list.Add(new String[] { "62", "theroadhouse", "10" });
            list.Add(new String[] { "63", "outlawcountry", "12" });
            list.Add(new String[] { "64", "8209", "13" });
            list.Add(new String[] { "65", "bluegrass", "14" });
            list.Add(new String[] { "66", "spirit", "32" });
            list.Add(new String[] { "67", "8210", "34" });
            list.Add(new String[] { "68", "praise", "33" });
            list.Add(new String[] { "71", "jazzcafe", "71" });
            list.Add(new String[] { "72", "purejazz", "70" });
            list.Add(new String[] { "73", "spa73", "72" });
            list.Add(new String[] { "74", "siriusblues", "74" });
            list.Add(new String[] { "75", "siriuslysinatra", "73" });
            list.Add(new String[] { "76", "8215", "28" });
            list.Add(new String[] { "77", "broadwaysbest", "75" });
            list.Add(new String[] { "78", "metropolitanopera", "79" });
            list.Add(new String[] { "79", "siriuspops", "77" });
            list.Add(new String[] { "80", "symphonyhall", "78" });
            list.Add(new String[] { "81", "8036 (the strobe)", "83" });
            list.Add(new String[] { "83", "rumbon", "85" });
            list.Add(new String[] { "84", "reggaerhythms", "86" });
            list.Add(new String[] { "90", "8367", "120" });
            list.Add(new String[] { "91", "espndeportes", "154" });
            list.Add(new String[] { "99", "playboyradio", "99" });
            list.Add(new String[] { "100", "howardstern100", "100" });
            list.Add(new String[] { "101", "howardstern101", "101" });
            list.Add(new String[] { "102", "siriusstars", "155" });
            list.Add(new String[] { "103", "bluecollarcomedy", "148" });
            list.Add(new String[] { "104", "rawdog", "150" });
            list.Add(new String[] { "105", "laughbreak", "151" });
            list.Add(new String[] { "106", "thefoxxhole", "149" });
            list.Add(new String[] { "108", "maximradio", "139" });
            list.Add(new String[] { "109", "siriusoutq", "98" });
            list.Add(new String[] { "110", "indietalk", "130" });
            list.Add(new String[] { "111", "cosmopolitanradio", "162" });
            list.Add(new String[] { "112", "marthastewartlivingradio", "157" });
            list.Add(new String[] { "114", "doctorradio", "119" });
            list.Add(new String[] { "115", "radiodisney", "115" });
            list.Add(new String[] { "116", "8216", "116" });
            list.Add(new String[] { "117", "8212", "163" });
            list.Add(new String[] { "120", "espnradio", "140" });
            list.Add(new String[] { "122", "siriussportsaction", "143" });
            list.Add(new String[] { "123", "8213", "144" });
            list.Add(new String[] { "124", "siriusnflradio", "124" });
            list.Add(new String[] { "129", "cnbc", "127" });
            list.Add(new String[] { "130", "bloombergradio", "129" });
            list.Add(new String[] { "132", "cnn", "122" });
            list.Add(new String[] { "133", "cnnheadlinenews", "123" });
            list.Add(new String[] { "140", "wrn", "135" });
            list.Add(new String[] { "141", "bbcworld", "131" });
            list.Add(new String[] { "144", "siriuspatriot", "166" });
            list.Add(new String[] { "145", "foxnewstalk", "168" });
            list.Add(new String[] { "146", "siriusleft", "137" });
            list.Add(new String[] { "147", "roaddogtrucking", "171" });
            list.Add(new String[] { "159", "thecatholicchannel", "117" });
            list.Add(new String[] { "160", "ewtnglobal", "118" });
            list.Add(new String[] { "161", "8307", "170" });
            list.Add(new String[] { "195", "8182", "156" });
            list.Add(new String[] { "196", "8183", "133" });
            list.Add(new String[] { "197", "thevirus", "202" });
            list.Add(new String[] { "208", "8185", "204" });
            list.Add(new String[] { "209", "8186", "146" });
            list.Add(new String[] { "210", "8187", "175" });
            list.Add(new String[] { "211", "8368", "147" });
            list.Add(new String[] { "801", "npr", "134" });
            list.Add(new String[] { "802", "8225", "91" });
            list.Add(new String[] { "805", "8226", "20" });
            list.Add(new String[] { "806", "8208", "30" });
            list.Add(new String[] { "807", "8228", "64" });
            list.Add(new String[] { "808", "8227", "15" });
            list.Add(new String[] { "809", "8211", "76" });
            list.Add(new String[] { "810", "8229", "39" });
            list.Add(new String[] { "811", "8230", "226" });
            list.Add(new String[] { "812", "8231", "90" });
            list.Add(new String[] { "813", "8232", "92" });
            list.Add(new String[] { "814", "8233", "93" });
            list.Add(new String[] { "815", "8366", "94" });
            list.Add(new String[] { "816", "8235", "138" });
            list.Add(new String[] { "817", "8236", "167" });
            list.Add(new String[] { "818", "8237", "132" });
            list.Add(new String[] { "819", "8239", "136" });
            list.Add(new String[] { "820", "8238", "169" });
            list.Add(new String[] { "824", "8369", "248" });

            return list;

        }*/
    }
}