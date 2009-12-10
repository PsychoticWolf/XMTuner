using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;

namespace XMTuner
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
            TheRequest.Timeout = 30000;
            try
            {
                TheReply = (HttpWebResponse)TheRequest.GetResponse();
            }
            catch (WebException e)
            {
                TheReply = (HttpWebResponse)e.Response;
            }
        }

        public void fetch(String postdata)
        {
            TheRequest.Method = "POST";
            TheRequest.CookieContainer = new CookieContainer();
            byte[] buffer = Encoding.ASCII.GetBytes(postdata);
            TheRequest.ContentType = "application/x-www-form-urlencoded";
            TheRequest.ContentLength = buffer.Length;
            Stream PostData = TheRequest.GetRequestStream();

            PostData.Write(buffer, 0, buffer.Length);
            PostData.Close();

            try
            {
                TheReply = (HttpWebResponse)TheRequest.GetResponse();
            }
            catch (WebException e) {
                TheReply = (HttpWebResponse)e.Response;
            }
        }

        public int getStatus()
        {
            int statusCode = (int) TheReply.StatusCode;
            return statusCode;
        }

        public String response()
        {
            StreamReader Stream = new StreamReader(TheReply.GetResponseStream());
            String result = Stream.ReadToEnd();
            TheReply.Close();
            Stream.Close();
            return result;
        }

        public void close()
        {
            TheReply.Close();
        }

        public CookieCollection getCookies()
        {
            return TheReply.Cookies;
            
        }

        public String[] getHeader(String type)
        {
            return TheReply.Headers.GetValues(type);
        }



    }
}
