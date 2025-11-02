using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace Lab_Advanced_Command
{
    public partial class OrdersForm : Form
    {
        private const string CONNECTION_STRING = @"server=MNHY\SQLEXPRESS01; database=Chuong_5_Lab_Advanced_Command; integrated security=true; TrustServerCertificate=True";
        private DataTable? billsTable;
        private DataTable? billDetailsTable;

        // Properties to store logged-in user information
        public int LoggedInAccountID { get; set; }
        public string LoggedInUsername { get; set; } = string.Empty;

        public OrdersForm()
        {
            InitializeComponent();
        }

        private void OrdersForm_Load(object sender, EventArgs e)
        {
            LoadBills();
            LoadStatusComboBox();
            InitializeDateFilters();
            ResetForm();
        }

        private void InitializeDateFilters()
        {
            // Set default date range (last 30 days)
            dtpStartDate.Value = DateTime.Now.AddDays(-30);
            dtpEndDate.Value = DateTime.Now;
        }

        private void LoadStatusComboBox()
        {
            cboStatus.Items.Clear();
            cboStatus.Items.Add("Tất cả");
            cboStatus.Items.Add("Chưa thanh toán");
            cboStatus.Items.Add("Đã thanh toán");
            cboStatus.SelectedIndex = 0;
        }

        private void LoadBills()
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // Use stored procedure instead of raw SQL
                sqlCommand.CommandText = "GetAllBills";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                billsTable = new DataTable();
                sqlDataAdapter.Fill(billsTable);

                sqlConnection.Close();
                sqlCommand.Dispose();

                dgvBills.AutoGenerateColumns = false;
                dgvBills.DataSource = billsTable;

                UpdateStatistics();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải danh sách hóa đơn: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadBillDetails(int billId)
        {
            try
            {
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // Use stored procedure instead of raw SQL
                sqlCommand.CommandText = "GetBillDetails";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@BillID", billId);

                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                billDetailsTable = new DataTable();
                sqlDataAdapter.Fill(billDetailsTable);

                sqlConnection.Close();
                sqlCommand.Dispose();

                dgvBillDetails.AutoGenerateColumns = false;
                dgvBillDetails.DataSource = billDetailsTable;

                lblDetailCount.Text = $"Số món: {billDetailsTable.Rows.Count}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải chi tiết hóa đơn: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStatistics()
        {
            if (billsTable == null || billsTable.Rows.Count == 0)
            {
                lblTotalBills.Text = "Tổng số hóa đơn: 0";
                lblTotalAmount.Text = "Tổng tiền chưa giảm: 0 đ";
                lblTotalDiscount.Text = "Tổng giảm giá: 0 đ";
                lblTotalFinalAmount.Text = "Tổng thực thu: 0 đ";
                return;
            }

            int totalBills = billsTable.Rows.Count;
            decimal totalAmount = 0;
            decimal totalDiscount = 0;
            decimal totalFinalAmount = 0;

            foreach (DataRow row in billsTable.Rows)
            {
                totalAmount += Convert.ToDecimal(row["TotalAmount"]);
                totalDiscount += Convert.ToDecimal(row["Discount"]);
                totalFinalAmount += Convert.ToDecimal(row["FinalAmount"]);
            }

            lblTotalBills.Text = $"Tổng số hóa đơn: {totalBills}";
            lblTotalAmount.Text = $"Tổng tiền chưa giảm: {totalAmount:N0} đ";
            lblTotalDiscount.Text = $"Tổng giảm giá: {totalDiscount:N0} đ";
            lblTotalFinalAmount.Text = $"Tổng thực thu: {totalFinalAmount:N0} đ";
        }

        private void ResetForm()
        {
            txtBillId.Clear();
            txtAccountName.Clear();
            dtpCheckIn.Value = DateTime.Now;
            dtpCheckOut.Value = DateTime.Now;
            txtTotalAmount.Text = "0";
            txtDiscount.Text = "0";
            txtFinalAmount.Text = "0";
            txtStatus.Text = "Chưa thanh toán";

            dgvBillDetails.DataSource = null;
            lblDetailCount.Text = "Số món: 0";

            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            btnViewDetails.Enabled = false;
        }

        private string GetCellValue(DataGridViewRow row, string columnName)
        {
            try
            {
                if (row.Cells[columnName].Value != null)
                {
                    return row.Cells[columnName].Value.ToString() ?? "";
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show($"Không tìm thấy cột: {columnName}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "";
        }

        private void dgvBills_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvBills.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow selectedRow = dgvBills.SelectedRows[0];

                    txtBillId.Text = GetCellValue(selectedRow, "colBillID");
                    txtAccountName.Text = GetCellValue(selectedRow, "colAccountName");
                    
                    string checkInStr = GetCellValue(selectedRow, "colDateCheckIn");
                    if (!string.IsNullOrEmpty(checkInStr) && DateTime.TryParse(checkInStr, out DateTime checkIn))
                    {
                        dtpCheckIn.Value = checkIn;
                    }

                    string checkOutStr = GetCellValue(selectedRow, "colDateCheckOut");
                    if (!string.IsNullOrEmpty(checkOutStr) && DateTime.TryParse(checkOutStr, out DateTime checkOut))
                    {
                        dtpCheckOut.Value = checkOut;
                    }

                    txtTotalAmount.Text = GetCellValue(selectedRow, "colTotalAmount");
                    txtDiscount.Text = GetCellValue(selectedRow, "colDiscount");
                    txtFinalAmount.Text = GetCellValue(selectedRow, "colFinalAmount");
                    txtStatus.Text = GetCellValue(selectedRow, "colStatus");

                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                    btnViewDetails.Enabled = true;

                    // Auto load bill details
                    if (!string.IsNullOrEmpty(txtBillId.Text))
                    {
                        LoadBillDetails(Convert.ToInt32(txtBillId.Text));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi hiển thị thông tin hóa đơn: {ex.Message}",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetForm();
                }
            }
        }

        private void dgvBills_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header row clicks
            if (e.RowIndex < 0)
                return;

            if (dgvBills.SelectedRows.Count > 0)
            {
                try
                {
                    DataGridViewRow selectedRow = dgvBills.SelectedRows[0];
                    string billIdStr = GetCellValue(selectedRow, "colBillID");

                    if (!string.IsNullOrEmpty(billIdStr) && int.TryParse(billIdStr, out int billId))
                    {
                        // Open OrderDetailsForm with the selected bill ID
                        OrderDetailsForm detailsForm = new OrderDetailsForm(billId);
                        detailsForm.ShowDialog();

                        // Refresh the bills list after closing the details form (in case any changes were made)
                        LoadBills();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi mở chi tiết hóa đơn: {ex.Message}",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnViewDetails_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBillId.Text))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xem chi tiết!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            LoadBillDetails(Convert.ToInt32(txtBillId.Text));
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBillId.Text))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần cập nhật!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // Use stored procedure instead of raw SQL
                sqlCommand.CommandText = "UpdateBill";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(txtBillId.Text));
                sqlCommand.Parameters.AddWithValue("@DateCheckOut", dtpCheckOut.Value);
                sqlCommand.Parameters.AddWithValue("@Discount", Convert.ToInt32(txtDiscount.Text));
                
                int totalAmount = Convert.ToInt32(txtTotalAmount.Text);
                int discount = Convert.ToInt32(txtDiscount.Text);
                int finalAmount = totalAmount - discount;
                
                sqlCommand.Parameters.AddWithValue("@FinalAmount", finalAmount);
                sqlCommand.Parameters.AddWithValue("@Status", txtStatus.Text);

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                
                int rowsAffected = 0;
                if (reader.Read())
                {
                    rowsAffected = Convert.ToInt32(reader["RowsAffected"]);
                }
                
                reader.Close();
                sqlConnection.Close();
                sqlCommand.Dispose();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Cập nhật hóa đơn thành công!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadBills();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Cập nhật hóa đơn thất bại!",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtBillId.Text))
            {
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!", "Cảnh báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa hóa đơn #{txtBillId.Text} không?\nLưu ý: Tất cả chi tiết hóa đơn cũng sẽ bị xóa!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                    SqlCommand sqlCommand = sqlConnection.CreateCommand();

                    // Use stored procedure instead of raw SQL
                    sqlCommand.CommandText = "DeleteBill";
                    sqlCommand.CommandType = CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@ID", Convert.ToInt32(txtBillId.Text));

                    sqlConnection.Open();
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    
                    int success = 0;
                    string message = "";
                    if (reader.Read())
                    {
                        success = Convert.ToInt32(reader["Success"]);
                        message = reader["Message"].ToString() ?? "";
                    }
                    
                    reader.Close();
                    sqlConnection.Close();
                    sqlCommand.Dispose();

                    if (success > 0)
                    {
                        MessageBox.Show("Xóa hóa đơn thành công!",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadBills();
                        ResetForm();
                    }
                    else
                    {
                        MessageBox.Show("Xóa hóa đơn thất bại!",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadBills();
            ResetForm();
        }

        private void cboStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (billsTable == null) return;

            string selectedStatus = cboStatus.SelectedItem?.ToString() ?? "Tất cả";

            if (selectedStatus == "Tất cả")
            {
                dgvBills.DataSource = billsTable;
            }
            else
            {
                DataView dataView = billsTable.DefaultView;
                dataView.RowFilter = $"Status = '{selectedStatus}'";
                dgvBills.DataSource = dataView;
            }

            UpdateStatistics();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (billsTable == null) return;

            string searchText = txtSearch.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {
                dgvBills.DataSource = billsTable;
            }
            else
            {
                DataView dataView = billsTable.DefaultView;
                dataView.RowFilter = $"AccountName LIKE '%{searchText}%' OR Username LIKE '%{searchText}%'";
                dgvBills.DataSource = dataView;
            }

            UpdateStatistics();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            CalculateFinalAmount();
        }

        private void CalculateFinalAmount()
        {
            try
            {
                int totalAmount = string.IsNullOrEmpty(txtTotalAmount.Text) ? 0 : Convert.ToInt32(txtTotalAmount.Text);
                int discount = string.IsNullOrEmpty(txtDiscount.Text) ? 0 : Convert.ToInt32(txtDiscount.Text);
                int finalAmount = totalAmount - discount;

                if (finalAmount < 0) finalAmount = 0;

                txtFinalAmount.Text = finalAmount.ToString();
            }
            catch
            {
                txtFinalAmount.Text = "0";
            }
        }

        private void btnFilterDate_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime startDate = dtpStartDate.Value.Date;
                DateTime endDate = dtpEndDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

                if (startDate > endDate)
                {
                    MessageBox.Show("Ngày bắt đầu không thể lớn hơn ngày kết thúc!",
                        "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                SqlConnection sqlConnection = new SqlConnection(CONNECTION_STRING);
                SqlCommand sqlCommand = sqlConnection.CreateCommand();

                // Use stored procedure for date range filtering
                sqlCommand.CommandText = "GetBillsByDateRange";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", endDate);

                sqlConnection.Open();
                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);
                billsTable = new DataTable();
                sqlDataAdapter.Fill(billsTable);

                sqlConnection.Close();
                sqlCommand.Dispose();

                dgvBills.AutoGenerateColumns = false;
                dgvBills.DataSource = billsTable;

                UpdateStatistics();

                MessageBox.Show($"Đã lọc {billsTable.Rows.Count} hóa đơn từ {startDate:dd/MM/yyyy} đến {endDate:dd/MM/yyyy}",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lọc theo ngày: {ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
