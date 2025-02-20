using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Practik.Page_WareHouse
{
    /// <summary>
    /// Логика взаимодействия для Location.xaml
    /// </summary>
    public partial class Location : Page
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();

        public Location()
        {
            InitializeComponent();
            LoadData();
        }

        #region Методы
        /// <summary>
        /// Загружает данные из базы в DataGrid
        /// </summary>
        private void LoadData()
        {
            DGPrCrud.ItemsSource = context.ProductLocation.ToList();
        }

        /// <summary>
        /// Очищает поля ввода
        /// </summary>
        private void ClearFields()
        {
            Sections.Text = string.Empty;
            Shelfs.Text = string.Empty;
            Racks.Text = string.Empty;
        }

        /// <summary>
        /// Проверяет корректность введенных данных
        /// </summary>
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(Sections.Text))
            {
                MessageBox.Show("Поле 'Секция' не может быть пустым!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(Shelfs.Text, out int shelf) || shelf <= 0)
            {
                MessageBox.Show("Поле 'Стеллаж' должно быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!int.TryParse(Racks.Text, out int rack) || rack <= 0)
            {
                MessageBox.Show("Поле 'Полка' должно быть положительным числом!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
        #endregion

        #region CRUD операции
        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInputs()) return;

            ProductLocation loc = new ProductLocation
            {
                Section = Sections.Text,
                Shelf = int.Parse(Shelfs.Text),
                Rack = int.Parse(Racks.Text)
            };

            context.ProductLocation.Add(loc);
            context.SaveChanges();
            LoadData();
            ClearFields();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DGPrCrud.SelectedItem is ProductLocation selected)
            {
                if (!ValidateInputs()) return;

                selected.Section = Sections.Text;
                selected.Shelf = int.Parse(Shelfs.Text);
                selected.Rack = int.Parse(Racks.Text);

                context.SaveChanges();
                LoadData();
                ClearFields();
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DGPrCrud.SelectedItem is ProductLocation selected)
            {
                if (MessageBox.Show("Вы уверены, что хотите удалить запись?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    context.ProductLocation.Remove(selected);
                    context.SaveChanges();
                    LoadData();
                    ClearFields();
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        #endregion

        #region Обработчики событий
        private void DGPrCrud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DGPrCrud.SelectedItem is ProductLocation selected)
            {
                Sections.Text = selected.Section;
                Shelfs.Text = selected.Shelf.ToString();
                Racks.Text = selected.Rack.ToString();
            }
        }

        /// <summary>
        /// Ограничивает ввод только числами
        /// </summary>
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
        #endregion
    }
}
