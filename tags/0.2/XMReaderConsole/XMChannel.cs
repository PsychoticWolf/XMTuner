using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace XMReaderConsole
{
    class XMChannel : IComparable<XMChannel>, IEquatable<XMChannel>
    {
        public int num;
        public String name;
        public String desc;
        public String category;
        public String artist;
        public String song;
        public String album;

        public XMChannel(String cat, int nu, String na, String d)
        {
            num = nu;
            name = na;
            desc = d;
            category = cat;
        }

        public override String ToString()
        {
            String theString = category + " " + num + " " + name + " " + desc;
            return theString;
        }

        public String ToSimpleString()
        {
            String theString = "[" + num + "] " + name;
            return theString;
        }

        public void addPlayingInfo(String[] stringyInfo)
        {
            artist = stringyInfo[1];
            song = stringyInfo[2];
            album = stringyInfo[3];
        }

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
