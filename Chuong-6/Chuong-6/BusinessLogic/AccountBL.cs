using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Chuong_6.DataAccess;

namespace Chuong_6.BusinessLogic
{
    public class AccountBL
    {
        private readonly AccountDA accountDA;

        public AccountBL()
        {
            this.accountDA = new AccountDA();
        }

        // Lấy tất cả tài khoản
        public List<Account> GetAllAccounts()
        {
            return accountDA.GetAllAccounts();
        }

        // Lấy tài khoản theo tên
        public Account GetAccountByName(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            return accountDA.GetAccountByName(accountName);
        }

        // Thêm tài khoản mới
        public string AddAccount(string accountName, string password, string fullName,
                                 string email, string phone)
        {
            // Validate dữ liệu
            ValidateAccountData(accountName, password, fullName, phone);
            ValidateEmail(email);

            // Kiểm tra tài khoản đã tồn tại
            if (accountDA.CheckAccountExists(accountName))
            {
                throw new Exception("Tên tài khoản đã tồn tại!");
            }

            // Mã hóa mật khẩu (trong thực tế nên dùng hash)
            string hashedPassword = HashPassword(password);

            return accountDA.InsertAccount(accountName, hashedPassword, fullName, email, phone);
        }

        // Cập nhật tài khoản
        public int UpdateAccount(string accountName, string fullName, string email, string phone)
        {
            // Validate dữ liệu
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Họ tên không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Số điện thoại không được để trống!");
            }

            ValidateEmail(email);
            ValidatePhone(phone);

            // Kiểm tra tài khoản có tồn tại
            if (!accountDA.CheckAccountExists(accountName))
            {
                throw new Exception("Tài khoản không tồn tại!");
            }

            return accountDA.UpdateAccount(accountName, fullName, email, phone);
        }

        // Xóa tài khoản
        public int DeleteAccount(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            // Không cho phép xóa tài khoản admin
            if (accountName.ToLower() == "admin")
            {
                throw new Exception("Không thể xóa tài khoản admin!");
            }

            return accountDA.DeleteAccount(accountName);
        }

        // Lấy danh sách vai trò của tài khoản
        public List<RoleAccount> GetAccountRoles(string accountName)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            return accountDA.GetAccountRoles(accountName);
        }

        // Reset mật khẩu
        public int ResetPassword(string accountName, string newPassword)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(newPassword))
            {
                throw new ArgumentException("Mật khẩu mới không được để trống!");
            }

            if (newPassword.Length < 6)
            {
                throw new ArgumentException("Mật khẩu phải có ít nhất 6 ký tự!");
            }

            // Mã hóa mật khẩu
            string hashedPassword = HashPassword(newPassword);

            return accountDA.ResetPassword(accountName, hashedPassword);
        }

        // Kiểm tra tài khoản tồn tại
        public bool CheckAccountExists(string accountName)
        {
            return accountDA.CheckAccountExists(accountName);
        }

        // Lấy thông tin tài khoản dưới dạng object
        public Account GetAccountObject(string accountName)
        {
            return accountDA.GetAccountObject(accountName);
        }

        // Validate dữ liệu tài khoản
        private void ValidateAccountData(string accountName, string password,
                                         string fullName, string phone)
        {
            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tên tài khoản không được để trống!");
            }

            if (accountName.Length < 3)
            {
                throw new ArgumentException("Tên tài khoản phải có ít nhất 3 ký tự!");
            }

            if (!Regex.IsMatch(accountName, @"^[a-zA-Z0-9_]+$"))
            {
                throw new ArgumentException("Tên tài khoản chỉ được chứa chữ cái, số và dấu gạch dưới!");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Mật khẩu không được để trống!");
            }

            if (password.Length < 6)
            {
                throw new ArgumentException("Mật khẩu phải có ít nhất 6 ký tự!");
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Họ tên không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Số điện thoại không được để trống!");
            }

            ValidatePhone(phone);
        }

        // Validate email
        private void ValidateEmail(string email)
        {
            if (!string.IsNullOrWhiteSpace(email))
            {
                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (!Regex.IsMatch(email, emailPattern))
                {
                    throw new ArgumentException("Email không hợp lệ!");
                }
            }
        }

        // Validate số điện thoại
        private void ValidatePhone(string phone)
        {
            if (!Regex.IsMatch(phone, @"^[0-9]{10,11}$"))
            {
                throw new ArgumentException("Số điện thoại phải có 10-11 chữ số!");
            }
        }

        // Mã hóa mật khẩu (đơn giản, trong thực tế nên dùng BCrypt hoặc SHA256)
        private string HashPassword(string password)
        {
            // Trong thực tế, nên sử dụng thuật toán mã hóa mạnh hơn
            // Ví dụ: BCrypt, SHA256, PBKDF2
            // Ở đây đang để đơn giản để test
            return password;
        }

        // Tìm kiếm tài khoản
        public List<Account> SearchAccounts(string keyword)
        {
            List<Account> accounts = GetAllAccounts();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                return accounts;
            }

            string keywordLower = keyword.ToLower();

            return accounts.Where(a =>
                a.AccountName.ToLower().Contains(keywordLower) ||
                a.FullName.ToLower().Contains(keywordLower) ||
                (a.Email != null && a.Email.ToLower().Contains(keywordLower)) ||
                a.Phone.Contains(keyword)
            ).ToList();
        }

        // Kiểm tra quyền của tài khoản
        public bool HasRole(string accountName, string roleName)
        {
            List<RoleAccount> roles = GetAccountRoles(accountName);
            return roles.Any(r =>
                r.RoleName.Equals(roleName, StringComparison.OrdinalIgnoreCase) &&
                r.Actived
            );
        }

        // Đếm số lượng tài khoản
        public int GetTotalAccounts()
        {
            List<Account> accounts = GetAllAccounts();
            return accounts.Count;
        }

        // Lấy tài khoản mới nhất
        public List<Account> GetRecentAccounts(int top = 10)
        {
            List<Account> accounts = GetAllAccounts();
            return accounts.OrderByDescending(a => a.DateCreated).Take(top).ToList();
        }
    }
}
