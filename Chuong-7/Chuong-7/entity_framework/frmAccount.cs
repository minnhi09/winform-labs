using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using entity_framework.models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework
{
    public partial class frmAccount : Form
    {
        private RestaurantContext _context;
        private string currentUsername = string.Empty;

        public frmAccount()
        {
            InitializeComponent();
            _context = new RestaurantContext();
        }

        public frmAccount(string username) : this()
        {
            this.currentUsername = username;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
        }

        private void frmAccount_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        /// <summary>
        /// Load danh sách tài khoản vào ListView
        /// </summary>
        private void LoadAccounts()
        {
            try
            {
                listViewAccounts.Items.Clear();

                var accounts = _context.Accounts
                    .OrderBy(a => a.AccountName)
                    .ToList();

                foreach (var account in accounts)
                {
                    ListViewItem item = new ListViewItem(account.AccountName);
                    item.SubItems.Add(account.FullName);
                    item.SubItems.Add(account.Email ?? "");
                    item.SubItems.Add(account.Phone ?? "");
                    item.SubItems.Add(account.DateCreated.ToString("dd/MM/yyyy"));
                    item.Tag = account;

                    listViewAccounts.Items.Add(item);
                }

                lblTitle.Text = $"Danh sách tài khoản ({accounts.Count})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách tài khoản: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (listViewAccounts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedAccount = (Account)listViewAccounts.SelectedItems[0].Tag;

            // Tạo form nhập mật khẩu
            Form frmPassword = new Form
            {
                Text = "Đổi mật khẩu",
                Width = 400,
                Height = 250,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblOld = new Label { Left = 20, Top = 20, Width = 120, Text = "Mật khẩu cũ:" };
            TextBox txtOldPassword = new TextBox { Left = 150, Top = 20, Width = 200, UseSystemPasswordChar = true };

            Label lblNew = new Label { Left = 20, Top = 60, Width = 120, Text = "Mật khẩu mới:" };
            TextBox txtNewPassword = new TextBox { Left = 150, Top = 60, Width = 200, UseSystemPasswordChar = true };

            Label lblConfirm = new Label { Left = 20, Top = 100, Width = 120, Text = "Xác nhận mật khẩu:" };
            TextBox txtConfirmPassword = new TextBox { Left = 150, Top = 100, Width = 200, UseSystemPasswordChar = true };

            Button btnOK = new Button { Text = "Đồng ý", Left = 150, Width = 80, Top = 150, DialogResult = DialogResult.OK };
            Button btnCancel = new Button { Text = "Hủy", Left = 250, Width = 80, Top = 150, DialogResult = DialogResult.Cancel };

            frmPassword.Controls.Add(lblOld);
            frmPassword.Controls.Add(txtOldPassword);
            frmPassword.Controls.Add(lblNew);
            frmPassword.Controls.Add(txtNewPassword);
            frmPassword.Controls.Add(lblConfirm);
            frmPassword.Controls.Add(txtConfirmPassword);
            frmPassword.Controls.Add(btnOK);
            frmPassword.Controls.Add(btnCancel);

            frmPassword.AcceptButton = btnOK;
            frmPassword.CancelButton = btnCancel;

            if (frmPassword.ShowDialog() == DialogResult.OK)
            {
                currentUsername = selectedAccount.AccountName;
                ChangePassword(txtOldPassword.Text, txtNewPassword.Text, txtConfirmPassword.Text);
            }
        }

        private void btnViewRoles_Click(object sender, EventArgs e)
        {
            if (listViewAccounts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedAccount = (Account)listViewAccounts.SelectedItems[0].Tag;
            currentUsername = selectedAccount.AccountName;
            LoadUserRoles();
        }

        /// <summary>
        /// Đổi mật khẩu người dùng
        /// </summary>
        public bool ChangePassword(string oldPassword, string newPassword, string confirmPassword)
        {
            try
            {
                // Validate inputs
                if (string.IsNullOrWhiteSpace(oldPassword))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu cũ!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (string.IsNullOrWhiteSpace(newPassword))
                {
                    MessageBox.Show("Vui lòng nhập mật khẩu mới!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (newPassword != confirmPassword)
                {
                    MessageBox.Show("Mật khẩu xác nhận không khớp!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                if (newPassword.Length < 6)
                {
                    MessageBox.Show("Mật khẩu mới phải có ít nhất 6 ký tự!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }

                // Get account and verify old password
                var account = _context.Accounts.Find(currentUsername);
                if (account == null)
                {
                    MessageBox.Show("Không tìm thấy tài khoản!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                if (account.Password != oldPassword)
                {
                    MessageBox.Show("Mật khẩu cũ không đúng!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Change password
                account.Password = newPassword;
                int result = _context.SaveChanges();

                if (result > 0)
                {
                    MessageBox.Show("Đổi mật khẩu thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return true;
                }

                return false;
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Lỗi cập nhật database: " + ex.InnerException?.Message, "Lỗi",
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
                var account = _context.Accounts.Find(currentUsername);

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
                var roles = _context.RoleAccounts
                    .Include(ra => ra.Role)
                    .Where(ra => ra.AccountName == currentUsername && ra.Actived)
                    .ToList();

                if (roles.Count == 0)
                {
                    MessageBox.Show("Người dùng chưa được gán vai trò nào!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string rolesText = "Danh sách vai trò:\n\n";
                foreach (var roleAccount in roles)
                {
                    rolesText += $"- {roleAccount.Role.RoleName}\n";
                    if (!string.IsNullOrEmpty(roleAccount.Role.Notes))
                    {
                        rolesText += $"  ({roleAccount.Role.Notes})\n";
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
                return _context.RoleAccounts
                    .Any(ra => ra.AccountName == currentUsername && ra.RoleID == roleId && ra.Actived);
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
                return _context.RoleAccounts
                    .Include(ra => ra.Role)
                    .Any(ra => ra.AccountName == currentUsername &&
                               ra.Role.RoleName == roleName &&
                               ra.Actived);
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
            return currentUsername.ToLower() == "admin" || CheckPermissionByName("Admin");
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

