using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chuong_6.DataAccess
{
    // Class cho RoleAccount (bảng RoleAccount - quan hệ giữa Role và Account)
    public class RoleAccount
    {
        public string AccountName { get; set; }
        public int RoleID { get; set; }
        public bool Actived { get; set; }
        public string Notes { get; set; }

        // Thông tin bổ sung từ JOIN với bảng Role
        public string RoleName { get; set; }
        public string Path { get; set; }
        public string RoleNotes { get; set; }
    }

    // Class cho AccountInfo trong Role (dùng trong RoleDA để lấy thông tin Account)
    public class RoleAccountInfo
    {
        public string AccountName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
        public bool Actived { get; set; }
        public string Notes { get; set; }
    }

    // Class cho thống kê Role
    public class RoleStatistic
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public int TotalAccounts { get; set; }
        public int ActiveAccounts { get; set; }
    }

    // Class cho kết quả gán Role
    public class RoleAssignResult
    {
        public int Result { get; set; }
        public string Message { get; set; }
    }
}
