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
            lblTitle = new Label();
            lblCategory = new Label();
            cboCategory = new ComboBox();
            dgvFood = new DataGridView();
            colFoodId = new DataGridViewTextBoxColumn();
            colFoodName = new DataGridViewTextBoxColumn();
            colCategoryName = new DataGridViewTextBoxColumn();
            colPrice = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvFood).BeginInit();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTitle.Location = new Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(279, 30);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ DANH SÁCH MÓN ĂN";
            // 
            // lblCategory
            // 
            lblCategory.AutoSize = true;
            lblCategory.Font = new Font("Segoe UI", 10F);
            lblCategory.Location = new Point(12, 60);
            lblCategory.Name = "lblCategory";
            lblCategory.Size = new Size(111, 19);
            lblCategory.TabIndex = 1;
            lblCategory.Text = "Nhóm món ăn:";
            // 
            // cboCategory
            // 
            cboCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategory.Font = new Font("Segoe UI", 10F);
            cboCategory.FormattingEnabled = true;
            cboCategory.Location = new Point(129, 57);
            cboCategory.Name = "cboCategory";
            cboCategory.Size = new Size(300, 25);
            cboCategory.TabIndex = 2;
            // 
            // dgvFood
            // 
            dgvFood.AllowUserToAddRows = false;
            dgvFood.AllowUserToDeleteRows = false;
            dgvFood.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvFood.BackgroundColor = SystemColors.Window;
            dgvFood.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFood.Columns.AddRange(new DataGridViewColumn[] { colFoodId, colFoodName, colCategoryName, colPrice });
            dgvFood.Location = new Point(12, 100);
            dgvFood.Name = "dgvFood";
            dgvFood.ReadOnly = true;
            dgvFood.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFood.Size = new Size(760, 338);
            dgvFood.TabIndex = 3;
            // 
            // colFoodId
            // 
            colFoodId.DataPropertyName = "FoodId";
            colFoodId.FillWeight = 60F;
            colFoodId.HeaderText = "Mã món";
            colFoodId.Name = "colFoodId";
            colFoodId.ReadOnly = true;
            // 
            // colFoodName
            // 
            colFoodName.DataPropertyName = "FoodName";
            colFoodName.FillWeight = 120F;
            colFoodName.HeaderText = "Tên món ăn";
            colFoodName.Name = "colFoodName";
            colFoodName.ReadOnly = true;
            // 
            // colCategoryName
            // 
            colCategoryName.DataPropertyName = "CategoryName";
            colCategoryName.FillWeight = 100F;
            colCategoryName.HeaderText = "Nhóm món ăn";
            colCategoryName.Name = "colCategoryName";
            colCategoryName.ReadOnly = true;
            // 
            // colPrice
            // 
            colPrice.DataPropertyName = "Price";
            colPrice.FillWeight = 80F;
            colPrice.HeaderText = "Giá";
            colPrice.Name = "colPrice";
            colPrice.ReadOnly = true;
            // 
            // FoodForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(784, 450);
            Controls.Add(dgvFood);
            Controls.Add(cboCategory);
            Controls.Add(lblCategory);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FoodForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý món ăn";
            ((System.ComponentModel.ISupportInitialize)dgvFood).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblTitle;
        private Label lblCategory;
        private ComboBox cboCategory;
        private DataGridView dgvFood;
        private DataGridViewTextBoxColumn colFoodId;
        private DataGridViewTextBoxColumn colFoodName;
        private DataGridViewTextBoxColumn colCategoryName;
        private DataGridViewTextBoxColumn colPrice;
    }
}
