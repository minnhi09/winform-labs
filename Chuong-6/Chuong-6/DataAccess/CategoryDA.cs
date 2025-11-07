using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chuong_6.DataAccess
{
    public class CategoryDA
    {
        private const string CONNECTION_STRING = @"Server=mnhy\SQLEXPRESS01;Database=chuong_6_restaurant_management;Trusted_Connection=True;";

        public List<Category> GetAll()
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "sp_FoodCategory_GetAll";

            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Category> list = new List<Category>();

            while (reader.Read())
            {
                Category category = new Category();
                category.ID = Convert.ToInt32(reader["ID"]);
                category.Name = reader["Name"].ToString();
                category.Type = Convert.ToInt32(reader["Type"]);
                list.Add(category);
            }

            sqlConnection.Close();
            sqlCommand.Dispose();
            return list;
        }

        public Category GetByID(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "sp_FoodCategory_GetByID";
            sqlCommand.Parameters.AddWithValue("@ID", id);

            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            Category category = null;

            if (reader.Read())
            {
                category = new Category();
                category.ID = Convert.ToInt32(reader["ID"]);
                category.Name = reader["Name"].ToString();
                category.Type = Convert.ToInt32(reader["Type"]);
            }

            sqlConnection.Close();
            sqlCommand.Dispose();
            return category;
        }

        public List<Category> GetByType(int type)
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "sp_FoodCategory_GetByType";
            sqlCommand.Parameters.AddWithValue("@Type", type);

            sqlConnection.Open();
            SqlDataReader reader = sqlCommand.ExecuteReader();
            List<Category> list = new List<Category>();

            while (reader.Read())
            {
                Category category = new Category();
                category.ID = Convert.ToInt32(reader["ID"]);
                category.Name = reader["Name"].ToString();
                category.Type = Convert.ToInt32(reader["Type"]);
                list.Add(category);
            }

            sqlConnection.Close();
            sqlCommand.Dispose();
            return list;
        }

        public int Insert(Category category)
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "sp_FoodCategory_Insert";
            sqlCommand.Parameters.AddWithValue("@Name", category.Name);
            sqlCommand.Parameters.AddWithValue("@Type", category.Type);

            sqlConnection.Open();
            int newId = Convert.ToInt32(sqlCommand.ExecuteScalar());
            sqlConnection.Close();
            sqlCommand.Dispose();
            return newId;
        }

        public int Update(Category category)
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "sp_FoodCategory_Update";
            sqlCommand.Parameters.AddWithValue("@ID", category.ID);
            sqlCommand.Parameters.AddWithValue("@Name", category.Name);
            sqlCommand.Parameters.AddWithValue("@Type", category.Type);

            sqlConnection.Open();
            int affectedRows = Convert.ToInt32(sqlCommand.ExecuteScalar());
            sqlConnection.Close();
            sqlCommand.Dispose();
            return affectedRows;
        }

        public int Delete(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = "sp_FoodCategory_Delete";
            sqlCommand.Parameters.AddWithValue("@ID", id);

            sqlConnection.Open();
            try
            {
                int affectedRows = Convert.ToInt32(sqlCommand.ExecuteScalar());
                return affectedRows;
            }
            catch (SqlException ex)
            {
                // Re-throw exception để xử lý ở tầng trên
                throw new Exception("Lỗi khi xóa loại thực phẩm: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
                sqlCommand.Dispose();
            }
        }
    }
}
