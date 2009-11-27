using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Windows.Forms;

namespace XMReaderConsole
{
    class URL
    {
        Uri Loc;
        //public String postdata;
        HttpWebRequest TheRequest;
        HttpWebResponse TheReply;

        public URL(String location)
        {
            Loc = new Uri(location);
            TheRequest = (HttpWebRequest) HttpWebRequest.Create(Loc);
            TheRequest.UserAgent = "Mozilla/5.0 (compatible;)";

        }

        public URL(Uri location)
        {
            Loc = location;
            TheRequest = (HttpWebRequest)HttpWebRequest.Create(Loc);
            TheRequest.UserAgent = "Mozilla/5.0 (compatible;)";

        }

        public void setRequestHeader(String headername, String headervalue)
        {
            String header = headername+":"+headervalue;
            TheRequest.Headers.Add(header);
        }

        public void fetch()
        {
            TheReply = (HttpWebResponse) TheRequest.GetResponse();
        }

        public void fetch(String postdata)
        {
            TheRequest.Method = "POST";
            byte[] buffer = Encoding.ASCII.GetBytes(postdata);
            TheRequest.ContentType = "application/x-www-form-urlencoded";
            TheRequest.ContentLength = buffer.Length;
            Stream PostData = TheRequest.GetRequestStream();

            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();

            TheReply = (HttpWebResponse)TheRequest.GetResponse();
        }

        public int response()
        {
            int statusCode = (int) TheReply.StatusCode;
            return statusCode;
        }

        public String result()
        {
            StreamReader Stream = new StreamReader(TheReply.GetResponseStream());
            String result = Stream.ReadToEnd();
            TheReply.Close();
            Stream.Close();
            return result;
        }

        public CookieCollection getCookies()
        {
            //MessageBox.Show(TheReply.Cookies.Count.ToString());
            return TheReply.Cookies;
            
        }

        public String[] getHeader(String type)
        {
            return TheReply.Headers.GetValues(type);
        }



    }
}
