using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using entity_framework.models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework
{
    public partial class frmHall : Form
    {
        private RestaurantContext _context;

        public frmHall()
        {
            InitializeComponent();
            _context = new RestaurantContext();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
        }

        private void frmHall_Load(object sender, EventArgs e)
        {
            LoadRestaurant();
            LoadHallDataToListView();
        }

        private void LoadRestaurant()
        {
            try
            {
                var restaurants = _context.Restaurants
                    .OrderBy(r => r.Name)
                    .ToList();

                cboRestaurantID.DataSource = restaurants;
                cboRestaurantID.DisplayMember = "Name";
                cboRestaurantID.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải nhà hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadHallDataToListView()
        {
            try
            {
                lvHall.Items.Clear();
                var halls = _context.Halls
                    .Include(h => h.Restaurant)
                    .ToList();

                int stt = 1;
                foreach (var hall in halls)
                {
                    ListViewItem item = new ListViewItem(stt.ToString());
                    item.SubItems.Add(hall.Name);
                    item.SubItems.Add(hall.Restaurant?.Name ?? "");

                    item.Tag = hall.ID;
                    lvHall.Items.Add(item);
                    stt++;
                }

                lblStatistics.Text = $"Thống kê: Tổng số {halls.Count} sảnh";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách sảnh: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvHall_Click(object sender, EventArgs e)
        {
            if (lvHall.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvHall.SelectedItems[0];
                int hallID = Convert.ToInt32(selectedItem.Tag);

                try
                {
                    var hall = _context.Halls.Find(hallID);
                    if (hall != null)
                    {
                        txtID.Text = hall.ID.ToString();
                        txtName.Text = hall.Name;
                        cboRestaurantID.SelectedValue = hall.RestaurantID;
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

                var newHall = new Hall
                {
                    Name = txtName.Text.Trim(),
                    RestaurantID = Convert.ToInt32(cboRestaurantID.SelectedValue)
                };

                _context.Halls.Add(newHall);
                int result = _context.SaveChanges();

                if (result > 0)
                {
                    MessageBox.Show("Thêm sảnh thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadHallDataToListView();
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
                    MessageBox.Show("Vui lòng chọn sảnh cần cập nhật!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput())
                {
                    return;
                }

                int id = int.Parse(txtID.Text);
                var hall = _context.Halls.Find(id);

                if (hall != null)
                {
                    hall.Name = txtName.Text.Trim();
                    hall.RestaurantID = Convert.ToInt32(cboRestaurantID.SelectedValue);

                    int result = _context.SaveChanges();

                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật sảnh thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadHallDataToListView();
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
                    MessageBox.Show("Vui lòng chọn sảnh cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(txtID.Text);

                // Check if hall has tables
                bool hasTables = _context.Tables.Any(t => t.HallID == id);
                if (hasTables)
                {
                    MessageBox.Show("Không thể xóa sảnh này vì có bàn đang sử dụng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string hallName = txtName.Text;
                DialogResult dialogResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa sảnh '{hallName}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    var hall = _context.Halls.Find(id);
                    if (hall != null)
                    {
                        _context.Halls.Remove(hall);
                        int result = _context.SaveChanges();

                        if (result > 0)
                        {
                            MessageBox.Show("Xóa sảnh thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadHallDataToListView();
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
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên sảnh!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (cboRestaurantID.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn nhà hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboRestaurantID.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtName.Clear();
            if (cboRestaurantID.Items.Count > 0)
            {
                cboRestaurantID.SelectedIndex = 0;
            }
            txtName.Focus();
        }
    }
}
