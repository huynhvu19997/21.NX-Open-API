using System;
using System.Linq;
using HtmlAgilityPack;

namespace basic
{
    public class LicenseInfo
    {
        public string DesignTitle { get; set; }
        public string DesignTotal { get; set; }
        public string DesignUserInfo { get; set; }

        public string CamTitle { get; set; }
        public string CamTotal { get; set; }
        public string CamUserInfo { get; set; }

        public string ZwCadTitle { get; set; }
        public string ZwCadTotal { get; set; }
        public string ZwCadUserInfo { get; set; }
    }

    public class ViewContentHtml
    {
        /// <summary>
        /// Hàm này nhận vào chuỗi HTML và trả về đối tượng LicenseInfo
        /// </summary>
        /// <param name="html">Chuỗi HTML cần phân tích</param>
        /// <returns>LicenseInfo chứa các phần dữ liệu đã tách</returns>
        public LicenseInfo ParseHtml(string html)
        {
            // Tạo đối tượng HtmlDocument để phân tích HTML
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Lấy tất cả các thẻ <p> và <b> trong body
            var nodes = doc.DocumentNode.SelectNodes("//body//p|//body//b");

            // Nếu không tìm thấy gì, trả về null
            if (nodes == null) return null;

            // Tạo đối tượng LicenseInfo để lưu kết quả
            LicenseInfo info = new LicenseInfo();

            // Ghi chú: Dùng LINQ để tìm node chứa từ khóa cụ thể
            // License Thiết kế
            info.DesignTitle = nodes.FirstOrDefault(n => n.InnerText.Contains("License Thiết kế"))?.InnerText.Trim();
            info.DesignTotal = nodes.FirstOrDefault(n => n.InnerText.Contains("Tổng số license: 2"))?.InnerText.Trim();
            info.DesignUserInfo = nodes.FirstOrDefault(n => n.InnerText.Contains("Chưa có người dùng"))?.InnerText.Trim();

            // License CAM
            info.CamTitle = nodes.FirstOrDefault(n => n.InnerText.Contains("License CAM"))?.InnerText.Trim();
            info.CamTotal = nodes.FirstOrDefault(n => n.InnerText.Contains("Tổng số license: 1"))?.InnerText.Trim();
            info.CamUserInfo = nodes.FirstOrDefault(n => n.InnerText.Contains("Chưa có người dùng"))?.InnerText.Trim();

            // License ZWCAD
            info.ZwCadTitle = nodes.FirstOrDefault(n => n.InnerText.Contains("License ZWCAD"))?.InnerText.Trim();
            info.ZwCadTotal = nodes.FirstOrDefault(n => n.InnerText.Contains("Tổng số license: 10"))?.InnerText.Trim();
            info.ZwCadUserInfo = nodes.LastOrDefault(n => n.InnerText.Contains("đang sử dụng"))?.InnerText.Trim();

            return info;
        }
    }
}


// Cách dùng
//var parser = new ViewContentHtml();
//string html = "<html>...nội dung từ web...</html>";
//LicenseInfo info = parser.ParseHtml(html);

//Console.WriteLine(info.DesignTitle);
//Console.WriteLine(info.DesignTotal);
//Console.WriteLine(info.DesignUserInfo);


/* ví dụ
 string html = @"
<html>
<head><title>Thông tin License</title></head>
<body>
    <h1>Thông tin License</h1>
    <b>License Thiết kế:</b>
    <p>Tổng số license: 2</p>
    <p>Tổng số người dùng: 0</p>
    <b>Thông tin người dùng:</b>
    <p>Chưa có người dùng</p>

    <b>License CAM:</b>
    <p>Tổng số license: 1</p>
    <p>Tổng số người dùng: 0</p>
    <b>Thông tin người dùng:</b>
    <p>Chưa có người dùng</p>

    <b>License ZWCAD:</b>
    <p>Tổng số license: 10</p>
    <p>Tổng số người dùng: 1/9</p>
    <b>Thông tin người dùng:</b>
    <p>hoi.hm0303 đang sử dụng trên máy MrNKhanh-TK, bắt đầu lúc Fri 9/27 8:25</p>
</body>
</html>";   
     
     */


/* cách dùng với hàm parseHtml
 * 
var parser = new ViewContentHtml();
LicenseInfo info = parser.ParseHtml(html);

Console.WriteLine("Thiết kế:");
Console.WriteLine(info.DesignTitle);
Console.WriteLine(info.DesignTotal);
Console.WriteLine(info.DesignUserInfo);

Console.WriteLine("\nCAM:");
Console.WriteLine(info.CamTitle);
Console.WriteLine(info.CamTotal);
Console.WriteLine(info.CamUserInfo);

Console.WriteLine("\nZWCAD:");
Console.WriteLine(info.ZwCadTitle);
Console.WriteLine(info.ZwCadTotal);

 */
