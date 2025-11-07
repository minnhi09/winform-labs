using System;
using System.Data;
using System.Windows.Forms;
using Chuong_6.BusinessLogic;
using Chuong_6.DataAccess;

namespace Chuong_6.RestaurantManagement
{
    public partial class frmAccount : Form
    {
        private readonly AuthBL authBL;
        private string currentUsername = string.Empty;

        public frmAccount()
        {
            InitializeComponent();
            this.authBL = new AuthBL();
        }

        public frmAccount(string username) : this()
        {
            this.currentUsername = username;
        }

        /// <summary>
        /// Đổi mật khẩu người dùng
        /// </summary>
        public bool ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            try
            {
                // Validate using AuthBL
                string validationError = authBL.ValidateChangePasswordInfo(oldPassword, newPassword, confirmPassword);
                if (!string.IsNullOrEmpty(validationError))
                {
                    MessageBox.Show(validationError, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Change password using AuthBL
                bool result = authBL.ChangePassword(currentUsername, oldPassword, newPassword);

                if (result)
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

                return false;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show(ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Load thông tin tài khoản hiện tại
        /// </summary>
        public void LoadAccountInfo()
        {
            try
            {
                Account account = authBL.GetUserInfo(currentUsername);

                if (account != null)
                {
                    MessageBox.Show($"Thông tin tài khoản:\n" +
                        $"Tài khoản: {account.AccountName}\n" +
                        $"Họ tên: {account.FullName}\n" +
                        $"Email: {account.Email ?? "Chưa có"}\n" +
                        $"Điện thoại: {account.Phone}\n" +
                        $"Ngày tạo: {account.DateCreated:dd/MM/yyyy}",
                        "Thông tin tài khoản",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải thông tin: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Hiển thị danh sách vai trò của người dùng
        /// </summary>
        public void LoadUserRoles()
        {
            try
            {
                DataTable roles = authBL.GetUserRoles(currentUsername);

                if (roles.Rows.Count == 0)
                {
                    MessageBox.Show("Người dùng chưa được gán vai trò nào!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string rolesText = "Danh sách vai trò:\n\n";
                foreach (DataRow row in roles.Rows)
                {
                    rolesText += $"- {row["RoleName"]}\n";
                    if (row["Notes"] != DBNull.Value && !string.IsNullOrEmpty(row["Notes"].ToString()))
                    {
                        rolesText += $"  ({row["Notes"]})\n";
                    }
                }

                MessageBox.Show(rolesText, "Vai trò của bạn",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải vai trò: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Kiểm tra quyền của người dùng
        /// </summary>
        public bool CheckPermission(int roleId)
        {
            try
            {
                return authBL.CheckUserPermission(currentUsername, roleId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra quyền: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra quyền theo tên vai trò
        /// </summary>
        public bool CheckPermissionByName(string roleName)
        {
            try
            {
                return authBL.CheckUserPermissionByName(currentUsername, roleName);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra quyền: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra có phải admin không
        /// </summary>
        public bool IsAdmin()
        {
            return authBL.IsAdmin(currentUsername);
        }

        /// <summary>
        /// Mở form quản lý tài khoản (chỉ dành cho admin)
        /// </summary>
        public void OpenAccountManagement()
        {
            if (!IsAdmin())
            {
                MessageBox.Show("Bạn không có quyền truy cập chức năng này!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            frmAccountManagement frm = new frmAccountManagement(currentUsername);
            frm.ShowDialog();
        }
    }
}

