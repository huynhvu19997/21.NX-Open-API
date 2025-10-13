using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using NXOpen; //  tương tác sâu hơn với NX
using NXOpenUI; // tùy chọn làm việc với giao diện NX

namespace General_NXFuncations
{
    class General_NxFuncation
    {
        public void StartNXAppliction(string nxExecutablePath)
        {
            try
            {
                if(!File.Exists(nxExecutablePath))
                {
                    Console.WriteLine("đường dẫn NX không tồn tại: " + nxExecutablePath);
                    return;
                }

                // khởi tạo tiến trình NX

                Process nxProcess = new Process();
                nxProcess.StartInfo.FileName = nxExecutablePath; // đường dẫn đến file. exe
                nxProcess.StartInfo.UseShellExecute = true; // cho phép sử dụng shell
                nxProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal; // Hiển thị của sổ bình thường

                // gửi thông báo khi Nx đang được mở
                Console.WriteLine("đang mở ứng dụng simen NX....");
                nxProcess.Start();// khởi chạy NX

                Console.WriteLine("NX đã khởi động thành công!");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Có lỗi khi mở NX: " + ex.Message);
            }

            /*
             * Cách sử dụng với console
             * 
             *
                    static void Main(string[] args)
                    {
                    // Tạo một instance của class để sử dụng hàm
                    General_NxFuncation general_NxFuncation = new General_NxFuncation();

                    // Đường dẫn tới Siemens NX (ugraf.exe)
                    string nxExecutablePath = @"C:\Program Files\Siemens\NX\ugraf.exe"; // Thay đường dẫn NX

                    // Gọi hàm mở NX
                    general_NxFuncation.StartNXApplication(nxExecutablePath);
                    }
             * 
            */
        }

