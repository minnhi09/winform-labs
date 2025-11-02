namespace Lab_Advanced_Command
{
    partial class FoodForm
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
            components = new System.ComponentModel.Container();
            lblTitle = new Label();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            lblSearch = new Label();
            txtSearch = new TextBox();
            dgvFood = new DataGridView();
            contextMenuFood = new ContextMenuStrip(components);
            menuItemAdd = new ToolStripMenuItem();
            menuItemUpdate = new ToolStripMenuItem();
            colID = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colUnit = new DataGridViewTextBoxColumn();
            FoodCategoryID = new DataGridViewTextBoxColumn();
            colCategoryName = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            colNotes = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvFood).BeginInit();
            contextMenuFood.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(14, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(421, 37);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ DANH SÁCH MÓN ĂN";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 10F);
            lblCategory.Location = new Point(14, 80);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(126, 23);
            lblCategory.TabIndex = 1;
            lblCategory.Text = "Nhóm món ăn:";
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Font = new Font("Segoe UI", 10F);
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(147, 76);
            cboCategory.Margin = new Padding(3, 4, 3, 4);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(342, 31);
            cboCategory.TabIndex = 2;
            cboCategory.SelectedIndexChanged += cboCategory_SelectedIndexChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F);
            lblSearch.Location = new Point(510, 80);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(119, 23);
            lblSearch.TabIndex = 4;
            lblSearch.Text = "Tìm theo tên:";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(635, 76);
            txtSearch.Margin = new Padding(3, 4, 3, 4);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nhập tên món ăn...";
            txtSearch.Size = new Size(248, 30);
            txtSearch.TabIndex = 5;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // dgvFood
            // 
            dgvFood.AllowUserToAddRows = false;
            dgvFood.AllowUserToDeleteRows = false;
            dgvFood.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFood.BackgroundColor = SystemColors.Window;
            dgvFood.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFood.Columns.AddRange(new DataGridViewColumn[] { colID, colName, colUnit, FoodCategoryID, colCategoryName, colPrice, colNotes });
            dgvFood.ContextMenuStrip = contextMenuFood;
            dgvFood.Location = new Point(14, 133);
            dgvFood.Margin = new Padding(3, 4, 3, 4);
            dgvFood.Name = "dgvFood";
            dgvFood.ReadOnly = true;
            dgvFood.RowHeadersWidth = 51;
            dgvFood.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFood.Size = new Size(869, 451);
            dgvFood.TabIndex = 3;
            // 
            // contextMenuFood
            // 
            contextMenuFood.ImageScalingSize = new Size(20, 20);
            contextMenuFood.Items.AddRange(new ToolStripItem[] { menuItemAdd, menuItemUpdate });
            contextMenuFood.Name = "contextMenuFood";
            contextMenuFood.Size = new Size(225, 52);
            // 
            // menuItemAdd
            // 
            menuItemAdd.Name = "menuItemAdd";
            menuItemAdd.Size = new Size(224, 24);
            menuItemAdd.Text = "➕ Thêm món ăn mới";
            menuItemAdd.Click += menuItemAdd_Click;
            // 
            // menuItemUpdate
            // 
            menuItemUpdate.Name = "menuItemUpdate";
            menuItemUpdate.Size = new Size(224, 24);
            menuItemUpdate.Text = "✏️ Cập nhật món ăn";
            menuItemUpdate.Click += menuItemUpdate_Click;
            // 
            // colID
            // 
            colID.DataPropertyName = "ID";
            colID.FillWeight = 50F;
            colID.HeaderText = "Mã món";
            colID.MinimumWidth = 6;
            colID.Name = "colID";
            colID.ReadOnly = true;
            // 
            // colName
            // 
            colName.DataPropertyName = "Name";
            colName.FillWeight = 120F;
            colName.HeaderText = "Tên món ăn";
            colName.MinimumWidth = 6;
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colUnit
            // 
            colUnit.DataPropertyName = "Unit";
            colUnit.FillWeight = 60F;
            colUnit.HeaderText = "Đơn vị";
            colUnit.MinimumWidth = 6;
            colUnit.Name = "colUnit";
            colUnit.ReadOnly = true;
            // 
            // FoodCategoryID
            // 
            FoodCategoryID.DataPropertyName = "FoodCategoryID";
            FoodCategoryID.HeaderText = "Mã nhóm";
            FoodCategoryID.MinimumWidth = 6;
            FoodCategoryID.Name = "FoodCategoryID";
            FoodCategoryID.ReadOnly = true;
            // 
            // colCategoryName
            // 
            colCategoryName.DataPropertyName = "CategoryName";
            colCategoryName.FillWeight = 90F;
            colCategoryName.HeaderText = "Nhóm món ăn";
            colCategoryName.MinimumWidth = 6;
            colCategoryName.Name = "colCategoryName";
            colCategoryName.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.DataPropertyName = "Price";
            colPrice.FillWeight = 70F;
            colPrice.HeaderText = "Đơn giá";
            colPrice.MinimumWidth = 6;
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // colNotes
            // 
            colNotes.DataPropertyName = "Notes";
            colNotes.FillWeight = 130F;
            colNotes.HeaderText = "Ghi chú";
            colNotes.MinimumWidth = 6;
            colNotes.Name = "colNotes";
            colNotes.ReadOnly = true;
            // 
            // FoodForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(896, 600);
            Controls.Add(dgvFood);
            Controls.Add(txtSearch);
            Controls.Add(lblSearch);
            Controls.Add(cboCategory);
            Controls.Add(lblCategory);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "FoodForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý món ăn";
            Load += FoodForm_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFood).EndInit();
            contextMenuFood.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblCategory;
        private ComboBox cboCategory;
        private Label lblSearch;
        private TextBox txtSearch;
        private DataGridView dgvFood;
        private ContextMenuStrip contextMenuFood;
        private ToolStripMenuItem menuItemAdd;
        private ToolStripMenuItem menuItemUpdate;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colUnit;
        private DataGridViewTextBoxColumn FoodCategoryID;
        private DataGridViewTextBoxColumn colCategoryName;
        private DataGridViewTextBoxColumn colPrice;
        private DataGridViewTextBoxColumn colNotes;
    }
}
