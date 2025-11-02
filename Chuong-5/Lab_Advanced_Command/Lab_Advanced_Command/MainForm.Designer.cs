namespace Lab_Advanced_Command
{
    partial class MainForm
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
            lblSubtitle = new Label();
            btnFoodManagement = new Button();
            btnAccountManagement = new Button();
            btnRoleManagement = new Button();
            btnLogout = new Button();
            btnExit = new Button();
            panelButtons = new Panel();
            panelUser = new Panel();
            lblWelcome = new Label();
            lblUsername = new Label();
            panelButtons.SuspendLayout();
            panelUser.SuspendLayout();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = false;
            lblTitle.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblTitle.ForeColor = Color.FromArgb(0, 122, 204);
            lblTitle.Location = new Point(14, 30);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(572, 60);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "HỆ THỐNG QUẢN LÝ";
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSubtitle
            // 
            lblSubtitle.AutoSize = false;
            lblSubtitle.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
            lblSubtitle.ForeColor = Color.Gray;
            lblSubtitle.Location = new Point(14, 90);
            lblSubtitle.Name = "lblSubtitle";
            lblSubtitle.Size = new Size(572, 30);
            lblSubtitle.TabIndex = 1;
            lblSubtitle.Text = "Vui lòng chọn chức năng bên dưới";
            lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panelUser
            // 
            panelUser.BackColor = Color.FromArgb(240, 248, 255);
            panelUser.BorderStyle = BorderStyle.FixedSingle;
            panelUser.Controls.Add(lblWelcome);
            panelUser.Controls.Add(lblUsername);
            panelUser.Location = new Point(100, 130);
            panelUser.Name = "panelUser";
            panelUser.Size = new Size(400, 60);
            panelUser.TabIndex = 3;
            // 
            // lblWelcome
            // 
            lblWelcome.AutoSize = false;
            lblWelcome.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblWelcome.ForeColor = Color.FromArgb(0, 122, 204);
            lblWelcome.Location = new Point(10, 8);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(380, 25);
            lblWelcome.TabIndex = 0;
            lblWelcome.Text = "Xin chào!";
            lblWelcome.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblUsername
            // 
            lblUsername.AutoSize = false;
            lblUsername.Font = new Font("Segoe UI", 9F, FontStyle.Italic);
            lblUsername.ForeColor = Color.Gray;
            lblUsername.Location = new Point(10, 30);
            lblUsername.Name = "lblUsername";
            lblUsername.Size = new Size(380, 20);
            lblUsername.TabIndex = 1;
            lblUsername.Text = "Tài khoản: ";
            lblUsername.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panelButtons
            // 
            panelButtons.Controls.Add(btnFoodManagement);
            panelButtons.Controls.Add(btnAccountManagement);
            panelButtons.Controls.Add(btnRoleManagement);
            panelButtons.Controls.Add(btnLogout);
            panelButtons.Controls.Add(btnExit);
            panelButtons.Location = new Point(100, 200);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new Size(400, 440);
            panelButtons.TabIndex = 2;
            // 
            // btnFoodManagement
            // 
            btnFoodManagement.BackColor = Color.FromArgb(46, 204, 113);
            btnFoodManagement.Cursor = Cursors.Hand;
            btnFoodManagement.FlatAppearance.BorderSize = 0;
            btnFoodManagement.FlatStyle = FlatStyle.Flat;
            btnFoodManagement.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnFoodManagement.ForeColor = Color.White;
            btnFoodManagement.Location = new Point(50, 20);
            btnFoodManagement.Name = "btnFoodManagement";
            btnFoodManagement.Size = new Size(300, 70);
            btnFoodManagement.TabIndex = 0;
            btnFoodManagement.Text = "🍽️ Quản lý món ăn";
            btnFoodManagement.UseVisualStyleBackColor = false;
            btnFoodManagement.Click += btnFoodManagement_Click;
            btnFoodManagement.MouseEnter += (s, e) => btnFoodManagement.BackColor = Color.FromArgb(39, 174, 96);
            btnFoodManagement.MouseLeave += (s, e) => btnFoodManagement.BackColor = Color.FromArgb(46, 204, 113);
            // 
            // btnAccountManagement
            // 
            btnAccountManagement.BackColor = Color.FromArgb(52, 152, 219);
            btnAccountManagement.Cursor = Cursors.Hand;
            btnAccountManagement.FlatAppearance.BorderSize = 0;
            btnAccountManagement.FlatStyle = FlatStyle.Flat;
            btnAccountManagement.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnAccountManagement.ForeColor = Color.White;
            btnAccountManagement.Location = new Point(50, 110);
            btnAccountManagement.Name = "btnAccountManagement";
            btnAccountManagement.Size = new Size(300, 70);
            btnAccountManagement.TabIndex = 1;
            btnAccountManagement.Text = "👤 Quản lý tài khoản";
            btnAccountManagement.UseVisualStyleBackColor = false;
            btnAccountManagement.Click += btnAccountManagement_Click;
            btnAccountManagement.MouseEnter += (s, e) => btnAccountManagement.BackColor = Color.FromArgb(41, 128, 185);
            btnAccountManagement.MouseLeave += (s, e) => btnAccountManagement.BackColor = Color.FromArgb(52, 152, 219);
            // 
            // btnRoleManagement
            // 
            btnRoleManagement.BackColor = Color.FromArgb(155, 89, 182);
            btnRoleManagement.Cursor = Cursors.Hand;
            btnRoleManagement.FlatAppearance.BorderSize = 0;
            btnRoleManagement.FlatStyle = FlatStyle.Flat;
            btnRoleManagement.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnRoleManagement.ForeColor = Color.White;
            btnRoleManagement.Location = new Point(50, 200);
            btnRoleManagement.Name = "btnRoleManagement";
            btnRoleManagement.Size = new Size(300, 70);
            btnRoleManagement.TabIndex = 2;
            btnRoleManagement.Text = "👥 Quản lý vai trò";
            btnRoleManagement.UseVisualStyleBackColor = false;
            btnRoleManagement.Click += btnRoleManagement_Click;
            btnRoleManagement.MouseEnter += (s, e) => btnRoleManagement.BackColor = Color.FromArgb(142, 68, 173);
            btnRoleManagement.MouseLeave += (s, e) => btnRoleManagement.BackColor = Color.FromArgb(155, 89, 182);
            // 
            // btnLogout
            // 
            btnLogout.BackColor = Color.FromArgb(241, 196, 15);
            btnLogout.Cursor = Cursors.Hand;
            btnLogout.FlatAppearance.BorderSize = 0;
            btnLogout.FlatStyle = FlatStyle.Flat;
            btnLogout.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnLogout.ForeColor = Color.White;
            btnLogout.Location = new Point(50, 290);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(300, 70);
            btnLogout.TabIndex = 3;
            btnLogout.Text = "🔓 Đăng xuất";
            btnLogout.UseVisualStyleBackColor = false;
            btnLogout.Click += btnLogout_Click;
            btnLogout.MouseEnter += (s, e) => btnLogout.BackColor = Color.FromArgb(243, 156, 18);
            btnLogout.MouseLeave += (s, e) => btnLogout.BackColor = Color.FromArgb(241, 196, 15);
            // 
            // btnExit
            // 
            btnExit.BackColor = Color.FromArgb(231, 76, 60);
            btnExit.Cursor = Cursors.Hand;
            btnExit.FlatAppearance.BorderSize = 0;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new Font("Segoe UI", 14F, FontStyle.Bold);
            btnExit.ForeColor = Color.White;
            btnExit.Location = new Point(50, 380);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(300, 70);
            btnExit.TabIndex = 4;
            btnExit.Text = "🚪 Thoát";
            btnExit.UseVisualStyleBackColor = false;
            btnExit.Click += btnExit_Click;
            btnExit.MouseEnter += (s, e) => btnExit.BackColor = Color.FromArgb(192, 57, 43);
            btnExit.MouseLeave += (s, e) => btnExit.BackColor = Color.FromArgb(231, 76, 60);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(600, 660);
            Controls.Add(panelUser);
            Controls.Add(panelButtons);
            Controls.Add(lblSubtitle);
            Controls.Add(lblTitle);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "MainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Hệ thống quản lý - Chương 5";
            Load += MainForm_Load;
            panelButtons.ResumeLayout(false);
            panelUser.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Label lblTitle;
        private Label lblSubtitle;
        private Button btnFoodManagement;
        private Button btnAccountManagement;
        private Button btnRoleManagement;
        private Button btnLogout;
        private Button btnExit;
        private Panel panelButtons;
        private Panel panelUser;
        private Label lblWelcome;
        private Label lblUsername;
    }
}
