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
    public partial class FormChangePassWord : Form
    {
        Person person;
        public FormChangePassWord(Person person)
        {
            InitializeComponent();
            this.person = person;
            lblTaiKhoan.Text = person.PrintDetail();
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
            lblcheckLoiPassmoi.Text = "!";
            lblcheckloipasscu.Text = "!";
            if (txtMkcu.Text.Trim() != person.PassWord.Trim())
            {
                lblcheckloipasscu.Text = "PassWord cũ không đúng !";
                return;
            }
            if (!CheckLoi.PassWord(txtMkmoi.Text))
            {
                lblcheckLoiPassmoi.Text = "Password chỉ chứa chữ và số, độ dài < 20 kí tự";
                return;
            }
            DialogResult result = MessageBox.Show("Xác nhận đổi mật khẩu ?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                person.ChangePassword(txtMkmoi.Text.Trim());
                MessageBox.Show("Đổi mật khẩu thành công !", "Notice", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
