using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Chuong_6.DataAccess;
using Chuong_6.BusinessLogic;

namespace Chuong_6.RestaurantManagement
{
    public partial class frmInvoice : Form
    {
        private readonly InvoiceBL invoiceBL;
        private readonly InvoiceDetailsBL invoiceDetailsBL;
        private readonly TableBL tableBL;
        private readonly FoodBL foodBL;
        private Account currentUser;
        private int currentInvoiceID = 0;

        public frmInvoice(Account user)
        {
            InitializeComponent();
            this.invoiceBL = new InvoiceBL();
            this.invoiceDetailsBL = new InvoiceDetailsBL();
            this.tableBL = new TableBL();
            this.foodBL = new FoodBL();
            this.currentUser = user;
        }

        private void frmInvoice_Load(object sender, EventArgs e)
        {
            LoadTables();
            LoadFoods();
            InitializeListView();
            LoadInvoices();

            // Set date range to current month
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = DateTime.Now;

            // Debug: Test ListView
            System.Diagnostics.Debug.WriteLine("=== Form Load Complete ===");
            System.Diagnostics.Debug.WriteLine($"grpInvoiceDetails.Dock: {grpInvoiceDetails.Dock}");
            System.Diagnostics.Debug.WriteLine($"grpInvoiceDetails.Location: {grpInvoiceDetails.Location}");
            System.Diagnostics.Debug.WriteLine($"grpInvoiceDetails.Size: {grpInvoiceDetails.Size}");
            System.Diagnostics.Debug.WriteLine($"lvInvoiceDetails.Dock: {lvInvoiceDetails.Dock}");
            System.Diagnostics.Debug.WriteLine($"lvInvoiceDetails.Size: {lvInvoiceDetails.Size}");
        }

        private void InitializeListView()
        {
            // Ensure ListView is in Details view
            lvInvoiceDetails.View = View.Details;
            lvInvoiceDetails.FullRowSelect = true;
            lvInvoiceDetails.GridLines = true;
            lvInvoiceDetails.Visible = true;

            // Clear existing items and columns
            lvInvoiceDetails.Items.Clear();
            lvInvoiceDetails.Columns.Clear();

            // Add columns with proper settings
            lvInvoiceDetails.Columns.Add("STT", 50, HorizontalAlignment.Center);
            lvInvoiceDetails.Columns.Add("Món ăn", 250, HorizontalAlignment.Left);
            lvInvoiceDetails.Columns.Add("Đơn vị", 100, HorizontalAlignment.Center);
            lvInvoiceDetails.Columns.Add("Đơn giá", 120, HorizontalAlignment.Right);
            lvInvoiceDetails.Columns.Add("Số lượng", 100, HorizontalAlignment.Center);
            lvInvoiceDetails.Columns.Add("Thành tiền", 150, HorizontalAlignment.Right);

            System.Diagnostics.Debug.WriteLine($"ListView initialized - Visible: {lvInvoiceDetails.Visible}, Size: {lvInvoiceDetails.Size}");
        }

