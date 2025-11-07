using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using entity_framework.models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework
{
    public partial class frmInvoiceDetails : Form
    {
        private RestaurantContext _context;
        private int invoiceID;

        public frmInvoiceDetails(int invoiceID)
        {
            InitializeComponent();
            _context = new RestaurantContext();
            this.invoiceID = invoiceID;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
        }

        private void frmInvoiceDetails_Load(object sender, EventArgs e)
        {
            LoadInvoiceInfo();
            LoadInvoiceDetails();
        }

        private void LoadInvoiceInfo()
        {
            try
            {
                var invoice = _context.Invoices
                    .Include(i => i.Table)
                    .Include(i => i.Account)
                    .FirstOrDefault(i => i.ID == invoiceID);

                if (invoice != null)
                {
                    lblInvoiceID.Text = $"Mã hóa đơn: {invoice.ID}";
                    lblInvoiceName.Text = $"Tên hóa đơn: {invoice.Name}";
                    lblTable.Text = $"Bàn: {invoice.Table.Name}";
                    lblAccount.Text = $"Nhân viên: {invoice.Account.FullName}";
                    lblStatus.Text = $"Trạng thái: {(invoice.Status ? "Đã thanh toán" : "Chưa thanh toán")}";
                    lblCheckoutDate.Text = $"Ngày thanh toán: {invoice.CheckoutDate:dd/MM/yyyy HH:mm}";

                    // Calculate totals
                    int subtotal = invoice.Total;
                    double discountPercent = invoice.Discount ?? 0;
                    double taxPercent = invoice.Tax ?? 0;

                    int discountAmount = (int)(subtotal * discountPercent);
                    int afterDiscount = subtotal - discountAmount;
                    int taxAmount = (int)(afterDiscount * taxPercent);
                    int total = afterDiscount + taxAmount;

                    lblSubtotal.Text = $"Tạm tính: {subtotal:N0} VNĐ";
                    lblDiscount.Text = $"Giảm giá ({discountPercent:P0}): {discountAmount:N0} VNĐ";
                    lblTax.Text = $"Thuế ({taxPercent:P0}): {taxAmount:N0} VNĐ";
                    lblTotal.Text = $"Tổng cộng: {total:N0} VNĐ";

                    // Set form title
                    this.Text = $"Chi tiết hóa đơn - {invoice.Name}";
                }
                else
                {
                    MessageBox.Show("Không tìm thấy hóa đơn!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải thông tin hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadInvoiceDetails()
        {
            try
            {
                dgvInvoiceDetails.DataSource = null;

                var details = _context.InvoiceDetails
                    .Include(id => id.Food)
                    .Where(id => id.InvoiceID == invoiceID)
                    .Select(id => new
                    {
                        id.ID,
                        id.InvoiceID,
                        id.FoodID,
                        FoodName = id.Food.Name,
                        Unit = id.Food.Unit,
                        id.Price,
                        id.Amount,
                        Total = id.Price * id.Amount
                    })
                    .ToList();

                dgvInvoiceDetails.DataSource = details;

                // Format columns
                if (dgvInvoiceDetails.Columns.Count > 0)
                {
                    dgvInvoiceDetails.Columns["ID"].HeaderText = "Mã";
                    dgvInvoiceDetails.Columns["ID"].Width = 60;

                    dgvInvoiceDetails.Columns["InvoiceID"].Visible = false;
                    dgvInvoiceDetails.Columns["FoodID"].Visible = false;

                    dgvInvoiceDetails.Columns["FoodName"].HeaderText = "Tên món";
                    dgvInvoiceDetails.Columns["FoodName"].Width = 250;

                    dgvInvoiceDetails.Columns["Unit"].HeaderText = "Đơn vị";
                    dgvInvoiceDetails.Columns["Unit"].Width = 100;

                    dgvInvoiceDetails.Columns["Price"].HeaderText = "Đơn giá";
                    dgvInvoiceDetails.Columns["Price"].Width = 120;
                    dgvInvoiceDetails.Columns["Price"].DefaultCellStyle.Format = "N0";

                    dgvInvoiceDetails.Columns["Amount"].HeaderText = "Số lượng";
                    dgvInvoiceDetails.Columns["Amount"].Width = 100;

                    dgvInvoiceDetails.Columns["Total"].HeaderText = "Thành tiền";
                    dgvInvoiceDetails.Columns["Total"].Width = 150;
                    dgvInvoiceDetails.Columns["Total"].DefaultCellStyle.Format = "N0";
                }

                lblTotalItems.Text = $"Tổng số món: {details.Count}";

                int totalQuantity = 0;
                foreach (var detail in details)
                {
                    totalQuantity += detail.Amount;
                }
                lblTotalQuantity.Text = $"Tổng số lượng: {totalQuantity}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết hóa đơn: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Chức năng in hóa đơn đang được phát triển!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("Chức năng xuất Excel đang được phát triển!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
