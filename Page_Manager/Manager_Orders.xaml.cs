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
    /// Логика взаимодействия для Manager_Orders.xaml
    /// </summary>
    public partial class Manager_Orders : Page
    {
        private WarehouseManagementEntities _context = new WarehouseManagementEntities();
        private List<OrderItem> _cart = new List<OrderItem>(); // Корзина в памяти
        private int productId;
        private int orderedQuantity;

        public Manager_Orders()
        {
            InitializeComponent();
            CartDataGrid.ItemsSource = _cart;
        }

        // Добавление товара в корзину
        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            var productWindow = new SelectProductWindow(); // Окно выбора товара
            if (productWindow.ShowDialog() == true)
            {
                var selectedProduct = productWindow.SelectedProduct;
                int quantity = productWindow.SelectedQuantity;

                if (selectedProduct != null && quantity > 0)
                {
                    decimal total = selectedProduct.Price * quantity;
                    _cart.Add(new OrderItem
                    {
                        ProductID = selectedProduct.ID_Product,
                        ProductName = selectedProduct.ProductName,
                        Quantity = quantity,
                        Price = selectedProduct.Price,
                        TotalAmount = total
                    });

                    CartDataGrid.Items.Refresh();
                }
            }
        }

        // Удаление товара из корзины
        private void RemoveFromCart_Click(object sender, RoutedEventArgs e)
        {
            if (CartDataGrid.SelectedItem is OrderItem selectedItem)
            {
                _cart.Remove(selectedItem);
                CartDataGrid.Items.Refresh();
            }
        }

        // Оформление заказа
        private void Checkout_Click(object sender, RoutedEventArgs e)
        {
            if (_cart.Count == 0)
            {
                MessageBox.Show("Корзина пуста!");
                return;
            }

            // Создание заказа
            Orders newOrder = new Orders
            {
                OrderDate = DateTime.Now,
                Client_ID = 1, // Можно добавить выбор клиента
                OrderStatus_ID = 1 // "В обработке"
            };

            _context.Orders.Add(newOrder);
            _context.SaveChanges();

            int orderId = newOrder.ID_Order; // Получаем ID созданного заказа

            // Добавление товаров в OrderDetails
            foreach (var item in _cart)
            {
                OrderDetails orderDetail = new OrderDetails
                {
                    Order_ID = orderId,
                    Product_ID = item.ProductID,
                    Quantity = item.Quantity,
                    TotalAmount = item.TotalAmount
                };

                _context.OrderDetails.Add(orderDetail);

                // Обновляем остаток товара на складе
                // Получаем количество товара на складе
                var warehouseEntry = _context.Warehouse.FirstOrDefault(w => w.Product_ID == productId);

                if (warehouseEntry != null)
                {
                    int availableQuantity = warehouseEntry.Quantity;

                    if (orderedQuantity <= availableQuantity)
                    {
                        // Списываем товар со склада
                        warehouseEntry.Quantity -= orderedQuantity;
                        _context.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show($"Недостаточно товара на складе! Доступно: {availableQuantity}");
                    }
                }
                else
                {
                    MessageBox.Show("Товар отсутствует на складе!");
                }

                _context.SaveChanges();

                // Открываем окно оплаты
                PaymentWindow paymentWindow = new PaymentWindow(orderId);
                paymentWindow.ShowDialog();

                MessageBox.Show("Заказ оформлен!");
                _cart.Clear();
                CartDataGrid.Items.Refresh();
            }
        }

        // Класс для хранения товаров в корзине
        public class OrderItem
        {
            public int ProductID { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal Price { get; set; }
            public decimal TotalAmount { get; set; }
        }
    }
}
