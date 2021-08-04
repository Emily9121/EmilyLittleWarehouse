using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace MC65SnipeIT
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void ExitToOS_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void WiFiSetup_Click(object sender, EventArgs e)
        {
            //ProcessStartInfo.Filename = @"\Windows\WCConfigure.exe
            Process.Start("\\Windows\\WCConfigEd.exe", "");
        }

        private void ServSetup_Click(object sender, EventArgs e)
        {
            ServerSettings srvset = new ServerSettings();
            srvset.ShowDialog();
        }

        private void GSMSetup_Click(object sender, EventArgs e)
        {
            Process.Start("\\Windows\\remnet.exe", "");
        }

        private void Cat_Click(object sender, EventArgs e)
        {
            MainMenu mmenu = new MainMenu();
            mmenu.ShowDialog();

        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            About abouts = new About();
            abouts.ShowDialog();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // Main Menu Keyboard Shortcut
            if (e.KeyCode == Keys.D1)
            {
                Cat_Click(this, new EventArgs());
            }
            // WiFi Setup Keyboard Shortcut
            if (e.KeyCode == Keys.D2)
            {
                WiFiSetup_Click(this, new EventArgs());
            }
            // GSM Setip Keyboard Shortcut
            if (e.KeyCode == Keys.D3)
            {
                GSMSetup_Click(this, new EventArgs());
            }
            // Server Settings Keyboard Shortcut
            if (e.KeyCode == Keys.D4)
            {
                ServSetup_Click(this, new EventArgs());
            }
            // Exit Keyboard Shortcut
            if (e.KeyCode == Keys.D5)
            {
                ExitToOS_Click(this, new EventArgs());
            }
            // About Keyboard Shortcut
            if (e.KeyCode == Keys.D6)
            {
                linkLabel1_Click(this, new EventArgs());
            }
        }
    }
}