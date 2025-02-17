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
            InitializeComponent();
            //LoadComboBoxData();
            //DGPrCrud.ItemsSource = context.Products.ToList();

        }

        private void products_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rep_Prodasi_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rep_Ostatki_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rep_Popylarnoe_Click(object sender, RoutedEventArgs e)
        {

        }
        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
        //private void LoadComboBoxData()
        //{
        //    try
        //    {
        //        ComboBoxCategory.ItemsSource = context.ProductCategories.ToList();
        //        ComboBoxCategory.DisplayMemberPath = "CategoryName";
        //        ComboBoxCategory.SelectedValuePath = "ID_ProductCategory";

        //        ComboBoxManufacturer.ItemsSource = context.Manufacturers.ToList();
        //        ComboBoxManufacturer.DisplayMemberPath = "ManufacturerName";
        //        ComboBoxManufacturer.SelectedValuePath = "ID_Manufacturer";

        //        ComboBoxUnit.ItemsSource = context.ProductUnits.ToList();
        //        ComboBoxUnit.DisplayMemberPath = "ProductUnitName";
        //        ComboBoxUnit.SelectedValuePath = "ID_ProductUnit";

        //        ComboBoxStatus.ItemsSource = context.ProductStatuses.ToList();
        //        ComboBoxStatus.DisplayMemberPath = "ProductStatusName";
        //        ComboBoxStatus.SelectedValuePath = "ID_ProductStatus";
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private bool ValidateProductInput()
        //{
        //    if (string.IsNullOrWhiteSpace(ProductName.Text) ||
        //        string.IsNullOrWhiteSpace(Article.Text) ||
        //        ComboBoxCategory.SelectedItem == null ||
        //        ComboBoxManufacturer.SelectedItem == null ||
        //        ComboBoxUnit.SelectedItem == null ||
        //        ComboBoxStatus.SelectedItem == null ||
        //        string.IsNullOrWhiteSpace(Price.Text))
        //    {
        //        MessageBox.Show("Заполните все обязательные поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return false;
        //    }
        //    return true;
        //}

        //private void Create_Click(object sender, RoutedEventArgs e)
        //{
        //    //Products products = new Products();
        //    //products.ProductName = ProductName.Text;

        //    //context.Products.Add(products);

        //    //context.SaveChanges();
        //    //DGPrCrud.ItemsSource = context.Products.ToList();
        //    if (!ValidateProductInput()) return;

        //    // Парсим значения
        //    decimal discount = Decimal.Parse(Discount.Text);
        //    decimal price = Decimal.Parse(Price.Text);

        //    if (price == 0)
        //    {
        //        MessageBox.Show("Некорректный формат цены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        return;
        //    }

        //    // Получаем ID из выбранных ComboBox
        //    int categoryId = (ComboBoxCategory.SelectedItem as ProductCategories).ID_ProductCategory;
        //    int manufacturerId = (ComboBoxManufacturer.SelectedItem as Manufacturers).ID_Manufacturer;
        //    int unitId = (ComboBoxUnit.SelectedItem as ProductUnits).ID_ProductUnit;
        //    int statusId = (ComboBoxStatus.SelectedItem as ProductStatuses).ID_ProductStatus;

        //    // Создаем объект товара
        //    Products product = new Products
        //    {
        //        ProductName = ProductName.Text,
        //        Article = Article.Text,
        //        ProductCategory_ID = categoryId,
        //        Manufacturer_ID = manufacturerId,
        //        ProductUnit_ID = unitId,
        //        Discount = discount,
        //        Price = price,
        //        ProductStatus_ID = statusId
        //    };

        //    try
        //    {
        //        context.Products.Add(product);
        //        context.SaveChanges();
        //        MessageBox.Show("Товар успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        //        // Обновляем DataGrid
        //        DGPrCrud.ItemsSource = context.Products.ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void Update_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DGPrCrud.SelectedItem != null)
        //    {
        //        var selected = DGPrCrud.SelectedItem as Products;

        //        selected.ProductName = ProductName.Text;
        //        selected.Article = Article.Text;
        //        selected.Discount = string.IsNullOrWhiteSpace(Discount.Text) ? 0 : Decimal.Parse(Discount.Text);
        //        selected.Price = string.IsNullOrWhiteSpace(Price.Text) ? 0 : Decimal.Parse(Price.Text);

        //        // Получаем ID из выбранных ComboBox
        //        selected.ProductCategory_ID = (ComboBoxCategory.SelectedItem as ProductCategories).ID_ProductCategory;
        //        selected.Manufacturer_ID = (ComboBoxManufacturer.SelectedItem as Manufacturers).ID_Manufacturer;
        //        selected.ProductUnit_ID = (ComboBoxUnit.SelectedItem as ProductUnits).ID_ProductUnit;
        //        selected.ProductStatus_ID = (ComboBoxStatus.SelectedItem as ProductStatuses).ID_ProductStatus;

        //        try
        //        {
        //            context.SaveChanges();
        //            // Обновляем DataGrid
        //            DGPrCrud.ItemsSource = context.Products.ToList();

        //            MessageBox.Show("Товар успешно обновлён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show($"Ошибка при обновлении товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        //        }
        //    }
        //    else
        //    {
        //        MessageBox.Show("Выберите товар для обновления или!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }

        //}

        //private void Delete_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DGPrCrud.SelectedItem != null)
        //    {

        //        context.Products.Remove(DGPrCrud.SelectedItem as Products);

        //        context.SaveChanges();
        //        // Обновляем DataGrid
        //        DGPrCrud.ItemsSource = context.Products.ToList();

        //        // Снимаем выделение с DataGrid
        //        DGPrCrud.SelectedItem = null;

        //        MessageBox.Show("Товар успешно обновлён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

        //    }
        //    else
        //    {
        //        MessageBox.Show("Выберите товар для удаления или!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        //доделать потом почему то вылазит
        //    }
        //}

        //private void DGPrCrud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    var selected = DGPrCrud.SelectedItem as Products;

        //    if (selected != null)
        //    {
        //        ProductName.Text = selected.ProductName;
        //        Article.Text = selected.Article;
        //        Discount.Text = selected.Discount.ToString();
        //        Price.Text = selected.Price.ToString();

        //        // Устанавливаем выбранные элементы в ComboBox
        //        ComboBoxCategory.SelectedValue = selected.ProductCategory_ID;
        //        ComboBoxManufacturer.SelectedValue = selected.Manufacturer_ID;
        //        ComboBoxUnit.SelectedValue = selected.ProductUnit_ID;
        //        ComboBoxStatus.SelectedValue = selected.ProductStatus_ID;
        //    }
        //    else
        //    {
        //        MessageBox.Show("Выберите товар для редактирования.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        //доделать потом почему то вылазит
        //    }
        //}

    }
}
