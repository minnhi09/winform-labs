using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using entity_framework.models;

namespace entity_framework
{
    public partial class frmMain : Form
    {
        private Account? currentUser = null;
        private System.Windows.Forms.Timer? timer;

        public frmMain()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void InitializeTimer()
        {
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000; // Update every second
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            // Disable all menu items except Login and Exit when not logged in
            SetMenuState(false);
            lblDateTime.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        }

        private void SetMenuState(bool isLoggedIn)
        {
            // Enable/disable menu items based on login state
            quảnLýToolStripMenuItem.Enabled = isLoggedIn;
            ngườiDùngToolStripMenuItem.Enabled = isLoggedIn;
            đổiMậtKhẩuToolStripMenuItem.Enabled = isLoggedIn;
            đăngXuấtToolStripMenuItem.Enabled = isLoggedIn;
            đăngNhậpToolStripMenuItem.Enabled = !isLoggedIn;

            if (isLoggedIn)
            {
                lblWelcome.Text = $"Chào mừng bạn đến với hệ thống quản lý nhà hàng!";
            }
            else
            {
                lblWelcome.Text = "Vui lòng đăng nhập để sử dụng hệ thống";
            }
        }

        public void SetLoggedInUser(Account user)
        {
            currentUser = user;
            lblUserName.Text = user.AccountName;
            SetMenuState(true);
        }

        // Hệ thống Menu
        private void đăngNhậpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLogin loginForm = new frmLogin(this);
            loginForm.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                currentUser = null;
                lblUserName.Text = "Chưa đăng nhập";
                SetMenuState(false);
                MessageBox.Show("Đăng xuất thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        // Quản lý Menu
        private void nhàHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmRestaurant());
        }

        private void sảnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmHall());
        }

        private void bànToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmTable());
        }

        private void loạiMónToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmFoodCategory());
        }

        private void mónĂnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmFood());
        }

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentUser == null)
            {
                MessageBox.Show("Vui lòng đăng nhập để sử dụng chức năng này!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            OpenForm(new frmInvoice(currentUser));
        }

        private void chiTiếtHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Vui lòng mở form Hóa đơn và double-click vào hóa đơn cần xem chi tiết!", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // Người dùng Menu
        private void tàiKhoảnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmAccountManagement());
        }

        private void quyềnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenForm(new frmRole());
        }

        // Trợ giúp Menu
        private void hướngDẫnSửDụngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Hướng dẫn sử dụng:\n\n" +
                "1. Đăng nhập vào hệ thống\n" +
                "2. Chọn menu tương ứng để quản lý\n" +
                "3. Sử dụng các chức năng Thêm, Sửa, Xóa để quản lý dữ liệu\n" +
                "4. Đăng xuất khi hoàn thành công việc",
                "Hướng dẫn sử dụng",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("HỆ THỐNG QUẢN LÝ NHÀ HÀNG\n\n" +
                "Phiên bản: 1.0\n" +
                "Ngày phát hành: " + DateTime.Now.ToString("dd/MM/yyyy") + "\n\n" +
                "Phát triển bởi: Nhóm sinh viên\n" +
                "© 2025 All rights reserved.",
                "Thông tin",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        // Helper method to open forms
        private void OpenForm(Form form)
        {
            try
            {
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi mở form: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát ứng dụng?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
