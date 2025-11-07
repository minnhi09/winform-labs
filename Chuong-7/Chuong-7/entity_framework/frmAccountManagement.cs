using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using entity_framework.models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework
{
    public partial class frmAccountManagement : Form
    {
        private RestaurantContext _context;
        private string currentUser;

        public frmAccountManagement()
        {
            InitializeComponent();
            _context = new RestaurantContext();
        }

        public frmAccountManagement(string username) : this()
        {
            this.currentUser = username;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
        }

        private void frmAccountManagement_Load(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void LoadAccounts()
        {
            try
            {
                listViewAccounts.Items.Clear();

                var accounts = _context.Accounts
                    .OrderBy(a => a.AccountName)
                    .ToList();

                foreach (var account in accounts)
                {
                    ListViewItem item = new ListViewItem(account.AccountName);
                    item.SubItems.Add(account.FullName);
                    item.SubItems.Add(account.Email ?? "");
                    item.SubItems.Add(account.Phone ?? "");
                    item.SubItems.Add(account.DateCreated.ToString("dd/MM/yyyy HH:mm"));
                    item.Tag = account;

                    listViewAccounts.Items.Add(item);
                }

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
            if (listViewAccounts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var account = (Account)listViewAccounts.SelectedItems[0].Tag;

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
            if (listViewAccounts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var account = (Account)listViewAccounts.SelectedItems[0].Tag;
            string accountName = account.AccountName;

            if (accountName.ToLower() == "admin")
            {
                MessageBox.Show("Không thể xóa tài khoản admin!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Check constraints
            bool hasInvoices = _context.Invoices.Any(i => i.AccountID == accountName);
            if (hasInvoices)
            {
                MessageBox.Show("Không thể xóa tài khoản này vì có hóa đơn liên quan!", "Cảnh báo",
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
                    // Delete role assignments first
                    var roleAccounts = _context.RoleAccounts.Where(ra => ra.AccountName == accountName);
                    _context.RoleAccounts.RemoveRange(roleAccounts);

                    // Delete account
                    _context.Accounts.Remove(account);
                    int affected = _context.SaveChanges();

                    if (affected > 0)
                    {
                        MessageBox.Show("Xóa tài khoản thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadAccounts();
                    }
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show($"Lỗi xóa tài khoản: {ex.InnerException?.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (listViewAccounts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var account = (Account)listViewAccounts.SelectedItems[0].Tag;
            string accountName = account.AccountName;

            DialogResult result = MessageBox.Show(
                $"Reset mật khẩu về '123456' cho tài khoản '{accountName}'?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    account.Password = "123456";
                    int affected = _context.SaveChanges();

                    if (affected > 0)
                    {
                        MessageBox.Show("Reset mật khẩu thành công!", "Thành công",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show($"Lỗi reset mật khẩu: {ex.InnerException?.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            if (listViewAccounts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var account = (Account)listViewAccounts.SelectedItems[0].Tag;
            string accountName = account.AccountName;
            frmAssignRole frm = new frmAssignRole(accountName);
            frm.ShowDialog();
        }

        private void btnViewRoles_Click(object sender, EventArgs e)
        {
            if (listViewAccounts.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn tài khoản!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var account = (Account)listViewAccounts.SelectedItems[0].Tag;
            string accountName = account.AccountName;

            try
            {
                var roles = _context.RoleAccounts
                    .Include(ra => ra.Role)
                    .Where(ra => ra.AccountName == accountName)
                    .ToList();

                if (roles.Count == 0)
                {
                    MessageBox.Show("Tài khoản chưa được gán vai trò nào!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string rolesText = $"Vai trò của tài khoản '{accountName}':\n\n";
                foreach (var roleAccount in roles)
                {
                    string status = roleAccount.Actived ? "Hoạt động" : "Vô hiệu hóa";
                    rolesText += $"• {roleAccount.Role.RoleName} - {status}\n";
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
                string keyword = txtSearch.Text.Trim().ToLower();

                listViewAccounts.Items.Clear();

                var accounts = _context.Accounts
                    .Where(a => a.AccountName.ToLower().Contains(keyword) ||
                               a.FullName.ToLower().Contains(keyword) ||
                               (a.Email != null && a.Email.ToLower().Contains(keyword)) ||
                               (a.Phone != null && a.Phone.Contains(keyword)))
                    .OrderBy(a => a.AccountName)
                    .ToList();

                foreach (var account in accounts)
                {
                    ListViewItem item = new ListViewItem(account.AccountName);
                    item.SubItems.Add(account.FullName);
                    item.SubItems.Add(account.Email ?? "");
                    item.SubItems.Add(account.Phone ?? "");
                    item.SubItems.Add(account.DateCreated.ToString("dd/MM/yyyy HH:mm"));
                    item.Tag = account;

                    listViewAccounts.Items.Add(item);
                }

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

        private void listViewAccounts_DoubleClick(object sender, EventArgs e)
        {
            if (listViewAccounts.SelectedItems.Count > 0)
            {
                btnEdit_Click(sender, e);
            }
        }
    }
}
