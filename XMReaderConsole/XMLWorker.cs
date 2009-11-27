using System;
using System.Collections.Generic;
using System.Xml;
using System.Text;
using System.IO;

namespace XMReaderConsole
{
    class XMLWorker
    {
        public MemoryStream CreateXMFeed(List<XMChannel> list, String bitrate, String serverHost, Boolean UseMMS)
        {
            String link = "http://" + serverHost + "/feeds/?bandwidth=" + bitrate;

            String bitrate_desc = "";
            if (bitrate.Equals("high"))
            {
                bitrate_desc = "128 Kbps";
            }
            else if (bitrate.Equals("low"))
            {
                bitrate_desc = "32 Kbps";
            }

            MemoryStream MemoryStream = new MemoryStream();

            //Create a writer to write XML
            XmlTextWriter writer = new XmlTextWriter(MemoryStream, System.Text.Encoding.UTF8);

            //Use indentation for readability.
            writer.Formatting = Formatting.Indented;
            writer.Indentation = 4;

            //feed = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
            writer.WriteStartDocument();

            //Write an element (this one is the root).
            //feed += "<rss version=\"2.0\" xmlns:media=\"http://search.yahoo.com/mrss/\" xmlns:upnp=\"urn:schemas-upnp-org:metadata-1-0/upnp/\">\n";

            writer.WriteStartElement("rss");
            writer.WriteAttributeString("version", "2.0");
            writer.WriteAttributeString("xmlns:media", "http://search.yahoo.com/mrss/");
            writer.WriteAttributeString("xmlns:atom", "http://www.w3.org/2005/Atom");
            writer.WriteAttributeString("xmlns:upnp", "urn:schemas-upnp-org:metadata-1-0/upnp/");

            //<channel>
            writer.WriteStartElement("channel");

            //Write the title element.
            //<title>XM Channels (" + bitrate_desc + ")</title>
            writer.WriteStartElement("title");
            writer.WriteString("XM Channels (" + bitrate_desc + ")");
            writer.WriteEndElement();

            //<link>" + link + "</link>
            writer.WriteStartElement("link");
            writer.WriteString(link);
            writer.WriteEndElement();

            writer.WriteStartElement("atom:link");
            writer.WriteAttributeString("href", link);
            writer.WriteAttributeString("rel", "self");
            writer.WriteAttributeString("type", "application/rss+xml");
            writer.WriteEndElement();

            //<description>English</description>
            writer.WriteStartElement("description");
            writer.WriteString("English");
            writer.WriteEndElement();

            //<language>en-us</language>
            writer.WriteStartElement("language");
            writer.WriteString("en-us");
            writer.WriteEndElement();

            //Channels
            foreach (XMChannel chan in list)
            {
                String media;
                if (UseMMS) {
                    media = "mms://";
                } else {
                    media = "http://";
                }
                media += serverHost + "/streams/" + chan.num + "/" + bitrate;

                //<item>
                writer.WriteStartElement("item");

                //<title>XM " + chan.num + " - " + chan.name + "</title>
                writer.WriteStartElement("title");
                writer.WriteString("XM " + chan.num + " - " + chan.name);
                writer.WriteEndElement();

                //<link>"+media+"</link>
                writer.WriteStartElement("link");
                writer.WriteString(media);
                writer.WriteEndElement();

                writer.WriteStartElement("guid");
                writer.WriteString(media);
                writer.WriteEndElement();

                //<description>" + chan.desc + "</description>
                writer.WriteStartElement("description");
                writer.WriteString(chan.desc);
                writer.WriteEndElement();

                //<media:content url=\""+media+"\" type=\"audio/x-ms-wma\" medium=\"audio\" />
                writer.WriteStartElement("media:content");
                writer.WriteAttributeString("url", media);
                writer.WriteAttributeString("type", "audio/x-ms-wma");
                writer.WriteAttributeString("medium", "audio");
                writer.WriteEndElement();

                //<language>en-us</language>
                //writer.WriteStartElement("language");
                //writer.WriteString("en-us");
                //writer.WriteEndElement();

                //<upnp:region>United States</upnp:region>
                writer.WriteStartElement("upnp:region");
                writer.WriteString("United States");
                writer.WriteEndElement();

                //</item>
                writer.WriteEndElement();
            }

            //Close channel </channel>
            writer.WriteEndElement();

            //Write the close tag for the root element. </rss>
            writer.WriteEndElement();

            writer.Flush();
            MemoryStream OutputStream = new MemoryStream();
            MemoryStream.WriteTo(OutputStream);

            //Write the XML to file and close the writer.
            writer.Close();

            return OutputStream;
        }
    }
}
