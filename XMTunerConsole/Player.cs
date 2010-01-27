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
                    pLabel1.Text = "XM " + npChannel.num + " - " + npChannel.name;
                    pLabel2.Text = "Loading...";
                }

                panel1.Refresh();
            }
            else
            {
                if (num == 0)
                {
                    num = playerNum;
                }

                XMChannel npChannel = self.Find(num);

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
                pLabel1.Text = "XM " + npChannel.num + " - " + npChannel.name;
                pLabel2.Text = npChannel.song;
                pLabel3.Text = npChannel.artist;
                //pLabel5 is Player status (Handled in axWindowsMediaPlayer1_StatusChange)
                pLabel6.Text = axWindowsMediaPlayer1.Ctlcontrols.currentPositionString; //pLabel6 is Player timer

                //pLabel4: Tri-mode Artist/Program Now/Next Text
                if (p <= 5)
                {
                    if (npChannel.album.Equals("")) { p = 5; p++; return; }
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
            if (loggedIn == false)
            {
                if (isConfigurationLoaded == false)
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
            axWindowsMediaPlayer1.URL = url;
            playerNum = num;
            updateNowPlayingData(false, false, num);

            updateRecentlyPlayedBox();
        }

        private void axWindowsMediaPlayer1_StatusChange(object sender, EventArgs e)
        {
            if (axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsReady &&
                axWindowsMediaPlayer1.playState != WMPLib.WMPPlayState.wmppsStopped)
            {
                String status = axWindowsMediaPlayer1.status;
                if (status.Contains("Playing"))
                {
                    String[] temp = status.Replace("reflector:", "").Split(':');
                    status = "Playing (" + temp[1].Trim() + ")";
                }
                pLabel5.Visible = true;
                pLabel5.Text = status;
            }
        }

        private void axWindowsMediaPlayer1_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            // If Windows Media Player is in the playing state, enable the data update timer. 
            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                axWindowsMediaPlayer1.enableContextMenu = true;
                pTimer.Enabled = true;
                showWMPPlayerUI();
            }
            else
            {
                pTimer.Enabled = false;
            }

            if (axWindowsMediaPlayer1.playState == WMPLib.WMPPlayState.wmppsStopped)
            {
                axWindowsMediaPlayer1.enableContextMenu = false;
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
            String dummy = pLabel2.Text;
            updateRecentlyPlayedBox();
            doNotification();
            updateChannels();
        }

        private void doNotification()
        {
            if (playerNum == 0) { return; } //Bail early if we have no work to do.
            XMChannel npChannel = self.Find(playerNum);
            String title = "XM " + npChannel.num + " - " + npChannel.name;
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
            axWindowsMediaPlayer1.Size = new Size(165, 35);
            pHoverTimer.Start();
        }

        private void pHoverTimer_Tick(object sender, EventArgs e)
        {
            pHoverTimer.Stop();
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.Size = new Size(165, 50);
        }

        private void shutdownPlayer()
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }

        #endregion
    }

}