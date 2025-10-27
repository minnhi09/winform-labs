namespace Lab_Basic_Command
{
    partial class BillManager
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.btnClearFilter = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.chkFilterDate = new System.Windows.Forms.CheckBox();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lvBills = new System.Windows.Forms.ListView();
            this.chID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAccount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCheckIn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chCheckOut = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chTotalAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDiscount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFinalAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmViewDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.lvBillDetails = new System.Windows.Forms.ListView();
            this.chDetailID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFoodName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chQuantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPrice = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAmount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chNotes = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnViewDetail = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblStatTotalAmount = new System.Windows.Forms.Label();
            this.lblStatDiscount = new System.Windows.Forms.Label();
            this.lblStatFinalAmount = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.btnClearFilter);
            this.groupBox1.Controls.Add(this.btnLoad);
            this.groupBox1.Controls.Add(this.dtpTo);
            this.groupBox1.Controls.Add(this.dtpFrom);
            this.groupBox1.Controls.Add(this.chkFilterDate);
            this.groupBox1.Controls.Add(this.cboStatus);
            this.groupBox1.Controls.Add(this.cboTable);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1326, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bộ lọc";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(1190, 54);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(120, 36);
            this.btnExport.TabIndex = 7;
            this.btnExport.Text = "Xuất báo cáo";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnClearFilter
            // 
            this.btnClearFilter.Location = new System.Drawing.Point(1190, 18);
            this.btnClearFilter.Name = "btnClearFilter";
            this.btnClearFilter.Size = new System.Drawing.Size(120, 36);
            this.btnClearFilter.TabIndex = 6;
            this.btnClearFilter.Text = "Xóa bộ lọc";
            this.btnClearFilter.UseVisualStyleBackColor = true;
            this.btnClearFilter.Click += new System.EventHandler(this.btnClearFilter_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(1050, 18);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(120, 72);
            this.btnLoad.TabIndex = 5;
            this.btnLoad.Text = "Tải dữ liệu";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.Enabled = false;
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(883, 58);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(150, 22);
            this.dtpTo.TabIndex = 4;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Enabled = false;
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(683, 58);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(150, 22);
            this.dtpFrom.TabIndex = 3;
            // 
            // chkFilterDate
            // 
            this.chkFilterDate.AutoSize = true;
            this.chkFilterDate.Location = new System.Drawing.Point(583, 60);
            this.chkFilterDate.Name = "chkFilterDate";
            this.chkFilterDate.Size = new System.Drawing.Size(97, 20);
            this.chkFilterDate.TabIndex = 2;
            this.chkFilterDate.Text = "Lọc ngày từ";
            this.chkFilterDate.UseVisualStyleBackColor = true;
            this.chkFilterDate.CheckedChanged += new System.EventHandler(this.chkFilterDate_CheckedChanged);
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "-- Tất cả --",
            "Chưa thanh toán",
            "Đã thanh toán"});
            this.cboStatus.Location = new System.Drawing.Point(370, 57);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(200, 24);
            this.cboStatus.TabIndex = 1;
            // 
            // cboTable
            // 
            this.cboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new System.Drawing.Point(370, 25);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(200, 24);
            this.cboTable.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(839, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 16);
            this.label3.TabIndex = 0;
            this.label3.Text = "Đến";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(280, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 0;
            this.label2.Text = "Trạng thái:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(280, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bàn:";
            // 
            // lvBills
            // 
            this.lvBills.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chID,
            this.chTable,
            this.chAccount,
            this.chCheckIn,
            this.chCheckOut,
            this.chStatus,
            this.chTotalAmount,
            this.chDiscount,
            this.chFinalAmount});
            this.lvBills.ContextMenuStrip = this.contextMenuStrip1;
            this.lvBills.FullRowSelect = true;
            this.lvBills.GridLines = true;
            this.lvBills.HideSelection = false;
            this.lvBills.Location = new System.Drawing.Point(12, 143);
            this.lvBills.MultiSelect = false;
            this.lvBills.Name = "lvBills";
            this.lvBills.Size = new System.Drawing.Size(1326, 300);
            this.lvBills.TabIndex = 1;
            this.lvBills.UseCompatibleStateImageBehavior = false;
            this.lvBills.View = System.Windows.Forms.View.Details;
            this.lvBills.Click += new System.EventHandler(this.lvBills_Click);
            this.lvBills.DoubleClick += new System.EventHandler(this.lvBills_DoubleClick);
            // 
            // chID
            // 
            this.chID.Text = "ID";
            this.chID.Width = 50;
            // 
            // chTable
            // 
            this.chTable.Text = "Bàn";
            this.chTable.Width = 120;
            // 
            // chAccount
            // 
            this.chAccount.Text = "Nhân viên";
            this.chAccount.Width = 150;
            // 
            // chCheckIn
            // 
            this.chCheckIn.Text = "Giờ vào";
            this.chCheckIn.Width = 150;
            // 
            // chCheckOut
            // 
            this.chCheckOut.Text = "Giờ ra";
            this.chCheckOut.Width = 150;
            // 
            // chStatus
            // 
            this.chStatus.Text = "Trạng thái";
            this.chStatus.Width = 120;
            // 
            // chTotalAmount
            // 
            this.chTotalAmount.Text = "Tổng tiền";
            this.chTotalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chTotalAmount.Width = 120;
            // 
            // chDiscount
            // 
            this.chDiscount.Text = "Giảm giá (%)";
            this.chDiscount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chDiscount.Width = 100;
            // 
            // chFinalAmount
            // 
            this.chFinalAmount.Text = "Thành tiền";
            this.chFinalAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chFinalAmount.Width = 120;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmViewDetail});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(158, 28);
            // 
            // tsmViewDetail
            // 
            this.tsmViewDetail.Name = "tsmViewDetail";
            this.tsmViewDetail.Size = new System.Drawing.Size(157, 24);
            this.tsmViewDetail.Text = "Xem chi tiết";
            this.tsmViewDetail.Click += new System.EventHandler(this.tsmViewDetail_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lblTotalAmount);
            this.groupBox2.Controls.Add(this.lvBillDetails);
            this.groupBox2.Location = new System.Drawing.Point(12, 525);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1326, 200);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chi tiết hóa đơn";
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalAmount.ForeColor = System.Drawing.Color.Red;
            this.lblTotalAmount.Location = new System.Drawing.Point(1050, 168);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(93, 20);
            this.lblTotalAmount.TabIndex = 1;
            this.lblTotalAmount.Text = "Tổng tiền:";
            // 
            // lvBillDetails
            // 
            this.lvBillDetails.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chDetailID,
            this.chFoodName,
            this.chQuantity,
            this.chPrice,
            this.chAmount,
            this.chNotes});
            this.lvBillDetails.FullRowSelect = true;
            this.lvBillDetails.GridLines = true;
            this.lvBillDetails.HideSelection = false;
            this.lvBillDetails.Location = new System.Drawing.Point(16, 25);
            this.lvBillDetails.Name = "lvBillDetails";
            this.lvBillDetails.Size = new System.Drawing.Size(1294, 130);
            this.lvBillDetails.TabIndex = 0;
            this.lvBillDetails.UseCompatibleStateImageBehavior = false;
            this.lvBillDetails.View = System.Windows.Forms.View.Details;
            // 
            // chDetailID
            // 
            this.chDetailID.Text = "ID";
            this.chDetailID.Width = 50;
            // 
            // chFoodName
            // 
            this.chFoodName.Text = "Tên món";
            this.chFoodName.Width = 300;
            // 
            // chQuantity
            // 
            this.chQuantity.Text = "Số lượng";
            this.chQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chQuantity.Width = 100;
            // 
            // chPrice
            // 
            this.chPrice.Text = "Đơn giá";
            this.chPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chPrice.Width = 120;
            // 
            // chAmount
            // 
            this.chAmount.Text = "Thành tiền";
            this.chAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.chAmount.Width = 120;
            // 
            // chNotes
            // 
            this.chNotes.Text = "Ghi chú";
            this.chNotes.Width = 400;
            // 
            // btnViewDetail
            // 
            this.btnViewDetail.Enabled = false;
            this.btnViewDetail.Location = new System.Drawing.Point(1218, 449);
            this.btnViewDetail.Name = "btnViewDetail";
            this.btnViewDetail.Size = new System.Drawing.Size(120, 36);
            this.btnViewDetail.TabIndex = 3;
            this.btnViewDetail.Text = "Xem chi tiết";
            this.btnViewDetail.UseVisualStyleBackColor = true;
            this.btnViewDetail.Click += new System.EventHandler(this.btnViewDetail_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(12, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 18);
            this.label4.TabIndex = 4;
            this.label4.Text = "Danh sách hóa đơn";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.lblStatTotalAmount);
            this.groupBox3.Controls.Add(this.lblStatDiscount);
            this.groupBox3.Controls.Add(this.lblStatFinalAmount);
            this.groupBox3.Location = new System.Drawing.Point(12, 449);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1200, 65);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thống kê";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 28);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(178, 18);
            this.label5.TabIndex = 0;
            this.label5.Text = "Tổng tiền (chưa giảm giá):";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(420, 28);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(132, 18);
            this.label6.TabIndex = 2;
            this.label6.Text = "Tổng tiền giảm giá:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(820, 28);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 20);
            this.label7.TabIndex = 4;
            this.label7.Text = "Thực thu:";
            // 
            // lblStatTotalAmount
            // 
            this.lblStatTotalAmount.AutoSize = true;
            this.lblStatTotalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatTotalAmount.ForeColor = System.Drawing.Color.Blue;
            this.lblStatTotalAmount.Location = new System.Drawing.Point(228, 28);
            this.lblStatTotalAmount.Name = "lblStatTotalAmount";
            this.lblStatTotalAmount.Size = new System.Drawing.Size(17, 18);
            this.lblStatTotalAmount.TabIndex = 1;
            this.lblStatTotalAmount.Text = "0";
            // 
            // lblStatDiscount
            // 
            this.lblStatDiscount.AutoSize = true;
            this.lblStatDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatDiscount.ForeColor = System.Drawing.Color.Orange;
            this.lblStatDiscount.Location = new System.Drawing.Point(570, 28);
            this.lblStatDiscount.Name = "lblStatDiscount";
            this.lblStatDiscount.Size = new System.Drawing.Size(17, 18);
            this.lblStatDiscount.TabIndex = 3;
            this.lblStatDiscount.Text = "0";
            // 
            // lblStatFinalAmount
            // 
            this.lblStatFinalAmount.AutoSize = true;
            this.lblStatFinalAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatFinalAmount.ForeColor = System.Drawing.Color.Red;
            this.lblStatFinalAmount.Location = new System.Drawing.Point(930, 26);
            this.lblStatFinalAmount.Name = "lblStatFinalAmount";
            this.lblStatFinalAmount.Size = new System.Drawing.Size(24, 25);
            this.lblStatFinalAmount.TabIndex = 5;
            this.lblStatFinalAmount.Text = "0";
            // 
            // BillManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1350, 736);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnViewDetail);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.lvBills);
            this.Controls.Add(this.groupBox1);
            this.Name = "BillManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý hóa đơn";
            this.Load += new System.EventHandler(this.BillManager_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkFilterDate;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.ListView lvBills;
        private System.Windows.Forms.ColumnHeader chID;
        private System.Windows.Forms.ColumnHeader chTable;
        private System.Windows.Forms.ColumnHeader chAccount;
        private System.Windows.Forms.ColumnHeader chCheckIn;
        private System.Windows.Forms.ColumnHeader chCheckOut;
        private System.Windows.Forms.ColumnHeader chStatus;
        private System.Windows.Forms.ColumnHeader chTotalAmount;
        private System.Windows.Forms.ColumnHeader chDiscount;
        private System.Windows.Forms.ColumnHeader chFinalAmount;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lvBillDetails;
        private System.Windows.Forms.ColumnHeader chDetailID;
        private System.Windows.Forms.ColumnHeader chFoodName;
        private System.Windows.Forms.ColumnHeader chQuantity;
        private System.Windows.Forms.ColumnHeader chPrice;
        private System.Windows.Forms.ColumnHeader chAmount;
        private System.Windows.Forms.ColumnHeader chNotes;
        private System.Windows.Forms.Button btnViewDetail;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.Button btnClearFilter;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmViewDetail;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblStatTotalAmount;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblStatDiscount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblStatFinalAmount;
    }
}
