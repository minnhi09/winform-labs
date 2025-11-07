using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Chuong_6.DataAccess;
using Chuong_6.BusinessLogic;

namespace Chuong_6.RestaurantManagement
{
    public partial class frmRole : Form
    {
        private readonly RoleBL roleBL;

        public frmRole()
        {
            InitializeComponent();
            this.roleBL = new RoleBL();
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
                List<Role> roles = roleBL.GetAllRoles();

                int stt = 1;
                foreach (Role role in roles)
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
                    Role role = roleBL.GetRoleObject(roleID);
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

                string roleName = txtRoleName.Text.Trim();
                string path = txtPath.Text.Trim();
                string notes = txtNotes.Text.Trim();

                int result = roleBL.AddRole(roleName,
                    string.IsNullOrWhiteSpace(path) ? null : path,
                    string.IsNullOrWhiteSpace(notes) ? null : notes);

                if (result > 0)
                {
                    MessageBox.Show("Thêm vai trò thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRoleDataToListView();
                    ClearForm();
                }
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
                string roleName = txtRoleName.Text.Trim();
                string path = txtPath.Text.Trim();
                string notes = txtNotes.Text.Trim();

                int result = roleBL.UpdateRole(id, roleName,
                    string.IsNullOrWhiteSpace(path) ? null : path,
                    string.IsNullOrWhiteSpace(notes) ? null : notes);

                if (result > 0)
                {
                    MessageBox.Show("Cập nhật vai trò thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRoleDataToListView();
                    ClearForm();
                }
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

                string roleName = txtRoleName.Text;
                DialogResult dialogResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa vai trò '{roleName}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    int id = int.Parse(txtID.Text);
                    int result = roleBL.DeleteRole(id);

                    if (result > 0)
                    {
                        MessageBox.Show("Xóa vai trò thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRoleDataToListView();
                        ClearForm();
                    }
                }
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