        private void LoadTables()
        {
            try
            {
                List<Table> tables = tableBL.GetAllTables();

                cboTable.DataSource = tables;
                cboTable.DisplayMember = "Name";
                cboTable.ValueMember = "ID";

                if (tables.Count > 0)
                {
                    cboTable.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách bàn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadFoods()
        {
            try
            {
                List<Food> foods = foodBL.GetAllFoods();

                cboFood.DataSource = foods;
                cboFood.DisplayMember = "Name";
                cboFood.ValueMember = "ID";

                if (foods.Count > 0)
                {
                    cboFood.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách món ăn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoices()
        {
            try
            {
                List<Invoice> invoices = invoiceBL.GetAllInvoices();

                dgvInvoices.DataSource = null;
                dgvInvoices.DataSource = invoices;

                // Format columns
                if (dgvInvoices.Columns.Count > 0)
                {
                    dgvInvoices.Columns["ID"].HeaderText = "Mã HĐ";
                    dgvInvoices.Columns["ID"].Width = 80;

                    dgvInvoices.Columns["Name"].HeaderText = "Tên hóa đơn";
                    dgvInvoices.Columns["Name"].Width = 200;

                    dgvInvoices.Columns["TableID"].HeaderText = "Mã bàn";
                    dgvInvoices.Columns["TableID"].Width = 80;

                    dgvInvoices.Columns["TableName"].HeaderText = "Tên bàn";
                    dgvInvoices.Columns["TableName"].Width = 150;

                    dgvInvoices.Columns["Total"].HeaderText = "Tổng tiền";
                    dgvInvoices.Columns["Total"].Width = 120;
                    dgvInvoices.Columns["Total"].DefaultCellStyle.Format = "N0";

                    dgvInvoices.Columns["Discount"].HeaderText = "Giảm giá";
                    dgvInvoices.Columns["Discount"].Width = 100;
                    dgvInvoices.Columns["Discount"].DefaultCellStyle.Format = "P0";

                    dgvInvoices.Columns["Tax"].HeaderText = "Thuế";
                    dgvInvoices.Columns["Tax"].Width = 100;
                    dgvInvoices.Columns["Tax"].DefaultCellStyle.Format = "P0";

                    dgvInvoices.Columns["Status"].HeaderText = "Trạng thái";
                    dgvInvoices.Columns["Status"].Width = 120;

                    dgvInvoices.Columns["AccountID"].HeaderText = "Mã TK";
                    dgvInvoices.Columns["AccountID"].Width = 100;

                    dgvInvoices.Columns["AccountName"].HeaderText = "Nhân viên";
                    dgvInvoices.Columns["AccountName"].Width = 150;

                    dgvInvoices.Columns["CheckoutDate"].HeaderText = "Ngày thanh toán";
                    dgvInvoices.Columns["CheckoutDate"].Width = 150;
                    dgvInvoices.Columns["CheckoutDate"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
                }

                lblTotalInvoices.Text = $"Tổng số: {invoices.Count} hóa đơn";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoiceDetails(int invoiceID)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"=== LoadInvoiceDetails START ===");
                System.Diagnostics.Debug.WriteLine($"InvoiceID: {invoiceID}");
                System.Diagnostics.Debug.WriteLine($"ListView Visible: {lvInvoiceDetails.Visible}");
                System.Diagnostics.Debug.WriteLine($"ListView View: {lvInvoiceDetails.View}");
                System.Diagnostics.Debug.WriteLine($"ListView Size: {lvInvoiceDetails.Size}");
                System.Diagnostics.Debug.WriteLine($"ListView Columns Count: {lvInvoiceDetails.Columns.Count}");
                System.Diagnostics.Debug.WriteLine($"grpInvoiceDetails Visible: {grpInvoiceDetails.Visible}");
                System.Diagnostics.Debug.WriteLine($"grpInvoiceDetails Size: {grpInvoiceDetails.Size}");

                lvInvoiceDetails.BeginUpdate();
                lvInvoiceDetails.Items.Clear();

                List<InvoiceDetail> details = invoiceDetailsBL.GetInvoiceDetailsByInvoiceID(invoiceID);

                System.Diagnostics.Debug.WriteLine($"Retrieved {details?.Count ?? 0} invoice details from DB");

                if (details == null || details.Count == 0)
                {
                    System.Diagnostics.Debug.WriteLine("No details found - updating summary with 0");
                    UpdateSummary(0);
                    lvInvoiceDetails.EndUpdate();
                    lvInvoiceDetails.Refresh();
                    return;
                }

                int stt = 1;
                int subtotal = 0;

                foreach (InvoiceDetail detail in details)
                {
                    // Validate data before adding
                    if (detail == null)
                    {
                        System.Diagnostics.Debug.WriteLine($"Skipping null detail at index {stt}");
                        continue;
                    }

                    ListViewItem item = new ListViewItem(stt.ToString());
                    item.SubItems.Add(detail.FoodName ?? "N/A");
                    item.SubItems.Add(detail.Unit ?? "");
                    item.SubItems.Add(detail.Price.ToString("N0"));
                    item.SubItems.Add(detail.Amount.ToString());
                    item.SubItems.Add(detail.Total.ToString("N0"));

                    item.Tag = detail.ID;
                    lvInvoiceDetails.Items.Add(item);

                    System.Diagnostics.Debug.WriteLine($"Added item {stt}: {detail.FoodName} - Amount: {detail.Amount} - Total: {detail.Total:N0}");

                    subtotal += detail.Total;
                    stt++;
                }

                // Update summary
                UpdateSummary(subtotal);
                lvInvoiceDetails.EndUpdate();
                lvInvoiceDetails.Refresh();

                System.Diagnostics.Debug.WriteLine($"ListView Items Count after add: {lvInvoiceDetails.Items.Count}");
                System.Diagnostics.Debug.WriteLine($"Subtotal: {subtotal:N0}");
                System.Diagnostics.Debug.WriteLine($"=== LoadInvoiceDetails END ===");
            }
            catch (Exception ex)
            {
                lvInvoiceDetails.EndUpdate();
                System.Diagnostics.Debug.WriteLine($"ERROR in LoadInvoiceDetails: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                MessageBox.Show("Lỗi khi tải chi tiết hóa đơn: " + ex.Message + "\n" + ex.StackTrace, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateSummary(int subtotal)
        {
            float discountPercent = (float)nudDiscount.Value / 100;
            float taxPercent = (float)nudTax.Value / 100;

            int discountAmount = (int)(subtotal * discountPercent);
            int afterDiscount = subtotal - discountAmount;
            int taxAmount = (int)(afterDiscount * taxPercent);
            int total = afterDiscount + taxAmount;

            lblSubTotal.Text = $"Tạm tính: {subtotal:N0} VNĐ";
            lblDiscount.Text = $"Giảm giá: {discountAmount:N0} VNĐ";
            lblTax.Text = $"Thuế: {taxAmount:N0} VNĐ";
            lblTotal.Text = $"Tổng cộng: {total:N0} VNĐ";
        }

        private void dgvInvoices_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvInvoices.CurrentRow != null && dgvInvoices.CurrentRow.DataBoundItem != null)
            {
                Invoice invoice = (Invoice)dgvInvoices.CurrentRow.DataBoundItem;
                currentInvoiceID = invoice.ID;

                // Load invoice details
                LoadInvoiceDetails(currentInvoiceID);

                // Set discount and tax (database stores as percentage: 5 = 5%, 10 = 10%)
                // invoice.Discount and invoice.Tax are already in decimal format (0.05 = 5%, 0.10 = 10%)
                nudDiscount.Value = (decimal)(invoice.Discount * 100);
                nudTax.Value = (decimal)(invoice.Tax * 100);

                // Enable/disable buttons based on invoice status
                bool isNotPaid = invoice != null && !invoice.Status;
                btnAddFood.Enabled = isNotPaid;
                btnIncreaseAmount.Enabled = isNotPaid;
                btnDecreaseAmount.Enabled = isNotPaid;
                btnDeleteFood.Enabled = isNotPaid;
                btnCheckout.Enabled = isNotPaid;
            }
            else
            {
                currentInvoiceID = 0;
                lvInvoiceDetails.Items.Clear();
                UpdateSummary(0);
                btnAddFood.Enabled = false;
                btnIncreaseAmount.Enabled = false;
                btnDecreaseAmount.Enabled = false;
                btnDeleteFood.Enabled = false;
                btnCheckout.Enabled = false;
            }
        }

        private void dgvInvoices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // This method is kept for compatibility with designer
        }

        // Double-click to open invoice details form
        private void dgvInvoices_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvInvoices.Rows[e.RowIndex].DataBoundItem != null)
            {
                Invoice invoice = (Invoice)dgvInvoices.Rows[e.RowIndex].DataBoundItem;
                OpenInvoiceDetailsForm(invoice.ID);
            }
        }

        private void OpenInvoiceDetailsForm(int invoiceID)
        {
            try
            {
                frmInvoiceDetails frmDetails = new frmInvoiceDetails(invoiceID);
                frmDetails.ShowDialog();

                // Refresh after closing
                LoadInvoices();
                if (currentInvoiceID > 0)
                {
                    LoadInvoiceDetails(currentInvoiceID);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở form chi tiết: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCreateInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboTable.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn bàn!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int tableID = Convert.ToInt32(cboTable.SelectedValue);

                // Check if table already has an unpaid invoice
                Invoice existingInvoice = invoiceBL.GetInvoiceByTableID(tableID);
                if (existingInvoice != null)
                {
                    MessageBox.Show("Bàn này đã có hóa đơn chưa thanh toán!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string invoiceName = $"HD-{DateTime.Now:yyyyMMddHHmmss}";
                int newInvoiceID = invoiceBL.AddInvoice(invoiceName, tableID, currentUser.AccountName);

                if (newInvoiceID > 0)
                {
                    MessageBox.Show("Tạo hóa đơn thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInvoices();

                    // Select the new invoice
                    foreach (DataGridViewRow row in dgvInvoices.Rows)
                    {
                        Invoice inv = (Invoice)row.DataBoundItem;
                        if (inv.ID == newInvoiceID)
                        {
                            row.Selected = true;
                            dgvInvoices.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tạo hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentInvoiceID == 0)
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cboFood.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn món ăn!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int foodID = Convert.ToInt32(cboFood.SelectedValue);
                int amount = (int)nudAmount.Value;

                if (amount <= 0)
                {
                    MessageBox.Show("Số lượng phải lớn hơn 0!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int result = invoiceDetailsBL.AddInvoiceDetail(currentInvoiceID, foodID, amount);

                if (result > 0)
                {
                    MessageBox.Show("Thêm món thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInvoiceDetails(currentInvoiceID);
                    LoadInvoices(); // Refresh to update total
                }
                else
                {
                    MessageBox.Show("Không thể thêm món ăn!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm món: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIncreaseAmount_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvInvoiceDetails.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn món ăn!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int detailID = Convert.ToInt32(lvInvoiceDetails.SelectedItems[0].Tag);
                var result = invoiceDetailsBL.IncreaseAmount(detailID);

                if (result.NewAmount > 0)
                {
                    MessageBox.Show("Tăng số lượng thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInvoiceDetails(currentInvoiceID);
                    LoadInvoices();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tăng số lượng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDecreaseAmount_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvInvoiceDetails.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn món ăn!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int detailID = Convert.ToInt32(lvInvoiceDetails.SelectedItems[0].Tag);
                var result = invoiceDetailsBL.DecreaseAmount(detailID);

                if (result.NewAmount >= 0)
                {
                    MessageBox.Show(result.Message, "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadInvoiceDetails(currentInvoiceID);
                    LoadInvoices();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi giảm số lượng: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvInvoiceDetails.SelectedItems.Count == 0)
                {
                    MessageBox.Show("Vui lòng chọn món ăn cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show("Bạn có chắc muốn xóa món này?", "Xác nhận",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    int detailID = Convert.ToInt32(lvInvoiceDetails.SelectedItems[0].Tag);
                    int result = invoiceDetailsBL.DeleteInvoiceDetail(detailID);

                    if (result > 0)
                    {
                        MessageBox.Show("Xóa món thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadInvoiceDetails(currentInvoiceID);
                        LoadInvoices();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa món: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentInvoiceID == 0)
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Get current invoice
                Invoice invoice = invoiceBL.GetInvoiceByID(currentInvoiceID);

                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (invoice.Status)
                {
                    MessageBox.Show("Hóa đơn đã được thanh toán!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show($"Xác nhận thanh toán hóa đơn?\n\n" +
                    $"Tên hóa đơn: {invoice.Name}\n" +
                    $"Bàn: {invoice.TableName}\n" +
                    $"Tổng tiền: {CalculateFinalTotal(invoice):N0} VNĐ",
                    "Xác nhận thanh toán",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    // Convert percentage to decimal (5% = 0.05)
                    float discount = (float)nudDiscount.Value / 100;
                    float tax = (float)nudTax.Value / 100;

                    var result = invoiceBL.CheckoutInvoice(currentInvoiceID, discount, tax);

                    if (result != null && result.AffectedRows > 0)
                    {
                        MessageBox.Show("Thanh toán thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadInvoices();

                        if (dgvInvoices.Rows.Count > 0)
                        {
                            dgvInvoices.Rows[0].Selected = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thanh toán: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int CalculateFinalTotal(Invoice invoice)
        {
            float discountPercent = (float)nudDiscount.Value / 100;
            float taxPercent = (float)nudTax.Value / 100;

            int discountAmount = (int)(invoice.Total * discountPercent);
            int afterDiscount = invoice.Total - discountAmount;
            int taxAmount = (int)(afterDiscount * taxPercent);
            int total = afterDiscount + taxAmount;

            return total;
        }

        private void btnDeleteInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentInvoiceID == 0)
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn cần xóa!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Invoice invoice = invoiceBL.GetInvoiceByID(currentInvoiceID);

                if (invoice == null)
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (invoice.Status)
                {
                    MessageBox.Show("Không thể xóa hóa đơn đã thanh toán!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult confirm = MessageBox.Show($"Bạn có chắc muốn xóa hóa đơn '{invoice.Name}'?",
                    "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    int result = invoiceBL.DeleteInvoice(currentInvoiceID);

                    if (result > 0)
                    {
                        MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadInvoices();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fromDate = dtpFrom.Value.Date;
                DateTime toDate = dtpTo.Value.Date.AddDays(1).AddSeconds(-1);

                if (fromDate > toDate)
                {
                    MessageBox.Show("Ngày bắt đầu không được lớn hơn ngày kết thúc!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                List<Invoice> invoices = invoiceBL.GetInvoicesByDate(fromDate, toDate);

                dgvInvoices.DataSource = null;
                dgvInvoices.DataSource = invoices;

                lblTotalInvoices.Text = $"Tổng số: {invoices.Count} hóa đơn";

                MessageBox.Show($"Tìm thấy {invoices.Count} hóa đơn!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadInvoices();
        }

        private void nudDiscount_ValueChanged(object sender, EventArgs e)
        {
            if (currentInvoiceID > 0 && lvInvoiceDetails.Items.Count > 0)
            {
                int subtotal = 0;
                foreach (ListViewItem item in lvInvoiceDetails.Items)
                {
                    subtotal += int.Parse(item.SubItems[5].Text.Replace(",", ""));
                }
                UpdateSummary(subtotal);
            }
        }

        private void nudTax_ValueChanged(object sender, EventArgs e)
        {
            if (currentInvoiceID > 0 && lvInvoiceDetails.Items.Count > 0)
            {
                int subtotal = 0;
                foreach (ListViewItem item in lvInvoiceDetails.Items)
                {
                    subtotal += int.Parse(item.SubItems[5].Text.Replace(",", ""));
                }
                UpdateSummary(subtotal);
            }
        }
    }
}