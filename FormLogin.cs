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
    public partial class FormLogin : Form
    {
        SqlConnection conn = new SqlConnection();

        SqlCommand cmd;

        public FormLogin()
        {

            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }



        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            conn.ConnectionString = ConnectionData.str;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd = new SqlCommand("Select * From Customer", conn);
            SqlDataReader rd = cmd.ExecuteReader();
            bool check = false;
            if (txtacc.Text.Trim() == string.Empty || txtpass.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui Lòng Nhập Thông Tin Đầy Đủ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                while (rd.Read())
                {
                    string acc = rd["AccountCustomer"].ToString();
                    string pass = rd["PasswordCustomer"].ToString();

                    if (acc.Trim() == txtacc.Text && pass.Trim() == txtpass.Text)
                    {
                        FormCustomer user = new FormCustomer(new Customer(rd["IdCustomer"].ToString(), rd["NameCustomer"].ToString(), rd["PhoneNumber"].ToString(), acc, pass));
                        check = true;
                        this.Hide();
                        user.ShowDialog();
                        this.Show();
                        break;
                    }
                }
                if (check == false)
                {
                    MessageBox.Show("Tài Khoản Hoặc Mật Khẩu Không Đúng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            conn.Close();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            conn.ConnectionString = ConnectionData.str;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd = new SqlCommand("Select * From NhanVien", conn);
            SqlDataReader rd = cmd.ExecuteReader();
            bool check = false;
            if (txtacc.Text.Trim() == string.Empty || txtpass.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui Lòng Nhập Thông Tin Đầy Đủ", "Thông Báo");
            }
            else
            {
                while (rd.Read())
                {
                    string acc = rd["Account"].ToString();
                    string pass = rd["Password"].ToString();

                    if (acc.Trim() == txtacc.Text && pass.Trim() == txtpass.Text)
                    {
                        FormEmployee user = new FormEmployee(new Barista(rd["IDNV"].ToString(), rd["NameNV"].ToString(), rd["PhoneNumber"].ToString(), pass, Convert.ToDateTime(rd["HireDate"].ToString()), Convert.ToInt32(rd["Salary"].ToString()), acc));
                        check = true;
                        this.Hide();
                        user.ShowDialog();
                        this.Show();
                        break;
                    }
                }
                if (check == false)
                {
                    MessageBox.Show("Tài Khoản Hoặc Mật Khẩu Không Đúng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            conn.Close();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            conn.ConnectionString = ConnectionData.str;
            if (conn.State != ConnectionState.Open)
            {
                conn.Open();
            }

            cmd = new SqlCommand("Select * From Manager", conn);
            SqlDataReader rd = cmd.ExecuteReader();
            bool check = false;
            if (txtacc.Text.Trim() == string.Empty || txtpass.Text.Trim() == string.Empty)
            {
                MessageBox.Show("Vui Lòng Nhập Thông Tin Đầy Đủ", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                while (rd.Read())
                {
                    string acc = rd["AccountManager"].ToString();
                    string pass = rd["PasswordManager"].ToString();

                    if (acc.Trim() == txtacc.Text && pass.Trim() == txtpass.Text)
                    {
                        FormManager ad = new FormManager(new Manager(rd["Idmanager"].ToString(), rd["NameManager"].ToString(), rd["PhoneNumberManager"].ToString(), acc, pass));
                        check = true;
                        this.Hide();
                        ad.ShowDialog();
                        this.Show();
                        break;
                    }
                }
                if (check == false)
                {
                    MessageBox.Show("Tài Khoản Hoặc Mật Khẩu Không Đúng", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            conn.Close();
        }

        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            FormSignUp newCustomer = new FormSignUp();
            Hide();
            newCustomer.ShowDialog();
            this.Show();
        }
    }
}
