using Practik.Page_Admin;
using Practik.Page_Admin.Reports_Full;
using Practik.Page_Manager;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Practik
{
    /// <summary>
    /// Логика взаимодействия для Leader_Clients.xaml
    /// </summary>
    public partial class Leader_Clients : Window
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();

        public Leader_Clients()
        {
            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при инициализации окна. Пожалуйста, перемещайтесь по приложению медленнее.", ex);
            }
        }

        private void products_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PageFrame.Content = new Products_Admin();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при переходе на страницу товаров. Пожалуйста, перемещайтесь по приложению медленнее.", ex);
            }
        }

        private void Rep_Prodasi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PageFrame.Content = new Money();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при переходе на страницу отчетов по продажам. Пожалуйста, перемещайтесь по приложению медленнее.", ex);
            }
        }

        private void Rep_Ostatki_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PageFrame.Content = new Old();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при переходе на страницу отчетов по остаткам. Пожалуйста, перемещайтесь по приложению медленнее.", ex);
            }
        }

        private void Rep_Popylarnoe_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PageFrame.Content = new Many();
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при переходе на страницу отчетов по популярным товарам. Пожалуйста, перемещайтесь по приложению медленнее.", ex);
            }
        }

        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {
            
        }

        /// <summary>
        /// Метод для отображения сообщения об ошибке
        /// </summary>
        /// <param name="message">Сообщение для пользователя</param>
        /// <param name="ex">Исключение</param>
        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}\n\nДетали ошибки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}