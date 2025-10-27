namespace Lab_Basic_Command
{
    partial class AccountRoleManager
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
            this.lblAccountInfo = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnAddRole = new System.Windows.Forms.Button();
            this.cboRole = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lvAccountRoles = new System.Windows.Forms.ListView();
            this.chRoleID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chRoleName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chDescription = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmRemove = new System.Windows.Forms.ToolStripMenuItem();
            this.btnRemoveRole = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblAccountInfo
            // 
            this.lblAccountInfo.AutoSize = true;
            this.lblAccountInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccountInfo.Location = new System.Drawing.Point(12, 9);
            this.lblAccountInfo.Name = "lblAccountInfo";
            this.lblAccountInfo.Size = new System.Drawing.Size(157, 20);
            this.lblAccountInfo.TabIndex = 0;
            this.lblAccountInfo.Text = "Tài khoản: {name}";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnAddRole);
            this.groupBox1.Controls.Add(this.cboRole);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 100);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thêm vai trò";
            // 
            // btnAddRole
            // 
            this.btnAddRole.Location = new System.Drawing.Point(640, 36);
            this.btnAddRole.Name = "btnAddRole";
            this.btnAddRole.Size = new System.Drawing.Size(120, 36);
            this.btnAddRole.TabIndex = 2;
            this.btnAddRole.Text = "Thêm vai trò";
            this.btnAddRole.UseVisualStyleBackColor = true;
            this.btnAddRole.Click += new System.EventHandler(this.btnAddRole_Click);
            // 
            // cboRole
            // 
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.FormattingEnabled = true;
            this.cboRole.Location = new System.Drawing.Point(120, 42);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(500, 24);
            this.cboRole.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Vai trò:";
            // 
            // lvAccountRoles
            // 
            this.lvAccountRoles.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chRoleID,
            this.chRoleName,
            this.chDescription});
            this.lvAccountRoles.ContextMenuStrip = this.contextMenuStrip1;
            this.lvAccountRoles.FullRowSelect = true;
            this.lvAccountRoles.GridLines = true;
            this.lvAccountRoles.HideSelection = false;
            this.lvAccountRoles.Location = new System.Drawing.Point(12, 185);
            this.lvAccountRoles.MultiSelect = false;
            this.lvAccountRoles.Name = "lvAccountRoles";
            this.lvAccountRoles.Size = new System.Drawing.Size(776, 253);
            this.lvAccountRoles.TabIndex = 2;
            this.lvAccountRoles.UseCompatibleStateImageBehavior = false;
            this.lvAccountRoles.View = System.Windows.Forms.View.Details;
            this.lvAccountRoles.Click += new System.EventHandler(this.lvAccountRoles_Click);
            // 
            // chRoleID
            // 
            this.chRoleID.Text = "ID";
            this.chRoleID.Width = 50;
            // 
            // chRoleName
            // 
            this.chRoleName.Text = "Tên vai trò";
            this.chRoleName.Width = 200;
            // 
            // chDescription
            // 
            this.chDescription.Text = "Mô tả";
            this.chDescription.Width = 500;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmRemove});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 28);
            // 
            // tsmRemove
            // 
            this.tsmRemove.Name = "tsmRemove";
            this.tsmRemove.Size = new System.Drawing.Size(134, 24);
            this.tsmRemove.Text = "Xóa vai trò";
            this.tsmRemove.Click += new System.EventHandler(this.tsmRemove_Click);
            // 
            // btnRemoveRole
            // 
            this.btnRemoveRole.Enabled = false;
            this.btnRemoveRole.Location = new System.Drawing.Point(668, 453);
            this.btnRemoveRole.Name = "btnRemoveRole";
            this.btnRemoveRole.Size = new System.Drawing.Size(120, 36);
            this.btnRemoveRole.TabIndex = 3;
            this.btnRemoveRole.Text = "Xóa vai trò";
            this.btnRemoveRole.UseVisualStyleBackColor = true;
            this.btnRemoveRole.Click += new System.EventHandler(this.btnRemoveRole_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 158);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 18);
            this.label2.TabIndex = 4;
            this.label2.Text = "Danh sách vai trò hiện tại";
            // 
            // AccountRoleManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 501);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnRemoveRole);
            this.Controls.Add(this.lvAccountRoles);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblAccountInfo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AccountRoleManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quản lý vai trò tài khoản";
            this.Load += new System.EventHandler(this.AccountRoleManager_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAccountInfo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAddRole;
        private System.Windows.Forms.ComboBox cboRole;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvAccountRoles;
        private System.Windows.Forms.ColumnHeader chRoleID;
        private System.Windows.Forms.ColumnHeader chRoleName;
        private System.Windows.Forms.ColumnHeader chDescription;
        private System.Windows.Forms.Button btnRemoveRole;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmRemove;
    }
}
