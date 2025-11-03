using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using NXOpen;
using NXOpen.Features;
using NXOpen.UF;
using NXOpen.Assemblies;
using System.IO;

namespace basic
{
    class OOP_Ses2
    {
    }

    class NXManager
    {
        private const string NXProcessName = "ugraf";

        public bool IsNXRunning()
        {
            var processes = Process.GetProcessesByName(NXProcessName);
            return processes.Any();
        }

        public void LaunchNX()
        {
            string nxPath = @"C:\Program Files\Siemens\NX\NX12\UGII\ugraf.exe"; // Đường dẫn NX
            try
            {
                Process.Start(nxPath);
                Console.WriteLine("Đang mở phần mềm NX...");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Không thể mở NX: " + ex.Message);
            }

        }

        // cách dùng bên console
        //NXManager nxManager = new NXManager();

        //    if (nxManager.IsNXRunning())
        //    {
        //        Console.WriteLine("NX đang chạy.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("NX chưa chạy. Đang mở...");
        //        nxManager.LaunchNX();
        //    }

            /*Tạo Part và vẽ */
        public class NXPartService
        {
            private Session theSession;
            private UFSession theUFSession;
            private Part workPart;

            public NXPartService()
            {
                theSession = Session.GetSession();
                theUFSession = UFSession.GetUFSession();
                workPart = theSession.Parts.Work;
            }

            public void CreateNewPart(string tenPart)
            {
                string partPath = $"C:\\NXParts\\{tenPart}.prt";
                theUFSession.Part.New(partPath, UFConstants.UF_PART_METRIC, out Tag partTag);
                workPart = theSession.Parts.Work;
            }

            // Hàm kiểm tra kích thước đầu vào
            private void ValidateDimension(params double[] dims)
            {
                foreach (var d in dims)
                    if (d <= 0)
                        throw new ArgumentException("Kích thước phải > 0.");
            }

            public void CreateRectangleAndExtrude(double width, double height, double depth)
            {
                UFSession theUFSession = UFSession.GetUFSession();

                // vẽ 4 điểm hình chữ nhật
                double[][] pts = new double[][] {   // mảng không gian 3 chiều
                new double[] {0, 0, 0},
                new double[] {width, 0, 0},
                new double[] {width, height, 0},
                new double[] {0, height, 0}
                };

                // vòng lapwjx nối vẽ đường thẳng dạng tag
                Tag[] curveTags = new Tag[4];
                for (int i = 0; i < 4; ++i) 
                {
                    NXOpen.UF.UFCurve.Line line_struct = new NXOpen.UF.UFCurve.Line();
                    line_struct.start_point = pts[i];
                    line_struct.end_point = pts[(i + 1) % 4];
                    Tag lineTag;
                    theUFSession.Curve.CreateLine(ref line_struct, out lineTag);
                    curveTags[i] = lineTag;
                }

                string region_option = "CURVE";
                string[] region_strings = new string[] { };
                double[] direction = new double[] { 0.0, 0.0, 1.0 };
                double[] limits = new double[] { 0.0, depth };

                NXOpen.UF.FeatureSigns sign = NXOpen.UF.FeatureSigns.Positive;

                Tag[] extrudeFeatureTags;
                theUFSession.Modl.CreateExtruded(
                    curveTags,
                    region_option,
                    region_strings,
                    direction,
                    limits,
                    sign,
                    out extrudeFeatureTags //Kết quả lưu lại vào mảng extrudeFeatureTags (có thể có nhiều tag nếu tạo nhiều khối).
                );

                Session.GetSession().ListingWindow.Open();
                Session.GetSession().ListingWindow.WriteLine($"Đã tạo khối: Rộng={width}mm, Cao={height}mm, Sâu={depth}mm");
            }
        }
        /*Thay đổi tham số Part*/
        class NXParameterService
        {
            private Session theSession;

            public NXParameterService()
            {
                theSession = Session.GetSession();
            }

            // mở file part từ đường dẫn
            public Part OpenPart(string partPath)
            {
                NXOpen.PartLoadStatus partLoadStatus;
                theSession.Parts.OpenDisplay(partPath, out partLoadStatus);
                Part openedPart = theSession.Parts.Work; // Lấy part đang mở
                return openedPart;
            }

            // liệt kê toàn bộ parameter của part
            public void ListAllParameters(Part part)
            {
                theSession.ListingWindow.Open();
                foreach (Expression expr in part.Expressions)
                {
                    theSession.ListingWindow.WriteLine(
                        $"Tên: {expr.Name}, Giá trị: {expr.Value}, Biểu thức: {expr.ExpressionString}");
                }
            }

            //Thay đổi giá trị Parameter theo tên
            public void EditParameter(Part part, string parameterName, double newValue)
            {
                foreach (Expression expr in part.Expressions)
                {
                    if (expr.Name == parameterName)
                    {
                        expr.Value = newValue;
                        theSession.ListingWindow.WriteLine(
                            $"Đã sửa {expr.Name} thành {newValue}");
                        return;
                    }
                }
                throw new ArgumentException($"Không tìm thấy parameter tên: {parameterName}");
            }
        }

