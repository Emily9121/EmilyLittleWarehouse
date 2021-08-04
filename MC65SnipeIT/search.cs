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

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MC65SnipeIT
{
    public partial class search : Form
    {
        public search()
        {
            InitializeComponent();
        }

        public class Locations
        {
            public string name { get; set; }
            public int id { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void searchBtn_Click(object sender, EventArgs e)
        {
            try
            {
                // Search Query
                string WEBSERVICE_URL = MobileConfiguration.Settings["ServerAddress"] + "/api/v1/hardware?limit=50&offset=0&search=" + searchBox.Text;
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

                        ResultBox.DataSource = location;
                        ResultBox.DisplayMember = "name";
                        ResultBox.ValueMember = "id";

                        if (jsonResponse == "{\"total\":0,\"rows\":[]}")
                        {
                            ItemID.Text = "No result found";
                        }
                        else { }

                    }
                }
            }
            catch (Exception)
            { MessageBox.Show("Application couldn't download the location list, Check your network connection and/or your API key"); }
        }

        private void ResultBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ItemID.Text = "ID: " + ResultBox.SelectedValue.ToString();
            string IDtoRead = ResultBox.SelectedValue.ToString();
        }

        private void searchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                searchBtn_Click(this, new EventArgs());
                searchBox.SelectionStart = 0;
                searchBox.SelectionLength = searchBox.Text.Length;
            }

        }

        private void GetInfoBtn_Click(object sender, EventArgs e)
        {
            ReadInfo2 ri2 = new ReadInfo2(ResultBox.SelectedValue.ToString(),"Back to search");
            ri2.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string IDofObject = ResultBox.SelectedValue.ToString();
                string WEBSERVICE_URL = MobileConfiguration.Settings["ServerAddress"] + "/api/v1/hardware/" + IDofObject;
                var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
                webRequest.Method = "GET";
                webRequest.Timeout = 20000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Authorization", "Bearer " + MobileConfiguration.Settings["ServerKey"]);
                using (System.IO.Stream s = webRequest.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))

                        try
                        {
                            {
                                var jsonResponse = sr.ReadToEnd();
                                JObject RootJson = JObject.Parse(jsonResponse);
                                string NameOfObjectToClean = RootJson["name"].ToString();
                                string NameOfObjectToClean2 = NameOfObjectToClean.Replace("\"", "");
                                string NameOfObject = NameOfObjectToClean2.Replace("quot;", "\"");

                                string AssetTagObjectToClean = RootJson["asset_tag"].ToString();
                                string AssetTagObject = AssetTagObjectToClean.Replace("\"", "");

                                string LocationArray = RootJson["rtd_location"].ToString();
                                JObject LocationArrayJson = JObject.Parse(LocationArray);
                                string LocationNameToClean = LocationArrayJson["name"].ToString();
                                string LocationNameToClean2 = LocationNameToClean.Replace("\"", "");
                                string LocationName = LocationNameToClean2.Replace("quot;", "\"");


                                using (StreamWriter sw = File.AppendText(Path.Combine("\\" + MobileConfiguration.Settings["Storage"] + "\\", "ELW-List.txt")))
                                {

                                    sw.WriteLine(LocationName + " - " + AssetTagObject + ": " + NameOfObject);
                                    ItemID.Text = ResultBox.SelectedValue.ToString() + " Added to list!";
                                }

                            }
                        }
                        catch (Exception)
                        { MessageBox.Show("I just don't know what went wrong!"); }
                }
            }
            catch (Exception)
            { MessageBox.Show("Couldn't contact the server"); }
        }
        
    }
}
