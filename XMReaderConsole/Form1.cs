using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace XMReaderConsole
{
    public partial class Form1 : Form
    {
        XMTuner self;
        WebListner xmServer;
        bool loggedIn = false;
        bool serverRunning = false;
        String port = "";
        String bitrate;
        bool highbit = true;
        bool autologin = false;
        bool isMMS = false;
        int i = 0;
        double sec = 0;
        double minute = 0;
        double hour = 0;
        String runTime = "";

        public Form1()
        {
           
            InitializeComponent();
        }

        // This delegate enables asynchronous calls for setting
        // the text property on a TextBox control.
        public delegate void SetTextCallback(ref RichTextBox outputbox, string text, Color color);

        public static void output(String output, String level, ref RichTextBox outputbox)
        {
            Color color = Color.Black;
            if (level.ToLower().Equals("debug") || level.ToLower().Equals("error"))
            {
                color = Color.Red;
            }

            //We can't talk to outputbox from the server thread...
            if (outputbox.InvokeRequired)
            {
                SetTextCallback d = new Form1.SetTextCallback(SetText);
                outputbox.Invoke (d, new object[] {outputbox, output, color});
            }
            else
            {
                outputbox.SelectionColor = color;
                outputbox.AppendText(output);
                outputbox.Refresh();
            }


        }

        // This method is passed in to the SetTextCallBack delegate
        // to set the Text property of textBox1.
        private static void SetText(ref RichTextBox outputbox, string text, Color color)
        {
            outputbox.SelectionColor = color;
            outputbox.AppendText(text);
            outputbox.Refresh();
        }

        public void refreshOutput(String output)
        {
            outputbox.Text = output;
            outputbox.Refresh();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            outputbox.Text = outputbox.Text+"Please wait... logging in\n";
            outputbox.Refresh();
            if (highbit) { bitrate = "high"; } else { bitrate = "low"; }
            self = new XMTuner(txtUser.Text, txtPassword.Text, ref outputbox, bitrate, isMMS);
            timer2.Enabled = true;
            xmServer = new WebListner(self, port);
            i = 0;
            serverRunning = true;
            self.OutputData = outputbox.Text + self.OutputData;
            //outputbox.Text = self.OutputData;
            xmServer.start();
            
            //outputbox.Text = self.OutputData;
            loggedIn = true;
            if (loggedIn) {button1.Enabled = false; button5.Enabled = true;}
            
            loadChannels();
            self.writeLog();
        }



        private void outputbox_TextChanged(object sender, EventArgs e)
        {
            outputbox.SelectionStart = outputbox.Text.Length;
            outputbox.ScrollToCaret();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            log();
            //MessageBox.Show("Info updated and logged");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2(txtUser.Text, txtPassword.Text, port, highbit, autologin, isMMS);
            form2.ShowDialog();
            refreshConfig();
        }

        private bool refreshConfig()
        {
            configMan configuration = new configMan();
            configuration.readConfig();
            //outputbox.SelectionColor = Color.Blue;
            if (configuration.isConfig)
            {
                String[] configArray = configuration.getConfig();
                txtUser.Text = configArray[1];
                txtPassword.Text = configArray[2];
                port = configArray[3];
                highbit = Convert.ToBoolean(configArray[4]);
                autologin = Convert.ToBoolean(configArray[5]);
                isMMS = Convert.ToBoolean(configArray[6]);

                outputbox.AppendText("Configuration Loaded\n");
                if (isMMS) { outputbox.AppendText("URLs default to MMS\n"); }
                return true;
            }
            else
            {
                if (port.Equals(""))
                {
                    port = "19081";
                }
                
                outputbox.AppendText("No Configuration\n");
                return false;
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (serverRunning)
            {
                //outputbox.Text = self.OutputData;
                //outputbox.Refresh();
                i++;
                sec = i;

                if (sec >= 60) { minute = sec / 60; minute = Math.Floor(minute); sec = sec - (minute * 60); }
                if (minute >= 60) { hour = minute / 60; hour = Math.Floor(hour); minute = minute - (hour * 60); }


                runTime = hour.ToString() + ":";
                if (minute < 10) { runTime += "0"+minute.ToString() + ":"; }
                else {runTime+=minute.ToString()+":";}

                if (sec < 10) { runTime += "0" + sec.ToString(); }
                else { runTime += sec.ToString(); }

                lblClock.Text = runTime;
            }
        }
        private void Form1_FormClosing(Object sender, FormClosingEventArgs e)
        {
            log();
        }

        public void log()
        {
            string path = @"XMTuner.log";
            FileStream fs = new FileStream(path, FileMode.Create, FileAccess.Write);
            StreamWriter textOut = new StreamWriter(fs);

            DateTime datetime = DateTime.Now;
            String header = "XMTuner Output\n";
            header += datetime.ToString() + "\n\n";
            textOut.Write(header+outputbox.Text+"\nTime: "+i);
            
            textOut.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            about aboutBox = new about();
            aboutBox.ShowDialog();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (loggedIn)
            {
                self.doWhatsOn();
            }
            log();
        }



        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (refreshConfig() && autologin)
            {
                button1_Click(sender, e);
            }
            lblClock.Text = "0:00:00";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            xmServer.stop();
            serverRunning = false;
            button1.Enabled = true;
            button5.Enabled = false;
            
        }

        private void loadChannels()
        {
            int i = 0;
            foreach (XMChannel chan in self.getChannels())
            {
                channelBox.Items.Add(chan.ToSimpleString());
                if (i == 0) { channelBox.SelectedItem = chan.ToSimpleString(); }
                i++;
            }
            if (isMMS) { protocolBox.SelectedItem = "MMS"; } else { protocolBox.SelectedItem = "HTTP"; }
            addressBox.Text = getChannelAddress((String)channelBox.SelectedItem, (String)protocolBox.SelectedItem);
            
        }

        private String getChannelAddress(String channelString, String protocol)
        {
            String[] tmp1 = channelString.Split('[');
            String[] tmp2 = tmp1[1].Split(']');
            String channelNum = tmp2[0];
            String channelAddress = protocol.ToLower()+"://"+ipName.Text+":"+port+"/streams/"+channelNum+"/"+bitrate;

            return channelAddress;
        }

        private void makeAddress(object sender, EventArgs e)
        {
            String protocol;
            String channel;
            protocol = (String) protocolBox.SelectedItem;
            channel = (String)channelBox.SelectedItem; 
            addressBox.Text = getChannelAddress(channel, protocol);
        }

        private void channelBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void exitXMTunerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

    }
}