        /*Thay đổi tham số Assembly*/
        class NXAssemblyParameterService
        {
            private Session theSession;

            public NXAssemblyParameterService()
            {
                theSession = Session.GetSession();
            }

            /// Mở assembly part
            public Part OpenAssembly(string asmPath)
            {
                NXOpen.PartLoadStatus partLoadStatus;
                theSession.Parts.OpenDisplay(asmPath, out partLoadStatus);
                return theSession.Parts.Work;
            }

            /// Truy xuất component theo tên và sửa parameter
            public void EditComponentParameter(Part assemblyPart, string componentName, string parameterName, double newValue)
            {
                // Tìm component trong assembly
                foreach (Component comp in assemblyPart.ComponentAssembly.RootComponent.GetChildren())
                {
                    if (comp.Name == componentName)
                    {
                        // Mở part của component
                        Part compPart = comp.Prototype as Part;
                        if (compPart != null)
                        {
                            // Sửa parameter như với part thường
                            foreach (Expression expr in compPart.Expressions)
                            {
                                if (expr.Name == parameterName)
                                {
                                    expr.Value = newValue;
                                    theSession.ListingWindow.Open();
                                    theSession.ListingWindow.WriteLine(
                                      $"Đã sửa parameter '{parameterName}' trong component '{componentName}' thành {newValue}");
                                    return;
                                }
                            }
                            throw new Exception($"Không tìm thấy parameter '{parameterName}' trong component '{componentName}'");
                        }
                        else
                        {
                            throw new Exception($"Không lấy được Part từ component '{componentName}'");
                        }
                    }
                }
                throw new Exception($"Không tìm thấy component '{componentName}' trong assembly");
            }
        }

        /*ạo file part và chọn mặt phẳng vẽ sketch tự động*/
        public class NXAutoSketchService
        {
            private Session theSession;
            private UFSession theUFSession;
            private Part workPart;

            public NXAutoSketchService()
            {
                theSession = Session.GetSession();
                theUFSession = UFSession.GetUFSession();
                workPart = theSession.Parts.Work;
            }

            // Tạo file part mới với đơn vị mm
            public void CreateNewPart(string tenPart)
            {
                string folder = @"C:\NXParts";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string partPath = Path.Combine(folder, $"{tenPart}.prt");
                theUFSession.Part.New(partPath, UFConstants.UF_PART_METRIC, out Tag partTag);
                workPart = theSession.Parts.Work;
            }

            // Tạo mặt phẳng chuẩn theo kiểu (0: XY, 1: YZ, 2: ZX)
            public Tag CreateDatumPlane(int type)
            {
                Matrix3x3 orientation = new Matrix3x3();
                Point3d origin = new Point3d(0, 0, 0);
                switch (type)
                {
                    case 0: // XY
                        orientation.Xx = 1; orientation.Xy = 0; orientation.Xz = 0;
                        orientation.Yx = 0; orientation.Yy = 1; orientation.Yz = 0;
                        orientation.Zx = 0; orientation.Zy = 0; orientation.Zz = 1;
                        break;
                    case 1: // YZ
                        orientation.Xx = 0; orientation.Xy = 1; orientation.Xz = 0;
                        orientation.Yx = 0; orientation.Yy = 0; orientation.Yz = 1;
                        orientation.Zx = 1; orientation.Zy = 0; orientation.Zz = 0;
                        break;
                    case 2: // ZX
                        orientation.Xx = 0; orientation.Xy = 0; orientation.Xz = 1;
                        orientation.Yx = 1; orientation.Yy = 0; orientation.Yz = 0;
                        orientation.Zx = 0; orientation.Zy = 1; orientation.Zz = 0;
                        break;
                    default:
                        throw new ArgumentException("Chỉ chọn 0 (XY), 1 (YZ), hoặc 2 (ZX)");
                }
                DatumPlane datumPlane = workPart.Datums.CreateFixedDatumPlane(origin, orientation);
                theSession.ListingWindow.Open();
                theSession.ListingWindow.WriteLine($"Đã tạo mặt phẳng {datumPlane.Name}. Vui lòng vào NX, chọn mặt phẳng này rồi bấm Create Sketch để bắt đầu vẽ.");
                return datumPlane.Tag;
            }

            //hàm chọn các mặt phẳng mặt định để vẽ
            public DatumPlane SelectDefaultDatum(int type)
            {
                // type: 0 = XY, 1 = YZ, 2 = ZX
                string datumName;
                switch (type)
                {
                    case 0:
                        datumName = "DATUM_CSYS_XY";
                        break;
                    case 1:
                        datumName = "DATUM_CSYS_YZ";
                        break;
                    case 2:
                        datumName = "DATUM_CSYS_ZX";
                        break;
                    default:
                        throw new ArgumentException("Chỉ nhận 0 (XY), 1 (YZ), 2 (ZX)");
                }
                DatumPlane datum = workPart.Datums.FindObject(datumName) as DatumPlane;
                if (datum == null)
                    throw new Exception($"Không tìm thấy mặt phẳng mặc định: {datumName}");
                return datum;
            }
            
