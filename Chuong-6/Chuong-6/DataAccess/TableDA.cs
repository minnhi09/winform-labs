using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Chuong_6.DataAccess
{
    public class TableDA
    {
        private readonly string connectionString = "Server=mnhy\\SQLEXPRESS01;Database=chuong_6_restaurant_management;Trusted_Connection=True;";

        // Lấy tất cả Table
        public List<Table> GetAllTables()
        {
            List<Table> tables = new List<Table>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Table_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(CreateTableFromReader(reader));
                        }
                    }
                }
            }

            return tables;
        }

        // Lấy Table theo ID
        public Table GetTableByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Table_GetByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return CreateTableFromReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        // Lấy Table theo HallID
        public List<Table> GetTablesByHallID(int hallID)
        {
            List<Table> tables = new List<Table>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Table_GetByHallID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@HallID", hallID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(CreateTableFromReader(reader));
                        }
                    }
                }
            }

            return tables;
        }

        // Lấy Table theo Status
        public List<Table> GetTablesByStatus(int status)
        {
            List<Table> tables = new List<Table>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Table_GetByStatus", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Status", status);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tables.Add(CreateTableFromReader(reader));
                        }
                    }
                }
            }

            return tables;
        }

        // Thêm mới Table
        public int InsertTable(string tableCode, string name, int status, int? seats, int hallID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Table_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TableCode", tableCode);
                    cmd.Parameters.AddWithValue("@Name", (object)name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Seats", (object)seats ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@HallID", hallID);

                    conn.Open();
                    try
                    {
                        object result = cmd.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi thêm bàn: {ex.Message}");
                    }
                }
            }
        }

        // Cập nhật Table
        public int UpdateTable(int id, string tableCode, string name, int status, int? seats, int hallID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Table_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@TableCode", tableCode);
                    cmd.Parameters.AddWithValue("@Name", (object)name ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Status", status);
                    cmd.Parameters.AddWithValue("@Seats", (object)seats ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@HallID", hallID);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi cập nhật bàn: {ex.Message}");
                    }
                }
            }
        }

        // Xóa Table
        public int DeleteTable(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Table_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi xóa bàn: {ex.Message}");
                    }
                }
            }
        }

        // Cập nhật trạng thái Table
        public int UpdateTableStatus(int id, int status)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Table_UpdateStatus", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Status", status);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi cập nhật trạng thái bàn: {ex.Message}");
                    }
                }
            }
        }

        // Kiểm tra Table có tồn tại không
        public bool CheckTableExists(int id)
        {
            Table table = GetTableByID(id);
            return table != null;
        }

        // Lấy thông tin Table dưới dạng object
        public Table GetTableObject(int id)
        {
            return GetTableByID(id);
        }

        // Helper method để tạo Table object từ DataReader
        private Table CreateTableFromReader(SqlDataReader reader)
        {
            return new Table
            {
                ID = Convert.ToInt32(reader["ID"]),
                TableCode = reader["TableCode"].ToString(),
                Name = reader["Name"] != DBNull.Value ? reader["Name"].ToString() : null,
                Status = Convert.ToInt32(reader["Status"]),
                Seats = reader["Seats"] != DBNull.Value ? Convert.ToInt32(reader["Seats"]) : 0,
                HallID = Convert.ToInt32(reader["HallID"]),
                HallName = reader["HallName"]?.ToString(),
                RestaurantID = Convert.ToInt32(reader["RestaurantID"]),
                RestaurantName = reader["RestaurantName"]?.ToString()
            };
        }

        // Lấy trạng thái của bàn dưới dạng string
        public static string GetStatusText(int status)
        {
            switch (status)
            {
                case 0:
                    return "Trống";
                case 1:
                    return "Đã đặt";
                case 2:
                    return "Có khách";
                default:
                    return "Không xác định";
            }
        }
    }
}
