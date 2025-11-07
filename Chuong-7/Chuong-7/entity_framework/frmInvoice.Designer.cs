namespace entity_framework
{
    partial class frmInvoice
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Panel pnlRight;
        private System.Windows.Forms.GroupBox grpInvoiceList;
        private System.Windows.Forms.GroupBox grpInvoiceDetails;
        private System.Windows.Forms.GroupBox grpActions;
        private System.Windows.Forms.GroupBox grpSummary;
        private System.Windows.Forms.DataGridView dgvInvoices;
        private System.Windows.Forms.ListView lvInvoiceDetails;
        private System.Windows.Forms.ComboBox cboTable;
        private System.Windows.Forms.ComboBox cboFood;
        private System.Windows.Forms.NumericUpDown nudAmount;
        private System.Windows.Forms.NumericUpDown nudDiscount;
        private System.Windows.Forms.NumericUpDown nudTax;
        private System.Windows.Forms.Button btnCreateInvoice;
        private System.Windows.Forms.Button btnAddFood;
        private System.Windows.Forms.Button btnDeleteFood;
        private System.Windows.Forms.Button btnIncreaseAmount;
        private System.Windows.Forms.Button btnDecreaseAmount;
        private System.Windows.Forms.Button btnCheckout;
        private System.Windows.Forms.Button btnDeleteInvoice;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.Label lblFood;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.Label lblDiscount;
        private System.Windows.Forms.Label lblTax;
        private System.Windows.Forms.Label lblSubTotal;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalInvoices;
        private System.Windows.Forms.Label lblDiscountPercent;
        private System.Windows.Forms.Label lblTaxPercent;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblTo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.grpInvoiceList = new System.Windows.Forms.GroupBox();
            this.lblTotalInvoices = new System.Windows.Forms.Label();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.btnDeleteInvoice = new System.Windows.Forms.Button();
            this.dgvInvoices = new System.Windows.Forms.DataGridView();
            this.pnlRight = new System.Windows.Forms.Panel();
            this.grpSummary = new System.Windows.Forms.GroupBox();
            this.btnCheckout = new System.Windows.Forms.Button();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTax = new System.Windows.Forms.Label();
            this.lblDiscount = new System.Windows.Forms.Label();
            this.lblSubTotal = new System.Windows.Forms.Label();
            this.nudTax = new System.Windows.Forms.NumericUpDown();
            this.lblTaxPercent = new System.Windows.Forms.Label();
            this.nudDiscount = new System.Windows.Forms.NumericUpDown();
            this.lblDiscountPercent = new System.Windows.Forms.Label();
            this.grpActions = new System.Windows.Forms.GroupBox();
            this.btnCreateInvoice = new System.Windows.Forms.Button();
            this.cboTable = new System.Windows.Forms.ComboBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.lblFood = new System.Windows.Forms.Label();
            this.cboFood = new System.Windows.Forms.ComboBox();
            this.lblAmount = new System.Windows.Forms.Label();
            this.nudAmount = new System.Windows.Forms.NumericUpDown();
            this.btnAddFood = new System.Windows.Forms.Button();
            this.btnIncreaseAmount = new System.Windows.Forms.Button();
            this.btnDecreaseAmount = new System.Windows.Forms.Button();
            this.btnDeleteFood = new System.Windows.Forms.Button();
            this.grpInvoiceDetails = new System.Windows.Forms.GroupBox();
            this.lvInvoiceDetails = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.grpInvoiceList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.grpSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).BeginInit();
            this.grpActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).BeginInit();
            this.grpInvoiceDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.pnlLeft);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlRight);
            this.splitContainer1.Size = new System.Drawing.Size(2600, 1723);
            this.splitContainer1.SplitterDistance = 1300;
            this.splitContainer1.SplitterWidth = 9;
            this.splitContainer1.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.grpInvoiceList);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.pnlLeft.Size = new System.Drawing.Size(1300, 1723);
            this.pnlLeft.TabIndex = 0;
            // 
            // grpInvoiceList
            // 
            this.grpInvoiceList.Controls.Add(this.lblTotalInvoices);
            this.grpInvoiceList.Controls.Add(this.btnRefresh);
            this.grpInvoiceList.Controls.Add(this.btnSearch);
            this.grpInvoiceList.Controls.Add(this.lblTo);
            this.grpInvoiceList.Controls.Add(this.dtpTo);
            this.grpInvoiceList.Controls.Add(this.lblFrom);
            this.grpInvoiceList.Controls.Add(this.dtpFrom);
            this.grpInvoiceList.Controls.Add(this.btnDeleteInvoice);
            this.grpInvoiceList.Controls.Add(this.dgvInvoices);
            this.grpInvoiceList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInvoiceList.Location = new System.Drawing.Point(11, 12);
            this.grpInvoiceList.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.grpInvoiceList.Name = "grpInvoiceList";
            this.grpInvoiceList.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.grpInvoiceList.Size = new System.Drawing.Size(1278, 1699);
            this.grpInvoiceList.TabIndex = 0;
            this.grpInvoiceList.TabStop = false;
            this.grpInvoiceList.Text = "Danh sách hóa đơn";
            // 
            // lblTotalInvoices
            // 
            this.lblTotalInvoices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTotalInvoices.AutoSize = true;
            this.lblTotalInvoices.Location = new System.Drawing.Point(22, 1601);
            this.lblTotalInvoices.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTotalInvoices.Name = "lblTotalInvoices";
            this.lblTotalInvoices.Size = new System.Drawing.Size(221, 32);
            this.lblTotalInvoices.TabIndex = 8;
            this.lblTotalInvoices.Text = "Tổng số: 0 hóa đơn";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(923, 49);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(162, 57);
            this.btnRefresh.TabIndex = 6;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(748, 49);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(162, 57);
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "Tìm kiếm";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(379, 62);
            this.lblTo.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(122, 32);
            this.lblTo.TabIndex = 3;
            this.lblTo.Text = "Đến ngày:";
            // 
            // dtpTo
            // 
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpTo.Location = new System.Drawing.Point(514, 54);
            this.dtpTo.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(212, 39);
            this.dtpTo.TabIndex = 4;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(22, 62);
            this.lblFrom.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(105, 32);
            this.lblFrom.TabIndex = 1;
            this.lblFrom.Text = "Từ ngày:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Location = new System.Drawing.Point(141, 54);
            this.dtpFrom.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(212, 39);
            this.dtpFrom.TabIndex = 2;
            // 
            // btnDeleteInvoice
            // 
            this.btnDeleteInvoice.Location = new System.Drawing.Point(22, 123);
            this.btnDeleteInvoice.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnDeleteInvoice.Name = "btnDeleteInvoice";
            this.btnDeleteInvoice.Size = new System.Drawing.Size(217, 57);
            this.btnDeleteInvoice.TabIndex = 7;
            this.btnDeleteInvoice.Text = "Xóa hóa đơn";
            this.btnDeleteInvoice.UseVisualStyleBackColor = true;
            this.btnDeleteInvoice.Click += new System.EventHandler(this.btnDeleteInvoice_Click);
            // 
            // dgvInvoices
            // 
            this.dgvInvoices.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvInvoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInvoices.Location = new System.Drawing.Point(22, 197);
            this.dgvInvoices.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.dgvInvoices.Name = "dgvInvoices";
            this.dgvInvoices.RowHeadersWidth = 82;
            this.dgvInvoices.Size = new System.Drawing.Size(1235, 1379);
            this.dgvInvoices.TabIndex = 0;
            this.dgvInvoices.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInvoices_CellContentClick);
            this.dgvInvoices.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvInvoices_CellDoubleClick);
            this.dgvInvoices.SelectionChanged += new System.EventHandler(this.dgvInvoices_SelectionChanged);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.grpInvoiceDetails);
            this.pnlRight.Controls.Add(this.grpActions);
            this.pnlRight.Controls.Add(this.grpSummary);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(11, 12, 11, 12);
            this.pnlRight.Size = new System.Drawing.Size(1291, 1723);
            this.pnlRight.TabIndex = 0;
            // 
            // grpSummary
            // 
            this.grpSummary.Controls.Add(this.btnCheckout);
            this.grpSummary.Controls.Add(this.lblTotal);
            this.grpSummary.Controls.Add(this.lblTax);
            this.grpSummary.Controls.Add(this.lblDiscount);
            this.grpSummary.Controls.Add(this.lblSubTotal);
            this.grpSummary.Controls.Add(this.nudTax);
            this.grpSummary.Controls.Add(this.lblTaxPercent);
            this.grpSummary.Controls.Add(this.nudDiscount);
            this.grpSummary.Controls.Add(this.lblDiscountPercent);
            this.grpSummary.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grpSummary.Location = new System.Drawing.Point(11, 1293);
            this.grpSummary.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.grpSummary.Name = "grpSummary";
            this.grpSummary.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.grpSummary.Size = new System.Drawing.Size(1269, 418);
            this.grpSummary.TabIndex = 2;
            this.grpSummary.TabStop = false;
            this.grpSummary.Text = "Tổng kết";
            // 
            // btnCheckout
            // 
            this.btnCheckout.Enabled = false;
            this.btnCheckout.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnCheckout.Location = new System.Drawing.Point(867, 197);
            this.btnCheckout.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnCheckout.Name = "btnCheckout";
            this.btnCheckout.Size = new System.Drawing.Size(368, 148);
            this.btnCheckout.TabIndex = 8;
            this.btnCheckout.Text = "THANH TOÁN";
            this.btnCheckout.UseVisualStyleBackColor = true;
            this.btnCheckout.Click += new System.EventHandler(this.btnCheckout_Click);
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lblTotal.ForeColor = System.Drawing.Color.Red;
            this.lblTotal.Location = new System.Drawing.Point(22, 308);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(300, 37);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Tổng cộng: 0 VNĐ";
            // 
            // lblTax
            // 
            this.lblTax.AutoSize = true;
            this.lblTax.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTax.Location = new System.Drawing.Point(22, 246);
            this.lblTax.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTax.Name = "lblTax";
            this.lblTax.Size = new System.Drawing.Size(150, 29);
            this.lblTax.TabIndex = 6;
            this.lblTax.Text = "Thuế: 0 VNĐ";
            // 
            // lblDiscount
            // 
            this.lblDiscount.AutoSize = true;
            this.lblDiscount.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDiscount.Location = new System.Drawing.Point(22, 197);
            this.lblDiscount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDiscount.Name = "lblDiscount";
            this.lblDiscount.Size = new System.Drawing.Size(190, 29);
            this.lblDiscount.TabIndex = 5;
            this.lblDiscount.Text = "Giảm giá: 0 VNĐ";
            // 
            // lblSubTotal
            // 
            this.lblSubTotal.AutoSize = true;
            this.lblSubTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblSubTotal.Location = new System.Drawing.Point(22, 135);
            this.lblSubTotal.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSubTotal.Name = "lblSubTotal";
            this.lblSubTotal.Size = new System.Drawing.Size(214, 31);
            this.lblSubTotal.TabIndex = 4;
            this.lblSubTotal.Text = "Tạm tính: 0 VNĐ";
            // 
            // nudTax
            // 
            this.nudTax.Location = new System.Drawing.Point(563, 57);
            this.nudTax.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.nudTax.Name = "nudTax";
            this.nudTax.Size = new System.Drawing.Size(173, 39);
            this.nudTax.TabIndex = 3;
            this.nudTax.ValueChanged += new System.EventHandler(this.nudTax_ValueChanged);
            // 
            // lblTaxPercent
            // 
            this.lblTaxPercent.AutoSize = true;
            this.lblTaxPercent.Location = new System.Drawing.Point(433, 62);
            this.lblTaxPercent.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTaxPercent.Name = "lblTaxPercent";
            this.lblTaxPercent.Size = new System.Drawing.Size(114, 32);
            this.lblTaxPercent.TabIndex = 2;
            this.lblTaxPercent.Text = "Thuế (%):";
            // 
            // nudDiscount
            // 
            this.nudDiscount.Location = new System.Drawing.Point(217, 57);
            this.nudDiscount.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.nudDiscount.Name = "nudDiscount";
            this.nudDiscount.Size = new System.Drawing.Size(173, 39);
            this.nudDiscount.TabIndex = 1;
            this.nudDiscount.ValueChanged += new System.EventHandler(this.nudDiscount_ValueChanged);
            // 
            // lblDiscountPercent
            // 
            this.lblDiscountPercent.AutoSize = true;
            this.lblDiscountPercent.Location = new System.Drawing.Point(22, 62);
            this.lblDiscountPercent.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDiscountPercent.Name = "lblDiscountPercent";
            this.lblDiscountPercent.Size = new System.Drawing.Size(154, 32);
            this.lblDiscountPercent.TabIndex = 0;
            this.lblDiscountPercent.Text = "Giảm giá (%):";
            // 
            // grpActions
            // 
            this.grpActions.Controls.Add(this.btnCreateInvoice);
            this.grpActions.Controls.Add(this.cboTable);
            this.grpActions.Controls.Add(this.lblTable);
            this.grpActions.Controls.Add(this.lblFood);
            this.grpActions.Controls.Add(this.cboFood);
            this.grpActions.Controls.Add(this.lblAmount);
            this.grpActions.Controls.Add(this.nudAmount);
            this.grpActions.Controls.Add(this.btnAddFood);
            this.grpActions.Controls.Add(this.btnIncreaseAmount);
            this.grpActions.Controls.Add(this.btnDecreaseAmount);
            this.grpActions.Controls.Add(this.btnDeleteFood);
            this.grpActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpActions.Location = new System.Drawing.Point(11, 12);
            this.grpActions.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.grpActions.Name = "grpActions";
            this.grpActions.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.grpActions.Size = new System.Drawing.Size(1269, 350);
            this.grpActions.TabIndex = 0;
            this.grpActions.TabStop = false;
            this.grpActions.Text = "Thao tác";
            // 
            // btnCreateInvoice
            // 
            this.btnCreateInvoice.Location = new System.Drawing.Point(628, 49);
            this.btnCreateInvoice.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnCreateInvoice.Name = "btnCreateInvoice";
            this.btnCreateInvoice.Size = new System.Drawing.Size(260, 57);
            this.btnCreateInvoice.TabIndex = 2;
            this.btnCreateInvoice.Text = "Tạo hóa đơn";
            this.btnCreateInvoice.UseVisualStyleBackColor = true;
            this.btnCreateInvoice.Click += new System.EventHandler(this.btnCreateInvoice_Click);
            // 
            // cboTable
            // 
            this.cboTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTable.FormattingEnabled = true;
            this.cboTable.Location = new System.Drawing.Point(173, 54);
            this.cboTable.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.cboTable.Name = "cboTable";
            this.cboTable.Size = new System.Drawing.Size(429, 40);
            this.cboTable.TabIndex = 1;
            // 
            // lblTable
            // 
            this.lblTable.AutoSize = true;
            this.lblTable.Location = new System.Drawing.Point(22, 62);
            this.lblTable.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(59, 32);
            this.lblTable.TabIndex = 0;
            this.lblTable.Text = "Bàn:";
            // 
            // lblFood
            // 
            this.lblFood.AutoSize = true;
            this.lblFood.Location = new System.Drawing.Point(22, 148);
            this.lblFood.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFood.Name = "lblFood";
            this.lblFood.Size = new System.Drawing.Size(102, 32);
            this.lblFood.TabIndex = 3;
            this.lblFood.Text = "Món ăn:";
            // 
            // cboFood
            // 
            this.cboFood.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFood.FormattingEnabled = true;
            this.cboFood.Location = new System.Drawing.Point(173, 140);
            this.cboFood.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.cboFood.Name = "cboFood";
            this.cboFood.Size = new System.Drawing.Size(537, 40);
            this.cboFood.TabIndex = 4;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Location = new System.Drawing.Point(737, 148);
            this.lblAmount.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(115, 32);
            this.lblAmount.TabIndex = 5;
            this.lblAmount.Text = "Số lượng:";
            // 
            // nudAmount
            // 
            this.nudAmount.Location = new System.Drawing.Point(867, 143);
            this.nudAmount.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.nudAmount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudAmount.Name = "nudAmount";
            this.nudAmount.Size = new System.Drawing.Size(130, 39);
            this.nudAmount.TabIndex = 6;
            this.nudAmount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // btnAddFood
            // 
            this.btnAddFood.Enabled = false;
            this.btnAddFood.Location = new System.Drawing.Point(1018, 138);
            this.btnAddFood.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnAddFood.Name = "btnAddFood";
            this.btnAddFood.Size = new System.Drawing.Size(217, 57);
            this.btnAddFood.TabIndex = 7;
            this.btnAddFood.Text = "Thêm món";
            this.btnAddFood.UseVisualStyleBackColor = true;
            this.btnAddFood.Click += new System.EventHandler(this.btnAddFood_Click);
            // 
            // btnIncreaseAmount
            // 
            this.btnIncreaseAmount.Enabled = false;
            this.btnIncreaseAmount.Location = new System.Drawing.Point(173, 246);
            this.btnIncreaseAmount.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnIncreaseAmount.Name = "btnIncreaseAmount";
            this.btnIncreaseAmount.Size = new System.Drawing.Size(217, 74);
            this.btnIncreaseAmount.TabIndex = 8;
            this.btnIncreaseAmount.Text = "Tăng SL";
            this.btnIncreaseAmount.UseVisualStyleBackColor = true;
            this.btnIncreaseAmount.Click += new System.EventHandler(this.btnIncreaseAmount_Click);
            // 
            // btnDecreaseAmount
            // 
            this.btnDecreaseAmount.Enabled = false;
            this.btnDecreaseAmount.Location = new System.Drawing.Point(412, 246);
            this.btnDecreaseAmount.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnDecreaseAmount.Name = "btnDecreaseAmount";
            this.btnDecreaseAmount.Size = new System.Drawing.Size(217, 74);
            this.btnDecreaseAmount.TabIndex = 9;
            this.btnDecreaseAmount.Text = "Giảm SL";
            this.btnDecreaseAmount.UseVisualStyleBackColor = true;
            this.btnDecreaseAmount.Click += new System.EventHandler(this.btnDecreaseAmount_Click);
            // 
            // btnDeleteFood
            // 
            this.btnDeleteFood.Enabled = false;
            this.btnDeleteFood.Location = new System.Drawing.Point(650, 246);
            this.btnDeleteFood.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.btnDeleteFood.Name = "btnDeleteFood";
            this.btnDeleteFood.Size = new System.Drawing.Size(217, 74);
            this.btnDeleteFood.TabIndex = 10;
            this.btnDeleteFood.Text = "Xóa món";
            this.btnDeleteFood.UseVisualStyleBackColor = true;
            this.btnDeleteFood.Click += new System.EventHandler(this.btnDeleteFood_Click);
            // 
            // grpInvoiceDetails
            // 
            this.grpInvoiceDetails.Controls.Add(this.lvInvoiceDetails);
            this.grpInvoiceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpInvoiceDetails.Location = new System.Drawing.Point(11, 362);
            this.grpInvoiceDetails.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.grpInvoiceDetails.Name = "grpInvoiceDetails";
            this.grpInvoiceDetails.Padding = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.grpInvoiceDetails.Size = new System.Drawing.Size(1269, 931);
            this.grpInvoiceDetails.TabIndex = 1;
            this.grpInvoiceDetails.TabStop = false;
            this.grpInvoiceDetails.Text = "Chi tiết hóa đơn";
            // 
            // lvInvoiceDetails
            // 
            this.lvInvoiceDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvInvoiceDetails.FullRowSelect = true;
            this.lvInvoiceDetails.GridLines = true;
            this.lvInvoiceDetails.HideSelection = false;
            this.lvInvoiceDetails.Location = new System.Drawing.Point(6, 39);
            this.lvInvoiceDetails.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.lvInvoiceDetails.Name = "lvInvoiceDetails";
            this.lvInvoiceDetails.Size = new System.Drawing.Size(1257, 873);
            this.lvInvoiceDetails.TabIndex = 0;
            this.lvInvoiceDetails.UseCompatibleStateImageBehavior = false;
            this.lvInvoiceDetails.View = System.Windows.Forms.View.Details;
            // 
            // frmInvoice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2600, 1723);
            this.Controls.Add(this.splitContainer1);
            this.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.Name = "frmInvoice";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý hóa đơn";
            this.Load += new System.EventHandler(this.frmInvoice_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.grpInvoiceList.ResumeLayout(false);
            this.grpInvoiceList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInvoices)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.grpSummary.ResumeLayout(false);
            this.grpSummary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudDiscount)).EndInit();
            this.grpActions.ResumeLayout(false);
            this.grpActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudAmount)).EndInit();
            this.grpInvoiceDetails.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}