using System.Data;
using System.Data.SqlClient;
using Check_Loi;
namespace CaffeeShop
{
    public partial class FormManager : Form
    {
        private Manager manager = new Manager();

        public FormManager()
        {
            InitializeComponent();
        }
        public FormManager(Manager a)
        {
            this.manager = a;
            InitializeComponent();
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            chkCf.Checked = true;
            dgvNhanVien.DataSource = manager.ViewShopData("NhanVien");
            dgvCoffee.DataSource = manager.ViewShopData("Product WHERE STATUS = 1");
            dgvFeedBack.DataSource = manager.ViewFeedBack(new DateTime(1, 1, 1), DateTime.Now);
            //
            radioDaHoanThanh.Checked = true;
            loadDoanhThu(new DateTime(1, 1, 1), DateTime.Now, 1);
            //
            //
            dgvKhachHang.DataSource = manager.ViewShopData("Customer");
            //
            txtquatity.Value = 1;
        }

        //--------------------------------- quản lý nhân viên ---------------------------------
        /// <summary>
        /// chỉnh sửa nhân viên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFixNhanVien_Click(object sender, EventArgs e)
        {
            ResetCheckLoiNhanVien();
            if (!CheckLoiNhanVien()) return;
            DialogResult result = MessageBox.Show("Xác nhận sửa nhân viên " + txtNameNhanVien.Text + " ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Barista ba = new Barista(txtIdNhanVien.Text, txtNameNhanVien.Text, txtPhoneNhanVien.Text, txtPassNhanVien.Text, dtpkNhanVien.Value, Convert.ToInt32(txtSalaryNhanVien.Text), txtAccNhanVien.Text);
                    if (manager.FixEmployee(ba))
                    {
                        dgvNhanVien.DataSource = manager.ViewShopData("NhanVien");
                        MessageBox.Show("Sửa nhân viên " + ba.Name + " thành công !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIdNhanVien.Text = ""; txtNameNhanVien.Text = ""; txtPhoneNhanVien.Text = ""; txtPassNhanVien.Text = "";
                        txtSalaryNhanVien.Text = ""; txtAccNhanVien.Text = "";
                    }
                    else
                        MessageBox.Show("Account mà bạn muốn đổi đã tồn tại , không thể đổi !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Notice");
                }
            }
        }


