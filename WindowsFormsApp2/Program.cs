﻿using System;
using System.Windows.Forms;
using WindowsFormsApp2;

namespace MenuApp
{
    static class Program
    {
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