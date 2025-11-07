using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using entity_framework.models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework
{
    public partial class frmRole : Form
    {
        private RestaurantContext _context;

        public frmRole()
        {
            InitializeComponent();
            _context = new RestaurantContext();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
        }

        private void frmRole_Load(object sender, EventArgs e)
        {
            LoadRoleDataToListView();
        }

        private void LoadRoleDataToListView()
        {
            try
            {
                lvRole.Items.Clear();
                var roles = _context.Roles.ToList();

                int stt = 1;
                foreach (var role in roles)
                {
                    ListViewItem item = new ListViewItem(stt.ToString());
                    item.SubItems.Add(role.RoleName);
                    item.SubItems.Add(role.Path ?? "");
                    item.SubItems.Add(role.Notes ?? "");

                    item.Tag = role.ID;
                    lvRole.Items.Add(item);
                    stt++;
                }

                lblStatistics.Text = $"Thống kê: Tổng số {roles.Count} vai trò";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách vai trò: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvRole_Click(object sender, EventArgs e)
        {
            if (lvRole.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvRole.SelectedItems[0];
                int roleID = Convert.ToInt32(selectedItem.Tag);

                try
                {
                    var role = _context.Roles.Find(roleID);
                    if (role != null)
                    {
                        txtID.Text = role.ID.ToString();
                        txtRoleName.Text = role.RoleName;
                        txtPath.Text = role.Path ?? "";
                        txtNotes.Text = role.Notes ?? "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải thông tin: " + ex.Message, "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidateInput())
                {
                    return;
                }

                var newRole = new Role
                {
                    RoleName = txtRoleName.Text.Trim(),
                    Path = string.IsNullOrWhiteSpace(txtPath.Text) ? null : txtPath.Text.Trim(),
                    Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim()
                };

                _context.Roles.Add(newRole);
                int result = _context.SaveChanges();

                if (result > 0)
                {
                    MessageBox.Show("Thêm vai trò thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRoleDataToListView();
                    ClearForm();
                }
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Lỗi cập nhật database: " + ex.InnerException?.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Vui lòng chọn vai trò cần cập nhật!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput())
                {
                    return;
                }

                int id = int.Parse(txtID.Text);
                var role = _context.Roles.Find(id);

                if (role != null)
                {
                    role.RoleName = txtRoleName.Text.Trim();
                    role.Path = string.IsNullOrWhiteSpace(txtPath.Text) ? null : txtPath.Text.Trim();
                    role.Notes = string.IsNullOrWhiteSpace(txtNotes.Text) ? null : txtNotes.Text.Trim();

                    int result = _context.SaveChanges();

                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật vai trò thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRoleDataToListView();
                        ClearForm();
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Lỗi cập nhật database: " + ex.InnerException?.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtID.Text))
                {
                    MessageBox.Show("Vui lòng chọn vai trò cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(txtID.Text);

                // Check if role has users assigned
                bool hasUsers = _context.RoleAccounts.Any(ra => ra.RoleID == id);
                if (hasUsers)
                {
                    MessageBox.Show("Không thể xóa vai trò này vì có người dùng đang sử dụng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string roleName = txtRoleName.Text;
                DialogResult dialogResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa vai trò '{roleName}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    var role = _context.Roles.Find(id);
                    if (role != null)
                    {
                        _context.Roles.Remove(role);
                        int result = _context.SaveChanges();

                        if (result > 0)
                        {
                            MessageBox.Show("Xóa vai trò thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRoleDataToListView();
                            ClearForm();
                        }
                    }
                }
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Lỗi cập nhật database: " + ex.InnerException?.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát không?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtRoleName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên vai trò!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRoleName.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtRoleName.Clear();
            txtPath.Clear();
            txtNotes.Clear();
            txtRoleName.Focus();
        }
    }
}
