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

namespace Practik.Page_WareHouse
{
    /// <summary>
    /// Логика взаимодействия для Location.xaml
    /// </summary>
    public partial class Location : Page
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();
        public Location()
        {
            InitializeComponent();
            DGPrCrud.ItemsSource = context.ProductLocation.ToList();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            ProductLocation loc = new ProductLocation();
            loc.Section = Sections.Text;
            loc.Shelf = Convert.ToInt32(Shelfs.Text);
            loc.Rack = Convert.ToInt32(Racks.Text);

            context.ProductLocation.Add(loc);
            context.SaveChanges();
            DGPrCrud.ItemsSource = context.ProductLocation.ToList();

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            if (DGPrCrud.SelectedItem != null)
            {
                var selected = DGPrCrud.SelectedItem as ProductLocation;

                selected.Section = Sections.Text;
                selected.Shelf = string.IsNullOrWhiteSpace(Shelfs.Text) ? 0 : Int32.Parse(Shelfs.Text);
                selected.Rack = string.IsNullOrWhiteSpace(Racks.Text) ? 0 : Int32.Parse(Racks.Text);

                context.SaveChanges();
                DGPrCrud.ItemsSource = context.ProductLocation.ToList();

            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (DGPrCrud.SelectedItem != null)
            {
                context.ProductLocation.Remove(DGPrCrud.SelectedItem as ProductLocation);

                context.SaveChanges();
                DGPrCrud.ItemsSource = context.ProductLocation.ToList();
            }
        }

        private void DGPrCrud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
