using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Lab_Advanced_Command
{
    public partial class AccountForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=Chuong_5_Lab_Advanced_Command; integrated security=true; TrustServerCertificate=True";
        private DataTable? accountTable;
        private bool isEditMode = false;

        public AccountForm()
        {
            InitializeComponent();
        }

        private void AccountForm_Load(object sender, EventArgs e)
        {
            LoadAccounts();
            ResetForm();
        }

        private void LoadAccounts()
        {
            try
            {
                // 1. Cấu hình kết nối
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // 2. Sử dụng stored procedure
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "GetAllAccounts";

                // 3. Thực hiện kết nối
                sqlConnection.Open();

                // 4. Thực thi stored procedure và đổ dữ liệu vào DataTable
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                accountTable = new DataTable();
                sqlDataAdapter.Fill(accountTable);

                // 5. Đóng kết nối
                sqlConnection.Close();
                sqlCommand.Dispose();

                // 6. Đảm bảo DataGridView không tự động tạo cột
                dgvAccounts.AutoGenerateColumns = false;
                
                // 7. Gán dữ liệu vào DataGridView
                dgvAccounts.DataSource = accountTable;

                // 8. Hiển thị số lượng tài khoản
                lblTotalAccounts.Text = $"Tổng số: {accountTable.Rows.Count} tài khoản";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách tài khoản: {ex.Message}", 
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            txtAccountId.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            txtFullName.Clear();
            chkIsActive.Checked = true;
            dtpCreatedDate.Value = DateTime.Now;

            txtUsername.Enabled = true;
            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;
            isEditMode = false;

            txtUsername.Focus();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (txtUsername.Text.Length < 3)
            {
                MessageBox.Show("Tên đăng nhập phải có ít nhất 3 ký tự!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (txtPassword.Text.Length < 6)
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 6 ký tự!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập họ và tên!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                // 1. Cấu hình kết nối
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // 2. Sử dụng stored procedure
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "InsertAccount";

                // 3. Thêm parameters
                SqlParameter idParameter = new SqlParameter("@ID", SqlDbType.Int);
                idParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(idParameter);

                sqlCommand.Parameters.AddWithValue("@Username", txtUsername.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Password", txtPassword.Text);
                sqlCommand.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);
                sqlCommand.Parameters.AddWithValue("@CreatedDate", dtpCreatedDate.Value);

                // 4. Thực hiện kết nối và thực thi
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

                // 5. Lấy ID mới được tạo
                int newId = Convert.ToInt32(idParameter.Value);

                // 6. Đóng kết nối
                sqlConnection.Close();
                sqlCommand.Dispose();

                // 7. Thông báo kết quả
                MessageBox.Show($"Thêm tài khoản thành công!\nMã tài khoản: {newId}\nTên đăng nhập: {txtUsername.Text}",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 8. Reload danh sách và reset form
                LoadAccounts();
                ResetForm();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Message.Contains("Tên đăng nhập đã tồn tại") || sqlEx.Message.Contains("UNIQUE KEY"))
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại! Vui lòng chọn tên khác.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsername.Focus();
                    txtUsername.SelectAll();
                }
                else
                {
                    MessageBox.Show($"Lỗi cơ sở dữ liệu: {sqlEx.Message}",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            if (string.IsNullOrEmpty(txtAccountId.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần cập nhật!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 1. Cấu hình kết nối
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // 2. Sử dụng stored procedure
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "UpdateAccount";

                // 3. Thêm parameters
                sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(txtAccountId.Text));
                sqlCommand.Parameters.AddWithValue("@Password", txtPassword.Text);
                sqlCommand.Parameters.AddWithValue("@FullName", txtFullName.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@IsActive", chkIsActive.Checked);

                // 4. Thực hiện kết nối và thực thi
                sqlConnection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                // 5. Đóng kết nối
                sqlConnection.Close();
                sqlCommand.Dispose();

                // 6. Thông báo kết quả
                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật tài khoản thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 7. Reload danh sách và reset form
                    LoadAccounts();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Cập nhật tài khoản thất bại!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {sqlEx.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtAccountId.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tài khoản '{txtUsername.Text}' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    // 1. Cấu hình kết nối
                    SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();

                    // 2. Gọi stored procedure DeleteAccount
                    sqlCommand.CommandText = "DeleteAccount";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(txtAccountId.Text));

                    // 3. Thực hiện kết nối và thực thi
                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    // 4. Đóng kết nối
                    sqlConnection.Close();
                    sqlCommand.Dispose();

                    // 5. Thông báo kết quả
                    MessageBox.Show("Xóa tài khoản thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 6. Reload danh sách và reset form
                    LoadAccounts();
                    ResetForm();
                }
                catch (SqlException sqlEx)
                {
                    // Xử lý lỗi từ stored procedure
                    if (sqlEx.Message.Contains("không thể xóa") || sqlEx.Message.Contains("đã có"))
                    {
                        MessageBox.Show(sqlEx.Message, "Không thể xóa",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi SQL: {sqlEx.Message}", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnManageRoles_Click(object sender, EventArgs e)
        {
            RoleForm roleForm = new RoleForm();
            roleForm.ShowDialog(this);
        }

        /// <summary>
        /// Lấy giá trị cell an toàn, tránh lỗi khi cell không tồn tại hoặc giá trị null
        /// </summary>
        private string GetCellValue(DataGridViewRow row, string columnName)
        {
            try
            {
                if (row.Cells[columnName].Value != null)
                {
                    return row.Cells[columnName].Value.ToString() ?? "";
                }
            }
            catch (ArgumentException)
            {
                // Cột không tồn tại
                MessageBox.Show($"Không tìm thấy cột: {columnName}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "";
        }

        private void dgvAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow selectedRow = dgvAccounts.SelectedRows[0];

                    // Sử dụng helper method để truy cập cell an toàn
                    txtAccountId.Text = GetCellValue(selectedRow, "colID");
                    txtUsername.Text = GetCellValue(selectedRow, "colUsername");
                    txtPassword.Text = GetCellValue(selectedRow, "colPassword");
                    txtFullName.Text = GetCellValue(selectedRow, "colFullName");
                    
                    // Xử lý IsActive
                    if (selectedRow.Cells["colIsActive"].Value != null)
                    {
                        chkIsActive.Checked = Convert.ToBoolean(selectedRow.Cells["colIsActive"].Value);
                    }
                    else
                    {
                        chkIsActive.Checked = true;
                    }
                    
                    // Xử lý CreatedDate
                    if (selectedRow.Cells["colCreatedDate"].Value != null)
                    {
                        dtpCreatedDate.Value = Convert.ToDateTime(selectedRow.Cells["colCreatedDate"].Value);
                    }
                    else
                    {
                        dtpCreatedDate.Value = DateTime.Now;
                    }

                    // Chuyển sang chế độ edit
                    txtUsername.Enabled = false;
                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;
                    isEditMode = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi hiển thị thông tin tài khoản: {ex.Message}\n\nChi tiết: {ex.StackTrace}", 
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetForm();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (accountTable == null) return;

            string searchText = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                dgvAccounts.DataSource = accountTable;
            }
            else
            {
                DataView dataView = accountTable.DefaultView;
                dataView.RowFilter = $"Username LIKE '%{searchText}%' OR FullName LIKE '%{searchText}%'";
                dgvAccounts.DataSource = dataView;
            }

            lblTotalAccounts.Text = $"Tổng số: {dgvAccounts.Rows.Count} tài khoản";
        }
    }
}
