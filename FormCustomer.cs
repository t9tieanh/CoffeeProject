using System.Data;
using System.Data.SqlClient;
using Check_Loi;

namespace CaffeeShop
{
    public partial class FormCustomer : Form
    {
        private DataTable dataCoffee = new DataTable(); // lưu thông tin sản phẩm trong cửa hàng 
        private DataTable daDat = new DataTable(); // lưu thông tin sản phẩm khách hàng đã đặt 
        private Customer customer = new Customer(); // người đăng nhập với tư cách customer 
        public FormCustomer()
        {
            InitializeComponent();
        }

        public FormCustomer(Customer a)
        {
            customer = a;
            InitializeComponent();
        }

        private void FormCustomer_Load(object sender, EventArgs e)
        {
            //tạo các cột cho datatable dadat 
            daDat.Columns.Add("ID product", typeof(string));
            daDat.Columns.Add("Name Product", typeof(string));
            daDat.Columns.Add("Price", typeof(int));
            daDat.Columns.Add("Số lượng", typeof(string));
            dgvDadat.DataSource = daDat;
            //
            chkCoffee.Checked = true;
            dgvProduct.DataSource = customer.ViewProduct(1);  //loadProduct(1);
            //
            dgvYourOrder.DataSource = customer.ViewYourOrder(0);
            //
            dgvDaThanhToan.DataSource = customer.ViewYourOrder(1);
            //
            lblSodu.Text = customer.SoDu.ToString();
            //
            chkTienmat.Checked = true;
            DisplayComment();
        }


        /// <summary>
        /// chọn chk cf
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkCoffee_CheckedChanged(object sender, EventArgs e)
        {
            dgvProduct.DataSource = customer.ViewProduct(1);
        }

        /// <summary>
        /// chọn chk milk
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkMilk_CheckedChanged(object sender, EventArgs e)
        {
            dgvProduct.DataSource = customer.ViewProduct(2);
        }

        /// <summary>
        /// chọn chk Tea
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkTea_CheckedChanged(object sender, EventArgs e)
        {
            dgvProduct.DataSource = customer.ViewProduct(3);
        }

        /// <summary>
        /// không cho phép numquantity < 0
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numQuantity_ValueChanged(object sender, EventArgs e)
        {
            if (numQuantity.Value <= 0) numQuantity.Value = 1;
        }

        private void InHoaDon()
        {
            //tiến hành ghi file
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog(); // chọn chỗ lưu
            if (result == DialogResult.OK)
            {
                string filePath = folderBrowserDialog.SelectedPath;
                using (StreamWriter writer = new StreamWriter(filePath + "/HoaDonThanhToan.txt"))
                {
                    writer.WriteLine("Quán cà phê AS Coffee");
                    writer.WriteLine("Tên khách hàng :  " + customer.Name);
                    writer.WriteLine("Ngày đặt hàng :" + dgvYourOrder.CurrentRow.Cells[3].Value.ToString());
                    for (int i = 0; i < dgvDentailYourOrder.Rows.Count; i++)
                    {
                        writer.WriteLine("-----------------------------------");
                        writer.Write(dgvDentailYourOrder.Rows[i].Cells[0].Value?.ToString().Trim() + "  sl: ");
                        writer.Write(dgvDentailYourOrder.Rows[i].Cells[1].Value?.ToString().Trim() + "  gia: ");
                        writer.Write(dgvDentailYourOrder.Rows[i].Cells[2].Value?.ToString().Trim() + "  ");
                        writer.WriteLine();
                    }
                    writer.WriteLine("-----------------------------------");
                    writer.WriteLine("Tổng tiền : " + dgvYourOrder.CurrentRow.Cells[1].Value.ToString());
                    writer.Close();
                }
                MessageBox.Show("Lưu hóa đơn thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// nút thêm hàng vào dgvdadat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDaDat1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn hàng bạn muốn thêm !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            for (int i = 0; i < daDat.Rows.Count; i++)
            {
                if (daDat.Rows[i]["ID product"].ToString() == txtID.Text)
                {
                    lbltotal.Text = (Convert.ToInt32(lbltotal.Text) + (int)numQuantity.Value * Convert.ToInt32(txtPrice.Text)).ToString();
                    daDat.Rows[i]["Số lượng"] = (Convert.ToInt32(numQuantity.Value) + Convert.ToInt32(daDat.Rows[i]["Số lượng"].ToString())).ToString();
                    dgvDadat.DataSource = daDat;
                    return;
                }
            }
            lbltotal.Text = (Convert.ToInt32(lbltotal.Text) + (int)numQuantity.Value * Convert.ToInt32(txtPrice.Text)).ToString();
            daDat.Rows.Add(txtID.Text, txtNameItem.Text, txtPrice.Text, numQuantity.Value);
            dgvDadat.DataSource = daDat;
        }

        private void SearchByName(string nameProduct)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "select idproduct as 'ID Product',namepro as N'Tên sản phẩm',pricepro as N'Giá' from product where namepro LIKE N'%" + nameProduct + "%'";
                adapter.SelectCommand = sqlCommand;
                dataCoffee.Clear();
                adapter.Fill(dataCoffee);
                dgvProduct.DataSource = dataCoffee;
            }
        }

