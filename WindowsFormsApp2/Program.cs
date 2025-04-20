// Program.cs
using System;
using System.Windows.Forms;
using AuthDLL;
using WindowsFormsApp2;  // Добавляем директиву using для пространства имен с LoginForm

namespace MenuApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    Application.Run(new MainForm(loginForm.AuthenticatedUser));
                }
            }
        }
    }
}