using Caliburn.Micro;
using DevExpress.Xpf.Grid;
using PropertyChanged;
using Repository.ClientRepository;
using Repository.Helper;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            LoadData();
        }

        private async void LoadData()
        {
            var list = await catRepo.GetAll();
            if (list != null)
            {
                cats = new ObservableCollection<Cat>(list);
            }
            else
            {
                cats = new ObservableCollection<Cat>();
            }
        }

        public void btnAdd(GridControl grid)
        {
            grid.SelectedItem = null;
            message = MessageHelper.Get("+");
        }
        public async void btnUpdate(int index, int? id, string name)
        {
            if (id != null) // Update
            {
                var value = new Cat(id, name);
                if (await catRepo.Update((int)id, value))
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
                var value = new Cat(null, name);
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
        public async void btnDelete(int index, int? id)
        {
            if (id != null)
            {
                if (MessageBox.Show("Xác nhận xóa?", "Xóa", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
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
