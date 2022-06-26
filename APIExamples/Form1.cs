using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace APIExamples
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var APItoken = "NNSXS.5B7SXMCWWX3VLZYCG3L3ODPPSZTIAB6Q2XDAF5Q.SG6FQ3UXGMJTXEVZXLFBCKPHLLR2A23524ZHOYKIPLDP434X3UEA";
                var request = (HttpWebRequest)WebRequest.Create("https://eu1.cloud.thethings.network/api/v3/applications/" + "kurtstestapplicationca" + "/devices?page=1&limit=5&field_mask=name");
                request.Headers.Add(HttpRequestHeader.Authorization, string.Concat("Bearer ", APItoken));
                request.Accept = "application/json";
                request.UserAgent = "kurtk"; //user agent is required https://developer.github.com/v3/#user-agent-required
                using (var response = request.GetResponse())
                {
                    WebHeaderCollection header = response.Headers;

                    var encoding = ASCIIEncoding.ASCII;
                    using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
                    {
                        string responseText = reader.ReadToEnd();
                        richTextBox1.Clear();
                        richTextBox1.Text = responseText;
                    }
                }
            }
            catch { }
        }
    }
}
