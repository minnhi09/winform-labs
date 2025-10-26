using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Basic_Command
{
    public partial class FoodForm : Form
    {
        // private const string CONNECTION_STRING =
        // "Server=.\\SQLEXPRESS; Database=RestaurantManagement; Integrated Security=True; TrustServerCertificate=True";
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=RestaurantManagement; integrated security=true;";

        private int _categoryId;
        private DataTable _dtFood;
        private SqlDataAdapter _adapter;

        public FoodForm()
        {
            InitializeComponent();
            LoadCategories();
            ClearForm();
            DisableButtons();
        }

        public void LoadFood(int categoryID)
        {
            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = @"
                SELECT * FROM Food
                WHERE FoodCategoryID = @id;
            ";

            sqlCommand.Parameters.AddWithValue("@id", categoryID);

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc hien truy van
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);

            // 4. xu ly du lieu
            DataTable dataTable = new DataTable("Food");
            sqlDataAdapter.Fill(dataTable);

            dgvFood.DataSource = dataTable;

            // 5. cap nhat tieu de
            sqlCommand.CommandText = @"
                SELECT Name FROM Category
                WHERE ID = @id;
            ";

            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@id", categoryID);

            string categoryName = sqlCommand.ExecuteScalar().ToString();
            this.Text = $"Danh sách các món ăn thuộc nhóm: {categoryName}";

            // 6. dong ket noi
            sqlConnection.Close();
        }

        // Nạp danh sách Category vào ComboBox
        private void LoadCategories()
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            
            sqlCommand.CommandText = "SELECT ID, Name FROM Category";
            
            sqlConnection.Open();
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            
            cboFoodCategoryID.DataSource = dt;
            cboFoodCategoryID.DisplayMember = "Name";
            cboFoodCategoryID.ValueMember = "ID";
            
            sqlConnection.Close();
        }

        // Xử lý khi click vào dòng trong DataGridView
        private void dgvFood_Click(object sender, EventArgs e)
        {
            if (dgvFood.CurrentRow != null && dgvFood.CurrentRow.Index >= 0)
            {
                DataGridViewRow row = dgvFood.CurrentRow;
                
                txtID.Text = row.Cells["ID"].Value?.ToString() ?? "";
                txtName.Text = row.Cells["FoodName"].Value?.ToString() ?? "";
                txtUnit.Text = row.Cells["Unit"].Value?.ToString() ?? "";
            
                cboFoodCategoryID.SelectedValue = Convert.ToInt32(row.Cells["FoodCategoryID"].Value);
                
                txtPrice.Text = row.Cells["Price"].Value?.ToString() ?? "";
                txtNotes.Text = row.Cells["Notes"].Value?.ToString() ?? "";
                
                EnableButtons();
            }
        }

        // Xử lý nút Save
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên món ăn!");
                return;
            }

            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Nếu có ID thì UPDATE, không có thì INSERT
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                // INSERT
                sqlCommand.CommandText = @"
                    INSERT INTO Food(Name, Unit, FoodCategoryID, Price, Notes)
                    VALUES (@name, @unit, @catId, @price, @notes);
                ";
            }
            else
            {
                // UPDATE
                sqlCommand.CommandText = @"
                    UPDATE Food
                    SET Name = @name,
                        Unit = @unit,
                        FoodCategoryID = @catId,
                        Price = @price,
                        Notes = @notes
                    WHERE ID = @id;
                ";
                sqlCommand.Parameters.AddWithValue("@id", int.Parse(txtID.Text));
            }

            sqlCommand.Parameters.AddWithValue("@name", txtName.Text);
            sqlCommand.Parameters.AddWithValue("@unit", txtUnit.Text);
            sqlCommand.Parameters.AddWithValue("@catId", cboFoodCategoryID.SelectedValue);

            double price = double.Parse(txtPrice.Text);
            sqlCommand.Parameters.AddWithValue("@price", price);
            
            sqlCommand.Parameters.AddWithValue("@notes", txtNotes.Text);

            sqlConnection.Open();
            int result = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();

            if (result > 0)
            {
                MessageBox.Show("Lưu thông tin món ăn thành công!");
                
                // Reload data
                if (cboFoodCategoryID.SelectedValue != null)
                {
                    LoadFood(Convert.ToInt32(cboFoodCategoryID.SelectedValue));
                }
                
                ClearForm();
                DisableButtons();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại!");
            }
        }

        // Xử lý nút Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Vui lòng chọn món ăn cần xóa!");
                return;
            }

            var confirmResult = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa món ăn '{txtName.Text}' không?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.Yes)
            {
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                sqlCommand.CommandText = "DELETE FROM Food WHERE ID = @id";
                sqlCommand.Parameters.AddWithValue("@id", int.Parse(txtID.Text));

                sqlConnection.Open();
                int result = sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                if (result > 0)
                {
                    MessageBox.Show("Xóa món ăn thành công!");
                    
                    // Reload data
                    if (cboFoodCategoryID.SelectedValue != null)
                    {
                        LoadFood(Convert.ToInt32(cboFoodCategoryID.SelectedValue));
                    }
                    
                    ClearForm();
                    DisableButtons();
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra. Vui lòng thử lại!");
                }
            }
        }

        // Xóa dữ liệu trên form
        private void ClearForm()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtUnit.Text = "";
            txtPrice.Text = "";
            txtNotes.Text = "";
            
            if (cboFoodCategoryID.Items.Count > 0)
            {
                cboFoodCategoryID.SelectedIndex = 0;
            }
        }

        // Vô hiệu hóa các nút
        private void DisableButtons()
        {
            btnSave.Enabled = true;  // Save luôn bật để có thể thêm mới
            btnDelete.Enabled = false;
        }

        // Kích hoạt các nút
        private void EnableButtons()
        {
            btnSave.Enabled = true;
            btnDelete.Enabled = true;
        }

        #region Old code

        public void LoadFood_Backup(int categoryID)
        {
            _categoryId = categoryID;

            var kq = DataProvider.Instance.ExecuteQuery("SELECT Name FROM Category WHERE ID = @id",
                new SqlParameter("@id", _categoryId));

            if (kq.Rows.Count > 0)
            {
                var catName = (string)kq.Rows[0]["Name"];
                this.Text = "Danh sách các món ăn thuộc nhóm: " + catName;
            }
        }

        public void LoadFood1(int categoryID)
        {
            _categoryId = categoryID;
            using (var conn = new SqlConnection(CONNECTION_STRING))
            using (var cmd = conn.CreateCommand())
            {
                // Lấy tên nhóm để set tiêu đề
                cmd.CommandText = "SELECT Name FROM Category WHERE ID = @id";
                cmd.Parameters.AddWithValue("@id", _categoryId);
                conn.Open();
                var catName = (string)cmd.ExecuteScalar();
                this.Text = "Danh sách các món ăn thuộc nhóm: " + catName;
            }

            // Nạp dữ liệu món ăn theo nhóm
            var sql = "SELECT ID, Name, Unit, FoodCategoryID, Price, Notes FROM Food WHERE FoodCategoryID = @cat";
            _adapter = new SqlDataAdapter(sql, CONNECTION_STRING);
            _adapter.SelectCommand.Parameters.AddWithValue("@cat", _categoryId);

            // Cho phép adapter tự build Insert/Update/Delete
            var builder = new SqlCommandBuilder(_adapter);

            _dtFood = new DataTable("Food");
            _adapter.Fill(_dtFood);
            dgvFood.DataSource = _dtFood;
        }
        #endregion
    }
}
