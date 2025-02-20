using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.IO.Font;
using iText.Kernel.Font;

namespace Practik.Page_Manager.Reports_Full
{
    /// <summary>
    /// Логика взаимодействия для Ostatki.xaml
    /// </summary>
    public partial class Ostatki : Page
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();
        public Ostatki()
        {
            InitializeComponent();
            // Загружаем данные о товарах на складе
            FiltrationDgr.ItemsSource = context.Warehouse.ToList();
        }

        /// <summary>
        /// Экспорт текущего отчета о товарах на складе в PDF.
        /// </summary>
        private void ExportToPdf(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                Title = "Сохранить отчет",
                Filter = "PDF файлы (*.pdf)|*.pdf",
                FileName = "Отчет по товарам на складе.pdf"
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
                        document.Add(new Paragraph("Отчет по товарам на складе").SetFontSize(18));
                        document.Add(new Paragraph("______________________________________"));
                        document.Add(new Paragraph(" "));

                        // Таблица для данных
                        Table table = new Table(2); // 2 столбца
                        table.AddHeaderCell("Название Товара");
                        table.AddHeaderCell("Осталось на складе");

                        // Заполнение таблицы данными
                        foreach (var item in FiltrationDgr.ItemsSource)
                        {
                            dynamic row = item;
                            table.AddCell(row.ProductName.ToString());
                            table.AddCell(row.Quantity.ToString());
                        }

                        document.Add(table);
                        document.Close();
                    }

                    MessageBox.Show("Отчет успешно сохранен!", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    // Обработка исключений при создании PDF
                    MessageBox.Show($"Ошибка при создании PDF: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
