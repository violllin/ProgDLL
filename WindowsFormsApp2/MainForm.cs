using System;
using System.Drawing;
using System.Windows.Forms;
using AuthDLL;
using MenuDLL;

namespace MenuApp
{
    public partial class MainForm : Form
    {
        private AuthUser user;
        private MenuBuilder menuBuilder;
        private MenuStrip menuStrip1; // Добавляем объявление menuStrip1

        public MainForm(AuthUser authenticatedUser)
        {
            InitializeComponent();
            user = authenticatedUser;
            InitializeMainForm();
        }

        private void InitializeMainForm()
        {
            // Настройка основной формы
            this.Text = "АИС Отдел кадров - Главное окно";
            this.WindowState = FormWindowState.Maximized;
            this.Size = new Size(800, 600);

            // Инициализация меню
            menuStrip1 = new MenuStrip();
            this.Controls.Add(menuStrip1);
            this.MainMenuStrip = menuStrip1;

            // Создание меню
            menuBuilder = new MenuBuilder("menu.txt", user);
            menuBuilder.BuildMenu(menuStrip1, MenuItem_Click);

            // Создание статусной строки
            var statusStrip = new StatusStrip();
            var userStatusLabel = new ToolStripStatusLabel
            {
                Text = $"Пользователь: {user.Username}"
            };
            statusStrip.Items.Add(userStatusLabel);
            this.Controls.Add(statusStrip);
        }

        private void MenuItem_Click(object sender, EventArgs e)
        {
            var menuItem = sender as ToolStripMenuItem;
            if (menuItem != null && menuItem.Tag != null)
            {
                string methodName = menuItem.Tag.ToString();
                MessageBox.Show($"Выбран пункт меню: {menuItem.Text}\nМетод: {methodName}",
                              "Информация",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Information);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(800, 600);
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            // Дополнительная инициализация при загрузке формы
        }
    }
}