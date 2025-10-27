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
    public partial class TableBillSummaryForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=RestaurantManagement; integrated security=true;";
        private int tableID;
        private string tableName;

        public TableBillSummaryForm(int tableID, string tableName)
        {
            InitializeComponent();
            this.tableID = tableID;
            this.tableName = tableName;
        }

        private void TableBillSummaryForm_Load(object sender, EventArgs e)
        {
            this.Text = $"Danh mục hóa đơn - Bàn: {tableName}";
            LoadBillDates();
        }

        private void LoadBillDates()
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Lấy danh sách các ngày có hóa đơn của bàn này
            sqlCommand.CommandText = @"
                SELECT DISTINCT CONVERT(DATE, DateCheckIn) AS BillDate
                FROM Bill
                WHERE TableID = @tableID AND DateCheckIn IS NOT NULL
                ORDER BY BillDate DESC;
            ";

            sqlCommand.Parameters.AddWithValue("@tableID", tableID);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                lbDates.Items.Clear();

                while (reader.Read())
                {
                    if (reader["BillDate"] != DBNull.Value)
                    {
                        DateTime date = (DateTime)reader["BillDate"];
                        lbDates.Items.Add(new DateItem
                        {
                            Date = date,
                            DisplayText = date.ToString("dd/MM/yyyy")
                        });
                    }
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách ngày: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        private void lbDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbDates.SelectedItem == null) return;

            DateItem selectedDate = (DateItem)lbDates.SelectedItem;
            LoadBillSummary(selectedDate.Date);
        }

        private void LoadBillSummary(DateTime date)
        {
            SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
            SqlCommand sqlCommand = sqlConnection.CreateCommand();

            // Lấy thống kê sản phẩm từ các hóa đơn trong ngày
            // Lưu ý: Discount ở cấp Bill (giảm giá cho toàn hóa đơn), không phải từng sản phẩm
            sqlCommand.CommandText = @"
                SELECT 
                    f.Name AS FoodName,
                    c.Name AS CategoryName,
                    SUM(bd.Quantity) AS TotalQuantity,
                    SUM(bd.Quantity * bd.Price) AS TotalAmount
                FROM Bill b
                INNER JOIN BillDetail bd ON b.ID = bd.BillID
                INNER JOIN Food f ON bd.FoodID = f.ID
                INNER JOIN Category c ON f.FoodCategoryID = c.ID
                WHERE b.TableID = @tableID 
                    AND CONVERT(DATE, b.DateCheckIn) = @date
                GROUP BY f.Name, c.Name
                ORDER BY f.Name;
            ";

            sqlCommand.Parameters.AddWithValue("@tableID", tableID);
            sqlCommand.Parameters.AddWithValue("@date", date.Date);

            try
            {
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                lvSummary.Items.Clear();

                decimal grandTotalAmount = 0;

                while (reader.Read())
                {
                    ListViewItem item = new ListViewItem(reader["FoodName"].ToString());
                    item.SubItems.Add(reader["CategoryName"].ToString());
                    item.SubItems.Add(reader["TotalQuantity"].ToString());

                    decimal totalAmount = reader["TotalAmount"] != DBNull.Value ? 
                        Convert.ToDecimal(reader["TotalAmount"]) : 0;

                    item.SubItems.Add(totalAmount.ToString("#,##0"));
                    item.SubItems.Add("-"); // Không có giảm giá ở cấp sản phẩm
                    item.SubItems.Add(totalAmount.ToString("#,##0")); // Thực thu = Tổng tiền (giảm giá ở cấp hóa đơn)

                    lvSummary.Items.Add(item);

                    grandTotalAmount += totalAmount;
                }

                reader.Close();

                // Lấy thông tin giảm giá từ các hóa đơn trong ngày
                sqlCommand.CommandText = @"
                    SELECT 
                        SUM(b.TotalAmount) AS GrandTotal,
                        SUM(b.TotalAmount * b.Discount / 100.0) AS GrandDiscount
                    FROM Bill b
                    WHERE b.TableID = @tableID 
                        AND CONVERT(DATE, b.DateCheckIn) = @date;
                ";

                SqlDataReader discountReader = sqlCommand.ExecuteReader();
                
                decimal grandTotal = 0;
                decimal grandDiscount = 0;

                if (discountReader.Read())
                {
                    grandTotal = discountReader["GrandTotal"] != DBNull.Value ? 
                        Convert.ToDecimal(discountReader["GrandTotal"]) : 0;
                    grandDiscount = discountReader["GrandDiscount"] != DBNull.Value ? 
                        Convert.ToDecimal(discountReader["GrandDiscount"]) : 0;
                }

                discountReader.Close();

                decimal grandFinalAmount = grandTotal - grandDiscount;

                // Hiển thị tổng cộng
                lblTotalAmount.Text = grandTotal.ToString("#,##0");
                lblDiscount.Text = grandDiscount.ToString("#,##0");
                lblFinalAmount.Text = grandFinalAmount.ToString("#,##0");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thống kê: " + ex.Message,
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        // Class helper để lưu trữ thông tin ngày trong ListBox
        private class DateItem
        {
            public DateTime Date { get; set; }
            public string DisplayText { get; set; }

            public override string ToString()
            {
                return DisplayText;
            }
        }
    }
}
