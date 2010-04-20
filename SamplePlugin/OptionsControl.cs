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
using System.Windows.Forms;

namespace XMTunerPlugin {
	public partial class OptionsControl : UserControl {

		// ---------------------------------------------------------------
		// Instance fields
		// ---------------------------------------------------------------

		private NameValueCollection options;
		private EventHandler changeHandler;

		// ---------------------------------------------------------------
		// Constructors
		// ---------------------------------------------------------------

		public OptionsControl(NameValueCollection props, EventHandler e) {
			InitializeComponent();
            setTooltips();

			// maintain a reference to this collection, as this is where we make changes
			this.options = props;

			// maintain a reference to the change handler, so "Apply" button can be enabled
			this.changeHandler = e;

			// load our properties from previously cached runs
            if (this.options["hostname"] != null)
                this.textHost.Text = this.options["hostname"];

            if (this.options["port"] != null)
                this.textPort.Text = this.options["port"];

            if (this.options["useDuration"] != null)
                this.checkDuration.Checked = Convert.ToBoolean(this.options["useDuration"]);
		}


		// ---------------------------------------------------------------
		// Instance methods
		// ---------------------------------------------------------------

        private void setTooltips()
        {
            toolTip1.SetToolTip(textHost, "IP Address or Hostname of the Computer running XMTuner\n(Ex: 192.168.1.100) (Leave Blank for localhost)");
            toolTip1.SetToolTip(textPort, "Port number that XMTuner is listening on\n(Leave blank for default: 19081)");
            toolTip1.SetToolTip(checkDuration, "Force non-zero duration values (will return duration of 24 hours instead)");
        }

        private void textHost_TextChanged(object sender, EventArgs e)
        {
            this.options["hostname"] = this.textHost.Text;
            this.changeHandler.Invoke(this, new EventArgs());
        }

        private void textPort_TextChanged(object sender, EventArgs e)
        {
            this.options["port"] = this.textPort.Text;
            this.changeHandler.Invoke(this, new EventArgs());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            this.options["useDuration"] = this.checkDuration.Checked.ToString();
            this.changeHandler.Invoke(this, new EventArgs());
        }
	}
}
