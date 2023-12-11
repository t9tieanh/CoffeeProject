using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CaffeeShop
{
    public class FeedBack : Save
    {
        private string idfeedBack;
        private string idCustomer;
        private string content;
        private DateTime creatDate;

        public FeedBack(string idCustomer, string content)
        {
            IdCustomer = idCustomer.Trim();
            Content = content.Trim();
            //
            CreatDate = DateTime.Now;
            // tạo idFeedBack
            SqlCommand sqlCommand;
            // SqlCommand sqlCommand;
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                /// lấy số lượng của feedback trong kho 
                sqlCommand = connection.CreateCommand();
                sqlCommand.CommandText = "SELECT COUNT(*) FROM FeedBack";
                IdfeedBack = "No" + (int)sqlCommand.ExecuteScalar();
                connection.Close();
            }
        }

        public string IdfeedBack { get => idfeedBack; set => idfeedBack = value; }
        public string IdCustomer { get => idCustomer; set => idCustomer = value; }
        public string Content { get => content; set => content = value; }
        public DateTime CreatDate { get => creatDate; set => creatDate = value; }
        public void SaveData()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionData.str))
            {
                connection.Open();
                SqlCommand sqlCommand = connection.CreateCommand();

                sqlCommand.CommandText = "INSERT INTO FEEDBACK (IdFeedback, IdCustomer, Cont, CreatDate) " +
                                         "VALUES (@IdFeedback, @IdCustomer, @Content, @CreateDate)";

                sqlCommand.Parameters.AddWithValue("@IdFeedback", IdfeedBack);
                sqlCommand.Parameters.AddWithValue("@IdCustomer", IdCustomer);
                sqlCommand.Parameters.AddWithValue("@Content", Content);
                sqlCommand.Parameters.AddWithValue("@CreateDate", CreatDate.ToString("yyyy-MM-dd"));

                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}