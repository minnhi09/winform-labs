namespace Lab_Advanced_Command
{
    public partial class MainForm : Form
    {
        // Properties to store logged-in user information
        public int LoggedInAccountID { get; set; }
        public string LoggedInUsername { get; set; } = string.Empty;
        public string LoggedInFullName { get; set; } = string.Empty;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Display logged-in user information
            if (!string.IsNullOrEmpty(LoggedInFullName))
            {
                lblWelcome.Text = $"Xin chào, {LoggedInFullName}!";
                lblUsername.Text = $"Tài khoản: {LoggedInUsername}";
            }
        }

        private void btnFoodManagement_Click(object sender, EventArgs e)
        {
            FoodForm foodForm = new FoodForm();
            foodForm.ShowDialog(this);
        }

        private void btnOrderManagement_Click(object sender, EventArgs e)
        {
            OrdersForm ordersForm = new OrdersForm();
            ordersForm.LoggedInAccountID = LoggedInAccountID;
            ordersForm.LoggedInUsername = LoggedInUsername;
            ordersForm.ShowDialog(this);
        }

        private void btnAccountManagement_Click(object sender, EventArgs e)
        {
            AccountForm accountForm = new AccountForm();
            accountForm.ShowDialog(this);
        }

        private void btnRoleManagement_Click(object sender, EventArgs e)
        {
            RoleForm roleForm = new RoleForm();
            roleForm.ShowDialog(this);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Xác nhận đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Hide();
                
                LoginForm loginForm = new LoginForm();
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    // Update logged-in user information
                    LoggedInAccountID = loginForm.LoggedInAccountID;
                    LoggedInUsername = loginForm.LoggedInUsername;
                    LoggedInFullName = loginForm.LoggedInFullName;

                    lblWelcome.Text = $"Xin chào, {LoggedInFullName}!";
                    lblUsername.Text = $"Tài khoản: {LoggedInUsername}";

                    this.Show();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Bạn có chắc chắn muốn thoát ứng dụng?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
