using System.Linq;
using System.Windows;

namespace Practik
{
    public partial class SelectProductWindow : Window
    {
        private WarehouseManagementEntities context = new WarehouseManagementEntities();
        public Products SelectedProduct { get; private set; }
        public int SelectedQuantity { get; private set; }

        public SelectProductWindow()
        {
            InitializeComponent();
            //LoadProducts();
        }

        //private void LoadProducts()
        //{
        //    var products = context.Products
        //        .Select(p => new
        //        {
        //            p.ID_Product,
        //            p.ProductName,
        //            p.Price,
        //            Quantity = context.Warehouse.Where(w => w.ID_Product == p.ID_Product).Sum(w => w.Quantity)
        //        }).ToList();

        //    DataGridProducts.ItemsSource = products;
        //}

        //private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        //{
        //    if (DataGridProducts.SelectedItem is dynamic selectedProduct)
        //    {
        //        if (int.TryParse(TextBoxQuantity.Text, out int quantity) && quantity > 0)
        //        {
        //            SelectedProduct = context.Products.Find(selectedProduct.ID_Product);
        //            SelectedQuantity = quantity;
        //            DialogResult = true;
        //            Close();
        //        }
        //        else
        //        {
        //            MessageBox.Show("Введите корректное количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
        //        }
        //    }
        //}
    }
}
