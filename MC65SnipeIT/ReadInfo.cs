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
    public partial class ReadInfo : Form
    {
        public ReadInfo(string message)
        {
            InitializeComponent();
            UrlBox.Text= message;

        }

        public class Locations
        {
            public string name { get; set; }
            public int id { get; set; }
        }

        private void ReadInfo_Load(object sender, EventArgs e)
        {

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
                                    string NameOfObject = RootJson["name"].ToString();
                                    string AssetTagObject = RootJson["asset_tag"].ToString();
                                    string LocationArray = RootJson["rtd_location"].ToString();
                                    JObject LocationArrayJson = JObject.Parse(LocationArray);
                                    string LocationName = LocationArrayJson["name"].ToString();
                                    string CategoryArray = RootJson["category"].ToString();
                                    JObject CategoryArrayJson = JObject.Parse(CategoryArray);
                                    string CategoryName = CategoryArrayJson["name"].ToString();
                                    // Custom Fields, Here lies madness!
                                    try 
                                    {
                                        string CustomFieldArray = RootJson["custom_fields"].ToString();
                                        JObject CustomFieldArrayJson = JObject.Parse(CustomFieldArray);
                                        try //Lenght
                                            {
                                                string LengthArray = CustomFieldArrayJson["Length"].ToString();
                                                JObject LenghtArrayJson = JObject.Parse(LengthArray);
                                                string LenghtValue = LenghtArrayJson["value"].ToString();
                                                LengthText.Text = LenghtValue;

                                            }
                                        catch (Exception)
                                        {
                                            LengthText.Text = "\"Not Applicable\"";

                                        }
                                        try //Voltage
                                        {
                                            string VoltageArray = CustomFieldArrayJson["Voltage"].ToString();
                                            JObject VoltageArrayJson = JObject.Parse(VoltageArray);
                                            string VoltageValue = VoltageArrayJson["value"].ToString();
                                            VoltageText.Text = VoltageValue;

                                        }
                                        catch (Exception)
                                        {
                                            VoltageText.Text = "\"Not Applicable\"";

                                        }

                                        try //Amp
                                        {
                                            string AmperageArray = CustomFieldArrayJson["Amperage"].ToString();
                                            JObject AmperageArrayJson = JObject.Parse(AmperageArray);
                                            string AmperageValue = AmperageArrayJson["value"].ToString();
                                            AmpText.Text = AmperageValue;

                                        }
                                        catch (Exception)
                                        {
                                            AmpText.Text = ".";

                                        }

                                        try // Polarity
                                        {
                                            string PolarityArray = CustomFieldArrayJson["Polarity Center"].ToString();
                                            JObject PolarityArrayJson = JObject.Parse(PolarityArray);
                                            string PolarityValue = PolarityArrayJson["value"].ToString();
                                            PolarityText.Text = PolarityValue;

                                        }
                                        catch (Exception)
                                        {
                                            PolarityText.Text = "\"Not Applicable\"";

                                        }
                                        try // Size
                                        {
                                            string SizeArray = CustomFieldArrayJson["DC Jack Diameter"].ToString();
                                            JObject SizeArrayJson = JObject.Parse(SizeArray);
                                            string SizeValue = SizeArrayJson["value"].ToString();
                                            SizeText.Text = SizeValue;

                                        }
                                        catch (Exception)
                                        {
                                            SizeText.Text = "\"Not Applicable\"";

                                        }
                                        try // Mac
                                        {
                                            string MACArray = CustomFieldArrayJson["MAC Address"].ToString();
                                            JObject MacArrayJson = JObject.Parse(MACArray);
                                            string MacValue = MacArrayJson["value"].ToString();
                                            MacText.Text = MacValue;

                                        }
                                        catch (Exception)
                                        {
                                            MacText.Text = "\"Not Applicable\"";

                                        }

                                    }
                                    catch (Exception)
                                    {//silentfail
                                        LengthText.Text = "\"Not Applicable\"";
                                        PolarityText.Text = "\"Not Applicable\"";
                                        VoltageText.Text = "\"Not Applicable\"";
                                        SizeText.Text = "\"Not Applicable\"";
                                        MacText.Text = "\"Not Applicable\"";
                                        AmpText.Text = ".";
                                    }

                                    // Length Custom Array Test

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

        private void UrlBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                GetBtn_Click(this, new EventArgs());
                UrlBox.SelectionStart = 0;
                UrlBox.SelectionLength = UrlBox.Text.Length;
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        }
    }
