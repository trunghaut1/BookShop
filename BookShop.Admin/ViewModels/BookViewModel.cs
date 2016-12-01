using BookShop.Admin.Converter;
using Caliburn.Micro;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.WindowsUI;
using PropertyChanged;
using Repository.ClientRepository;
using Repository.Helper;
using Repository.Model;
using Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace BookShop.Admin.ViewModels
{
    [ImplementPropertyChanged]
    public class BookViewModel : Screen
    {
        private int pageSize = 10;

        private BookRepository bookRepo;
        private CatRepository catRepo;
        private SubCatRepository subCatRepo;
        public string message { get; set; }
        public ObservableCollection<Book> books { get; set; }
        public ObservableCollection<Cat> cats { get; set; }
        public ObservableCollection<SubCat> subCats { get; set; }
        public Paging paging { get; set; }
        public ObservableCollection<int> pageList { get; set; }

        public BookViewModel()
        {
            bookRepo = new BookRepository();
            catRepo = new CatRepository();
            subCatRepo = new SubCatRepository();
            books = new ObservableCollection<Book>();
            cats = new ObservableCollection<Cat>();
            subCats = new ObservableCollection<SubCat>();
            paging = new Paging();
            pageList = new ObservableCollection<int>();
            LoadData(1);
        }

        private async void LoadData(int page)
        {
            ListPaging<Book> bookPaging = await bookRepo.GetPage(pageSize, page);
            await GetCat_SubCat();
            if (bookPaging?.list != null)
            {
                books = new ObservableCollection<Book>(bookPaging.list);
                paging = bookPaging.paging;
                ObservableCollection<int> _pageList = new ObservableCollection<int>();
                for (int i = 1; i <= paging.totalPage; i++)
                {
                    _pageList.Add(i);
                }
                pageList = _pageList;
            }
        }
        private async Task GetCat_SubCat()
        {
            IEnumerable<Cat> cat = await catRepo.GetAll();
            IEnumerable<SubCat> subCat = await subCatRepo.GetAll();
            if (cat != null)
            {
                cats = new ObservableCollection<Cat>(cat);
            }
            if (subCat != null)
            {
                subCats = new ObservableCollection<SubCat>(subCat);
            }
        }
        public void ChangePage(int page)
        {      
            if (page > 0)
            {
                LoadData(page);
            }         
        }
        public void ClearSelected(GridControl grid, ListBoxEdit catListBox, ListBoxEdit SubCatListBox)
        {
            grid.SelectedItem = null;
            catListBox.SelectedItem = null;
            SubCatListBox.SelectedItem = null;
            message = MessageHelper.Get("+");
        }
        public async void AddOrUpdate(int index, int? id, string name, string author, int price, int quantity, string summary, 
            MyImageEdit img, ObservableCollection<int> catList, ObservableCollection<int> subCatList)
        {
            var checkImg = img.Source as BitmapFrame;
            if (id != null) // Update
            {
                Book value = new Book(id, name, author, summary, null, price, quantity, catList, subCatList);
                if(checkImg == null)
                {
                    byte[] imageByte = Helper.Image2Byte(img.ImagePath);
                    value.Image = Convert.ToBase64String(imageByte);
                }
                else
                {
                    string base64 = books.Where(o => o.ID == id).Select(o => o.Image).FirstOrDefault();
                    value.Image = base64;
                }

                bool result = await bookRepo.Update((int)id, value);
                if(result)
                {
                    books[index] = value;
                    message = MessageHelper.Get("up");
                }
                else
                {
                    message = MessageHelper.Get("upErr");
                }
            }
            else // Add
            {
                Book value = new Book(null, name, author, summary, null, price, quantity, null, null);
                if(img.HasImage)
                {
                    byte[] imageByte = Helper.Image2Byte(img.ImagePath);
                    value.Image = Convert.ToBase64String(imageByte);
                }

                id = await bookRepo.Add(value);
                if (id != 0)
                {
                    value.ID = (int)id;
                    books.Add(value);
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
                var result = WinUIMessageBox.Show(Application.Current.MainWindow,
                "Bạn có muốn xóa giá trị này?", "Xác nhận",
                MessageBoxButton.YesNo, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None, FloatingMode.Window);
                if (result == MessageBoxResult.Yes)
                {
                    bool del = await bookRepo.Delete((int)id);
                    if (del)
                    {
                        books.RemoveAt(index);
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
        public async void AddCat()
        {
            IWindowManager manager = new WindowManager();
            MainViewModel viewmodel = new MainViewModel(typeof(CatViewModel), "QUẢN LÝ THỂ LOẠI CHÍNH");
            manager.ShowDialog(viewmodel, null, null);
            if (!viewmodel.IsActive)
            {
                await GetCat_SubCat();
            }
        }
        public async void AddSubCat()
        {
            IWindowManager manager = new WindowManager();
            MainViewModel viewmodel = new MainViewModel(typeof(SubCatViewModel), "QUẢN LÝ THỂ LOẠI CON");
            manager.ShowDialog(viewmodel, null, null);
            if (!viewmodel.IsActive)
            {
                await GetCat_SubCat();
            }
        }
    }
}
