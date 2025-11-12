using System;
using System.Windows.Forms;

namespace KhaiBaoYTe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            if (!string.IsNullOrEmpty(hoTen))
            {
                lstThongTin.Items.Add(hoTen);
                txtHoTen.Clear();
                CapNhatSoLuong();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo");
            }
        }

        private void btnXoaDangChon_Click(object sender, EventArgs e)
        {
            if (lstThongTin.SelectedIndex >= 0)
            {
                lstThongTin.Items.RemoveAt(lstThongTin.SelectedIndex);
                CapNhatSoLuong();
            }
        }

        private void btnXoaDau_Click(object sender, EventArgs e)
        {
            if (lstThongTin.Items.Count > 0)
            {
                lstThongTin.Items.RemoveAt(0);
                CapNhatSoLuong();
            }
        }

        private void btnXoaCuoi_Click(object sender, EventArgs e)
        {
            if (lstThongTin.Items.Count > 0)
            {
                lstThongTin.Items.RemoveAt(lstThongTin.Items.Count - 1);
                CapNhatSoLuong();
            }
        }

        private void btnXoaTatCa_Click(object sender, EventArgs e)
        {
            lstThongTin.Items.Clear();
            CapNhatSoLuong();
        }

        private void CapNhatSoLuong()
        {
            txtTongSo.Text = lstThongTin.Items.Count.ToString();
        }
    }
}
//////bài taapj4//////////////////////////

using System;
using System.Windows.Forms;

namespace DangNhapApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo");
                return;
            }

            // Kiểm tra tài khoản (ví dụ cứng)
            if (username == "admin" && password == "123456")
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }
    }
}

///////////////////////////////////////////cải tiến khi đăng nhập sẽ hiển thị form 
// form login//
using System;
using System.Windows.Forms;

namespace DangNhapApp
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
            txtMatKhau.UseSystemPasswordChar = true;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo");
                return;
            }

            // Kiểm tra tài khoản (ví dụ cứng)
            if (username == "admin" && password == "123456")
            {
                MessageBox.Show("Đăng nhập thành công!", "Thông báo");

                // Mở form khai báo y tế
                FormKhaiBao frm = new FormKhaiBao();
                frm.Show();

                // Ẩn form đăng nhập
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void chkHienMatKhau_CheckedChanged(object sender, EventArgs e)
        {
            txtMatKhau.UseSystemPasswordChar = !chkHienMatKhau.Checked;
        }
    }
}

// form khai báo y tế//

using System;
using System.Windows.Forms;

namespace DangNhapApp
{
    public partial class FormKhaiBao : Form
    {
        public FormKhaiBao()
        {
            InitializeComponent();
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            string hoTen = txtHoTen.Text.Trim();
            if (!string.IsNullOrEmpty(hoTen))
            {
                lstThongTin.Items.Add(hoTen);
                txtHoTen.Clear();
                CapNhatSoLuong();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo");
            }
        }

        private void btnXoaDangChon_Click(object sender, EventArgs e)
        {
            if (lstThongTin.SelectedIndex >= 0)
            {
                lstThongTin.Items.RemoveAt(lstThongTin.SelectedIndex);
                CapNhatSoLuong();
            }
        }

        private void btnXoaDau_Click(object sender, EventArgs e)
        {
            if (lstThongTin.Items.Count > 0)
            {
                lstThongTin.Items.RemoveAt(0);
                CapNhatSoLuong();
            }
        }

        private void btnXoaCuoi_Click(object sender, EventArgs e)
        {
            if (lstThongTin.Items.Count > 0)
            {
                lstThongTin.Items.RemoveAt(lstThongTin.Items.Count - 1);
                CapNhatSoLuong();
            }
        }

        private void btnXoaTatCa_Click(object sender, EventArgs e)
        {
            lstThongTin.Items.Clear();
            CapNhatSoLuong();
        }

        private void CapNhatSoLuong()
        {
            txtTongSo.Text = lstThongTin.Items.Count.ToString();
        }
    }
}

//// chỉnh sửa luồng logic///

//Đặt FormLogin là Startup Form trong Program.cs:
Application.Run(new FormLogin());

//Khi đăng nhập đúng → mở FormKhaiBao bằng frm.Show() và ẩn FormLogin.

