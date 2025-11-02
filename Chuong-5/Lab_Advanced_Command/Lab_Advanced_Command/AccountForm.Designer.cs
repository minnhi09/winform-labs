namespace Lab_Advanced_Command
{
    partial class AccountForm
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
            groupBoxInfo = new GroupBox();
            dtpCreatedDate = new DateTimePicker();
            lblCreatedDate = new Label();
            chkIsActive = new CheckBox();
            txtFullName = new TextBox();
            lblFullName = new Label();
            txtPassword = new TextBox();
            lblPassword = new Label();
            txtUsername = new TextBox();
            lblUsername = new Label();
            txtAccountId = new TextBox();
            lblAccountId = new Label();
            groupBoxList = new GroupBox();
            lblTotalAccounts = new Label();
            txtSearch = new TextBox();
            lblSearch = new Label();
            dgvAccounts = new DataGridView();
            colID = new DataGridViewTextBoxColumn();
            colUsername = new DataGridViewTextBoxColumn();
            colPassword = new DataGridViewTextBoxColumn();
            colFullName = new DataGridViewTextBoxColumn();
            colIsActive = new DataGridViewCheckBoxColumn();
            colCreatedDate = new DataGridViewTextBoxColumn();
            panelButtons = new Panel();
            btnAdd = new Button();
            btnUpdate = new Button();
            btnDelete = new Button();
            btnClear = new Button();
            btnManageRoles = new Button();
            groupBoxInfo.SuspendLayout();
            groupBoxList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).BeginInit();
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
            lblTitle.Size = new Size(1072, 45);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "QUẢN LÝ TÀI KHOẢN";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // groupBoxInfo
            // 
            groupBoxInfo.Controls.Add(dtpCreatedDate);
            groupBoxInfo.Controls.Add(lblCreatedDate);
            groupBoxInfo.Controls.Add(chkIsActive);
            groupBoxInfo.Controls.Add(txtFullName);
            groupBoxInfo.Controls.Add(lblFullName);
            groupBoxInfo.Controls.Add(txtPassword);
            groupBoxInfo.Controls.Add(lblPassword);
            groupBoxInfo.Controls.Add(txtUsername);
            groupBoxInfo.Controls.Add(lblUsername);
            groupBoxInfo.Controls.Add(txtAccountId);
            groupBoxInfo.Controls.Add(lblAccountId);
            groupBoxInfo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBoxInfo.Location = new Point(14, 65);
            groupBoxInfo.Name = "groupBoxInfo";
            groupBoxInfo.Size = new Size(400, 340);
            groupBoxInfo.TabIndex = 1;
            groupBoxInfo.TabStop = false;
            groupBoxInfo.Text = "Thông tin tài khoản";
            // 
            // dtpCreatedDate
            // 
            dtpCreatedDate.Font = new Font("Segoe UI", 10F);
            dtpCreatedDate.Format = DateTimePickerFormat.Short;
            dtpCreatedDate.Location = new Point(155, 280);
            dtpCreatedDate.Name = "dtpCreatedDate";
            dtpCreatedDate.Size = new Size(225, 30);
            dtpCreatedDate.TabIndex = 10;
            // 
            // lblCreatedDate
            // 
            lblCreatedDate.AutoSize = true;
            lblCreatedDate.Font = new Font("Segoe UI", 10F);
            lblCreatedDate.Location = new Point(20, 285);
            lblCreatedDate.Name = "lblCreatedDate";
            lblCreatedDate.Size = new Size(86, 23);
            lblCreatedDate.TabIndex = 9;
            lblCreatedDate.Text = "Ngày tạo:";
            // 
            // chkIsActive
            // 
            chkIsActive.AutoSize = true;
            chkIsActive.Checked = true;
            chkIsActive.CheckState = CheckState.Checked;
            chkIsActive.Font = new Font("Segoe UI", 10F);
            chkIsActive.Location = new Point(155, 240);
            chkIsActive.Name = "chkIsActive";
            chkIsActive.Size = new Size(102, 27);
            chkIsActive.TabIndex = 8;
            chkIsActive.Text = "Kích hoạt";
            chkIsActive.UseVisualStyleBackColor = true;
            // 
            // txtFullName
            // 
            txtFullName.Font = new Font("Segoe UI", 10F);
            txtFullName.Location = new Point(155, 193);
            txtFullName.MaxLength = 200;
            txtFullName.Name = "txtFullName";
            txtFullName.Size = new Size(225, 30);
            txtFullName.TabIndex = 7;
            // 
            // lblFullName
            // 
            lblFullName.AutoSize = true;
            lblFullName.Font = new Font("Segoe UI", 10F);
            lblFullName.Location = new Point(20, 196);
            lblFullName.Name = "lblFullName";
            lblFullName.Size = new Size(93, 23);
            lblFullName.TabIndex = 6;
            lblFullName.Text = "Họ và tên:";
            // 
            // txtPassword
            // 
            txtPassword.Font = new Font("Segoe UI", 10F);
            txtPassword.Location = new Point(155, 147);
            txtPassword.MaxLength = 100;
            txtPassword.Name = "txtPassword";
            txtPassword.PasswordChar = '●';
            txtPassword.Size = new Size(225, 30);
            txtPassword.TabIndex = 5;
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Font = new Font("Segoe UI", 10F);
            lblPassword.Location = new Point(20, 150);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(86, 23);
            lblPassword.TabIndex = 4;
            lblPassword.Text = "Mật khẩu:";
            // 
            // txtUsername
            // 
            txtUsername.Font = new Font("Segoe UI", 10F);
            txtUsername.Location = new Point(155, 101);
            txtUsername.MaxLength = 50;
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(225, 30);
            txtUsername.TabIndex = 3;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = true;
            lblUsername.Font = new Font("Segoe UI", 10F);
            lblUsername.Location = new Point(20, 104);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(130, 23);
            lblUsername.TabIndex = 2;
            lblUsername.Text = "Tên đăng nhập:";
            // 
            // txtAccountId
            // 
            txtAccountId.BackColor = Color.WhiteSmoke;
            txtAccountId.Font = new Font("Segoe UI", 10F);
            txtAccountId.Location = new Point(155, 55);
            txtAccountId.Name = "txtAccountId";
            txtAccountId.ReadOnly = true;
            txtAccountId.Size = new Size(225, 30);
            txtAccountId.TabIndex = 1;
            // 
            // lblAccountId
            // 
            lblAccountId.AutoSize = true;
            lblAccountId.Font = new Font("Segoe UI", 10F);
            lblAccountId.Location = new Point(20, 58);
            lblAccountId.Name = "lblAccountId";
            lblAccountId.Size = new Size(115, 23);
            lblAccountId.TabIndex = 0;
            lblAccountId.Text = "Mã tài khoản:";
            // 
            // groupBoxList
            // 
            groupBoxList.Controls.Add(lblTotalAccounts);
            groupBoxList.Controls.Add(txtSearch);
            groupBoxList.Controls.Add(lblSearch);
            groupBoxList.Controls.Add(dgvAccounts);
            groupBoxList.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupBoxList.Location = new Point(430, 65);
            groupBoxList.Name = "groupBoxList";
            groupBoxList.Size = new Size(656, 495);
            groupBoxList.TabIndex = 2;
            groupBoxList.TabStop = false;
            groupBoxList.Text = "Danh sách tài khoản";
            // 
            // lblTotalAccounts
            // 
            lblTotalAccounts.AutoSize = true;
            lblTotalAccounts.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblTotalAccounts.ForeColor = Color.Gray;
            lblTotalAccounts.Location = new Point(20, 460);
            lblTotalAccounts.Name = "lblTotalAccounts";
            lblTotalAccounts.Size = new Size(131, 20);
            lblTotalAccounts.TabIndex = 3;
            lblTotalAccounts.Text = "Tổng số: 0 tài khoản";
            // 
            // txtSearch
            // 
            txtSearch.Font = new Font("Segoe UI", 10F);
            txtSearch.Location = new Point(240, 35);
            txtSearch.Name = "txtSearch";
            txtSearch.PlaceholderText = "Nhập tên đăng nhập hoặc họ tên...";
            txtSearch.Size = new Size(395, 30);
            txtSearch.TabIndex = 2;
            txtSearch.TextChanged += txtSearch_TextChanged;
            // 
            // lblSearch
            // 
            lblSearch.AutoSize = true;
            lblSearch.Font = new Font("Segoe UI", 10F);
            lblSearch.Location = new Point(20, 38);
            lblSearch.Name = "lblSearch";
            lblSearch.Size = new Size(205, 23);
            lblSearch.TabIndex = 1;
            lblSearch.Text = "🔍 Tìm kiếm tài khoản:";
            // 
            // dgvAccounts
            // 
            dgvAccounts.AllowUserToAddRows = false;
            dgvAccounts.AllowUserToDeleteRows = false;
            dgvAccounts.AutoGenerateColumns = false;
            dgvAccounts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvAccounts.BackgroundColor = SystemColors.Window;
            dgvAccounts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvAccounts.Columns.AddRange(new DataGridViewColumn[] { colID, colUsername, colPassword, colFullName, colIsActive, colCreatedDate });
            dgvAccounts.Location = new Point(20, 80);
            dgvAccounts.MultiSelect = false;
            dgvAccounts.Name = "dgvAccounts";
            dgvAccounts.ReadOnly = true;
            dgvAccounts.RowHeadersWidth = 51;
            dgvAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvAccounts.Size = new Size(615, 365);
            dgvAccounts.TabIndex = 0;
            dgvAccounts.SelectionChanged += dgvAccounts_SelectionChanged;
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
            // colUsername
            // 
            colUsername.DataPropertyName = "Username";
            colUsername.FillWeight = 80F;
            colUsername.HeaderText = "Tên đăng nhập";
            colUsername.MinimumWidth = 6;
            colUsername.Name = "colUsername";
            colUsername.ReadOnly = true;
            // 
            // colPassword
            // 
            colPassword.DataPropertyName = "Password";
            colPassword.FillWeight = 80F;
            colPassword.HeaderText = "Mật khẩu";
            colPassword.MinimumWidth = 6;
            colPassword.Name = "colPassword";
            colPassword.ReadOnly = true;
            // 
            // colFullName
            // 
            colFullName.DataPropertyName = "FullName";
            colFullName.FillWeight = 100F;
            colFullName.HeaderText = "Họ và tên";
            colFullName.MinimumWidth = 6;
            colFullName.Name = "colFullName";
            colFullName.ReadOnly = true;
            // 
            // colIsActive
            // 
            colIsActive.DataPropertyName = "IsActive";
            colIsActive.FillWeight = 60F;
            colIsActive.HeaderText = "Kích hoạt";
            colIsActive.MinimumWidth = 6;
            colIsActive.Name = "colIsActive";
            colIsActive.ReadOnly = true;
            // 
            // colCreatedDate
            // 
            colCreatedDate.DataPropertyName = "CreatedDate";
            colCreatedDate.FillWeight = 80F;
            colCreatedDate.HeaderText = "Ngày tạo";
            colCreatedDate.MinimumWidth = 6;
            colCreatedDate.Name = "colCreatedDate";
            colCreatedDate.ReadOnly = true;
            // 
            // panelButtons
            // 
            panelButtons.BackColor = Color.WhiteSmoke;
            panelButtons.Controls.Add(btnAdd);
            panelButtons.Controls.Add(btnUpdate);
            panelButtons.Controls.Add(btnDelete);
            panelButtons.Controls.Add(btnClear);
            panelButtons.Controls.Add(btnManageRoles);
            panelButtons.Location = new Point(14, 415);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(400, 145);
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
            btnAdd.Size = new Size(170, 50);
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
            btnUpdate.Location = new Point(210, 15);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.Size = new Size(170, 50);
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
            btnDelete.Location = new Point(20, 80);
            btnDelete.Name = "btnDelete";
            btnDelete.Size = new Size(170, 50);
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
            btnClear.Location = new Point(210, 80);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(170, 50);
            btnClear.TabIndex = 3;
            btnClear.Text = "🔄 Làm mới";
            btnClear.UseVisualStyleBackColor = false;
            btnClear.Click += btnClear_Click;
            // 
            // btnManageRoles
            // 
            btnManageRoles.BackColor = Color.FromArgb(52, 152, 219);
            btnManageRoles.FlatAppearance.BorderSize = 0;
            btnManageRoles.FlatStyle = FlatStyle.Flat;
            btnManageRoles.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnManageRoles.ForeColor = Color.White;
            btnManageRoles.Location = new Point(115, 135);
            btnManageRoles.Name = "btnManageRoles";
            btnManageRoles.Size = new Size(170, 40);
            btnManageRoles.TabIndex = 4;
            btnManageRoles.Text = "👥 Quản lý vai trò";
            btnManageRoles.UseVisualStyleBackColor = false;
            btnManageRoles.Click += btnManageRoles_Click;
            // 
            // AccountForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1100, 575);
            Controls.Add(panelButtons);
            Controls.Add(groupBoxList);
            Controls.Add(groupBoxInfo);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "AccountForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý tài khoản";
            Load += AccountForm_Load;
            groupBoxInfo.ResumeLayout(false);
            groupBoxInfo.PerformLayout();
            groupBoxList.ResumeLayout(false);
            groupBoxList.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAccounts).EndInit();
            panelButtons.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private GroupBox groupBoxInfo;
        private TextBox txtAccountId;
        private Label lblAccountId;
        private TextBox txtUsername;
        private Label lblUsername;
        private TextBox txtPassword;
        private Label lblPassword;
        private TextBox txtFullName;
        private Label lblFullName;
        private CheckBox chkIsActive;
        private DateTimePicker dtpCreatedDate;
        private Label lblCreatedDate;
        private GroupBox groupBoxList;
        private DataGridView dgvAccounts;
        private Panel panelButtons;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnDelete;
        private Button btnClear;
        private Button btnManageRoles;
        private Label lblSearch;
        private TextBox txtSearch;
        private Label lblTotalAccounts;
        private DataGridViewTextBoxColumn colID;
        private DataGridViewTextBoxColumn colUsername;
        private DataGridViewTextBoxColumn colPassword;
        private DataGridViewTextBoxColumn colFullName;
        private DataGridViewCheckBoxColumn colIsActive;
        private DataGridViewTextBoxColumn colCreatedDate;
    }
}
