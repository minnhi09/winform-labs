using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Chuong_6.DataAccess
{
    public class InvoiceDetailsDA
    {
        private readonly string connectionString = "Server=mnhy\\SQLEXPRESS01;Database=chuong_6_restaurant_management;Trusted_Connection=True;";

        // Lấy tất cả InvoiceDetails
        public List<InvoiceDetail> GetAllInvoiceDetails()
        {
            List<InvoiceDetail> details = new List<InvoiceDetail>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InvoiceDetail_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            details.Add(MapInvoiceDetailFromReader(reader));
                        }
                    }
                }
            }

            return details;
        }

        // Lấy InvoiceDetails theo ID
        public InvoiceDetail GetInvoiceDetailByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InvoiceDetail_GetByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapInvoiceDetailFromReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        // Lấy InvoiceDetails theo InvoiceID
        public List<InvoiceDetail> GetInvoiceDetailsByInvoiceID(int invoiceID)
        {
            List<InvoiceDetail> details = new List<InvoiceDetail>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InvoiceDetail_GetByInvoiceID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            details.Add(MapInvoiceDetailFromReader(reader));
                        }
                    }
                }
            }

            return details;
        }

        // Thêm mới InvoiceDetails (tự động cộng dồn nếu món đã có)
        public int InsertInvoiceDetail(int invoiceID, int foodID, int amount)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InvoiceDetail_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                    cmd.Parameters.AddWithValue("@FoodID", foodID);
                    cmd.Parameters.AddWithValue("@Amount", amount);

                    conn.Open();
                    try
                    {
                        object result = cmd.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi thêm chi tiết hóa đơn: {ex.Message}");
                    }
                }
            }
        }

        // Cập nhật số lượng InvoiceDetails
        public InvoiceDetailUpdateResult UpdateInvoiceDetail(int id, int amount)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InvoiceDetail_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Amount", amount);

                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var result = new InvoiceDetailUpdateResult
                                {
                                    AffectedRows = Convert.ToInt32(reader["AffectedRows"])
                                };

                                if (reader.FieldCount > 1 && reader["Message"] != DBNull.Value)
                                {
                                    result.Message = reader["Message"].ToString();
                                }

                                return result;
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi cập nhật chi tiết hóa đơn: {ex.Message}");
                    }
                }
            }
            return null;
        }

        // Xóa InvoiceDetails
        public int DeleteInvoiceDetail(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InvoiceDetail_Delete", conn))
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
                        throw new Exception($"Lỗi khi xóa chi tiết hóa đơn: {ex.Message}");
                    }
                }
            }
        }

        // Xóa tất cả InvoiceDetails của một Invoice
        public int DeleteInvoiceDetailsByInvoiceID(int invoiceID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InvoiceDetail_DeleteByInvoiceID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi xóa chi tiết hóa đơn: {ex.Message}");
                    }
                }
            }
        }

        // Cập nhật số lượng món (tăng/giảm)
        public InvoiceDetailAmountResult UpdateAmount(int id, int amountChange)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InvoiceDetail_UpdateAmount", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@AmountChange", amountChange);

                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var result = new InvoiceDetailAmountResult
                                {
                                    NewAmount = Convert.ToInt32(reader["NewAmount"])
                                };

                                if (reader.FieldCount > 1 && reader["Message"] != DBNull.Value)
                                {
                                    result.Message = reader["Message"].ToString();
                                }

                                return result;
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi cập nhật số lượng: {ex.Message}");
                    }
                }
            }
            return null;
        }

        // Lấy tổng tiền theo InvoiceID
        public InvoiceDetailTotal GetTotalByInvoiceID(int invoiceID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InvoiceDetail_GetTotalByInvoiceID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);

                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new InvoiceDetailTotal
                                {
                                    InvoiceID = Convert.ToInt32(reader["InvoiceID"]),
                                    TotalItems = Convert.ToInt32(reader["TotalItems"]),
                                    TotalQuantity = reader["TotalQuantity"] != DBNull.Value
                                        ? Convert.ToInt32(reader["TotalQuantity"]) : 0,
                                    TotalAmount = reader["TotalAmount"] != DBNull.Value
                                        ? Convert.ToInt32(reader["TotalAmount"]) : 0
                                };
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi tính tổng tiền: {ex.Message}");
                    }
                }
            }
            return null;
        }

        // Kiểm tra món ăn đã có trong hóa đơn chưa
        public FoodExistsResult CheckFoodExists(int invoiceID, int foodID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_InvoiceDetail_CheckFoodExists", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);
                    cmd.Parameters.AddWithValue("@FoodID", foodID);

                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new FoodExistsResult
                                {
                                    IsExists = Convert.ToBoolean(reader["IsExists"]),
                                    CurrentAmount = Convert.ToInt32(reader["CurrentAmount"])
                                };
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi kiểm tra món ăn: {ex.Message}");
                    }
                }
            }
            return null;
        }

        // Kiểm tra InvoiceDetail có tồn tại không
        public bool CheckInvoiceDetailExists(int id)
        {
            InvoiceDetail detail = GetInvoiceDetailByID(id);
            return detail != null;
        }

        // Lấy thông tin InvoiceDetail dưới dạng object
        public InvoiceDetail GetInvoiceDetailObject(int id)
        {
            return GetInvoiceDetailByID(id);
        }

        // Helper method để map từ SqlDataReader sang InvoiceDetail object
        private InvoiceDetail MapInvoiceDetailFromReader(SqlDataReader reader)
        {
            return new InvoiceDetail
            {
                ID = Convert.ToInt32(reader["ID"]),
                InvoiceID = Convert.ToInt32(reader["InvoiceID"]),
                FoodID = Convert.ToInt32(reader["FoodID"]),
                FoodName = reader["FoodName"]?.ToString() ?? string.Empty,
                Unit = reader["Unit"]?.ToString() ?? string.Empty,
                Price = reader["Price"] != DBNull.Value ? Convert.ToInt32(reader["Price"]) : 0,
                Amount = reader["Amount"] != DBNull.Value ? Convert.ToInt32(reader["Amount"]) : 0,
                Total = reader["Total"] != DBNull.Value ? Convert.ToInt32(reader["Total"]) : 0
            };
        }
    }

    // Model classes bổ sung
    public class InvoiceDetail
    {
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public int FoodID { get; set; }
        public string FoodName { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Total { get; set; }
    }

    public class InvoiceDetailUpdateResult
    {
        public int AffectedRows { get; set; }
        public string Message { get; set; }
    }

    public class InvoiceDetailAmountResult
    {
        public int NewAmount { get; set; }
        public string Message { get; set; }
    }

    public class InvoiceDetailTotal
    {
        public int InvoiceID { get; set; }
        public int TotalItems { get; set; }
        public int TotalQuantity { get; set; }
        public int TotalAmount { get; set; }
    }

    public class FoodExistsResult
    {
        public bool IsExists { get; set; }
        public int CurrentAmount { get; set; }
    }
}
