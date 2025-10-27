using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace basic
{
    // định nghĩa một lớp
    class Student 
    {
        private int ID;
        private string name;

        // các ham định nghĩa bên trong lớp được gọi là thành viên
        public Student()
        {

        }
        public void Input() 
        { }

        public void Show()
        { }

        Student s1 = new Student();// khai báo đối tượng s1 thuộc lớp S1
        //s1.Input(); // truy cập S1 với các hàm thành viên

    }

    // ví dụ 1:
    class person
    {
        private String name;
        private int age;
        private double salary;

        public person() // Hàm tạo- Contructor // Hàm không có đối số
        {
            name = "";
            age = 0;
            salary = 0;
        }

        //public person( string name, int age, double salary)
        //{
        //    this.name = name; // dùng this khi tên các đối số trùng với các biến private bên trong lớp
        //    this.age = age;
        //    this.salary = salary;
        //}

        public void input()
        {
            Console.Write("Nhập tên: ");
            name = Console.ReadLine();
            Console.Write("Nhập tuổi ");
            age = Int32.Parse(Console.ReadLine());
            Console.Write("Nhập lương: ");
            salary = double.Parse(Console.ReadLine());
        }

        public void show()
        {
            Console.WriteLine($"tên:{name}, tuổi:{age}, lương:{salary}");
        }

        
    }

    //--------Cách dùng bên Main-----//
    //person p = new person();
    //p.input();
    //p.show();
    //Console.ReadLine();

    /*
     * Hàm tạo là hàm để tạo dữ liệu cho đối số
     *Contructor là gì?
     * 
         - Constructor là một hàm có cùng tên với lớp.
         - Không có kiểu trả về (không dùng void, int, v.v.).
         - Được dùng để khởi tạo giá trị ban đầu cho các thuộc tính (biến) của đối tượng.

     * Lợi ích của contructor
        - Tự động khởi tạo đối tượng: Giúp đảm bảo rằng mọi đối tượng đều có giá trị ban đầu hợp lệ.
        - Tăng tính bảo trì: Nếu cần thay đổi cách khởi tạo, chỉ cần sửa trong constructor.
        - Hỗ trợ đa hình (Overloading): Có thể tạo nhiều constructor với tham số khác nhau để linh hoạt khi khởi tạo.
        - Tích hợp logic khởi tạo: Có thể thêm kiểm tra, tính toán, hoặc gọi hàm khác khi khởi tạo.
         
         */

    // Hàm cso 2 loại hàm có đối số và hàm không có đối số


    //--------------------------------------------------------------------------------///
    //Kiểu PROPERTY- Thuộc tính
    /* đặt tính:
       - có tên và kiểu thuộc private static
       - có thể gán giá trị dữ liệu qua việc truy cập tên đối tượng và tên nhờ toán tử chấm
     */

    class Property1
    {
        private int ID; // mã số
        private string name;
       
        public int ID1 // luôn có từ khóa pulic, kiểu dữ liệu trùng với kiểu của ID
        {
            get { return ID; } //Chỉ một lệnh  return ID
            set { ID = value; } // chỉ một l
        }

        public string name1
        {
            get { return name; }
            set { name = value; }
        }

        public void show()
        {
            Console.WriteLine($"ID= {ID}; Tên: {name}");
        }


        //-----Cách dùng bên Main-----//
        //Property1 p1 = new Property1();
        //p1.ID1 = 012356;
        //p1.name1 = "Mr.VU";
        //p1.show();
        //Console.ReadLine();
    }

    //--------------------------------------------------------------------------------------------//
    //-----Tính kế thừa-------
    // Hiểu đôn giản là tận dụng cái cũ đã có và có thêm nhứng tính năng mới

    class Base1 //lớp cơ sở
    {
        private Type data1;
        private Type data2;

        public void methodBase1()
        {

        }

        public void methodBase2()
        {

        }
    }

    class mid1:Base1 // lớp dẫn xuất 1
    {
        private Type data3; // dữ liệu riêng của lớp mới


        public new void methodBase1() //Thêm chữ new để kế thừa
        {

        }

        public new void methodBase2() //Thêm chữ new để kế thừa
        {

        }

        public void methodMid1_1()
        {

        }

        public void methodMid2_2()
        {

        }
    }

    class mid2:Base1 // lớp dẫn xuất 2
    {
        private Type data4;

        public new void methodBase1() //Thêm chữ new để kế thừa
        {

        }

        public new void methodBase2() //Thêm chữ new để kế thừa
        {

        }

        public void methodMid2_1()
        {

        }

        public void methodMid2_2()
        {

        }
    }

    //Ví dụ: 
    //Hãy viết chương trình có thừa kế để quản lý các lớp thuộc
    //lớp cơ sở Employee(nhân viên) là: lớp các nhà khoa học(Scientist), lớp
    //các nhà quản lý(Manager) và lớp người công nhân(Worker).

    class Employee
    {
        private int ID;
        private string name;

        public Employee()
        {
            ID = 0;
            name = "";
        }

        public void Input()
        {
            Console.Write("Nhập ID: ");
            ID = Int32.Parse(Console.ReadLine());
            Console.Write("Nhập tên: ");
            name = Console.ReadLine();
        }

        public void Show()
        {
            Console.Write($"ID: {ID}");
            Console.WriteLine($"Tên: {name}");
        }
    }
   //****************************************************
        class Scientist : Employee
        {
            private int pub;
            public Scientist() : base()
            {
                pub = 0;
            }

            public new void Input()
            {
                base.Input();
                Console.Write("Nhập số công trình khoa học: ");
                pub = Int32.Parse(Console.ReadLine());
            }

            public new void Show()
            {
                base.Show();
                Console.WriteLine($"; Công trình khoa học: {pub}");
            }
        }

    //*****************************************************

        class Manager: Employee
        {
            private string contract;
            public Manager():base()
            {
                contract = "";
            }

            public new void Input()
            {
                base.Input();
                Console.Write("Nhập nội dung hợp đồng: ");
                contract = Console.ReadLine();
            }

            public new void Show()
            {
                base.Show();
                Console.WriteLine($"Hợp đồng: {contract}");
            }
        }

        class Worker :Employee
        {

        }

    //Scientist s1 = new Scientist();
    //Console.WriteLine("Nhà khoa học.");
    //s1.Input();
    //s1.Show();
    //Console.WriteLine("Nhà quản lí.");
    //Manager m1 = new Manager();
    //m1.Input();
    //m1.Show();
    //Worker w1 = new Worker();
    //w1.Input();
    //w1.Show();
    //Console.ReadLine();

    // Từ khóa Virture và override
    // cú pháp
    class Base2
    {
        private int a, b;
        
        public virtual void Method1()
        {

        }
    }

    class Derived1: Base2
    {
        private int c;
        public override void Method1()
        {
            
        }
    }

    //Viết lại chương trinhf trên theo cú pháp virture override

    class Employee1
    {
        private int ID;
        private string name;
        public Employee1()
        {
            ID = 0;
            name = "";
        }


        public virtual void Input()
        {
            Console.Write("Nhập ID: ");
            ID = Int32.Parse(Console.ReadLine());
            Console.Write("Nhập tên: ");
            name = Console.ReadLine();
        }

        public virtual void Show()
        {
            Console.Write($"ID: {ID}");
            Console.WriteLine($"Tên: {name}");
        }
    }
        //---------------------------------
        class Scientist1: Employee1
        {
            private int pub;
            public Scientist1() : base()
            {
                pub = 0;
            }

            public override void Input()
            {
                base.Input();
                Console.Write("Nhập số công trình khoa học: ");
                pub = Int32.Parse(Console.ReadLine());
            }

            public override void Show()
            {
                base.Show();
                Console.WriteLine($"; Công trình khoa học: {pub}");
            }
        }

        class Manager1 : Employee1
        {
            private string contract;
            public Manager1() : base()
            {
                contract = "";
            }

            public override void Input()
            {
                base.Input();
                Console.Write("Nhập nội dung hợp đồng: ");
                contract = Console.ReadLine();
            }

            public override void Show()
            {
                base.Show();
                Console.WriteLine($"Hợp đồng: {contract}");
            }
        }

        class Worker1: Employee1
        {

        }
    //********----------------------------------//
    // VÍ dụ 2: Viết chương trình có thừa kế để tạo các cửa sổ khác nhau
                //khi ta đã định nghĩa cửa sổ Window chính.Có hại lớp Listbox và Button
                //thừa kế lớp cơ sở Window này.Ở đây ta sử dụng hai từ khoá virtual và
                //override để viết thừa kế.Trong hàm Main() ta sử dụng mảng đối tượng để
                //gán giá trị và hiển thị.

    class Window
    {
        protected int top;
        protected int left;

        public Window(int top, int left)
        {
            this.top = top;
            this.left = left;
        }
        public virtual void DrawWindow()
        {
            Console.WriteLine("Window: at {0},{1}", top, left);
        }
    }
    //-------------------
    class ListBox:Window
    {
        private string boxContent;
        public ListBox(int top, int left, string content): base(top, left)
        {
            boxContent = content;
        }

        public override void DrawWindow()
        {
            base.DrawWindow();
            Console.WriteLine($"Ghi nội dung vào của sổ listbox: {boxContent}");
        }
    }

    class Button: Window
    {
        public Button(int top, int left):base(top, left)
        {

        }
        public override void DrawWindow()
        {
            Console.WriteLine("vẽ một button at {0},{1}", top, left);
        }
    }

    //Window w = new Window(4,5);
    //ListBox lBox = new ListBox(7, 8, "Đây là một list box!");
    //Button b = new Button(10, 11);
    //w.DrawWindow();
    //lBox.DrawWindow();
    //b.DrawWindow();
    //Window[] winarr = new Window[3];
    //winarr[0] = new Window(11, 23);
    //winarr[1] = new ListBox(33, 44, "Một listbox khác");
    //winarr[2] = new Button(44, 55);
    //for(int i=0; i<3; i++)
    //{
    //    winarr[i].DrawWindow();
    //}
    //Console.ReadLine();

    //--------------------------------------------------------------------------------------------------//
    //*****************Tính đa hình*********************//
    /*
     * Hàm trùng tên được định nghĩa trong một lớp nếu chúng có tên
       giống nhau nhưng khác nhau về:
            - Số lượng các đối số.
            - Kiếu của các đối số (nếu có số lượng đối số giống nhau).
     */

    class Vector
    {
        private int[] a = new int[10];
        private int[] b = new int[10];
        private int[] c = new int[10];
        public int n;
        //-----------------\\
        public Vector()
        {
            for(int i =0; i<10; i++)
            {
                a[i] = b[i] = c[i] = 0;
            }
        }

        //-----------------\\
        public void Input(int []a, int n)
        {
            for(int i =0; i<n; i++)
            {
                a[i] = Int32.Parse(Console.ReadLine());
            }
        }
        //-------------------\\
        public void Input()
        {
            Console.Write("nhập số n: ");
            n = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Nhập vector a ");
            Input(a, n);
            Console.WriteLine("Nhập vector b ");
            Input(b, n);
        }

        //---------------------\\
        public void Add()
        {
            for(int i=0; i<n; i++)
            {
                a[i] = a[i] + b[i];
            }
        }
        //------------------------\\
        public void Show(int[]a, int n)
        {
            for(int i =0; i<n;i++)
            {
                Console.Write("" + a[i]);
            }
        }
        //------------------------\\
        public void Show()
        {
            Console.WriteLine("\nVector a");
            Show(a, n);
            Console.WriteLine("\nVector b");
            Show(b, n);
            Add();
            Console.WriteLine("\nVector c = a+b");
            Show(c, n);
        }

        //Vector ob = new Vector();
        //ob.Input();
        //ob.Show();
        //Console.ReadLine();
    }
    //******Toán tử trùng tên******\\


/*Bài tập mẫu*/
/*
 * 1.Tạo một lớp có tên CylinderCreator sử dụng NX Open API.  
 * 2.Lớp phải có:

        - Constructor để khởi tạo Session, WorkPart, và BlockFeatureBuilder.
        - Một phương thức CreateCylinder(double radius, double height) để tạo khối trụ tại gốc tọa độ.
        - Một phương thức ShowInfo() để hiển thị thông tin về khối trụ vừa tạo (tên, vị trí, kích thước).


    3. Viết một chương trình chính (Main) để:

        - Tạo đối tượng CylinderCreator.
        - Gọi phương thức CreateCylinder() với bán kính và chiều cao do người dùng nhập.
        - Gọi ShowInfo() để hiển thị thông tin.
 */
/*
using NXOpen;
using NXOpen.Features;
using NXOpen.GeometricUtilities;
using NXOpen.UF;

public class CylinderCreator
    {
        private Session theSession;
        private Part workPart;
        private Features.Feature cylinderFeature;

        // Constructor: khởi tạo môi trường NX
        public CylinderCreator()
        {
            theSession = Session.GetSession();
            workPart = theSession.Parts.Work;
        }

        // Tạo khối trụ tại gốc tọa độ
        public void CreateCylinder(double radius, double height)
        {
            // Tạo khối trụ bằng cách sử dụng Extrude hình tròn
            Point3d center = new Point3d(0, 0, 0);
            Vector3d direction = new Vector3d(0, 0, 1);

            // Tạo sketch hình tròn
            Sketch sketch = CreateCircleSketch(center, radius);

            // Tạo khối trụ bằng cách extrude sketch
            Features.ExtrudeBuilder extrudeBuilder = workPart.Features.CreateExtrudeBuilder(null);
            extrudeBuilder.Section = sketch.Section;
            extrudeBuilder.Direction = workPart.Directions.CreateDirection(center, direction, SmartObject.UpdateOption.WithinModeling);
            extrudeBuilder.Limits.StartExtend.Value.RightHandSide = "0";
            extrudeBuilder.Limits.EndExtend.Value.RightHandSide = height.ToString();
            extrudeBuilder.BooleanOperation.Type = BooleanOperation.BooleanType.Create;

            cylinderFeature = extrudeBuilder.CommitFeature();
            extrudeBuilder.Destroy();
        }

        // Hiển thị thông tin khối trụ
        public void ShowInfo()
        {
            ListingWindow lw = theSession.ListingWindow;
            lw.Open();
            lw.WriteLine("Khối trụ đã được tạo:");
            lw.WriteLine("Tên feature: " + cylinderFeature.Name);
            lw.WriteLine("Loại feature: " + cylinderFeature.FeatureType);
            lw.Close();
        }

        // Tạo sketch hình tròn
        private Sketch CreateCircleSketch(Point3d center, double radius)
        {
            SketchInPlaceBuilder sketchBuilder = workPart.Sketches.CreateSketchInPlaceBuilder2(null);
            sketchBuilder.PlaneReference = workPart.ModelingViews.WorkView.Orientation;
            Sketch sketch = sketchBuilder.Commit();
            sketchBuilder.Destroy();

            // Vẽ hình tròn
            workPart.Curves.CreateCircle(center, new Vector3d(0, 0, 1), radius);
            sketch.Update();

            return sketch;
        }
    }
 */


/*
 public class Program
{
   public static void Main(string[] args)
   {
       CylinderCreator creator = new CylinderCreator();

       // Nhập bán kính và chiều cao từ người dùng
       double radius = 10.0;
       double height = 50.0;

       creator.CreateCylinder(radius, height);
       creator.ShowInfo();
   }
}
*/
   class OOP_Ses1
   {

   }
}
