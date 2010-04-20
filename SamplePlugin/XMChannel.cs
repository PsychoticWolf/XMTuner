/*
 *  Copyright (c) 2009-2010 PCFire Software
 *  This code is from XMTuner - http://www.xmtuner.net/
 *  All rights reserved.
 * 
 *  XMChannel.cs (r189)
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;


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

        public String channelKey;
        public int xmxref;

        public XMChannel(String cat, int nu, String na, String d, String net, String u) : this(cat, nu, na, d, net) { url = u; }
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

        public String ShortName()
        {
            return network + " " + num;
        }

        public void addPlayingInfo(String[] stringyInfo)
        {
            artist = HttpUtility.HtmlDecode(stringyInfo[1]);
            song = HttpUtility.HtmlDecode(stringyInfo[2]);
            album = HttpUtility.HtmlDecode(stringyInfo[3]);
        }

        public virtual void addChannelMetadata(String[] stringyInfo)
        {
            url = stringyInfo[1];
            logo_small = stringyInfo[2];
            logo = stringyInfo[3];

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
            logo_small = stringyInfo[2];
            logo = stringyInfo[3];
            xmxref = Convert.ToInt32(stringyInfo[0]);

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
