using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Chuong_6.BusinessLogic;
using Chuong_6.DataAccess;

namespace Chuong_6.RestaurantManagement
{
    public partial class frmAccountManagement : Form
    {
        private readonly AccountBL accountBL;
        private readonly RoleBL roleBL;
        private string currentUser;

        public frmAccountManagement()
        {
            InitializeComponent();
            this.accountBL = new AccountBL();
            this.roleBL = new RoleBL();
        }

        public frmAccountManagement(string username) : this()
        {
            this.currentUser = username;
        }

        private void frmAccountManagement_Load(object sender, EventArgs e)
        {
            LoadAccounts();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            dgvAccounts.AutoGenerateColumns = false;
            dgvAccounts.AllowUserToAddRows = false;
            dgvAccounts.AllowUserToDeleteRows = false;
            dgvAccounts.ReadOnly = true;
            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.MultiSelect = false;

            dgvAccounts.Columns.Clear();

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "AccountName",
                HeaderText = "Tài khoản",
                Name = "colAccountName",
                Width = 120
            });

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "FullName",
                HeaderText = "Họ tên",
                Name = "colFullName",
                Width = 200
            });

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Email",
                HeaderText = "Email",
                Name = "colEmail",
                Width = 180
            });

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Phone",
                HeaderText = "Điện thoại",
                Name = "colPhone",
                Width = 110
            });

            dgvAccounts.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DateCreated",
                HeaderText = "Ngày tạo",
                Name = "colDateCreated",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy HH:mm" }
            });
        }

        private void LoadAccounts()
        {
            try
            {
                List<Account> accounts = accountBL.GetAllAccounts();
                dgvAccounts.DataSource = accounts;
                lblTotal.Text = $"Tổng số: {accounts.Count} tài khoản";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAccountDetail frm = new frmAccountDetail();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadAccounts();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string accountName = dgvAccounts.SelectedRows[0].Cells["colAccountName"].Value.ToString();
            Account account = accountBL.GetAccountByName(accountName);

            if (account != null)
            {
                frmAccountDetail frm = new frmAccountDetail(account);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadAccounts();
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string accountName = dgvAccounts.SelectedRows[0].Cells["colAccountName"].Value.ToString();

            if (accountName.ToLower() == "admin")
            {
                MessageBox.Show("Không thể xóa tài khoản admin!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa tài khoản '{accountName}'?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int affected = accountBL.DeleteAccount(accountName);
                    if (affected > 0)
                    {
                        MessageBox.Show("Xóa tài khoản thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAccounts();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi xóa tài khoản: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string accountName = dgvAccounts.SelectedRows[0].Cells["colAccountName"].Value.ToString();

            DialogResult result = MessageBox.Show(
                $"Reset mật khẩu về '123456' cho tài khoản '{accountName}'?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    int affected = accountBL.ResetPassword(accountName, "123456");
                    if (affected > 0)
                    {
                        MessageBox.Show("Reset mật khẩu thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi reset mật khẩu: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAssignRole_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string accountName = dgvAccounts.SelectedRows[0].Cells["colAccountName"].Value.ToString();
            frmAssignRole frm = new frmAssignRole(accountName);
            frm.ShowDialog();
        }

        private void btnViewRoles_Click(object sender, EventArgs e)
        {
            if (dgvAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string accountName = dgvAccounts.SelectedRows[0].Cells["colAccountName"].Value.ToString();

            try
            {
                List<RoleAccount> roles = accountBL.GetAccountRoles(accountName);

                if (roles.Count == 0)
                {
                    MessageBox.Show("Tài khoản chưa được gán vai trò nào!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string rolesText = $"Vai trò của tài khoản '{accountName}':\n\n";
                foreach (var role in roles)
                {
                    string status = role.Actived ? "Hoạt động" : "Vô hiệu hóa";
                    rolesText += $"• {role.RoleName} - {status}\n";
                }

                MessageBox.Show(rolesText, "Danh sách vai trò",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtSearch.Text.Trim();
                List<Account> accounts = accountBL.SearchAccounts(keyword);
                dgvAccounts.DataSource = accounts;
                lblTotal.Text = $"Tìm thấy: {accounts.Count} tài khoản";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tìm kiếm: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtSearch.Clear();
            LoadAccounts();
        }

        private void dgvAccounts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEdit_Click(sender, e);
            }
        }
    }
}
