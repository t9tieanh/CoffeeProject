using Check_Loi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CaffeeShop
{
    public partial class FormSignUp : Form
    {
        public FormSignUp()
        {
            InitializeComponent();
        }
        private bool CheckLoiIn()
        {
            bool flag = true;
            if (!CheckLoi.AccountAndId(txtTenTaiKhoan.Text))
            {
                lblcheckloiAccount.Text = "Id chỉ chưa chữ cái và số ! độ dài <= 10 ";
                flag = false;
            }
            if (!CheckLoi.PassWord(txtPassWord.Text))
            {
                lblcheckloipass.Text = "PassWord chỉ chưa chữ cái và số ! độ dài <= 20";
                flag = false;
            }
            if (!CheckLoi.Name(txtTenKhachHang.Text))
            {
                lblCheckloiTen.Text = "Tên không hợp lệ ! độ dài <= 50";
                flag = false;
            }
            if (!CheckLoi.PhoneNumber(txtSoDienThoai.Text))
            {
                lblcheckloisdt.Text = "Số điện thoại không hợp lệ ! độ dài <= 11";
                flag = false;
            }
            if (txtPassWord.Text != txtPassWord2.Text)
            {
                lblcheckloipassAgain.Text = "PassWord không khớp !";
                flag = false;
            }
            return flag;
        }
        private void ResetCheckLoi()
        {
            lblcheckloiAccount.Text = "!";
            lblcheckloipass.Text = "!";
            lblCheckloiTen.Text = "!";
            lblcheckloipassAgain.Text = "!";
            lblcheckloisdt.Text = "!";
        }

        private void btndangki_Click(object sender, EventArgs e)
        {
            ResetCheckLoi();
            if (!CheckLoiIn()) return;
            if (txtPassWord.Text != txtPassWord2.Text)
            {
                MessageBox.Show("Mật khẩu không khớp !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Customer customer = new Customer(txtTenKhachHang.Text, txtTenTaiKhoan.Text, txtSoDienThoai.Text, txtPassWord.Text);
            customer.SaveData();
            MessageBox.Show("Tạo tài khoản thành công, mời bạn đăng nhập lại !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }
        private void button18_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