            // hàm vẽ đường thẳng
            public void DrawLineOnDefaultDatum(int type, double[] startPoint, double[] endPoint)
            {
                // type: 0 - XY, 1 - YZ, 2 - ZX

                // Chọn mặt phẳng datum mặc định
                string datumName;
                switch (type)
                {
                    case 0: datumName = "DATUM_CSYS_XY"; break;
                    case 1: datumName = "DATUM_CSYS_YZ"; break;
                    case 2: datumName = "DATUM_CSYS_ZX"; break;
                    default: throw new ArgumentException("type phải là 0 (XY), 1 (YZ), hoặc 2 (ZX)");
                }
                DatumPlane datum = workPart.Datums.FindObject(datumName) as DatumPlane;
                if (datum == null)
                    throw new Exception($"Không tìm thấy mặt phẳng mặc định: {datumName}");

                // Vẽ đường thẳng trên mặt phẳng đã chọn
                NXOpen.UF.UFCurve.Line line = new NXOpen.UF.UFCurve.Line
                {
                    start_point = startPoint,
                    end_point = endPoint
                };
                Tag lineTag;
                theUFSession.Curve.CreateLine(ref line, out lineTag);

                theSession.ListingWindow.Open();
                string planeName = type == 0 ? "XY" : type == 1 ? "YZ" : "ZX";
                theSession.ListingWindow.WriteLine(
                    $"Đã vẽ 1 đường thẳng trên mặt phẳng {planeName}: [{startPoint[0]},{startPoint[1]},{startPoint[2]}] → [{endPoint[0]},{endPoint[1]},{endPoint[2]}]");
            }

            // Vẽ hình chữ nhật trên mặt phẳng đã chọn (ví dụ: Sketch trên XY với width, height)
            public void CreateSketchOnPlane(Tag datumPlaneTag, int type, double width = 100, double height = 50)
            {
                double[][] pts;

                if (type == 0) // XY
                {
                    pts = new double[][]
                    {
                        new double[] {0, 0, 0},
                        new double[] {width, 0, 0},
                        new double[] {width, height, 0},
                        new double[] {0, height, 0}
                    };
                }
                else if (type == 1) // YZ
                {
                    pts = new double[][]
                    {
                        new double[] {0, 0, 0},
                        new double[] {0, width, 0},
                        new double[] {0, width, height},
                        new double[] {0, 0, height}
                    };
                }
                else if (type == 2) // ZX
                {
                    pts = new double[][]
                    {
                        new double[] {0, 0, 0},
                        new double[] {width, 0, 0},
                        new double[] {width, 0, height},
                        new double[] {0, 0, height}
                    };
                }
                else
                {
                    throw new ArgumentException("type chỉ nhận giá trị 0 (XY), 1 (YZ) hoặc 2 (ZX)");
                }

                Tag[] curveTags = new Tag[4];
                for (int i = 0; i < 4; ++i)
                {
                    NXOpen.UF.UFCurve.Line line = new NXOpen.UF.UFCurve.Line
                    {
                        start_point = pts[i],
                        end_point = pts[(i + 1) % 4]
                    };
                    theUFSession.Curve.CreateLine(ref line, out Tag lineTag);
                    curveTags[i] = lineTag;
                }

                theSession.ListingWindow.Open();
                string planeName = type == 0 ? "XY" : type == 1 ? "YZ" : "ZX";
                theSession.ListingWindow.WriteLine($"Đã vẽ hình chữ nhật {width}x{height} trên mặt phẳng {planeName} (Tag: {datumPlaneTag})");
            }

        }


        

        class Program
        {
            static void Main(string[] args)
            {
                // Khởi tạo NXAutoSketchService
                NXAutoSketchService nxService = new NXAutoSketchService();

                // Tạo file part mới
                nxService.CreateNewPart("DemoPart");

                // Tạo mặt phẳng XY
                Tag xyPlane = nxService.CreateDatumPlane(0); // 0 = XY

                // Vẽ hình chữ nhật trên XY
                nxService.CreateSketchOnPlane(xyPlane, 0, 120, 80); // Vẽ trên XY

                Tag yzPlane = nxService.CreateDatumPlane(1);
                nxService.CreateSketchOnPlane(yzPlane, 1, 100, 50); // Vẽ trên YZ

                Tag zxPlane = nxService.CreateDatumPlane(2);
                nxService.CreateSketchOnPlane(zxPlane, 2, 150, 30); // Vẽ trên ZX

                
                Console.WriteLine("Đã thực hiện xong tạo part và vẽ sketch!");
                Console.ReadLine(); // Giữ cửa sổ console mở để xem kết quả

                nxService.DrawLineOnDefaultDatum(0, new double[] { 0, 0, 0 }, new double[] { 100, 0, 0 }); // Đường thẳng trên XY
            }
        }



    }
}
