using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Collections.Specialized;
using System.Diagnostics;

namespace MC65SnipeIT
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
            if (MobileConfiguration.Settings["NyaPrint"] == "TRUE")
            {
                GenStickers.Text = "6 - Generate/Print Stickers";
            }
            else
            {
                //change nothing
            }

            // If EarlyConnect is enabled, We do a request to the distant server IMMEDIATELY - The content of the request don't really matter, but we're caching things on the way
            // Can help with slower connection like cellular or if we need time to dial.

            if (MobileConfiguration.Settings["EarlyConnect"] == "TRUE")
            {
                try
                {
                    string WEBSERVICE_URL = MobileConfiguration.Settings["ServerAddress"] + "/api/v1/hardware/" + "1";
                    var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
                    webRequest.Method = "GET";
                    webRequest.Timeout = 30000;
                    webRequest.ContentType = "application/json";
                    webRequest.Headers.Add("Authorization", "Bearer " + MobileConfiguration.Settings["ServerKey"]);
                    using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                        {
                            var jsonResponse = sr.ReadToEnd();
                        }
                    }
                }
                catch (Exception)
                { MessageBox.Show("Connection timed out - Make sure you're properly connected before continuing!"); }
            }
        }

        private void Exitbtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ReadInfo2 readinfo2 = new ReadInfo2(" ","Back To Main Menu");
            readinfo2.ShowDialog();
        
        }

        private void MainMenu_KeyDown(object sender, KeyEventArgs e)
        {
            // Read Info Keyboard Shortcut
            if (e.KeyCode == Keys.D1)
            {
                button1_Click(this, new EventArgs());
            }
            // Exit Keyboard Shortcut
            if (e.KeyCode == Keys.D9)
            {
                Exitbtn_Click(this, new EventArgs());
            }
            // Add  Item Keyboard Shortcut
            if (e.KeyCode == Keys.D2)
            {
                button2_Click(this, new EventArgs());
            }
            // Move Item Keyboard Shortcut
            if (e.KeyCode == Keys.D3)
            {
                button3_Click(this, new EventArgs());
            }
            // Batch Move Item Keyboard Shortcut
            if (e.KeyCode == Keys.D4)
            {
                button4_Click(this, new EventArgs());
            }
            // Add Location Keyboard Shortcut
            if (e.KeyCode == Keys.D5)
            {
                button5_Click(this, new EventArgs());
            }
            // Generate (And print if config) Keyboard Shortcut
            if (e.KeyCode == Keys.D6)
            {
                GenStickers_Click(this, new EventArgs());
            }
            // Generate (And print if config) Keyboard Shortcut
            if (e.KeyCode == Keys.D7)
            {
                SearchItemBtn_Click(this, new EventArgs());
            }
            // Open Item List in Word Mobile Keyboard Shortcut
            if (e.KeyCode == Keys.D8)
            {
                ListBtn_Click(this, new EventArgs());
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MoveItem moveitem = new MoveItem();
            moveitem.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BatchMove batchmove = new BatchMove();
            batchmove.ShowDialog();
        }

        private void GenStickers_Click(object sender, EventArgs e)
        {
            Sticker sticker = new Sticker();
            sticker.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddLoc addloc = new AddLoc();
            addloc.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddItem additem = new AddItem();
            additem.ShowDialog();
        }

        private void SearchItemBtn_Click(object sender, EventArgs e)
        {
            search searchdlg = new search();
            searchdlg.ShowDialog();
        }

        private void ListBtn_Click(object sender, EventArgs e)
        {
            Process.Start((Path.Combine("\\" + MobileConfiguration.Settings["Storage"] + "\\", "ELW-List.txt")),"");
        }
    }
}
