using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Data;
using System.Reflection;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Windows.Forms;
namespace Practik.Page_Manager.Reports_Full
{
    /// <summary>
    /// Логика взаимодействия для Prodasi.xaml
    /// </summary>
    public partial class Prodasi : Page
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();
        public Prodasi()
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

            //Creating iTextSharp Table from the DataTable data
            PdfPTable pdfTable = new PdfPTable(FiltrationDgr.FrozenColumnCount);
            pdfTable.DefaultCell.Padding = 3;
            pdfTable.WidthPercentage = 30;
            pdfTable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdfTable.DefaultCell.BorderWidth = 1;

            ////Adding Header row
            //foreach (DataGridViewColumn column in FiltrationDgr.Columns)
            //{
            //    PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
            //    cell.BackgroundColor = new iTextSharp.text.Color(240, 240, 240);
            //    pdfTable.AddCell(cell);
            //}

            ////Adding DataRow
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    foreach (DataGridViewCell cell in row.Cells)
            //    {
            //        pdfTable.AddCell(cell.Value.ToString());
            //    }
            //}

            //Exporting to PDF
            string folderPath = "C:\\PDFs\\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + "DataGridViewExport.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                pdfDoc.Add(pdfTable);
                pdfDoc.Close();
                stream.Close();
            }
        }
    }
}
