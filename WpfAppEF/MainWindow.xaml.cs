using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfAppEF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ProductContext productContext = new ProductContext();
        private CollectionViewSource categoryViewSource;
        public MainWindow()
        {
            InitializeComponent();
            categoryViewSource = (CollectionViewSource)FindResource(nameof(categoryViewSource));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            productContext.SaveChanges();

            categoryDataGrid.Items.Refresh();
            productsDataGrid.Items.Refresh();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            productContext.Database.EnsureCreated();
            productContext.Categories.Load();
            categoryViewSource.Source = productContext.Categories.Local.ToObservableCollection();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            productContext.Dispose();
            base.OnClosing(e);
        }
    }
}
