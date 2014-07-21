using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;

namespace HackerHelper.usercontrols
{
    public partial class WebDirForce : UserControl
    {
        private string _dictPath;
        private List<string> _list;
        public WebDirForce()
        {
            InitializeComponent();
        }

        private void btnDict_Click(object sender, EventArgs e)
        {
            initDict();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (_list != null)
            {
                initDataGridView();
            }
            else
            {
                MessageBox.Show("请选择字典！");

            }
        }

        private static async Task<string> RunAsync(string url)
        {
            string statusCode = "";
            var httpClientHandler = new HttpClientHandler { AllowAutoRedirect = false };
            using (var client = new HttpClient(httpClientHandler))
            {
                var referer = new Uri(url);
                client.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:30.0) Gecko/20100101 Firefox/30.0");
                client.DefaultRequestHeaders.Add("Referer", referer.Scheme + "://" + referer.Host);
                try
                {
                    var response = await client.GetAsync(url);
                    statusCode = response.StatusCode.ToString();
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return statusCode;
        }

        private async void initDataGridView()
        {
            this.progressBar1.Maximum = _list.Count;
            this.progressBar1.Value = 0;
            this.progressBar1.Step = 1;
            string baseUrl = this.txtTargetUrl.Text.Trim();
            foreach (var item in _list)
            {
                string status = await RunAsync(baseUrl + item);

                int index = dataGridView.Rows.Add();
                this.dataGridView.Rows[index].Cells[0].Value = index;
                this.dataGridView.Rows[index].Cells[1].Value = item;
                this.dataGridView.Rows[index].Cells[2].Value = status;
                this.progressBar1.Value += 1;
            }
        }

        private async void initDict()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                _dictPath = fileDialog.FileName;
                this.txtDict.Text = _dictPath;
                _list = await ReadDict(_dictPath);
            }
            else
            {
                return;
            }
        }
        private async Task<List<string>> ReadDict(string dictPath)
        {
            List<string> list = new List<string>();
            using (var sr = new StreamReader(dictPath))
            {
                string line;
                while ((line = await sr.ReadLineAsync()) != null)
                {
                    list.Add(line);
                }
            }
            return list;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this._list = null;
            this.txtDict.Text = "";
            this.txtTargetUrl.Text = "";
            this.progressBar1.Value = 0;
            this.dataGridView.Rows.Clear();
        }

        private void btnOutput_Click(object sender, EventArgs e)
        {
            saveFile();
            MessageBox.Show("导出完毕");
        }

        private async void saveFile()
        {
            if (this.dataGridView.Rows.Count <= 0) return;
            string savePath = "";
            SaveFileDialog saveFile = new SaveFileDialog();
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                savePath = saveFile.FileName;
            }
            StringBuilder sbBuilder = new StringBuilder();
            for (int i = 0; i < this.dataGridView.Rows.Count; i++)
            {
                sbBuilder.Append(string.Format("id:{0};\tstatus:{2};\tpath:{1}{3}", this.dataGridView.Rows[i].Cells[0].Value, this.dataGridView.Rows[i].Cells[1].Value, this.dataGridView.Rows[i].Cells[2].Value, Environment.NewLine));
            }
            using (StreamWriter sw = new StreamWriter(savePath))
            {
                await sw.WriteLineAsync(sbBuilder.ToString());
            }

        }
    }
}
