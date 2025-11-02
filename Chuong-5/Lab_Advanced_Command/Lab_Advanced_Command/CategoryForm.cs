using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Lab_Advanced_Command
{
    public partial class CategoryForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=Chuong_5_Lab_Advanced_Command; integrated security=true; TrustServerCertificate=True";

        public CategoryForm()
        {
            InitializeComponent();
        }

        private void CategoryForm_Load(object sender, EventArgs e)
        {
            // Focus vào textbox tên nhóm món ăn khi form load
            txtCategoryName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(txtCategoryName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhóm món ăn!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategoryName.Focus();
                return;
            }

            try
            {
                // 1. Cấu hình kết nối
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // 2. Cấu hình stored procedure
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "InsertCategory";

                // 3. Thêm parameters
                SqlParameter idParameter = new SqlParameter("@ID", SqlDbType.Int);
                idParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(idParameter);

                sqlCommand.Parameters.AddWithValue("@Name", txtCategoryName.Text.Trim());
                sqlCommand.Parameters.AddWithValue("@Notes", txtNotes.Text.Trim());

                // 4. Thực hiện kết nối và thực thi
                sqlConnection.Open();
                
                try
                {
                    int result = sqlCommand.ExecuteNonQuery();
                    
                    // 5. Lấy ID mới được tạo
                    int newCategoryId = Convert.ToInt32(idParameter.Value);

                    // 6. Đóng kết nối
                    sqlConnection.Close();
                    sqlCommand.Dispose();

                    // 7. Thông báo kết quả
                    MessageBox.Show($"Thêm nhóm món ăn thành công!\nMã nhóm: {newCategoryId}\nTên nhóm: {txtCategoryName.Text}",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Đóng form và trả về DialogResult.OK
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                catch (SqlException sqlEx)
                {
                    sqlConnection.Close();
                    
                    // Xử lý lỗi từ stored procedure (tên nhóm đã tồn tại)
                    if (sqlEx.Message.Contains("Tên nhóm món ăn đã tồn tại"))
                    {
                        MessageBox.Show("Tên nhóm món ăn đã tồn tại! Vui lòng nhập tên khác.",
                            "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtCategoryName.Focus();
                        txtCategoryName.SelectAll();
                    }
                    else
                    {
                        MessageBox.Show($"Lỗi cơ sở dữ liệu: {sqlEx.Message}",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // Đóng form và trả về DialogResult.Cancel
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all textboxes
            txtCategoryName.Clear();
            txtNotes.Clear();
            txtCategoryName.Focus();
        }
    }
}
