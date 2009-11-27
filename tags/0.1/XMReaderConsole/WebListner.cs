using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Xml;
using System.IO;
using System.Text;

namespace XMReaderConsole
{
    class WebListner
    {
        private static System.Threading.AutoResetEvent listenForNextRequest = new System.Threading.AutoResetEvent(false);
        String port;
        HttpListener theServer = new HttpListener();
        String prefix;
        XMTuner myTuner;
        
        String serverHost;
        
        public bool isRunning = false;


        public WebListner(XMTuner tuner, String newport)
        {
            if (Convert.ToInt32(newport) < 2)
            {
                port = "9955";
            }
            else
            {
                port = newport;
            }

            myTuner = tuner;
            prefix = "http://*:" + port + "/";
        }

        public void start()
        {
            myTuner.output("Starting server...", "info");
            theServer.Prefixes.Clear();
            theServer.Prefixes.Add(prefix);
            theServer.Start();
            myTuner.output("Server started", "info");
            myTuner.output("Listening on port " + port, "info");

            System.Threading.ThreadPool.QueueUserWorkItem(listen);
        }

        public void stop()
        {
            theServer.Close();
            myTuner.output("Server stopped", "info");
        }

        private void listen(object state)
        {
            while (theServer.IsListening)
            {
                IAsyncResult result = theServer.BeginGetContext(new AsyncCallback(ListenerCallback), theServer);
                listenForNextRequest.WaitOne();
            }
        }

        private void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener) result.AsyncState;

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

