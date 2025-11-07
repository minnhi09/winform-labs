using System;
using System.Data;
using System.Text.RegularExpressions;
using Chuong_6.DataAccess;

namespace Chuong_6.BusinessLogic
{
    public class AuthBL
    {
        private readonly AuthDA authDA;

        public AuthBL()
        {
            this.authDA = new AuthDA();
        }

        /// <summary>
        /// Đăng nhập hệ thống
        /// </summary>
        public Account Login(string accountName, string password)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Mật khẩu không được để trống!");
            }

            // Validate định dạng tên tài khoản
            if (!IsValidAccountName(accountName))
            {
                throw new ArgumentException("Tên tài khoản không hợp lệ! Chỉ được chứa chữ cái, số và dấu gạch dưới (3-50 ký tự)");
            }

            try
            {
                // Gọi DA để xác thực
                DataTable dt = authDA.Login(accountName, password);

                if (dt == null || dt.Rows.Count == 0)
                {
                    throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng!");
                }

                // Chuyển đổi sang object Account
                DataRow row = dt.Rows[0];
                return new Account
                {
                    AccountName = row["AccountName"].ToString(),
                    FullName = row["FullName"].ToString(),
                    Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                    Phone = row["Phone"].ToString(),
                    DateCreated = Convert.ToDateTime(row["DateCreated"])
                };
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedAccessException || ex is ArgumentException)
                {
                    throw;
                }
                throw new Exception($"Lỗi khi đăng nhập: {ex.Message}");
            }
        }

        /// <summary>
        /// Đổi mật khẩu người dùng
        /// </summary>
        public bool ChangePassword(string accountName, string oldPassword, string newPassword)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(oldPassword))
            {
                throw new ArgumentException("Mật khẩu cũ không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("Mật khẩu mới không được để trống!");
            }

            if (newPassword.Length < 6)
            {
                throw new ArgumentException("Mật khẩu mới phải có ít nhất 6 ký tự!");
            }

            if (newPassword.Length > 50)
            {
                throw new ArgumentException("Mật khẩu mới không được quá 50 ký tự!");
            }

            if (oldPassword == newPassword)
            {
                throw new ArgumentException("Mật khẩu mới phải khác mật khẩu cũ!");
            }

            try
            {
                int result = authDA.ChangePassword(accountName, oldPassword, newPassword);
                return result > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi đổi mật khẩu: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết người dùng
        /// </summary>
        public Account GetUserInfo(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            try
            {
                DataTable dt = authDA.GetUserInfo(accountName);

                if (dt == null || dt.Rows.Count == 0)
                {
                    throw new Exception($"Không tìm thấy tài khoản '{accountName}'!");
                }

                DataRow row = dt.Rows[0];
                return new Account
                {
                    AccountName = row["AccountName"].ToString(),
                    FullName = row["FullName"].ToString(),
                    Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                    Phone = row["Phone"].ToString(),
                    DateCreated = Convert.ToDateTime(row["DateCreated"])
                };
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException)
                {
                    throw;
                }
                throw new Exception($"Lỗi khi lấy thông tin người dùng: {ex.Message}");
            }
        }

        /// <summary>
        /// Lấy danh sách vai trò của người dùng
        /// </summary>
        public DataTable GetUserRoles(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            return authDA.GetUserRoles(accountName);
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền truy cập vai trò cụ thể không
        /// </summary>
        public bool CheckUserPermission(string accountName, int roleID)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (roleID <= 0)
            {
                throw new ArgumentException("ID vai trò không hợp lệ!");
            }

            return authDA.CheckUserPermission(accountName, roleID);
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền theo tên vai trò không
        /// </summary>
        public bool CheckUserPermissionByName(string accountName, string roleName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Tên vai trò không được để trống!");
            }

            return authDA.CheckUserPermissionByName(accountName, roleName);
        }

        /// <summary>
        /// Kiểm tra người dùng có quyền truy cập đường dẫn không
        /// </summary>
        public bool CheckUserAccessByPath(string accountName, string path)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Đường dẫn không được để trống!");
            }

            return authDA.CheckUserAccessByPath(accountName, path);
        }

        /// <summary>
        /// Xác thực và lấy đầy đủ thông tin người dùng (bao gồm roles)
        /// </summary>
        public UserFullInfo ValidateAndGetUserInfo(string accountName, string password)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Mật khẩu không được để trống!");
            }

            DataTable dt = authDA.ValidateAndGetUserInfo(accountName, password);

            if (dt == null || dt.Rows.Count == 0)
            {
                throw new UnauthorizedAccessException("Tên đăng nhập hoặc mật khẩu không đúng!");
            }

            DataRow row = dt.Rows[0];
            return new UserFullInfo
            {
                AccountName = row["AccountName"].ToString(),
                FullName = row["FullName"].ToString(),
                Email = row["Email"] != DBNull.Value ? row["Email"].ToString() : null,
                Phone = row["Phone"].ToString(),
                DateCreated = Convert.ToDateTime(row["DateCreated"]),
                Roles = row["Roles"] != DBNull.Value ? row["Roles"].ToString() : ""
            };
        }

        /// <summary>
        /// Kiểm tra tài khoản có tồn tại không
        /// </summary>
        public bool CheckAccountExists(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                return false;
            }

            return authDA.CheckAccountExists(accountName);
        }

        /// <summary>
        /// Kiểm tra người dùng có phải là admin không
        /// </summary>
        public bool IsAdmin(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                return false;
            }

            try
            {
                // Kiểm tra quyền "Quản trị viên" (RoleID = 1 theo dữ liệu mẫu)
                return CheckUserPermission(accountName, 1);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra tên tài khoản có hợp lệ không
        /// </summary>
        public bool IsValidAccountName(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                return false;
            }

            // Chỉ chấp nhận chữ cái, số và dấu gạch dưới, độ dài 3-50 ký tự
            Regex regex = new Regex(@"^[a-zA-Z0-9_]{3,50}$");
            return regex.IsMatch(accountName);
        }

        /// <summary>
        /// Kiểm tra mật khẩu có đủ mạnh không
        /// </summary>
        public bool IsStrongPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            // Mật khẩu phải có ít nhất 6 ký tự và không quá 50 ký tự
            return password.Length >= 6 && password.Length <= 50;
        }

        /// <summary>
        /// Kiểm tra email có hợp lệ không
        /// </summary>
        public bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return true; // Email có thể null
            }

            try
            {
                // Regex kiểm tra email
                Regex regex = new Regex(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$");
                return regex.IsMatch(email);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra số điện thoại có hợp lệ không (VN)
        /// </summary>
        public bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                return false;
            }

            // Chấp nhận số điện thoại Việt Nam: 10-11 số
            Regex regex = new Regex(@"^[0-9]{10,11}$");
            return regex.IsMatch(phone);
        }

        /// <summary>
        /// Lấy message lỗi validation
        /// </summary>
        public string GetPasswordStrengthMessage(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return "Mật khẩu không được để trống";
            }

            if (password.Length < 6)
            {
                return "Mật khẩu phải có ít nhất 6 ký tự";
            }

            if (password.Length > 50)
            {
                return "Mật khẩu không được quá 50 ký tự";
            }

            // Có thể thêm các message khác
            return "Mật khẩu hợp lệ";
        }

        /// <summary>
        /// Validate thông tin đăng nhập trước khi submit
        /// </summary>
        public string ValidateLoginInfo(string accountName, string password)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                return "Tên tài khoản không được để trống!";
            }

            if (!IsValidAccountName(accountName))
            {
                return "Tên tài khoản không hợp lệ! Chỉ chứa chữ cái, số và dấu gạch dưới (3-50 ký tự)";
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                return "Mật khẩu không được để trống!";
            }

            return null; // Hợp lệ
        }

        /// <summary>
        /// Validate thông tin đổi mật khẩu
        /// </summary>
        public string ValidateChangePasswordInfo(string oldPassword, string newPassword, string confirmPassword)
        {
            if (string.IsNullOrWhiteSpace(oldPassword))
            {
                return "Mật khẩu cũ không được để trống!";
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                return "Mật khẩu mới không được để trống!";
            }

            if (newPassword.Length < 6)
            {
                return "Mật khẩu mới phải có ít nhất 6 ký tự!";
            }

            if (newPassword.Length > 50)
            {
                return "Mật khẩu mới không được quá 50 ký tự!";
            }

            if (oldPassword == newPassword)
            {
                return "Mật khẩu mới phải khác mật khẩu cũ!";
            }

            if (!string.IsNullOrWhiteSpace(confirmPassword) && newPassword != confirmPassword)
            {
                return "Mật khẩu xác nhận không khớp!";
            }

            return null; // Hợp lệ
        }
    }

    // Model class bổ sung
    public class UserFullInfo
    {
        public string AccountName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime DateCreated { get; set; }
        public string Roles { get; set; }
    }
}
