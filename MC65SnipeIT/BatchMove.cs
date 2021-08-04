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
    public partial class BatchMove : Form
    {
        public BatchMove()
        {
            InitializeComponent();
            try
            {

                string WEBSERVICE_URL = MobileConfiguration.Settings["ServerAddress"] + "/api/v1/locations?limit=300&offset=0&sort=created_at";
                var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
                webRequest.Method = "GET";
                webRequest.Timeout = 20000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Authorization", "Bearer " + MobileConfiguration.Settings["ServerKey"]);
                using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        var jsonResponse = sr.ReadToEnd();
                        JObject jObj = JObject.Parse(jsonResponse);
                        string rows = jObj["rows"].ToString();
                        List<Locations> location = JsonConvert.DeserializeObject<List<Locations>>(rows);
                        locationbox.DataSource = location;
                        locationbox.DisplayMember = "name";
                        locationbox.ValueMember = "id";

                    }
                }
            }
            catch (Exception)
            { MessageBox.Show("Application couldn't download the location list, Check your network connection and/or your API key"); }
        
        }

        public class Locations
        {
            public string name { get; set; }
            public int id { get; set; }
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
                string patchData = "{\"rtd_location_id\":" + locationbox.SelectedValue + "}";
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
                                DebugText = IDofObject + " Moved to Loc " + locationbox.Text;
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
                MoveBtn_Click(this, new EventArgs());
                UrlBox.SelectionStart = 0;
                UrlBox.SelectionLength = UrlBox.Text.Length;
            }

        }

        private void ExitBnt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void locationbox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UrlBox.Focus();
        }
    }
}
