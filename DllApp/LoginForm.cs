using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace DllApp
{
    public partial class LoginForm : Form
    {
        private bool capsLockPressed = false;
        private TextBox usernameTextBox;
        private TextBox passwordTextBox;
        private Label capsLockLabel;

        private TextBox textBox2;
        private TextBox textBox3;
        private PictureBox pictureBox1;
        private TextBox textBox4;
        private TextBox textBox5;
        private Label label1;
        private Label label2;
        private Button button1;
        private Button button2;
        private TextBox textBox1;

        private object _authService;
        public LocalAuthUser AuthenticatedUser { get; private set; }

        private ToolStripStatusLabel capsLockStatusLabel;

        public LoginForm()
        {
            InitializeComponent();
            InitializeStatusBar();
            SetupCapsLockDetection();
            LoadAuthService();
        }

        private void LoadAuthService()
        {
            try
            {
                string dllPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AuthLibrary.dll");

                if (!File.Exists(dllPath))
                {
                    MessageBox.Show($"Файл AuthLibrary.dll не найден: {dllPath}");
                    return;
                }

                Assembly assembly = Assembly.LoadFrom(dllPath);

                // Правильное имя класса - AuthManager в namespace AuthDLL
                Type authServiceType = assembly.GetType("AuthDLL.AuthManager");

                if (authServiceType == null)
                {
                    // Диагностика: выводим все доступные типы
                    string availableTypes = string.Join("\n", assembly.GetTypes().Select(t => t.FullName));
                    MessageBox.Show($"AuthManager не найден. Доступные типы:\n{availableTypes}");
                    return;
                }

                _authService = Activator.CreateInstance(authServiceType, "USERS.txt");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки AuthManager: {ex.Message}");
            }
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.AntiqueWhite;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(12, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox1.Size = new System.Drawing.Size(670, 40);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "АИС Отдел кадров";
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Gold;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Enabled = false;
            this.textBox2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox2.Location = new System.Drawing.Point(12, 58);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox2.Size = new System.Drawing.Size(670, 40);
            this.textBox2.TabIndex = 1;
            this.textBox2.Text = "Версия 1.0.0.0";
            // 
            // textBox3
            // 
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Enabled = false;
            this.textBox3.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox3.Location = new System.Drawing.Point(12, 104);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.textBox3.Size = new System.Drawing.Size(670, 40);
            this.textBox3.TabIndex = 2;
            this.textBox3.Text = "Введите имя пользователя и пароль";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(35, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 96);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(282, 192);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(400, 23);
            this.textBox4.TabIndex = 4;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(282, 248);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.PasswordChar = '*';
            this.textBox5.Size = new System.Drawing.Size(400, 23);
            this.textBox5.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(31, 192);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(158, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Имя пользователя";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(31, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 23);
            this.label2.TabIndex = 7;
            this.label2.Text = "Пароль";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(35, 310);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(115, 30);
            this.button1.TabIndex = 8;
            this.button1.Text = "Вход";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.LoginButton_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.Location = new System.Drawing.Point(569, 310);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(115, 30);
            this.button2.TabIndex = 9;
            this.button2.Text = "Отмена";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // LoginForm
            // 
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(686, 384);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Name = "LoginForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string username = textBox4.Text;
            string password = textBox5.Text;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Введите имя пользователя и пароль", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка инициализации сервиса
            if (_authService == null)
            {
                MessageBox.Show("Ошибка инициализации системы аутентификации", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                Type authServiceType = _authService.GetType();
                MethodInfo authenticateMethod = authServiceType.GetMethod("Authenticate");

                object user = authenticateMethod.Invoke(_authService, new object[] { username, password });

                if (user != null)
                {
                    Type userType = user.GetType();

                    PropertyInfo permissionsProp = userType.GetProperty("MenuPermissions");

                    AuthenticatedUser = new LocalAuthUser
                    {
                        Username = username,
                        Password = password,
                        MenuPermissions = (Dictionary<string, int>)permissionsProp.GetValue(user)
                    };

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверные учетные данные");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка аутентификации: {ex.Message}");
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private string GetCurrentInputLanguage()
        {
            return InputLanguage.CurrentInputLanguage.Culture.DisplayName;
        }

        private void InitializeStatusBar()
        {
            var statusStrip1 = new StatusStrip();

            capsLockStatusLabel = new ToolStripStatusLabel
            {
                Text = "Caps Lock: " + (Control.IsKeyLocked(Keys.CapsLock) ? "ВКЛ" : "ВЫКЛ"),
                Font = new Font("Calibri", 9, FontStyle.Bold),
                ForeColor = Control.IsKeyLocked(Keys.CapsLock) ? Color.Red : Color.Black
            };

            statusStrip1.Items.Add(capsLockStatusLabel);
            this.Controls.Add(statusStrip1);
        }

        private void SetupCapsLockDetection()
        {
            // Подписываемся на события клавиатуры для обоих полей ввода
            textBox4.KeyDown += UpdateCapsLockStatus;
            textBox4.KeyUp += UpdateCapsLockStatus;
            textBox4.Enter += UpdateCapsLockStatus;

            textBox5.KeyDown += UpdateCapsLockStatus;
            textBox5.KeyUp += UpdateCapsLockStatus;
            textBox5.Enter += UpdateCapsLockStatus;

            this.KeyPreview = true;
        }

        private void UpdateCapsLockStatus(object sender, EventArgs e)
        {
            bool isCapsLockOn = Control.IsKeyLocked(Keys.CapsLock);
            capsLockStatusLabel.Text = "Caps Lock: " + (isCapsLockOn ? "ВКЛ" : "ВЫКЛ");
            capsLockStatusLabel.ForeColor = isCapsLockOn ? Color.Red : Color.Black;
        }
    }
}