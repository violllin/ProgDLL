using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DllApp
{
    public partial class MainForm : Form
    {
        private LocalAuthUser user;
        private MenuStrip menuStrip1;

        public MainForm(LocalAuthUser authenticatedUser)
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

            try
            {
                string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MenuLibrary.dll");

                // 1. Проверка существования файла
                if (!File.Exists(dllPath))
                {
                    MessageBox.Show($"Файл MenuLibrary.dll не найден по пути: {dllPath}");
                    return;
                }

                // 2. Загрузка сборки
                Assembly menuAssembly = Assembly.LoadFrom(dllPath);

                // 3. Получение типа (с учётом правильного namespace)
                Type menuBuilderType = menuAssembly.GetType("MenuDLL.MenuBuilder");

                // Альтернативный вариант поиска типа:
                // Type menuBuilderType = menuAssembly.GetTypes()
                //     .FirstOrDefault(t => t.Name == "MenuBuilder");

                if (menuBuilderType == null)
                {
                    string availableTypes = string.Join("\n", menuAssembly.GetTypes().Select(t => t.FullName));
                    MessageBox.Show($"MenuBuilder не найден. Доступные типы:\n{availableTypes}");
                    return;
                }

                // 4. Создание экземпляра с передачей пользователя
                object menuBuilder;
                if (user != null && user.MenuPermissions != null)
                {
                    // Создаём AuthUser из LocalAuthUser
                    var authUser = new AuthDLL.AuthUser
                    {
                        Username = user.Username,
                        Password = user.Password,
                        MenuPermissions = user.MenuPermissions
                    };

                    menuBuilder = Activator.CreateInstance(menuBuilderType,
                        new object[] { "menu.txt", authUser });
                }
                else
                {
                    menuBuilder = Activator.CreateInstance(menuBuilderType,
                        new object[] { "menu.txt", null });
                }

                // 5. Вызов BuildMenu
                MethodInfo buildMenuMethod = menuBuilderType.GetMethod("BuildMenu");
                buildMenuMethod.Invoke(menuBuilder, new object[] { menuStrip1, (EventHandler)MenuItem_Click });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка инициализации меню: {ex.ToString()}");
            }

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