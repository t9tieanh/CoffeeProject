using System.Data.SqlClient;
using System.Data;

namespace CaffeeShop
{
    public class Manager : Person
    {
        public Manager(){}

        public Manager(string id, string name, string phoneNumber, string account, string passWord) : base(id, name, phoneNumber, account, passWord) { }

        /// <summary>
        /// các hàm check tồn tại của account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private bool CheckExitstAccount (string account)
        {
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT COUNT(*) FROM  NhanVien WHERE Account = '" + account + "'";
                if ((int)sqlCommand.ExecuteScalar() == 0)
                {
                    connection.Close ();
                    return true;
                }
                connection.Close();
                return false;
            }
        }

        private bool CheckExitstAccount(string account , string id)
        {
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT COUNT(*) FROM  NhanVien WHERE Account = '" + account + "' AND IDNV != '" + id + "'";
                if ((int)sqlCommand.ExecuteScalar() == 0)
                {
                    connection.Close();
                    return true;
                }
                connection.Close();
                return false;
            }
        }
        //--------------------------------- Quản lý nhân viên ----------------------------------------
        /// <summary>
        /// thêm nhân viên 
        public bool AddEmployee(Barista ba)
        {
            if (!CheckExitstAccount(ba.Account))
                return false;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open(); 
                SqlCommand sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "INSERT INTO NhanVien VALUES (@Id, @Name, @PhoneNumber, @Salary, @HireDate, @Account, @Password)";
                sqlCommand.Parameters.AddWithValue("@Id", ba.Id);
                sqlCommand.Parameters.AddWithValue("@Name", ba.Name);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", ba.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Salary", ba.Salary);
                sqlCommand.Parameters.AddWithValue("@HireDate", ba.HireDay.ToString("yyyy-MM-dd"));
                sqlCommand.Parameters.AddWithValue("@Account", ba.Account);
                sqlCommand.Parameters.AddWithValue("@Password", ba.PassWord); // Cần mã hóa mật khẩu trước khi lưu vào DB
                sqlCommand.ExecuteNonQuery();
                ba.InsertSalary(0, Convert.ToDateTime("2000-1-1"), connection);
                connection.Close();
            }
            return true;
        }

        /// <summary>
        /// Xóa nhân viên
        /// </summary>
        /// <param name="IdEmployee"></param>
        public void DeleteEmployee(string IdEmployee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                // Xóa thông tin lương từ bảng SALARY
                sqlCommand.CommandText = "DELETE FROM SALARY WHERE IDNV = @IdEmployee";
                sqlCommand.Parameters.AddWithValue("@IdEmployee", IdEmployee);
                sqlCommand.ExecuteNonQuery();

                // Xóa nhân viên từ bảng NhanVien
                sqlCommand.CommandText = "DELETE FROM NhanVien WHERE IDNV = @IdEmployee";
                sqlCommand.Parameters.Clear(); // Xóa các parameter trước khi sử dụng lại
                sqlCommand.Parameters.AddWithValue("@IdEmployee", IdEmployee);
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        public bool FixEmployee(Barista ba)
        {
            if (!CheckExitstAccount(ba.Account, ba.Id)) return false;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "UPDATE NhanVien SET NameNV = @Name, PhoneNumber = @PhoneNumber, Salary = @Salary, HireDate = @HireDate, Account = @Account, Password = @Password WHERE IDNV = @Id";

                sqlCommand.Parameters.AddWithValue("@Name", ba.Name);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", ba.PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@Salary", Convert.ToInt32(ba.Salary));
                sqlCommand.Parameters.AddWithValue("@HireDate", ba.HireDay.ToString("yyyy-MM-dd"));
                sqlCommand.Parameters.AddWithValue("@Account", ba.Account);
                sqlCommand.Parameters.AddWithValue("@Password", ba.PassWord);
                sqlCommand.Parameters.AddWithValue("@Id", ba.Id);

                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }
        //---------------------------------------------- Quản lý Hàng Tồn Kho ----------------------------------------------------------
        /// <summary>
        /// thêm Product
        /// </summary>
        /// <param name="newProduct"></param>
        public void AddProduct (Product newProduct)
        {
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "UPDATE Product SET NamePro = N'" + newProduct.Name + "',PricePro = " + newProduct.Price + ", Quantity = " + newProduct.Quatity + ",Type = " + (int)newProduct.LoaiProduct + ",Status = 1 WHERE EXISTS (SELECT * FROM product WHERE IdProduct = " + newProduct.Id + " AND STATUS = 0 ) AND IdProduct = ('" + newProduct.Id + "')";
                if (sqlCommand.ExecuteNonQuery() == 0)
                {
                    sqlCommand.CommandText = "INSERT INTO Product VALUES (@Id, @Name, @Price, @Quantity, @Type, @Status)";
                    sqlCommand.Parameters.AddWithValue("@Id", newProduct.Id);
                    sqlCommand.Parameters.AddWithValue("@Name", newProduct.Name);
                    sqlCommand.Parameters.AddWithValue("@Price",newProduct.Price );
                    sqlCommand.Parameters.AddWithValue("@Quantity", newProduct.Quatity);
                    sqlCommand.Parameters.AddWithValue("@Type",(int)newProduct.LoaiProduct);
                    sqlCommand.Parameters.AddWithValue("@Status", 1);
                    sqlCommand.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        /// <summary>
        /// xóa product trên database
        /// </summary>
        /// <param name="IdProduct"></param>
        public void DeleteProduct(string IdProduct)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "UPDATE Product SET Status = 0 WHERE IdProduct = @IdProduct";
                sqlCommand.Parameters.AddWithValue("@IdProduct", IdProduct);

                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
        /// <summary>
        /// sửa product
        /// </summary>
        /// <param name="product"></param>
        public void FixProduct(Product product)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "UPDATE Product SET NamePro = @Name, PricePro = @Price, Quantity = @Quantity, Type = @Type WHERE IdProduct = @Id";

                sqlCommand.Parameters.AddWithValue("@Name", product.Name);
                sqlCommand.Parameters.AddWithValue("@Price", product.Price);
                sqlCommand.Parameters.AddWithValue("@Quantity", product.Quatity);
                sqlCommand.Parameters.AddWithValue("@Type", (int)product.LoaiProduct);
                sqlCommand.Parameters.AddWithValue("@Id", product.Id);

                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// override từ person
        /// </summary>
        /// <returns></returns>
        public override string PrintDetail()
        {
            return "Manager " + base.PrintDetail(); 
        }

        /// <summary>
        /// overide từ abstrac func của person
        /// </summary>
        /// <param name="NewPassword"></param>
        public override void ChangePassword(string NewPassword)
        {
            PassWord = NewPassword;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "UPDATE Manager SET passwordmanager = @NewPassword WHERE idManager = @Id";
                sqlCommand.Parameters.AddWithValue("@NewPassword", NewPassword);
                sqlCommand.Parameters.AddWithValue("@Id", this.Id);

                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// nạp tiền cho khách
        /// </summary>
        /// <param name="idCustomer"></param>
        /// <param name="soTiennap"></param>
        public void NapTienChoKhach(string idCustomer, int soTiennap)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "UPDATE Customer SET soDu = soDu + @SoTienNap WHERE IdCustomer = @IdCustomer";
                sqlCommand.Parameters.AddWithValue("@SoTienNap", soTiennap);
                sqlCommand.Parameters.AddWithValue("@IdCustomer", idCustomer);

                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Xem các thông tin của cửa hàng thôi qua lệnh sql 
        /// </summary>
        /// <param name="lenhSql"></param>
        /// <returns></returns>
        public DataTable ViewShopData (string lenhSql)
        {

            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand sqlCommand;
            DataTable datatable = new DataTable () ;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT * FROM " + lenhSql;
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(datatable);
            }
            return datatable;
        }

        /// <summary>
        /// xem FeedBack từ khách hàng
        /// </summary>
        /// <param name="before"></param>
        /// <param name="after"></param>
        /// <returns></returns>
        public DataTable ViewFeedBack (DateTime before, DateTime after)
        {
            DataTable feedBack = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT fb.Idfeedback,Cus.NameCustomer,fb.Cont,fb.CreatDate FROM FeedBack as fb , Customer as Cus WHERE fb.CreatDate >= '" + before.ToString("yyyy-MM-dd") + "' AND fb.CreatDate <= '" + after.ToString("yyyy-MM-dd") + "' AND Cus.IdCustomer = fb.IdCustomer";
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(feedBack);
            }
            return feedBack;
        }

        //----------------Xem doanh thu-------------------
        public DataTable ViewOrder(DateTime before, DateTime after, int dahoanthanh)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand sqlCommand;
            DataTable doanhThu = new DataTable ();
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT OD.IdOrder as 'ID Order' ,CUS.NameCustomer, OD.total AS N'Tổng tiền' , OD.CreatDate AS N'Ngày tạo đơn',OD.StatusOrder AS N'Tình trạng chuẩn bị' , OD.DaThanhToan AS N'Tình trạng thanh toán'   FROM[Order] AS OD, Customer AS CUS WHERE OD.DaThanhToan =" + dahoanthanh + " AND OD.CreatDate >= '" + before.ToString("yyyy-MM-dd") + "' AND OD.CreatDate <= '" + after.ToString("yyyy-MM-dd") + "' AND  CUS.IdCustomer = OD.IdCustomer ";
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(doanhThu);
            }
            return doanhThu;
        }

        public int DoanhThu (DateTime before, DateTime after) 
        {
            SqlCommand sqlCommand;
            int doanhThu;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "SELECT SUM(Total) FROM [Order] WHERE DaThanhToan = 1 AND CreatDate >= '" + before.ToString("yyyy-MM-dd") + "' AND CreatDate <= '" + after.ToString("yyyy-MM-dd") + "'";
                doanhThu = (int) sqlCommand.ExecuteScalar();
                connection.Close();
            }
            return doanhThu;
        }
        public int OrderCount (DateTime before, DateTime after , int daHoanThanh)
        {
            SqlCommand sqlCommand;
            int count;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "SELECT COUNT(*) FROM [Order] WHERE DaThanhToan = "+ daHoanThanh +" AND CreatDate >= '" + before.ToString("yyyy-MM-dd") + "' AND CreatDate <= '" + after.ToString("yyyy-MM-dd") + "'";
                count = (int)sqlCommand.ExecuteScalar();
                connection.Close();
            }
            return count;
        }

    }
}