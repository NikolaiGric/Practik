using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Practik.Page_Manager
{
    /// <summary>
    /// Логика взаимодействия для Manager_Products.xaml
    /// </summary>
    public partial class Manager_Products : Page
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();

        public Manager_Products()
        {
            InitializeComponent();
            Orders.Visibility = Visibility.Visible;
            Reports.Visibility = Visibility.Visible;
            Products.Visibility = Visibility.Visible;
            FiltrationDgr.ItemsSource = context.Products.ToList();
            Search4.ItemsSource = context.ProductCategories.ToList();
            Search4.DisplayMemberPath = "CategoryName"; // Что показываем в ComboBox
            Search4.SelectedValuePath = "ID_ProductCategory"; // Какой ID будет значением
            Search5.ItemsSource = context.Manufacturers.ToList();
            Search5.DisplayMemberPath = "ManufacturerName"; // Что показываем в ComboBox
            Search5.SelectedValuePath = "ID_Manufacturer"; // Какой ID будет значением

        }

        private void Search1_Click(object sender, RoutedEventArgs e)
        {
            FiltrationDgr.ItemsSource = context.Products.ToList().Where(i => i.ProductName.Contains(Search0.Text));
        }

        private void Search2_Click(object sender, RoutedEventArgs e)
        {
            FiltrationDgr.ItemsSource = context.Products.ToList().Where(i => i.ProductName.Contains(Search1.Text));
        }

        private void Search3_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(Search2.Text, out var priceTo))
            {
                FiltrationDgr.ItemsSource = context.Products.ToList().Where(i => i.Price >= priceTo);
            }
            else
            {
                MessageBox.Show("Введите корректное числовое значение в поле 'Цена до'!");
            }
        }

        private void Search4_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(Search3.Text, out var priceTo))
            {
                FiltrationDgr.ItemsSource = context.Products.ToList().Where(i => i.Price <= priceTo);
            }
            else
            {
                MessageBox.Show("Введите корректное числовое значение в поле 'Цена до'!");
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            FiltrationDgr.ItemsSource = context.Products.ToList();
            Search4.SelectedItem = null;
            Search5.SelectedItem = null;
        }

        private void Search4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Search4.SelectedItem is ProductCategories selectedCategory)
            {
                FiltrationDgr.ItemsSource = context.Products
                    .Where(p => p.ProductCategory_ID == selectedCategory.ID_ProductCategory)
                    .ToList();
            }
        }

        private void Search5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Search5.SelectedItem is Manufacturers selectedManufacturer)
            {
                FiltrationDgr.ItemsSource = context.Products
                    .Where(p => p.ProductCategory_ID == selectedManufacturer.ID_Manufacturer)
                    .ToList();
            }
        }

        //private void Products_Click(object sender, RoutedEventArgs e)
        //{
        //    PageFrame.Content = new Manager_Products();
        //}

        //private void Orders_Click(object sender, RoutedEventArgs e)
        //{
        //    PageFrame.Content = new Manager_Orders();
        //}

        //private void Reports_Click(object sender, RoutedEventArgs e)
        //{
        //    PageFrame.Content = new Manager_Reports();
        //}
        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Products_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Manager_Products();
            Orders.Visibility = Visibility.Hidden;
            Reports.Visibility = Visibility.Hidden;
            Products.Visibility = Visibility.Hidden;
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Manager_Orders();
            Orders.Visibility = Visibility.Hidden;
            Reports.Visibility = Visibility.Hidden;
            Products.Visibility = Visibility.Hidden;
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Manager_Reports();
            Orders.Visibility = Visibility.Hidden;
            Reports.Visibility = Visibility.Hidden;
            Products.Visibility = Visibility.Hidden;
        }
    }
}
