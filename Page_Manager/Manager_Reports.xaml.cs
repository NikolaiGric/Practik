using Practik.Page_Manager.Reports_Full;
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

namespace Practik.Page_Manager
{
    /// <summary>
    /// Логика взаимодействия для Manager_Reports.xaml
    /// </summary>
    public partial class Manager_Reports : Page
    {
        public Manager_Reports()
        {
            InitializeComponent();
            V1.Visibility = Visibility.Visible;
            V2.Visibility = Visibility.Visible;
            V3.Visibility = Visibility.Visible;
            end.Visibility = Visibility.Visible;
        }

        private void PageFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void end_Click(object sender, RoutedEventArgs e)
        {

            V1.Visibility = Visibility.Hidden;
            V2.Visibility = Visibility.Hidden;
            V3.Visibility = Visibility.Hidden;
            end.Visibility = Visibility.Hidden;
            PageFrame.Content = null;

        }

            private void V1_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Prodasi();

        }

        private void V2_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Ostatki();
        }

        private void V3_Click(object sender, RoutedEventArgs e)
        {
            PageFrame.Content = new Populyrnoe();
        }
    }
}
