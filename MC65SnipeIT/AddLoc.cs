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
    public partial class AddLoc : Form
    {
        public AddLoc()
        {
            InitializeComponent();
            try
            {
                // Get existing location list

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
                        ParentComboBox.DataSource = location;
                        ParentComboBox.DisplayMember = "name";
                        ParentComboBox.ValueMember = "id";

                    }
                }

                // Get existing user list 

                string WEBSERVICE2_URL = MobileConfiguration.Settings["ServerAddress"] + "/api/v1/users?limit=300&offset=0&sort=created_at&order=asc";
                var webRequest2 = System.Net.WebRequest.Create(WEBSERVICE2_URL);
                webRequest2.Method = "GET";
                webRequest2.Timeout = 20000;
                webRequest2.ContentType = "application/json";
                webRequest2.Headers.Add("Authorization", "Bearer " + MobileConfiguration.Settings["ServerKey"]);
                using (System.IO.Stream s = webRequest2.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        var jsonResponse = sr.ReadToEnd();
                        JObject jObj = JObject.Parse(jsonResponse);
                        string rows = jObj["rows"].ToString();
                        List<Locations> location2 = JsonConvert.DeserializeObject<List<Locations>>(rows);
                        ManagerComboBox.DataSource = location2;
                        ManagerComboBox.DisplayMember = "name";
                        ManagerComboBox.ValueMember = "id";

                    }
                }

                // Autofill
                if (MobileConfiguration.Settings["AutofillAddress"] == "TRUE")
                {
                        AddressBox.Text = MobileConfiguration.Settings["Address"];
                        Address2Box.Text = MobileConfiguration.Settings["Address2"];
                        ZIPBox.Text = MobileConfiguration.Settings["ZipCode"];
                        CityBox.Text = MobileConfiguration.Settings["City"];
                        CountryBox.Text = MobileConfiguration.Settings["Country"];
                        CurrencyBox.Text = MobileConfiguration.Settings["Currency"];
                }
                else { }

            }
            catch (Exception)
            { MessageBox.Show("Application couldn't download the location or user list, Check your network connection and/or your API key"); }
        }

        public class Locations
        {
            public string name { get; set; }
            public int id { get; set; }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string WEBSERVICE_URL = MobileConfiguration.Settings["ServerAddress"] + "/api/v1/locations";
                var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
                webRequest.Method = "POST";
                webRequest.Timeout = 20000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Authorization", "Bearer " + MobileConfiguration.Settings["ServerKey"]);

                // Here we prepare the form data

                string nameData = "\"name\":" + "\"" + NameBox.Text + "\"";
                string managerData = "\"manager_id\":" + "\"" + ManagerComboBox.SelectedValue + "\"";
                string addressData = "\"address\":" + "\""  + AddressBox.Text + "\"";
                string address2Data = "\"address2\":" + "\"" + Address2Box.Text + "\"";
                string zipcodeData = "\"zip\":" + "\"" + ZIPBox.Text + "\"";
                string cityData = "\"city\":" + "\"" + CityBox.Text + "\"";
                string countryData = "\"country\":" + "\"" + CountryBox.Text + "\"";
                string currencyData = "\"currency\":" + "\"" + CurrencyBox.Text + "\"";
                string allData;

                if (ParentCheck.Checked)
                {
                    string parentData = "\"parent_id\":" + "\"" + ParentComboBox.SelectedValue + "\"";
                    allData = "{" + nameData + "," + addressData + "," + address2Data + "," + managerData + "," + countryData + "," + zipcodeData + "," + cityData + "," + parentData + "," + currencyData + "}";
                }
                else
                {
                    allData = "{" + nameData + "," + addressData + "," + address2Data + "," + managerData + "," + countryData + "," + zipcodeData + "," + cityData + "," + currencyData + "}";
                }

                byte[] byteArray = Encoding.UTF8.GetBytes(allData);
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
                            statusText.Text = status + " : " + message;
                        }
                        catch (Exception) { MessageBox.Show("Something went very wrong"); }
                    }
                }
            }
            catch (Exception) { MessageBox.Show("Couldn't send the command, check if your connection is up!"); }
        }

        private void ParentCheck_CheckStateChanged(object sender, EventArgs e)
        {
            if (ParentCheck.Checked)
            {
                ParentComboBox.Enabled = true; 
            }
            else
            {
                ParentComboBox.Enabled = false;
            }
        }

        private void LabelBtn_Click(object sender, EventArgs e)
        {
            StickerLocation stickerloc = new StickerLocation();
            stickerloc.ShowDialog();
        }
    }
}
