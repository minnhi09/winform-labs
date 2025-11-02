using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Lab_Advanced_Command
{
    public partial class RoleForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=Chuong_5_Lab_Advanced_Command; integrated security=true; TrustServerCertificate=True";
        private DataTable? roleTable;

        public RoleForm()
        {
            InitializeComponent();
        }

        private void RoleForm_Load(object sender, EventArgs e)
        {
            LoadRoles();
            ResetForm();
        }

        private void LoadRoles()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = @"
                    SELECT ID, RoleName, Description
                    FROM Roles
                    ORDER BY RoleName;";

                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                roleTable = new DataTable();
                sqlDataAdapter.Fill(roleTable);

                sqlConnection.Close();
                sqlCommand.Dispose();

                dgvRoles.AutoGenerateColumns = false;
                dgvRoles.DataSource = roleTable;

                lblTotalRoles.Text = $"Tổng số: {roleTable.Rows.Count} vai trò";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách vai trò: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResetForm()
        {
            txtRoleId.Clear();
            txtRoleName.Clear();
            txtDescription.Clear();

            btnAdd.Enabled = true;
            btnUpdate.Enabled = false;

            txtRoleName.Focus();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtRoleName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên vai trò!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRoleName.Focus();
                return false;
            }

            if (txtRoleName.Text.Length < 3)
            {
                MessageBox.Show("Tên vai trò phải có ít nhất 3 ký tự!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRoleName.Focus();
                return false;
            }

            return true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = @"
                    INSERT INTO Roles (RoleName, Description)
                    VALUES (@RoleName, @Description);
                    SELECT SCOPE_IDENTITY();";

                sqlCommand.Parameters.AddWithValue("@RoleName", txtRoleName.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Description", 
                    string.IsNullOrWhiteSpace(txtDescription.Text) ? DBNull.Value : txtDescription.Text.Trim());

                sqlConnection.Open();
                int newId = Convert.ToInt32(sqlCommand.ExecuteScalar());

                sqlConnection.Close();
                sqlCommand.Dispose();

                MessageBox.Show($"Thêm vai trò thành công!\nMã vai trò: {newId}\nTên vai trò: {txtRoleName.Text}",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadRoles();
                ResetForm();
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Message.Contains("UNIQUE KEY") || sqlEx.Message.Contains("duplicate"))
                {
                    MessageBox.Show("Tên vai trò đã tồn tại! Vui lòng chọn tên khác.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRoleName.Focus();
                    txtRoleName.SelectAll();
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

            if (string.IsNullOrEmpty(txtRoleId.Text))
            {
                MessageBox.Show("Vui lòng chọn vai trò cần cập nhật!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = @"
                    UPDATE Roles
                    SET RoleName = @RoleName,
                        Description = @Description
                    WHERE ID = @ID;";

                sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(txtRoleId.Text));
                sqlCommand.Parameters.AddWithValue("@RoleName", txtRoleName.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Description",
                    string.IsNullOrWhiteSpace(txtDescription.Text) ? DBNull.Value : txtDescription.Text.Trim());

                sqlConnection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                sqlConnection.Close();
                sqlCommand.Dispose();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật vai trò thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadRoles();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Cập nhật vai trò thất bại!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Message.Contains("UNIQUE KEY") || sqlEx.Message.Contains("duplicate"))
                {
                    MessageBox.Show("Tên vai trò đã tồn tại! Vui lòng chọn tên khác.",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtRoleName.Focus();
                    txtRoleName.SelectAll();
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRoleId.Text))
            {
                MessageBox.Show("Vui lòng chọn vai trò cần xóa!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa vai trò '{txtRoleName.Text}' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();

                    sqlCommand.CommandText = "DELETE FROM Roles WHERE ID = @ID;";
                    sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(txtRoleId.Text));

                    sqlConnection.Open();
                    int rowsAffected = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                    sqlCommand.Dispose();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Xóa vai trò thành công!",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadRoles();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("Xóa vai trò thất bại!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException sqlEx)
                {
                    if (sqlEx.Message.Contains("REFERENCE constraint"))
                    {
                        MessageBox.Show("Không thể xóa vai trò này vì đang được sử dụng bởi các tài khoản!",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

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
                MessageBox.Show($"Không tìm thấy cột: {columnName}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "";
        }

        private void dgvRoles_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvRoles.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow selectedRow = dgvRoles.SelectedRows[0];

                    txtRoleId.Text = GetCellValue(selectedRow, "colID");
                    txtRoleName.Text = GetCellValue(selectedRow, "colRoleName");
                    txtDescription.Text = GetCellValue(selectedRow, "colDescription");

                    btnAdd.Enabled = false;
                    btnUpdate.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi hiển thị thông tin vai trò: {ex.Message}",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetForm();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (roleTable == null) return;

            string searchText = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                dgvRoles.DataSource = roleTable;
            }
            else
            {
                DataView dataView = roleTable.DefaultView;
                dataView.RowFilter = $"RoleName LIKE '%{searchText}%' OR Description LIKE '%{searchText}%'";
                dgvRoles.DataSource = dataView;
            }

            lblTotalRoles.Text = $"Tổng số: {dgvRoles.Rows.Count} vai trò";
        }
    }
}
