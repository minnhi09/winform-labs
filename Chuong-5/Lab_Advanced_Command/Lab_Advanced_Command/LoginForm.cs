using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Lab_Advanced_Command
{
    public partial class LoginForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=Chuong_5_Lab_Advanced_Command; integrated security=true; TrustServerCertificate=True";
        
        // Properties to store logged-in user information
        public int LoggedInAccountID { get; private set; }
        public string LoggedInUsername { get; private set; } = string.Empty;
        public string LoggedInFullName { get; private set; } = string.Empty;

        public LoginForm()
        {
            InitializeComponent();

            txtUsername.Text = "admin";
            txtPassword.Text = "123456";
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Set focus to username textbox
            txtUsername.Focus();
            
            // Set password char
            txtPassword.UseSystemPasswordChar = true;
            
            // Set Enter key for login
            this.AcceptButton = btnLogin;
            this.CancelButton = btnCancel;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Perform login
            try
            {
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "CheckLogin";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Password", txtPassword.Text);

                sqlConnection.Open();
                
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Login successful
                        LoggedInAccountID = Convert.ToInt32(reader["ID"]);
                        LoggedInUsername = reader["Username"]?.ToString() ?? string.Empty;
                        LoggedInFullName = reader["FullName"]?.ToString() ?? string.Empty;
                        bool isActive = Convert.ToBoolean(reader["IsActive"]);

                        sqlConnection.Close();
                        sqlCommand.Dispose();

                        // Set dialog result to OK
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        // This shouldn't happen as the procedure will throw an error
                        // But we keep it as a safety check
                        MessageBox.Show("Đăng nhập thất bại!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        txtPassword.Clear();
                        txtPassword.Focus();
                    }
                }

                sqlConnection.Close();
                sqlCommand.Dispose();
            }
            catch (SqlException sqlEx)
            {
                // Handle errors from stored procedure
                string errorMessage = sqlEx.Message;
                
                if (errorMessage.Contains("không đúng") || errorMessage.Contains("không được để trống"))
                {
                    MessageBox.Show(errorMessage,
                        "Đăng nhập thất bại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (errorMessage.Contains("bị khóa"))
                {
                    MessageBox.Show(errorMessage,
                        "Tài khoản bị khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    MessageBox.Show($"Lỗi cơ sở dữ liệu: {errorMessage}",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
                txtPassword.Clear();
                txtPassword.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát ứng dụng?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = !chkShowPassword.Checked;
        }

        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Prevent spaces in username
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
            }
        }
    }
}
