namespace Lab_Advanced_Command
{
    partial class CategoryForm
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
            lblTitle = new System.Windows.Forms.Label();
            lblCategoryName = new System.Windows.Forms.Label();
            txtCategoryName = new System.Windows.Forms.TextBox();
            lblNotes = new System.Windows.Forms.Label();
            txtNotes = new System.Windows.Forms.TextBox();
            panelButtons = new System.Windows.Forms.Panel();
            btnSave = new System.Windows.Forms.Button();
            btnClear = new System.Windows.Forms.Button();
            btnCancel = new System.Windows.Forms.Button();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = false;
            lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(204)))));
            lblTitle.Location = new System.Drawing.Point(12, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new System.Drawing.Size(460, 40);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "THÊM NHÓM MÓN ĂN";
            lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCategoryName
            // 
            lblCategoryName.AutoSize = true;
            lblCategoryName.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblCategoryName.Location = new System.Drawing.Point(30, 70);
            lblCategoryName.Name = "lblCategoryName";
            lblCategoryName.Size = new System.Drawing.Size(125, 23);
            lblCategoryName.TabIndex = 1;
            lblCategoryName.Text = "Tên nhóm (*):";
            // 
            // txtCategoryName
            // 
            txtCategoryName.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtCategoryName.Location = new System.Drawing.Point(170, 67);
            txtCategoryName.MaxLength = 200;
            txtCategoryName.Name = "txtCategoryName";
            txtCategoryName.Size = new System.Drawing.Size(280, 30);
            txtCategoryName.TabIndex = 2;
            // 
            // lblNotes
            // 
            lblNotes.AutoSize = true;
            lblNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            lblNotes.Location = new System.Drawing.Point(30, 120);
            lblNotes.Name = "lblNotes";
            lblNotes.Size = new System.Drawing.Size(73, 23);
            lblNotes.TabIndex = 3;
            lblNotes.Text = "Ghi chú:";
            // 
            // txtNotes
            // 
            txtNotes.Font = new System.Drawing.Font("Segoe UI", 10F);
            txtNotes.Location = new System.Drawing.Point(170, 117);
            txtNotes.MaxLength = 500;
            txtNotes.Multiline = true;
            txtNotes.Name = "txtNotes";
            txtNotes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            txtNotes.Size = new System.Drawing.Size(280, 100);
            txtNotes.TabIndex = 4;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = System.Drawing.Color.WhiteSmoke;
            panelButtons.Controls.Add(btnSave);
            panelButtons.Controls.Add(btnClear);
            panelButtons.Controls.Add(btnCancel);
            panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelButtons.Location = new System.Drawing.Point(0, 240);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new System.Drawing.Size(484, 70);
            panelButtons.TabIndex = 5;
            // 
            // btnSave
            // 
            btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnSave.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnSave.ForeColor = System.Drawing.Color.White;
            btnSave.Location = new System.Drawing.Point(30, 15);
            btnSave.Name = "btnSave";
            btnSave.Size = new System.Drawing.Size(120, 40);
            btnSave.TabIndex = 0;
            btnSave.Text = "💾 Lưu";
            btnSave.UseVisualStyleBackColor = false;
            btnSave.Click += new System.EventHandler(btnSave_Click);
            // 
            // btnClear
            // 
            btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(156)))), ((int)(((byte)(18)))));
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnClear.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnClear.ForeColor = System.Drawing.Color.White;
            btnClear.Location = new System.Drawing.Point(182, 15);
            btnClear.Name = "btnClear";
            btnClear.Size = new System.Drawing.Size(120, 40);
            btnClear.TabIndex = 1;
            btnClear.Text = "🗑️ Xóa";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += new System.EventHandler(btnClear_Click);
            // 
            // btnCancel
            // 
            btnCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            btnCancel.FlatAppearance.BorderSize = 0;
            btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnCancel.ForeColor = System.Drawing.Color.White;
            btnCancel.Location = new System.Drawing.Point(334, 15);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new System.Drawing.Size(120, 40);
            btnCancel.TabIndex = 2;
            btnCancel.Text = "❌ Hủy";
            btnCancel.UseVisualStyleBackColor = false;
            btnCancel.Click += new System.EventHandler(btnCancel_Click);
            // 
            // CategoryForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(484, 310);
            Controls.Add(panelButtons);
            Controls.Add(txtNotes);
            Controls.Add(lblNotes);
            Controls.Add(txtCategoryName);
            Controls.Add(lblCategoryName);
            Controls.Add(lblTitle);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "CategoryForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Thêm nhóm món ăn";
            Load += new System.EventHandler(CategoryForm_Load);
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblCategoryName;
        private System.Windows.Forms.TextBox txtCategoryName;
        private System.Windows.Forms.Label lblNotes;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnCancel;
    }
}
