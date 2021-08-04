using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Xml;
using System.Collections.Specialized;

namespace MC65SnipeIT
{
    public partial class MoveItem : Form
    {
        public MoveItem()
        {
            InitializeComponent();
        }

        public class Locations
        {
            public string name { get; set; }
            public int id { get; set; }
        }

        private void ExitBnt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MoveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string DebugText;
                DebugText = "Couldn't send the command, check if your connection is up!";
                string ToClean = MobileConfiguration.Settings["ServerAddress"] + "/hardware/";
                string IDofObject = UrlBox.Text.Replace(MobileConfiguration.Settings["ServerAddress"] + "/hardware/", "");
                string WEBSERVICE_URL = MobileConfiguration.Settings["ServerAddress"] + "/api/v1/hardware/" + IDofObject;
                var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
                webRequest.Method = "PATCH";
                webRequest.Timeout = 20000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Authorization", "Bearer " + MobileConfiguration.Settings["ServerKey"]);
                string patchData = "{\"rtd_location_id\":" + locationbox.Text + "}";
                byte[] byteArray = Encoding.UTF8.GetBytes(patchData);
                webRequest.ContentLength = byteArray.Length;
                Stream dataStream = webRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                using (System.IO.Stream s0 = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr0 = new System.IO.StreamReader(s0))
                    {
                        var jsonResponse = sr0.ReadToEnd();
                        try
                        {
                            JObject jObj2 = JObject.Parse(jsonResponse);
                            string status = jObj2["status"].ToString();
                            string message = jObj2["messages"].ToString();

                            if (status == "\"success\"")
                            {
                                DebugText = IDofObject + " Moved to Loc ID " + locationbox.Text;
                                using (StreamWriter sw = File.AppendText(Path.Combine("\\" + MobileConfiguration.Settings["Storage"] + "\\", "ELW-MoveLog.txt")))
                                {
                                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + DebugText);
                                }
                            }

                            else
                            {
                                DebugText = "ERROR: " + IDofObject + " NOT MOVED TO " + locationbox.Text;
                                using (StreamWriter sw = File.AppendText(Path.Combine("\\" + MobileConfiguration.Settings["Storage"] + "\\", "ELW-MoveLog.txt")))
                                {

                                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss") + " - " + DebugText);
                                }
                            }
                        }
                        catch (Exception) { MessageBox.Show("Something went very wrong"); }
                        history.Items.Insert(0, DebugText);
                    }
                }
            }
            catch (Exception) { MessageBox.Show("Couldn't send the command, check if your connection is up!"); }
        }

        private void UrlBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                locationbox.Focus();
                locationbox.SelectionStart = 0;
                locationbox.SelectionLength = locationbox.Text.Length;
            }
        }
        private void locationbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                MoveBtn_Click(this, new EventArgs());
                UrlBox.Focus();
                UrlBox.SelectionStart = 0;
                UrlBox.SelectionLength = UrlBox.Text.Length;
            }
        }
    }
}
