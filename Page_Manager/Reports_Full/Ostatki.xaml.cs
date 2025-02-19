using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
            FiltrationDgr.ItemsSource = context.Warehouse.ToList();
        }

        private void ExportToPdf(object sender, RoutedEventArgs e)
        {

        }
    }
}
