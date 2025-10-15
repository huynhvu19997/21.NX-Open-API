using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BG_mang_1_chieu
{
    class BG_mang_1_chieu
    {
        private void nhapmang(int[]a, int n)
        {
            /*Nhập mảng 1 chiều*/
            for (int i=0; i<n; i++)
            {
                a[1] = Int32.Parse(Console.ReadLine());
            }
        }

        private void summang(int[]a, int[]b,int[]c, int n)
        {
            for(int i=0;i<n; i++)
            {
                c[i] = a[i] + b[i];
               
            }
        }

        private void show(int[]a, int n)
        {
            for(int i=0; i<n; i++)
            {
                Console.Write($"{a[i]}");
            }
        }

        public void ProramMain()
        {
            int n;
            Console.Write("Nhập N: ");
            n = Int32.Parse(Console.ReadLine());
            int[] a = new int[n];
            int[] b = new int[n];
            int[] c = new int[n];
            Console.WriteLine("Nhập các phần tử mảng a:");
            nhapmang(a, n);
            Console.WriteLine("Nhập các phần tử mảng b:");
            nhapmang(b, n);
            summang(a, b, c, n);
            Console.WriteLine("Hiển thị các mảng a,b,c");
            Console.WriteLine("mảng a");
            show(a, n);
            Console.WriteLine("mảng b");
            show(b, n);
            Console.WriteLine("mảng c");
            show(c, n);
        }

        /*Tính tổng và tích 2 ma trận a,b nhập từ bàn phím*/
        private void nhapmatran(int[,]a,int m, int n)
        {
            for(int i=0; i<m;i++)
            {
                for (int j = 0; j < n; j++)
                {
                    a[i, j] = Int32.Parse(Console.ReadLine());
                }
            }     
        }

        private void cong2matran(int[,]a, int[,]b, int[,]c, int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                for(int j=0; j<n;j++)
                {
                    c[i, j] = a[i, j] + b[i, j];
                }
            }
        }

        private void Nhan2matran(int[,] a, int[,] b, int[,] d, int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    d[i, j] = 0;
                    for(int k=0;k<m;k++)
                    {
                        d[i, j] = a[i, k] * b[k, j];
                    }
                }
            }
        }

        private void In(int[,] a, int m, int n)
        {
            for (int i = 0; i < m; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.WriteLine($"{a[i, j]}");
                }
            }
        }

        public void Main_Mang2chieu_BT1()
        {
            int m, n;
            Console.Write("Nhập kích thước ma trận m,n: ");
            m = Int32.Parse(Console.ReadLine());
            n = Int32.Parse(Console.ReadLine());

            int[,] a = new int[m, n];
            int[,] b = new int[m, n];
            int[,] c = new int[m, n];
            int[,] d = new int[m, n];

            Console.Write("Nhập ma trận a");
            nhapmatran(a, m, n);
            Console.Write("Nhập ma trận b");
            nhapmatran(b, m, n);
            cong2matran(a, b, c, m, n);
            Nhan2matran(a, b, d, m, n);
        }
    }
}
