using BookShop.Admin.Views;
using Caliburn.Micro;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BookShop.Admin.ViewModels
{
    [ImplementPropertyChanged]
    public class MainViewModel : Conductor<Screen>
    {
        public MainViewModel()
        {
            DisplayName = "BookShop - Quản trị";
        }
        public string txtViewTitle { get; set; }

        private void loadView(string title)
        {
            txtViewTitle = $"QUẢN LÝ {title.ToUpper()}";
        }
        public void mnUser()
        {
            loadView("người dùng");
            ActivateItem(new UserViewModel());
        }
        public void mnCat()
        {
            loadView("thể loại chính");
            ActivateItem(new CatViewModel());
        }
        public void mnSubCat()
        {
            loadView("thể loại con");
            ActivateItem(new SubCatViewModel());
        }
        public void mnBook()
        {
            loadView("sách");
            ActivateItem(new BookViewModel());
        }
    }
}
