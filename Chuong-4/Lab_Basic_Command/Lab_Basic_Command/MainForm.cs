using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lab_Basic_Command
{
    public partial class MainForm : Form
    {
        // constructor
        public MainForm()
        {
            InitializeComponent();
        }

        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=RestaurantManagement; integrated security=true;";

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Load danh sách bàn khi form khởi động
            btnLoad.PerformClick();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT * FROM [Table] ORDER BY ID;";

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc thi cau lenh sql
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            // 4. xu ly du lieu tra ve => hien thi len danh sach ListView
            DisplayTable(sqlDataReader);

            // 5. dong ket noi
            sqlConnection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 0. lay du lieu tu form
            string tableName = "";
            string status = "Trống";
            string notes = "";

            tableName = txtName.Text.Trim();
            status = cboStatus.Text;
            notes = txtNotes.Text.Trim();

            // Kiểm tra dữ liệu
            if (string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show("Vui lòng nhập tên bàn");
                txtName.Focus();
                return;
            }

            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = @"
                INSERT INTO [Table](Name, Status, Notes) 
                VALUES (@name, @status, @notes);
            ";

            sqlCommand.Parameters.AddWithValue("@name", tableName);
            sqlCommand.Parameters.AddWithValue("@status", status);
            sqlCommand.Parameters.AddWithValue("@notes", notes);

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc thi cau lenh sql
            int numOfRowAffected = sqlCommand.ExecuteNonQuery();

            // 4. xu ly du lieu tra ve => hien thi thong bao cho nguoi dung
            if (numOfRowAffected == 1)
            {
                MessageBox.Show("Thêm bàn thành công");

                // Reset form
                ClearForm();

                // Reload danh sách
                btnLoad.PerformClick();
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
            }

            // 5. dong ket noi
            sqlConnection.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 0. lay du lieu tu form
            int tableID = 0;
            string tableName = "";
            string status = "";
            string notes = "";

            tableID = int.Parse(txtID.Text);
            tableName = txtName.Text.Trim();
            status = cboStatus.Text;
            notes = txtNotes.Text.Trim();

            // Kiểm tra dữ liệu
            if (string.IsNullOrWhiteSpace(tableName))
            {
                MessageBox.Show("Vui lòng nhập tên bàn");
                txtName.Focus();
                return;
            }

            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = @"
                UPDATE [Table]
                SET
                    Name = @name,
                    Status = @status,
                    Notes = @notes
                WHERE
                    ID = @id;
            ";

            sqlCommand.Parameters.AddWithValue("@id", tableID);
            sqlCommand.Parameters.AddWithValue("@name", tableName);
            sqlCommand.Parameters.AddWithValue("@status", status);
            sqlCommand.Parameters.AddWithValue("@notes", notes);

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc thi cau lenh sql
            int numOfRowAffected = sqlCommand.ExecuteNonQuery();

            // 4. xu ly du lieu tra ve => hien thi thong bao cho nguoi dung
            if (numOfRowAffected == 1)
            {
                MessageBox.Show("Cập nhật bàn thành công");

                // Reset form
                ClearForm();

                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                // Reload danh sách
                btnLoad.PerformClick();
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
            }

            // 5. dong ket noi
            sqlConnection.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int tableID = 0;

            tableID = int.Parse(txtID.Text);

            // Xác nhận xóa
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa bàn này?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = @"
                DELETE FROM [Table]
                WHERE ID = @id;
            ";

            sqlCommand.Parameters.AddWithValue("@id", tableID);

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc thi cau lenh sql
            int numOfRowAffected = sqlCommand.ExecuteNonQuery();

            // 4. xu ly du lieu tra ve => hien thi thong bao cho nguoi dung
            if (numOfRowAffected == 1)
            {
                MessageBox.Show("Xóa bàn thành công");

                // Reset form
                ClearForm();

                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                // Reload danh sách
                btnLoad.PerformClick();
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
            }

            // 5. dong ket noi
            sqlConnection.Close();
        }

        // Presentation logic
        private void DisplayTable(SqlDataReader sqlDataReader)
        {
            lvTable.Items.Clear();

            while (sqlDataReader.Read())
            {
                ListViewItem item = new ListViewItem(sqlDataReader["ID"].ToString());

                item.SubItems.Add(sqlDataReader["Name"].ToString());
                item.SubItems.Add(sqlDataReader["Status"].ToString());
                item.SubItems.Add(sqlDataReader["Notes"].ToString());

                lvTable.Items.Add(item);
            }
        }

        private void lvTable_Click(object sender, EventArgs e)
        {
            if (lvTable.SelectedItems.Count == 0) return;
            var item = lvTable.SelectedItems[0];

            txtID.Text = item.SubItems[0].Text;
            txtName.Text = item.SubItems[1].Text;
            cboStatus.Text = item.SubItems[2].Text;
            txtNotes.Text = item.SubItems[3].Text;

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void ClearForm()
        {
            txtID.Text = "";
            txtName.Text = "";
            cboStatus.SelectedIndex = 0;
            txtNotes.Text = "";
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (lvTable.SelectedItems.Count > 0)
                btnDelete.PerformClick();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }

        // Menu event handlers
        private void menuCategory_Click(object sender, EventArgs e)
        {
            CategoryForm categoryForm = new CategoryForm();
            categoryForm.Show();
        }

        private void menuFood_Click(object sender, EventArgs e)
        {
            FoodForm foodForm = new FoodForm();
            foodForm.Show();
        }

        private void menuAccount_Click(object sender, EventArgs e)
        {
            AccountManager accountManager = new AccountManager();
            accountManager.Show();
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
