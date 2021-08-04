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

namespace MC65SnipeIT
{
    public partial class ReadInfo2 : Form
    {
        public ReadInfo2(string message, string exitbtn)
        {
            InitializeComponent();
            UrlBox.Text = message;
            exit.Text = exitbtn;
        }


        public class CustomFields
        {
            public string ItemName { get; set; }
            public string value { get; set; }
        }

        private void GetBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string ToClean = MobileConfiguration.Settings["ServerAddress"] + "/hardware/";
                string IDofObject = UrlBox.Text.Replace(MobileConfiguration.Settings["ServerAddress"] + "/hardware/", "");
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

                                string CategoryArray = RootJson["category"].ToString();
                                JObject CategoryArrayJson = JObject.Parse(CategoryArray);
                                string CategoryNameToClean = CategoryArrayJson["name"].ToString();
                                string CategoryNameToClean2 = CategoryNameToClean.Replace("\"", "");
                                string CategoryName = CategoryNameToClean2.Replace("quot;", "\"");

                                // Custom Fields, Here lies madness!
                                try
                                {
                                    List<string> results = new List<string>();
                                    string CustomFieldArray = RootJson["custom_fields"].ToString();
                                    JObject CustomFieldArrayJson = JObject.Parse(CustomFieldArray);

                                    var data = JsonConvert.DeserializeObject<Dictionary<string, CustomFields>>(CustomFieldArray);
                                    foreach (var item in data)
                                    {
                                        string CustomArray = CustomFieldArrayJson[item.Key].ToString();
                                        JObject CustomArrayJson = JObject.Parse(CustomArray);
                                        string Item = item.Key + ": " + CustomArrayJson["value"].ToString();
                                        Item = Item.Replace("\"", "");
                                        results.Add(Item);
                                    }

                                    ResultBox.DataSource = results;
                                    ResultBox.DisplayMember = "name";

                                    if (jsonResponse == "{\"total\":0,\"rows\":[]}")
                                    {
                                        // do nothing
                                    }
                                    else { }


                                }
                                catch (Exception)
                                {
                                    List<string> noresults = new List<string>();
                                    noresults.Add("No Custom Fields for this item!");
                                    ResultBox.DataSource = noresults;
                                    ResultBox.DisplayMember = "name";
                                }

                                ItemName.Text = NameOfObject;
                                idtag.Text = AssetTagObject;
                                ItemLocation.Text = LocationName;
                                CategoryText.Text = CategoryName;

                            }
                        }
                        catch (Exception)
                        { MessageBox.Show("Are you sure this item exist?"); }
                }
            }
            catch (Exception)
            { MessageBox.Show("Couldn't contact the server"); }
        }
        

        private void ResultBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void exit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UrlBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                GetBtn_Click(this, new EventArgs());
                UrlBox.SelectionStart = 0;
                UrlBox.SelectionLength = UrlBox.Text.Length;
            }
        }

        private void AddToLstBtn_Click(object sender, EventArgs e)
        {
            try
            {
                string IDofObject = UrlBox.Text;
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
                                    LstStatus.Text = NameOfObject + " Added to list!";
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