        /// <summary>
        /// Mở file Part (.prt) trong NX.
        /// </summary>
        /// <param name="partFilePath">Đường dẫn đầy đủ tới file Part.</param>
        public void OpenPartFile(string partFilePath)
        {
            try
            {
                // Kiểm tra NX có đang chạy không
                if (!IsNXRunning())
                {
                    Console.WriteLine("NX chưa khởi động. Không thể mở file Part!");
                    return;
                }

                // Lấy session hiện tại của NX
                Session nxSession = Session.GetSession();

                // Tạo biến loadStatus để lưu trạng thái khi load file
                PartLoadStatus loadStatus;

                // Mở file Part
                Part part = nxSession.Parts.Open(partFilePath, out loadStatus);

                // Nếu part là null, có nghĩa là file không được mở
                if (part == null)
                {
                    Console.WriteLine($"Không thể mở file Part: {partFilePath}");
                }
                else
                {
                    Console.WriteLine($"Đã mở file Part: {partFilePath} thành công!");
                }

                // Giải phóng tài nguyên của loadStatus sau khi sử dụng
                loadStatus.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi mở file Part: {partFilePath}. Chi tiết lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Mở file Assembly (.prt hoặc file assembly chứa cấu trúc sản phẩm).
        /// </summary>
        /// <param name="assemblyFilePath">Đường dẫn đầy đủ tới file Assembly.</param>
        public void OpenAssemblyFile(string assemblyFilePath)
        {
            try
            {
                // Kiểm tra NX có đang chạy không
                if (!IsNXRunning())
                {
                    Console.WriteLine("NX chưa khởi động. Không thể mở file Assembly!");
                    return;
                }

                // Lấy session hiện tại của NX
                Session nxSession = Session.GetSession();

                // Tạo biến loadStatus để lưu trạng thái khi load file
                PartLoadStatus loadStatus;

                // Mở file Assembly
                Part assemblyPart = nxSession.Parts.Open(assemblyFilePath, out loadStatus);

                // Nếu assemblyPart là null, có nghĩa là file không được mở
                if (assemblyPart == null)
                {
                    Console.WriteLine($"Không thể mở file Assembly: {assemblyFilePath}");
                }
                else
                {
                    Console.WriteLine($"Đã mở file Assembly: {assemblyFilePath} thành công!");
                }

                // Giải phóng tài nguyên của loadStatus sau khi sử dụng
                loadStatus.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi mở file Assembly: {assemblyFilePath}. Chi tiết lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Mở file Drawing (.dwg hoặc bản vẽ liên kết với NX Part/Assembly).
        /// </summary>
        /// <param name="drawingFilePath">Đường dẫn đầy đủ tới file Drawing.</param>
        public void OpenDrawingFile(string drawingFilePath)
        {
            try
            {
                // Kiểm tra NX có đang chạy không
                if (!IsNXRunning())
                {
                    Console.WriteLine("NX chưa khởi động. Không thể mở file Drawing!");
                    return;
                }

                // Lấy session hiện tại của NX
                Session nxSession = Session.GetSession();

                // Tạo biến loadStatus để lưu trạng thái khi load file
                PartLoadStatus loadStatus;

                // Mở file Drawing
                Part drawingPart = nxSession.Parts.Open(drawingFilePath, out loadStatus);

                // Nếu drawingPart là null, có nghĩa là file không được mở
                if (drawingPart == null)
                {
                    Console.WriteLine($"Không thể mở file Drawing: {drawingFilePath}");
                }
                else
                {
                    Console.WriteLine($"Đã mở file Drawing: {drawingFilePath} thành công!");
                }

                // Giải phóng tài nguyên của loadStatus sau khi sử dụng
                loadStatus.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi mở file Drawing: {drawingFilePath}. Chi tiết lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Kiểm tra NX có đang chạy hay không.
        /// </summary>
        /// <returns>True nếu NX đang chạy, ngược lại False.</returns>
        public bool IsNXRunning()
        {
            try
            {
                foreach (System.Diagnostics.Process process in System.Diagnostics.Process.GetProcesses())
                {
                    if (process.ProcessName.ToLower().Contains("ugraf")) // Tên tiến trình NX
                    {
                        return true; // NX đang chạy
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi kiểm tra NX: {ex.Message}");
            }
            return false;
        }

        /// <summary>
        /// Mở file Part, thay đổi giá trị của một expression (tham số).
        /// </summary>
        /// <param name="partFilePath">Đường dẫn đầy đủ tới file Part.</param>
        /// <param name="expressionName">Tên của tham số (expression) cần thay đổi.</param>
        /// <param name="newValue">Giá trị mới cần thay đổi.</param>
        public void OpenPartAndChangeExpression(string partFilePath, string expressionName, double newValue)
        {
            try
            {
                // Kiểm tra NX có đang chạy không
                if (!IsNXRunning())
                {
                    Console.WriteLine("NX chưa khởi động. Không thể mở file Part!");
                    return;
                }

                // Lấy session hiện tại của NX
                Session nxSession = Session.GetSession();

                // Tạo biến loadStatus để lưu trạng thái khi load file
                PartLoadStatus loadStatus;

                // Mở file Part
                Part part = nxSession.Parts.Open(partFilePath, out loadStatus);

                // Kiểm tra trạng thái mở file
                if (part == null)
                {
                    Console.WriteLine($"Không thể mở file Part: {partFilePath}");
                    return;
                }

                Console.WriteLine($"Đã mở file Part: {partFilePath} thành công!");

                // Truy cập vào các expressions trong Part
                Expression[] expressions = part.Expressions.ToArray();

                // Tìm expression cần thay đổi
                bool expressionFound = false;
                foreach (Expression exp in expressions)
                {
                    if (exp.Name == expressionName)
                    {
                        // Thay đổi giá trị của expression bằng cách đặt lại công thức
                        exp.SetFormula(newValue.ToString()); // Chuyển giá trị thành chuỗi và ứng dụng vào công thức

                        Console.WriteLine($"Đã thay đổi giá trị của tham số '{expressionName}' thành {newValue}");
                        expressionFound = true;
                        break;
                    }
                }

                if (!expressionFound)
                {
                    Console.WriteLine($"Không tìm thấy tham số với tên '{expressionName}' trong file Part.");
                }

                // Lưu file Part sau khi thay đổi
                part.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
                Console.WriteLine($"Đã lưu file Part: {partFilePath}");

                // Giải phóng loadStatus sau khi sử dụng
                loadStatus.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi thay đổi tham số trong Part: {partFilePath}. Chi tiết lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Mở file Assembly và thay đổi giá trị của một tham số (Expression).
        /// </summary>
        /// <param name="assemblyFilePath">Đường dẫn đầy đủ tới file Assembly.</param>
        /// <param name="expressionName">Tên của tham số (Expression) cần thay đổi.</param>
        /// <param name="newValue">Giá trị mới cần thay đổi cho tham số.</param>
        public void OpenAssemblyAndChangeExpression(string assemblyFilePath, string expressionName, double newValue)
        {
            try
            {
                // Kiểm tra NX có đang chạy hay không
                if (!IsNXRunning())
                {
                    Console.WriteLine("NX chưa khởi động. Không thể mở file Assembly!");
                    return;
                }

                // Lấy Session hiện tại của NX
                Session nxSession = Session.GetSession();

                // Tạo biến loadStatus để lưu trạng thái khi load file
                PartLoadStatus loadStatus;

                // Mở file Assembly
                Part assemblyPart = nxSession.Parts.Open(assemblyFilePath, out loadStatus);

                // Kiểm tra trạng thái mở file Assembly
                if (assemblyPart == null)
                {
                    Console.WriteLine($"Không thể mở file Assembly: {assemblyFilePath}");
                    return;
                }

                Console.WriteLine($"Đã mở file Assembly: {assemblyFilePath} thành công!");

                // Truy cập các expressions (tham số) bên trong Assembly
                Expression[] expressions = assemblyPart.Expressions.ToArray();

                // Tìm expression cần thay đổi
                bool expressionFound = false;
                foreach (Expression exp in expressions)
                {
                    if (exp.Name == expressionName)
                    {
                        // Thay đổi giá trị của expression
                        exp.SetFormula(newValue.ToString()); // Dùng SetFormula để cập nhật công thức

                        Console.WriteLine($"Đã thay đổi giá trị của tham số '{expressionName}' thành {newValue}");
                        expressionFound = true;
                        break;
                    }
                }

                if (!expressionFound)
                {
                    Console.WriteLine($"Không tìm thấy tham số với tên '{expressionName}' trong Assembly.");
                }

                // Lưu lại file Assembly
                assemblyPart.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
                Console.WriteLine($"Đã lưu file Assembly: {assemblyFilePath}");

                // Giải phóng dữ liệu loadStatus sau khi sử dụng
                loadStatus.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi thay đổi tham số trong Assembly: {assemblyFilePath}. Chi tiết lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Mở file Part và thay đổi nhiều giá trị của các expressions (tham số).
        /// </summary>
        /// <param name="partFilePath">Đường dẫn đầy đủ tới file Part.</param>
        /// <param name="parametersToChange">Danh sách các tham số cần thay đổi (key: tên tham số, value: giá trị mới).</param>
        public void OpenPartAndChangeExpressions(string partFilePath, Dictionary<string, double> parametersToChange)
        {
            try
            {
                // Kiểm tra NX có đang chạy không
                if (!IsNXRunning())
                {
                    Console.WriteLine("NX chưa khởi động. Không thể mở file Part!");
                    return;
                }

                // Lấy Session hiện tại của NX
                Session nxSession = Session.GetSession();

                // Tạo biến loadStatus để lưu trạng thái khi load file
                PartLoadStatus loadStatus;

                // Mở file Part
                Part part = nxSession.Parts.Open(partFilePath, out loadStatus);

                // Kiểm tra trạng thái mở file
                if (part == null)
                {
                    Console.WriteLine($"Không thể mở file Part: {partFilePath}");
                    return;
                }

                Console.WriteLine($"Đã mở file Part: {partFilePath} thành công!");

                // Truy cập các expressions trong Part
                Expression[] expressions = part.Expressions.ToArray();

                // Lặp qua danh sách parametersToChange để thay đổi giá trị
                foreach (var kvp in parametersToChange)
                {
                    string parameterName = kvp.Key; // Tên tham số
                    double newValue = kvp.Value;    // Giá trị mới

                    // Tìm expression cần thay đổi
                    bool expressionFound = false;
                    foreach (Expression exp in expressions)
                    {
                        if (exp.Name == parameterName)
                        {
                            // Thay đổi giá trị của tham số
                            exp.SetFormula(newValue.ToString()); // Đặt giá trị mới

                            Console.WriteLine($"Đã thay đổi giá trị của tham số '{parameterName}' thành {newValue}");
                            expressionFound = true;
                            break;
                        }
                    }

                    // Nếu không tìm thấy tham số
                    if (!expressionFound)
                    {
                        Console.WriteLine($"Không tìm thấy tham số với tên '{parameterName}' trong file Part.");
                    }
                }

                // Lưu file Part sau khi thực hiện thay đổi
                part.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
                Console.WriteLine($"Đã lưu file Part: {partFilePath}");

                // Giải phóng loadStatus sau khi sử dụng
                loadStatus.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi thay đổi tham số trong Part: {partFilePath}. Chi tiết lỗi: {ex.Message}");
            }
        }


        /// <summary>
        /// Mở file Assembly và thay đổi nhiều giá trị của các expressions.
        /// </summary>
        /// <param name="assemblyFilePath">Đường dẫn tới file Assembly.</param>
        /// <param name="parametersToChange">Danh sách tham số cần thay đổi (key: tên tham số, value: giá trị mới).</param>
        public void OpenAssemblyAndChangeExpressions(string assemblyFilePath, Dictionary<string, double> parametersToChange)
        {
            try
            {
                // Kiểm tra NX có đang chạy không
                if (!IsNXRunning())
                {
                    Console.WriteLine("NX chưa khởi động. Không thể mở file Assembly!");
                    return;
                }

                // Lấy session hiện tại của NX
                Session nxSession = Session.GetSession();

                // Tạo biến loadStatus để lưu trạng thái khi load file
                PartLoadStatus loadStatus;

                // Mở file Assembly
                Part assemblyPart = nxSession.Parts.Open(assemblyFilePath, out loadStatus);

                // Kiểm tra trạng thái mở Assembly
                if (assemblyPart == null)
                {
                    Console.WriteLine($"Không thể mở file Assembly: {assemblyFilePath}");
                    return;
                }

                Console.WriteLine($"Đã mở file Assembly: {assemblyFilePath} thành công!");

                // Lấy danh sách các expressions trong Assembly
                Expression[] expressions = assemblyPart.Expressions.ToArray();

                // Lặp qua các parametersToChange để chỉnh sửa giá trị
                foreach (var kvp in parametersToChange)
                {
                    string parameterName = kvp.Key; // Tên tham số
                    double newValue = kvp.Value;    // Giá trị mới

                    // Tìm expression cần thay đổi trong Assembly
                    bool expressionFound = false;
                    foreach (Expression exp in expressions)
                    {
                        if (exp.Name == parameterName)
                        {
                            // Thay đổi giá trị của expression
                            exp.SetFormula(newValue.ToString());
                            Console.WriteLine($"Đã thay đổi giá trị của tham số '{parameterName}' thành {newValue}");
                            expressionFound = true;
                            break;
                        }
                    }

                    // Nếu tham số không được tìm thấy
                    if (!expressionFound)
                    {
                        Console.WriteLine($"Không tìm thấy tham số với tên '{parameterName}' trong Assembly.");
                    }
                }

                // Lưu file Assembly sau khi chỉnh sửa
                assemblyPart.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
                Console.WriteLine($"Đã lưu file Assembly: {assemblyFilePath}");

                // Giải phóng loadStatus sau khi sử dụng
                loadStatus.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi cập nhật tham số trong Assembly: {assemblyFilePath}. Chi tiết lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Tạo một file Model mới với đơn vị Millimeter trong NX.
        /// </summary>
        /// <param name="modelFilePath">Đường dẫn đầy đủ để lưu file Model.</param>
        public void CreateModelWithMillimeterUnit(string modelFilePath)
        {
            try
            {
                // Kiểm tra NX có đang chạy không
                if (!IsNXRunning())
                {
                    Console.WriteLine("NX chưa khởi động. Không thể tạo file Model mới!");
                    return;
                }

                // Lấy Session hiện tại của NX
                Session nxSession = Session.GetSession();

                // Tạo một file Model mới với đơn vị Millimeter
                Part newPart = nxSession.Parts.NewDisplay(modelFilePath, Part.Units.Millimeters);

                // Hiển thị thông báo thành công
                Console.WriteLine($"File Model mới đã được tạo tại: {modelFilePath}");
                Console.WriteLine("Hệ thống thiết kế sử dụng đơn vị: Millimeter (mm)");

                // Lưu file Model
                newPart.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
                Console.WriteLine($"File Model đã được lưu: {modelFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi tạo Model mới tại {modelFilePath}. Chi tiết lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Tạo một file Assembly với đơn vị Millimeter.
        /// </summary>
        /// <param name="assemblyFilePath">Đường dẫn lưu file Assembly.</param>
        public void CreateAssemblyWithMillimeterUnit(string assemblyFilePath)
        {
            try
            {
                // Kiểm tra NX có đang chạy hay không
                if (!IsNXRunning())
                {
                    Console.WriteLine("NX chưa khởi động.");
                    return;
                }

                // Lấy Session hiện tại của NX
                Session nxSession = Session.GetSession();

                // Tạo một file Assembly mới (mặc định là Millimeter)
                Part assemblyPart = nxSession.Parts.NewDisplay(assemblyFilePath, Part.Units.Millimeters);
               
                // Hiển thị thông báo thành công
                Console.WriteLine($"File Assembly mới đã được tạo tại: {assemblyFilePath}");
                Console.WriteLine("Đơn vị hệ metric: Millimeter (mm)");

                // Lưu file Assembly
                assemblyPart.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
                Console.WriteLine($"File Assembly đã được lưu: {assemblyFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi tạo Assembly tại {assemblyFilePath}. Chi tiết lỗi: {ex.Message}");
            }
        }

        /// <summary>
        /// Tạo một file Drawing mới.
        /// </summary>
        /// <param name="drawingFilePath">Đường dẫn lưu file Drawing.</param>
        public void CreateDrawing(string drawingFilePath)
        {
            try
            {
                // Kiểm tra NX có đang chạy không
                if (!IsNXRunning())
                {
                    Console.WriteLine("NX chưa khởi động.");
                    return;
                }

                // Lấy Session hiện tại của NX
                Session nxSession = Session.GetSession();

                // Tạo một file Drawing giống Part nhưng đơn vị là Millimeter
                Part drawingPart = nxSession.Parts.NewDisplay(drawingFilePath,Part.Units.Millimeters);

                // Hiển thị thông báo thành công
                Console.WriteLine($"File Drawing mới đã được tạo tại: {drawingFilePath}");
                Console.WriteLine("Đơn vị hệ metric: Millimeter (mm)");

                // Lưu File Drawing
                drawingPart.Save(BasePart.SaveComponents.True, BasePart.CloseAfterSave.False);
                Console.WriteLine($"File Drawing đã được lưu: {drawingFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Có lỗi khi tạo Drawing tại {drawingFilePath}. Chi tiết lỗi: {ex.Message}");
            }
        }
    }
}

