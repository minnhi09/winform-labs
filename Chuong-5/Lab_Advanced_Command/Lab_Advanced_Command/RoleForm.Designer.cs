namespace Lab_Advanced_Command
{
    partial class RoleForm
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
            lblTitle = new Label();
            groupBoxInfo = new GroupBox();
            txtDescription = new TextBox();
            lblDescription = new Label();
            txtRoleName = new TextBox();
            lblRoleName = new Label();
            txtRoleId = new TextBox();
            lblRoleId = new Label();
            groupBoxList = new GroupBox();
            lblTotalRoles = new Label();
            txtSearch = new TextBox();
            lblSearch = new Label();
            dgvRoles = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colRoleName = new DataGridViewTextBoxColumn();
            colDescription = new DataGridViewTextBoxColumn();
            panelButtons = new Panel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            groupBoxInfo.SuspendLayout();
            groupBoxList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).BeginInit();
            panelButtons.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = false;
            lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 122, 204);
            lblTitle.Location = new Point(14, 12);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(872, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ VAI TRÒ";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBoxInfo
            // 
            groupBoxInfo.Controls.Add(txtDescription);
            groupBoxInfo.Controls.Add(lblDescription);
            groupBoxInfo.Controls.Add(txtRoleName);
            groupBoxInfo.Controls.Add(lblRoleName);
            groupBoxInfo.Controls.Add(txtRoleId);
            groupBoxInfo.Controls.Add(lblRoleId);
            groupBoxInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBoxInfo.Location = new Point(14, 65);
            groupBoxInfo.Name = "groupBoxInfo";
            groupBoxInfo.Size = new Size(380, 280);
            groupBoxInfo.TabIndex = 1;
            groupBoxInfo.TabStop = false;
            groupBoxInfo.Text = "Thông tin vai trò";
            // 
            // txtDescription
            // 
            txtDescription.Font = new Font("Segoe UI", 10F);
            txtDescription.Location = new Point(140, 147);
            txtDescription.MaxLength = 300;
            txtDescription.Multiline = true;
            txtDescription.Name = "txtDescription";
            txtDescription.ScrollBars = ScrollBars.Vertical;
            txtDescription.Size = new Size(220, 100);
            txtDescription.TabIndex = 5;
            // 
            // lblDescription
            // 
            lblDescription.AutoSize = true;
            lblDescription.Font = new Font("Segoe UI", 10F);
            lblDescription.Location = new Point(20, 150);
            lblDescription.Name = "lblDescription";
            lblDescription.Size = new Size(58, 23);
            lblDescription.TabIndex = 4;
            lblDescription.Text = "Mô tả:";
            // 
            // txtRoleName
            // 
            txtRoleName.Font = new Font("Segoe UI", 10F);
            txtRoleName.Location = new Point(140, 101);
            txtRoleName.MaxLength = 100;
            txtRoleName.Name = "txtRoleName";
            txtRoleName.Size = new Size(220, 30);
            txtRoleName.TabIndex = 3;
            // 
            // lblRoleName
            // 
            lblRoleName.AutoSize = true;
            lblRoleName.Font = new Font("Segoe UI", 10F);
            lblRoleName.Location = new Point(20, 104);
            lblRoleName.Name = "lblRoleName";
            lblRoleName.Size = new Size(94, 23);
            lblRoleName.TabIndex = 2;
            lblRoleName.Text = "Tên vai trò:";
            // 
            // txtRoleId
            // 
            txtRoleId.BackColor = Color.WhiteSmoke;
            txtRoleId.Font = new Font("Segoe UI", 10F);
            txtRoleId.Location = new Point(140, 55);
            txtRoleId.Name = "txtRoleId";
            txtRoleId.ReadOnly = true;
            txtRoleId.Size = new Size(220, 30);
            txtRoleId.TabIndex = 1;
            // 
            // lblRoleId
            // 
            lblRoleId.AutoSize = true;
            lblRoleId.Font = new Font("Segoe UI", 10F);
            lblRoleId.Location = new Point(20, 58);
            lblRoleId.Name = "lblRoleId";
            lblRoleId.Size = new Size(93, 23);
            lblRoleId.TabIndex = 0;
            lblRoleId.Text = "Mã vai trò:";
            // 
            // groupBoxList
            // 
            groupBoxList.Controls.Add(lblTotalRoles);
            groupBoxList.Controls.Add(txtSearch);
            groupBoxList.Controls.Add(lblSearch);
            groupBoxList.Controls.Add(dgvRoles);
            groupBoxList.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBoxList.Location = new Point(410, 65);
            groupBoxList.Name = "groupBoxList";
            groupBoxList.Size = new Size(476, 430);
            groupBoxList.TabIndex = 2;
            groupBoxList.TabStop = false;
            groupBoxList.Text = "Danh sách vai trò";
            // 
            // lblTotalRoles
            // 
            lblTotalRoles.AutoSize = true;
            lblTotalRoles.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblTotalRoles.ForeColor = Color.Gray;
            lblTotalRoles.Location = new Point(20, 395);
            lblTotalRoles.Name = "lblTotalRoles";
            lblTotalRoles.Size = new Size(118, 20);
            lblTotalRoles.TabIndex = 3;
            lblTotalRoles.Text = "Tổng số: 0 vai trò";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(180, 35);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nhập tên vai trò...";
            txtSearch.Size = new Size(275, 30);
            txtSearch.TabIndex = 2;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F);
            lblSearch.Location = new Point(20, 38);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(145, 23);
            lblSearch.TabIndex = 1;
            lblSearch.Text = "🔍 Tìm kiếm vai trò:";
            // 
            // dgvRoles
            // 
            dgvRoles.AllowUserToAddRows = false;
            dgvRoles.AllowUserToDeleteRows = false;
            dgvRoles.AutoGenerateColumns = false;
            dgvRoles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvRoles.BackgroundColor = SystemColors.Window;
            dgvRoles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvRoles.Columns.AddRange(new DataGridViewColumn[] { colID, colRoleName, colDescription });
            dgvRoles.Location = new Point(20, 80);
            dgvRoles.MultiSelect = false;
            dgvRoles.Name = "dgvRoles";
            dgvRoles.ReadOnly = true;
            dgvRoles.RowHeadersWidth = 51;
            dgvRoles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvRoles.Size = new Size(435, 300);
            dgvRoles.TabIndex = 0;
            dgvRoles.SelectionChanged += dgvRoles_SelectionChanged;
            // 
            // colID
            // 
            colID.DataPropertyName = "ID";
            colID.FillWeight = 40F;
            colID.HeaderText = "ID";
            colID.MinimumWidth = 6;
            colID.Name = "colID";
            colID.ReadOnly = true;
            // 
            // colRoleName
            // 
            colRoleName.DataPropertyName = "RoleName";
            colRoleName.FillWeight = 100F;
            colRoleName.HeaderText = "Tên vai trò";
            colRoleName.MinimumWidth = 6;
            colRoleName.Name = "colRoleName";
            colRoleName.ReadOnly = true;
            // 
            // colDescription
            // 
            colDescription.DataPropertyName = "Description";
            colDescription.FillWeight = 140F;
            colDescription.HeaderText = "Mô tả";
            colDescription.MinimumWidth = 6;
            colDescription.Name = "colDescription";
            colDescription.ReadOnly = true;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.WhiteSmoke;
            panelButtons.Controls.Add(btnAdd);
            panelButtons.Controls.Add(btnUpdate);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(btnClear);
            panelButtons.Location = new Point(14, 355);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(380, 140);
            panelButtons.TabIndex = 3;
            // 
            // btnAdd
            // 
            btnAdd.BackColor = Color.FromArgb(46, 204, 113);
            btnAdd.FlatAppearance.BorderSize = 0;
            btnAdd.FlatStyle = FlatStyle.Flat;
            btnAdd.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnAdd.ForeColor = Color.White;
            btnAdd.Location = new Point(20, 15);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(160, 50);
            btnAdd.TabIndex = 0;
            btnAdd.Text = "➕ Thêm mới";
            btnAdd.UseVisualStyleBackColor = false;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.BackColor = Color.FromArgb(243, 156, 18);
            btnUpdate.Enabled = false;
            btnUpdate.FlatAppearance.BorderSize = 0;
            btnUpdate.FlatStyle = FlatStyle.Flat;
            btnUpdate.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(200, 15);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(160, 50);
            btnUpdate.TabIndex = 1;
            btnUpdate.Text = "✏️ Cập nhật";
            btnUpdate.UseVisualStyleBackColor = false;
            btnUpdate.Click += btnUpdate_Click;
            // 
            // btnDelete
            // 
            btnDelete.BackColor = Color.FromArgb(231, 76, 60);
            btnDelete.FlatAppearance.BorderSize = 0;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnDelete.ForeColor = Color.White;
            btnDelete.Location = new Point(20, 75);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(160, 50);
            btnDelete.TabIndex = 2;
            btnDelete.Text = "🗑️ Xóa";
            btnDelete.UseVisualStyleBackColor = false;
            btnDelete.Click += btnDelete_Click;
            // 
            // btnClear
            // 
            btnClear.BackColor = Color.FromArgb(149, 165, 166);
            btnClear.FlatAppearance.BorderSize = 0;
            btnClear.FlatStyle = FlatStyle.Flat;
            btnClear.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            btnClear.ForeColor = Color.White;
            btnClear.Location = new Point(200, 75);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(160, 50);
            btnClear.TabIndex = 3;
            btnClear.Text = "🔄 Làm mới";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // RoleForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(900, 510);
            Controls.Add(panelButtons);
            Controls.Add(groupBoxList);
            Controls.Add(groupBoxInfo);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "RoleForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Quản lý vai trò";
            Load += RoleForm_Load;
            groupBoxInfo.ResumeLayout(false);
            groupBoxInfo.PerformLayout();
            groupBoxList.ResumeLayout(false);
            groupBoxList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvRoles).EndInit();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private GroupBox groupBoxInfo;
        private TextBox txtRoleId;
        private Label lblRoleId;
        private TextBox txtRoleName;
        private Label lblRoleName;
        private TextBox txtDescription;
        private Label lblDescription;
        private GroupBox groupBoxList;
        private DataGridView dgvRoles;
        private Panel panelButtons;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblTotalRoles;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colRoleName;
        private DataGridViewTextBoxColumn colDescription;
    }
}
