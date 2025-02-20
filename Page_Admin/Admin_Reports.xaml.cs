using Practik.Page_Manager;
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

namespace Practik.Page_Admin
{
    /// <summary>
    /// Логика взаимодействия для Admin_Reports.xaml
    /// </summary>
    public partial class Admin_Reports : Page
    {
        public Admin_Reports()
        {
            InitializeComponent();
        }

        private void products_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Manager_Products();
        }

        private void Orders_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Manager_Orders();
        }

        private void Reports_Click(object sender, RoutedEventArgs e)
        {
             
        }
        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DGPrCrud_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
