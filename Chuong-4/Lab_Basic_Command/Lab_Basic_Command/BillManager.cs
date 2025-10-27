using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Lab_Basic_Command
{
    public partial class BillManager : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=RestaurantManagement; integrated security=true;";

        public BillManager()
        {
            InitializeComponent();
        }

        private void BillManager_Load(object sender, EventArgs e)
        {
            // Load danh sách bàn vào ComboBox
            LoadTables();
            
            // Load danh sách hóa đơn
            btnLoad.PerformClick();
        }

        private void LoadTables()
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = "SELECT ID, Name FROM [Table] ORDER BY Name;";

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                cboTable.Items.Clear();
                cboTable.Items.Add(new TableItem { ID = 0, Name = "-- Tất cả --" });

                while (reader.Read())
                {
                    var tableItem = new TableItem
                    {
                        ID = (int)reader["ID"],
                        Name = reader["Name"].ToString()
                    };
                    cboTable.Items.Add(tableItem);
                }

                if (cboTable.Items.Count > 0)
                {
                    cboTable.SelectedIndex = 0;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bàn: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Lọc theo bàn và trạng thái
            string whereClause = "WHERE 1=1";
            
            // Lọc theo bàn
            if (cboTable.SelectedIndex > 0)
            {
                TableItem selectedTable = (TableItem)cboTable.SelectedItem;
                whereClause += $" AND b.TableID = {selectedTable.ID}";
            }

            // Lọc theo trạng thái
            if (cboStatus.SelectedIndex > 0)
            {
                int status = cboStatus.SelectedIndex - 1; // 0: Chưa thanh toán, 1: Đã thanh toán
                whereClause += $" AND b.Status = {status}";
            }

            // Lọc theo ngày
            if (chkFilterDate.Checked)
            {
                whereClause += $" AND CAST(b.DateCheckIn AS DATE) BETWEEN '{dtpFrom.Value:yyyy-MM-dd}' AND '{dtpTo.Value:yyyy-MM-dd}'";
            }

            sqlCommand.CommandText = $@"
                SELECT 
                    b.ID,
                    t.Name AS TableName,
                    a.DisplayName AS AccountName,
                    b.DateCheckIn,
                    b.DateCheckOut,
                    CASE b.Status 
                        WHEN 0 THEN N'Chưa thanh toán'
                        WHEN 1 THEN N'Đã thanh toán'
                    END AS StatusText,
                    b.TotalAmount,
                    b.Discount,
                    b.TotalAmount * (1 - b.Discount / 100) AS FinalAmount
                FROM Bill b
                INNER JOIN [Table] t ON b.TableID = t.ID
                INNER JOIN Account a ON b.AccountID = a.ID
                {whereClause}
                ORDER BY b.DateCheckIn DESC;
            ";

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                DisplayBills(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void DisplayBills(SqlDataReader reader)
        {
            lvBills.Items.Clear();

            decimal totalAmount = 0;
            decimal totalDiscount = 0;
            decimal finalAmount = 0;

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["ID"].ToString());
                item.SubItems.Add(reader["TableName"].ToString());
                item.SubItems.Add(reader["AccountName"].ToString());
                item.SubItems.Add(Convert.ToDateTime(reader["DateCheckIn"]).ToString("dd/MM/yyyy HH:mm"));
                item.SubItems.Add(reader["DateCheckOut"] != DBNull.Value 
                    ? Convert.ToDateTime(reader["DateCheckOut"]).ToString("dd/MM/yyyy HH:mm") 
                    : "");
                item.SubItems.Add(reader["StatusText"].ToString());
                
                decimal billTotal = Convert.ToDecimal(reader["TotalAmount"]);
                decimal discount = Convert.ToDecimal(reader["Discount"]);
                decimal billFinal = Convert.ToDecimal(reader["FinalAmount"]);
                
                item.SubItems.Add(billTotal.ToString("N0"));
                item.SubItems.Add(discount.ToString());
                item.SubItems.Add(billFinal.ToString("N0"));

                // Tính tổng
                totalAmount += billTotal;
                decimal discountAmount = billTotal * (discount / 100);
                totalDiscount += discountAmount;
                finalAmount += billFinal;

                lvBills.Items.Add(item);
            }

            // Hiển thị thống kê
            UpdateStatistics(totalAmount, totalDiscount, finalAmount);
        }

        private void UpdateStatistics(decimal totalAmount, decimal totalDiscount, decimal finalAmount)
        {
            lblStatTotalAmount.Text = totalAmount.ToString("N0") + " VNĐ";
            lblStatDiscount.Text = totalDiscount.ToString("N0") + " VNĐ";
            lblStatFinalAmount.Text = finalAmount.ToString("N0") + " VNĐ";
        }

        private void lvBills_Click(object sender, EventArgs e)
        {
            if (lvBills.SelectedItems.Count == 0) return;

            var item = lvBills.SelectedItems[0];
            int billID = int.Parse(item.SubItems[0].Text);

            LoadBillDetails(billID);
            btnViewDetail.Enabled = true;
        }

        private void LoadBillDetails(int billID)
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            sqlCommand.CommandText = @"
                SELECT 
                    bd.ID,
                    f.Name AS FoodName,
                    bd.Quantity,
                    bd.Price,
                    bd.Quantity * bd.Price AS Amount,
                    bd.Notes
                FROM BillDetail bd
                INNER JOIN Food f ON bd.FoodID = f.ID
                WHERE bd.BillID = @billID;
            ";

            sqlCommand.Parameters.AddWithValue("@billID", billID);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                DisplayBillDetails(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết hóa đơn: " + ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void DisplayBillDetails(SqlDataReader reader)
        {
            lvBillDetails.Items.Clear();

            decimal totalAmount = 0;

            while (reader.Read())
            {
                ListViewItem item = new ListViewItem(reader["ID"].ToString());
                item.SubItems.Add(reader["FoodName"].ToString());
                item.SubItems.Add(reader["Quantity"].ToString());
                item.SubItems.Add(Convert.ToDecimal(reader["Price"]).ToString("N0"));
                item.SubItems.Add(Convert.ToDecimal(reader["Amount"]).ToString("N0"));
                item.SubItems.Add(reader["Notes"] != DBNull.Value ? reader["Notes"].ToString() : "");

                lvBillDetails.Items.Add(item);

                totalAmount += Convert.ToDecimal(reader["Amount"]);
            }

            // Hiển thị tổng tiền
            lblTotalAmount.Text = $"Tổng tiền: {totalAmount:N0} VNĐ";
        }

        private void btnViewDetail_Click(object sender, EventArgs e)
        {
            if (lvBills.SelectedItems.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xem chi tiết");
                return;
            }

            var item = lvBills.SelectedItems[0];
            int billID = int.Parse(item.SubItems[0].Text);

            BillDetailForm detailForm = new BillDetailForm(billID, false); // false = xem hóa đơn hiện có
            detailForm.ShowDialog();

            // Reload sau khi đóng form chi tiết
            btnLoad.PerformClick();
        }

        private void tsmViewDetail_Click(object sender, EventArgs e)
        {
            btnViewDetail.PerformClick();
        }

        private void lvBills_DoubleClick(object sender, EventArgs e)
        {
            // Mở form chi tiết khi double click vào hóa đơn
            btnViewDetail.PerformClick();
        }

        private void chkFilterDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.Enabled = chkFilterDate.Checked;
            dtpTo.Enabled = chkFilterDate.Checked;
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            cboTable.SelectedIndex = 0;
            cboStatus.SelectedIndex = 0;
            chkFilterDate.Checked = false;
            dtpFrom.Value = DateTime.Now.AddMonths(-1);
            dtpTo.Value = DateTime.Now;
            btnLoad.PerformClick();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (lvBills.Items.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất");
                return;
            }

            MessageBox.Show("Chức năng xuất báo cáo đang được phát triển", "Thông báo");
        }

        private void btnNewBill_Click(object sender, EventArgs e)
        {
            // Hiển thị form chọn bàn để tạo hóa đơn mới
            TableSelectionForm tableSelectionForm = new TableSelectionForm();
            if (tableSelectionForm.ShowDialog() == DialogResult.OK)
            {
                int selectedTableID = tableSelectionForm.SelectedTableID;
                
                // Mở form chi tiết hóa đơn với hóa đơn mới
                BillDetailForm detailForm = new BillDetailForm(selectedTableID, true); // true = tạo hóa đơn mới
                detailForm.ShowDialog();

                // Reload danh sách sau khi đóng form
                btnLoad.PerformClick();
            }
        }
    }

    // Helper class cho ComboBox
    public class TableItem
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
