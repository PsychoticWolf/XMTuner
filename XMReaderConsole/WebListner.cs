﻿using System;
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
        WebWorker worker;
        public bool isRunning = false;

        public WebListner(XMTuner tuner, String newport)
        {
            if (Convert.ToInt32(newport) < 2)
            {
                port = "19081";
            }
            else
            {
                port = newport;
            }

            myTuner = tuner;
            worker = new WebWorker(myTuner);
            prefix = "http://*:" + port + "/";
        }

        public void start()
        {
            myTuner.output("Starting server...", "info");
            theServer.Prefixes.Clear();
            theServer.Prefixes.Add(prefix);
            try
            {
                theServer.Start();
                isRunning = true;
                myTuner.output("Server started", "info");
                myTuner.output("Listening on port " + port, "info");
            }
            catch (HttpListenerException e)
            {
                isRunning = false;
                myTuner.output("Server failed to start (Port already in use?)", "error");
                myTuner.output("Check your settings or close the other application using the port", "error");
                myTuner.output("Error " + e.ErrorCode + ": " + e.Message, "debug");
                return;
            }
            System.Threading.ThreadPool.QueueUserWorkItem(listen);
        }

        public void stop()
        {
            theServer.Close();
            isRunning = false;
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

            HttpListenerContext context = null;
            try
            {
                context = listener.EndGetContext(result);
                listenForNextRequest.Set();
            }
            catch (ObjectDisposedException)
            {
                listenForNextRequest.Set();
                return;
            }

            if (context == null)
            {
                return;
            }

            ProcessRequest(context);
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

            response.StatusCode = (int)status;

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

        protected void ProcessRequest(HttpListenerContext context)
        {
            //do work
            HttpListenerRequest request = context.Request;
            serverHost = request.UserHostName;
            String requestURL = request.Url.PathAndQuery;
            myTuner.output("Incoming Request: (Source: " + request.RemoteEndPoint + ") " + request.HttpMethod + " - " + requestURL, "debug");

            char[] seperator = new char[] {'/'};
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
                if (baseURL.StartsWith("?"))
                {
                    methodURL = baseURL;
                    baseURL = "/";
                }

                if (sizeofURL > 1)
                {
                    methodURL = parsedURL[1];
                }
            }

            if (baseURL.Equals("/"))
            {
                String responseString;
                NameValueCollection URLParams = request.QueryString;
                myTuner.output("Incoming 'What's On' Request", "info");
                responseString = worker.DoNowPlaying(serverHost, URLParams);
                SendRequest(context, null, responseString, "text/html", false, HttpStatusCode.OK);

            }
            else  if (baseURL.Equals("streams"))
            {
                String response;
                String contentType = "audio/x-ms-wma";
                HttpStatusCode status = HttpStatusCode.OK;
                Boolean redirect = false;

                // Parse Stream URL
                NameValueCollection streamParams = worker.parseStreamURL(methodURL);

                //Validate Channel
                String chanName = myTuner.checkChannel(Convert.ToInt32(streamParams[0]));
                if (!chanName.Equals(""))
                {
                    myTuner.output("Incoming Stream Request for XM" + streamParams[0] + " - " + chanName + "", "info");
                    //Do Action for Stream
                    NameValueCollection streamCollection = worker.DoStream(streamParams, request.RawUrl, serverHost);
                    response = streamCollection["msg"];
                    if (streamCollection["isErr"].Equals("true"))
                    {
                        contentType = "text/html";
                        status = HttpStatusCode.ServiceUnavailable;
                    }
                    else
                    {
                        redirect = true;
                    }
                }
                else
                {
                    myTuner.output("Incoming Stream Request for Unknown XM Channel", "error");
                    response = "Invalid Channel Stream Request";
                    contentType = "text/plain";
                }

                SendRequest(context, null, response, contentType, redirect, status);
            }
            else if (baseURL.Equals("feeds"))
            {
                //Redirect legacy lineup.xml URL from uXM
                if (methodURL.StartsWith("lineup.xml"))
                {
                    String destinationurl = "/" + baseURL + "/";
                    SendRequest(context, null, destinationurl, "text/plain", true, HttpStatusCode.Found);
                    return;
                }
                //Do Action for Feeds
                NameValueCollection URLParams = request.QueryString;
                MemoryStream stream = worker.DoFeed(methodURL, URLParams, request.UserAgent, serverHost);
                SendRequest(context, stream, null, "text/xml;charset=UTF-8", false, HttpStatusCode.OK);
            }
            else
            {
                string responseString = "<HTML><BODY>Unknown Request</BODY></HTML>";
                SendRequest(context, null, responseString, "", false, HttpStatusCode.BadRequest);
            }
            myTuner.output("Incoming Request Completed", "debug");
        }

    }
}