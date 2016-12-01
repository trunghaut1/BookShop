using Caliburn.Micro;
using PropertyChanged;
using System;
using System.Windows;

namespace BookShop.Admin.ViewModels
{
    [ImplementPropertyChanged]
    public class MainViewModel : Conductor<Screen>
    {
        public WindowState windowState { get; set; }
        public Visibility menuVisible { get; set; } = Visibility.Visible;

        public MainViewModel(Type T, string title)
        {
            DisplayName = "BookShop - Quản trị";
            if (T != null)
            {
                windowState = WindowState.Normal;
                menuVisible = Visibility.Collapsed;
                object viewmodel = Activator.CreateInstance(T);
                ActivateItem((Screen)viewmodel);
                loadView(title);
            }
            else
            {
                windowState = WindowState.Maximized;
            } 
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
        public void mnOrder()
        {
            loadView("đơn hàng");
            ActivateItem(new OrderViewModel());
        }
    }
}
