using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fomular_match;
using NXOpen;
using NXOpenUI;

namespace basic
{
    class Program
    {
        static void Main(string[] args)
        {
            //Các cách khai báo 

            int tuoi = 25;// số nguyên
            long danso = 9500000; // lớn hơn Interger nhưng hiếm xài

            float diem = 8.5f;// kiểu số thực 7 chữ số thập phân cần ít bộ nhớ
            double pi = 3.14159; // kiểu số thực dùng phổ biến
            decimal giaTien = 199.99m; // 28-29 chữ số thường dùng cho tài chính

            string hoTen = "Nguyen VAn A";
            bool laSinhVien = true;
            
            int[] mang = { 1, 2, 3 };
            List<string> ten = new List<string>() { "An", "Binh", "Cuong" };
           
            /*///////////////////////////////////////////
             Toán tử logic:
             - Chia lấy dư : kí hiệu % VD: 5%2 trả về 1
             Phép toán so sánh:
             - == : so sánh bằng
             - != khác nhau
             - > : lớn hơn
             - < : bé hơn
             - >= : lớn hơn hoặc bằng
             - <= : bé hơn hoặc bằng
             Phép toán logic:
             - && : và  
             - || : or
             - ! : phủ định 
             Phép tăng giảm:
             - ++: tăng thêm 1 vd: x++ hoăc ++X
             - --: trừ đi 1  VD: X-- hoặc --X

             ///////////////////////////////////////////////////*/
            
            
            //////////////////////////////////////////////////////
            ///Bài tập cơ bản 
            ///BT1: Tính tổng 2 số nguyên
            Console.Write("Nhập số thứ nhất: ");
            int a = int.Parse(Console.ReadLine());
            Console.Write("Nhập số thứ hai:");
            int b = int.Parse(Console.ReadLine());

            int tong = a + b;
            Console.WriteLine("Tổng hai số là: " + tong);
            Console.ReadLine();
            /////////////////////////////////////////////////////

            //////////////////////////////////////////////////////
            ///Bài tập cơ bản 
            ///BT2: Kiểm tra số chẵn lẽ
            Console.Write("Nhập một số nguyên: ");
            int n = int.Parse(Console.ReadLine());

            if (n % 2 == 0)
                Console.WriteLine(n + " là số chẵn.");
            else
                Console.WriteLine(n + " là số lẻ.");
            /////////////////////////////////////////////////////

            //////////////////////////////////////////////////////
            ///Bài tập cơ bản 
            ///BT3: tính giai thừa
            Console.Write("Nhập số nguyên n: ");
            int n1 = int.Parse(Console.ReadLine());
            long giaiThua = 1;

            for (int i = 1; i <= n1; i++)
            {
                giaiThua *= i;
            }

            Console.WriteLine("Giai thừa của " + n1 + " là: " + giaiThua);
            /////////////////////////////////////////////////////

            /*
             * Cú pháp:
             
                if (điều_kiện)
                {
                    // Thực hiện nếu điều kiện đúng
                }

              * Cú pháp vòng lặp for
              * 
                for (khởi_tạo; điều_kiện; cập_nhật)
                {
                    // Thực hiện lặp
                }
                - Dùng khi:
                    + Cần biết chỉ số (index) của phần tử.
                    + Muốn truy cập, thay đổi phần tử theo vị trí.
                    + Cần kiểm soát chặt chẽ số lần lặp.

               * Cú pháp foreach
               * 
                foreach (kiểu_dữ_liệu biến in tập_hợp)
                {
                    // Thực hiện với từng phần tử
                }
                - Dùng khi:
                    + Chỉ cần duyệt qua từng phần tử.
                    + Không cần biết chỉ số.
                    + Không thay đổi phần tử trong mảng (chỉ đọc).

             */
            //// ví dụ for và foreach

            int[] arr = { 1, 2, 3 };

            // Dùng for để thay đổi giá trị
            for (int i = 0; i < arr.Length; i++)
            {
                arr[i] += 1;
            }

            // Dùng foreach chỉ để đọc
            foreach (int item in arr)
            {
                Console.WriteLine(item); // In ra 2, 3, 4
            }
            //////////////////////////////////////////////////////////////
            

            Fomular_Funcation toanhoc = new Fomular_Funcation();
            double tong1 = toanhoc.cong(20, 30);
            string ingiatri = toanhoc.inGiaTri("Hãy chửi tôi");
            Console.WriteLine(ingiatri);
            double abc = Cong(10, 20) + Nhan(10, 2) / Chia(100, 2);
            Console.WriteLine($"tổng abc:" + abc);
            Console.ReadLine();

            Session mySession = Session.GetSession(); // lấy NX section hiện tại
            PartCollection parts = mySession.Parts; // Lấy partcolection 
            Part workPart = parts.Work; // lấy part đang hoạt động


        }


        public static double Cong(double x, double y)
        {
            return x + y;
        }

        public static double Tru(double x, double y)
        {
            return x - y;
        }

        public static double Nhan(double x, double y)
        {
            return x * y;
        }

        public static double Chia(double x, double y)
        {
            if (y == 0)
            {
                Console.WriteLine("Không thể chia cho 0!");
                return double.NaN;
            }
            return x / y;
        }

        public static string InGiaTri(string s)
        {
            return "Giá trị bạn nhập là: " + s;
        }

    }
}
