namespace Lab_Advanced_Command
{
    partial class OrderDetailsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.grpBillInfo = new System.Windows.Forms.GroupBox();
            this.lblStatusValue = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblCheckOutValue = new System.Windows.Forms.Label();
            this.lblCheckOut = new System.Windows.Forms.Label();
            this.lblCheckInValue = new System.Windows.Forms.Label();
            this.lblCheckIn = new System.Windows.Forms.Label();
            this.lblAccountValue = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.lblBillIdValue = new System.Windows.Forms.Label();
            this.lblBillId = new System.Windows.Forms.Label();
            this.grpItems = new System.Windows.Forms.GroupBox();
            this.dgvItems = new System.Windows.Forms.DataGridView();
            this.colItemID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFoodName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCategory = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colQuantity = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelItemInfo = new System.Windows.Forms.Panel();
            this.lblTotalQuantity = new System.Windows.Forms.Label();
            this.lblItemCount = new System.Windows.Forms.Label();
            this.grpPayment = new System.Windows.Forms.GroupBox();
            this.lblFinalAmountValue = new System.Windows.Forms.Label();
            this.lblFinalAmount = new System.Windows.Forms.Label();
            this.lblDiscountValue = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblTotalAmountValue = new System.Windows.Forms.Label();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.panelHeader.SuspendLayout();
            this.grpBillInfo.SuspendLayout();
            this.grpItems.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).BeginInit();
            this.panelItemInfo.SuspendLayout();
            this.grpPayment.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 70);
            this.panelHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1000, 70);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🧾 CHI TIẾT HÓA ĐƠN";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // grpBillInfo
            // 
            this.grpBillInfo.Controls.Add(this.lblStatusValue);
            this.grpBillInfo.Controls.Add(this.lblStatus);
            this.grpBillInfo.Controls.Add(this.lblCheckOutValue);
            this.grpBillInfo.Controls.Add(this.lblCheckOut);
            this.grpBillInfo.Controls.Add(this.lblCheckInValue);
            this.grpBillInfo.Controls.Add(this.lblCheckIn);
            this.grpBillInfo.Controls.Add(this.lblAccountValue);
            this.grpBillInfo.Controls.Add(this.lblAccount);
            this.grpBillInfo.Controls.Add(this.lblBillIdValue);
            this.grpBillInfo.Controls.Add(this.lblBillId);
            this.grpBillInfo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpBillInfo.Location = new System.Drawing.Point(15, 85);
            this.grpBillInfo.Name = "grpBillInfo";
            this.grpBillInfo.Size = new System.Drawing.Size(970, 140);
            this.grpBillInfo.TabIndex = 1;
            this.grpBillInfo.TabStop = false;
            this.grpBillInfo.Text = "Thông tin chung";
            // 
            // lblStatusValue
            // 
            this.lblStatusValue.AutoSize = true;
            this.lblStatusValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblStatusValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblStatusValue.Location = new System.Drawing.Point(780, 100);
            this.lblStatusValue.Name = "lblStatusValue";
            this.lblStatusValue.Size = new System.Drawing.Size(125, 20);
            this.lblStatusValue.TabIndex = 9;
            this.lblStatusValue.Text = "Đã thanh toán";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatus.Location = new System.Drawing.Point(680, 100);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(77, 19);
            this.lblStatus.TabIndex = 8;
            this.lblStatus.Text = "Trạng thái:";
            // 
            // lblCheckOutValue
            // 
            this.lblCheckOutValue.AutoSize = true;
            this.lblCheckOutValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCheckOutValue.Location = new System.Drawing.Point(780, 65);
            this.lblCheckOutValue.Name = "lblCheckOutValue";
            this.lblCheckOutValue.Size = new System.Drawing.Size(145, 19);
            this.lblCheckOutValue.TabIndex = 7;
            this.lblCheckOutValue.Text = "02/11/2025 14:30:00";
            // 
            // lblCheckOut
            // 
            this.lblCheckOut.AutoSize = true;
            this.lblCheckOut.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCheckOut.Location = new System.Drawing.Point(680, 65);
            this.lblCheckOut.Name = "lblCheckOut";
            this.lblCheckOut.Size = new System.Drawing.Size(64, 19);
            this.lblCheckOut.TabIndex = 6;
            this.lblCheckOut.Text = "Ngày ra:";
            // 
            // lblCheckInValue
            // 
            this.lblCheckInValue.AutoSize = true;
            this.lblCheckInValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCheckInValue.Location = new System.Drawing.Point(780, 30);
            this.lblCheckInValue.Name = "lblCheckInValue";
            this.lblCheckInValue.Size = new System.Drawing.Size(145, 19);
            this.lblCheckInValue.TabIndex = 5;
            this.lblCheckInValue.Text = "02/11/2025 12:00:00";
            // 
            // lblCheckIn
            // 
            this.lblCheckIn.AutoSize = true;
            this.lblCheckIn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblCheckIn.Location = new System.Drawing.Point(680, 30);
            this.lblCheckIn.Name = "lblCheckIn";
            this.lblCheckIn.Size = new System.Drawing.Size(75, 19);
            this.lblCheckIn.TabIndex = 4;
            this.lblCheckIn.Text = "Ngày vào:";
            // 
            // lblAccountValue
            // 
            this.lblAccountValue.AutoSize = true;
            this.lblAccountValue.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAccountValue.Location = new System.Drawing.Point(160, 65);
            this.lblAccountValue.Name = "lblAccountValue";
            this.lblAccountValue.Size = new System.Drawing.Size(187, 19);
            this.lblAccountValue.TabIndex = 3;
            this.lblAccountValue.Text = "Nguyễn Văn An (admin)";
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAccount.Location = new System.Drawing.Point(30, 65);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(82, 19);
            this.lblAccount.TabIndex = 2;
            this.lblAccount.Text = "Người tạo:";
            // 
            // lblBillIdValue
            // 
            this.lblBillIdValue.AutoSize = true;
            this.lblBillIdValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblBillIdValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblBillIdValue.Location = new System.Drawing.Point(160, 30);
            this.lblBillIdValue.Name = "lblBillIdValue";
            this.lblBillIdValue.Size = new System.Drawing.Size(25, 20);
            this.lblBillIdValue.TabIndex = 1;
            this.lblBillIdValue.Text = "01";
            // 
            // lblBillId
            // 
            this.lblBillId.AutoSize = true;
            this.lblBillId.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblBillId.Location = new System.Drawing.Point(30, 30);
            this.lblBillId.Name = "lblBillId";
            this.lblBillId.Size = new System.Drawing.Size(90, 19);
            this.lblBillId.TabIndex = 0;
            this.lblBillId.Text = "Mã hóa đơn:";
            // 
            // grpItems
            // 
            this.grpItems.Controls.Add(this.dgvItems);
            this.grpItems.Controls.Add(this.panelItemInfo);
            this.grpItems.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpItems.Location = new System.Drawing.Point(15, 235);
            this.grpItems.Name = "grpItems";
            this.grpItems.Size = new System.Drawing.Size(970, 350);
            this.grpItems.TabIndex = 2;
            this.grpItems.TabStop = false;
            this.grpItems.Text = "Danh sách món ăn";
            // 
            // dgvItems
            // 
            this.dgvItems.AllowUserToAddRows = false;
            this.dgvItems.AllowUserToDeleteRows = false;
            this.dgvItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colItemID,
            this.colFoodName,
            this.colCategory,
            this.colUnit,
            this.colQuantity,
            this.colPrice,
            this.colTotalPrice});
            this.dgvItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItems.Location = new System.Drawing.Point(3, 22);
            this.dgvItems.Name = "dgvItems";
            this.dgvItems.ReadOnly = true;
            this.dgvItems.RowTemplate.Height = 25;
            this.dgvItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItems.Size = new System.Drawing.Size(964, 285);
            this.dgvItems.TabIndex = 1;
            // 
            // colItemID
            // 
            this.colItemID.DataPropertyName = "ID";
            this.colItemID.FillWeight = 50F;
            this.colItemID.HeaderText = "ID";
            this.colItemID.Name = "colItemID";
            this.colItemID.ReadOnly = true;
            // 
            // colFoodName
            // 
            this.colFoodName.DataPropertyName = "FoodName";
            this.colFoodName.FillWeight = 200F;
            this.colFoodName.HeaderText = "Tên món ăn";
            this.colFoodName.Name = "colFoodName";
            this.colFoodName.ReadOnly = true;
            // 
            // colCategory
            // 
            this.colCategory.DataPropertyName = "CategoryName";
            this.colCategory.FillWeight = 120F;
            this.colCategory.HeaderText = "Loại món";
            this.colCategory.Name = "colCategory";
            this.colCategory.ReadOnly = true;
            // 
            // colUnit
            // 
            this.colUnit.DataPropertyName = "Unit";
            this.colUnit.FillWeight = 80F;
            this.colUnit.HeaderText = "Đơn vị";
            this.colUnit.Name = "colUnit";
            this.colUnit.ReadOnly = true;
            // 
            // colQuantity
            // 
            this.colQuantity.DataPropertyName = "Quantity";
            this.colQuantity.FillWeight = 70F;
            this.colQuantity.HeaderText = "Số lượng";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.ReadOnly = true;
            // 
            // colPrice
            // 
            this.colPrice.DataPropertyName = "Price";
            this.colPrice.FillWeight = 100F;
            this.colPrice.HeaderText = "Đơn giá";
            this.colPrice.Name = "colPrice";
            this.colPrice.ReadOnly = true;
            // 
            // colTotalPrice
            // 
            this.colTotalPrice.DataPropertyName = "TotalPrice";
            this.colTotalPrice.FillWeight = 100F;
            this.colTotalPrice.HeaderText = "Thành tiền";
            this.colTotalPrice.Name = "colTotalPrice";
            this.colTotalPrice.ReadOnly = true;
            // 
            // panelItemInfo
            // 
            this.panelItemInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.panelItemInfo.Controls.Add(this.lblTotalQuantity);
            this.panelItemInfo.Controls.Add(this.lblItemCount);
            this.panelItemInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelItemInfo.Location = new System.Drawing.Point(3, 307);
            this.panelItemInfo.Name = "panelItemInfo";
            this.panelItemInfo.Size = new System.Drawing.Size(964, 40);
            this.panelItemInfo.TabIndex = 0;
            // 
            // lblTotalQuantity
            // 
            this.lblTotalQuantity.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotalQuantity.Location = new System.Drawing.Point(500, 10);
            this.lblTotalQuantity.Name = "lblTotalQuantity";
            this.lblTotalQuantity.Size = new System.Drawing.Size(450, 20);
            this.lblTotalQuantity.TabIndex = 1;
            this.lblTotalQuantity.Text = "Tổng số lượng: 0";
            this.lblTotalQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblItemCount
            // 
            this.lblItemCount.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblItemCount.Location = new System.Drawing.Point(10, 10);
            this.lblItemCount.Name = "lblItemCount";
            this.lblItemCount.Size = new System.Drawing.Size(450, 20);
            this.lblItemCount.TabIndex = 0;
            this.lblItemCount.Text = "Tổng số món: 0";
            // 
            // grpPayment
            // 
            this.grpPayment.Controls.Add(this.lblFinalAmountValue);
            this.grpPayment.Controls.Add(this.lblFinalAmount);
            this.grpPayment.Controls.Add(this.lblDiscountValue);
            this.grpPayment.Controls.Add(this.lblDiscount);
            this.grpPayment.Controls.Add(this.lblTotalAmountValue);
            this.grpPayment.Controls.Add(this.lblTotalAmount);
            this.grpPayment.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.grpPayment.Location = new System.Drawing.Point(15, 595);
            this.grpPayment.Name = "grpPayment";
            this.grpPayment.Size = new System.Drawing.Size(970, 100);
            this.grpPayment.TabIndex = 3;
            this.grpPayment.TabStop = false;
            this.grpPayment.Text = "Thanh toán";
            // 
            // lblFinalAmountValue
            // 
            this.lblFinalAmountValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFinalAmountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblFinalAmountValue.Location = new System.Drawing.Point(680, 30);
            this.lblFinalAmountValue.Name = "lblFinalAmountValue";
            this.lblFinalAmountValue.Size = new System.Drawing.Size(270, 50);
            this.lblFinalAmountValue.TabIndex = 5;
            this.lblFinalAmountValue.Text = "0 đ";
            this.lblFinalAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblFinalAmount
            // 
            this.lblFinalAmount.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblFinalAmount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblFinalAmount.Location = new System.Drawing.Point(680, 10);
            this.lblFinalAmount.Name = "lblFinalAmount";
            this.lblFinalAmount.Size = new System.Drawing.Size(270, 25);
            this.lblFinalAmount.TabIndex = 4;
            this.lblFinalAmount.Text = "💰 TỔNG THỰC THU:";
            // 
            // lblDiscountValue
            // 
            this.lblDiscountValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDiscountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblDiscountValue.Location = new System.Drawing.Point(360, 55);
            this.lblDiscountValue.Name = "lblDiscountValue";
            this.lblDiscountValue.Size = new System.Drawing.Size(200, 25);
            this.lblDiscountValue.TabIndex = 3;
            this.lblDiscountValue.Text = "0 đ";
            this.lblDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDiscount.Location = new System.Drawing.Point(360, 30);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(104, 20);
            this.lblDiscount.TabIndex = 2;
            this.lblDiscount.Text = "🎟️ Giảm giá:";
            // 
            // lblTotalAmountValue
            // 
            this.lblTotalAmountValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTotalAmountValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.lblTotalAmountValue.Location = new System.Drawing.Point(30, 55);
            this.lblTotalAmountValue.Name = "lblTotalAmountValue";
            this.lblTotalAmountValue.Size = new System.Drawing.Size(200, 25);
            this.lblTotalAmountValue.TabIndex = 1;
            this.lblTotalAmountValue.Text = "0 đ";
            this.lblTotalAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTotalAmount.Location = new System.Drawing.Point(30, 30);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(163, 20);
            this.lblTotalAmount.TabIndex = 0;
            this.lblTotalAmount.Text = "💵 Tổng tiền chưa giảm:";
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.btnClose);
            this.panelButtons.Controls.Add(this.btnExport);
            this.panelButtons.Controls.Add(this.btnPrint);
            this.panelButtons.Location = new System.Drawing.Point(15, 705);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(970, 50);
            this.panelButtons.TabIndex = 4;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnClose.ForeColor = System.Drawing.Color.White;
            this.btnClose.Location = new System.Drawing.Point(810, 5);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(150, 40);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "❌ Đóng";
            this.btnClose.UseVisualStyleBackColor = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExport.ForeColor = System.Drawing.Color.White;
            this.btnExport.Location = new System.Drawing.Point(410, 5);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(180, 40);
            this.btnExport.TabIndex = 1;
            this.btnExport.Text = "📄 Xuất hóa đơn";
            this.btnExport.UseVisualStyleBackColor = false;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.btnPrint.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnPrint.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnPrint.ForeColor = System.Drawing.Color.White;
            this.btnPrint.Location = new System.Drawing.Point(10, 5);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(180, 40);
            this.btnPrint.TabIndex = 0;
            this.btnPrint.Text = "🖨️ In hóa đơn";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // OrderDetailsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 770);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.grpPayment);
            this.Controls.Add(this.grpItems);
            this.Controls.Add(this.grpBillInfo);
            this.Controls.Add(this.panelHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "OrderDetailsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chi tiết hóa đơn";
            this.Load += new System.EventHandler(this.OrderDetailsForm_Load);
            this.panelHeader.ResumeLayout(false);
            this.grpBillInfo.ResumeLayout(false);
            this.grpBillInfo.PerformLayout();
            this.grpItems.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItems)).EndInit();
            this.panelItemInfo.ResumeLayout(false);
            this.grpPayment.ResumeLayout(false);
            this.grpPayment.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.GroupBox grpBillInfo;
        private System.Windows.Forms.Label lblBillId;
        private System.Windows.Forms.Label lblBillIdValue;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Label lblAccountValue;
        private System.Windows.Forms.Label lblCheckIn;
        private System.Windows.Forms.Label lblCheckInValue;
        private System.Windows.Forms.Label lblCheckOut;
        private System.Windows.Forms.Label lblCheckOutValue;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblStatusValue;
        private System.Windows.Forms.GroupBox grpItems;
        private System.Windows.Forms.DataGridView dgvItems;
        private System.Windows.Forms.Panel panelItemInfo;
        private System.Windows.Forms.Label lblItemCount;
        private System.Windows.Forms.Label lblTotalQuantity;
        private System.Windows.Forms.GroupBox grpPayment;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Label lblTotalAmountValue;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblDiscountValue;
        private System.Windows.Forms.Label lblFinalAmount;
        private System.Windows.Forms.Label lblFinalAmountValue;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn colItemID;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFoodName;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCategory;
        private System.Windows.Forms.DataGridViewTextBoxColumn colUnit;
        private System.Windows.Forms.DataGridViewTextBoxColumn colQuantity;
        private System.Windows.Forms.DataGridViewTextBoxColumn colPrice;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalPrice;
    }
}
