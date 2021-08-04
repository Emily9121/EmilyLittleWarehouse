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
    public partial class AddItem : Form
    {
        public AddItem()
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
                        locationBox.DataSource = location;
                        locationBox.DisplayMember = "name";
                        locationBox.ValueMember = "id";

                    }
                }

                // Get status

                string WEBSERVICE2_URL = MobileConfiguration.Settings["ServerAddress"] + "/api/v1/statuslabels";
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
                        statusBox.DataSource = location2;
                        statusBox.DisplayMember = "name";
                        statusBox.ValueMember = "id";

                    }
                }

                // Get companies

                string WEBSERVICE3_URL = MobileConfiguration.Settings["ServerAddress"] + "/api/v1/companies";
                var webRequest3 = System.Net.WebRequest.Create(WEBSERVICE3_URL);
                webRequest3.Method = "GET";
                webRequest3.Timeout = 20000;
                webRequest3.ContentType = "application/json";
                webRequest3.Headers.Add("Authorization", "Bearer " + MobileConfiguration.Settings["ServerKey"]);
                using (System.IO.Stream s = webRequest3.GetResponse().GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(s))
                    {
                        var jsonResponse = sr.ReadToEnd();
                        JObject jObj = JObject.Parse(jsonResponse);
                        string rows = jObj["rows"].ToString();
                        List<Locations> location3 = JsonConvert.DeserializeObject<List<Locations>>(rows);
                        companyBox.DataSource = location3;
                        companyBox.DisplayMember = "name";
                        companyBox.ValueMember = "id";

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

        private void ModelNewBtn_Click(object sender, EventArgs e)
        {

        }

        private void RefreshModelBtn_Click(object sender, EventArgs e)
        {

        }

        private void ExitBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string WEBSERVICE_URL = MobileConfiguration.Settings["ServerAddress"] + "/api/v1/hardware";
                var webRequest = System.Net.WebRequest.Create(WEBSERVICE_URL);
                webRequest.Method = "POST";
                webRequest.Timeout = 20000;
                webRequest.ContentType = "application/json";
                webRequest.Headers.Add("Authorization", "Bearer " + MobileConfiguration.Settings["ServerKey"]);

                // Here we prepare the form data

                string nameData = "\"name\":" + "\"" + nameBox.Text + "\"";
                string modelData = "\"model_id\":" + "\"" + MobileConfiguration.Settings["ModelID"] + "\"";
                string companyData = "\"company_id\":" + "\"" + companyBox.SelectedValue + "\"";
                string statusData = "\"status_id\":" + "\"" + statusBox.SelectedValue + "\"";
                string locationData = "\"location_id\":" + "\"" + locationBox.SelectedValue + "\"";
                string rtdlocationData = "\"rtd_location_id\":" + "\"" + locationBox.SelectedValue + "\"";
                string notesData = "\"notes\":" + "\"" + noteBox.Text + "\"";
                string allData;
                allData = "{" + nameData + "," + modelData + "," + companyData + "," + statusData + "," + locationData + "," + rtdlocationData + "," + notesData + "}";

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
                            string Payload = jObj2["payload"].ToString();
                            JObject jObj3 = JObject.Parse(Payload);
                            string ID = jObj3["id"].ToString();
                            statusText.Text = status + " : " + "ID:" + ID + " - " + message;
                        }
                        catch (Exception) { MessageBox.Show("Something went very wrong"); }
                    }
                }
            }
            catch (Exception) { MessageBox.Show("Couldn't send the command, check if your connection is up!"); }
        }
    }
}
