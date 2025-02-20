using PdfSharp.UniversalAccessibility;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Data.Entity;


namespace Practik
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();
        public MainWindow()
        {
            InitializeComponent();
            Administrator_Wh ad = new Administrator_Wh();
            ad.Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var login = tbLogin.Text;
            var password = tbPassword.Password;

            // Находим пользователя по логину и паролю
            var user = context.Employees.FirstOrDefault(u => u.Login == login && u.Password == password);

            if (user != null)
            {
                MessageBox.Show("Вход выполнен!");

                // Открываем нужное окно в зависимости от RoleID
                switch (user.Role_ID)
                {
                    case 1:
                        Administrator_Wh manager = new Administrator_Wh();
                        manager.Show();
                        break;
                    case 2:
                        Administration_Wh_WareHouse warehouseAdmin = new Administration_Wh_WareHouse();
                        warehouseAdmin.Show();
                        break;
                    case 3:
                        Leader_Clients boss = new Leader_Clients();
                        boss.Show();
                        break;
                    default:
                        MessageBox.Show("Роль не определена!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                }

                this.Close(); // Закрываем текущее окно
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }
    }
}
