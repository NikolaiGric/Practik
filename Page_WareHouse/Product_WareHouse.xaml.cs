using Practik.Page_Manager;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Practik.Page_WareHouse
{
    /// <summary>
    /// Логика взаимодействия для Product_WareHouse.xaml
    /// </summary>
    public partial class Product_WareHouse : Page
    {
        private WarehouseManagementEntities context = new WarehouseManagementEntities();

        public Product_WareHouse()
        {
            InitializeComponent();
            // Инициализация источников данных для фильтрации
            FiltrationDgr.ItemsSource = context.Products.ToList();
            Search4.ItemsSource = context.ProductCategories.ToList();
            Search4.DisplayMemberPath = "CategoryName"; // Отображаемое значение в ComboBox
            Search4.SelectedValuePath = "ID_ProductCategory"; // Значение для ID
            Search5.ItemsSource = context.Manufacturers.ToList();
            Search5.DisplayMemberPath = "ManufacturerName"; // Отображаемое значение в ComboBox
            Search5.SelectedValuePath = "ID_Manufacturer"; // Значение для ID
        }

        #region Поиск и фильтрация

        private void Search1_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Поиск по имени товара
                if (!string.IsNullOrWhiteSpace(Search0.Text))
                {
                    FiltrationDgr.ItemsSource = context.Products
                        .Where(i => i.ProductName.Contains(Search0.Text))
                        .ToList();
                }
                else
                {
                    MessageBox.Show("Введите название товара для поиска.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении поиска: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search2_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Поиск по имени товара (альтернативный метод)
                if (!string.IsNullOrWhiteSpace(Search1.Text))
                {
                    FiltrationDgr.ItemsSource = context.Products
                        .Where(i => i.ProductName.Contains(Search1.Text))
                        .ToList();
                }
                else
                {
                    MessageBox.Show("Введите название товара для поиска.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении поиска: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search3_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Фильтрация по цене от
                if (decimal.TryParse(Search2.Text, out var priceFrom))
                {
                    FiltrationDgr.ItemsSource = context.Products
                        .Where(i => i.Price >= priceFrom)
                        .ToList();
                }
                else if (!string.IsNullOrWhiteSpace(Search2.Text))
                {
                    MessageBox.Show("Введите корректное числовое значение в поле 'Цена от'!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении фильтрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search4_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Фильтрация по цене до
                if (decimal.TryParse(Search3.Text, out var priceTo))
                {
                    FiltrationDgr.ItemsSource = context.Products
                        .Where(i => i.Price <= priceTo)
                        .ToList();
                }
                else if (!string.IsNullOrWhiteSpace(Search3.Text))
                {
                    MessageBox.Show("Введите корректное числовое значение в поле 'Цена до'!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выполнении фильтрации: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Сброс фильтров
                FiltrationDgr.ItemsSource = context.Products.ToList();
                Search4.SelectedItem = null;
                Search5.SelectedItem = null;
                Search0.Clear();
                Search1.Clear();
                Search2.Clear();
                Search3.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сбросе фильтров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Фильтрация по выбранной категории
                if (Search4.SelectedItem is ProductCategories selectedCategory)
                {
                    FiltrationDgr.ItemsSource = context.Products
                        .Where(p => p.ProductCategory_ID == selectedCategory.ID_ProductCategory)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации по категории: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Search5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Фильтрация по выбранному производителю
                if (Search5.SelectedItem is Manufacturers selectedManufacturer)
                {
                    FiltrationDgr.ItemsSource = context.Products
                        .Where(p => p.Manufacturer_ID == selectedManufacturer.ID_Manufacturer) // Исправлено на Manufacturer_ID
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при фильтрации по производителю: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Навигация
        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {
        }
        #endregion
    }
}
