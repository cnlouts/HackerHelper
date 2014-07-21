using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace HackerHelper.usercontrols
{
    public partial class googleUrlCapture : UserControl
    {
        public googleUrlCapture()
        {
            InitializeComponent();
            BindCmb();
        }

        private void BindCmb()
        {
            DataTable dt = new DataTable("cmbSource");
            dt.Columns.Add("id", typeof(int));
            DataRow row;
            for (int i = 0; i < 100; i++)
            {
                row = dt.NewRow();
                row["id"] = i;
                dt.Rows.Add(row);
            }
            this.cmbPageNum.DataSource = dt;
            this.cmbPageNum.DisplayMember = "id";
            this.cmbPageNum.ValueMember = "id";
            this.cmbPageNum.SelectedIndex = 0;
        }

        private  void btnSearch_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.cmbPageNum.Text);
            getJson("2");
        }

        private async Task<string> getJson(string pageNum)
        {
            //https://ajax.googleapis.com/ajax/services/search/web?v=1.0&q=cnlouts&rsz=8&start=5
            string result = null;
            string keyword = HttpUtility.UrlEncode(this.txtKeywords.Text);
            string url =
                string.Format("https://ajax.googleapis.com/ajax/services/search/web?v=1.0&q={0}&rsz=8&start={1}",keyword,pageNum);
            using (HttpClient client = new HttpClient())
            {
                Uri referer = new Uri(url);
                client.DefaultRequestHeaders.Add("User-Agent",
                 "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:30.0) Gecko/20100101 Firefox/30.0");
                client.DefaultRequestHeaders.Add("Referer", referer.Scheme + "://" + referer.Host);
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsStringAsync();
                }

            }
            var tmp = JsonConvert.DeserializeObject(result);
            JObject jsonObj = new JObject(result);
            //string responseDetails = (string)jsonObj.SelectToken("responseDetails");
            //JObject cursorJson = (JObject)jsonObj.SelectToken("responseData.cursor");
            //JArray cursorPages = (JArray)jsonObj.SelectToken("responseData.cursor.pages");
            JArray resultsJson = (JArray)jsonObj.SelectToken("responseData.results");
            return result;
        }
    }
}
