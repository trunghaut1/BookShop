using Caliburn.Micro;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.WindowsUI;
using PropertyChanged;
using Repository.ClientRepository;
using Repository.Helper;
using Repository.Model;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace BookShop.Admin.ViewModels
{
    [ImplementPropertyChanged]
    public class CatViewModel : Screen
    {
        private CatRepository catRepo;
        public string message { get; set; }
        public ObservableCollection<Cat> cats { get; set; }

        public CatViewModel()
        {
            catRepo = new CatRepository();

            cats = new ObservableCollection<Cat>();
            LoadData();
        }

        private async void LoadData()
        {
            var list = await catRepo.GetAll();
            if (list != null)
            {
                cats = new ObservableCollection<Cat>(list);
            }
        }

        public void ClearSelected(GridControl grid)
        {
            grid.SelectedItem = null;
            message = MessageHelper.Get("+");
        }
        public async void AddOrUpdate(int index, int? id, string name)
        {
            if (id != null) // Update
            {
                Cat value = new Cat(id, name);
                bool reusult = await catRepo.Update((int)id, value);
                if (reusult)
                {
                    cats[index] = value;
                    message = MessageHelper.Get("up");
                }
                else
                {
                    message = MessageHelper.Get("upErr");
                }
            }
            else // Add
            {
                Cat value = new Cat(null, name);
                id = await catRepo.Add(value);
                if (id != 0)
                {
                    value.ID = (int)id;
                    cats.Add(value);
                    message = MessageHelper.Get("add");
                }
                else
                {
                    message = MessageHelper.Get("addErr");
                }
            }
        }
        public async void Delete(int index, int? id)
        {
            if (id != null)
            {
                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                var result = WinUIMessageBox.Show(window,
                "Bạn có muốn xóa giá trị này?", "Xác nhận",
                MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None, FloatingMode.Window);
                if (result == MessageBoxResult.Yes)
                {
                    bool del = await catRepo.Delete((int)id);
                    if (del)
                    {
                        cats.RemoveAt(index);
                        message = MessageHelper.Get("del");
                    }
                    else
                    {
                        message = MessageHelper.Get("delErr");
                    }
                }
            }
            else
            {
                message = MessageHelper.Get("delNoti");
            }
        }
    }
}
