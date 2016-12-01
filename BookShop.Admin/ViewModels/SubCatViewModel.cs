using BookShop.Admin.Converter;
using Caliburn.Micro;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.WindowsUI;
using PropertyChanged;
using Repository.ClientRepository;
using Repository.Helper;
using Repository.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace BookShop.Admin.ViewModels
{
    [ImplementPropertyChanged]
    public class SubCatViewModel : Screen
    {
        private SubCatRepository subCatRepo;
        private CatRepository catRepo;
        public string message { get; set; }
        public ObservableCollection<Cat> cats { get; set; }
        public ObservableCollection<SubCat> subCats { get; set; }

        public SubCatViewModel()
        {
            catRepo = new CatRepository();
            subCatRepo = new SubCatRepository();

            cats = new ObservableCollection<Cat>();
            subCats = new ObservableCollection<SubCat>();
            LoadData();  
        }
        private async void LoadData()
        {
            IEnumerable<Cat> cat = await catRepo.GetAll();
            IEnumerable<SubCat> subCat = await subCatRepo.GetAll();
            if (subCat != null)
            {
                subCats = new ObservableCollection<SubCat>(subCat);
            }
            if (cat != null)
            {
                cats = new ObservableCollection<Cat>(cat);
            }
        }
        public void ClearSelected(GridControl grid)
        {
            grid.SelectedItem = null;
            message = MessageHelper.Get("+");
        }
        public async void AddOrUpdate(int index, int? id, string name, int catId)
        {
            if (id != null) // Update
            {
                SubCat value = new SubCat(id, name, catId);
                bool result = await subCatRepo.Update((int)id, value);
                if (result)
                {
                    value.Cat = cats.Where(o => o.ID == catId).FirstOrDefault();
                    subCats[index] = value;
                    message = MessageHelper.Get("up");
                }
                else
                {
                    message = MessageHelper.Get("upErr");
                }
            }
            else // Add
            {
                SubCat value = new SubCat(null, name, catId);
                id = await subCatRepo.Add(value);
                if (id != 0)
                {
                    value.ID = (int)id;
                    value.Cat = cats.Where(o => o.ID == catId).FirstOrDefault();
                    subCats.Add(value);
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
                    bool del = await subCatRepo.Delete((int)id);
                    if (del)
                    {
                        subCats.RemoveAt(index);
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
        public async void AddCat(ComboBox cbo)
        {
            IWindowManager manager = new WindowManager();
            MainViewModel viewmodel = new MainViewModel(typeof(CatViewModel), "QUẢN LÝ THỂ LOẠI CHÍNH");
            manager.ShowDialog(viewmodel, null, null);
            if (!viewmodel.IsActive)
            {
                IEnumerable<Cat> cat = await catRepo.GetAll();
                if (cat != null)
                {
                    cats = new ObservableCollection<Cat>(cat);
                }
                foreach (SubCat value in subCats)
                {
                    value.Cat = cats.Where(o => o.ID == value.CatID).FirstOrDefault();
                }
                Binding binding = Helper.SetBinding("subCats", "SelectedItem.CatID");
                BindingOperations.SetBinding(cbo, ComboBox.SelectedValueProperty, binding);
            }
        }
    }
}
