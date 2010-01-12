using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Drawing;

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

            try
            {
                Stream PostData = TheRequest.GetRequestStream();
                PostData.Write(buffer, 0, buffer.Length);
                PostData.Close();
                TheReply = (HttpWebResponse)TheRequest.GetResponse();
            }
            catch (WebException e) {
                TheReply = (HttpWebResponse)e.Response;
            }
        }

        public int getStatus()
        {
            if (TheReply == null)
            {
                return 0;
            }
            int statusCode = (int) TheReply.StatusCode;
            return statusCode;
        }

        public String response()
        {
            if (TheReply == null)
            {
                return "";
            }
            StreamReader Stream = new StreamReader(TheReply.GetResponseStream());
            String result = Stream.ReadToEnd();
            TheReply.Close();
            Stream.Close();
            return result;
        }

        /* The placement of this method is a bit shameless (since it returns an Image 
         * and not a more generic Stream, but its for our protection.
         * As I want to make sure we're closing streams reliably. */
        public Image responseAsImage()
        {
            if (TheReply == null)
            {
                return null;
            }
            Stream stream = TheReply.GetResponseStream();
            Image image = Image.FromStream(stream);
            TheReply.Close();
            stream.Close();
            return image;
        }

        public void close()
        {
            if (TheReply != null)
            {
                TheReply.Close();
            }
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
