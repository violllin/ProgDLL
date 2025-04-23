using System;
using System.Windows.Forms;

namespace DllApp
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var loginForm = new LoginForm();
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                Application.Run(new MainForm(loginForm.AuthenticatedUser));
            }
        }
    }
}
