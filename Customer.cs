using System.Data;
using System.Data.SqlClient;

namespace CaffeeShop
{
    public class Customer : Person, Save
    {
        /// <summary>
        /// chỉ được xem
        /// </summary>
        public int SoDu { get => LoadSoDu(); }

        public Customer() { }

        public Customer(string id, string name, string phoneNumber, string account, string passWord) : base(id, name, phoneNumber, account, passWord){}
        public Customer(string name, string account, string phoneNumber, string passWord) : base(name, account, phoneNumber, passWord)
        {
            SqlCommand sqlCommand;
            // SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                /// lấy số lượng của feedback trong kho 
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT COUNT(*) FROM Customer";
                Id = "No" + (int)sqlCommand.ExecuteScalar();
                connection.Close();
            }
        }
        /// <summary>
        /// xem số dư ở database
        /// </summary>
        /// <returns></returns>
        private int LoadSoDu()
        {
            int sodu;
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                /// lấy số lượng của order trong kho để làm id
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT SoDu FROM Customer WHERE IdCustomer = '" + Id + "'";
                sodu = (int)sqlCommand.ExecuteScalar();
                connection.Close();
            }
            return sodu;
        }

        /// <summary>
        /// đổi mật khẩu override cho person
        /// </summary>
        /// <param name="NewPassword"></param>
        public override void ChangePassword(string NewPassword)
        {
            PassWord = NewPassword;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "UPDATE Customer SET passwordcustomer = @NewPassword WHERE idcustomer = @CustomerId";
                sqlCommand.Parameters.AddWithValue("@NewPassword", NewPassword);
                sqlCommand.Parameters.AddWithValue("@CustomerId", this.Id);

                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// tạo feedback
        /// </summary>
        /// <param name="contentFeedBack"></param>
        public void CreateFeedBack(string contentFeedBack)
        {
            FeedBack newFeedback = new FeedBack(Id, contentFeedBack);
            newFeedback.SaveData();
        }
        //------------------------------- Order Coffee-------------------------------------------------
        /// <summary>
        /// khách hàng đặt hàng trường hợp chỉ đặt một loại (Overload 1)
        /// </summary>
        /// <param name="idOrder"></param>
        /// <param name="idCoffee"></param>
        /// <param name="quanlity"></param>
        public void OrderCoffee(string idProduct, int quanlity, string idCustomer, int total)
        {
            Order newOrder = new Order(idCustomer, total);
            newOrder.SaveData();
            DentailOrder newDentailOrder = new DentailOrder(newOrder.IdOrder, idProduct, quanlity);
            newDentailOrder.SaveData();
        }

        /// <summary>
        /// order coffee overload trường hợp đặt nhiều loại (Overload 2)
        /// </summary>
        /// <param name="product">chứa id product và số lượng của loại đó</param> 
        /// <param name="idCustomer"></param>
        /// <param name="total"></param>
        public void OrderCoffee(List<(string, int)> product, string idCustomer, int total)
        {
            Order newOrder = new Order(idCustomer, total);
            newOrder.SaveData();
            foreach (var pro in product)
            {
                DentailOrder newDentailOrder = new DentailOrder(newOrder.IdOrder, pro.Item1, pro.Item2);
                newDentailOrder.SaveData();
            }
        }
        //--------------------------------------------- Xử lý thanh toán --------------------------------------

        /// <summary>
        /// khách hàng xử lý thanh toán
        /// </summary>
        /// <param name="paymentMethods"></param>
        /// <param name="totalPayment"></param>
        /// <returns></returns>

        public bool PaymentProcessing(bool paymentMethods, string idOrder)
        {
            if (paymentMethods)
            {
                SqlCommand sqlCommandd;
                using (SqlConnection connection = new SqlConnection(ConnectionData.str))
                {
                    connection.Open();
                    sqlCommandd = connection.CreateCommand();
                    sqlCommandd.CommandText = "SELECT Total FROM [order] WHERE IdOrder = '" + idOrder + "'";
                    int totalPayment = (int)sqlCommandd.ExecuteScalar();
                    if (totalPayment > SoDu) // số dư không đủ 
                    {
                        connection.Close();
                        return false;
                    }
                    sqlCommandd.CommandText = "UPDATE Customer SET soDu = soDu - " + totalPayment + " WHERE IdCustomer = '" + Id + "'";
                    sqlCommandd.ExecuteNonQuery();
                    connection.Close();
                }
            }
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "UPDATE [Order] SET DaThanhToan = 1 WHERE IdOrder = '" + idOrder + "'";
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
            return true;
        }

        /// <summary>
        /// override từ person
        /// </summary>
        /// <returns></returns>
        public override string PrintDetail()
        {
            return "Customer " + base.PrintDetail();
        }

        /// <summary>
        /// sử dụng từ interface save -> đẩy dữ liệu của người dùng xuống database khi người dùng tạo tài khoản 
        /// </summary>
        public void SaveData()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "INSERT INTO CUSTOMER (IdCustomer, NameCustomer , PhoneNumber, PassWordCustomer, AccountCustomer, soDu) " +
                                         "VALUES (@IdCustomer, @NameCustomer , @PhoneNumber, @PassWordCustomer, @AccountCustomer, @soDu)";

                sqlCommand.Parameters.AddWithValue("@IdCustomer", Id);
                sqlCommand.Parameters.AddWithValue("@NameCustomer", Name);
                sqlCommand.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                sqlCommand.Parameters.AddWithValue("@PassWordCustomer", PassWord);
                sqlCommand.Parameters.AddWithValue("@AccountCustomer", Account);
                sqlCommand.Parameters.AddWithValue("@soDu", 0);

                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Xem Menu
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public DataTable ViewProduct(int i)
        {
            DataTable product = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "select idproduct as 'ID Product',namepro as N'Tên sản phẩm',pricepro as N'Giá' from product where type = " + i + "AND Status = 1";
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(product);
            }
            return product;
        }

        /// <summary>
        /// Xem Order của mình
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public DataTable ViewYourOrder (int i)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand sqlCommand;
            DataTable yourOrder = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT IdOrder as 'ID Order' , total AS N'Tổng tiền' , StatusOrder AS N'Trạng thái Chuẩn bị' , CreatDate AS N'Ngày tạo đơn' , DaThanhToan AS N'Tình trạng thanh toán' FROM [Order] WHERE IdCustomer = '" + Id + "' AND DaThanhToan = " + i + " ";
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(yourOrder);
            }
            return yourOrder;
        }

        /// <summary>
        /// Xem DentailOrder của mình
        /// </summary>
        /// <param name="idOrder"></param>
        /// <returns></returns>
        public DataTable ViewDentailOrder (string idOrder)
        {
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand sqlCommand;
            DataTable dentailOrder = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT Product.NamePro AS N'Tên sản phẩm' , DentailOrder.quantity , Product.PricePro AS N'Giá' FROM DentailOrder,Product WHERE IdOrder = '" + idOrder + "' AND DentailOrder.IdProduct = Product.IdProduct";
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(dentailOrder);
            }
            return dentailOrder;
        }
    }
}