        /// <summary>
        /// nút tìm sản phẩm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnsearchname1_Click(object sender, EventArgs e)
        {
            // loadProduct(txtName.Text);
            SearchByName(txtName.Text);
        }
        /// <summary>
        /// xóa sản phẩm từ dgvDadat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveProduct1_Click(object sender, EventArgs e)
        {
            if (dgvDadat.CurrentRow != null)
            {
                // Lấy chỉ mục của hàng được chọn
                int i = dgvDadat.CurrentRow.Index;
                lbltotal.Text = (Convert.ToInt32(lbltotal.Text) - Convert.ToInt32(dgvDadat.Rows[i].Cells[2].Value) * Convert.ToInt32(dgvDadat.Rows[i].Cells[3].Value)).ToString();
                daDat.Rows.RemoveAt(i);
                dgvDadat.DataSource = daDat;
            }
            else
                MessageBox.Show("Bạn chưa chọn mặt hàng", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        /// <summary>
        /// ấn nút đặt hàng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOrder1_Click(object sender, EventArgs e)
        {
            if (daDat.Rows.Count == 0)
            {
                MessageBox.Show("Order của bạn đang rỗng !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (daDat.Rows.Count == 1)
                customer.OrderCoffee(daDat.Rows[0]["ID product"].ToString(), Convert.ToInt32(daDat.Rows[0]["Số lượng"]), customer.Id, Convert.ToInt32(lbltotal.Text));
            else
            {
                List<(string, int)> product = new List<(string, int)>();
                foreach (DataRow row in daDat.Rows)
                    product.Add((row["ID product"].ToString(), Convert.ToInt32(row["Số lượng"])));
                customer.OrderCoffee(product, customer.Id, Convert.ToInt32(lbltotal.Text));
            }
            dgvYourOrder.DataSource = customer.ViewYourOrder(0);
            MessageBox.Show("Order thành công !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        /// <summary>
        /// chọn trong dgv product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvProduct_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvProduct.CurrentRow.Index;
            txtNameItem.Text = dgvProduct.Rows[i].Cells[1].Value.ToString();
            txtPrice.Text = dgvProduct.Rows[i].Cells[2].Value.ToString();
            txtID.Text = dgvProduct.Rows[i].Cells[0].Value.ToString();
        }

        private Panel creatComment(string acc, DateTime creat, string content)
        {
            Panel panel = new Panel();
            panel.Margin = new Padding(5);
            panel.AutoSize = true;
            Label userNameLabel = new Label();
            userNameLabel.AutoSize = true;
            userNameLabel.Text = acc;
            userNameLabel.Font = new Font(userNameLabel.Font, FontStyle.Bold);
            userNameLabel.Location = new Point(60, 10);
            panel.Controls.Add(userNameLabel);

            Label contentLabel = new Label();
            contentLabel.AutoSize = true;
            contentLabel.Text = content;
            contentLabel.Location = new Point(60, 40);
            panel.Controls.Add(contentLabel);

            Label timestampLabel = new Label();
            timestampLabel.AutoSize = true;
            timestampLabel.Text = creat.ToString("yyyy-MM-dd HH:mm:ss");
            timestampLabel.ForeColor = Color.Gray;
            timestampLabel.Location = new Point(60, 70);
            panel.Controls.Add(timestampLabel);
            return panel;
        }
        private void DisplayComment()
        {
            flcomment.Controls.Clear();
            using (SqlConnection con = new SqlConnection(ConnectionData.str))
            {
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Select Customer.AccountCustomer, FeedBack.Cont, FeedBack.CreatDate From Customer JOIN FeedBack ON Customer.IdCustomer = FeedBack.IdCustomer";
                SqlDataReader rd = cmd.ExecuteReader();
                while (rd.Read())
                {
                    Panel panl = creatComment(rd["AccountCustomer"].ToString().Trim(), Convert.ToDateTime(rd["CreatDate"].ToString()), rd["Cont"].ToString().Trim());
                    flcomment.Controls.Add(panl);
                }
            }
        }

        /// <summary>
        /// gửi feedback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void guna2PictureBox2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtContent.Text))
            {
                MessageBox.Show("Vui lòng nhập feedback !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!CheckLoi.FeedBack(txtContent.Text))
            {
                MessageBox.Show("FeedBack dài không quá 300 kí tự !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            customer.CreateFeedBack(txtContent.Text);
            DisplayComment();
            txtContent.Text = "";
            MessageBox.Show("Gửi thành công !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dgvDentailYourOrder_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvDentailYourOrder.CurrentCell.RowIndex;
            txtNameProductOrder.Text = dgvDentailYourOrder.Rows[i].Cells[0].Value.ToString();
            txtPriceOrder.Text = dgvDentailYourOrder.Rows[i].Cells[2].Value.ToString();
        }

        private void dgvYourOrder_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvYourOrder.CurrentCell == null) return;
            int i = dgvYourOrder.CurrentCell.RowIndex;
            dgvDentailYourOrder.DataSource = customer.ViewDentailOrder(dgvYourOrder.Rows[i].Cells[0].Value.ToString());
            txtCreateDate.Text = Convert.ToDateTime(dgvYourOrder.Rows[i].Cells[3].Value).ToString("dd-MM-yyyy");
        }

        private void btnThanhToan_Click_1(object sender, EventArgs e)
        {
            int i = dgvYourOrder.CurrentCell.RowIndex;
            if (!Convert.ToBoolean(dgvYourOrder.CurrentRow.Cells[2].Value))
                MessageBox.Show("Đơn hàng chưa được xử lý !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (Convert.ToBoolean(dgvYourOrder.Rows[i].Cells[4].Value) == true)
                MessageBox.Show("Bạn đã thanh toán cho mặt hàng này rồi !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                if (customer.PaymentProcessing(chkSoDuheThong.Checked, dgvYourOrder.Rows[i].Cells[0].Value.ToString()))
                {
                    MessageBox.Show("Thanh toán thành công !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvDentailYourOrder.DataSource = customer.ViewDentailOrder(dgvYourOrder.Rows[i].Cells[0].Value.ToString());
                    DialogResult result = MessageBox.Show("Bạn có muốn In Hóa đơn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                        InHoaDon();

                    lblSodu.Text = customer.SoDu.ToString();
                    dgvYourOrder.DataSource = customer.ViewYourOrder(0);
                    dgvDaThanhToan.DataSource = customer.ViewYourOrder(1);
                }
                else
                    MessageBox.Show("Thanh toán không thành thành công do số dư tài khoản không đủ vui lòng liên hệ Chủ quán để nạp tiền !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvDenTailDaThanhToan_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDenTailDaThanhToan.CurrentCell == null) return;
            int i = dgvDenTailDaThanhToan.CurrentCell.RowIndex;
            txtNameDaThanhToan.Text = dgvDenTailDaThanhToan.Rows[i].Cells[0].Value.ToString();
            txtPriceDaThanhToan.Text = dgvDenTailDaThanhToan.Rows[i].Cells[2].Value.ToString();

        }

        private void dgvDaThanhToan_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvDaThanhToan.CurrentRow == null) return;
            dgvDenTailDaThanhToan.DataSource = customer.ViewDentailOrder(dgvDaThanhToan.CurrentRow.Cells[0].Value.ToString());
            txtCreateDateDaThanhToan.Text = Convert.ToDateTime(dgvDaThanhToan.CurrentRow.Cells[3].Value).ToString("dd-MM-yyyy");
        }

        private void guna2TabControl1_Click(object sender, EventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (tabControl != null)
            {
                // Lấy chỉ số của tab được chọn
                int selectedIndex = tabControl.SelectedIndex;
                // Lấy thông tin về tab theo chỉ số
                if (selectedIndex == 5)
                    Close();
                else if (selectedIndex == 4)
                {
                    Hide();
                    FormChangePassWord changePassWord = new FormChangePassWord(customer);
                    changePassWord.ShowDialog();
                    tabControl.SelectedIndex = 0;
                    Show();
                }
            }
        }
    }
}
