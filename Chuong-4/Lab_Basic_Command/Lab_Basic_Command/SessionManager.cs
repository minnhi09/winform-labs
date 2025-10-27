using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Basic_Command
{
    /// <summary>
    /// Class tĩnh để lưu thông tin phiên đăng nhập của người dùng
    /// </summary>
    public static class SessionManager
    {
        public static int AccountID { get; set; }
        public static string Username { get; set; }
        public static string DisplayName { get; set; }
        public static bool IsLoggedIn { get; set; }

        /// <summary>
        /// Đăng nhập và lưu thông tin người dùng
        /// </summary>
        public static void Login(int accountID, string username, string displayName)
        {
            AccountID = accountID;
            Username = username;
            DisplayName = displayName;
            IsLoggedIn = true;
        }

        /// <summary>
        /// Đăng xuất và xóa thông tin người dùng
        /// </summary>
        public static void Logout()
        {
            AccountID = 0;
            Username = string.Empty;
            DisplayName = string.Empty;
            IsLoggedIn = false;
        }

        /// <summary>
        /// Lấy thông tin hiển thị người dùng
        /// </summary>
        public static string GetUserInfo()
        {
            if (IsLoggedIn)
            {
                return $"{DisplayName} ({Username})";
            }
            return "Chưa đăng nhập";
        }
    }
}
