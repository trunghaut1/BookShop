using Caliburn.Micro;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.WindowsUI;
using PropertyChanged;
using Repository.ClientRepository;
using Repository.Model;
using System;
using System.Linq;
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
                CheckConnect();
            } 
        }
        private async void CheckConnect()
        {
            BookRepository repo = new BookRepository();
            bool result = await repo.CheckConnect();
            if(!result)
            {
                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                WinUIMessageBox.Show(window,
                "Không thể kết nối đến máy chủ!", "Lỗi",
                MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None, FloatingMode.Window);
            }
        }
        public MainViewModel(Book book)
        {
            DisplayName = "BookShop - Quản trị";
            windowState = WindowState.Normal;
            menuVisible = Visibility.Collapsed;
            ActivateItem(new RecommendViewModel(book));
            loadView("quản lý gợi ý sản phẩm");
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
        public void mnTime()
        {
            loadView("thời gian");
            ActivateItem(new TimeViewModel());
        }
    }
}
