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

namespace MC65SnipeIT
{
    public partial class ServerSettings : Form
    {
        public ServerSettings()
        {
            InitializeComponent();
            ServAddress.Text = MobileConfiguration.Settings["ServerAddress"];
            Token.Text = MobileConfiguration.Settings["ServerKey"];
            //if (MobileConfiguration.Settings["Debug"] == "TRUE")
            //{
            //    checkBox1.Checked = true;
            //} 
            //else
            //{
            //    // Nothing
            //}
            //if (MobileConfiguration.Settings["ReadOnly"] == "TRUE")
            //{
            //    checkBox2.Checked = true;
            //}
            //else
            //{
            //    // Nothing
            //}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Token/Key Files (*.txt)|*.txt";
            openFileDialog1.ShowDialog();
            StreamReader file = new StreamReader(openFileDialog1.FileName);
            string apikey;
            apikey = file.ReadLine();
            Token.Text = apikey;
        }

        private void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {

        }
    }

    public static class MobileConfiguration
    {
        public static NameValueCollection Settings;

        static MobileConfiguration()
        {
            string appPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string configFile = Path.Combine(appPath, "App.config");

            if (!File.Exists(configFile))
            {
                throw new FileNotFoundException(string.Format("Application configuration file '{0}' not found.", configFile));
            }

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(configFile);
            XmlNodeList nodeList = xmlDocument.GetElementsByTagName("appSettings");
            Settings = new NameValueCollection();

            foreach (XmlNode node in nodeList)
            {
                foreach (XmlNode key in node.ChildNodes)
                {
                    Settings.Add(key.Attributes["key"].Value, key.Attributes["value"].Value);
                }
            }
        }
    }
}