﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Linq;


namespace XMTuner
{
    class XMChannel : IComparable<XMChannel>, IEquatable<XMChannel>
    {
        protected String network = "??";

        public int num;
        public String name;
        public String desc;
        public String category;
        public String artist;
        public String song;
        public String album;
        public String url;
        public String logo;
        public String logo_small;
        public System.Drawing.Image logo_small_image = null;
        public List<String[]> programData = new List<string[]>();
        public List<String> history = new List<String>();

        public String channelKey;
        public int xmxref;

        public XMChannel(String cat, int nu, String na, String d, String net) : this(cat, nu, na, d) { network = net; }
        public XMChannel(String cat, int nu, String na, String d)
        {
            num = nu;
            name = na.Trim();
            desc = d;
            category = cat;
        }

        public override String ToString()
        {
            String theString = network + " " + num + " - " + name;
            return theString;
        }

        public String ShortName
        {
            get
            {
                return network + " " + num;
            }
        }

        public void addPlayingInfo(String[] stringyInfo)
        {
            artist = HttpUtility.HtmlDecode(stringyInfo[1]);
            song = HttpUtility.HtmlDecode(stringyInfo[2]);
            album = HttpUtility.HtmlDecode(stringyInfo[3]);

            addSongToHistory();
        }

        private void addSongToHistory()
        {
            int numItems = 25;
            String currentTime = DateTime.Now.ToString("T");
            String entry = network + " " + num + " - " + artist + " - " + song;

            if (history.Count > 0)
            {
                String[] lastEntry = history.FirstOrDefault().Split(':');
                if (lastEntry[3].Trim() == entry)
                {
                    return;
                }
            }

            history.Insert(0, currentTime + ": " + entry);
            if (history.Count > numItems)
            {
                history.RemoveRange(numItems, history.Count - numItems);
            }
        }

        public virtual void addChannelMetadata(String[] stringyInfo)
        {
            if (stringyInfo[1] != null)
            {
                url = stringyInfo[1];
            }
            if (stringyInfo[2] != null)
            {
                logo_small = stringyInfo[2];
            }
            if (stringyInfo[3] != null)
            {
                logo = stringyInfo[3];
            }
        }

        public void addProgram(String[] program)
        {
            programData.Add(program);
        }

        public void clearProgram()
        {
            programData.Clear();
        }

        #region Sirius Specific Methods
        public void addChannelMetadataS(String[] stringyInfo)
        {
            if (stringyInfo[0] != null)
            {
                xmxref = Convert.ToInt32(stringyInfo[0]);
            }
            if (stringyInfo[2] != null)
            {
                logo_small = stringyInfo[2];
            }
            if (stringyInfo[3] != null)
            {
                logo = stringyInfo[3];
            }

        }

        public void addChannelData(String[] details)
        {
            url = details[0];
            channelKey = details[1];
        }
        #endregion


        #region IComparable<XMChannel> Members

        public int CompareTo(XMChannel other)
        {
            return other.num.CompareTo(this.num);
        }

        #endregion

        #region IEquatable<XMChannel> Members

        public bool Equals(XMChannel other)
        {
            return other.num.Equals(this.num);
        }

        #endregion
    }
}
