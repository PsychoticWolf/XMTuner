/*
 *  Copyright (c) 2003-2009 MediaMall Technologies, Inc.
 *  All rights reserved.
 * 
 *  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY
 *  EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES
 *  OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT
 *  SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL,
 *  SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT
 *  OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 *  HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
 *  OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
 *  SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Xml;
using MediaMallTechnologies.Plugin;
using XMTuner;

namespace XMTunerPlugin {
	public class SamplePluginProvider : MediaMallTechnologies.Plugin.IPlayOnProvider {

		// ---------------------------------------------------------------
		// Instance fields
		// ---------------------------------------------------------------

		private IPlayOnHost host;
		private VirtualFolder rootFolder;
		private Hashtable titleLookup = new Hashtable();
		private Hashtable folderLookup = new Hashtable();

        private FeedReader feed;

		// ---------------------------------------------------------------
		// Instance methods
		// ---------------------------------------------------------------

		public void SetPlayOnHost(IPlayOnHost h) {
			this.host = h;

            feed = new FeedReader(this.host);

			// create a root folder
			this.rootFolder = new VirtualFolder(this.ID, this.Name);

			// create a sub folder
			VirtualFolder subFolder = new VirtualFolder(createGuid(), "Sample Folder", "http://www.themediamall.com/downloads/playon/plugins/api/sample/sample.xml", true);

			// add this folder and cache its ID for lookups
			this.rootFolder.AddFolder(subFolder);
			this.folderLookup[subFolder.Id] = subFolder;

			// check for custom properties, and create a dummy empty folder just as an example
			if (this.host.Properties["testValue"] != null) {
				subFolder = new VirtualFolder(createGuid(), this.host.Properties["testValue"], null, false);
				this.rootFolder.AddFolder(subFolder);
				this.folderLookup[subFolder.Id] = subFolder;
			}
		}

		/* ------------------------------------------------------------- */

		private void load(VirtualFolder vf) {

			try {
				// reset this folder to clear potential stale content (relevant for dynamically loaded data)
				//vf.Reset();

				// load dynamic data
                /*
				HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(vf.SourceURL);
				StreamReader sr = new StreamReader(req.GetResponse().GetResponseStream());
				string xml = sr.ReadToEnd();
				sr.Close();

				// parse XML
				XmlDocument doc = new XmlDocument();
				doc.LoadXml(xml);
				XmlNodeList items = doc.GetElementsByTagName("item");
				foreach (XmlNode node in items) {
					string title = "", description = "", thumbnail = "", url = "";
					long duration = 0;
					DateTime date = DateTime.MinValue;
					foreach (XmlNode child in node.ChildNodes) {
						switch (child.Name) {
							case "title":
								title = child.InnerText;
								break;
							case "description":
								description = child.InnerText;
								break;
							case "url":
								url = child.InnerText;
								break;
							case "thumbnail":
								thumbnail = child.InnerText;
								break;
							case "duration":
								duration = long.Parse(child.InnerText);
								break;
							case "pubDate":
								date = DateTime.Parse(child.InnerText, System.Globalization.CultureInfo.InvariantCulture);
								break;
						}
					}

					// create ID
					string guid = vf.FindGuid(url);
					if (guid == null)
						guid = createGuid();
					VideoResource info = new VideoResource(guid, vf.Id, title, url, description, thumbnail, date, url, null, duration, 0, null, null);

					// cache lookup and add to folder
					this.titleLookup[info.Id] = info;
					vf.AddMedia(info);
				}
                 */
                foreach (XMChannel channel in feed.channels)
                {
                    String url = channel.url;
                    String title = channel.ToString();
                    String description = channel.desc;
                    String thumbnail = null;

                    // create ID
                    string guid = vf.FindGuid(url);
                    if (guid == null)
                        guid = createGuid();
                    AudioResource info = new AudioResource(guid, vf.Id, title, url, description, thumbnail, DateTime.MinValue, url, null, 0, 0, null, null, null, -1);
                    // cache lookup and add to folder
                    this.titleLookup[info.Id] = info;
                    vf.AddMedia(info);
                }
			}
			catch (Exception ex) {
				log("Error: " + ex);
			}
		}

		/* ------------------------------------------------------------- */

		public string Name {
			get {
				return "XMTuner Plugin";
			}
		}

		/* ------------------------------------------------------------- */

		public string ID {
			get {
				// make sure this is 24 chars or less (otherwise won't work on Xbox 360)
				return "playon.xmtunerplugin";
			}
		}

		/* ------------------------------------------------------------- */

		private string createGuid() {
			return this.ID + "-" + Guid.NewGuid();
		}

		/* ------------------------------------------------------------- */

		private List<AbstractSharedMediaInfo> getRange(List<AbstractSharedMediaInfo> list, int startIndex, int requestCount) {

			if (requestCount == 0)
				requestCount = int.MaxValue;
			List<AbstractSharedMediaInfo> items;
			if (startIndex > list.Count) {
				items = new List<AbstractSharedMediaInfo>(0);
			}
			else {
				items = list.GetRange(startIndex, Math.Min(requestCount, list.Count - startIndex));
			}
			return items;
		}

		/* ------------------------------------------------------------- */

		public MediaMallTechnologies.Plugin.Payload GetSharedMedia(string id, bool includeChildren, int startIndex, int requestCount) {

			if (id == null || id.Length == 0)
				return new Payload("-1", "-1", "[Unknown]", 0, new List<AbstractSharedMediaInfo>(0));

			List<AbstractSharedMediaInfo> currentList;

			// Root
			if (id == this.ID) {
                load(this.rootFolder);
				// return all subfolders for this root folder
				currentList = new List<AbstractSharedMediaInfo>();
				/* foreach (VirtualFolder vf in this.rootFolder.Items) {
					vf.ParentId = this.ID;
					currentList.Add(new SharedMediaFolderInfo(vf.Id, id, vf.Title, vf.Items.Count));
				} */
                foreach (object o in this.rootFolder.Items)
                {
                    if (o is VirtualFolder)
                    {
                        VirtualFolder vf = o as VirtualFolder;
                        vf.ParentId = this.ID;
                        currentList.Add(new SharedMediaFolderInfo(vf.Id, vf.Id, vf.Title, vf.Items.Count));
                    }
                    else if (o is SharedMediaFileInfo)
                    {
                        SharedMediaFileInfo file = (SharedMediaFileInfo)o;
                        currentList.Add(file);
                    }
                }
				return new Payload(id, "0", this.Name, currentList.Count, getRange(currentList, startIndex, requestCount));
			}
			else
            {
				// if ID is for an item, return it
				if (titleLookup[id] != null) {
					SharedMediaFileInfo fileInfo = (SharedMediaFileInfo)titleLookup[id];
					currentList = new List<AbstractSharedMediaInfo>();
					currentList.Add(fileInfo);
					return new Payload(id, fileInfo.OwnerId, fileInfo.Title, 1, currentList, false);
				}
				// if ID is for a folder, return it with subfolders and items
				if (folderLookup[id] != null) {
					currentList = new List<AbstractSharedMediaInfo>();
					VirtualFolder vf = (VirtualFolder)folderLookup[id];
					
					// load this folder if dynamic (but avoid unnecessary web traffic)
					if (vf.Dynamic && (DateTime.Now - vf.LastLoad).TotalSeconds > 300) {
						load(vf);
						vf.LastLoad = DateTime.Now;
					}

					foreach (object o in vf.Items) {
						if (o is VirtualFolder) {
							VirtualFolder folder = o as VirtualFolder;
							currentList.Add(new SharedMediaFolderInfo(folder.Id, vf.Id, folder.Title, folder.Items.Count));
						}
						else if (o is SharedMediaFileInfo) {
							SharedMediaFileInfo file = (SharedMediaFileInfo)o;
							currentList.Add(file);
						}
					}
					return new Payload(id, vf.ParentId, vf.Title, currentList.Count, getRange(currentList, startIndex, requestCount));
				}

				return new Payload("-1", "-1", "[Unknown]", 0, new List<AbstractSharedMediaInfo>(0));
			}
		}

		/* ------------------------------------------------------------- */

		public System.Drawing.Image Image {
			get {
				System.Drawing.Image image = null;
				Stream imageStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("SamplePlugin.Sample.png");
				if (imageStream != null) {
					image = System.Drawing.Image.FromStream(imageStream);
					imageStream.Close();
				}
				return image;
			}
		}

		/* ------------------------------------------------------------- */

		public string Resolve(MediaMallTechnologies.Plugin.SharedMediaFileInfo fileInfo) {

            string type = "wmp";
			string url = fileInfo.Path;

			StringWriter sw = new StringWriter();
			XmlTextWriter writer = new XmlTextWriter(sw);
			writer.Formatting = Formatting.Indented;
			writer.WriteStartElement("media");
			writer.WriteStartElement("url");
			writer.WriteAttributeString("type", type);
			writer.WriteString(url);
			writer.WriteEndElement();
			writer.WriteEndElement();
			writer.Close();
			string xml = sw.ToString();

			return xml;
		}

		/* ------------------------------------------------------------- */

		private void log(string message) {
			this.host.LogMessage(message);
		}

	}
}
