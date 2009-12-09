using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;

namespace XMReaderConsole
{
    class Updater
    {

        // get the running version
        Version curVersion =
         System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        // in newVersion variable we will store the
        // version info from xml file
        Version newVersion = null;
        // and in this variable we will put the url we
        // would like to open so that the user can
        // download the new version
        // it can be a homepage or a direct
        // link to zip/exe file
        string url = "";

        public Updater()
        {
            checkForUpdate();
            compareVersions();
        }

        //Background Version, reports to the outputbox
        public Updater(RichTextBox outputbox)
        {
            checkForUpdate();
            compareVersions(outputbox);
        }

        private void checkForUpdate() {

            XmlTextReader reader;
            try
            {
                 // provide the XmlTextReader with the URL of
                 // our xml document
                    string xmlURL = "http://users.pcfire.net/~wolf/XMReader/update.xml";
                 reader = new XmlTextReader(xmlURL);
                 // simply (and easily) skip the junk at the beginning
                 reader.MoveToContent();
                 // internal - as the XmlTextReader moves only
                 // forward, we save current xml element name
                 // in elementName variable. When we parse a
                 // text node, we refer to elementName to check
                 // what was the node name
                 string elementName = "";
                 // we check if the xml starts with a proper
                 // "ourfancyapp" element node
                 if ((reader.NodeType == XmlNodeType.Element) &&
                     (reader.Name == "xmtuner"))
                 {
                  while (reader.Read())
                  {
                   // when we find an element node,
                   // we remember its name
                   if (reader.NodeType == XmlNodeType.Element)
                    elementName = reader.Name;
                   else
                   {
                    // for text nodes...
                    if ((reader.NodeType == XmlNodeType.Text) &&
                        (reader.HasValue))
                    {
                     // we check what the name of the node was
                     switch (elementName)
                     {
                     case "version":
                      // thats why we keep the version info
                      // in xxx.xxx.xxx.xxx format
                      // the Version class does the
                      // parsing for us
                      newVersion = new Version(reader.Value);
                      break;
                     case "url":
                      url = reader.Value;
                      break;
                     }
                    }
                   }
                  }
                 }
                 reader.Close();
            }
            catch (Exception)
            {
            }
        }

        private void compareVersions()
        {
            // compare the versions
            if (curVersion.CompareTo(newVersion) < 0)
            {
                // ask the user if he would like
                // to download the new version
                string title = "New version available!";
                string question = "A new version of XMTuner is available.\n New Version: " + newVersion.ToString(3) + "\n Current Version: " + curVersion.ToString(3) + "\n\nWould you like to download the new version?";
                if (DialogResult.Yes ==
                 MessageBox.Show(question, title,
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    // navigate the default web
                    // browser to our app
                    // homepage (the url
                    // comes from the xml content)
                    System.Diagnostics.Process.Start(url);
                }
            }
            else
            {
                string title = "Latest Version";
                string message = "You're using the latest version of XMTuner. Check again later for new updates.";
                MessageBox.Show(message, title,
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Question);

            }

        }

        private void compareVersions(RichTextBox outputbox)
        {
            if (curVersion.CompareTo(newVersion) < 0)
            {
                DateTime currentTime = DateTime.Now;
                String ct = currentTime.ToString("%H:") + currentTime.ToString("mm:") + currentTime.ToString("ss");

                string message = ct+"  New version available! Get XMTuner " + newVersion.ToString(3) + " at "+url+"\n";
                outputbox.SelectionColor = Color.OrangeRed;
                outputbox.AppendText(message);
            }
        }
    }
}