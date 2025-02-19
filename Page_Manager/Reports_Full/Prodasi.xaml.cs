using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Practik.Page_Manager.Reports_Full
{
    public partial class Prodasi : Page
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();

        public Prodasi()
        {
            InitializeComponent();
            LoadReport(); // Загружаем данные при открытии страницы
        }

        // Метод загрузки отчета с фильтрацией по дате
        private void LoadReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = context.Orders.AsQueryable();

            if (startDate.HasValue)
                query = query.Where(o => o.ShippingDate >= startDate.Value);

            if (endDate.HasValue)
                query = query.Where(o => o.ShippingDate <= endDate.Value);

            // Выбираем данные для отчета
            var orders = query
                .Select(o => new
                {
                    OrderID = o.ID_Order,
                    o.ShippingDate, // Дата отправки
                    TotalAmount = o.OrderDetails.Sum(od => od.TotalAmount), // Сумма заказа
                    Revenue = o.OrderDetails.Sum(od => od.TotalAmount) * 0.2m // Выручка (20% от суммы)
                })
                .ToList();

            FiltrationDgr.ItemsSource = orders;
        }

        // Фильтрация по датам
        private void FilterReport(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            LoadReport(startDate, endDate);
        }

        // Очистка фильтров
        private void ClearFilters(object sender, RoutedEventArgs e)
        {
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            LoadReport();
        }

        private void ExportToPdf(object sender, RoutedEventArgs e)
        {

        }
    }
}
