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
    public partial class TableBillHistoryForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=RestaurantManagement; integrated security=true;";
        private int tableID;
        private string tableName;

        public TableBillHistoryForm(int tableID, string tableName)
        {
            InitializeComponent();
            this.tableID = tableID;
            this.tableName = tableName;
        }

        private void TableBillHistoryForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Nhật ký hóa đơn - Bàn: {tableName}";
            LoadBillHistory();
        }

        private void LoadBillHistory()
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Lấy danh sách tất cả hóa đơn của bàn này
            sqlCommand.CommandText = @"
                SELECT 
                    b.ID,
                    b.DateCheckIn,
                    a.DisplayName,
                    b.TotalAmount,
                    b.Discount,
                    b.TotalAmount * (1 - b.Discount / 100.0) AS FinalAmount
                FROM Bill b
                INNER JOIN Account a ON b.AccountID = a.ID
                WHERE b.TableID = @tableID
                ORDER BY b.DateCheckIn DESC;
            ";

            sqlCommand.Parameters.AddWithValue("@tableID", tableID);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                lvBills.Items.Clear();

                int billCount = 0;
                decimal totalAmount = 0;
                decimal totalDiscount = 0;
                decimal totalFinalAmount = 0;

                while (reader.Read())
                {
                    billCount++;

                    ListViewItem item = new ListViewItem(reader["ID"].ToString());

                    // Ngày lập
                    DateTime dateCheckIn = reader["DateCheckIn"] != DBNull.Value ? 
                        (DateTime)reader["DateCheckIn"] : DateTime.MinValue;
                    item.SubItems.Add(dateCheckIn.ToString("dd/MM/yyyy HH:mm:ss"));

                    // Tên nhân viên
                    item.SubItems.Add(reader["DisplayName"].ToString());

                    // Số tiền trước giảm giá
                    decimal billTotal = reader["TotalAmount"] != DBNull.Value ? 
                        Convert.ToDecimal(reader["TotalAmount"]) : 0;
                    item.SubItems.Add(billTotal.ToString("#,##0"));

                    // Số tiền giảm giá (phần trăm và số tiền)
                    decimal discountPercent = reader["Discount"] != DBNull.Value ? 
                        Convert.ToDecimal(reader["Discount"]) : 0;
                    decimal discountAmount = billTotal * discountPercent / 100;
                    item.SubItems.Add($"{discountAmount.ToString("#,##0")} ({discountPercent}%)");

                    // Số tiền thực thu
                    decimal finalAmount = reader["FinalAmount"] != DBNull.Value ? 
                        Convert.ToDecimal(reader["FinalAmount"]) : 0;
                    item.SubItems.Add(finalAmount.ToString("#,##0"));

                    lvBills.Items.Add(item);

                    // Tính tổng
                    totalAmount += billTotal;
                    totalDiscount += discountAmount;
                    totalFinalAmount += finalAmount;
                }

                reader.Close();

                // Hiển thị thống kê
                lblBillCount.Text = billCount.ToString();
                lblTotalAmount.Text = totalAmount.ToString("#,##0");
                lblDiscount.Text = totalDiscount.ToString("#,##0");
                lblFinalAmount.Text = totalFinalAmount.ToString("#,##0");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải nhật ký hóa đơn: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void lvBills_DoubleClick(object sender, EventArgs e)
        {
            if (lvBills.SelectedItems.Count == 0) return;

            int billID = int.Parse(lvBills.SelectedItems[0].SubItems[0].Text);

            // Mở form chi tiết hóa đơn
            BillDetailForm detailForm = new BillDetailForm(billID, false);
            detailForm.ShowDialog();

            // Reload lại danh sách sau khi đóng form chi tiết
            LoadBillHistory();
        }
    }
}
