using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font;
using iText.Kernel.Font;

namespace Practik.Page_Admin.Reports_Full
{
    /// <summary>
    /// Логика взаимодействия для Money.xaml
    /// </summary>
    public partial class Money : Page
    {
        // Контекст базы данных
        private WarehouseManagementEntities context = new WarehouseManagementEntities();

        public Money()
        {
            InitializeComponent();
            LoadReport(); // Загружаем данные при открытии страницы
        }

        /// <summary>
        /// Метод загрузки отчета с фильтрацией по дате.
        /// </summary>
        /// <param name="startDate">Начальная дата фильтрации.</param>
        /// <param name="endDate">Конечная дата фильтрации.</param>
        private void LoadReport(DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = context.Orders.AsQueryable();

            // Применяем фильтры по дате, если они заданы
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

            // Привязываем данные к DataGrid
            FiltrationDgr.ItemsSource = orders;
        }

        /// <summary>
        /// Обработчик для фильтрации отчета по датам.
        /// </summary>
        private void FilterReport(object sender, RoutedEventArgs e)
        {
            DateTime? startDate = StartDatePicker.SelectedDate;
            DateTime? endDate = EndDatePicker.SelectedDate;

            // Проверка корректности введенных дат
            if (startDate > endDate)
            {
                MessageBox.Show("Начальная дата не может быть больше конечной даты.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            LoadReport(startDate, endDate);
        }

        /// <summary>
        /// Очистка фильтров.
        /// </summary>
        private void ClearFilters(object sender, RoutedEventArgs e)
        {
            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
            LoadReport(); // Перезагружаем отчет без фильтров
        }

        /// <summary>
        /// Экспорт текущего отчета в PDF.
        /// </summary>
        private void ExportToPdf(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Сохранить отчет",
                Filter = "PDF файлы (*.pdf)|*.pdf",
                FileName = "Отчет по продажам.pdf"
            };

            if (dialog.ShowDialog() == true)
            {
                string filePath = dialog.FileName;
                string fontPath = @"C:\Windows\Fonts\arial.ttf"; // Путь к шрифту Arial

                try
                {
                    // Создаем шрифт для PDF
                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

                    using (PdfWriter writer = new PdfWriter(filePath))
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);
                        document.SetFont(font);
                        document.Add(new Paragraph("Отчет по продажам").SetFontSize(18));
                        document.Add(new Paragraph("______________________________________"));
                        document.Add(new Paragraph(" "));

                        // Таблица для данных
                        Table table = new Table(4); // 4 столбца
                        table.AddHeaderCell("ID заказа");
                        table.AddHeaderCell("Дата покупки");
                        table.AddHeaderCell("Сумма заказа");
                        table.AddHeaderCell("Выручка");

                        // Заполнение таблицы данными
                        foreach (var item in FiltrationDgr.ItemsSource)
                        {
                            dynamic row = item;
                            table.AddCell(row.OrderID.ToString());
                            table.AddCell(row.ShippingDate.ToString("dd.MM.yyyy"));
                            table.AddCell(row.TotalAmount.ToString("F2"));
                            table.AddCell(row.Revenue.ToString("F2"));
                        }

                        document.Add(table);
                        document.Close();
                    }

                    MessageBox.Show("Отчет успешно сохранен!", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при создании PDF: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
