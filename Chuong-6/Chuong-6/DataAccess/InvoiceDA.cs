using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Chuong_6.DataAccess
{
    public class InvoiceDA
    {
        private readonly string connectionString = "Server=mnhy\\SQLEXPRESS01;Database=chuong_6_restaurant_management;Trusted_Connection=True;";

        // Lấy tất cả Invoice
        public List<Invoice> GetAllInvoices()
        {
            List<Invoice> invoices = new List<Invoice>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Invoice_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            invoices.Add(MapInvoiceFromReader(reader));
                        }
                    }
                }
            }

            return invoices;
        }

        // Lấy Invoice theo ID
        public Invoice GetInvoiceByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Invoice_GetByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapInvoiceFromReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        // Lấy Invoice theo TableID (chưa thanh toán)
        public Invoice GetInvoiceByTableID(int tableID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Invoice_GetByTableID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TableID", tableID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return MapInvoiceFromReader(reader);
                        }
                    }
                }
            }

            return null;
        }

        // Thêm mới Invoice
        public int InsertInvoice(string name, int tableID, string accountID,
                                 int total = 0, float discount = 0, float tax = 0)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Invoice_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@TableID", tableID);
                    cmd.Parameters.AddWithValue("@AccountID", accountID);
                    cmd.Parameters.AddWithValue("@Total", total);
                    cmd.Parameters.AddWithValue("@Discount", discount);
                    cmd.Parameters.AddWithValue("@Tax", tax);

                    conn.Open();
                    try
                    {
                        object result = cmd.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi thêm hóa đơn: {ex.Message}");
                    }
                }
            }
        }

        // Cập nhật Invoice
        public int UpdateInvoice(int id, string name, int total, float discount, float tax)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Invoice_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Name", name);
                    cmd.Parameters.AddWithValue("@Total", total);
                    cmd.Parameters.AddWithValue("@Discount", discount);
                    cmd.Parameters.AddWithValue("@Tax", tax);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi cập nhật hóa đơn: {ex.Message}");
                    }
                }
            }
        }

        // Thanh toán Invoice
        public InvoiceCheckoutResult CheckoutInvoice(int id, float discount = 0, float tax = 0)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Invoice_Checkout", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@Discount", discount);
                    cmd.Parameters.AddWithValue("@Tax", tax);

                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new InvoiceCheckoutResult
                                {
                                    TotalAmount = Convert.ToInt32(reader["TotalAmount"]),
                                    AffectedRows = Convert.ToInt32(reader["AffectedRows"])
                                };
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi thanh toán hóa đơn: {ex.Message}");
                    }
                }
            }
            return null;
        }

        // Xóa Invoice
        public int DeleteInvoice(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Invoice_Delete", conn))
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
                        throw new Exception($"Lỗi khi xóa hóa đơn: {ex.Message}");
                    }
                }
            }
        }

        // Lấy chi tiết hóa đơn
        public List<InvoiceDetailInfo> GetInvoiceDetails(int invoiceID)
        {
            List<InvoiceDetailInfo> details = new List<InvoiceDetailInfo>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Invoice_GetDetails", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@InvoiceID", invoiceID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            details.Add(new InvoiceDetailInfo
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                InvoiceID = invoiceID,
                                FoodID = Convert.ToInt32(reader["FoodID"]),
                                FoodName = reader["FoodName"].ToString(),
                                Unit = reader["Unit"].ToString(),
                                Price = Convert.ToInt32(reader["Price"]),
                                Amount = Convert.ToInt32(reader["Amount"]),
                                Total = Convert.ToInt32(reader["Total"])
                            });
                        }
                    }
                }
            }

            return details;
        }

        // Tính tổng tiền hóa đơn
        public InvoiceTotal CalculateTotal(int invoiceID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Invoice_CalculateTotal", conn))
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
                                return new InvoiceTotal
                                {
                                    SubTotal = Convert.ToInt32(reader["SubTotal"]),
                                    Discount = Convert.ToDouble(reader["Discount"]),
                                    Tax = Convert.ToDouble(reader["Tax"]),
                                    Total = Convert.ToInt32(reader["Total"])
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

        // Lấy hóa đơn theo ngày
        public List<Invoice> GetInvoicesByDate(DateTime fromDate, DateTime toDate)
        {
            List<Invoice> invoices = new List<Invoice>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Invoice_GetByDate", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            invoices.Add(MapInvoiceFromReader(reader));
                        }
                    }
                }
            }

            return invoices;
        }

        // Lấy thống kê doanh thu
        public RevenueStatistic GetRevenue(DateTime fromDate, DateTime toDate)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Invoice_GetRevenue", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@FromDate", fromDate);
                    cmd.Parameters.AddWithValue("@ToDate", toDate);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new RevenueStatistic
                            {
                                TotalInvoices = Convert.ToInt32(reader["TotalInvoices"]),
                                TotalRevenue = Convert.ToInt32(reader["TotalRevenue"]),
                                TotalDiscount = Convert.ToInt32(reader["TotalDiscount"]),
                                TotalTax = Convert.ToInt32(reader["TotalTax"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Kiểm tra Invoice có tồn tại không
        public bool CheckInvoiceExists(int id)
        {
            Invoice invoice = GetInvoiceByID(id);
            return invoice != null;
        }

        // Lấy thông tin Invoice dưới dạng object
        public Invoice GetInvoiceObject(int id)
        {
            return GetInvoiceByID(id);
        }

        // Helper method để map từ SqlDataReader sang Invoice object
        private Invoice MapInvoiceFromReader(SqlDataReader reader)
        {
            return new Invoice
            {
                ID = Convert.ToInt32(reader["ID"]),
                Name = reader["Name"].ToString(),
                TableID = Convert.ToInt32(reader["TableID"]),
                TableName = reader["TableName"] != DBNull.Value ? reader["TableName"].ToString() : null,
                Total = Convert.ToInt32(reader["Total"]),
                Discount = (float)Convert.ToDouble(reader["Discount"]),
                Tax = (float)Convert.ToDouble(reader["Tax"]),
                Status = Convert.ToBoolean(reader["Status"]),
                AccountID = reader["AccountID"].ToString(),
                AccountName = reader["AccountName"] != DBNull.Value ? reader["AccountName"].ToString() : null,
                CheckoutDate = Convert.ToDateTime(reader["CheckoutDate"])
            };
        }
    }

    // Model classes bổ sung
    public class InvoiceCheckoutResult
    {
        public int TotalAmount { get; set; }
        public int AffectedRows { get; set; }
    }

    public class InvoiceTotal
    {
        public int SubTotal { get; set; }
        public double Discount { get; set; }
        public double Tax { get; set; }
        public int Total { get; set; }
    }

    public class InvoiceDetailInfo
    {
        public int ID { get; set; }
        public int InvoiceID { get; set; }
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public string Unit { get; set; }
        public int Price { get; set; }
        public int Amount { get; set; }
        public int Total { get; set; }
    }

    public class RevenueStatistic
    {
        public int TotalInvoices { get; set; }
        public int TotalRevenue { get; set; }
        public int TotalDiscount { get; set; }
        public int TotalTax { get; set; }
    }
}
