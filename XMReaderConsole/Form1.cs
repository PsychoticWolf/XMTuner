using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XMReaderConsole
{
    public partial class Form1 : Form
    {
        XMTuner self;
        WebListner xmServer;
        bool loggedIn = false;
        
   
        public Form1()
        {
            InitializeComponent();
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
            outputbox.Text = "Please wait... logging in";
            outputbox.Refresh();
            self = new XMTuner(txtUser.Text, txtPassword.Text);
            xmServer = new WebListner(self);
            outputbox.Text = self.OutputData;
            xmServer.start();
            loggedIn = true;
            outputbox.Text = self.OutputData;
            self.writeLog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (loggedIn)
            {
                int channum = Convert.ToInt32(txtChan.Text);
                self.play(channum, "high");
                outputbox.Text = self.OutputData;
                self.writeLog();
            }
            else
            {
                outputbox.AppendText("No connection - try again");
                outputbox.Refresh();
                self.writeLog();
            }
        }

        private void outputbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            refreshOutput(self.OutputData);
        }

    }
}
