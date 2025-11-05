 private void BtnSum_Click(object sender, EventArgs e)
        {
            // Xử lý cộng hai số khi ấn nút
            double a, b;
            if (double.TryParse(tbA.Text, out a) && double.TryParse(tbB.Text, out b))
            {
                double sum = a + b;
                lblResult.Text = $"Tổng: {a} + {b} = {sum}";
            }
            else
            {
                lblResult.Text = "Vui lòng nhập đúng số!";
            }
        }
