using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using entity_framework.models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework
{
    public partial class frmFoodCategory : Form
    {
        private RestaurantContext _context;
        private List<FoodCategory> listCategory = new List<FoodCategory>();
        private FoodCategory categoryCurrent = new FoodCategory();

        public frmFoodCategory()
        {
            InitializeComponent();
            _context = new RestaurantContext();
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
        }

        private void frmFoodCategory_Load(object sender, EventArgs e)
        {
            LoadTypeComboBox();
            LoadCategoryDataToListView();
        }

        private void LoadTypeComboBox()
        {
            cboType.Items.Clear();
            cboType.Items.Add(new { Text = "Đồ ăn", Value = 1 });
            cboType.Items.Add(new { Text = "Thức uống", Value = 2 });
            cboType.DisplayMember = "Text";
            cboType.ValueMember = "Value";
            if (cboType.Items.Count > 0)
            {
                cboType.SelectedIndex = 0;
            }
        }

        private void LoadCategoryDataToListView()
        {
            try
            {
                lvFoodCategory.Items.Clear();
                listCategory = _context.FoodCategories.ToList();

                int stt = 1;
                foreach (FoodCategory category in listCategory)
                {
                    ListViewItem item = new ListViewItem(stt.ToString());
                    item.SubItems.Add(category.Name);
                    item.SubItems.Add(category.Type == 1 ? "Đồ ăn" : "Thức uống");

                    item.Tag = category;
                    lvFoodCategory.Items.Add(item);
                    stt++;
                }

                lblStatistics.Text = $"Thống kê: Tổng số {listCategory.Count} loại thực phẩm";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách loại thực phẩm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvFoodCategory_Click(object sender, EventArgs e)
        {
            if (lvFoodCategory.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvFoodCategory.SelectedItems[0];
                categoryCurrent = (FoodCategory)selectedItem.Tag;

                txtID.Text = categoryCurrent.ID.ToString();
                txtName.Text = categoryCurrent.Name;
                cboType.SelectedIndex = categoryCurrent.Type - 1;
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

                FoodCategory newCategory = new FoodCategory
                {
                    Name = txtName.Text.Trim(),
                    Type = ((dynamic)cboType.SelectedItem).Value
                };

                _context.FoodCategories.Add(newCategory);
                int result = _context.SaveChanges();

                if (result > 0)
                {
                    MessageBox.Show("Thêm loại thực phẩm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadCategoryDataToListView();
                    ClearForm();
                }
                else
                {
                    MessageBox.Show("Thêm loại thực phẩm thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show("Lỗi cập nhật database: " + ex.InnerException?.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm loại thực phẩm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên loại thực phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (cboType.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboType.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtName.Clear();
            if (cboType.Items.Count > 0)
            {
                cboType.SelectedIndex = 0;
            }
            categoryCurrent = new FoodCategory();
            txtName.Focus();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (categoryCurrent == null || categoryCurrent.ID == 0)
                {
                    MessageBox.Show("Vui lòng chọn loại thực phẩm cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Check if category has foods
                bool hasFoods = _context.Foods.Any(f => f.FoodCategoryID == categoryCurrent.ID);
                if (hasFoods)
                {
                    MessageBox.Show("Không thể xóa loại thực phẩm này vì có món ăn đang sử dụng!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult dialogResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa loại thực phẩm '{categoryCurrent.Name}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    var category = _context.FoodCategories.Find(categoryCurrent.ID);
                    if (category != null)
                    {
                        _context.FoodCategories.Remove(category);
                        int result = _context.SaveChanges();

                        if (result > 0)
                        {
                            MessageBox.Show("Xóa loại thực phẩm thành công!", "Thông báo",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                            LoadCategoryDataToListView();
                            ClearForm();
                        }
                        else
                        {
                            MessageBox.Show("Xóa loại thực phẩm thất bại!", "Lỗi",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Lỗi khi xóa loại thực phẩm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (categoryCurrent == null || categoryCurrent.ID == 0)
                {
                    MessageBox.Show("Vui lòng chọn loại thực phẩm cần cập nhật!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput())
                {
                    return;
                }

                var category = _context.FoodCategories.Find(categoryCurrent.ID);
                if (category != null)
                {
                    category.Name = txtName.Text.Trim();
                    category.Type = ((dynamic)cboType.SelectedItem).Value;

                    int result = _context.SaveChanges();

                    if (result > 0)
                    {
                        MessageBox.Show("Cập nhật loại thực phẩm thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadCategoryDataToListView();
                        ClearForm();
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật loại thực phẩm thất bại!", "Lỗi",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                MessageBox.Show("Lỗi khi cập nhật loại thực phẩm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