        private bool CheckLoiNhanVien()
        {
            bool flag = true;
            if (!CheckLoi.AccountAndId(txtIdNhanVien.Text))
            {
                lblCheckLoiIDNhanVien.Text = "Id chỉ chưa chữ cái và số ! độ dài <= 10 ";
                flag = false;
            }
            if (!CheckLoi.AccountAndId(txtAccNhanVien.Text))
            {
                lblCheckloiAccount.Text = "Account chỉ chưa chữ cái và số ! độ dài <= 10";
                flag = false;
            }
            if (!CheckLoi.IsInt(txtSalaryNhanVien.Text))
            {
                lblCheckLoiSalary.Text = "Salary không hợp lệ";
                flag = false;
            }
            if (!CheckLoi.PassWord(txtPassNhanVien.Text))
            {
                lblCheckloiPassWord.Text = "PassWord chỉ chưa chữ cái và số ! độ dài <= 20";
                flag = false;
            }
            if (!CheckLoi.Name(txtNameNhanVien.Text))
            {
                lblCheckLoiName.Text = "Tên không hợp lệ ! độ dài <= 50";
                flag = false;
            }
            if (!CheckLoi.PhoneNumber(txtPhoneNhanVien.Text))
            {
                lblCheckloiPhoneNumber.Text = "Số điện thoại không hợp lệ ! độ dài <= 11";
                flag = false;
            }
            return flag;
        }
        private void ResetCheckLoiNhanVien()
        {
            lblCheckLoiIDNhanVien.Text = "!";
            lblCheckloiAccount.Text = "!";
            lblCheckLoiSalary.Text = "!";
            lblCheckLoiName.Text = "!";
            lblCheckloiPassWord.Text = "!";
            lblCheckloiPhoneNumber.Text = "!";
        }
        private void btnAddNhanVien_Click(object sender, EventArgs e)
        {
            ResetCheckLoiNhanVien();
            if (!CheckLoiNhanVien()) return;
            DialogResult result = MessageBox.Show("Xác nhận thêm nhân viên " + txtNameNhanVien.Text + " ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    Barista ba = new Barista(txtIdNhanVien.Text, txtNameNhanVien.Text, txtPhoneNhanVien.Text, txtPassNhanVien.Text, dtpkNhanVien.Value, Convert.ToInt32(txtSalaryNhanVien.Text), txtAccNhanVien.Text);
                    if (!manager.AddEmployee(ba))
                        MessageBox.Show("Account đã tồn tại không thể thêm !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        MessageBox.Show("Thêm thành công !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvNhanVien.DataSource = manager.ViewShopData("NhanVien");
                        txtIdNhanVien.Text = ""; txtNameNhanVien.Text = ""; txtPhoneNhanVien.Text = ""; txtPassNhanVien.Text = "";
                        txtSalaryNhanVien.Text = ""; txtAccNhanVien.Text = "";
                    }
                }
                catch
                {
                    MessageBox.Show("ID đã tôn tại !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// xóa nhân viên 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteNhanVien_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.CurrentRow == null)
            {
                MessageBox.Show("Bạn chưa chọn nhân viên muốn xóa", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DialogResult result = MessageBox.Show("Xác nhận xóa nhân viên " + dgvNhanVien.CurrentRow.Cells[1].Value.ToString() + " ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                manager.DeleteEmployee(dgvNhanVien.CurrentRow.Cells[0].Value.ToString());
                dgvNhanVien.DataSource = manager.ViewShopData("NhanVien");
                MessageBox.Show("Xóa thành công !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtIdNhanVien.Text = ""; txtNameNhanVien.Text = ""; txtPhoneNhanVien.Text = ""; txtPassNhanVien.Text = "";
                txtSalaryNhanVien.Text = ""; txtAccNhanVien.Text = "";
            }
        }


        //---------------------------------------- chỉnh sửa product -----------------------------
        private bool CheckLoiProduct()
        {
            bool flag = true;
            if (!CheckLoi.AccountAndId(txtIDcoffee.Text))
            {
                lblCheckloiIdProduct.Text = "Id chỉ chưa chữ cái và số !";
                flag = false;
            }
            if (!CheckLoi.IsInt(txtPriceCoffee.Text))
            {
                lblcheckLoiPriceProduct.Text = "Giá không hợp lệ";
                flag = false;
            }
            if (!CheckLoi.Name(txtNameCoffee.Text))
            {
                lblCheckLoiNameProduct.Text = "Tên không hợp lệ !";
                flag = false;
            }
            return flag;
        }
        private void ResetCheckLoiProduct()
        {
            lblCheckloiIdProduct.Text = "!";
            lblcheckLoiPriceProduct.Text = "!";
            lblCheckLoiNameProduct.Text = "!";
        }

        /// <summary>
        /// thêm product 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddCoffee_Click(object sender, EventArgs e)
        {
            ResetCheckLoiProduct();
            if (!CheckLoiProduct()) return;
            DialogResult result = MessageBox.Show("Xác nhận thêm sản phẩm " + txtNameCoffee.Text + " ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    LoaiProduct typeCf;
                    if (chkCf.Checked) { typeCf = LoaiProduct.Coffee; }
                    else if (chkMilk.Checked) { typeCf = LoaiProduct.Milk; }
                    else { typeCf = LoaiProduct.Tea; }
                    Product newProduct = new Product(txtIDcoffee.Text, txtNameCoffee.Text, Convert.ToInt32(txtPriceCoffee.Text), typeCf, (int)txtquatity.Value);
                    manager.AddProduct(newProduct);
                    dgvCoffee.DataSource = manager.ViewShopData("Product WHERE STATUS = 1");
                    MessageBox.Show("Thêm thành công !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch
                {
                    MessageBox.Show("Id đã tồn tại !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        /// <summary>
        /// xóa product 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            if (dgvCoffee.CurrentRow == null)
            {
                MessageBox.Show("Bạn chưa chọn Coffee muốn xóa !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult result = MessageBox.Show("Xác nhận xóa sản phẩm " + dgvCoffee.CurrentRow.Cells[1].Value.ToString() + " ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                manager.DeleteProduct(dgvCoffee.CurrentRow.Cells[0].Value.ToString());
                dgvCoffee.DataSource = manager.ViewShopData("Product WHERE STATUS = 1");
                MessageBox.Show("Xóa thành công !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnFixProduct_Click(object sender, EventArgs e)
        {
            ResetCheckLoiProduct();
            if (!CheckLoiProduct()) return;
            DialogResult result = MessageBox.Show("Xác nhận xóa sản phẩm " + dgvCoffee.CurrentRow.Cells[1].Value.ToString() + " ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                try
                {
                    LoaiProduct typeCf;
                    if (chkCf.Checked) { typeCf = LoaiProduct.Coffee; }
                    else if (chkMilk.Checked) { typeCf = LoaiProduct.Milk; }
                    else { typeCf = LoaiProduct.Tea; }
                    Product newProduct = new Product(txtIDcoffee.Text, txtNameCoffee.Text, Convert.ToInt32(txtPriceCoffee.Text), typeCf, (int)txtquatity.Value);
                    manager.FixProduct(newProduct);
                    dgvCoffee.DataSource = manager.ViewShopData("Product WHERE STATUS = 1");
                    MessageBox.Show("Sửa product " + newProduct.Name + " thành công !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXemchitiet_Click(object sender, EventArgs e)
        {
            dgvFeedBack.DataSource = manager.ViewFeedBack(dateTimebefore.Value, dateTimePickerAfter.Value);
        }


        private void txtquatity_ValueChanged(object sender, EventArgs e)
        {
            if (txtquatity.Value <= 0) txtquatity.Value = 1;
        }

        /// <summary>
        /// load doanh thu
        /// </summary>
        /// <param name="before"></param>
        /// <param name="after"></param>
        private void loadDoanhThu(DateTime before, DateTime after, int dahoanthanh)
        {
            dgvDoanhThu.DataSource = manager.ViewOrder(before, after, dahoanthanh);
            lbldoanhthu.Text = "Doanh thu: " + manager.DoanhThu(before, after).ToString();
            lblOrderThanhcong.Text = "Số Order thành công: "+ manager.OrderCount(before, after, 1).ToString();
            lblOrderDangXuLy.Text = "Số Order đang được xử lý: " + manager.OrderCount(before, after, 0).ToString();

        }

        private void btnXemDoanhThu_Click(object sender, EventArgs e)
        {
            if (radioChuaHoanThanh.Checked) loadDoanhThu(DateTimePickerDoanhThuBefore.Value, dateTimePickerAfterDoanhThu.Value, 0);
            else loadDoanhThu(DateTimePickerDoanhThuBefore.Value, dateTimePickerAfterDoanhThu.Value, 1);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ấn các nút radio đơn hàng đã hoàn thành , chưa hoàn thành 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radioDaHoanThanh_CheckedChanged(object sender, EventArgs e)
        {
            loadDoanhThu(DateTimePickerDoanhThuBefore.Value, dateTimePickerAfterDoanhThu.Value, 1);
        }

        private void radioChuaHoanThanh_CheckedChanged(object sender, EventArgs e)
        {
            loadDoanhThu(DateTimePickerDoanhThuBefore.Value, dateTimePickerAfterDoanhThu.Value, 0);
        }

        private void btnNap_Click(object sender, EventArgs e)
        {
            lblCheckLoiNaptienchokhach.Text = "!";
            if (dgvKhachHang.CurrentRow == null)
            {
                MessageBox.Show("Xin vui lòng chọn khách hàng muốn nạp tiền !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!CheckLoi.IsInt(txtSoTienNap.Text))
            {
                lblCheckLoiNaptienchokhach.Text = "Số tiền nạp không hợp lệ !";
                return;
            }
            DialogResult result = MessageBox.Show("Xác nhận nạp " + txtSoTienNap.Text.Trim() + " cho Customer " + dgvKhachHang.CurrentRow.Cells[1].Value.ToString(), "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                manager.NapTienChoKhach(dgvKhachHang.CurrentRow.Cells[0].Value.ToString(), Convert.ToInt32(txtSoTienNap.Text));
                dgvKhachHang.DataSource = manager.ViewShopData("Customer");
            }
        }

        private void dgvNhanVien_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

            int i = dgvNhanVien.CurrentRow.Index;
            txtIdNhanVien.Text = dgvNhanVien.Rows[i].Cells[0].Value.ToString().Trim();
            txtNameNhanVien.Text = dgvNhanVien.Rows[i].Cells[1].Value.ToString().Trim();
            txtPhoneNhanVien.Text = dgvNhanVien.Rows[i].Cells[2].Value.ToString().Trim();
            txtSalaryNhanVien.Text = dgvNhanVien.Rows[i].Cells[3].Value.ToString().Trim();
            dtpkNhanVien.Value = (DateTime)dgvNhanVien.Rows[i].Cells[4].Value;
            txtAccNhanVien.Text = dgvNhanVien.Rows[i].Cells[5].Value.ToString().Trim();
            txtPassNhanVien.Text = dgvNhanVien.Rows[i].Cells[6].Value.ToString().Trim();
        }
        /// <summary>
        /// load thông tin product khi chọn vào 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvCoffee_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvCoffee.CurrentRow.Index;
            txtIDcoffee.Text = dgvCoffee.Rows[i].Cells[0].Value.ToString().Trim();
            txtNameCoffee.Text = dgvCoffee.Rows[i].Cells[1].Value.ToString().Trim();
            txtPriceCoffee.Text = dgvCoffee.Rows[i].Cells[2].Value.ToString().Trim();
            txtquatity.Text = dgvCoffee.Rows[i].Cells[3].Value.ToString().Trim();

            int flag = Convert.ToInt32(dgvCoffee.Rows[i].Cells[4].Value.ToString());
            if (flag == 1) chkCf.Checked = true;
            else if (flag == 2) chkMilk.Checked = true;
            else chkTea.Checked = true;
        }

        private void dgvFeedBack_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvFeedBack.CurrentRow.Index;
            txtHienThiContent.Text = dgvFeedBack.Rows[i].Cells[2].Value.ToString();
        }

        private void guna2TabControl1_Click(object sender, EventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (tabControl != null)
            {
                // Lấy chỉ số của tab được chọn
                int selectedIndex = tabControl.SelectedIndex;
                // Lấy thông tin về tab theo chỉ số
                if (selectedIndex == 6)
                    Close();
                else if (selectedIndex == 5)
                {
                    Hide();
                    FormChangePassWord changePassWord = new FormChangePassWord(manager);
                    changePassWord.ShowDialog();
                    tabControl.SelectedIndex = 0;
                    Show();
                }
            }
        }
    }
}
