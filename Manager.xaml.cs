using Practik.Page_Manager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static MaterialDesignThemes.Wpf.Theme;

namespace Practik
{
    /// <summary>
    /// Логика взаимодействия для Administrator_Wh.xaml
    /// </summary>
    public partial class Administrator_Wh : Window
    {
        WarehouseManagementEntities context = new WarehouseManagementEntities();
        public Administrator_Wh()
        {
            InitializeComponent(); 
            Orders.Visibility = Visibility.Visible;
            Reports.Visibility = Visibility.Visible;
            Products.Visibility = Visibility.Visible;
        }
        private void Products_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Manager_Products();
            Orders.Visibility = Visibility.Hidden;
            Reports.Visibility = Visibility.Hidden;
            Products.Visibility = Visibility.Hidden;
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Manager_Orders();
            Orders.Visibility = Visibility.Hidden;
            Reports.Visibility = Visibility.Hidden;
            Products.Visibility = Visibility.Hidden;
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Manager_Reports();
            Orders.Visibility = Visibility.Hidden;
            Reports.Visibility = Visibility.Hidden;
            Products.Visibility = Visibility.Hidden;
            // 
        }
        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }
    }
}
