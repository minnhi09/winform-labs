using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Lab_Advanced_Command
{
    public partial class FoodInfoForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=Chuong_5_Lab_Advanced_Command; integrated security=true; TrustServerCertificate=True";

        public FoodInfoForm()
        {
            InitializeComponent();
        }

        private void FoodInfoForm_Load(object sender, EventArgs e)
        {
            InitValues();
        }

        private void InitValues()
        {
            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT ID, Name FROM Categories;";

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc thi cau lenh sql
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();

            sqlDataAdapter.Fill(dataTable);

            // 4. dong ket noi 
            sqlConnection.Close();
            sqlCommand.Dispose();

            // 5. ui logic
            cboCategory.DataSource = dataTable;
            cboCategory.DisplayMember = "Name";
            cboCategory.ValueMember = "ID";
        }

        private void ResetText()
        {
            txtFoodId.ResetText();
            txtFoodName.ResetText();
            txtUnit.ResetText();
            txtNotes.ResetText();
            numPrice.ResetText();

            cboCategory.ResetText();
        }

        private void btnAddNewCategory_Click(object sender, EventArgs e)
        {
            // TODO: Open form to add new category
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Cấu hình kết nối
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // 2. Cấu hình stored procedure
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.CommandText = "InsertFood";

                // 3. Thêm parameters
                SqlParameter idParameter = new SqlParameter("@ID", SqlDbType.Int);
                idParameter.Direction = ParameterDirection.Output;
                sqlCommand.Parameters.Add(idParameter);

                sqlCommand.Parameters.AddWithValue("@Name", txtFoodName.Text);
                sqlCommand.Parameters.AddWithValue("@Unit", txtUnit.Text);
                sqlCommand.Parameters.AddWithValue("@FoodCategoryID", cboCategory.SelectedValue);
                sqlCommand.Parameters.AddWithValue("@Price", (int)numPrice.Value);
                sqlCommand.Parameters.AddWithValue("@Notes", txtNotes.Text);

                // 4. Thực hiện kết nối và thực thi
                sqlConnection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                // 5. Lấy ID mới được tạo
                int newFoodId = Convert.ToInt32(idParameter.Value);

                // 6. Đóng kết nối
                sqlConnection.Close();
                sqlCommand.Dispose();

                // 7. Thông báo kết quả
                if (rowsAffected > 0)
                {
                    MessageBox.Show($"Thêm món ăn thành công!\nMã món ăn: {newFoodId}", 
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    // Hiển thị ID vừa tạo
                    txtFoodId.Text = newFoodId.ToString();
                    
                    // Reset form để tiếp tục thêm mới
                    DialogResult result = MessageBox.Show("Bạn có muốn thêm món ăn khác không?", 
                        "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    
                    if (result == DialogResult.Yes)
                    {
                        ResetText();
                        txtFoodName.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("Thêm món ăn thất bại!", "Lỗi", 
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show($"Lỗi cơ sở dữ liệu: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // TODO: Update food logic
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
