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
    /// Логика взаимодействия для PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        private WarehouseManagementEntities _context = new WarehouseManagementEntities();
        private int _orderId;
        private decimal _totalAmount;

        public PaymentWindow(int orderId)
        {
            InitializeComponent();
            _orderId = orderId;

            // Получаем сумму заказа
            _totalAmount = _context.OrderDetails
                                   .Where(od => od.Order_ID == _orderId)
                                   .Sum(od => od.TotalAmount);

            TotalAmountText.Text = $"{_totalAmount:C}"; // Форматируем как валюту
        }

        // Нажатие кнопки "Оплатить"
        private void Pay_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(PaymentAmountTextBox.Text, out decimal paymentAmount))
            {
                if (paymentAmount >= _totalAmount)
                {
                    // Обновляем статус заказа на "Оплачен"
                    var order = _context.Orders.FirstOrDefault(o => o.ID_Order == _orderId);
                    if (order != null)
                    {
                        order.OrderStatus_ID = 2; // ID статуса "Оплачен"
                        _context.SaveChanges();
                    }

                    MessageBox.Show($"Оплата прошла успешно!\nСдача: {(paymentAmount - _totalAmount):C}");
                    this.DialogResult = true;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Недостаточная сумма. Введите корректную сумму оплаты.");
                }
            }
            else
            {
                MessageBox.Show("Введите корректную сумму!");
            }
        }
    }
}
