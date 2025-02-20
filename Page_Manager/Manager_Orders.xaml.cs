using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Practik
{
    public partial class Manager_Orders : Page
    {
        private WarehouseManagementEntities context = new WarehouseManagementEntities();
        private List<CartItem> cart = new List<CartItem>();

        public Manager_Orders()
        {
            InitializeComponent();
            LoadUsers();
            UpdateCart();
        }

        private void LoadUsers()
        {
            var users = context.Clients.Select(c => new { c.ID_Client, c.SurnameClient }).ToList();
            ComboBoxUsers.ItemsSource = users;
            ComboBoxUsers.DisplayMemberPath = "SurnameClient";
            ComboBoxUsers.SelectedValuePath = "ID_Client";
        }

        private void ButtonAddProduct_Click(object sender, RoutedEventArgs e)
        {
            SelectProductWindow selectProductWindow = new SelectProductWindow();
            if (selectProductWindow.ShowDialog() == true)
            {
                var selectedProduct = selectProductWindow.SelectedProduct;
                var quantity = selectProductWindow.SelectedQuantity;

                if (selectedProduct != null && quantity > 0)
                {
                    var existingItem = cart.FirstOrDefault(c => c.ProductID == selectedProduct.ID_Product);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += quantity;
                    }
                    else
                    {
                        cart.Add(new CartItem
                        {
                            ProductID = selectedProduct.ID_Product,
                            ProductName = selectedProduct.ProductName,
                            Quantity = quantity,
                            Price = selectedProduct.Price,
                            Total = quantity * selectedProduct.Price
                        });
                    }
                    UpdateCart();
                }
            }
        }


        private void ButtonRemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridCart.SelectedItem is CartItem selectedItem)
            {
                cart.Remove(selectedItem);
                UpdateCart();
            }
        }

        private void UpdateCart()
        {
            DataGridCart.ItemsSource = null;
            DataGridCart.ItemsSource = cart;
        }

        private void ButtonCheckout_Click(object sender, RoutedEventArgs e)
        {
            if (!cart.Any())
            {
                MessageBox.Show("Корзина пуста!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (ComboBoxUsers.SelectedValue == null)
            {
                MessageBox.Show("Выберите пользователя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int clientId = (int)ComboBoxUsers.SelectedValue;
            decimal totalAmount = cart.Sum(c => c.Total);
            Orders newOrder = new Orders
            {
                Client_ID = clientId,
                OrderDate = DateTime.Now,
                ShippingDate = DateTime.Now,
                OrderStatus_ID = 2 // Указываем ID статуса по умолчанию (например, "Новый заказ")
            };
            context.Orders.Add(newOrder);
            context.SaveChanges();

            foreach (var item in cart)
            {
                decimal itemTotal = item.Quantity * item.Price;
                totalAmount += itemTotal;

                OrderDetails orderDetail = new OrderDetails
                {
                    Order_ID = newOrder.ID_Order,
                    Product_ID = item.ProductID,
                    Quantity = item.Quantity,
                    TotalAmount = item.Total
                };
                context.OrderDetails.Add(orderDetail);

                var product = context.Warehouse.FirstOrDefault(p => p.Product_ID == item.ProductID);
                if (product != null)
                {
                    product.Quantity -= item.Quantity;
                }
            }

            context.SaveChanges();
            cart.Clear();
            UpdateCart();
            MessageBox.Show("Заказ успешно оформлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }

    public class CartItem
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
    }
}
