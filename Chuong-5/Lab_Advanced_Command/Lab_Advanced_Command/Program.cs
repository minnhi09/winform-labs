namespace Lab_Advanced_Command
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            // Show login form first
            LoginForm loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // If login successful, show main form with user information
                MainForm mainForm = new MainForm();
                mainForm.LoggedInAccountID = loginForm.LoggedInAccountID;
                mainForm.LoggedInUsername = loginForm.LoggedInUsername;
                mainForm.LoggedInFullName = loginForm.LoggedInFullName;
                
                Application.Run(mainForm);
            }
            // If login cancelled, application will exit
        }
    }
}