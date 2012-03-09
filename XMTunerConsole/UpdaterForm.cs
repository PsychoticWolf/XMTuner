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
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Threading;

namespace XMTuner
{
    public partial class UpdaterForm : Form
    {
        WebClient client = new WebClient();
        String downloadURL;
        String destination = Directory.GetCurrentDirectory() + "\\update.msi";
        String url;

        public UpdaterForm(Version curVersion, Version newVersion, String url, String downloadURL, String details)
        {
            InitializeComponent();
            lblCurrentVersion.Text = "Current Version: " + curVersion.ToString(3);
            lblNewVersion.Text = "New Version: " + newVersion.ToString(3);
            this.downloadURL = downloadURL;
            this.url = url;
            lnkMoreInfo.Enabled = true;
            lblDetails.Text = details;
            File.Delete(destination);
        }

        private void UpdaterForm_Load(object sender, EventArgs e)
        {

        }

        private void btnStartDownload_Click(object sender, EventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Marquee;
            dnldStatus.Text = "Starting...";
            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            // Starts the download
            client.DownloadFileAsync(new Uri(downloadURL), destination);

            btnStartDownload.Enabled = false;
            btnNo.Enabled = false;

        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Style = ProgressBarStyle.Blocks;
            btnAbort.Enabled = true;
            double bytesIn = double.Parse(e.BytesReceived.ToString());
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
            double percentage = bytesIn / totalBytes * 100;

            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
            dnldStatus.Text = e.ProgressPercentage + "% | " + e.BytesReceived + " of " + e.TotalBytesToReceive + " bytes";
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                dnldStatus.Text = "Download Cancelled";
                btnStartDownload.Text = "Retry";
                btnStartDownload.Enabled = true;
                btnAbort.Enabled = false;
            }
            else
            {
                dnldStatus.Text = "Download Completed Click \"Update!\" to continue...";
                btnAbort.Text = "Update!";
                btnAbort.Font = new Font(btnAbort.Font, btnAbort.Font.Style | FontStyle.Bold);

                btnAbort.Enabled = true;
                
            }
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (btnAbort.Text.Equals("Update!"))
            {
                System.Diagnostics.Process.Start(destination);
                Thread.Sleep(1500);
                //XXX Service?
                Application.Exit();
                return;
            }
            client.CancelAsync();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(url);
        }
    }
}
