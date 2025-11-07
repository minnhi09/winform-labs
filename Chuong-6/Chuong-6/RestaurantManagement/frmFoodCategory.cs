using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Chuong_6.DataAccess;
using Chuong_6.BusinessLogic;

namespace Chuong_6.RestaurantManagement
{
    public partial class frmFoodCategory : Form
    {
        List<Category> listCategory = new List<Category>();
        Category categoryCurrent = new Category();
        CategoryBL categoryBL = new CategoryBL();

        public frmFoodCategory()
        {
            InitializeComponent();
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
                listCategory = categoryBL.GetAll();

                int stt = 1;
                foreach (Category category in listCategory)
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
                categoryCurrent = (Category)selectedItem.Tag;

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

                Category newCategory = new Category
                {
                    Name = txtName.Text.Trim(),
                    Type = ((dynamic)cboType.SelectedItem).Value
                };

                int result = categoryBL.Insert(newCategory);

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
            categoryCurrent = new Category();
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

                DialogResult dialogResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa loại thực phẩm '{categoryCurrent.Name}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    int result = categoryBL.Delete(categoryCurrent.ID);

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

                categoryCurrent.Name = txtName.Text.Trim();
                categoryCurrent.Type = ((dynamic)cboType.SelectedItem).Value;

                int result = categoryBL.Update(categoryCurrent);

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
