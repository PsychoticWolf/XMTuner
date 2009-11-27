using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Net;
using System.Xml;
using System.IO;
using System.Security;

namespace XMReaderConsole
{
    class WebListner
    {
        private static System.Threading.AutoResetEvent listenForNextRequest = new System.Threading.AutoResetEvent(false);
        String port = "9955";
        HttpListener theServer = new HttpListener();
        String prefix;
        XMTuner myTuner;
        String serverHost;
        
        public bool isRunning = false;
       

        public WebListner(XMTuner tuner)
        {
            myTuner = tuner;
            prefix = "http://*:" + port + "/";
        }

        public void start()
        {
            theServer.Prefixes.Clear();
            theServer.Prefixes.Add(prefix);
            theServer.Start();
            myTuner.output("Webserver started");

            System.Threading.ThreadPool.QueueUserWorkItem(listen);
            //MethodInvoker simpleDelegate = new MethodInvoker(listen);
            //simpleDelegate.BeginInvoke(null, null);
        }

        public void stop()
        {
            theServer.Close();
            myTuner.output("Webserver stopped");
        }

        private void listen(object state)
        {
            while (theServer.IsListening)
            {
                IAsyncResult result = theServer.BeginGetContext(new AsyncCallback(ListenerCallback), theServer);
                listenForNextRequest.WaitOne();
                //result.AsyncWaitHandle.WaitOne();
            }
        }

        private void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener) result.AsyncState;
            //HttpListenerContext context;

            if (listener == null)
            {
                return;
            }

            HttpListenerContext context = listener.EndGetContext(result);
            listenForNextRequest.Set();

            if (context == null)
            {
                return;
            }

