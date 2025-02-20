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
            LoadProducts();
        }

        public class ProductDisplay
        {
            public int ID_Product { get; set; }
            public string ProductName { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
        }

        private void LoadProducts()
        {
            var products = context.Products
                .Select(p => new ProductDisplay
                {
                    ID_Product = p.ID_Product,
                    ProductName = p.ProductName,
                    Price = p.Price,
                    Quantity = context.Warehouse
                        .Where(w => w.Product_ID == p.ID_Product)
                        .Sum(w => (int?)w.Quantity) ?? 0
                })
                .Where(p => p.Quantity > 0) // Только товары, которые есть на складе
                .ToList();

            ProductsDataGrid.ItemsSource = products;
        }

        private void ButtonSelect_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsDataGrid.SelectedItem is ProductDisplay selectedProduct)
            {
                if (int.TryParse(QuantityTextBox.Text, out int quantity) && quantity > 0)
                {
                    if (quantity > selectedProduct.Quantity)
                    {
                        MessageBox.Show("Недостаточное количество товара на складе!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return;
                    }

                    SelectedProduct = context.Products.FirstOrDefault(p => p.ID_Product == selectedProduct.ID_Product);
                    SelectedQuantity = quantity;
                    DialogResult = true;
                    Close();
                }
                else
                {
                    MessageBox.Show("Введите корректное количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Выберите товар!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
