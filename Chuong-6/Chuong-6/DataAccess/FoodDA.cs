using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Chuong_6.DataAccess
{
    public class FoodDA
    {
        private readonly string connectionString = "Server=mnhy\\SQLEXPRESS01;Database=chuong_6_restaurant_management;Trusted_Connection=True;";

        // Lấy tất cả Food
        public List<Food> GetAllFoods()
        {
            List<Food> foods = new List<Food>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Food_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Food food = new Food
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Name = reader["Name"].ToString(),
                                Unit = reader["Unit"].ToString(),
                                FoodCategoryID = Convert.ToInt32(reader["FoodCategoryID"]),
                                Price = Convert.ToInt32(reader["Price"]),
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                                CategoryName = reader["CategoryName"].ToString()
                            };

                            foods.Add(food);
                        }
                    }
                }
            }

            return foods;
        }

        // Lấy Food theo ID
        public Food GetFoodByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Food_GetByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Food
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Name = reader["Name"].ToString(),
                                Unit = reader["Unit"].ToString(),
                                FoodCategoryID = Convert.ToInt32(reader["FoodCategoryID"]),
                                Price = Convert.ToInt32(reader["Price"]),
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                                CategoryName = reader["CategoryName"].ToString()
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Lấy Food theo CategoryID
        public List<Food> GetFoodsByCategoryID(int categoryID)
        {
            List<Food> foods = new List<Food>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Food_GetByCategoryID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FoodCategoryID", categoryID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            foods.Add(new Food
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                Name = reader["Name"].ToString(),
                                Unit = reader["Unit"].ToString(),
                                FoodCategoryID = Convert.ToInt32(reader["FoodCategoryID"]),
                                Price = Convert.ToInt32(reader["Price"]),
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                                CategoryName = reader["CategoryName"].ToString()
                            });
                        }
                    }
                }
            }

            return foods;
        }

        // Thêm mới Food
        public int InsertFood(string name, string unit, int categoryID, int price, string notes = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Food_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Unit", unit);
                    cmd.Parameters.AddWithValue("@FoodCategoryID", categoryID);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

                    conn.Open();
                    try
                    {
                        object result = cmd.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi thêm món ăn: {ex.Message}");
                    }
                }
            }
        }

        // Cập nhật Food
        public int UpdateFood(int id, string name, string unit, int categoryID, int price, string notes = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Food_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Unit", unit);
                    cmd.Parameters.AddWithValue("@FoodCategoryID", categoryID);
                    cmd.Parameters.AddWithValue("@Price", price);
                    cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi cập nhật món ăn: {ex.Message}");
                    }
                }
            }
        }

        // Xóa Food
        public int DeleteFood(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Food_Delete", conn))
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
                        throw new Exception($"Lỗi khi xóa món ăn: {ex.Message}");
                    }
                }
            }
        }

        // Lấy thông tin Food dưới dạng object
        public Food GetFoodObject(int id)
        {
            return GetFoodByID(id);
        }
    }
}
