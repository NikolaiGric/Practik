using Practik.Page_WareHouse;
using System;
using System.Windows;
using System.Windows.Navigation;

namespace Practik
{
    /// <summary>
    /// Логика взаимодействия для Administration_Wh_WareHouse.xaml
    /// </summary>
    public partial class Administration_Wh_WareHouse : Window
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();
        public Administration_Wh_WareHouse()
        {
            InitializeComponent();
        }
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Product_WareHouse();
        }

        private void WareHouse_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Location();
        }

        private void Location_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Quantity_WareHouse();
        }
        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
