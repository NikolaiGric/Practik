using MigraDoc.DocumentObjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Practik.Page_WareHouse
{
    /// <summary>
    /// Логика взаимодействия для Quantity_WareHouse.xaml
    /// </summary>
    public partial class Quantity_WareHouse : System.Windows.Controls.Page
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();
        public Quantity_WareHouse()
        {
            InitializeComponent();
            /*ComboBoxProductName.ItemsSource = context.Products.ToList();
            ComboBoxProductName.DisplayMemberPath = "ProductName";
            //ComboBoxProductName.SelectedValuePath = "ID_Product";
            ComboBoxProductLocation.ItemsSource = context.ProductLocation.ToList();
            ComboBoxProductLocation.DisplayMemberPath = "Section";
            //ComboBoxProductLocation.SelectedValuePath = "ID_ProductLocation";*/

            DGPrCrud.ItemsSource = context.Warehouse.ToList();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            Warehouse wr = new Warehouse();
            wr.Quantity = Convert.ToInt32(Quantity.Text);
            /*int productnameId = (ComboBoxProductName.SelectedItem as Products).ID_Product;
            int locationId = (ComboBoxProductLocation.SelectedItem as ProductLocation).ID_ProductLocation;
            wr.Product_ID = productnameId;
            wr.ProductLocation_ID = locationId;

            context.Warehouse.Add(wr);*/
            context.SaveChanges();
            DGPrCrud.ItemsSource = context.Warehouse.ToList();

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DGPrCrud.SelectedItem != null)
            {
                var selected = DGPrCrud.SelectedItem as Warehouse;

                /*selected.Quantity = string.IsNullOrWhiteSpace(Quantity.Text) ? 0 : Int32.Parse(Quantity.Text);
                selected.Product_ID = (ComboBoxProductName.SelectedItem as Products).ID_Product;
                selected.ProductLocation_ID = (ComboBoxProductLocation.SelectedItem as ProductLocation).ID_ProductLocation; 


                context.SaveChanges();
                DGPrCrud.ItemsSource = context.Warehouse.ToList();
*/
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DGPrCrud.SelectedItem != null)
            {
                context.Warehouse.Remove(DGPrCrud.SelectedItem as Warehouse);

                context.SaveChanges();
                DGPrCrud.ItemsSource = context.Warehouse.ToList();
            }
        }

        private void DGPrCrud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Получаем выбранный товар
                var selected = DGPrCrud.SelectedItem as Products;

                /*if (selected != null)
                {
                    // Заполняем поля данными выбранного товара
                    Quantity.Text = selected.ProductName;

                    // Устанавливаем выбранные элементы в ComboBox
                    ComboBoxShelf.SelectedValue = selected.ProductCategory_ID;
                    ComboBoxManufacturer.SelectedValue = selected.Manufacturer_ID;
                    ComboBoxUnit.SelectedValue = selected.ProductUnit_ID;
                }*/
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при выборе товара: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
