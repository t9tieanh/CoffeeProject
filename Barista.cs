using System.Data;
using System.Data.SqlClient;
namespace CaffeeShop
{
    public class Barista : Person
    {
        private DateTime hireDay = new DateTime();
        private int salary;
        public Barista() { }
        public Barista(string id, string name, string phoneNumber, string password, DateTime hireDay, int salary, string account) : base(id, name, phoneNumber, account, password)
        {
            HireDay = hireDay;
            Salary = salary;
        }

        public void UpdateSalary (int day, SqlConnection con)
        {
            using(SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "Update Salary SET DayWork = @DayWork, Total = @Total, CheckDay = @check Where Month = " + DateTime.Now.Month + " And Year = " + DateTime.Now.Year + " And IDNV= " + this.Id;
                cmd.Connection = con;
                cmd.Parameters.AddWithValue("@DayWork", day + 1);
                cmd.Parameters.AddWithValue("@Total", (day + 1) * this.Salary);
                cmd.Parameters.AddWithValue("@check", DateTime.Now);
                cmd.ExecuteNonQuery();
            }
        }

        public void InsertSalary(int day, DateTime time, SqlConnection con)
        {
            using(SqlCommand sql = new SqlCommand())
            {
                int count = 0;
                using (SqlCommand cmd = con.CreateCommand())
                {
                    cmd.CommandText = "SELECT IDSa FROM Salary";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);

                        if (dt.Rows.Count > 0)
                        {
                            count = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1][0]) + 1;
                        }
                    }
                }
                sql.CommandText = "INSERT INTO Salary (IDSa, IDNV, DayWork, Month, Year, Total, CheckDay) values (@IDSa, @IDNV, @DayWork, @Month, @Year, @Total, @CheckDay)";
                sql.Connection = con;
                sql.Parameters.AddWithValue("@IDSa", count);
                sql.Parameters.AddWithValue("@IDNV", this.Id);
                sql.Parameters.AddWithValue("@DayWork", day);
                sql.Parameters.AddWithValue("@Month", DateTime.Now.Month);
                sql.Parameters.AddWithValue("@Year", DateTime.Now.Year);
                sql.Parameters.AddWithValue("@Total", day * this.Salary);
                sql.Parameters.AddWithValue("@CheckDay", time);
                sql.ExecuteNonQuery();
            }
        }
        public void ChamCong()
        {
            bool check = true;
            using (SqlConnection con = new SqlConnection(ConnectionData.str))
            {
                if(con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "Select CheckDay From Salary Where IDNV = @ID ORDER BY Month";
                cmd.Parameters.AddWithValue("@ID", this.Id);
                cmd.ExecuteNonQuery();
                SqlDataAdapter ap = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                ap.Fill(dataTable);
                if(dataTable.Rows.Count > 0 )
                {
                    if(DateTime.Now.Date == Convert.ToDateTime(dataTable.Rows[dataTable.Rows.Count - 1]["CheckDay"]).Date)
                    {
                        MessageBox.Show("Bạn Đã Điểm Danh Cho Ngày Hôm Nay");
                    }
                    else
                    {
                        SqlCommand cm = con.CreateCommand();
                        cm.CommandText = "Select DayWork, Month, Year From Salary Where IDNV = @IDNV";
                        cm.Parameters.AddWithValue("@IDNV", this.Id);
                        SqlDataReader reader = cm.ExecuteReader();
                        while (reader.Read())
                        {
                            if (Convert.ToInt32(reader["Month"].ToString()) == DateTime.Now.Month && Convert.ToInt32(reader["Year"].ToString()) == DateTime.Now.Year)
                            {
                                int temp = Convert.ToInt32(reader["DayWork"].ToString());
                                check = false;
                                reader.Close();
                                UpdateSalary (temp, con);
                                break;
                            }
                        }
                        if (check)
                        {
                            InsertSalary(1,DateTime.Now, con);
                        }
                        MessageBox.Show("Điểm Danh Thành Công", "Thông Báo");
                    }          
                }
                con.Close();
            }
        }

        /// <summary>
        /// đổi mật khẩu cho abstrac person 
        /// </summary>
        /// <param name="NewPassword"></param>
        public override void ChangePassword(string NewPassword)
        {
            PassWord = NewPassword;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "UPDATE NhanVien SET PassWord = @NewPassword WHERE IDNV = @IDBarista";
                sqlCommand.Parameters.AddWithValue("@NewPassword", NewPassword);
                sqlCommand.Parameters.AddWithValue("@IDBarista", Id);

                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        public int Salary { get => salary; set => salary = value; }
        public DateTime HireDay { get => hireDay; set => hireDay = value; }
        /// <summary>
        /// Check xem kho còn đủ hàng để xử lý order không 
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        private bool Checkinventory (string idOrder)
        {
            int count;
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                sqlCommand = connection.CreateCommand();
                //check xem các sản phẩm của order có cái nào không tồn tại trong kho hay không  (status = 0) có thì retus false
                sqlCommand.CommandText = "SELECT count(*) FROM DentailOrder AS DOD , Product AS PRO WHERE PRO.Status = 0 AND DOD.IdProduct = PRO.IdProduct AND DOD.IdOrder = '" + idOrder + "'";
                count = (int)sqlCommand.ExecuteScalar();
                if (count > 0)
                { 
                    connection.Close() ;
                    return false;
                }
                //check xem các sản phẩm của order có cái nào có số lượng lớn hơn hàng trong kho không có thì return false
                sqlCommand.CommandText = "SELECT count(*) FROM DentailOrder AS DOD , Product AS PRO WHERE DOD.quantity > PRO.Quantity AND DOD.IdProduct = PRO.IdProduct AND DOD.IdOrder = '" + idOrder + "'";
                count = (int)sqlCommand.ExecuteScalar();
                connection.Close();
            }
            if (count > 0) return false;
            return true;
        }
        /// <summary>
        /// chuẩn bị Coffee 
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        public bool PrepareCoffee (string idOrder)
        {
            if (!Checkinventory(idOrder)) return false;// không đủ hàng
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                sqlCommand = connection.CreateCommand();
                //trừ hàng trong kho
                sqlCommand.CommandText = "UPDATE Product SET Quantity = Quantity - ( SELECT SUM (Quantity) FROM DentailOrder WHERE DentailOrder.IdOrder = '"+idOrder+"' AND DentailOrder.IdProduct = Product.IdProduct) WHERE Product.IdProduct IN (SELECT IdProduct FROM DentailOrder WHERE DentailOrder.IdOrder = '"+idOrder+"') ";
                sqlCommand.ExecuteNonQuery();
                //xác nhận
                sqlCommand.CommandText = "UPDATE [Order] SET StatusOrder = 1 WHERE IdOrder = '" + idOrder +"'";
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            return true; // đặt hàng thành công 
        }

        /// <summary>
        /// Xem Order của khách để xử lý đơn hàng 
        /// </summary>
        /// <returns></returns>
        public DataTable ViewCustomerOrder()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand sqlCommand;
            DataTable customerOrder = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT OD.IdOrder AS N'ID Order', CUS.NameCustomer AS N'Tên khách hàng' , OD.total AS N'Tổng Tiền' , OD.statusOrder AS N'Trạng thái đơn hàng', OD.CreatDate AS N'Ngày tạo đơn' FROM [Order] AS OD , Customer AS CUS WHERE OD.IdCustomer = CUS.IdCustomer AND OD.StatusOrder = 0";
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(customerOrder);
            }
            return customerOrder;
        }

        /// <summary>
        /// Xem dentail order của khách để xử lý đơn hàng 
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        public DataTable ViewCustomerDentailOrder(string idOrder)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand sqlCommand;
            DataTable customerDentailOrder = new DataTable ();
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT PRO.NamePro AS N'Tên sản phẩm',DOD.Quantity AS N'Số lượng' FROM DentailOrder AS DOD, Product AS PRO WHERE DOD.IdOrder = '" + idOrder + "' AND PRO.IdProduct = DOD.IdProduct";
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(customerDentailOrder);
            }
            return customerDentailOrder ;
        }

        /// <summary>
        /// Xem bảng lương
        /// </summary>
        /// <returns></returns>
        public DataTable ViewSalary()
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand sqlCommand;
            DataTable salary = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT NhanVien.NameNV, Salary.DayWork, Salary.Month, Salary.Year, Salary.Total " +
                                "FROM NhanVien INNER JOIN Salary ON NhanVien.IDNV = Salary.IDNV " +
                                "WHERE Salary.IDNV = @ID " +
                                "ORDER BY Salary.Month";
                sqlCommand.Parameters.AddWithValue("@ID", Id);
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(salary);
            }
            return salary ;
        }

        public override string PrintDetail()
        {
            return "Barista " + base.PrintDetail();
        }
    }
}