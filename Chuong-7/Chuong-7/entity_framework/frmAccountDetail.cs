using System;
using System.Windows.Forms;
using entity_framework.models;
using Microsoft.EntityFrameworkCore;

namespace entity_framework
{
    public partial class frmAccountDetail : Form
    {
        private RestaurantContext _context;
        private Account currentAccount;
        private bool isEditMode = false;

        public frmAccountDetail()
        {
            InitializeComponent();
            _context = new RestaurantContext();
        }

        public frmAccountDetail(Account account) : this()
        {
            this.currentAccount = account;
            this.isEditMode = true;
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            _context?.Dispose();
            base.OnFormClosed(e);
        }

        private void frmAccountDetail_Load(object sender, EventArgs e)
        {
            if (isEditMode && currentAccount != null)
            {
                LoadAccountData();
                txtAccountName.ReadOnly = true;
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
                this.Text = "Sửa thông tin tài khoản";
            }
            else
            {
                this.Text = "Thêm tài khoản mới";
            }
        }

        private void LoadAccountData()
        {
            txtAccountName.Text = currentAccount.AccountName;
            txtFullName.Text = currentAccount.FullName;
            txtEmail.Text = currentAccount.Email;
            txtPhone.Text = currentAccount.Phone;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (isEditMode)
                {
                    UpdateAccount();
                }
                else
                {
                    AddAccount();
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddAccount()
        {
            string accountName = txtAccountName.Text.Trim();
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(accountName))
            {
                throw new ArgumentException("Tài khoản không được để trống!");
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new ArgumentException("Mật khẩu không được để trống!");
            }

            if (password != confirmPassword)
            {
                throw new ArgumentException("Mật khẩu xác nhận không khớp!");
            }

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Họ tên không được để trống!");
            }

            // Check if account exists
            var existingAccount = _context.Accounts.Find(accountName);
            if (existingAccount != null)
            {
                throw new ArgumentException("Tài khoản đã tồn tại!");
            }

            var newAccount = new Account
            {
                AccountName = accountName,
                Password = password,
                FullName = fullName,
                Email = string.IsNullOrWhiteSpace(email) ? null : email,
                Phone = phone,
                DateCreated = DateTime.Now
            };

            _context.Accounts.Add(newAccount);
            _context.SaveChanges();

            MessageBox.Show("Thêm tài khoản thành công!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateAccount()
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            if (string.IsNullOrWhiteSpace(fullName))
            {
                throw new ArgumentException("Họ tên không được để trống!");
            }

            var account = _context.Accounts.Find(currentAccount.AccountName);
            if (account != null)
            {
                account.FullName = fullName;
                account.Email = string.IsNullOrWhiteSpace(email) ? null : email;
                account.Phone = phone;

                int affected = _context.SaveChanges();

                if (affected > 0)
                {
                    MessageBox.Show("Cập nhật tài khoản thành công!", "Thành công",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
