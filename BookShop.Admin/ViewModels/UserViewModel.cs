using System.Collections.Generic;
using Repository.Model;
using System.Windows;
using System.Collections.ObjectModel;
using Repository.Helper;
using Caliburn.Micro;
using PropertyChanged;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.WindowsUI;
using Repository.ClientRepository;
using DevExpress.Xpf.Core;

namespace BookShop.Admin.ViewModels
{
    [ImplementPropertyChanged]
    public class UserViewModel : Screen
    {
        private UserRepository userRepo;
        public string message { get; set; }
        public ObservableCollection<User> users { get; set; }

        public UserViewModel()
        {
            userRepo = new UserRepository();

            users = new ObservableCollection<User>();
            LoadData();
        }

        private async void LoadData()
        {
            IEnumerable<User> list = await userRepo.GetAll();
            if (list != null)
            {
                users = new ObservableCollection<User>(list);
            }       
        }

        public void ClearSelected(GridControl grid)
        {
            grid.SelectedItem = null;
            message = MessageHelper.Get("+");
        }
        public async void AddOrUpdate(int index, int? id, string name, string email, string pass, string phone, bool admin)
        {
            if(id != null) // Update
            {
                User value = new User(id, name, email, pass, phone, admin);
                bool result = await userRepo.Update((int)id, value);
                if (result)
                {
                    users[index] = value;
                    message = MessageHelper.Get("up");
                }
                else
                {
                    message = MessageHelper.Get("upErr");
                }
            }
            else // Add
            {
                User value = new User(null, name, email, pass, phone, admin);
                id = await userRepo.Add(value);
                if (id != 0)
                {
                    value.ID = (int)id;
                    users.Add(value);
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
            if(id != null)
            {
                var result = WinUIMessageBox.Show(Application.Current.MainWindow,
                "Bạn có muốn xóa giá trị này?", "Xác nhận",
                MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None,FloatingMode.Window);
                if (result == MessageBoxResult.Yes)
                {
                    bool del = await userRepo.Delete((int)id);
                    if (del)
                    {
                        users.RemoveAt(index);
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
