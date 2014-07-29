using System;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HackerHelper.usercontrols
{
    public partial class CnbingCapture : UserControl
    {

        #region 构造函数
        public CnbingCapture()
        {
            InitializeComponent();
            progressBar1.Maximum = 254;
            progressBar1.Step = 1;
            progressBar1.Value = 0;
        }
        #endregion

        #region 事件
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            btnSearch.Enabled = false;
            if (cmbchoice.SelectedItem.ToString() == "单IP")
            {
                var keywords = txtKeywords.Text;
                await HttpAsync(keywords);
            }
            else
            {
                var ip = txtKeywords.Text.Trim().Substring(3);
                var ipThree = ip.Substring(0, ip.LastIndexOf('.') + 1);
                foreach (var item in Enumerable.Range(1, 254))
                {
                    progressBar1.Value += progressBar1.Step;
                    await HttpAsync(string.Format("ip:{0}{1}", ipThree, item));
                }

            }
            btnSearch.Enabled = true;
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 使用异步返回 bing借口搜索结果
        /// </summary>
        /// <param name="keywords">格式ip:xxx.xxx.xxx.xxx</param>
        /// <param name="counter">默认搜索的条数100</param>
        /// <returns></returns>
        private async Task HttpAsync(string keywords, int counter = 100)
        {
            txtResult.Text += keywords.Substring(3) + Environment.NewLine;
            //bing 搜索的接口
            var baseUrl = string.Format("http://cn.bing.com/search?q={0}&go=&count=50&FORM=QBHL&qs=n&first=", keywords);
            //禁止自动重定向 302
            var handler = new HttpClientHandler { AllowAutoRedirect = false };
            using (var client = new HttpClient(handler))
            {
                //配置默认的HTTP header 头
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)");
                client.DefaultRequestHeaders.Add("Accept-Language", "zh-CN,zh;q=0.8,en;q=0.6");
                client.DefaultRequestHeaders.Add("Cookie", "SRCHHPGUSR=ADLT=DEMOTE&NRSLT=50");
                try
                {
                    for (var i = 0; i < counter / 50; i++)
                    {
                        var response = await client.GetAsync(baseUrl + (i * 50));
                        //如果网络访问异常 跳出 或则 跑完counter都可以
                        if (!response.IsSuccessStatusCode) continue;
                        var tmp = await response.Content.ReadAsStringAsync();
                        if (tmp == null) throw new ArgumentNullException(keywords);
                        //对返回的结果进行处理
                        var r = new Regex(@"<cite>(.*?)</cite>");
                        var collection = r.Matches(tmp);
                        //判断是会否 有匹配记录
                        if (collection.Count >= 0)
                        {
                            for (var j = 0; j < collection.Count; j++)
                            {
                                var startIndex = collection[j].ToString().IndexOf('>');
                                var endIndex = collection[j].ToString().LastIndexOf('<');
                                var result = collection[j].ToString().Substring(startIndex + 1, endIndex - startIndex - 1);
                                if (!txtResult.Text.Contains(result))
                                {
                                    txtResult.Text += string.Format("   {0}{1}", result, Environment.NewLine);
                                }

                            }
                        }
                        else
                        {
                            txtResult.Text += string.Format("   {0}{1}", "None", Environment.NewLine);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtResult.Text = string.Empty;
            progressBar1.Value = 0;
            btnSearch.Enabled = true;
        }
    }
}
