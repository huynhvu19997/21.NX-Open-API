using System;
using System.Windows.Forms;

namespace TreeViewExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnAddRoot_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtNodeTitle.Text))
            {
                treeView1.Nodes.Add(txtNodeTitle.Text);
                txtNodeTitle.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập tiêu đề Node!");
            }
        }

        private void btnAddChild_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null && !string.IsNullOrWhiteSpace(txtNodeTitle.Text))
            {
                treeView1.SelectedNode.Nodes.Add(txtNodeTitle.Text);
                treeView1.SelectedNode.Expand(); // Mở rộng node cha
                txtNodeTitle.Clear();
            }
            else
            {
                MessageBox.Show("Chọn Node cha và nhập tiêu đề Node con!");
            }
        }

        private void btnRemoveAll_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
        }

        private void btnRemoveSelected_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                treeView1.SelectedNode.Remove();
            }
            else
            {
                MessageBox.Show("Chưa chọn Node để xóa!");
            }
        }

        private void btnCountNodes_Click(object sender, EventArgs e)
        {
            int count = CountNodes(treeView1.Nodes);
            MessageBox.Show($"Tổng số Node của TreeView: {count}");
        }

        private int CountNodes(TreeNodeCollection nodes)
        {
            int total = nodes.Count;
            foreach (TreeNode node in nodes)
            {
                total += CountNodes(node.Nodes);
            }
            return total;
        }
    }
}

/*

✅ Giao diện cần có

TreeView: treeView1
TextBox: txtNodeTitle
Các nút:

btnAddRoot → Thêm Node gốc
btnAddChild → Thêm Node con
btnRemoveAll → Xóa tất cả Node
btnRemoveSelected → Xóa Node đang chọn
btnCountNodes → Đếm tổng số Node
*/
