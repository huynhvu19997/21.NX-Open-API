
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;


namespace basic
{

    /* logic viết
     Class ViewContentHtml:

        Thuộc tính: Url, Timer, LastHtml.
        Sự kiện: OnDataChanged → trả về danh sách SectionInfo khi có thay đổi.
        Phương thức:

        StartAutoUpdate(intervalMs) → bắt đầu cập nhật định kỳ.
        StopAutoUpdate() → dừng cập nhật.
        FetchHtml() → lấy HTML từ URL.
        ParseHtmlAuto() → phân tích HTML thành danh sách SectionInfo.
         */


    public class SectionInfo
    {
        public string Title { get; set; }
        public List<string> Contents { get; set; } = new List<string>();
    }

    class ViewContentHtml_V1
    {


        private readonly string _url;
        private string _lastHtml;
        private System.Windows.Forms.Timer _timer;

        public event Action<List<SectionInfo>> OnDataChanged;

        public ViewContentHtml_V1(string url)
        {
            _url = url;
        }

        private async Task<string> FetchHtmlAsync()
        {
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(_url);
        }

        public List<SectionInfo> ParseHtmlAuto(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var bodyNode = doc.DocumentNode.SelectSingleNode("//body");
            if (bodyNode == null) return new List<SectionInfo>();

            var sections = new List<SectionInfo>();
            SectionInfo currentSection = null;

            foreach (var node in bodyNode.ChildNodes)
            {
                if (node.Name == "b" || node.Name.StartsWith("h"))
                {
                    if (currentSection != null)
                        sections.Add(currentSection);

                    currentSection = new SectionInfo { Title = node.InnerText.Trim() };
                }
                else if (node.Name == "p")
                {
                    if (currentSection == null)
                        currentSection = new SectionInfo { Title = "Thông tin chung" };

                    currentSection.Contents.Add(node.InnerText.Trim());
                }
            }

            if (currentSection != null)
                sections.Add(currentSection);

            return sections;
        }

        public void StartAutoUpdate(int intervalMs = 10000)
        {
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = intervalMs;
            _timer.Tick += async (s, e) => await CheckForUpdate();
            _timer.Start();
        }

        public void StopAutoUpdate()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }
        }

        private async Task CheckForUpdate()
        {
            var newHtml = await FetchHtmlAsync();
            if (_lastHtml != newHtml)
            {
                _lastHtml = newHtml;
                var sections = ParseHtmlAuto(newHtml);
                OnDataChanged?.Invoke(sections);
            }
        }


    }
}


/* cách sử dụng trong form 
 * 
 * private ViewContentHtml viewHtml;

private async void Form1_Load(object sender, EventArgs e)
{
    viewHtml = new ViewContentHtml("http://banquyen.htmlptld.vn");

    // Đăng ký sự kiện khi dữ liệu thay đổi
    viewHtml.OnDataChanged += sections =>
    {
        txtResult.Clear();
        foreach (var section in sections)
        {
            txtResult.AppendText($"== {section.Title} ==\r\n");
            foreach (var content in section.Contents)
            {
                txtResult.AppendText(content + "\r\n");
            }
            txtResult.AppendText("\r\n");
        }
    };

    // Lấy dữ liệu ban đầu
    var html = await viewHtml.FetchHtmlAsync();
    var data = viewHtml.ParseHtmlAuto(html);
    viewHtml.OnDataChanged?.Invoke(data);

    // Bắt đầu tự động cập nhật mỗi 10 giây
    viewHtml.StartAutoUpdate(10000);
}
 * /
 */