            ProcessRequest(context);
        }

        public void SendRequest(HttpListenerContext context, String responseString, String contentType, bool redirect)
        {
            // Obtain a response object.
            HttpListenerResponse response = context.Response;

            //Set ContentType...
            if (contentType != "")
            {
                response.ContentType = contentType;
            }
            if (redirect)
            {
                response.Redirect(responseString);
            }

            // Construct a response.
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();

        }

        protected void ProcessRequest(HttpListenerContext context)
        {
            //do work
            myTuner.output("Incoming Request...");
            HttpListenerRequest request = context.Request;
            serverHost = request.UserHostName;
            String requestURL = request.Url.PathAndQuery;
            myTuner.output("For: "+requestURL);

            char[] seperator = new char[] { '/' };
            String[] parsedURL = requestURL.Split(seperator, 2, StringSplitOptions.RemoveEmptyEntries);
            int sizeofURL = parsedURL.Length;

            String baseURL;
            String methodURL = "";
            if (sizeofURL == 0)
            {
                baseURL = "/";
            }
            else
            {
                baseURL = parsedURL[0];
                if (sizeofURL > 1)
                {
                    methodURL = parsedURL[1];
                }
            }

            if (baseURL.Equals("streams"))
            {
                //Do Action for Stream
                string responseString = DoStream(methodURL);
                SendRequest(context, responseString, "audio/x-ms-wma", true);
            }
            else if (baseURL.Equals("feeds"))
            {
                //Do Action for Feeds
                NameValueCollection URLParams = request.QueryString;
                string responseString = DoFeed(methodURL,URLParams);
                SendRequest(context, responseString, "text/xml;charset=UTF-8", false);
            }
            else
            {
                string responseString = "<HTML><BODY>Unknown Request</BODY></HTML>";
                SendRequest(context, responseString, "", false);
            }
        }

        public string DoStream(string methodURL)
        {
            String response = "";

            String[] args = methodURL.Split('/');
            if (args.Length > 0)
            {
                int ChanNum = Convert.ToInt32(args[0]);
                string bitrate;
                if (args.Length > 1)
                {
                    bitrate = args[1];
                }
                else
                {
                    //Fetch default bitrate from config!
                    bitrate = myTuner.bitrate;
                }

                string channelURL = myTuner.play(ChanNum, bitrate);
                return channelURL;
            }

            return response;
        }

        public string DoFeed(string methodURL, NameValueCollection URLparams)
        {
            String bitrate = myTuner.bitrate;
            bitrate = URLparams["bitrate"];
            String bitrate_desc ="";
            if (bitrate.Equals("high"))
            {
                bitrate_desc = "128 Kbps";
            } else if (bitrate.Equals("low"))
            {
                bitrate_desc = "32 Kbps";
            }
            String link = "http://"+serverHost+"/feeds/?bandwidth="+bitrate;

            List <XMChannel> list = myTuner.getChannels();
            list.Sort();
            list.Reverse();

            StringWriter feedWriter = new StringWriter();
            //Create a writer to write XML to the console.
            XmlTextWriter writer = new XmlTextWriter(feedWriter);

            //Use indentation for readability.
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;

            writer.WriteStartDocument();
            //writer.WriteString("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");

            //Write an element (this one is the root).
            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteAttributeString("xmlns:media", "http://search.yahoo.com/mrss/");
            writer.WriteAttributeString("xmlns:upnp", "urn:schemas-upnp-org:metadata-1-0/upnp/");
            
            writer.WriteStartElement("channel");

            //Write the title element.
            writer.WriteStartElement("title");
            writer.WriteString("XM Channels ("+bitrate_desc+")");
            writer.WriteEndElement();

            writer.WriteStartElement("link");
            writer.WriteString(link);
            writer.WriteEndElement();

            writer.WriteStartElement("guid");
            writer.WriteString(link);
            writer.WriteEndElement();

            writer.WriteStartElement("description");
            writer.WriteString("English");
            writer.WriteEndElement();

            writer.WriteStartElement("language");
            writer.WriteString("en-us");
            writer.WriteEndElement();


            

            //feed = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
            //feed += "<rss version=\"2.0\" xmlns:media=\"http://search.yahoo.com/mrss/\" xmlns:upnp=\"urn:schemas-upnp-org:metadata-1-0/upnp/\">\n";
            //feed += "  <channel>\n";
            //feed += "    <title>XM Channels (" + bitrate_desc + ")</title>\n";
            //feed += "    <link>" + link + "</link>\n";
            //feed += "    <description>English</description>\n";
            //feed += "    <language>en-us</language>\n";

            //Channels
            foreach (XMChannel chan in list)
            {
                string media = "http://"+serverHost+"/streams/"+chan.num+"/"+bitrate;
                writer.WriteStartElement("item");

                //feed += "    <item>\n";
                writer.WriteStartElement("title");
                writer.WriteString("XM " + chan.num + " - " + chan.name);
                writer.WriteEndElement();

                //feed += "      <title>XM " + chan.num + " - " + chan.name + "</title>\n";

                writer.WriteStartElement("link");
                writer.WriteString(media);
                writer.WriteEndElement();

                //feed += "      <link>"+media+"</link>\n";

                writer.WriteStartElement("link");
                writer.WriteString(chan.desc);
                writer.WriteEndElement();
                //feed += "      <description>" + chan.desc + "</description>\n";

                writer.WriteStartElement("media:content");
                writer.WriteAttributeString("url",media);
                writer.WriteAttributeString("type","audio/x-ms-wma");
                writer.WriteAttributeString("medium","audio");
                writer.WriteEndElement();

                //feed += "      <media:content url=\""+media+"\" type=\"audio/x-ms-wma\" medium=\"audio\" />\n";

                writer.WriteStartElement("language");
                writer.WriteString("en-us");
                writer.WriteEndElement();

                //feed += "      <language>en-us</language>\n";

                writer.WriteStartElement("upnp:region");
                writer.WriteString("United States");
                writer.WriteEndElement();

                //feed += "      <upnp:region>United States</upnp:region>\n";
                //feed += "    </item>\n";
                writer.WriteEndElement();
            }

            //feedWriter.Write("<test>value</test>");

            //Close channel
            writer.WriteEndElement();

            //Write the close tag for the root element.
            writer.WriteEndElement();


            //Write the XML to file and close the writer.
            writer.Close();

            String feed = feedWriter.ToString();


            //feed += "  </channel>";
            //feed += "</rss>";

            return feed;
        }
    }
}
