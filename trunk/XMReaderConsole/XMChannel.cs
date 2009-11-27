using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace XMReaderConsole
{
    class XMChannel : IComparable<XMChannel>
    {
        public int num;
        public String name;
        public String desc;
        public String category;

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

        #region IComparable<XMChannel> Members

        public int CompareTo(XMChannel other)
        {
            return other.num.CompareTo(this.num);
        }

        #endregion
    }
}
