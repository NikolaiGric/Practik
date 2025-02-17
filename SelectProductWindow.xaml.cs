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
using System.Windows.Shapes;

namespace Practik
{
    /// <summary>
    /// Логика взаимодействия для SelectProductWindow.xaml
    /// </summary>
    public partial class SelectProductWindow : Window
    {
        private WarehouseManagementEntities _context = new WarehouseManagementEntities();
        public Products SelectedProduct { get; private set; }
        public int SelectedQuantity { get; private set; }

        public SelectProductWindow()
        {
            InitializeComponent();
            ProductsDataGrid.ItemsSource = _context.Products.ToList();
        }

        private void SelectProduct_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is Products product && int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
            {
                SelectedProduct = product;
                SelectedQuantity = quantity;
                DialogResult = true;
            }
            else
            {
                MessageBox.Show("Выберите товар и введите корректное количество!");
            }
        }

    }
}
