using System;
using System.Data;
using System.Windows.Forms;
using Chuong_6.BusinessLogic;
using Chuong_6.DataAccess;

namespace Chuong_6.RestaurantManagement
{
    public partial class frmLogin : Form
    {
        private frmMain? mainForm;
        private readonly AuthBL authBL;

        public frmLogin()
        {
            InitializeComponent();
            this.authBL = new AuthBL();
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

            // Validate using AuthBL
            string validationError = authBL.ValidateLoginInfo(username, password);
            if (!string.IsNullOrEmpty(validationError))
            {
                MessageBox.Show(validationError, "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (string.IsNullOrWhiteSpace(username))
                {
                    txtUsername.Focus();
                }
                else
                {
                    txtPassword.Focus();
                }
                return;
            }

            try
            {
                // Authenticate using AuthBL
                Account account = authBL.Login(username, password);

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
            catch (UnauthorizedAccessException ex)
            {
                // Authentication failed (wrong credentials)
                MessageBox.Show(ex.Message, "Lỗi đăng nhập",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Clear password and focus back to username
                txtPassword.Clear();
                txtUsername.Focus();
                txtUsername.SelectAll();
            }
            catch (Exception ex)
            {
                // Other errors
                MessageBox.Show($"Lỗi không xác định: {ex.Message}", "Lỗi",
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
                AuthBL authBL = new AuthBL();
                return authBL.GetUserRoles(username);
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
                AuthBL authBL = new AuthBL();
                return authBL.CheckUserPermission(username, roleId);
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
                AuthBL authBL = new AuthBL();
                return authBL.IsAdmin(username);
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
                AuthBL authBL = new AuthBL();
                return authBL.GetUserInfo(username);
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
