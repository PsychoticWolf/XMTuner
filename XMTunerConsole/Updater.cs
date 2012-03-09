/*
 * XMTuner: Copyright (C) 2009-2012 Chris Crews and Curtis M. Kularski.
 * 
 * This file is part of XMTuner.

 * XMTuner is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.

 * XMTuner is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.

 * You should have received a copy of the GNU General Public License
 * along with XMTuner.  If not, see <http://www.gnu.org/licenses/>.
 */

using System;
using System.Collections.Generic;
using System.Xml;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace XMTuner
{
    class Updater
    {

        // get the running version
        Version curVersion = Assembly.GetExecutingAssembly().GetName().Version;

        // in newVersion variable we will store the
        // version info from xml file
        Version newVersion = null;
        // and in this variable we will put the url we
        // would like to open so that the user can
        // download the new version
        // it can be a homepage or a direct
        // link to zip/exe file
        string url = "";
        String downloadURL = "";
        String details = "";

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
                    string xmlURL = "http://www.xmtuner.net/update.xml?v="+curVersion.ToString();
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
                      case "download":
                      downloadURL = reader.Value;
                      break;
                      case "details":
                      details = reader.Value;
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
                UpdaterForm update = new UpdaterForm(curVersion, newVersion, url, downloadURL, details);
                update.Show();
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

                string message = ct+"  New version available! Get XMTuner " + newVersion.ToString(3) + " at http://www.pcfire.net/xmtuner/update/\n";
                outputbox.SelectionColor = Color.OrangeRed;
                outputbox.AppendText(message);
            }
        }
    }
}