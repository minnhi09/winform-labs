using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using entity_framework.models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework
{
    public partial class frmAssignRole : Form
    {
        private RestaurantContext _context;
        private readonly string accountName;

        public frmAssignRole(string accountName)
        {
            InitializeComponent();
            _context = new RestaurantContext();
            this.accountName = accountName;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
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
                var roles = _context.Roles.ToList();

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
                var accountRoles = _context.RoleAccounts
                    .Where(ra => ra.AccountName == accountName)
                    .ToList();

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
                var currentRoles = _context.RoleAccounts
                    .Where(ra => ra.AccountName == accountName)
                    .ToList();

                // Xử lý từng vai trò
                for (int i = 0; i < clbRoles.Items.Count; i++)
                {
                    Role role = (Role)clbRoles.Items[i];
                    bool isChecked = clbRoles.GetItemChecked(i);
                    var existingRole = currentRoles.FirstOrDefault(r => r.RoleID == role.ID);

                    if (isChecked && existingRole == null)
                    {
                        // Gán vai trò mới
                        var newRoleAccount = new RoleAccount
                        {
                            AccountName = accountName,
                            RoleID = role.ID,
                            Actived = true
                        };
                        _context.RoleAccounts.Add(newRoleAccount);
                    }
                    else if (isChecked && existingRole != null && !existingRole.Actived)
                    {
                        // Kích hoạt lại vai trò
                        existingRole.Actived = true;
                    }
                    else if (!isChecked && existingRole != null && existingRole.Actived)
                    {
                        // Vô hiệu hóa vai trò
                        existingRole.Actived = false;
                    }
                }

                _context.SaveChanges();

                MessageBox.Show("Cập nhật vai trò thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"Lỗi cập nhật vai trò: {ex.InnerException?.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
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
