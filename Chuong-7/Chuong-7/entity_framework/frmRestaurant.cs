using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using entity_framework.models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework
{
    public partial class frmRestaurant : Form
    {
        private RestaurantContext _context;

        public frmRestaurant()
        {
            InitializeComponent();
            _context = new RestaurantContext();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
        }

        private void frmRestaurant_Load(object sender, EventArgs e)
        {
            LoadRestaurantDataToListView();
        }

        private void LoadRestaurantDataToListView()
        {
            try
            {
                lvRestaurant.Items.Clear();
                var restaurants = _context.Restaurants.ToList();

                int stt = 1;
                foreach (var restaurant in restaurants)
                {
                    ListViewItem item = new ListViewItem(stt.ToString());
                    item.SubItems.Add(restaurant.Name);
                    item.SubItems.Add(restaurant.Address);
                    item.SubItems.Add(restaurant.Phone);
                    item.SubItems.Add(restaurant.Website ?? "");

                    item.Tag = restaurant.ID;
                    lvRestaurant.Items.Add(item);
                    stt++;
                }

                lblStatistics.Text = $"Thống kê: Tổng số {restaurants.Count} nhà hàng";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách nhà hàng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvRestaurant_Click(object sender, EventArgs e)
        {
            if (lvRestaurant.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvRestaurant.SelectedItems[0];
                int restaurantID = Convert.ToInt32(selectedItem.Tag);

                try
                {
                    var restaurant = _context.Restaurants.Find(restaurantID);
                    if (restaurant != null)
                    {
                        txtID.Text = restaurant.ID.ToString();
                        txtName.Text = restaurant.Name;
                        txtAddress.Text = restaurant.Address;
                        txtPhone.Text = restaurant.Phone;
                        txtWebsite.Text = restaurant.Website ?? "";
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

                var newRestaurant = new Restaurant
                {
                    Name = txtName.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Phone = txtPhone.Text.Trim(),
                    Website = string.IsNullOrWhiteSpace(txtWebsite.Text) ? null : txtWebsite.Text.Trim()
                };

                _context.Restaurants.Add(newRestaurant);
                int result = _context.SaveChanges();

                if (result > 0)
                {
                    MessageBox.Show("Thêm nhà hàng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRestaurantDataToListView();
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
                    MessageBox.Show("Vui lòng chọn nhà hàng cần cập nhật!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput())
                {
                    return;
                }

                int id = int.Parse(txtID.Text);
                var restaurant = _context.Restaurants.Find(id);

                if (restaurant != null)
                {
                    restaurant.Name = txtName.Text.Trim();
                    restaurant.Address = txtAddress.Text.Trim();
                    restaurant.Phone = txtPhone.Text.Trim();
                    restaurant.Website = string.IsNullOrWhiteSpace(txtWebsite.Text) ? null : txtWebsite.Text.Trim();

                    int result = _context.SaveChanges();

                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật nhà hàng thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadRestaurantDataToListView();
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
                    MessageBox.Show("Vui lòng chọn nhà hàng cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = int.Parse(txtID.Text);

                // Check if restaurant has halls
                bool hasHalls = _context.Halls.Any(h => h.RestaurantID == id);
                if (hasHalls)
                {
                    MessageBox.Show("Không thể xóa nhà hàng này vì có sảnh đang sử dụng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string restaurantName = txtName.Text;
                DialogResult dialogResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa nhà hàng '{restaurantName}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    var restaurant = _context.Restaurants.Find(id);
                    if (restaurant != null)
                    {
                        _context.Restaurants.Remove(restaurant);
                        int result = _context.SaveChanges();

                        if (result > 0)
                        {
                            MessageBox.Show("Xóa nhà hàng thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadRestaurantDataToListView();
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
                MessageBox.Show("Vui lòng nhập tên nhà hàng!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAddress.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPhone.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtName.Clear();
            txtAddress.Clear();
            txtPhone.Clear();
            txtWebsite.Clear();
            txtName.Focus();
        }
    }
}