        protected void ProcessRequest(HttpListenerContext context)
        {
            //do work
            HttpListenerRequest request = context.Request;
            serverHost = request.UserHostName;
            String requestURL = request.Url.PathAndQuery;
            myTuner.output("Incoming Request: (Source: " + request.RemoteEndPoint + ") " + request.HttpMethod + " - " + requestURL, "debug");

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
                // Parse URL
                String[] streamParams = parseStreamURL(methodURL);

                //Validate Channel
                string chanName = myTuner.checkChannel(Convert.ToInt32(streamParams[0]));
                if (!chanName.Equals(""))
                {
                    myTuner.output("Incoming Stream Request for XM" + streamParams[0] + " - " + chanName + "", "info");
                    //Do Action for Stream
                    string responseString = DoStream(streamParams);
                    if (responseString == null)
                    {
                        String errorMsg = "<html><body><h1>Service Unavailable</h1> "+
                                          "<p>Stream Error: Unable to fetch XM stream URL. (XMRO Not Logged In or Down)</p>" +
                                          "</body></html>";
                        SendRequest(context, null, errorMsg, "text/html", false, HttpStatusCode.ServiceUnavailable);
                    }
                    else
                    {
                        SendRequest(context, null, responseString, "audio/x-ms-wma", true, HttpStatusCode.OK);
                    }
                }
                else
                {
                    myTuner.output("Incoming Stream Request for Unknown XM Channel", "error");
                    SendRequest(context, null, "Invalid Channel Stream Request", "text/plain", false, HttpStatusCode.OK);
                }
            }
            else if (baseURL.Equals("feeds"))
            {
                //Do Action for Feeds
                NameValueCollection URLParams = request.QueryString;
                Boolean useMMS;
                if (request.UserAgent.Contains("TVersity"))
                {
                    useMMS = true;
                }
                else
                {
                    useMMS = false;
                }

                MemoryStream stream = DoFeed(methodURL, URLParams, useMMS);
                SendRequest(context, stream, null, "text/xml;charset=UTF-8", false, HttpStatusCode.OK);
            }
            else
            {
                string responseString = "<HTML><BODY>Unknown Request</BODY></HTML>";
                SendRequest(context, null, responseString, "", false, HttpStatusCode.OK);
            }
            myTuner.output("Incoming Request Completed", "debug");
        }

        private void SendRequest(HttpListenerContext context, MemoryStream input, String responseString,
            String contentType, bool redirect, HttpStatusCode status)
        {
            //Process data we're given and write to the outputstream for the client.
            // We accept either an memorystream or a responseString, not both.

            // Obtain a response object (and get its outputstream).
            HttpListenerResponse response = context.Response;
            Stream output = response.OutputStream;

            //Try to fetch the Range header...
            String range = context.Request.Headers.Get("Range");
            Int32 maxrange = -1;
            if (range != null)
            {
                String[] rangeGrp = range.Split('-');
                maxrange = Convert.ToInt32(rangeGrp[1]);
            }

            response.StatusCode = (int) status;

            //Set ContentType... (if provided)
            if (contentType != "")
            {
                response.ContentEncoding = System.Text.Encoding.UTF8;
                response.ContentType = contentType;
            }

            //If redirect is true, responseString is the destination to redirect to.
            // Do the redirect.
            if (redirect)
            {
                response.Redirect(responseString);
            }

            // Construct a response. (for the build from responseString case)
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("");
            if (responseString != null)
            {
                buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            }

            //Write to the output buffer and send it to the client..
            // Hopefully not crashing because the client ran away in the meantime
            try
            {
                if (responseString != null)
                {
                    response.ContentLength64 = buffer.Length;
                    // Get a response stream and write the response to it.
                    output.Write(buffer, 0, buffer.Length);
                }
                else if (input != null)
                {
                    if (maxrange < input.Length && maxrange != -1)
                    {

                    }
                    else
                    {
                        response.ContentLength64 = input.Length;
                        input.WriteTo(output);
                    }
                }
                // You must close the output stream.
                output.Close();

            }
            catch (HttpListenerException e)
            {
                response.Abort();
                myTuner.output("Error - Request Aborted Abnormally (" + e.Message + ")", "debug");
            }


        }

        public String[] parseStreamURL(string methodURL)
        {
            String[] args = methodURL.Split('/');
            if (args.Length > 0)
            {
                //int ChanNum = Convert.ToInt32(args[0]);
                String ChanNum = args[0];
                String bitrate = "";
                if (args.Length > 1)
                {
                    bitrate = args[1];
                }
                //return ChanNum, bitrate;
                String[] streamParams = new String[2];
                streamParams[0] = ChanNum;
                streamParams[1] = bitrate;

                return streamParams;
            }
            return null;

        }

        public string DoStream(string[] streamParams)
        {
            int ChanNum = Convert.ToInt32(streamParams[0]);
            string bitrate;
            if (streamParams[1].Length > 1)
            {
                bitrate = streamParams[1];
            }
            else
            {
                //Fetch default bitrate from config!
                bitrate = myTuner.bitrate;
            }

            string channelURL = myTuner.play(ChanNum, bitrate);
            return channelURL;
        }

        public MemoryStream DoFeed(string methodURL, NameValueCollection URLparams, bool UseMMS)
        {
            String bitrate = myTuner.bitrate;
            if (URLparams.Get("bitrate") != null) 
            {
                if (URLparams["bitrate"].ToLower().Equals("high") ||
                    URLparams["bitrate"].ToLower().Equals("low"))
                {
                    bitrate = URLparams["bitrate"].ToLower();
                }
            }
            String bitrate_desc ="";
            if (bitrate.Equals("high"))
            {
                bitrate_desc = "128 Kbps";
            } else if (bitrate.Equals("low"))
            {
                bitrate_desc = "32 Kbps";
            }

            myTuner.output("Incoming Feed Request: XM Channels (All - "+bitrate_desc+")", "info");

            String link = "http://"+serverHost+"/feeds/?bandwidth="+bitrate;

            List <XMChannel> list = myTuner.getChannels();
            XMLWorker myLittleWorker = new XMLWorker();
            MemoryStream OutputStream = myLittleWorker.CreateXMFeed(list, bitrate, serverHost, UseMMS);

            return OutputStream;
        }
    }
}
