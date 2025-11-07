using System;
using System.Windows.Forms;
using Chuong_6.BusinessLogic;
using Chuong_6.DataAccess;

namespace Chuong_6.RestaurantManagement
{
    public partial class frmAccountDetail : Form
    {
        private readonly AccountBL accountBL;
        private Account currentAccount;
        private bool isEditMode = false;

        public frmAccountDetail()
        {
            InitializeComponent();
            this.accountBL = new AccountBL();
        }

        public frmAccountDetail(Account account) : this()
        {
            this.currentAccount = account;
            this.isEditMode = true;
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

            if (password != confirmPassword)
            {
                throw new ArgumentException("Mật khẩu xác nhận không khớp!");
            }

            string result = accountBL.AddAccount(accountName, password, fullName, email, phone);
            MessageBox.Show("Thêm tài khoản thành công!", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UpdateAccount()
        {
            string fullName = txtFullName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhone.Text.Trim();

            int affected = accountBL.UpdateAccount(currentAccount.AccountName, fullName, email, phone);

            if (affected > 0)
            {
                MessageBox.Show("Cập nhật tài khoản thành công!", "Thành công",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
