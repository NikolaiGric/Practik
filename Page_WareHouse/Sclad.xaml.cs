using Practik.Page_Manager;
using Practik.Page_WareHouse;
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
