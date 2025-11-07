using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Chuong_6.DataAccess;
using Chuong_6.BusinessLogic;

namespace Chuong_6.RestaurantManagement
{
    public partial class frmInvoiceDetails : Form
    {
        private readonly InvoiceBL invoiceBL;
        private readonly InvoiceDetailsBL invoiceDetailsBL;
        private int invoiceID;
        private Invoice? currentInvoice;

        public frmInvoiceDetails(int invoiceID)
        {
            InitializeComponent();
            this.invoiceBL = new InvoiceBL();
            this.invoiceDetailsBL = new InvoiceDetailsBL();
            this.invoiceID = invoiceID;
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
                currentInvoice = invoiceBL.GetInvoiceByID(invoiceID);

                if (currentInvoice != null)
                {
                    lblInvoiceID.Text = $"Mã hóa đơn: {currentInvoice.ID}";
                    lblInvoiceName.Text = $"Tên hóa đơn: {currentInvoice.Name}";
                    lblTable.Text = $"Bàn: {currentInvoice.TableName}";
                    lblAccount.Text = $"Nhân viên: {currentInvoice.AccountName}";
                    lblStatus.Text = $"Trạng thái: {(currentInvoice.Status ? "Đã thanh toán" : "Chưa thanh toán")}";
                    lblCheckoutDate.Text = $"Ngày thanh toán: {currentInvoice.CheckoutDate:dd/MM/yyyy HH:mm}";

                    // Calculate totals
                    int subtotal = currentInvoice.Total;
                    float discountPercent = currentInvoice.Discount;
                    float taxPercent = currentInvoice.Tax;

                    int discountAmount = (int)(subtotal * discountPercent);
                    int afterDiscount = subtotal - discountAmount;
                    int taxAmount = (int)(afterDiscount * taxPercent);
                    int total = afterDiscount + taxAmount;

                    lblSubtotal.Text = $"Tạm tính: {subtotal:N0} VNĐ";
                    lblDiscount.Text = $"Giảm giá ({discountPercent:P0}): {discountAmount:N0} VNĐ";
                    lblTax.Text = $"Thuế ({taxPercent:P0}): {taxAmount:N0} VNĐ";
                    lblTotal.Text = $"Tổng cộng: {total:N0} VNĐ";

                    // Set form title
                    this.Text = $"Chi tiết hóa đơn - {currentInvoice.Name}";
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
                List<InvoiceDetail> details = invoiceDetailsBL.GetInvoiceDetailsByInvoiceID(invoiceID);

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
