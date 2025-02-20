using Practik.Page_Manager;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Practik
{
    /// <summary>
    /// Логика взаимодействия для Administrator_Wh.xaml
    /// </summary>
    public partial class Administrator_Wh : Window
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();

        public Administrator_Wh()
        {
            InitializeComponent();

            try
            {
                Orders.Visibility = Visibility.Visible;
                Reports.Visibility = Visibility.Visible;
                Products.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при инициализации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PageFrame.Content = new Manager_Products();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке страницы товаров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PageFrame.Content = new Manager_Orders();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке страницы заказов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Manager_Reports mg = new Manager_Reports();
                mg.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии отчетов: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {
        }
    }
}
