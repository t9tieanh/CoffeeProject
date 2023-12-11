using Check_Loi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaffeeShop
{
    public partial class FormEmployee : Form
    {
        Barista barista;
        public FormEmployee(Barista barista)
        {
            InitializeComponent();
            this.barista = barista;
            dgvbang.DataSource = barista.ViewSalary();
            dgvOrder.DataSource = barista.ViewCustomerOrder();
            //load Thong tin nhan vien
            txtid.Text = barista.Id;
            txtname.Text = barista.Name;
            txtpass.Text = barista.PassWord;
            txtphone.Text = barista.PhoneNumber;
            txtsalary.Text = barista.Salary.ToString();
            txtacc.Text = barista.Account;
            //
            lblthongtintk.Text = barista.PrintDetail();
        }

        private void btndathang_Click(object sender, EventArgs e)
        {
            if (dgvOrder.CurrentRow == null) return;
            DialogResult result = MessageBox.Show("Xác nhận chuẩn bị đơn hàng", "Notice", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (result == DialogResult.Yes)
            {
                int i = dgvOrder.CurrentRow.Index;
                string idOrder = dgvOrder.Rows[i].Cells[0].Value.ToString();
                if (!barista.PrepareCoffee(idOrder))
                {
                    MessageBox.Show("Kho hết hàng, không thể xác nhận", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                MessageBox.Show("Xác nhận chuẩn bị đơn hàng thành công !", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvOrder.DataSource = barista.ViewCustomerOrder();
            }
        }

        private void dgvOrder_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int i = dgvOrder.CurrentRow.Index;
            string idOrder = dgvOrder.Rows[i].Cells[0].Value.ToString();
            dgvDentailOrder.DataSource = barista.ViewCustomerDentailOrder(idOrder);
        }

        /// <summary>
        /// nút chấm công
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnChamCong_Click(object sender, EventArgs e)
        {
            barista.ChamCong();
            dgvbang.DataSource = barista.ViewSalary();
        }

        private void guna2TabControl1_Click(object sender, EventArgs e)
        {
            TabControl tabControl = sender as TabControl;

            if (tabControl != null)
            {
                // Lấy chỉ số của tab được chọn
                int selectedIndex = tabControl.SelectedIndex;
                // Lấy thông tin về tab theo chỉ số
                if (selectedIndex == 3)
                    Close();
                else if (selectedIndex == 2)
                {
                    Hide();
                    FormChangePassWord changePassWord = new FormChangePassWord(barista);
                    changePassWord.ShowDialog();
                    tabControl.SelectedIndex = 0;
                    Show();
                }
            }
        }
    }
}
