using System;
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
    public partial class Many : Page
    {
        // Контекст базы данных
        private WarehouseManagementEntities context = new WarehouseManagementEntities();

        public Many()
        {
            try
            {
                InitializeComponent();
                LoadReport(); // Загрузка данных при инициализации страницы
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при инициализации страницы.", ex);
            }
        }

        #region Загрузка данных

        /// <summary>
        /// Загружает данные для отчета по продажам и отображает их в DataGrid.
        /// </summary>
        private void LoadReport(object sender = null, RoutedEventArgs e = null)
        {
            try
            {
                // Загружаем данные из базы в память
                var salesData = context.OrderDetails
                    .Select(od => new
                    {
                        ProductName = od.Products.ProductName, // Название товара
                        Quantity = od.Quantity, // Количество
                        Article = od.Products.Article, // Артикул
                        Price = od.Products.Price, // Цена
                        Total = od.Quantity * od.Products.Price // Сумма (количество * цена)
                    })
                    .ToList(); // Преобразуем в List для дальнейшей обработки

                // Добавляем порядковый номер к каждой строке
                var salesDataWithIndex = salesData
                    .Select((item, index) => new
                    {
                        RowNumber = index + 1, // Нумерация строк начинается с 1
                        item.ProductName,
                        item.Quantity,
                        item.Article,
                        item.Price,
                        item.Total
                    })
                    .ToList();

                // Привязываем данные к DataGrid
                SalesReportDgr.ItemsSource = salesDataWithIndex;
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при загрузке данных отчета.", ex);
            }
        }

        #endregion

        #region Экспорт в PDF

        /// <summary>
        /// Обработчик кнопки "Экспорт в PDF".
        /// </summary>
        private void ExportToPdf(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка наличия данных для экспорта
                if (SalesReportDgr.ItemsSource == null || SalesReportDgr.Items.Count == 0)
                {
                    MessageBox.Show("Нет данных для экспорта.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Диалог сохранения файла
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

                    // Создание PDF-документа
                    PdfFont font = PdfFontFactory.CreateFont(fontPath, PdfEncodings.IDENTITY_H);

                    using (PdfWriter writer = new PdfWriter(filePath))
                    using (PdfDocument pdf = new PdfDocument(writer))
                    {
                        Document document = new Document(pdf);
                        document.SetFont(font);

                        // Заголовок отчета
                        document.Add(new Paragraph("Отчет по продажам").SetFontSize(18));
                        document.Add(new Paragraph("______________________________________"));
                        document.Add(new Paragraph(" "));

                        // Таблица для данных
                        Table table = new Table(6); // 6 столбцов
                        table.AddHeaderCell("№");
                        table.AddHeaderCell("Товар");
                        table.AddHeaderCell("Количество");
                        table.AddHeaderCell("Единица");
                        table.AddHeaderCell("Цена");
                        table.AddHeaderCell("Сумма");

                        // Заполнение таблицы данными
                        foreach (var item in SalesReportDgr.ItemsSource)
                        {
                            dynamic row = item;
                            table.AddCell(row.RowNumber.ToString());
                            table.AddCell(row.ProductName.ToString());
                            table.AddCell(row.Quantity.ToString());
                            table.AddCell(row.Article.ToString());
                            table.AddCell(row.Price.ToString());
                            table.AddCell(row.Total.ToString());
                        }

                        document.Add(table);
                        document.Close();
                    }

                    MessageBox.Show("Отчет успешно сохранен!", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Ошибка при создании PDF.", ex);
            }
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Показывает сообщение об ошибке с деталями исключения.
        /// </summary>
        /// <param name="message">Сообщение для пользователя.</param>
        /// <param name="ex">Исключение.</param>
        private void ShowErrorMessage(string message, Exception ex)
        {
            MessageBox.Show($"{message}\n\nДетали ошибки: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #endregion
    }
}