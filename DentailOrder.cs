using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaffeeShop
{
    public class DentailOrder : Save
    {
        private string idDentailOrder;
        private string idOrder;
        private string idProduct;
        private int quantity;

        public DentailOrder(string idOrder, string idProduct, int quantity)
        {
            IdOrder = idOrder;
            IdProduct = idProduct;
            Quantity = quantity;

            //lấy iddentailorder 
            SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                /// lấy số lượng của feedback trong kho 
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT COUNT(*) FROM DentailOrder";
                IdDentailOrder = "No" + (int)sqlCommand.ExecuteScalar();
                connection.Close();
            }
        }

        public string IdDentailOrder { get => idDentailOrder; set => idDentailOrder = value; }
        public string IdOrder { get => idOrder; set => idOrder = value; }
        public string IdProduct { get => idProduct; set => idProduct = value; }
        public int Quantity { get => quantity; set => quantity = value; }

        /// <summary>
        /// đẩy dữ liệu lên database
        /// </summary>
        public void SaveData()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "INSERT INTO DentailOrder (IdDentailOrder, IdOrder, IdProduct, Quantity) " +
                                         "VALUES (@IdDentailOrder, @IdOrder, @IdProduct, @Quantity)";

                sqlCommand.Parameters.AddWithValue("@IdDentailOrder", IdDentailOrder);
                sqlCommand.Parameters.AddWithValue("@IdOrder", IdOrder);
                sqlCommand.Parameters.AddWithValue("@IdProduct", IdProduct);
                sqlCommand.Parameters.AddWithValue("@Quantity", Quantity);

                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
