using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeeShop
{
    public class Order : Save
    {
        private string idOrder;
        private string idCustomer;
        private int total;
        private bool statusOrder;
        private DateTime createDate;
        private bool daThanhToan;
        public Order(string idCustomer, int total)
        {
            IdCustomer = idCustomer;
            Total = total;
            //
            CreateDate = DateTime.Now;
            //
            StatusOrder = false;
            DaThanhToan = false;
            //
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                /// lấy số lượng của order trong kho để làm id
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT count(*) FROM [Order]";
                IdOrder = "No" + (int)sqlCommand.ExecuteScalar();
                connection.Close();
            }
        }

        public string IdOrder { get => idOrder; set => idOrder = value; }
        public string IdCustomer { get => idCustomer; set => idCustomer = value; }
        public int Total { get => total; set => total = value; }
        public bool StatusOrder { get => statusOrder; set => statusOrder = value; }
        public DateTime CreateDate { get => createDate; set => createDate = value; }
        public bool DaThanhToan { get => daThanhToan; set => daThanhToan = value; }

        /// <summary>
        /// lưu dữ liệu order lên database sử dụng khi khách hàng tạo order 
        /// </summary>
        public void SaveData()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "INSERT INTO [Order] (IdOrder, IdCustomer, Total, StatusOrder, CreatDate, DaThanhToan) " +
                                         "VALUES (@IdOrder, @IdCustomer, @Total, @StatusOrder, @CreateDate, @DaThanhToan)";

                sqlCommand.Parameters.AddWithValue("@IdOrder", IdOrder);
                sqlCommand.Parameters.AddWithValue("@IdCustomer", IdCustomer);
                sqlCommand.Parameters.AddWithValue("@Total", total);
                sqlCommand.Parameters.AddWithValue("@StatusOrder", 0); // Nếu cần, thay bằng giá trị thực tế
                sqlCommand.Parameters.AddWithValue("@CreateDate", createDate.ToString("yyyy-MM-dd"));
                sqlCommand.Parameters.AddWithValue("@DaThanhToan", 0); // Nếu cần, thay bằng giá trị thực tế

                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
