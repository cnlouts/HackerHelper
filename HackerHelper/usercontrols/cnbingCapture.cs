using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HackerHelper.usercontrols
{
    public partial class cnbingCapture : UserControl
    {
        public cnbingCapture()
        {
            InitializeComponent();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string keywords = this.txtKeywords.Text;
            await HttpAsync(keywords);
        }

        private async Task HttpAsync(string keywords, int counter = 100)
        {
            string baseUrl = string.Format("http://cn.bing.com/search?q={0}&go=&count=50&FORM=QBHL&qs=n&first=",keywords);
            //进制自动重定向
            HttpClientHandler handler = new HttpClientHandler(){AllowAutoRedirect = false};
            using (HttpClient client = new HttpClient(handler))
            {
                string result = "";
                //配置默认的HTTP header 头
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6");
                client.DefaultRequestHeaders.Add("Cookie","SRCHHPGUSR=ADLT=DEMOTE&NRSLT=50");
                try
                {
                    string[] tmp = new string[counter/50];
                    for (int i = 0; i < counter/50; i++)
                    {
                        HttpResponseMessage response = await client.GetAsync(baseUrl + (i*50).ToString());
                        if (response.IsSuccessStatusCode)
                        {
                            string _tmp = await response.Content.ReadAsStringAsync();
                            Regex r = new Regex(@"<cite>(.*?)</cite>");
                            MatchCollection collection = r.Matches(_tmp);
                            if (collection.Count > 0)
                            {
                                for (int j = 0; j < collection.Count; j++)
                                {
                                    this.txtResult.Text += collection[j].ToString() + Environment.NewLine;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
    }
}
