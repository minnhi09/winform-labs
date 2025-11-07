using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Chuong_6.DataAccess
{
    public class HallDA
    {
        private readonly string connectionString = "Server=mnhy\\SQLEXPRESS01;Database=chuong_6_restaurant_management;Trusted_Connection=True;";

        // Lấy tất cả Hall
        public List<Hall> GetAllHalls()
        {
            List<Hall> halls = new List<Hall>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Hall_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Hall hall = new Hall
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Name = reader["Name"].ToString(),
                                RestaurantID = Convert.ToInt32(reader["RestaurantID"])
                            };

                            // Kiểm tra xem cột RestaurantName có tồn tại không
                            try
                            {
                                int ordinal = reader.GetOrdinal("RestaurantName");
                                if (ordinal >= 0)
                                {
                                    hall.RestaurantName = reader["RestaurantName"].ToString();
                                }
                            }
                            catch
                            {
                                hall.RestaurantName = null;
                            }

                            halls.Add(hall);
                        }
                    }
                }
            }

            return halls;
        }

        // Lấy Hall theo ID
        public Hall GetHallByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Hall_GetByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Hall hall = new Hall
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Name = reader["Name"].ToString(),
                                RestaurantID = Convert.ToInt32(reader["RestaurantID"])
                            };

                            try
                            {
                                int ordinal = reader.GetOrdinal("RestaurantName");
                                if (ordinal >= 0)
                                {
                                    hall.RestaurantName = reader["RestaurantName"].ToString();
                                }
                            }
                            catch
                            {
                                hall.RestaurantName = null;
                            }

                            return hall;
                        }
                    }
                }
            }

            return null;
        }

        // Lấy Hall theo RestaurantID
        public List<Hall> GetHallsByRestaurantID(int restaurantID)
        {
            List<Hall> halls = new List<Hall>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Hall_GetByRestaurantID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RestaurantID", restaurantID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            halls.Add(new Hall
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Name = reader["Name"].ToString(),
                                RestaurantID = Convert.ToInt32(reader["RestaurantID"])
                            });
                        }
                    }
                }
            }

            return halls;
        }

        // Thêm mới Hall
        public int InsertHall(string name, int restaurantID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Hall_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@RestaurantID", restaurantID);

                    conn.Open();
                    try
                    {
                        object result = cmd.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi thêm sảnh: {ex.Message}");
                    }
                }
            }
        }

        // Cập nhật Hall
        public int UpdateHall(int id, string name, int restaurantID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Hall_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@RestaurantID", restaurantID);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi cập nhật sảnh: {ex.Message}");
                    }
                }
            }
        }

        // Xóa Hall
        public int DeleteHall(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Hall_Delete", conn))
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
                        throw new Exception($"Lỗi khi xóa sảnh: {ex.Message}");
                    }
                }
            }
        }

        // Kiểm tra Hall có tồn tại không
        public bool CheckHallExists(int id)
        {
            Hall hall = GetHallByID(id);
            return hall != null;
        }

        // Lấy thông tin Hall dưới dạng object
        public Hall GetHallObject(int id)
        {
            return GetHallByID(id);
        }
    }
}
