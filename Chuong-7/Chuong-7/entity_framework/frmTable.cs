using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using entity_framework.models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework
{
    public partial class frmTable : Form
    {
        private RestaurantContext _context;

        public frmTable()
        {
            InitializeComponent();
            _context = new RestaurantContext();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
        }

        private void frmTable_Load(object sender, EventArgs e)
        {
            LoadHall();
            LoadStatus();
            LoadTableDataToListView();
        }

        private void LoadHall()
        {
            try
            {
                var halls = _context.Halls
                    .Include(h => h.Restaurant)
                    .OrderBy(h => h.Name)
                    .ToList();

                cboHallID.DataSource = halls;
                cboHallID.DisplayMember = "Name";
                cboHallID.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải sảnh: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStatus()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add(new { Value = 0, Text = "Trống" });
            cboStatus.Items.Add(new { Value = 1, Text = "Đã đặt" });
            cboStatus.Items.Add(new { Value = 2, Text = "Có khách" });
            cboStatus.DisplayMember = "Text";
            cboStatus.ValueMember = "Value";
            cboStatus.SelectedIndex = 0;
        }

        private void LoadTableDataToListView()
        {
            try
            {
                lvTable.Items.Clear();
                var tables = _context.Tables
                    .Include(t => t.Hall)
                    .ThenInclude(h => h.Restaurant)
                    .ToList();

                int stt = 1;
                foreach (var table in tables)
                {
                    ListViewItem item = new ListViewItem(stt.ToString());
                    item.SubItems.Add(table.TableCode);
                    item.SubItems.Add(table.Name ?? "");
                    item.SubItems.Add(table.Seats?.ToString() ?? "");

                    // Status text
                    string statusText = table.Status == 0 ? "Trống" :
                                       table.Status == 1 ? "Đã đặt" : "Có khách";
                    item.SubItems.Add(statusText);

                    item.SubItems.Add(table.Hall?.Name ?? "");
                    item.SubItems.Add(table.Hall?.Restaurant?.Name ?? "");

                    item.Tag = table.ID;
                    lvTable.Items.Add(item);
                    stt++;
                }

                lblStatistics.Text = $"Thống kê: Tổng số {tables.Count} bàn";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bàn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvTable_Click(object sender, EventArgs e)
        {
            if (lvTable.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvTable.SelectedItems[0];
                int tableID = Convert.ToInt32(selectedItem.Tag);

                try
                {
                    var table = _context.Tables.Find(tableID);
                    if (table != null)
                    {
                        txtID.Text = table.ID.ToString();
                        txtTableCode.Text = table.TableCode;
                        txtName.Text = table.Name ?? "";
                        txtSeats.Text = table.Seats?.ToString() ?? "";
                        cboStatus.SelectedIndex = table.Status;
                        cboHallID.SelectedValue = table.HallID;
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

                var newTable = new Table
                {
                    TableCode = txtTableCode.Text.Trim(),
                    Name = string.IsNullOrWhiteSpace(txtName.Text) ? null : txtName.Text.Trim(),
                    Status = Convert.ToInt32(((dynamic)cboStatus.SelectedItem).Value),
                    Seats = string.IsNullOrWhiteSpace(txtSeats.Text) ? (int?)null : int.Parse(txtSeats.Text.Trim()),
                    HallID = Convert.ToInt32(cboHallID.SelectedValue)
                };

                _context.Tables.Add(newTable);
                int result = _context.SaveChanges();

                if (result > 0)
                {
                    MessageBox.Show("Thêm bàn thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadTableDataToListView();
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
                    MessageBox.Show("Vui lòng chọn bàn cần cập nhật!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput())
                {
                    return;
                }

                int id = int.Parse(txtID.Text);
                var table = _context.Tables.Find(id);

                if (table != null)
                {
                    table.TableCode = txtTableCode.Text.Trim();
                    table.Name = string.IsNullOrWhiteSpace(txtName.Text) ? null : txtName.Text.Trim();
                    table.Status = Convert.ToInt32(((dynamic)cboStatus.SelectedItem).Value);
                    table.Seats = string.IsNullOrWhiteSpace(txtSeats.Text) ? (int?)null : int.Parse(txtSeats.Text.Trim());
                    table.HallID = Convert.ToInt32(cboHallID.SelectedValue);

                    int result = _context.SaveChanges();

                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật bàn thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadTableDataToListView();
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
                    MessageBox.Show("Vui lòng chọn bàn cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(txtID.Text);

                // Check if table has invoices
                bool hasInvoices = _context.Invoices.Any(i => i.TableID == id);
                if (hasInvoices)
                {
                    MessageBox.Show("Không thể xóa bàn này vì có hóa đơn đang sử dụng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string tableCode = txtTableCode.Text;
                DialogResult dialogResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa bàn '{tableCode}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    var table = _context.Tables.Find(id);
                    if (table != null)
                    {
                        _context.Tables.Remove(table);
                        int result = _context.SaveChanges();

                        if (result > 0)
                        {
                            MessageBox.Show("Xóa bàn thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadTableDataToListView();
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
            if (string.IsNullOrWhiteSpace(txtTableCode.Text))
            {
                MessageBox.Show("Vui lòng nhập mã bàn!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTableCode.Focus();
                return false;
            }

            if (!string.IsNullOrWhiteSpace(txtSeats.Text))
            {
                if (!int.TryParse(txtSeats.Text.Trim(), out int seats) || seats <= 0)
                {
                    MessageBox.Show("Số chỗ ngồi phải là số nguyên dương!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSeats.Focus();
                    return false;
                }
            }

            if (cboHallID.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn sảnh!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboHallID.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtTableCode.Clear();
            txtName.Clear();
            txtSeats.Clear();
            cboStatus.SelectedIndex = 0;
            if (cboHallID.Items.Count > 0)
            {
                cboHallID.SelectedIndex = 0;
            }
            txtTableCode.Focus();
        }
    }
}
