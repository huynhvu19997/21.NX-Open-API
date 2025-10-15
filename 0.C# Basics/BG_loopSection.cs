using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_loopSection
{
    class BG_loopSection
    {
        /*if-else*/
        /*
         * cách ghi rút gọn của câu lệnh
         * If (x > y) max = x
         * else max =y;
         * 
         * hoặc có thể viết đơn giản 
         * max = (X>Y ? X:Y)
         */
        private void bt1()
        {
            /*nhập 3 số a,b,c từ bàn phím. tìm số lớn nhất của chúng*/
            double a, b, c, m1, max;
            Console.Write("Nhập a,b,c : ");
            a = double.Parse(Console.ReadLine());
            b = double.Parse(Console.ReadLine());
            c = double.Parse(Console.ReadLine());

            if(a>b)
            {
                m1 = a;
            }
            else
            {
                m1 = b;
            }
            if(m1>c)
            {
                max = m1;
            }
            else
            {
                max = c;
            }
            Console.WriteLine($"a ={a}, b={b},c={c}");
            Console.WriteLine($"Giá trị lớn nhất trong ba số là {max}");
        }

        private void bt2()
        {
            /*nhập vào điểm số của các môn học. Thang để phân loại điểm được biểu diễn trên trục số.*/
            /*vd: 0-----5-----8-----10 (diem)*/
            //dùng if lồng

            double diem;
            Console.Write("nhập điểm: ");
            diem = double.Parse(Console.ReadLine());

            if(diem >=8.0)
            {
                if (diem > 10)
                {
                    Console.WriteLine("điểm không hợp lệ");
                }
                else
                {
                    Console.WriteLine(" Xếp loại A");
                }
            }
            else
            {
                if (diem >= 5.0)
                {
                    Console.WriteLine("Xếp loại B");
                }
                else
                {
                    if (diem >= 0)
                    {
                        Console.WriteLine("Xếp loại C");
                    }
                    else
                    {
                        Console.WriteLine("điểm không hợp lệ");
                    }
                }
                Console.ReadKey();
            }

        }

        /// ///////////////////////////////////////////////////////////////////////////
        /*Switch-Case*/
        /*cú pháp
         
        switch (biến_kiểm_tra)
        {
            case giá_trị_1:
                // Các lệnh thực thi nếu biến_kiểm_tra == giá_trị_1
                break;

            case giá_trị_2:
                // Các lệnh thực thi nếu biến_kiểm_tra == giá_trị_2
                break;

            // Có thể thêm nhiều case khác...

            default:
                // Các lệnh thực thi nếu không khớp với bất kỳ case nào
                break;
         }
         */

        private void bt3()
        {
            /*Nhập 2 số a, b tính + - * / */

            double a, b;
            int pt;

            Console.Write("Nhập a,b : ");
            a = double.Parse(Console.ReadLine());
            b = double.Parse(Console.ReadLine());

            do
            {
                Console.WriteLine("Menu");
                Console.WriteLine("1. Cộng");
                Console.WriteLine("2. Trừ");
                Console.WriteLine("3. Nhân");
                Console.WriteLine("4. Chia");
                pt = Int32.Parse(Console.ReadLine());

                switch(pt)
                {
                    case 1:
                        double tong = a + b;
                        Console.WriteLine($"Tổng 2 số {a} và {b} = {tong}");
                        break;
                    case 2:
                        double tru = a - b;
                        Console.WriteLine($"Hiệu 2 số {a} và {b} = {tru}");
                        break;
                    case 3:
                        double nhan = a * b;
                        Console.WriteLine($"Tích 2 số {a} và {b} = {nhan}");
                        break;
                    case 4:
                        if (b == 0)
                        {
                            Console.WriteLine($"Thương bằng không vì số b bằng 0");
                        }
                        else
                        {
                            double chia = a / b;
                            Console.WriteLine($"thương 2 số {a} và {b} = {chia}");
                        }
                        break;
                    case 5:
                        Console.WriteLine("đã thoát chế độ");
                        break;

                }
                Console.ReadKey();
            }
            while (pt != 5);
            {

            }
        }


        /*Vòng lặp*/
        /*For 
         cú pháp: 
         for (bt khởi tạo; bt điều kiện; bt thay đổi)
         {
            Câu lệnh;
            Câu lệnh;
         }
         */
         private void bt4()
         {
            /*Ví dụ đơn giản*/
            for (int i =1; i<= 5; i++)
            {
                Console.WriteLine("số thư" + i);
            }
         }

        private void bt5()
        {
            /*Hiển thị bình phương của số nguyên i từ 0 - 10*/
            for ( int i = 0; i<=10; i++)
            {
                int binhphuong = i * i;
                Console.WriteLine($"Bình phương của {i} là {binhphuong}");
                Console.ReadKey();
            }
        }

        private void bt6()
        {
            /*Hiển thị các cặp giá trị i và J đồng thời đầu và cuối vd i đầu , j cuối. */
            int i, j;
            for (i=0,j=5; (i<4) && (j>2); i++,j--)
            {
                Console.WriteLine($"i:{i},j:{j}");
                Console.ReadKey();
            }

            //Hãy thử với ||
        }

        /* vòng lặp do while : thực hiện trước kiểm tra sau*/
        /*
         cú pháp
            do
            {
                // Các câu lệnh cần lặp
            }
            while (điều_kiện);
             
            - do: bắt đầu khối lệnh cần lặp.
              while (điều_kiện): kiểm tra điều kiện sau khi thực hiện khối lệnh.
            - Nếu điều kiện đúng, vòng lặp tiếp tục.
            - Nếu điều kiện sai, vòng lặp kết thúc
        */
        private void bt7()
        {
            /*nhập số nguyên dương*/
            int number;
            do
            {
                Console.Write("Nhập một số nguyên dương: ");
                number = Convert.ToInt32(Console.ReadLine());
            }
            while (number <= 0);
            Console.WriteLine("Bạn đã nhập: " + number);
        }

        private void bt8()
        {
            /*Tính bình phương giá trị i với bước thay đổi 0.2 từ 0 đến 2*/
            double i = 0;
            do
            {
                double binhphuong = i * i;
                Console.WriteLine($"Bình phương của {i} là {binhphuong}");
                i += 0.2; // tương đương i=i+0.2
            }
            while (i<2);
            Console.ReadKey();
        }

        private void bt9()
        {
            /*Hiển thời đồng thời hai biến i,j với giá trị từ 2 đến 1 vớ bước lùi bằng 0.2 và tăng j 0-2 với bước tăng 0.1*/
            double i = 2, j = 0;
            do
            {
                Console.WriteLine($"i={i}, j={j}");
                i -= 0.2;
                j += 0.1;
            }
            while ((i>1) && (j<2));
            Console.ReadKey();
        }

        /*foreach*/
        /*
         Cú pháp
         
            foreach (kiểu_dữ_liệu biến in tập_hợp)
            {
                // Các câu lệnh xử lý từng phần tử
            }

            kiểu_dữ_liệu: kiểu của phần tử trong tập hợp (ví dụ: int, string, var).
            biến: tên biến đại diện cho từng phần tử khi duyệt.
            tập_hợp: mảng, danh sách, hoặc tập dữ liệu có thể lặp.

         */
        private void bt10()
        {
            /*duyệt mảng số nguyên*/
            int[] numbers = { 1, 2, 3, 4, 5 };

            foreach(int num in numbers)
            {
                Console.WriteLine($"Số: {num}");
            }
            Console.ReadKey();
        }

        private void bt11()
        {
            /*Khai báo mảng một chiều các số nguyên được gán trước
             * bao gồm cả số lẻ và số chẵn. Hãy hiển thị số lượng các số lẻ và số lượng
             * các số chẵn có trong mảng đó.*/
            int[] arr = new int[5] { 1, 4, 3, 6, 5 };
            int sumChan = 0, sumLe = 0;
            foreach(int m in arr)
            {
                if(m%2 ==0)
                {
                    sumChan++;
                }
                else
                {
                    sumLe++;
                }
            }
            Console.WriteLine($"Số phần tử chẵn là {sumChan}");
            Console.WriteLine($"Số phần tử lẽ là {sumLe}");
            Console.ReadKey();
        }

        /*Lệnh Break;*/
        private void bt12()
        {
            /*in ra gai thừa của của số từ 1 đến 100 nếu giai thừa số chẵn và  lớn hơn 150 thì thoát không in nữa*/
            for(int i=0; i<=100; i++)
            {
                int giaithua = i * i;
                if(giaithua >150)
                {
                    break;
                }
                if(i%2 == 0)
                {
                    Console.Write(" " + i);
                }

                Console.ReadKey();
            }
        }
    }
}
