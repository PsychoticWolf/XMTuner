using System;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;

namespace XMTuner
{
    public partial class Form1 : Form
    {
        int playerNum;
        int p;
        int sleepPlayerNum = 0;

        int wmpVisWidth;
        int wmpVisHeight;

        #region Player
        private void updateNowPlayingData(Boolean useDefault, Boolean isLoading, Int32 num)
        {
            if (useDefault == true || isLoading == true)
            {
                pLogoBox.ImageLocation = "";
                showLogo();
                syncStatusLabel();
                pLabel1.Text = ""; //"Channel:";
                pLabel2.Text = ""; //"Title:";
                pLabel3.Text = ""; //"Artist:";
                pLabel4.Text = ""; //"Album:";
                pLabel5.Text = "";
                pLabel6.Text = "";

                if (isLoading == true && num > 0)
                {
                    XMChannel npChannel = self.Find(num);
                    if (npChannel.num != 0)
                    {
                        pLabel1.Text = npChannel.ToString();
                        pLabel2.Text = "Loading...";
                    }
                }

                //playerPanel.Refresh();
            }
            else
            {
                if (num == 0)
                {
                    num = playerNum;
                }

                XMChannel npChannel = self.Find(num);
                if (npChannel.num == 0) { return; }

                if (pStatusLabel.Visible == true)
                {
                    syncStatusLabel();
                }

                if (npChannel.logo != null)
                {
                    if (pLogoBox.ImageLocation.Equals(""))
                    {
                        pLogoBox.ClientSize = new Size(128, 50);
                        pLogoBox.ImageLocation = npChannel.logo;
                    }
                }
                pLabel1.Text = npChannel.ToString();
                pLabel2.Text = npChannel.song;
                pLabel3.Text = npChannel.artist;
                //pLabel5 is Player status (Handled in axWindowsMediaPlayer1_StatusChange)
                pLabel6.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString; //pLabel6 is Player timer

                //pLabel4: Tri-mode Artist/Program Now/Next Text
                if (p <= 5)
                {
                    if (npChannel.album == null || npChannel.album.Equals("")) { p = 5; p++; return; }
                    pLabel4.Text = npChannel.album;
                }
                else if (p <= 10 && p > 5)
                {
                    String[] program = self.getCurrentProgram(npChannel.programData);
                    if (program == null) { p = 10; p++; return; }
                    pLabel4.Text = "Now: " + program[2];
                }
                else if (p > 10)
                {
                    String[] nextProgram = self.getNextProgram(npChannel.programData);
                    if (nextProgram == null) { p = 0; p++; return; }
                    pLabel4.Text = "Next: " + DateTime.Parse(nextProgram[4]).ToShortTimeString() + ": " + nextProgram[2];
                    if (p >= 15) { p = 0; }
                }
                p++;
            }
        }

        private void syncStatusLabel()
        {
            pStatusLabel.Visible = true;
            if (c.isLoggedIn == false)
            {
                if (cfg.loaded == false)
                {
                    pStatusLabel.Text = "XMTuner needs to be configured before you can begin...";
                }
                else
                {
                    pStatusLabel.Text = "Log in to begin...";
                }
            }
            else
            {
                if (playerNum != 0)
                {
                    pStatusLabel.Text = "";
                    pStatusLabel.Visible = false;
                }
                else
                {
                    pStatusLabel.Text = "Select a channel...";
                }
            }
        }

        private void play(int num)
        {
            updateNowPlayingData(true, true, num);

            String url = self.play(num, "high");
            if (url == null)
            {
                pLabel2.Text = "Error fetching stream";
                pLabel3.Text = "Check log for more details...";
                pStatusLabel.Text = "";
                return;
            }
            
            axWindowsMediaPlayer1.URL = url;
            playerNum = num;
            
            updateNowPlayingData(false, false, num);

            updateRecentlyPlayedBox();
        }

