using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Practik.Page_Admin
{
    public partial class Products_Admin : Page
    {
        // Контекст базы данных
        private WarehouseManagementEntities context = new WarehouseManagementEntities();

        public Products_Admin()
        {
            InitializeComponent();

            // Загрузка данных в ComboBox и DataGrid при запуске страницы
            LoadComboBoxData();
            LoadProductsData();
        }

        #region Загрузка данных

        /// <summary>
        /// Загружает данные в ComboBox (категории, производители, единицы измерения, статусы)
        /// </summary>
        private void LoadComboBoxData()
        {
            try
            {
                // Загрузка категорий товаров
                ComboBoxCategory.ItemsSource = context.ProductCategories.ToList();
                ComboBoxCategory.DisplayMemberPath = "CategoryName";
                ComboBoxCategory.SelectedValuePath = "ID_ProductCategory";

                // Загрузка производителей
                ComboBoxManufacturer.ItemsSource = context.Manufacturers.ToList();
                ComboBoxManufacturer.DisplayMemberPath = "ManufacturerName";
                ComboBoxManufacturer.SelectedValuePath = "ID_Manufacturer";

                // Загрузка единиц измерения
                ComboBoxUnit.ItemsSource = context.ProductUnits.ToList();
                ComboBoxUnit.DisplayMemberPath = "ProductUnitName";
                ComboBoxUnit.SelectedValuePath = "ID_ProductUnit";

                // Загрузка статусов товаров
                ComboBoxStatus.ItemsSource = context.ProductStatuses.ToList();
                ComboBoxStatus.DisplayMemberPath = "ProductStatusName";
                ComboBoxStatus.SelectedValuePath = "ID_ProductStatus";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Загружает данные о товарах в DataGrid
        /// </summary>
        private void LoadProductsData()
        {
            try
            {
                DGPrCrud.ItemsSource = context.Products.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки товаров: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Валидация данных

        /// <summary>
        /// Проверяет корректность введенных данных перед добавлением или обновлением товара
        /// </summary>
        /// <returns>True, если данные корректны, иначе False</returns>
        private bool ValidateProductInput()
        {
            // Проверка названия товара
            if (string.IsNullOrWhiteSpace(ProductName.Text))
            {
                MessageBox.Show("Введите название товара!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Проверка артикула
            if (string.IsNullOrWhiteSpace(Article.Text))
            {
                MessageBox.Show("Введите артикул товара!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Проверка выбора категории
            if (ComboBoxCategory.SelectedItem == null)
            {
                MessageBox.Show("Выберите категорию товара!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Проверка выбора производителя
            if (ComboBoxManufacturer.SelectedItem == null)
            {
                MessageBox.Show("Выберите производителя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Проверка выбора единицы измерения
            if (ComboBoxUnit.SelectedItem == null)
            {
                MessageBox.Show("Выберите единицу измерения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Проверка выбора статуса товара
            if (ComboBoxStatus.SelectedItem == null)
            {
                MessageBox.Show("Выберите статус товара!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            // Проверка цены (должна быть положительным числом)
            if (!decimal.TryParse(Price.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Некорректная цена товара!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        #endregion

        #region Обработка событий кнопок

        /// <summary>
        /// Обработчик кнопки "Добавить"
        /// </summary>
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateProductInput()) return;

            try
            {
                // Парсим цену
                decimal price = decimal.Parse(Price.Text);

                // Получаем ID из выбранных элементов ComboBox
                int categoryId = (ComboBoxCategory.SelectedItem as ProductCategories).ID_ProductCategory;
                int manufacturerId = (ComboBoxManufacturer.SelectedItem as Manufacturers).ID_Manufacturer;
                int unitId = (ComboBoxUnit.SelectedItem as ProductUnits).ID_ProductUnit;
                int statusId = (ComboBoxStatus.SelectedItem as ProductStatuses).ID_ProductStatus;

                // Создаем новый товар
                Products product = new Products
                {
                    ProductName = ProductName.Text,
                    Article = Article.Text,
                    ProductCategory_ID = categoryId,
                    Manufacturer_ID = manufacturerId,
                    ProductUnit_ID = unitId,
                    Price = price,
                    ProductStatus_ID = statusId
                };

                // Добавляем товар в базу данных
                context.Products.Add(product);
                context.SaveChanges();

                MessageBox.Show("Товар успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Обновляем DataGrid
                LoadProductsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Изменить"
        /// </summary>
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DGPrCrud.SelectedItem == null)
            {
                MessageBox.Show("Выберите товар для обновления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!ValidateProductInput()) return;

            try
            {
                // Получаем выбранный товар
                var selected = DGPrCrud.SelectedItem as Products;

                // Обновляем данные товара
                selected.ProductName = ProductName.Text;
                selected.Article = Article.Text;
                selected.Price = decimal.Parse(Price.Text);

                // Обновляем ID из выбранных элементов ComboBox
                selected.ProductCategory_ID = (ComboBoxCategory.SelectedItem as ProductCategories).ID_ProductCategory;
                selected.Manufacturer_ID = (ComboBoxManufacturer.SelectedItem as Manufacturers).ID_Manufacturer;
                selected.ProductUnit_ID = (ComboBoxUnit.SelectedItem as ProductUnits).ID_ProductUnit;
                selected.ProductStatus_ID = (ComboBoxStatus.SelectedItem as ProductStatuses).ID_ProductStatus;

                // Сохраняем изменения в базе данных
                context.SaveChanges();

                MessageBox.Show("Товар успешно обновлён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Обновляем DataGrid
                LoadProductsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик кнопки "Удалить"
        /// </summary>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DGPrCrud.SelectedItem == null)
            {
                MessageBox.Show("Выберите товар для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Получаем выбранный товар
                var selected = DGPrCrud.SelectedItem as Products;

                // Удаляем товар из базы данных
                context.Products.Remove(selected);
                context.SaveChanges();

                MessageBox.Show("Товар успешно удалён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);

                // Обновляем DataGrid
                LoadProductsData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Обработка событий DataGrid

        /// <summary>
        /// Обработчик события выбора товара в DataGrid
        /// </summary>
        private void DGPrCrud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Получаем выбранный товар
                var selected = DGPrCrud.SelectedItem as Products;

                if (selected != null)
                {
                    // Заполняем поля данными выбранного товара
                    ProductName.Text = selected.ProductName;
                    Article.Text = selected.Article;
                    Price.Text = selected.Price.ToString();

                    // Устанавливаем выбранные элементы в ComboBox
                    ComboBoxCategory.SelectedValue = selected.ProductCategory_ID;
                    ComboBoxManufacturer.SelectedValue = selected.Manufacturer_ID;
                    ComboBoxUnit.SelectedValue = selected.ProductUnit_ID;
                    ComboBoxStatus.SelectedValue = selected.ProductStatus_ID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборе товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        #region Обработка событий навигации

        /// <summary>
        /// Обработчик события навигации (перехода на другую страницу)
        /// </summary>
        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {
            // Очистка выделения в DataGrid при переходе на другую страницу
            DGPrCrud.SelectedItem = null;
        }

        #endregion
    }
}