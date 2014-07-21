using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
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

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(this.cmbPageNum.Text);
            await getJson(this.cmbPageNum.Text);
        }

        private async Task getJson(string pageNum)
        {
            //https://ajax.googleapis.com/ajax/services/search/web?v=1.0&q=cnlouts&rsz=8&start=5
            string result = "";
            string keyword = HttpUtility.UrlEncode(this.txtKeywords.Text);
            this.progressBar1.Maximum = 8*Convert.ToInt32(pageNum);
            this.progressBar1.Value = 0;
            this.progressBar1.Step = 1;
            for (int j = 1; j <=Convert.ToInt32(pageNum); j++)
            {
                string url =
    string.Format("https://ajax.googleapis.com/ajax/services/search/web?v=1.0&q={0}&rsz=8&start={1}", keyword, j);
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
                JObject jsonObj = JObject.Parse(result);
                //string responseDetails = (string)jsonObj.SelectToken("responseDetails");
                //JObject cursorJson = (JObject)jsonObj.SelectToken("responseData.cursor");
                //JArray cursorPages = (JArray)jsonObj.SelectToken("responseData.cursor.pages");
                JArray resultsJson = (JArray)jsonObj.SelectToken("responseData.results");
                for (int i = 0; i < resultsJson.Count; i++)
                {
                    //resultsJson[i]["url"];
                    int index = this.dataGridView1.Rows.Add();
                    this.dataGridView1.Rows[index].Cells[0].Value = index;
                    this.dataGridView1.Rows[index].Cells[1].Value = resultsJson[i]["url"];
                    this.dataGridView1.Rows[index].Cells[2].Value = resultsJson[i]["content"];
                    this.progressBar1.Value += this.progressBar1.Step;
                }
            }
           this.progressBar1.Value =  8 * Convert.ToInt32(pageNum);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Rows.Clear();
            this.progressBar1.Value = 0;
        }

        private async void btnOutput_Click(object sender, EventArgs e)
        {
            await saveResult();
        }

        private async Task saveResult()
        {
            if (this.dataGridView1.Rows.Count <= 0) return;
            string savePath = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                savePath = saveFile.FileName;
            }
            //拼接大量字符窜使用stringBuilder
            StringBuilder sbBuilder = new StringBuilder();
            for (int i = 0; i < this.dataGridView1.Rows.Count; i++)
            {
                sbBuilder.Append(string.Format("{1}{2}", this.dataGridView1.Rows[i].Cells[1].Value,  Environment.NewLine));
            }
            //使用异步写入文件
            using (StreamWriter sw = new StreamWriter(savePath))
            {
                await sw.WriteLineAsync(sbBuilder.ToString());
            }

        }
    }
}
