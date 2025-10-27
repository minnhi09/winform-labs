using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Lab_Basic_Command
{
    public partial class BillDetailForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=RestaurantManagement; integrated security=true;";
        private int billID;
        private bool isNewBill;

        // Constructor cho hóa đơn mới (isNewBill = true, id là TableID)
        // Constructor cho hóa đơn hiện có (isNewBill = false, id là BillID)
        public BillDetailForm(int id, bool isNewBill)
        {
            InitializeComponent();
            this.isNewBill = isNewBill;
            
            if (isNewBill)
            {
                CreateNewBill(id); // id là TableID
            }
            else
            {
                this.billID = id; // id là BillID
            }
        }

        private void BillDetailForm_Load(object sender, EventArgs e)
        {
            LoadAllFoods();
            
            if (!isNewBill)
            {
                LoadBillInfo();
                LoadBillDetails();
            }
            else
            {
                txtDiscount.Text = "0";
                UpdateTotalAmount();
            }
        }

        private void CreateNewBill(int tableID)
        {
            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                INSERT INTO Bill(TableID, AccountID, DateCheckIn, Status, TotalAmount, Discount)
                VALUES (@tableID, @accountID, GETDATE(), 0, 0, 0);
                SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";

            cmd.Parameters.AddWithValue("@tableID", tableID);
            cmd.Parameters.AddWithValue("@accountID", SessionManager.AccountID);

            conn.Open();
            billID = (int)cmd.ExecuteScalar();
            conn.Close();
        }

        private void LoadBillInfo()
        {
            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT 
                    b.ID,
                    t.Name AS TableName,
                    a.DisplayName AS AccountName,
                    b.DateCheckIn,
                    b.DateCheckOut,
                    b.Status,
                    b.TotalAmount,
                    b.Discount,
                    b.Notes
                FROM Bill b
                INNER JOIN [Table] t ON b.TableID = t.ID
                INNER JOIN Account a ON b.AccountID = a.ID
                WHERE b.ID = @billID
            ";

            cmd.Parameters.AddWithValue("@billID", billID);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                txtBillID.Text = reader["ID"].ToString();
                txtTable.Text = reader["TableName"].ToString();
                txtAccount.Text = reader["AccountName"].ToString();
                dtpCheckIn.Value = Convert.ToDateTime(reader["DateCheckIn"]);
                
                if (reader["DateCheckOut"] != DBNull.Value)
                {
                    dtpCheckOut.Value = Convert.ToDateTime(reader["DateCheckOut"]);
                }

                int status = Convert.ToInt32(reader["Status"]);
                cboStatus.SelectedIndex = status;

                txtDiscount.Text = reader["Discount"].ToString();
                txtNotes.Text = reader["Notes"].ToString();

                // Nếu đã thanh toán thì không cho chỉnh sửa
                if (status == 1)
                {
                    DisableEditingControls();
                }
            }

            reader.Close();
            conn.Close();
        }

        private void LoadBillDetails()
        {
            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                SELECT 
                    bd.ID,
                    bd.FoodID,
                    f.Name AS FoodName,
                    bd.Quantity,
                    bd.Price,
                    (bd.Quantity * bd.Price) AS Amount,
                    bd.Notes
                FROM BillDetail bd
                INNER JOIN Food f ON bd.FoodID = f.ID
                WHERE bd.BillID = @billID
            ";

            cmd.Parameters.AddWithValue("@billID", billID);

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            DisplayBillDetails(reader);

            reader.Close();
            conn.Close();

            UpdateTotalAmount();
        }

        private void DisplayBillDetails(SqlDataReader reader)
        {
            lvBillDetails.Items.Clear();
            decimal total = 0;

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["ID"].ToString());
                item.SubItems.Add(reader["FoodID"].ToString());
                item.SubItems.Add(reader["FoodName"].ToString());
                item.SubItems.Add(reader["Quantity"].ToString());
                item.SubItems.Add(Convert.ToDecimal(reader["Price"]).ToString("N0"));
                
                decimal amount = Convert.ToDecimal(reader["Amount"]);
                item.SubItems.Add(amount.ToString("N0"));
                item.SubItems.Add(reader["Notes"].ToString());

                total += amount;

                lvBillDetails.Items.Add(item);
            }
        }

        private void LoadAllFoods()
        {
            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT ID, Name, Price FROM Food ORDER BY Name";

            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            cboFood.Items.Clear();
            cboFood.Items.Add(new FoodItem { ID = 0, Name = "-- Chọn món --" });

            while (reader.Read())
            {
                cboFood.Items.Add(new FoodItem
                {
                    ID = Convert.ToInt32(reader["ID"]),
                    Name = reader["Name"].ToString(),
                    Price = Convert.ToDecimal(reader["Price"])
                });
            }

            reader.Close();
            conn.Close();

            cboFood.SelectedIndex = 0;
        }

        private void cboFood_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboFood.SelectedIndex > 0)
            {
                FoodItem food = (FoodItem)cboFood.SelectedItem;
                txtPrice.Text = food.Price.ToString("N0");
            }
            else
            {
                txtPrice.Text = "";
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            if (cboFood.SelectedIndex <= 0)
            {
                MessageBox.Show("Vui lòng chọn món ăn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quantity = 0;
            if (!int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return;
            }

            FoodItem food = (FoodItem)cboFood.SelectedItem;
            decimal price = food.Price;

            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                INSERT INTO BillDetail(BillID, FoodID, Quantity, Price, Notes)
                VALUES (@billID, @foodID, @quantity, @price, @notes)
            ";

            cmd.Parameters.AddWithValue("@billID", billID);
            cmd.Parameters.AddWithValue("@foodID", food.ID);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@price", price);
            cmd.Parameters.AddWithValue("@notes", txtDetailNotes.Text.Trim());

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();

            if (result > 0)
            {
                MessageBox.Show("Thêm món thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Reset form
                cboFood.SelectedIndex = 0;
                txtQuantity.Text = "1";
                txtPrice.Text = "";
                txtDetailNotes.Text = "";

                // Reload
                LoadBillDetails();
            }
        }

        private void btnUpdateFood_Click(object sender, EventArgs e)
        {
            if (lvBillDetails.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int quantity = 0;
            if (!int.TryParse(txtQuantity.Text, out quantity) || quantity <= 0)
            {
                MessageBox.Show("Vui lòng nhập số lượng hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQuantity.Focus();
                return;
            }

            int detailID = int.Parse(lvBillDetails.SelectedItems[0].SubItems[0].Text);

            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                UPDATE BillDetail
                SET Quantity = @quantity, Notes = @notes
                WHERE ID = @id
            ";

            cmd.Parameters.AddWithValue("@id", detailID);
            cmd.Parameters.AddWithValue("@quantity", quantity);
            cmd.Parameters.AddWithValue("@notes", txtDetailNotes.Text.Trim());

            conn.Open();
            int result = cmd.ExecuteNonQuery();
            conn.Close();

            if (result > 0)
            {
                MessageBox.Show("Cập nhật thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBillDetails();
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            if (lvBillDetails.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa món này?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            int detailID = int.Parse(lvBillDetails.SelectedItems[0].SubItems[0].Text);

            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM BillDetail WHERE ID = @id";
            cmd.Parameters.AddWithValue("@id", detailID);

            conn.Open();
            int deleteResult = cmd.ExecuteNonQuery();
            conn.Close();

            if (deleteResult > 0)
            {
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadBillDetails();
            }
        }

        private void lvBillDetails_Click(object sender, EventArgs e)
        {
            if (lvBillDetails.SelectedItems.Count > 0)
            {
                var item = lvBillDetails.SelectedItems[0];
                
                int foodID = int.Parse(item.SubItems[1].Text);
                
                // Tìm và chọn món trong ComboBox
                for (int i = 1; i < cboFood.Items.Count; i++)
                {
                    FoodItem food = (FoodItem)cboFood.Items[i];
                    if (food.ID == foodID)
                    {
                        cboFood.SelectedIndex = i;
                        break;
                    }
                }

                txtQuantity.Text = item.SubItems[3].Text;
                txtDetailNotes.Text = item.SubItems[6].Text;

                btnUpdateFood.Enabled = true;
                btnDeleteFood.Enabled = true;
            }
        }

        private void UpdateTotalAmount()
        {
            decimal totalAmount = 0;

            foreach (ListViewItem item in lvBillDetails.Items)
            {
                decimal amount = decimal.Parse(item.SubItems[5].Text.Replace(",", ""));
                totalAmount += amount;
            }

            txtTotalAmount.Text = totalAmount.ToString("N0");

            // Tính thành tiền sau giảm giá
            decimal discount = 0;
            decimal.TryParse(txtDiscount.Text, out discount);

            decimal finalAmount = totalAmount * (1 - discount / 100);
            txtFinalAmount.Text = finalAmount.ToString("N0");

            // Cập nhật vào database
            UpdateBillAmount(totalAmount, discount);
        }

        private void UpdateBillAmount(decimal totalAmount, decimal discount)
        {
            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                UPDATE Bill
                SET TotalAmount = @totalAmount, Discount = @discount
                WHERE ID = @billID
            ";

            cmd.Parameters.AddWithValue("@billID", billID);
            cmd.Parameters.AddWithValue("@totalAmount", totalAmount);
            cmd.Parameters.AddWithValue("@discount", discount);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            UpdateTotalAmount();
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            if (lvBillDetails.Items.Count == 0)
            {
                MessageBox.Show("Hóa đơn chưa có món nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Xác nhận thanh toán hóa đơn?\nThành tiền: {txtFinalAmount.Text} VNĐ",
                "Xác nhận thanh toán",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes) return;

            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = @"
                UPDATE Bill
                SET Status = 1, 
                    DateCheckOut = GETDATE(),
                    Notes = @notes
                WHERE ID = @billID
            ";

            cmd.Parameters.AddWithValue("@billID", billID);
            cmd.Parameters.AddWithValue("@notes", txtNotes.Text.Trim());

            conn.Open();
            int updateResult = cmd.ExecuteNonQuery();
            conn.Close();

            if (updateResult > 0)
            {
                MessageBox.Show("Thanh toán thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DisableEditingControls();
                cboStatus.SelectedIndex = 1;
                dtpCheckOut.Value = DateTime.Now;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng in hóa đơn đang được phát triển", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (isNewBill && lvBillDetails.Items.Count == 0)
            {
                // Xóa hóa đơn rỗng
                DialogResult result = MessageBox.Show(
                    "Hóa đơn chưa có món nào. Bạn có muốn hủy hóa đơn này?",
                    "Xác nhận",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    DeleteBill();
                }
            }

            this.Close();
        }

        private void DeleteBill()
        {
            SqlConnection conn = new SqlConnection(CONNECTION_STRING);
            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "DELETE FROM Bill WHERE ID = @billID";
            cmd.Parameters.AddWithValue("@billID", billID);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        private void DisableEditingControls()
        {
            cboFood.Enabled = false;
            txtQuantity.Enabled = false;
            txtDetailNotes.Enabled = false;
            txtDiscount.Enabled = false;
            txtNotes.Enabled = false;
            btnAddFood.Enabled = false;
            btnUpdateFood.Enabled = false;
            btnDeleteFood.Enabled = false;
            btnPayment.Enabled = false;
        }

        // Helper class
        private class FoodItem
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}
