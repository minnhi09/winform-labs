namespace entity_framework
{
    partial class frmRole
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
            this.groupBoxLeft = new System.Windows.Forms.GroupBox();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txtRoleName = new System.Windows.Forms.TextBox();
            this.txtID = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnReset = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBoxRight = new System.Windows.Forms.GroupBox();
            this.lvRole = new System.Windows.Forms.ListView();
            this.columnHeaderSTT = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderRoleName = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderPath = new System.Windows.Forms.ColumnHeader();
            this.columnHeaderNotes = new System.Windows.Forms.ColumnHeader();
            this.lblStatistics = new System.Windows.Forms.Label();
            this.btnExit = new System.Windows.Forms.Button();
            this.groupBoxLeft.SuspendLayout();
            this.groupBoxRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxLeft
            // 
            this.groupBoxLeft.Controls.Add(this.txtNotes);
            this.groupBoxLeft.Controls.Add(this.txtPath);
            this.groupBoxLeft.Controls.Add(this.txtRoleName);
            this.groupBoxLeft.Controls.Add(this.txtID);
            this.groupBoxLeft.Controls.Add(this.btnDelete);
            this.groupBoxLeft.Controls.Add(this.btnUpdate);
            this.groupBoxLeft.Controls.Add(this.btnAdd);
            this.groupBoxLeft.Controls.Add(this.btnReset);
            this.groupBoxLeft.Controls.Add(this.label4);
            this.groupBoxLeft.Controls.Add(this.label3);
            this.groupBoxLeft.Controls.Add(this.label2);
            this.groupBoxLeft.Controls.Add(this.label1);
            this.groupBoxLeft.Location = new System.Drawing.Point(20, 24);
            this.groupBoxLeft.Name = "groupBoxLeft";
            this.groupBoxLeft.Size = new System.Drawing.Size(569, 700);
            this.groupBoxLeft.TabIndex = 0;
            this.groupBoxLeft.TabStop = false;
            this.groupBoxLeft.Text = "Thông tin Role";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(195, 320);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(322, 100);
            this.txtNotes.TabIndex = 3;
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(195, 240);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(322, 39);
            this.txtPath.TabIndex = 2;
            // 
            // txtRoleName
            // 
            this.txtRoleName.Location = new System.Drawing.Point(195, 160);
            this.txtRoleName.Name = "txtRoleName";
            this.txtRoleName.Size = new System.Drawing.Size(322, 39);
            this.txtRoleName.TabIndex = 1;
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(195, 80);
            this.txtID.Name = "txtID";
            this.txtID.ReadOnly = true;
            this.txtID.Size = new System.Drawing.Size(322, 39);
            this.txtID.TabIndex = 0;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(398, 580);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(122, 70);
            this.btnDelete.TabIndex = 7;
            this.btnDelete.Text = "Xóa";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(266, 580);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(122, 70);
            this.btnUpdate.TabIndex = 6;
            this.btnUpdate.Text = "Sửa";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(135, 580);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(122, 70);
            this.btnAdd.TabIndex = 5;
            this.btnAdd.Text = "Thêm";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(3, 580);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(122, 70);
            this.btnReset.TabIndex = 4;
            this.btnReset.Text = "Nhập lại";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(32, 320);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 32);
            this.label4.TabIndex = 3;
            this.label4.Text = "Ghi chú:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 240);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "Đường dẫn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 32);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên vai trò:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(32, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "ID:";
            // 
            // groupBoxRight
            // 
            this.groupBoxRight.Controls.Add(this.lvRole);
            this.groupBoxRight.Location = new System.Drawing.Point(618, 24);
            this.groupBoxRight.Name = "groupBoxRight";
            this.groupBoxRight.Size = new System.Drawing.Size(1138, 700);
            this.groupBoxRight.TabIndex = 1;
            this.groupBoxRight.TabStop = false;
            this.groupBoxRight.Text = "Danh sách Role";
            // 
            // lvRole
            // 
            this.lvRole.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderSTT,
            this.columnHeaderRoleName,
            this.columnHeaderPath,
            this.columnHeaderNotes});
            this.lvRole.FullRowSelect = true;
            this.lvRole.GridLines = true;
            this.lvRole.Location = new System.Drawing.Point(10, 42);
            this.lvRole.Name = "lvRole";
            this.lvRole.Size = new System.Drawing.Size(1116, 642);
            this.lvRole.TabIndex = 0;
            this.lvRole.UseCompatibleStateImageBehavior = false;
            this.lvRole.View = System.Windows.Forms.View.Details;
            this.lvRole.Click += new System.EventHandler(this.lvRole_Click);
            // 
            // columnHeaderSTT
            // 
            this.columnHeaderSTT.Text = "STT";
            this.columnHeaderSTT.Width = 80;
            // 
            // columnHeaderRoleName
            // 
            this.columnHeaderRoleName.Text = "Tên vai trò";
            this.columnHeaderRoleName.Width = 250;
            // 
            // columnHeaderPath
            // 
            this.columnHeaderPath.Text = "Đường dẫn";
            this.columnHeaderPath.Width = 300;
            // 
            // columnHeaderNotes
            // 
            this.columnHeaderNotes.Text = "Ghi chú";
            this.columnHeaderNotes.Width = 400;
            // 
            // lblStatistics
            // 
            this.lblStatistics.AutoSize = true;
            this.lblStatistics.Location = new System.Drawing.Point(618, 750);
            this.lblStatistics.Name = "lblStatistics";
            this.lblStatistics.Size = new System.Drawing.Size(120, 32);
            this.lblStatistics.TabIndex = 2;
            this.lblStatistics.Text = "Thống kê:";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(1625, 736);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(130, 70);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // frmRole
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1774, 830);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lblStatistics);
            this.Controls.Add(this.groupBoxRight);
            this.Controls.Add(this.groupBoxLeft);
            this.Name = "frmRole";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "QUẢN LÝ VAI TRÒ";
            this.Load += new System.EventHandler(this.frmRole_Load);
            this.groupBoxLeft.ResumeLayout(false);
            this.groupBoxLeft.PerformLayout();
            this.groupBoxRight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxLeft;
        private System.Windows.Forms.GroupBox groupBoxRight;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.TextBox txtRoleName;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.ListView lvRole;
        private System.Windows.Forms.ColumnHeader columnHeaderSTT;
        private System.Windows.Forms.ColumnHeader columnHeaderRoleName;
        private System.Windows.Forms.ColumnHeader columnHeaderPath;
        private System.Windows.Forms.ColumnHeader columnHeaderNotes;
        private System.Windows.Forms.Label lblStatistics;
        private System.Windows.Forms.Button btnExit;
    }
}