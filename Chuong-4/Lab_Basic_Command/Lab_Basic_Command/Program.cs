using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab_Basic_Command
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Hiển thị form đăng nhập trước
            LoginForm loginForm = new LoginForm();
            
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // Lưu thông tin đăng nhập vào SessionManager
                SessionManager.Login(
                    loginForm.LoggedInAccountID,
                    loginForm.LoggedInUsername,
                    loginForm.LoggedInDisplayName
                );

                // Nếu đăng nhập thành công, mở MainForm
                Application.Run(new MainForm());
            }
            else
            {
                // Nếu người dùng hủy đăng nhập, thoát ứng dụng
                Application.Exit();
            }
            
            // Application.Run(new CategoryForm());
            // Application.Run(new FoodForm());
        }
    }
}
