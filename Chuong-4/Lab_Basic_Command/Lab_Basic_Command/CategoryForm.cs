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
    public partial class CategoryForm : Form
    {
        // constructor
        public CategoryForm()
        {
            InitializeComponent();
        }

        public CategoryForm(string name) : this() 
        {
            this.Text = name;
        }

        // public const string CONNECTION_STRING = "Server=MNHY\\SQLEXPRESS01;Initial Catalog=RestaurantManagement;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=RestaurantManagement; integrated security=true;";

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT * FROM Category;";

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc thi cau lenh sql
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            // 4. xu ly du lieu tra ve => hien thi len danh sach ListView
            DisplayCategory(sqlDataReader);

            // 5. dong ket noi
            sqlConnection.Close();

            // logic types
            // 1, 2, 3, 5 => Data access logic
            // 4 => Presentation logic
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 0. lay du lieu tu form
            string categoryName = "";
            int categoryType = 0;

            categoryName = txtName.Text;
            categoryType = int.Parse(txtType.Text);

            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = @"
                INSERT INTO Category(Name, Type) 
                VALUES (@name, @type);
            ";

            sqlCommand.Parameters.AddWithValue("@name", categoryName);
            sqlCommand.Parameters.AddWithValue("@type", categoryType);

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc thi cau lenh sql
            int numOfRowAffected = sqlCommand.ExecuteNonQuery();

            // 4. xu ly du lieu tra ve => hien thi thong bao cho nguoi dung
            if (numOfRowAffected == 1)
            {
                MessageBox.Show("Thêm nhóm món ăn thành công"); 

                txtName.Text = "";
                txtType.Text = "";

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
            int categoryID = 0;
            string categoryName = "";
            int categoryType = 0;

            categoryID = int.Parse(txtID.Text);
            categoryName = txtName.Text;
            categoryType = int.Parse(txtType.Text);

            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = @"
                UPDATE Category
                SET
                    Name = @name,
                    Type = @type
                WHERE
                    ID = @id;
            ";

            sqlCommand.Parameters.AddWithValue("@id", categoryID);
            sqlCommand.Parameters.AddWithValue("@name", categoryName);
            sqlCommand.Parameters.AddWithValue("@type", categoryType);

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc thi cau lenh sql
            int numOfRowAffected = sqlCommand.ExecuteNonQuery();

            // 4. xu ly du lieu tra ve => hien thi thong bao cho nguoi dung
            if (numOfRowAffected == 1)
            {
                MessageBox.Show("Cập nhật nhóm món ăn thành công"); 

                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";

                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

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
            int categoryID = 0;

            categoryID = int.Parse(txtID.Text);

            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = @"
                DELETE FROM Category
                WHERE ID = @id;
            ";

            sqlCommand.Parameters.AddWithValue("@id", categoryID);

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc thi cau lenh sql
            int numOfRowAffected = sqlCommand.ExecuteNonQuery();

            // 4. xu ly du lieu tra ve => hien thi thong bao cho nguoi dung
            if (numOfRowAffected == 1)
            {
                MessageBox.Show("Xóa nhóm món ăn thành công"); 

                txtID.Text = "";
                txtName.Text = "";
                txtType.Text = "";

                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;

                btnLoad.PerformClick();  
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
            }

            // 5. dong ket noi
            sqlConnection.Close();
        }

















        // presentation logic
        private void DisplayCategory(SqlDataReader sqlDataReader)
        {

            lvCategory.Items.Clear();

            while (true)
            {
                if (!sqlDataReader.Read())
                {
                    break;
                }

                ListViewItem item = new ListViewItem(sqlDataReader["ID"].ToString());

                item.SubItems.Add(sqlDataReader["Name"].ToString());
                item.SubItems.Add(sqlDataReader["Type"].ToString());

                lvCategory.Items.Add(item);
            }
        }

        private void lvCategory_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count == 0) return;
            var item = lvCategory.SelectedItems[0];

            txtID.Text = item.SubItems[0].Text;
            txtName.Text = item.SubItems[1].Text;
            txtType.Text = item.SubItems[2].Text;

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count > 0)
                btnDelete.PerformClick();
        }

        private void tsmViewFood_Click(object sender, EventArgs e)
        {
            if (lvCategory.SelectedItems.Count == 0)
            {
                return;
            }

            int catId = int.Parse(lvCategory.SelectedItems[0].SubItems[0].Text);

            var foodForm = new FoodForm();
            foodForm.LoadFood(catId);

            foodForm.Show(); 
            // foodForm.ShowDialog(); 
        }




        #region Old Code
        private void DisplayCategory_Backup(SqlDataReader reader)
        {
            lvCategory.Items.Clear();
            while (reader.Read())
            {
                var item = new ListViewItem(reader["ID"].ToString());     // cột 0
                item.SubItems.Add(reader["Name"].ToString());             // cột 1
                item.SubItems.Add(reader["Type"].ToString());             // cột 2
                lvCategory.Items.Add(item);
            }
            // reset ô nhập
            txtID.Text = "";
            txtName.Text = "";
            txtType.Text = "";
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
        private void btnLoad_Click_Backup(object sender, EventArgs e)
        {
            //using (var sqlConnection = new SqlConnection(CONNECTION_STRING))
            //using (var cmd = sqlConnection.CreateCommand())
            //{
            //    cmd.CommandText = "SELECT ID, Name, [Type] FROM Category ORDER BY ID";
            //    sqlConnection.Open();
            //    using (var reader = cmd.ExecuteReader())
            //    {
            //        DisplayCategory(reader);
            //    }
            //}
        }

        private void btnAdd_Click_Backup(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) || string.IsNullOrWhiteSpace(txtType.Text))
            {
                MessageBox.Show("Nhập đầy đủ Tên nhóm và Loại (0/1).");
                return;
            }

            using (var sqlConnection = new SqlConnection(CONNECTION_STRING))
            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO Category(Name, [Type]) VALUES (@name, @type)";
                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@type", int.Parse(txtType.Text.Trim()));

                sqlConnection.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 1)
                {
                    MessageBox.Show("Thêm nhóm món ăn thành công");
                    btnLoad.PerformClick();            // reload
                    txtName.Text = "";
                    txtType.Text = "";
                }
                else MessageBox.Show("Có lỗi khi thêm.");
            }
        }

        private void btnUpdate_Click_Backup(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Chọn dòng cần cập nhật.");
                return;
            }

            using (var sqlConnection = new SqlConnection(CONNECTION_STRING))
            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = @"UPDATE Category 
                            SET Name = @name, [Type] = @type 
                            WHERE ID = @id";
                cmd.Parameters.AddWithValue("@name", txtName.Text.Trim());
                cmd.Parameters.AddWithValue("@type", int.Parse(txtType.Text.Trim()));
                cmd.Parameters.AddWithValue("@id", int.Parse(txtID.Text));

                sqlConnection.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 1)
                {
                    // cập nhật ngay trên listview cho mượt
                    var item = lvCategory.SelectedItems[0];
                    item.SubItems[1].Text = txtName.Text.Trim();
                    item.SubItems[2].Text = txtType.Text.Trim();

                    MessageBox.Show("Cập nhật nhóm món ăn thành công");

                    // clear + disable
                    txtID.Text = txtName.Text = txtType.Text = "";
                    btnUpdate.Enabled = btnDelete.Enabled = false;
                }
                else MessageBox.Show("Cập nhật thất bại.");
            }
        }

        private void btnDelete_Click_Backup(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtID.Text))
            {
                MessageBox.Show("Chọn dòng cần xóa.");
                return;
            }

            if (MessageBox.Show("Xóa nhóm đã chọn?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            using (var sqlConnection = new SqlConnection(CONNECTION_STRING))
            using (var cmd = sqlConnection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM Category WHERE ID = @id";
                cmd.Parameters.AddWithValue("@id", int.Parse(txtID.Text));

                sqlConnection.Open();
                int rows = cmd.ExecuteNonQuery();

                if (rows == 1)
                {
                    // remove khỏi listview
                    var item = lvCategory.SelectedItems[0];
                    lvCategory.Items.Remove(item);

                    MessageBox.Show("Xóa nhóm món ăn thành công");
                    txtID.Text = txtName.Text = txtType.Text = "";
                    btnUpdate.Enabled = btnDelete.Enabled = false;
                }
                else MessageBox.Show("Xóa thất bại.");
            }
        }



        private void CategoryForm_Load(object sender, EventArgs e)
        {

        }


        //private void contextMenuStrip1(object sender, EventArgs e)
        //{

        //}

        #endregion
    }
}
