using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Chuong_6.BusinessLogic;
using Chuong_6.DataAccess;

namespace Chuong_6.RestaurantManagement
{
    public partial class frmAssignRole : Form
    {
        private readonly RoleBL roleBL;
        private readonly AccountBL accountBL;
        private readonly string accountName;

        public frmAssignRole(string accountName)
        {
            InitializeComponent();
            this.roleBL = new RoleBL();
            this.accountBL = new AccountBL();
            this.accountName = accountName;
        }

        private void frmAssignRole_Load(object sender, EventArgs e)
        {
            lblAccountName.Text = $"Gán vai trò cho tài khoản: {accountName}";
            LoadRoles();
            LoadAccountRoles();
        }

        private void LoadRoles()
        {
            try
            {
                List<Role> roles = roleBL.GetAllRoles();

                clbRoles.Items.Clear();
                foreach (var role in roles)
                {
                    clbRoles.Items.Add(role, false);
                }

                clbRoles.DisplayMember = "RoleName";
                clbRoles.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải danh sách vai trò: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAccountRoles()
        {
            try
            {
                List<RoleAccount> accountRoles = accountBL.GetAccountRoles(accountName);

                for (int i = 0; i < clbRoles.Items.Count; i++)
                {
                    Role role = (Role)clbRoles.Items[i];
                    bool hasRole = accountRoles.Any(ar => ar.RoleID == role.ID && ar.Actived);
                    clbRoles.SetItemChecked(i, hasRole);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải vai trò của tài khoản: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                List<RoleAccount> currentRoles = accountBL.GetAccountRoles(accountName);

                // Xử lý từng vai trò
                for (int i = 0; i < clbRoles.Items.Count; i++)
                {
                    Role role = (Role)clbRoles.Items[i];
                    bool isChecked = clbRoles.GetItemChecked(i);
                    var existingRole = currentRoles.FirstOrDefault(r => r.RoleID == role.ID);

                    if (isChecked && existingRole == null)
                    {
                        // Gán vai trò mới
                        roleBL.AssignRoleToAccount(accountName, role.ID);
                    }
                    else if (isChecked && existingRole != null && !existingRole.Actived)
                    {
                        // Kích hoạt lại vai trò
                        roleBL.ToggleRoleAccountStatus(accountName, role.ID, true);
                    }
                    else if (!isChecked && existingRole != null && existingRole.Actived)
                    {
                        // Vô hiệu hóa vai trò
                        roleBL.ToggleRoleAccountStatus(accountName, role.ID, false);
                    }
                }

                MessageBox.Show("Cập nhật vai trò thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi cập nhật vai trò: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
