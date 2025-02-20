using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms.VisualStyles;

namespace Practik.Page_WareHouse
{
    /// <summary>
    /// Логика взаимодействия для Quantity_WareHouse.xaml
    /// </summary>
    public partial class Quantity_WareHouse : Page
    {
        private WarehouseManagementEntities context = new WarehouseManagementEntities();

        public Quantity_WareHouse()
        {
            InitializeComponent();
            LoadData();
        }

        #region Загрузка данных
        private void LoadData()
        {
            try
            {
                DGPrCrud.ItemsSource = context.Warehouse.ToList();

                // Заполняем ComboBox данными
                ComboBoxSection.ItemsSource = context.ProductLocation.Select(p => p.Section).Distinct().ToList();
                ComboBoxShelf.ItemsSource = context.ProductLocation.Select(p => p.Shelf).Distinct().ToList();
                ComboBoxRack.ItemsSource = context.ProductLocation.Select(p => p.Rack).Distinct().ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Добавление записи
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Валидация данных перед добавлением
                if (!int.TryParse(Quantity.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Введите корректное количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                if (ComboBoxSection.SelectedItem == null || ComboBoxShelf.SelectedItem == null || ComboBoxRack.SelectedItem == null)
                {
                    MessageBox.Show("Выберите корректное местоположение!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Warehouse newWarehouse = new Warehouse
                {
                    Quantity = quantity,
                    ProductLocation = context.ProductLocation.FirstOrDefault(p =>
                        p.Section == ComboBoxSection.SelectedItem.ToString() &&
                        p.Shelf == Convert.ToInt32(ComboBoxShelf.SelectedItem.ToString()) &&
                        p.Rack == Convert.ToInt32(ComboBoxShelf.SelectedItem.ToString()))
                };

                if (newWarehouse.ProductLocation == null)
                {
                    MessageBox.Show("Выбранное местоположение не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                context.Warehouse.Add(newWarehouse);
                context.SaveChanges();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Изменение записи
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGPrCrud.SelectedItem == null)
                {
                    MessageBox.Show("Выберите запись для изменения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Warehouse selected = DGPrCrud.SelectedItem as Warehouse;

                if (!int.TryParse(Quantity.Text, out int quantity) || quantity <= 0)
                {
                    MessageBox.Show("Введите корректное количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (ComboBoxSection.SelectedItem == null || ComboBoxShelf.SelectedItem == null || ComboBoxRack.SelectedItem == null)
                {
                    MessageBox.Show("Выберите корректное местоположение!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                selected.Quantity = quantity;
                selected.ProductLocation = context.ProductLocation.FirstOrDefault(p =>
                    p.Section == ComboBoxSection.SelectedItem.ToString() &&
                    p.Shelf == Convert.ToInt32(ComboBoxShelf.SelectedItem.ToString()) &&
                    p.Rack == Convert.ToInt32(ComboBoxShelf.SelectedItem.ToString()));

                if (selected.ProductLocation == null)
                {
                    MessageBox.Show("Выбранное местоположение не существует!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                context.SaveChanges();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Удаление записи
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (DGPrCrud.SelectedItem == null)
                {
                    MessageBox.Show("Выберите запись для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                Warehouse selected = DGPrCrud.SelectedItem as Warehouse;
                context.Warehouse.Remove(selected);
                context.SaveChanges();
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion

        #region Обработчик выбора в DataGrid
        private void DGPrCrud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Warehouse selected = DGPrCrud.SelectedItem as Warehouse;

                if (selected != null)
                {
                    Quantity.Text = selected.Quantity.ToString();
                    ComboBoxSection.SelectedItem = selected.ProductLocation.Section;
                    ComboBoxShelf.SelectedItem = selected.ProductLocation.Shelf;
                    ComboBoxRack.SelectedItem = selected.ProductLocation.Rack;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборе записи: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        #endregion
    }
}
