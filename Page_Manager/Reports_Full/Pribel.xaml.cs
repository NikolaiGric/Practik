using PdfSharp;
using MigraDoc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp;
using MigraDoc;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Practik.Page_Manager.Reports_Full
{
    /// <summary>
    /// Логика взаимодействия для Pribel.xaml
    /// </summary>
    public partial class Pribel : Page
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();
        public Pribel()
        {
            InitializeComponent();
            LoadReport(); // Загружаем данные при инициализации страницы
        }

        // Метод загрузки отчета с опциональной фильтрацией по дате
        private void LoadReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            // Базовый запрос к таблице заказов
            var query = context.Orders.AsQueryable();

            // Если задана дата начала, фильтруем данные
            if (startDate.HasValue)
            {
                query = query.Where(o => o.ShippingDate >= startDate.Value);
            }
            // Если задана дата окончания, фильтруем данные
            if (endDate.HasValue)
            {
                query = query.Where(o => o.ShippingDate <= endDate.Value);
            }

            // Выбираем необходимые поля и вычисляем выручку как 20% от цены товара
            //var orders = query.Select(o => new
            //{
            //    o.ID_Order,
            //    ProductName = o.Products.ProductName,
            //    o.ShippingDate,
            //    o.TotalAmount,
            //    o.Price,
            //    Revenue = o.Price * 0.2m  // Выручка = 20% от цены
            //}).ToList();

            //FiltrationDgr.ItemsSource = orders;
        }

        // Обработчик кнопки "Показать отчет" для фильтрации по датам
        private void FilterReport(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;
            LoadReport(startDate, endDate);
        }

        // Обработчик кнопки "Очистить" для сброса фильтров
        private void ClearFilters(object sender, RoutedEventArgs e)
        {
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            LoadReport();
        }

        // Экспорт текущего отчета в PDF с помощью PdfSharp
        private void ExportToPdf(object sender, RoutedEventArgs e)
        {
            //// Создаем новый PDF документ
            //var pdf = new PdfDocument();
            //pdf.Info.Title = "Отчет о прибыли";

            //var page = pdf.AddPage();
            //var gfx = XGraphics.FromPdfPage(page);
            //var font = new XFont("Arial", 12);

            //// Рисуем заголовок отчета
            //gfx.DrawString("Отчет о прибыли", font, XBrushes.Black, new XPoint(50, 50));

            //int yPosition = 100;
            //// Перебираем элементы, привязанные к DataGrid
            //foreach (var item in FiltrationDgr.ItemsSource)
            //{
            //    // Используем reflection для доступа к свойствам анонимного объекта
            //    var idOrder = item.GetType().GetProperty("ID_Order").GetValue(item, null);
            //    var productName = item.GetType().GetProperty("ProductName").GetValue(item, null);
            //    var shippingDate = item.GetType().GetProperty("ShippingDate").GetValue(item, null);
            //    var totalAmount = item.GetType().GetProperty("TotalAmount").GetValue(item, null);
            //    var revenue = item.GetType().GetProperty("Revenue").GetValue(item, null);

            //    string line = $"ID Заказа: {idOrder}, Название: {productName}, Дата: {((DateTime)shippingDate):d}, Цена: {totalAmount}, Выручка: {revenue}";
            //    gfx.DrawString(line, font, XBrushes.Black, new XPoint(50, yPosition));
            //    yPosition += 20;
            //}

            //// Сохраняем PDF файл и открываем его
            //string filename = "Report.pdf";
            //pdf.Save(filename);
            //Process.Start(filename);
        }
    }
}