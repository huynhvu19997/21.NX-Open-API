using System;
using System.Diagnostics;
using NXOpen;
using NXOpen.Drawings;

namespace basic
{
    public class DrawingAutomation
    {
        private Session theSession;
        public Part workPart;

        public DrawingAutomation()
        {
            theSession = Session.GetSession();
            workPart = theSession.Parts.Work;
        }

        public Part OpenDrawingTemplate(string templatePath)
        {
            PartLoadStatus status;
            theSession.Parts.OpenBaseDisplay(templatePath, out status);
            workPart = theSession.Parts.Work;
            return workPart;
        }

        public DrawingSheet GetFirstSheet()
        {
            var sheets = workPart.DrawingSheets.ToArray();
            if (sheets.Length == 0)
                throw new Exception("Không tìm thấy sheet trong bản vẽ!");
            return sheets[0];
        }

        // Gọi Python tạo base view cho 1 part/assy bất kỳ
        public void CreateBaseView_Python(
            string pythonScriptPath,
            string partOrAssyPath,
            string sheetName,
            string viewName,
            string viewType,
            double x,
            double y,
            double scale)
        {
            string nxPythonExe = @"C:\Program Files\Siemens\NX\NX2401\NXBIN\nxpython.bat";
            string args = $"\"{pythonScriptPath}\" \"{partOrAssyPath}\" \"{sheetName}\" \"{viewName}\" \"{viewType}\" {x} {y} {scale}";

            var psi = new ProcessStartInfo
            {
                FileName = nxPythonExe,
                Arguments = args,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };
            var process = Process.Start(psi);
            string output = process.StandardOutput.ReadToEnd();
            string err = process.StandardError.ReadToEnd();
            process.WaitForExit();

            theSession.ListingWindow.Open();
            theSession.ListingWindow.WriteLine($"Gọi Python tạo base view, output:\n{output}\nError:\n{err}");
        }

        //static void Main(string[] args)
        //{
        //    DrawingAutomation auto = new DrawingAutomation();
        //    auto.OpenDrawingTemplate(@"C:\NXTemplates\DrawingTemplate.prt");
        //    DrawingSheet sheet = auto.GetFirstSheet();

        //    // Truyền đường dẫn part hoặc assembly!
        //    auto.CreateBaseView_Python(
        //        @"D:\1. OE_Keson\6.2025\12.Laser line scanner\PythonProject1\create_base_view.py",
        //        @"C:\NXParts\MyPart.prt",      // hoặc assembly: @"C:\NXParts\MyAssy.prt"
        //        sheet.Name,
        //        "TOP_VIEW",
        //        "top",
        //        120.0, 80.0, 1.0
        //    );
        //}

    }
}
