using System;
using System.Collections.Generic;
using System.Linq;
using Chuong_6.DataAccess;

namespace Chuong_6.BusinessLogic
{
    public class RoleBL
    {
        private readonly RoleDA roleDA;

        public RoleBL()
        {
            this.roleDA = new RoleDA();
        }

        // Lấy tất cả Role
        public List<Role> GetAllRoles()
        {
            try
            {
                return roleDA.GetAllRoles();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách vai trò: {ex.Message}");
            }
        }

        // Lấy Role theo ID
        public Role GetRoleByID(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID vai trò không hợp lệ!");
            }

            try
            {
                return roleDA.GetRoleByID(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin vai trò: {ex.Message}");
            }
        }

        // Thêm Role mới
        public int AddRole(string roleName, string path = null, string notes = null)
        {
            // Validate dữ liệu
            ValidateRoleData(roleName, path);

            try
            {
                return roleDA.InsertRole(roleName, path, notes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm vai trò: {ex.Message}");
            }
        }

        // Cập nhật Role
        public int UpdateRole(int id, string roleName, string path = null, string notes = null)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID vai trò không hợp lệ!");
            }

            // Validate dữ liệu
            ValidateRoleData(roleName, path);

            // Kiểm tra Role có tồn tại không
            if (!roleDA.CheckRoleExists(id))
            {
                throw new Exception("Vai trò không tồn tại!");
            }

            try
            {
                return roleDA.UpdateRole(id, roleName, path, notes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật vai trò: {ex.Message}");
            }
        }

        // Xóa Role
        public int DeleteRole(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID vai trò không hợp lệ!");
            }

            // Không cho phép xóa role admin (ID = 1)
            if (id == 1)
            {
                throw new Exception("Không thể xóa vai trò Quản trị viên!");
            }

            // Kiểm tra Role có tồn tại không
            if (!roleDA.CheckRoleExists(id))
            {
                throw new Exception("Vai trò không tồn tại!");
            }

            try
            {
                return roleDA.DeleteRole(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa vai trò: {ex.Message}");
            }
        }

        // Tìm kiếm Role theo tên
        public List<Role> SearchRoleByName(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return GetAllRoles();
            }

            try
            {
                return roleDA.SearchRoleByName(searchTerm);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi tìm kiếm vai trò: {ex.Message}");
            }
        }

        // Lấy thống kê Role
        public List<RoleStatistic> GetRoleStatistics()
        {
            try
            {
                return roleDA.GetRoleStatistics();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thống kê vai trò: {ex.Message}");
            }
        }

        // Lấy danh sách Account theo Role
        public List<RoleAccountInfo> GetAccountsByRole(int roleID)
        {
            if (roleID <= 0)
            {
                throw new ArgumentException("ID vai trò không hợp lệ!");
            }

            try
            {
                return roleDA.GetAccountsByRole(roleID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách tài khoản: {ex.Message}");
            }
        }

        // Gán Role cho Account
        public RoleAssignResult AssignRoleToAccount(string accountName, int roleID, string notes = null)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (roleID <= 0)
            {
                throw new ArgumentException("ID vai trò không hợp lệ!");
            }

            try
            {
                return roleDA.AssignRoleToAccount(accountName, roleID, notes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi gán vai trò: {ex.Message}");
            }
        }

        // Xóa Role khỏi Account
        public int RemoveRoleFromAccount(string accountName, int roleID)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (roleID <= 0)
            {
                throw new ArgumentException("ID vai trò không hợp lệ!");
            }

            // Không cho phép xóa quyền admin của tài khoản admin
            if (accountName.ToLower() == "admin" && roleID == 1)
            {
                throw new Exception("Không thể xóa quyền Quản trị viên của tài khoản admin!");
            }

            try
            {
                return roleDA.RemoveRoleFromAccount(accountName, roleID);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa vai trò: {ex.Message}");
            }
        }

        // Kích hoạt/Vô hiệu hóa vai trò của tài khoản
        public int ToggleRoleAccountStatus(string accountName, int roleID, bool actived)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (roleID <= 0)
            {
                throw new ArgumentException("ID vai trò không hợp lệ!");
            }

            // Không cho phép vô hiệu hóa quyền admin của tài khoản admin
            if (accountName.ToLower() == "admin" && roleID == 1 && !actived)
            {
                throw new Exception("Không thể vô hiệu hóa quyền Quản trị viên của tài khoản admin!");
            }

            try
            {
                return roleDA.ToggleRoleAccountStatus(accountName, roleID, actived);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật trạng thái: {ex.Message}");
            }
        }

        // Validate dữ liệu Role
        private void ValidateRoleData(string roleName, string path)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Tên vai trò không được để trống!");
            }

            if (roleName.Length > 100)
            {
                throw new ArgumentException("Tên vai trò không được vượt quá 100 ký tự!");
            }

            if (!string.IsNullOrWhiteSpace(path))
            {
                if (path.Length > 500)
                {
                    throw new ArgumentException("Đường dẫn không được vượt quá 500 ký tự!");
                }

                if (!path.StartsWith("/"))
                {
                    throw new ArgumentException("Đường dẫn phải bắt đầu bằng dấu /");
                }
            }
        }

        // Kiểm tra Role có tồn tại không
        public bool CheckRoleExists(int id)
        {
            return roleDA.CheckRoleExists(id);
        }

        // Lấy thông tin Role dưới dạng object
        public Role GetRoleObject(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("ID vai trò không hợp lệ!");
            }

            try
            {
                return roleDA.GetRoleObject(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy thông tin vai trò: {ex.Message}");
            }
        }

        // Đếm số lượng Role
        public int GetTotalRoles()
        {
            List<Role> roles = GetAllRoles();
            return roles.Count;
        }

        // Lấy tên Role theo ID
        public string GetRoleName(int id)
        {
            if (id <= 0)
            {
                return string.Empty;
            }

            Role role = GetRoleObject(id);
            return role?.RoleName ?? string.Empty;
        }

        // Kiểm tra tên Role có trùng không
        public bool IsDuplicateName(string roleName, int excludeID = 0)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return false;
            }

            List<Role> roles = GetAllRoles();
            string nameLower = roleName.Trim().ToLower();

            return roles.Any(r =>
                r.ID != excludeID &&
                r.RoleName.Trim().ToLower() == nameLower
            );
        }

        // Validate trước khi xóa
        public string ValidateBeforeDelete(int id)
        {
            if (id <= 0)
            {
                return "ID vai trò không hợp lệ!";
            }

            if (id == 1)
            {
                return "Không thể xóa vai trò Quản trị viên!";
            }

            if (!CheckRoleExists(id))
            {
                return "Vai trò không tồn tại!";
            }

            // Kiểm tra có tài khoản nào đang dùng vai trò này không
            List<RoleAccountInfo> accounts = GetAccountsByRole(id);
            if (accounts.Count > 0)
            {
                return $"Không thể xóa vai trò đang được gán cho {accounts.Count} tài khoản!";
            }

            return null; // Có thể xóa
        }

        // Đếm số lượng tài khoản đang sử dụng Role
        public int GetAccountCountByRole(int roleID)
        {
            if (roleID <= 0)
            {
                return 0;
            }

            List<RoleAccountInfo> accounts = GetAccountsByRole(roleID);
            return accounts.Count;
        }

        // Đếm số lượng tài khoản active đang sử dụng Role
        public int GetActiveAccountCountByRole(int roleID)
        {
            if (roleID <= 0)
            {
                return 0;
            }

            List<RoleAccountInfo> accounts = GetAccountsByRole(roleID);
            return accounts.Count(a => a.Actived);
        }
    }
}
