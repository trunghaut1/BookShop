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
    public class SubCatViewModel : Screen
    {
        private SubCatRepository subCatRepo;
        private CatRepository catRepo;
        public string message { get; set; }
        public ObservableCollection<Cat> cats { get; set; }
        public ObservableCollection<SubCat> subcats { get; set; }

        public SubCatViewModel()
        {
            catRepo = new CatRepository();
            subCatRepo = new SubCatRepository();
            LoadData();
            
        }
        private async void LoadData()
        {
            var cat = await catRepo.GetAll();
            var list = await subCatRepo.GetAll();
            if (list != null && cat != null)
            {
                cats = new ObservableCollection<Cat>(cat);
                subcats = new ObservableCollection<SubCat>(list);
                foreach(var value in subcats)
                {
                    value.Cat = cats.Where(o => o.ID == value.CatID).FirstOrDefault();
                }
            }
            else
            {
                cats = new ObservableCollection<Cat>();
                subcats = new ObservableCollection<SubCat>();
            }
        }
        public void btnAdd(GridControl grid)
        {
            grid.SelectedItem = null;
            message = MessageHelper.Get("+");
        }
        public async void btnUpdate(int index, int? id, string name, int catId)
        {
            if (id != null) // Update
            {
                var value = new SubCat(id, name, catId);
                if (await subCatRepo.Update((int)id, value))
                {
                    value.Cat = cats.Where(o => o.ID == catId).FirstOrDefault();
                    subcats[index] = value;
                    message = MessageHelper.Get("up");
                }
                else
                {
                    message = MessageHelper.Get("upErr");
                }
            }
            else // Add
            {
                var value = new SubCat(null, name, catId);
                id = await subCatRepo.Add(value);
                if (id != 0)
                {
                    value.ID = (int)id;
                    value.Cat = cats.Where(o => o.ID == catId).FirstOrDefault();
                    subcats.Add(value);
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
                    bool del = await subCatRepo.Delete((int)id);
                    if (del)
                    {
                        subcats.RemoveAt(index);
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
