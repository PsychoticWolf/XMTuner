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
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;

namespace XMTunerPlugin {

	public class SamplePluginSettings : MediaMallTechnologies.Plugin.IPlayOnProviderSettings {

		// ---------------------------------------------------------------
		// Instance methods
		// ---------------------------------------------------------------

		public System.Drawing.Image Image {
			get {
				Image image = null;
                Stream imageStream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("XMTunerPlugin.xmtuner.png");
				if (imageStream != null) {
					image = System.Drawing.Image.FromStream(imageStream);
					imageStream.Close();
				}
				return image;
			}
		}

		/* ------------------------------------------------------------- */

		public string Link {
			get {
				return "www.xmtuner.net";
			}
		}

		/* ------------------------------------------------------------- */

		public string Name {
			get {
				return "XMTuner";
			}
		}

		/* ------------------------------------------------------------- */

		public string ID {
			get {
				// IMPORTANT: Make sure you generate a single unique GUID here, for example from http://www.guidgenerator.com/
                return "4514a343-80a2-42ec-a1ba-27152d10dea6";
			}
		}

		/* ------------------------------------------------------------- */

		public string Description {
			get {
				return "Play SIRIUS|XM Satelite Radio via XMTuner";
			}
		}

		/* ------------------------------------------------------------- */

		public bool RequiresLogin {
			get {
				return false;
			}
		}

		/* ------------------------------------------------------------- */

		public bool TestLogin(string username, string password) {
			// block and process the login test here, and return true if login is successful
			return true;
		}

		/* ------------------------------------------------------------- */

		public string CheckForUpdate() {
			try {
				HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://www.xmtuner.net/playon/version.xml");
				StreamReader sr = new StreamReader(req.GetResponse().GetResponseStream());
				string xml = sr.ReadToEnd();
				sr.Close();
				
				// <samplePluginVersionCheck>
				//   <version>
				//     <plugin>1.0.0.4</plugin>
				//     <platform>2.59.3460.28</platform>
				//     <url>http://www.themediamall.com/playon/plugins</url>
				//   </version>
				// </samplePluginVersionCheck>

				System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
				doc.LoadXml(xml);
				System.Xml.XmlNodeList versions = doc.GetElementsByTagName("version");
				for (int i = 0; i < versions.Count; i++) {
					System.Xml.XmlNode version = versions.Item(i);
					Version pluginVersion = new Version(version["plugin"].InnerText);
					string url = version["url"].InnerText;
					if (Assembly.GetExecutingAssembly().GetName().Version < pluginVersion)
						return url;
				}
			}
			catch {
			}
			return null;
		}

		/* ------------------------------------------------------------- */

		public bool HasOptions {
			get {
                return true;
			}
		}

		/* ------------------------------------------------------------- */

		public System.Windows.Forms.Control ConfigureOptions(NameValueCollection props, EventHandler e) {
			return new OptionsControl(props, e);
		}

	}
}
