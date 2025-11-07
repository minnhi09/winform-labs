using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using entity_framework.models;

namespace entity_framework
{
    public partial class frmLogin : Form
    {
        private frmMain? mainForm;

        public frmLogin()
        {
            InitializeComponent();
        }

        public frmLogin(frmMain main) : this()
        {
            this.mainForm = main;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Set focus to username textbox
            txtUsername.Focus();

            // Set default values for testing (optional - can be removed in production)
            txtUsername.Text = "admin";
            txtPassword.Text = "123456";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            PerformLogin();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle password visibility
            if (chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0'; // Show password
            }
            else
            {
                txtPassword.PasswordChar = '●'; // Hide password
            }
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow Enter key to submit login
            if (e.KeyChar == (char)Keys.Enter)
            {
                e.Handled = true;
                PerformLogin();
            }
        }

        private void PerformLogin()
        {
            // Validate input
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            // Basic validation
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            try
            {
                using (var context = new RestaurantContext())
                {
                    // Find account by username
                    var account = context.Accounts
                        .FirstOrDefault(a => a.AccountName == username);

                    if (account == null)
                    {
                        MessageBox.Show("Tên đăng nhập không tồn tại!", "Lỗi đăng nhập",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Clear();
                        txtUsername.Focus();
                        txtUsername.SelectAll();
                        return;
                    }

                    // Verify password (plain text comparison for now)
                    if (account.Password != password)
                    {
                        MessageBox.Show("Mật khẩu không đúng!", "Lỗi đăng nhập",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPassword.Clear();
                        txtPassword.Focus();
                        return;
                    }

                    // Login successful
                    MessageBox.Show($"Chào mừng {account.FullName}!", "Đăng nhập thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Update main form with logged in user
                    if (mainForm != null)
                    {
                        mainForm.SetLoggedInUser(account);
                    }

                    // Close login form
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kết nối database: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Lấy danh sách vai trò của người dùng
        /// </summary>
        public static DataTable GetUserRoles(string username)
        {
            try
            {
                using (var context = new RestaurantContext())
                {
                    var roles = context.RoleAccounts
                        .Where(ra => ra.AccountName == username && ra.Actived)
                        .Select(ra => new
                        {
                            ra.RoleID,
                            ra.Role.RoleName,
                            ra.Role.Path,
                            ra.Actived,
                            ra.Notes
                        })
                        .ToList();

                    // Convert to DataTable
                    DataTable dt = new DataTable();
                    dt.Columns.Add("RoleID", typeof(int));
                    dt.Columns.Add("RoleName", typeof(string));
                    dt.Columns.Add("Path", typeof(string));
                    dt.Columns.Add("Actived", typeof(bool));
                    dt.Columns.Add("Notes", typeof(string));

                    foreach (var role in roles)
                    {
                        dt.Rows.Add(role.RoleID, role.RoleName, role.Path, role.Actived, role.Notes);
                    }

                    return dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lấy thông tin vai trò: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new DataTable();
            }
        }

        /// <summary>
        /// Kiểm tra quyền truy cập của người dùng
        /// </summary>
        public static bool CheckUserPermission(string username, int roleId)
        {
            try
            {
                using (var context = new RestaurantContext())
                {
                    return context.RoleAccounts
                        .Any(ra => ra.AccountName == username && ra.RoleID == roleId && ra.Actived);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra quyền: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Kiểm tra người dùng có phải là admin không
        /// </summary>
        public static bool IsAdmin(string username)
        {
            try
            {
                using (var context = new RestaurantContext())
                {
                    // Kiểm tra xem user có role "Admin" hoặc role có tên chứa "admin" không
                    return context.RoleAccounts
                        .Any(ra => ra.AccountName == username &&
                                   ra.Actived &&
                                   ra.Role.RoleName.ToLower().Contains("admin"));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi kiểm tra quyền admin: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Lấy thông tin chi tiết người dùng
        /// </summary>
        public static Account? GetUserInfo(string username)
        {
            try
            {
                using (var context = new RestaurantContext())
                {
                    return context.Accounts
                        .FirstOrDefault(a => a.AccountName == username);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi lấy thông tin người dùng: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
