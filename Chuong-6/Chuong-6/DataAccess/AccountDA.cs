using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Chuong_6.DataAccess
{
    public class AccountDA
    {
        private readonly string connectionString = "Server=mnhy\\SQLEXPRESS01;Database=chuong_6_restaurant_management;Trusted_Connection=True;";

        // Lấy tất cả Account
        public List<Account> GetAllAccounts()
        {
            List<Account> accounts = new List<Account>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Account_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accounts.Add(new Account
                            {
                                AccountName = reader["AccountName"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                Phone = reader["Phone"].ToString(),
                                DateCreated = Convert.ToDateTime(reader["DateCreated"])
                            });
                        }
                    }
                }
            }

            return accounts;
        }

        // Lấy Account theo AccountName
        public Account GetAccountByName(string accountName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Account_GetByName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountName", accountName);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Account
                            {
                                AccountName = reader["AccountName"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                Phone = reader["Phone"].ToString(),
                                DateCreated = Convert.ToDateTime(reader["DateCreated"])
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Thêm mới Account
        public string InsertAccount(string accountName, string password, string fullName,
                                    string email, string phone)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Account_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountName", accountName);
                    cmd.Parameters.AddWithValue("@Password", password);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", phone);

                    conn.Open();
                    try
                    {
                        object result = cmd.ExecuteScalar();
                        return result?.ToString();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi thêm tài khoản: {ex.Message}");
                    }
                }
            }
        }

        // Cập nhật Account
        public int UpdateAccount(string accountName, string fullName, string email, string phone)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Account_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountName", accountName);
                    cmd.Parameters.AddWithValue("@FullName", fullName);
                    cmd.Parameters.AddWithValue("@Email", (object)email ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Phone", phone);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi cập nhật tài khoản: {ex.Message}");
                    }
                }
            }
        }

        // Xóa Account
        public int DeleteAccount(string accountName)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Account_Delete", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountName", accountName);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi xóa tài khoản: {ex.Message}");
                    }
                }
            }
        }

        // Lấy danh sách Role của Account
        public List<RoleAccount> GetAccountRoles(string accountName)
        {
            List<RoleAccount> roles = new List<RoleAccount>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Account_GetRoles", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountName", accountName);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new RoleAccount
                            {
                                AccountName = accountName,
                                RoleID = Convert.ToInt32(reader["ID"]),
                                RoleName = reader["RoleName"].ToString(),
                                Path = reader["Path"] != DBNull.Value ? reader["Path"].ToString() : null,
                                RoleNotes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null,
                                Actived = Convert.ToBoolean(reader["Actived"]),
                                Notes = reader["RoleNotes"] != DBNull.Value ? reader["RoleNotes"].ToString() : null
                            });
                        }
                    }
                }
            }

            return roles;
        }

        // Reset mật khẩu
        public int ResetPassword(string accountName, string newPassword)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Account_ResetPassword", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountName", accountName);
                    cmd.Parameters.AddWithValue("@NewPassword", newPassword);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi reset mật khẩu: {ex.Message}");
                    }
                }
            }
        }

        // Kiểm tra Account có tồn tại không
        public bool CheckAccountExists(string accountName)
        {
            Account account = GetAccountByName(accountName);
            return account != null;
        }

        // Lấy thông tin Account dưới dạng object
        public Account GetAccountObject(string accountName)
        {
            return GetAccountByName(accountName);
        }
    }
}
