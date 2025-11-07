using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Chuong_6.DataAccess;
using Chuong_6.BusinessLogic;

namespace Chuong_6.RestaurantManagement
{
    public partial class frmFood : Form
    {
        private readonly FoodBL foodBL;
        private readonly FoodCategoryBL categoryBL;

        public frmFood()
        {
            InitializeComponent();
            this.foodBL = new FoodBL();
            this.categoryBL = new FoodCategoryBL();
        }

        private void frmFood_Load(object sender, EventArgs e)
        {
            LoadCategory();
            LoadFoodDataToListView();
        }

        private void LoadCategory()
        {
            try
            {
                DataTable dt = categoryBL.GetAllCategories();

                cboFoodCategoryID.DataSource = dt;
                cboFoodCategoryID.DisplayMember = "Name";
                cboFoodCategoryID.ValueMember = "ID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh mục: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFoodDataToListView()
        {
            try
            {
                lvFood.Items.Clear();
                List<Food> foods = foodBL.GetAllFoods();

                int stt = 1;
                foreach (Food food in foods)
                {
                    ListViewItem item = new ListViewItem(stt.ToString());
                    item.SubItems.Add(food.Name);
                    item.SubItems.Add(food.Unit);
                    item.SubItems.Add(food.Price.ToString("N0"));
                    item.SubItems.Add(food.CategoryName ?? "");
                    item.SubItems.Add(food.Notes ?? "");

                    item.Tag = food.ID; // Lưu ID
                    lvFood.Items.Add(item);
                    stt++;
                }

                lblStatistics.Text = $"Thống kê: Tổng số {foods.Count} thực phẩm";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách thực phẩm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void lvFood_Click(object sender, EventArgs e)
        {
            if (lvFood.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = lvFood.SelectedItems[0];
                int foodID = Convert.ToInt32(selectedItem.Tag);

                try
                {
                    Food food = foodBL.GetFoodObject(foodID);
                    if (food != null)
                    {
                        txtID.Text = food.ID.ToString();
                        txtName.Text = food.Name;
                        txtUnit.Text = food.Unit;
                        txtPrice.Text = food.Price.ToString();
                        cboFoodCategoryID.SelectedValue = food.FoodCategoryID;
                        txtNotes.Text = food.Notes ?? "";
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

                string name = txtName.Text.Trim();
                string unit = txtUnit.Text.Trim();
                int categoryID = Convert.ToInt32(cboFoodCategoryID.SelectedValue);
                int price = int.Parse(txtPrice.Text.Trim());
                string notes = txtNotes.Text.Trim();

                int result = foodBL.AddFood(name, unit, categoryID, price,
                    string.IsNullOrWhiteSpace(notes) ? null : notes);

                if (result > 0)
                {
                    MessageBox.Show("Thêm thực phẩm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadFoodDataToListView();
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
                    MessageBox.Show("Vui lòng chọn thực phẩm cần cập nhật!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!ValidateInput())
                {
                    return;
                }

                int id = int.Parse(txtID.Text);
                string name = txtName.Text.Trim();
                string unit = txtUnit.Text.Trim();
                int categoryID = Convert.ToInt32(cboFoodCategoryID.SelectedValue);
                int price = int.Parse(txtPrice.Text.Trim());
                string notes = txtNotes.Text.Trim();

                int result = foodBL.UpdateFood(id, name, unit, categoryID, price,
                    string.IsNullOrWhiteSpace(notes) ? null : notes);

                if (result > 0)
                {
                    MessageBox.Show("Cập nhật thực phẩm thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadFoodDataToListView();
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
                    MessageBox.Show("Vui lòng chọn thực phẩm cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string foodName = txtName.Text;
                DialogResult dialogResult = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa thực phẩm '{foodName}' không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (dialogResult == DialogResult.Yes)
                {
                    int id = int.Parse(txtID.Text);
                    int result = foodBL.DeleteFood(id);

                    if (result > 0)
                    {
                        MessageBox.Show("Xóa thực phẩm thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadFoodDataToListView();
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
            this.Close();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập tên thực phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtUnit.Text))
            {
                MessageBox.Show("Vui lòng nhập đơn vị tính!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUnit.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPrice.Text))
            {
                MessageBox.Show("Vui lòng nhập giá!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            int price;
            if (!int.TryParse(txtPrice.Text.Trim(), out price) || price < 0)
            {
                MessageBox.Show("Giá phải là số nguyên dương!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrice.Focus();
                return false;
            }

            if (cboFoodCategoryID.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại thực phẩm!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboFoodCategoryID.Focus();
                return false;
            }

            return true;
        }

        private void ClearForm()
        {
            txtID.Clear();
            txtName.Clear();
            txtUnit.Clear();
            txtPrice.Clear();
            txtNotes.Clear();
            if (cboFoodCategoryID.Items.Count > 0)
            {
                cboFoodCategoryID.SelectedIndex = 0;
            }
            txtName.Focus();
        }
    }
}
