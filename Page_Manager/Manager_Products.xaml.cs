using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Practik.Page_Manager
{
    public partial class Manager_Products : Page
    {
        private WarehouseManagementEntities context = new WarehouseManagementEntities();

        public Manager_Products()
        {
            InitializeComponent();
            LoadData();
        }

        #region Загрузка данных
        private void LoadData()
        {
            // Загрузка данных в DataGrid
            FiltrationDgr.ItemsSource = context.Products.ToList();

            // Заполнение выпадающих списков
            Search4.ItemsSource = context.ProductCategories.ToList();
            Search4.DisplayMemberPath = "CategoryName";
            Search4.SelectedValuePath = "ID_ProductCategory";

            Search5.ItemsSource = context.Manufacturers.ToList();
            Search5.DisplayMemberPath = "ManufacturerName";
            Search5.SelectedValuePath = "ID_Manufacturer";
        }
        #endregion

        #region Поиск по названию товара
        private void Search1_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Search0.Text))
            {
                FiltrationDgr.ItemsSource = context.Products
                    .Where(i => i.ProductName.Contains(Search0.Text))
                    .ToList();
            }
            else
            {
                MessageBox.Show("Введите название товара для поиска.");
            }
        }
        #endregion

        #region Поиск по номеру заказа
        private void Search2_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Search1.Text))
            {
                FiltrationDgr.ItemsSource = context.Products
                    .Where(i => i.ProductName.Contains(Search1.Text))
                    .ToList();
            }
            else
            {
                MessageBox.Show("Введите номер заказа для поиска.");
            }
        }
        #endregion

        #region Поиск по цене
        private void Search3_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(Search2.Text, out var minPrice))
            {
                FiltrationDgr.ItemsSource = context.Products.Where(i => i.Price >= minPrice).ToList();
            }
            else
            {
                MessageBox.Show("Введите корректное числовое значение в поле 'Цена от'!");
            }
        }

        private void Search4_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(Search3.Text, out var maxPrice))
            {
                FiltrationDgr.ItemsSource = context.Products.Where(i => i.Price <= maxPrice).ToList();
            }
            else
            {
                MessageBox.Show("Введите корректное числовое значение в поле 'Цена до'!");
            }
        }
        #endregion

        #region Очистка фильтров
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
            Search0.Clear();
            Search1.Clear();
            Search2.Clear();
            Search3.Clear();
            Search4.SelectedItem = null;
            Search5.SelectedItem = null;
        }
        #endregion

        #region Фильтрация по категории
        private void Search4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Search4.SelectedItem is ProductCategories selectedCategory)
            {
                FiltrationDgr.ItemsSource = context.Products
                    .Where(p => p.ProductCategory_ID == selectedCategory.ID_ProductCategory)
                    .ToList();
            }
        }
        #endregion

        #region Фильтрация по производителю
        private void Search5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Search5.SelectedItem is Manufacturers selectedManufacturer)
            {
                FiltrationDgr.ItemsSource = context.Products
                    .Where(p => p.Manufacturer_ID == selectedManufacturer.ID_Manufacturer)
                    .ToList();
            }
        }
        #endregion

        #region Валидация ввода только чисел
        private void NumericValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !decimal.TryParse(e.Text, out _);
        }
        #endregion
    }
}
