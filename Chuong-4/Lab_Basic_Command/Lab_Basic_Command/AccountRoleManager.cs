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
    public partial class AccountRoleManager : Form
    {
        private int _accountID;
        private string _accountName;
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=RestaurantManagement; integrated security=true;";

        public AccountRoleManager()
        {
            InitializeComponent();
        }

        public AccountRoleManager(int accountID, string accountName) : this()
        {
            _accountID = accountID;
            _accountName = accountName;
        }

        private void AccountRoleManager_Load(object sender, EventArgs e)
        {
            this.Text = $"Quản lý vai trò - {_accountName}";
            lblAccountInfo.Text = $"Tài khoản: {_accountName} (ID: {_accountID})";

            LoadAllRoles();
            LoadAccountRoles();
        }

        private void LoadAllRoles()
        {
            // Load tất cả vai trò vào ComboBox
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT ID, RoleName, Description FROM Role ORDER BY RoleName;";

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                cboRole.Items.Clear();
                
                while (reader.Read())
                {
                    var roleItem = new RoleItem
                    {
                        ID = (int)reader["ID"],
                        RoleName = reader["RoleName"].ToString(),
                        Description = reader["Description"] != DBNull.Value ? reader["Description"].ToString() : ""
                    };
                    cboRole.Items.Add(roleItem);
                }

                if (cboRole.Items.Count > 0)
                {
                    cboRole.SelectedIndex = 0;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách vai trò: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void LoadAccountRoles()
        {
            // Load vai trò hiện tại của tài khoản
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = @"
                SELECT r.ID, r.RoleName, r.Description
                FROM Role r
                INNER JOIN AccountRole ar ON r.ID = ar.RoleID
                WHERE ar.AccountID = @accountID
                ORDER BY r.RoleName;
            ";

            sqlCommand.Parameters.AddWithValue("@accountID", _accountID);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                DisplayAccountRoles(reader);

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải vai trò của tài khoản: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void DisplayAccountRoles(SqlDataReader reader)
        {
            lvAccountRoles.Items.Clear();

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["ID"].ToString());
                item.SubItems.Add(reader["RoleName"].ToString());
                item.SubItems.Add(reader["Description"] != DBNull.Value ? reader["Description"].ToString() : "");

                lvAccountRoles.Items.Add(item);
            }
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            if (cboRole.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn vai trò cần thêm");
                return;
            }

            RoleItem selectedRole = (RoleItem)cboRole.SelectedItem;

            // Kiểm tra xem vai trò đã tồn tại chưa
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = @"
                SELECT COUNT(*) 
                FROM AccountRole 
                WHERE AccountID = @accountID AND RoleID = @roleID;
            ";

            sqlCommand.Parameters.AddWithValue("@accountID", _accountID);
            sqlCommand.Parameters.AddWithValue("@roleID", selectedRole.ID);

            try
            {
                sqlConnection.Open();
                int count = (int)sqlCommand.ExecuteScalar();

                if (count > 0)
                {
                    MessageBox.Show("Tài khoản đã có vai trò này rồi!");
                    return;
                }

                sqlConnection.Close();

                // Thêm vai trò mới
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = @"
                    INSERT INTO AccountRole(AccountID, RoleID)
                    VALUES (@accountID, @roleID);
                ";

                sqlCommand.Parameters.AddWithValue("@accountID", _accountID);
                sqlCommand.Parameters.AddWithValue("@roleID", selectedRole.ID);

                sqlConnection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    MessageBox.Show("Thêm vai trò thành công");
                    LoadAccountRoles();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnRemoveRole_Click(object sender, EventArgs e)
        {
            if (lvAccountRoles.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn vai trò cần xóa");
                return;
            }

            var item = lvAccountRoles.SelectedItems[0];
            int roleID = int.Parse(item.SubItems[0].Text);
            string roleName = item.SubItems[1].Text;

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa vai trò '{roleName}' khỏi tài khoản này?",
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
                DELETE FROM AccountRole
                WHERE AccountID = @accountID AND RoleID = @roleID;
            ";

            sqlCommand.Parameters.AddWithValue("@accountID", _accountID);
            sqlCommand.Parameters.AddWithValue("@roleID", roleID);

            try
            {
                sqlConnection.Open();
                int rowsAffected = sqlCommand.ExecuteNonQuery();

                if (rowsAffected == 1)
                {
                    MessageBox.Show("Xóa vai trò thành công");
                    LoadAccountRoles();
                }
                else
                {
                    MessageBox.Show("Đã có lỗi xảy ra. Vui lòng thử lại");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void lvAccountRoles_Click(object sender, EventArgs e)
        {
            btnRemoveRole.Enabled = lvAccountRoles.SelectedItems.Count > 0;
        }

        private void tsmRemove_Click(object sender, EventArgs e)
        {
            if (lvAccountRoles.SelectedItems.Count > 0)
            {
                btnRemoveRole.PerformClick();
            }
        }
    }

    // Helper class để lưu thông tin vai trò trong ComboBox
    public class RoleItem
    {
        public int ID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return RoleName;
        }
    }
}
