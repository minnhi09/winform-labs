using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Chuong_6.DataAccess
{
    public class RestaurantDA
    {
        private readonly string connectionString = "Server=mnhy\\SQLEXPRESS01;Database=chuong_6_restaurant_management;Trusted_Connection=True;";

        // Lấy tất cả Restaurant
        public List<Restaurant> GetAllRestaurants()
        {
            List<Restaurant> restaurants = new List<Restaurant>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Restaurant_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            restaurants.Add(new Restaurant
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Name = reader["Name"].ToString(),
                                Address = reader["Address"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Website = reader["Website"] != DBNull.Value ? reader["Website"].ToString() : null
                            });
                        }
                    }
                }
            }

            return restaurants;
        }

        // Lấy Restaurant theo ID
        public Restaurant GetRestaurantByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Restaurant_GetByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Restaurant
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Name = reader["Name"].ToString(),
                                Address = reader["Address"].ToString(),
                                Phone = reader["Phone"].ToString(),
                                Website = reader["Website"] != DBNull.Value ? reader["Website"].ToString() : null
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Thêm mới Restaurant
        public int InsertRestaurant(string name, string address, string phone, string website = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Restaurant_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Website", (object)website ?? DBNull.Value);

                    conn.Open();
                    try
                    {
                        object result = cmd.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi thêm nhà hàng: {ex.Message}");
                    }
                }
            }
        }

        // Cập nhật Restaurant
        public int UpdateRestaurant(int id, string name, string address, string phone, string website = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Restaurant_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Address", address);
                    cmd.Parameters.AddWithValue("@Phone", phone);
                    cmd.Parameters.AddWithValue("@Website", (object)website ?? DBNull.Value);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi cập nhật nhà hàng: {ex.Message}");
                    }
                }
            }
        }

        // Xóa Restaurant
        public int DeleteRestaurant(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Restaurant_Delete", conn))
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
                        throw new Exception($"Lỗi khi xóa nhà hàng: {ex.Message}");
                    }
                }
            }
        }

        // Kiểm tra Restaurant có tồn tại không
        public bool CheckRestaurantExists(int id)
        {
            Restaurant restaurant = GetRestaurantByID(id);
            return restaurant != null;
        }

        // Lấy thông tin Restaurant dưới dạng object
        public Restaurant GetRestaurantObject(int id)
        {
            return GetRestaurantByID(id);
        }
    }
}
