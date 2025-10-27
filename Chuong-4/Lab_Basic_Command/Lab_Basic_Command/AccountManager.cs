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
    public partial class AccountManager : Form
    {
        // constructor
        public AccountManager()
        {
            InitializeComponent();
        }

        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=RestaurantManagement; integrated security=true;";

        private void AccountManager_Load(object sender, EventArgs e)
        {
            // Load danh sách tài khoản khi form khởi động
            btnLoad.PerformClick();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT ID, Username, DisplayName, Phone, IsActive, Notes FROM Account ORDER BY ID;";

            // 2. thuc hien ket noi
            sqlConnection.Open();

            // 3. thuc thi cau lenh sql
            SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

            // 4. xu ly du lieu tra ve => hien thi len danh sach ListView
            DisplayAccounts(sqlDataReader);

            // 5. dong ket noi
            sqlConnection.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            // 0. lay du lieu tu form
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string displayName = txtDisplayName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            bool isActive = chkIsActive.Checked;
            string notes = txtNotes.Text.Trim();

            // Kiểm tra dữ liệu
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu");
                txtPassword.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(displayName))
            {
                MessageBox.Show("Vui lòng nhập tên hiển thị");
                txtDisplayName.Focus();
                return;
            }

            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = @"
                INSERT INTO Account(Username, Password, DisplayName, Phone, IsActive, Notes) 
                VALUES (@username, @password, @displayName, @phone, @isActive, @notes);
            ";

            sqlCommand.Parameters.AddWithValue("@username", username);
            sqlCommand.Parameters.AddWithValue("@password", password);
            sqlCommand.Parameters.AddWithValue("@displayName", displayName);
            sqlCommand.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone);
            sqlCommand.Parameters.AddWithValue("@isActive", isActive);
            sqlCommand.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes);

            try
            {
                // 2. thuc hien ket noi
                sqlConnection.Open();

                // 3. thuc thi cau lenh sql
                int numOfRowAffected = sqlCommand.ExecuteNonQuery();

                // 4. xu ly du lieu tra ve => hien thi thong bao cho nguoi dung
                if (numOfRowAffected == 1)
                {
                    MessageBox.Show("Thêm tài khoản thành công");

                    // Reset form
                    ClearForm();

                    // Reload danh sách
                    btnLoad.PerformClick();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
                }
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Duplicate key error
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.");
                }
                else
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            finally
            {
                // 5. dong ket noi
                sqlConnection.Close();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 0. lay du lieu tu form
            int accountID = int.Parse(txtID.Text);
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();
            string displayName = txtDisplayName.Text.Trim();
            string phone = txtPhone.Text.Trim();
            bool isActive = chkIsActive.Checked;
            string notes = txtNotes.Text.Trim();

            // Kiểm tra dữ liệu
            if (string.IsNullOrWhiteSpace(username))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập");
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(displayName))
            {
                MessageBox.Show("Vui lòng nhập tên hiển thị");
                txtDisplayName.Focus();
                return;
            }

            // 1. cau hinh ket noi
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Nếu có nhập password mới thì cập nhật, không thì giữ nguyên
            if (string.IsNullOrWhiteSpace(password))
            {
                sqlCommand.CommandText = @"
                    UPDATE Account
                    SET
                        Username = @username,
                        DisplayName = @displayName,
                        Phone = @phone,
                        IsActive = @isActive,
                        Notes = @notes
                    WHERE
                        ID = @id;
                ";
            }
            else
            {
                sqlCommand.CommandText = @"
                    UPDATE Account
                    SET
                        Username = @username,
                        Password = @password,
                        DisplayName = @displayName,
                        Phone = @phone,
                        IsActive = @isActive,
                        Notes = @notes
                    WHERE
                        ID = @id;
                ";
                sqlCommand.Parameters.AddWithValue("@password", password);
            }

            sqlCommand.Parameters.AddWithValue("@id", accountID);
            sqlCommand.Parameters.AddWithValue("@username", username);
            sqlCommand.Parameters.AddWithValue("@displayName", displayName);
            sqlCommand.Parameters.AddWithValue("@phone", string.IsNullOrWhiteSpace(phone) ? (object)DBNull.Value : phone);
            sqlCommand.Parameters.AddWithValue("@isActive", isActive);
            sqlCommand.Parameters.AddWithValue("@notes", string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes);

            try
            {
                // 2. thuc hien ket noi
                sqlConnection.Open();

                // 3. thuc thi cau lenh sql
                int numOfRowAffected = sqlCommand.ExecuteNonQuery();

                // 4. xu ly du lieu tra ve => hien thi thong bao cho nguoi dung
                if (numOfRowAffected == 1)
                {
                    MessageBox.Show("Cập nhật tài khoản thành công");

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
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627) // Duplicate key error
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.");
                }
                else
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            finally
            {
                // 5. dong ket noi
                sqlConnection.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int accountID = int.Parse(txtID.Text);

            // Xác nhận xóa
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa tài khoản này?",
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
                DELETE FROM Account
                WHERE ID = @id;
            ";

            sqlCommand.Parameters.AddWithValue("@id", accountID);

            try
            {
                // 2. thuc hien ket noi
                sqlConnection.Open();

                // 3. thuc thi cau lenh sql
                int numOfRowAffected = sqlCommand.ExecuteNonQuery();

                // 4. xu ly du lieu tra ve => hien thi thong bao cho nguoi dung
                if (numOfRowAffected == 1)
                {
                    MessageBox.Show("Xóa tài khoản thành công");

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
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Không thể xóa tài khoản này vì có dữ liệu liên quan.\n" + ex.Message);
            }
            finally
            {
                // 5. dong ket noi
                sqlConnection.Close();
            }
        }

        // Presentation logic
        private void DisplayAccounts(SqlDataReader sqlDataReader)
        {
            lvAccount.Items.Clear();

            while (sqlDataReader.Read())
            {
                ListViewItem item = new ListViewItem(sqlDataReader["ID"].ToString());

                item.SubItems.Add(sqlDataReader["Username"].ToString());
                item.SubItems.Add(sqlDataReader["DisplayName"].ToString());
                item.SubItems.Add(sqlDataReader["Phone"] != DBNull.Value ? sqlDataReader["Phone"].ToString() : "");
                item.SubItems.Add((bool)sqlDataReader["IsActive"] ? "Hoạt động" : "Khóa");
                item.SubItems.Add(sqlDataReader["Notes"] != DBNull.Value ? sqlDataReader["Notes"].ToString() : "");

                lvAccount.Items.Add(item);
            }
        }

        private void lvAccount_Click(object sender, EventArgs e)
        {
            if (lvAccount.SelectedItems.Count == 0) return;
            var item = lvAccount.SelectedItems[0];

            txtID.Text = item.SubItems[0].Text;
            txtUsername.Text = item.SubItems[1].Text;
            txtPassword.Text = ""; // Không hiển thị password
            txtDisplayName.Text = item.SubItems[2].Text;
            txtPhone.Text = item.SubItems[3].Text;
            chkIsActive.Checked = item.SubItems[4].Text == "Hoạt động";
            txtNotes.Text = item.SubItems[5].Text;

            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
        }

        private void ClearForm()
        {
            txtID.Text = "";
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtDisplayName.Text = "";
            txtPhone.Text = "";
            chkIsActive.Checked = true;
            txtNotes.Text = "";
        }

        private void tsmDelete_Click(object sender, EventArgs e)
        {
            if (lvAccount.SelectedItems.Count > 0)
                btnDelete.PerformClick();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
        }
    }
}
