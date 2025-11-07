using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Chuong_6.DataAccess
{
    public class RoleDA
    {
        private readonly string connectionString = "Server=mnhy\\SQLEXPRESS01;Database=chuong_6_restaurant_management;Trusted_Connection=True;";

        // Lấy tất cả Role
        public List<Role> GetAllRoles()
        {
            List<Role> roles = new List<Role>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Role_GetAll", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Role
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                RoleName = reader["RoleName"].ToString(),
                                Path = reader["Path"] != DBNull.Value ? reader["Path"].ToString() : null,
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null
                            });
                        }
                    }
                }
            }

            return roles;
        }

        // Lấy Role theo ID
        public Role GetRoleByID(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Role_GetByID", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Role
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                RoleName = reader["RoleName"].ToString(),
                                Path = reader["Path"] != DBNull.Value ? reader["Path"].ToString() : null,
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null
                            };
                        }
                    }
                }
            }

            return null;
        }

        // Thêm mới Role
        public int InsertRole(string roleName, string path = null, string notes = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Role_Insert", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleName", roleName);
                    cmd.Parameters.AddWithValue("@Path", (object)path ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

                    conn.Open();
                    try
                    {
                        object result = cmd.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi thêm vai trò: {ex.Message}");
                    }
                }
            }
        }

        // Cập nhật Role
        public int UpdateRole(int id, string roleName, string path = null, string notes = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Role_Update", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ID", id);
                    cmd.Parameters.AddWithValue("@RoleName", roleName);
                    cmd.Parameters.AddWithValue("@Path", (object)path ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi cập nhật vai trò: {ex.Message}");
                    }
                }
            }
        }

        // Xóa Role
        public int DeleteRole(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Role_Delete", conn))
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
                        throw new Exception($"Lỗi khi xóa vai trò: {ex.Message}");
                    }
                }
            }
        }

        // Tìm kiếm Role theo tên
        public List<Role> SearchRoleByName(string searchTerm)
        {
            List<Role> roles = new List<Role>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Role_SearchByName", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            roles.Add(new Role
                            {
                                ID = Convert.ToInt32(reader["ID"]),
                                RoleName = reader["RoleName"].ToString(),
                                Path = reader["Path"] != DBNull.Value ? reader["Path"].ToString() : null,
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null
                            });
                        }
                    }
                }
            }

            return roles;
        }

        // Lấy thống kê Role
        public List<RoleStatistic> GetRoleStatistics()
        {
            List<RoleStatistic> statistics = new List<RoleStatistic>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Role_GetStatistics", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            statistics.Add(new RoleStatistic
                            {
                                RoleID = Convert.ToInt32(reader["ID"]),
                                RoleName = reader["RoleName"].ToString(),
                                TotalAccounts = Convert.ToInt32(reader["TotalAccounts"]),
                                ActiveAccounts = Convert.ToInt32(reader["ActiveAccounts"])
                            });
                        }
                    }
                }
            }

            return statistics;
        }

        // Lấy danh sách Account theo Role
        public List<RoleAccountInfo> GetAccountsByRole(int roleID)
        {
            List<RoleAccountInfo> accounts = new List<RoleAccountInfo>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_Role_GetAccounts", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@RoleID", roleID);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            accounts.Add(new RoleAccountInfo
                            {
                                AccountName = reader["AccountName"].ToString(),
                                FullName = reader["FullName"].ToString(),
                                Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : null,
                                Phone = reader["Phone"].ToString(),
                                RoleID = roleID,
                                Actived = Convert.ToBoolean(reader["Actived"]),
                                Notes = reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : null
                            });
                        }
                    }
                }
            }

            return accounts;
        }

        // Gán Role cho Account
        public RoleAssignResult AssignRoleToAccount(string accountName, int roleID, string notes = null)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RoleAccount_Assign", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountName", accountName);
                    cmd.Parameters.AddWithValue("@RoleID", roleID);
                    cmd.Parameters.AddWithValue("@Notes", (object)notes ?? DBNull.Value);

                    conn.Open();
                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new RoleAssignResult
                                {
                                    Result = Convert.ToInt32(reader["Result"]),
                                    Message = reader["Message"].ToString()
                                };
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi gán vai trò: {ex.Message}");
                    }
                }
            }
            return null;
        }

        // Xóa Role khỏi Account
        public int RemoveRoleFromAccount(string accountName, int roleID)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RoleAccount_Remove", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountName", accountName);
                    cmd.Parameters.AddWithValue("@RoleID", roleID);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi xóa vai trò: {ex.Message}");
                    }
                }
            }
        }

        // Cập nhật trạng thái RoleAccount
        public int ToggleRoleAccountStatus(string accountName, int roleID, bool actived)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("sp_RoleAccount_ToggleStatus", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AccountName", accountName);
                    cmd.Parameters.AddWithValue("@RoleID", roleID);
                    cmd.Parameters.AddWithValue("@Actived", actived);

                    conn.Open();
                    try
                    {
                        return (int)cmd.ExecuteScalar();
                    }
                    catch (SqlException ex)
                    {
                        throw new Exception($"Lỗi khi cập nhật trạng thái: {ex.Message}");
                    }
                }
            }
        }

        // Kiểm tra Role có tồn tại không
        public bool CheckRoleExists(int id)
        {
            Role role = GetRoleByID(id);
            return role != null;
        }

        // Lấy thông tin Role dưới dạng object
        public Role GetRoleObject(int id)
        {
            return GetRoleByID(id);
        }
    }
}
