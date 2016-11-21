using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Repository.Helper;
using Caliburn.Micro;
using PropertyChanged;
using System.Windows.Controls;
using DevExpress.Xpf.Grid;
using Repository.ClientRepository;

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
            LoadData();
        }

        private async void LoadData()
        {
            var list = await userRepo.GetAll("user");
            if (list != null)
            {
                users = new ObservableCollection<User>(list);
            }     
            else
            {
                users = new ObservableCollection<User>();
            }       
        }

        public void btnAdd(GridControl grid)
        {
            grid.SelectedItem = null;
            message = MessageHelper.Get("+");
        }
        public async void btnUpdate(int index, int? id, string name, string email, string pass, string phone, bool admin)
        {
            if(id != null) // Update user
            {
                var value = new User(id, name, email, pass, phone, admin);
                if(await userRepo.Update($"user/{id}", value))
                {
                    users[index] = value;
                    message = MessageHelper.Get("up");
                }
                else
                {
                    message = MessageHelper.Get("upErr");
                }
            }
            else // Add user
            {
                var value = new User(null, name, email, pass, phone, admin);
                id = await userRepo.Add("user", value);
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
        public async void btnDelete(int index, int? id)
        {
            if(id != null)
            {
                if(MessageBox.Show("Xác nhận xóa?", "Xóa", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    bool del = await userRepo.Delete($"user/{id}");
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
