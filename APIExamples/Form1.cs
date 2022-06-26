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
using System.IO;

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
                //  API token is generated at the appliaction, I think a similar example could be made using OATH
                var APItoken = "NNSXS.5B7SXMCWWX3VLZYCG3L3ODPPSZTIAB6Q2XDAF5Q.SG6FQ3UXGMJTXEVZXLFBCKPHLLR2A23524ZHOYKIPLDP434X3UEA";

                string application = "kurtstestapplicationca";
                string enddevice = "eui-000098761234ffff";

                //  Schedule Uplink Message API
                var request = (HttpWebRequest)WebRequest.Create("https://nam1.cloud.thethings.network/api/v3/as/applications/" + application + "/devices/" + enddevice + "/down/replace");
                //  Get Device by Name API
                //var request = (HttpWebRequest)WebRequest.Create("https://eu1.cloud.thethings.network/api/v3/applications/" + "kurtstestapplicationca" + "/devices?page=1&limit=5&field_mask=name");
                
                request.Method = "POST";
                request.ContentType = "application/json; charset=utf-8";
                request.Headers.Add(HttpRequestHeader.Authorization, string.Concat("Bearer ", APItoken));
                request.Accept = "application/json";
                request.UserAgent = "kurtk"; //user agent is required https://developer.github.com/v3/#user-agent-required
                using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                {
                    byte[] data = new byte[] {0 , 1 , 2 , 3, 4};
                    string payload = Convert.ToBase64String(data);
                    string json = "{ \"downlinks\":[{ \"frm_payload\":\"" + payload + "\",\"confirmed\":false,\"f_port\":1}]}";

                    streamWriter.Write(json);
                    streamWriter.Flush();
                }

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
