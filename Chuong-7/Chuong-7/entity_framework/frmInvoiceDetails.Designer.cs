namespace entity_framework
{
    partial class frmInvoiceDetails
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.grpInvoiceInfo = new System.Windows.Forms.GroupBox();
            this.lblInvoiceID = new System.Windows.Forms.Label();
            this.lblInvoiceName = new System.Windows.Forms.Label();
            this.lblTable = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCheckoutDate = new System.Windows.Forms.Label();
            this.grpInvoiceDetails = new System.Windows.Forms.GroupBox();
            this.dgvInvoiceDetails = new System.Windows.Forms.DataGridView();
            this.lblTotalItems = new System.Windows.Forms.Label();
            this.lblTotalQuantity = new System.Windows.Forms.Label();
            this.grpSummary = new System.Windows.Forms.GroupBox();
            this.lblSubtotal = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.grpInvoiceInfo.SuspendLayout();
            this.grpInvoiceDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceDetails)).BeginInit();
            this.grpSummary.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpInvoiceInfo
            // 
            this.grpInvoiceInfo.Controls.Add(this.lblInvoiceID);
            this.grpInvoiceInfo.Controls.Add(this.lblInvoiceName);
            this.grpInvoiceInfo.Controls.Add(this.lblTable);
            this.grpInvoiceInfo.Controls.Add(this.lblAccount);
            this.grpInvoiceInfo.Controls.Add(this.lblStatus);
            this.grpInvoiceInfo.Controls.Add(this.lblCheckoutDate);
            this.grpInvoiceInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpInvoiceInfo.Location = new System.Drawing.Point(20, 20);
            this.grpInvoiceInfo.Name = "grpInvoiceInfo";
            this.grpInvoiceInfo.Padding = new System.Windows.Forms.Padding(10);
            this.grpInvoiceInfo.Size = new System.Drawing.Size(1360, 200);
            this.grpInvoiceInfo.TabIndex = 0;
            this.grpInvoiceInfo.TabStop = false;
            this.grpInvoiceInfo.Text = "Thông tin hóa đơn";
            // 
            // lblInvoiceID
            // 
            this.lblInvoiceID.AutoSize = true;
            this.lblInvoiceID.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInvoiceID.Location = new System.Drawing.Point(20, 35);
            this.lblInvoiceID.Name = "lblInvoiceID";
            this.lblInvoiceID.Size = new System.Drawing.Size(150, 31);
            this.lblInvoiceID.TabIndex = 0;
            this.lblInvoiceID.Text = "Mã hóa đơn:";
            // 
            // lblInvoiceName
            // 
            this.lblInvoiceName.AutoSize = true;
            this.lblInvoiceName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblInvoiceName.Location = new System.Drawing.Point(20, 70);
            this.lblInvoiceName.Name = "lblInvoiceName";
            this.lblInvoiceName.Size = new System.Drawing.Size(163, 31);
            this.lblInvoiceName.TabIndex = 1;
            this.lblInvoiceName.Text = "Tên hóa đơn:";
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTable.Location = new System.Drawing.Point(20, 105);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(59, 31);
            this.lblTable.TabIndex = 2;
            this.lblTable.Text = "Bàn:";
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAccount.Location = new System.Drawing.Point(700, 35);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(128, 31);
            this.lblAccount.TabIndex = 3;
            this.lblAccount.Text = "Nhân viên:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.Location = new System.Drawing.Point(700, 70);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(132, 31);
            this.lblStatus.TabIndex = 4;
            this.lblStatus.Text = "Trạng thái:";
            // 
            // lblCheckoutDate
            // 
            this.lblCheckoutDate.AutoSize = true;
            this.lblCheckoutDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCheckoutDate.Location = new System.Drawing.Point(700, 105);
            this.lblCheckoutDate.Name = "lblCheckoutDate";
            this.lblCheckoutDate.Size = new System.Drawing.Size(191, 31);
            this.lblCheckoutDate.TabIndex = 5;
            this.lblCheckoutDate.Text = "Ngày thanh toán:";
            // 
            // grpInvoiceDetails
            // 
            this.grpInvoiceDetails.Controls.Add(this.dgvInvoiceDetails);
            this.grpInvoiceDetails.Controls.Add(this.lblTotalItems);
            this.grpInvoiceDetails.Controls.Add(this.lblTotalQuantity);
            this.grpInvoiceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInvoiceDetails.Location = new System.Drawing.Point(20, 220);
            this.grpInvoiceDetails.Name = "grpInvoiceDetails";
            this.grpInvoiceDetails.Padding = new System.Windows.Forms.Padding(10);
            this.grpInvoiceDetails.Size = new System.Drawing.Size(1360, 600);
            this.grpInvoiceDetails.TabIndex = 1;
            this.grpInvoiceDetails.TabStop = false;
            this.grpInvoiceDetails.Text = "Chi tiết hóa đơn";
            // 
            // dgvInvoiceDetails
            // 
            this.dgvInvoiceDetails.AllowUserToAddRows = false;
            this.dgvInvoiceDetails.AllowUserToDeleteRows = false;
            this.dgvInvoiceDetails.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInvoiceDetails.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvInvoiceDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoiceDetails.Location = new System.Drawing.Point(20, 35);
            this.dgvInvoiceDetails.Name = "dgvInvoiceDetails";
            this.dgvInvoiceDetails.ReadOnly = true;
            this.dgvInvoiceDetails.RowHeadersWidth = 82;
            this.dgvInvoiceDetails.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvInvoiceDetails.Size = new System.Drawing.Size(1320, 510);
            this.dgvInvoiceDetails.TabIndex = 0;
            // 
            // lblTotalItems
            // 
            this.lblTotalItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalItems.AutoSize = true;
            this.lblTotalItems.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTotalItems.Location = new System.Drawing.Point(20, 555);
            this.lblTotalItems.Name = "lblTotalItems";
            this.lblTotalItems.Size = new System.Drawing.Size(150, 29);
            this.lblTotalItems.TabIndex = 1;
            this.lblTotalItems.Text = "Tổng số món: 0";
            // 
            // lblTotalQuantity
            // 
            this.lblTotalQuantity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalQuantity.AutoSize = true;
            this.lblTotalQuantity.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTotalQuantity.Location = new System.Drawing.Point(250, 555);
            this.lblTotalQuantity.Name = "lblTotalQuantity";
            this.lblTotalQuantity.Size = new System.Drawing.Size(180, 29);
            this.lblTotalQuantity.TabIndex = 2;
            this.lblTotalQuantity.Text = "Tổng số lượng: 0";
            // 
            // grpSummary
            // 
            this.grpSummary.Controls.Add(this.lblSubtotal);
            this.grpSummary.Controls.Add(this.lblDiscount);
            this.grpSummary.Controls.Add(this.lblTax);
            this.grpSummary.Controls.Add(this.lblTotal);
            this.grpSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpSummary.Location = new System.Drawing.Point(20, 820);
            this.grpSummary.Name = "grpSummary";
            this.grpSummary.Padding = new System.Windows.Forms.Padding(10);
            this.grpSummary.Size = new System.Drawing.Size(1360, 180);
            this.grpSummary.TabIndex = 2;
            this.grpSummary.TabStop = false;
            this.grpSummary.Text = "Tổng kết";
            // 
            // lblSubtotal
            // 
            this.lblSubtotal.AutoSize = true;
            this.lblSubtotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubtotal.Location = new System.Drawing.Point(20, 35);
            this.lblSubtotal.Name = "lblSubtotal";
            this.lblSubtotal.Size = new System.Drawing.Size(214, 31);
            this.lblSubtotal.TabIndex = 0;
            this.lblSubtotal.Text = "Tạm tính: 0 VNĐ";
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDiscount.Location = new System.Drawing.Point(20, 75);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(190, 29);
            this.lblDiscount.TabIndex = 1;
            this.lblDiscount.Text = "Giảm giá: 0 VNĐ";
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTax.Location = new System.Drawing.Point(20, 110);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(150, 29);
            this.lblTax.TabIndex = 2;
            this.lblTax.Text = "Thuế: 0 VNĐ";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.ForeColor = System.Drawing.Color.Red;
            this.lblTotal.Location = new System.Drawing.Point(700, 65);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(300, 37);
            this.lblTotal.TabIndex = 3;
            this.lblTotal.Text = "Tổng cộng: 0 VNĐ";
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnPrint);
            this.pnlButtons.Controls.Add(this.btnExport);
            this.pnlButtons.Controls.Add(this.btnClose);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(20, 1000);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1360, 80);
            this.pnlButtons.TabIndex = 3;
            // 
            // btnPrint
            // 
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Location = new System.Drawing.Point(886, 15);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(150, 50);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "In hóa đơn";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(1050, 15);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(150, 50);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "Xuất Excel";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(1214, 15);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(126, 50);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Đóng";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // frmInvoiceDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 1100);
            this.Controls.Add(this.grpInvoiceDetails);
            this.Controls.Add(this.grpSummary);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.grpInvoiceInfo);
            this.Name = "frmInvoiceDetails";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết hóa đơn";
            this.Load += new System.EventHandler(this.frmInvoiceDetails_Load);
            this.grpInvoiceInfo.ResumeLayout(false);
            this.grpInvoiceInfo.PerformLayout();
            this.grpInvoiceDetails.ResumeLayout(false);
            this.grpInvoiceDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoiceDetails)).EndInit();
            this.grpSummary.ResumeLayout(false);
            this.grpSummary.PerformLayout();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.GroupBox grpInvoiceInfo;
        private System.Windows.Forms.Label lblInvoiceID;
        private System.Windows.Forms.Label lblInvoiceName;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblCheckoutDate;
        private System.Windows.Forms.GroupBox grpInvoiceDetails;
        private System.Windows.Forms.DataGridView dgvInvoiceDetails;
        private System.Windows.Forms.Label lblTotalItems;
        private System.Windows.Forms.Label lblTotalQuantity;
        private System.Windows.Forms.GroupBox grpSummary;
        private System.Windows.Forms.Label lblSubtotal;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
    }
}
