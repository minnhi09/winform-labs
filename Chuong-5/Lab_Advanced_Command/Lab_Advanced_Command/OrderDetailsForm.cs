using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Lab_Advanced_Command
{
    public partial class OrderDetailsForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=Chuong_5_Lab_Advanced_Command; integrated security=true; TrustServerCertificate=True";
        private int billId;

        public OrderDetailsForm(int billId)
        {
            InitializeComponent();
            this.billId = billId;
        }

        private void OrderDetailsForm_Load(object sender, EventArgs e)
        {
            LoadBillDetails();
            LoadBillItems();
        }

        private void LoadBillDetails()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // Use stored procedure instead of raw SQL
                sqlCommand.CommandText = "GetBillByID";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@BillID", billId);

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();

                if (reader.Read())
                {
                    // Header information
                    lblBillIdValue.Text = reader["ID"].ToString();
                    lblAccountValue.Text = $"{reader["AccountName"]} ({reader["Username"]})";
                    lblCheckInValue.Text = Convert.ToDateTime(reader["DateCheckIn"]).ToString("dd/MM/yyyy HH:mm:ss");
                    
                    if (reader["DateCheckOut"] != DBNull.Value)
                    {
                        lblCheckOutValue.Text = Convert.ToDateTime(reader["DateCheckOut"]).ToString("dd/MM/yyyy HH:mm:ss");
                    }
                    else
                    {
                        lblCheckOutValue.Text = "Chưa checkout";
                    }

                    // Financial information
                    int totalAmount = Convert.ToInt32(reader["TotalAmount"]);
                    int discount = Convert.ToInt32(reader["Discount"]);
                    int finalAmount = Convert.ToInt32(reader["FinalAmount"]);

                    lblTotalAmountValue.Text = $"{totalAmount:N0} đ";
                    lblDiscountValue.Text = $"{discount:N0} đ";
                    lblFinalAmountValue.Text = $"{finalAmount:N0} đ";

                    // Status
                    string status = reader["Status"].ToString() ?? "";
                    lblStatusValue.Text = status;
                    
                    // Color code status
                    if (status == "Đã thanh toán")
                    {
                        lblStatusValue.ForeColor = Color.FromArgb(40, 167, 69);
                    }
                    else
                    {
                        lblStatusValue.ForeColor = Color.FromArgb(220, 53, 69);
                    }
                }

                reader.Close();
                sqlConnection.Close();
                sqlCommand.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải thông tin hóa đơn: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBillItems()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // Use stored procedure instead of raw SQL
                sqlCommand.CommandText = "GetBillDetailsWithCategory";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@BillID", billId);

                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                sqlDataAdapter.Fill(dataTable);

                sqlConnection.Close();
                sqlCommand.Dispose();

                dgvItems.AutoGenerateColumns = false;
                dgvItems.DataSource = dataTable;

                // Update item count
                lblItemCount.Text = $"Tổng số món: {dataTable.Rows.Count}";

                // Calculate total quantity
                int totalQuantity = 0;
                foreach (DataRow row in dataTable.Rows)
                {
                    totalQuantity += Convert.ToInt32(row["Quantity"]);
                }
                lblTotalQuantity.Text = $"Tổng số lượng: {totalQuantity}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết món ăn: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Chức năng in hóa đơn đang được phát triển!",
                "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                // Create save file dialog
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                saveFileDialog.FileName = $"HoaDon_{billId}_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                saveFileDialog.Title = "Xuất hóa đơn";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Generate receipt content
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    
                    sb.AppendLine("================================================");
                    sb.AppendLine("           HÓA ĐƠN THANH TOÁN");
                    sb.AppendLine("================================================");
                    sb.AppendLine();
                    sb.AppendLine($"Mã hóa đơn: {lblBillIdValue.Text}");
                    sb.AppendLine($"Người tạo: {lblAccountValue.Text}");
                    sb.AppendLine($"Ngày vào: {lblCheckInValue.Text}");
                    sb.AppendLine($"Ngày ra: {lblCheckOutValue.Text}");
                    sb.AppendLine($"Trạng thái: {lblStatusValue.Text}");
                    sb.AppendLine();
                    sb.AppendLine("================================================");
                    sb.AppendLine("CHI TIẾT MÓN ĂN");
                    sb.AppendLine("================================================");
                    sb.AppendLine();
                    sb.AppendLine(string.Format("{0,-5} {1,-30} {2,-10} {3,-8} {4,-12} {5,-12}",
                        "STT", "Tên món", "Loại", "ĐVT", "SL", "Đơn giá", "Thành tiền"));
                    sb.AppendLine(new string('-', 90));

                    if (dgvItems.DataSource is DataTable dt)
                    {
                        int stt = 1;
                        foreach (DataRow row in dt.Rows)
                        {
                            sb.AppendLine(string.Format("{0,-5} {1,-30} {2,-10} {3,-8} {4,-12} {5,-12:N0}",
                                stt++,
                                row["FoodName"].ToString(),
                                row["CategoryName"].ToString(),
                                row["Unit"].ToString(),
                                row["Quantity"].ToString(),
                                row["Price"].ToString(),
                                Convert.ToInt32(row["TotalPrice"])));
                        }
                    }

                    sb.AppendLine(new string('-', 90));
                    sb.AppendLine();
                    sb.AppendLine($"{lblItemCount.Text}");
                    sb.AppendLine($"{lblTotalQuantity.Text}");
                    sb.AppendLine();
                    sb.AppendLine("================================================");
                    sb.AppendLine("THANH TOÁN");
                    sb.AppendLine("================================================");
                    sb.AppendLine();
                    sb.AppendLine($"Tổng tiền chưa giảm:    {lblTotalAmountValue.Text}");
                    sb.AppendLine($"Giảm giá:              -{lblDiscountValue.Text}");
                    sb.AppendLine(new string('-', 50));
                    sb.AppendLine($"TỔNG THỰC THU:          {lblFinalAmountValue.Text}");
                    sb.AppendLine();
                    sb.AppendLine("================================================");
                    sb.AppendLine($"Xuất lúc: {DateTime.Now:dd/MM/yyyy HH:mm:ss}");
                    sb.AppendLine("         Cảm ơn quý khách!");
                    sb.AppendLine("================================================");

                    // Save to file
                    System.IO.File.WriteAllText(saveFileDialog.FileName, sb.ToString());

                    MessageBox.Show($"Đã xuất hóa đơn thành công!\nFile: {saveFileDialog.FileName}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Ask if want to open file
                    DialogResult result = MessageBox.Show("Bạn có muốn mở file vừa xuất không?",
                        "Mở file", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start("notepad.exe", saveFileDialog.FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi xuất hóa đơn: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