        private void axWindowsMediaPlayer1_StatusChange(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsReady //&&
                //axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsStopped
                )
            {
                String status = axWindowsMediaPlayer1.status;
                if (status.Contains("Playing"))
                {
                    String[] temp = status.Replace("reflector:", "").Split(':');
                    status = "Playing (" + temp[1].Trim() + ")";
                }
                pLabel5.Visible = true;
                pLabel5.Text = status;
                if (status.Equals("") == false)
                {
                    if (playerNum != 0)
                    {
                        output("Player (" + self.Find(playerNum).ShortName + "): " + status, LogLevel.Player);
                    }
                    else
                    {
                        output("Player: " + status, LogLevel.Player);
                    }
                }
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            output("Player ("+self.Find(playerNum).ShortName+"): " + axWindowsMediaPlayer1.playState, LogLevel.PlayerDebug);

            // If Windows Media Player is in the playing state, enable the data update timer. 
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                pRetryTimer.Enabled = false;
                axWindowsMediaPlayer1.enableContextMenu = true;
                pTimer.Enabled = true;
                showWMPPlayerUI();
            }
            else
            {
                pTimer.Enabled = false;
            }

            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped ||
                axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsReady)
            {
                axWindowsMediaPlayer1.enableContextMenu = false;
                //Save number when going to ready to restore on resume from sleep
                if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsReady)
                {
                    sleepPlayerNum = playerNum;
                }
                //Tell the app we're done playing so history stops being built.
                playerNum = 0;
                self.lastChannelPlayed = 0;
                updateNowPlayingData(true, false, 0);
            }

        }

        private void pTimer_Tick(object sender, EventArgs e)
        {
            updateNowPlayingData(false, false, 0);
        }

        private void pLabel2_TextChanged(object sender, EventArgs e)
        {
            if (pLabel2.Text.Equals("Title:") || pLabel2.Text.Equals(""))
            {
                return;
            }
            self.lastChannelPlayed = playerNum;
            updateRecentlyPlayedBox();
            doNotification();
            updateChannels();
        }

        private void doNotification()
        {
            if (playerNum == 0 || !cfg.showNotification) { return; } //Bail early if we have no work to do.
            XMChannel npChannel = self.Find(playerNum);
            String title = npChannel.ToString();
            String nptext = npChannel.artist + " - " + npChannel.song;
            NotifyWindow nw = new NotifyWindow(title, "Now Playing:\n" + nptext);
            nw.TitleFont = new Font("Tahoma", 8.25F, FontStyle.Bold);
            nw.Font = new Font("Tahoma", 10F);
            nw.TextColor = Color.White;
            nw.BackColor = Color.Black;
            nw.SetDimensions(300, 120);
            nw.WaitTime = 5000;
            nw.Notify();
        }

        private void initPlayer()
        {
            wmpVisWidth = axWindowsMediaPlayer1.Size.Width;
            wmpVisHeight = axWindowsMediaPlayer1.Size.Height;

            createPresetButtons();
            showLogo();
            axWindowsMediaPlayer1.uiMode = "none";

            pLabel5.Visible = false;
            updateNowPlayingData(true, false, 0);

            syncStatusLabel();

        }

        private void showLogo()
        {
            Image xmtunerLogo = Image.FromStream(Assembly.GetExecutingAssembly().GetManifestResourceStream("XMTuner.xmtuner64.png"));
            pLogoBox.ClientSize = new Size(64, 64);
            pLogoBox.Image = xmtunerLogo;
        }

        private void axWindowsMediaPlayer1_MouseMoveEvent(object sender, AxWMPLib._WMPOCXEvents_MouseMoveEvent e)
        {
            if (axWindowsMediaPlayer1.uiMode.Equals("mini") || axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsPlaying)
            {
                return;
            }
            showWMPPlayerUI();
        }

        private void showWMPPlayerUI()
        {
            axWindowsMediaPlayer1.uiMode = "mini";
            axWindowsMediaPlayer1.Size = new Size(wmpVisWidth, 35); //new Size(165, 35);
            pHoverTimer.Start();
        }

        private void pHoverTimer_Tick(object sender, EventArgs e)
        {
            pHoverTimer.Stop();
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.Size = new Size(wmpVisWidth, wmpVisHeight);
        }

        private void shutdownPlayer()
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        private void axWindowsMediaPlayer1_ErrorEvent(object sender, EventArgs e)
        {
            // Get the description of the first error. 
            int errCode = axWindowsMediaPlayer1.Error.get_Item(0).errorCode;
            string errDesc = axWindowsMediaPlayer1.Error.get_Item(0).errorDescription;

            // Display the error description.
            output(errCode + " " +errDesc, LogLevel.Error);

            //Handle things like stopping and trying to retune...
            int cnum = playerNum;
            axWindowsMediaPlayer1.Ctlcontrols.stop();
            pLabel1.Text = "Error!";
            pLabel2.Text = "A problem occured while playing your channel.";
            pLabel3.Text = "Attempting to retune (in 10 seconds)...";
            pLabel4.Text = errDesc;
            pStatusLabel.Text = "";
            output("Error! Attempting to retune channel "+self.Find(cnum).ToString()+" (in 10 seconds)...", LogLevel.Error);
            pRetryTimer.Tag = cnum;
            pRetryTimer.Start();
        }

        private void pRetryTimer_Tick(object sender, EventArgs e)
        {
            int cnum = (int)pRetryTimer.Tag;
            output("Attempting to retune to channel " + self.Find(cnum).ToString() + "...", LogLevel.Info);
            play(cnum);
        }

        private void txtChannelNum_Enter(object sender, EventArgs e)
        {
            play(Convert.ToInt32(txtChannelNum.Text));
        }

        private void txtChannelNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Delete) { return; }

            if (e.KeyChar == (char)Keys.Return)
            {
                if (txtChannelNum.Text.Equals("")) { return; }
                txtChannelNum.Text = txtChannelNum.Text.Replace(".", "");
                int num = Convert.ToInt32(txtChannelNum.Text);
                play(num);
                tabcontrol1.Focus();
                return;
            }
            if (Char.IsDigit(e.KeyChar) == false)
            {
                e.Handled = true;
            }
        }

        private void createPresetButtons()
        {
            const int w = 50;
            const int spacing = 2;
            int h = panelPresets.Size.Height; //const int h = 25;
            int maxwidth = panelPresets.Size.Width;
            int x = 0;
            const int y = 0;
            x = (maxwidth - (10 * (w + spacing))) / 2;

            for (int i = 1; i <= 10; i++)
            {
                Button pb = new Button();
                pb.Location = new Point(x, y);
                pb.Size = new Size(w, h);
                x += w + spacing;
                pb.Text = i.ToString();
                pb.Name = "presetButton"+i.ToString();
                pb.Click += new System.EventHandler(presetButton_Click);
                panelPresets.Controls.Add(pb);
            }

            //Now populate them with something useful...
            //updatePresetButtons();
        }
        private void presetButton_Click(object sender, EventArgs e)
        {
            Button buttonClicked = (Button)sender;
            int pn = Convert.ToInt32(buttonClicked.Name.Replace("presetButton", ""));
            int chnum = self.favorites.findPreset(pn);
            if (chnum > 0)
            {
                play(chnum);
            }
        }

        private void updatePresetButtons()
        {
            //if (self.favorites == null) { return; }

            ImageList imageList = new ImageList();
            imageList.ImageSize = new Size(45, 40);
            String imageKey;
            foreach (Button presetButton in panelPresets.Controls)
            {
                presetButton.Enabled = false;
                int pn = Convert.ToInt32(presetButton.Name.Replace("presetButton", ""));
                imageKey = pn.ToString();
                presetButton.Image = null;
                presetButton.ImageList = null;
                presetButton.ImageKey = null;
                presetButton.Text = pn.ToString();
                int cn = self.favorites.findPreset(pn);
                if (cn != 0)
                {
                    XMChannel chan = self.Find(cn);
                    if (chan.logo_small_image != null)
                    {
                        imageList.Images.Add(imageKey, chan.logo_small_image);
                    }
                    presetButton.Enabled = true;
                    presetButton.Text = null;
                    presetButton.ImageList = imageList;
                    presetButton.ImageKey = imageKey;
                }
            }


        }

        #endregion
    }

}