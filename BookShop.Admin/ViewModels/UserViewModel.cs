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

namespace BookShop.Admin.ViewModels
{
    [ImplementPropertyChanged]
    public class UserViewModel : Screen
    {
        public string message { get; set; }
        public ObservableCollection<User> users { get; set; }

        public UserViewModel()
        {
            LoadData();
        }

        private async Task<ObservableCollection<User>> GetList()
        {
            string json = await APIHelper.Get("user");
            if(!String.IsNullOrEmpty(json))
            {
                return new ObservableCollection<User>(JsonHelper.Json2List<User>(json));
            }
            return null;
        }

        private async void LoadData()
        {
            users = await GetList();
        }

        public void btnAdd(GridControl grid)
        {
            grid.SelectedItem = null;
            message = MessageHelper.Get("+");
        }
        public async void btnUpdate(int index, int? id, string name, string email, string pass, string phone, bool admin)
        {
            if(id != null)
            {
                var value = new User()
                {
                    ID = (int)id,
                    Name = name,
                    Email = email,
                    Pass = pass,
                    Phone = phone,
                    IsAdmin = admin
                };
                string json = await APIHelper.Put($"user/{id}", value);
                if(!String.IsNullOrEmpty(json))
                {
                    users[index] = value;
                    message = MessageHelper.Get("up");
                }
                else
                {
                    message = MessageHelper.Get("upErr");
                }
            }
            else
            {

            }
        }
    }
}
