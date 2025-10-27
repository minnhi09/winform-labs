using Microsoft.Data.SqlClient;
using System.Data;

namespace Lab_Advanced_Command
{
    public partial class FoodForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=Chuong_5_Lab_Advanced_Command; integrated security=true; TrustServerCertificate=True";
        private DataTable foodTable;


        public FoodForm()
        {
            InitializeComponent();
        }

        private void FoodForm_Load(object sender, EventArgs e)
        {
            LoadCategory();
        }

        private void LoadCategory()
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

        private void cboCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCategory.SelectedValue == null) return;
            if (cboCategory.SelectedIndex == -1) return;

            DataRowView dataRowView = cboCategory.SelectedItem as DataRowView;
            int categoryId = Convert.ToInt32(dataRowView["ID"]);

            LoadFoodByCategory(categoryId);
        }

        private void LoadFoodByCategory(int categoryId)
        {
            // 1. Cấu hình kết nối
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // 2. Tạo câu lệnh SQL JOIN để lấy thông tin món ăn kèm tên danh mục
            sqlCommand.CommandText = @"
                SELECT 
                    F.ID,
                    F.Name,
                    F.Unit,
                    C.Name AS CategoryName,
                    F.Price,
                    F.Notes
                FROM Foods F
                INNER JOIN Categories C ON F.FoodCategoryID = C.ID
                WHERE F.FoodCategoryID = @CategoryId
                ORDER BY F.Name;";

            // 3. Thêm tham số để tránh SQL Injection
            sqlCommand.Parameters.AddWithValue("@CategoryId", categoryId);

            // 4. Thực hiện kết nối
            sqlConnection.Open();

            // 5. Thực thi câu lệnh SQL và đổ dữ liệu vào DataTable
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
            foodTable = new DataTable();
            sqlDataAdapter.Fill(foodTable);

            // 6. Đóng kết nối
            sqlConnection.Close();
            sqlCommand.Dispose();

            // 7. Gán dữ liệu vào DataGridView
            dgvFood.DataSource = foodTable;
        }

        private void menuItemAdd_Click(object sender, EventArgs e)
        {
            // TODO: Xử lý logic thêm món ăn mới
            MessageBox.Show(
                "Chức năng thêm món ăn mới\n\nSẽ mở form để nhập thông tin món ăn mới:\n" +
                "- Tên món ăn\n" +
                "- Đơn vị tính\n" +
                "- Nhóm món ăn\n" +
                "- Đơn giá\n" +
                "- Ghi chú",
                "Thêm món ăn mới",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }

        private void menuItemUpdate_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn không
            if (dgvFood.SelectedRows.Count == 0)
            {
                MessageBox.Show(
                    "Vui lòng chọn một món ăn để cập nhật!",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
                return;
            }

            // Lấy thông tin món ăn được chọn
            DataGridViewRow selectedRow = dgvFood.SelectedRows[0];
            int foodId = Convert.ToInt32(selectedRow.Cells["colID"].Value);
            string foodName = selectedRow.Cells["colName"].Value?.ToString() ?? "";
            string unit = selectedRow.Cells["colUnit"].Value?.ToString() ?? "";
            string categoryName = selectedRow.Cells["colCategoryName"].Value?.ToString() ?? "";
            decimal price = Convert.ToDecimal(selectedRow.Cells["colPrice"].Value ?? 0);
            string notes = selectedRow.Cells["colNotes"].Value?.ToString() ?? "";

            // TODO: Xử lý logic cập nhật món ăn
            MessageBox.Show(
                $"Chức năng cập nhật món ăn\n\n" +
                $"Thông tin món ăn đang chọn:\n" +
                $"- Mã: {foodId}\n" +
                $"- Tên: {foodName}\n" +
                $"- Đơn vị: {unit}\n" +
                $"- Nhóm: {categoryName}\n" +
                $"- Giá: {price:N0} VNĐ\n" +
                $"- Ghi chú: {notes}\n\n" +
                $"Sẽ mở form để chỉnh sửa thông tin món ăn này.",
                "Cập nhật món ăn",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}
