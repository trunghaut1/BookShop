using BookShop.Admin.View;
using FlatTheme.ControlStyle;
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

namespace BookShop.Admin
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FlatWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void loadView(string title)
        {
            txtViewTitle.Text = $"QUẢN LÝ {title.ToUpper()}";
            mainGrid.Children.Clear();
            if(panelButton.Visibility == Visibility.Collapsed)
                panelButton.Visibility = Visibility.Visible;
        }

        private void mnUser_Click(object sender, RoutedEventArgs e)
        {
            loadView("USER");
            mainGrid.Children.Add(new UserView());
        }
    }
}
