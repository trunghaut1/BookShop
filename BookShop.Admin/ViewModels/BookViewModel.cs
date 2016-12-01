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
        public int catNum { get; set; }
        public int subCatNum { get; set; }
        public string message { get; set; }
        public ObservableCollection<Book> books { get; set; }
        public ObservableCollection<Cat> cats { get; set; }
        public ObservableCollection<SubCat> subCats { get; set; }
        public ObservableCollection<bookCat> bookCats { get; set; }
        public ObservableCollection<bookSubCat> bookSubCats { get; set; }
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
            bookCats = new ObservableCollection<bookCat>();
            bookSubCats = new ObservableCollection<bookSubCat>();
            paging = new Paging();
            pageList = new ObservableCollection<int>();
            LoadData(true, 1);
        }

        private async void LoadData(bool firstLoad, int page)
        {
            BookPaging bookPaging = await bookRepo.GetPage(pageSize, page);
            if(firstLoad)
            {
                IEnumerable<Cat> cat = await catRepo.GetAll();
                IEnumerable<SubCat> subCat = await subCatRepo.GetAll();
                await GetBookRef();
                if (cat != null)
                {
                    cats = new ObservableCollection<Cat>(cat);
                }
                if(subCat != null)
                {
                    subCats = new ObservableCollection<SubCat>(subCat);
                }
            }
            if (bookPaging?.book != null)
            {
                ObservableCollection<Book> _books = new ObservableCollection<Book>(bookPaging.book);
                foreach(Book value in _books)
                {
                    var bookCatList = bookCats.Where(o => o.BookID == value.ID).Select(o => o.CatID).ToList();
                    var bookSubCatList = bookSubCats.Where(o => o.BookID == value.ID).Select(o => o.SubCatID).ToList();
                    var catList = cats.Where(o => bookCatList.Contains(o.ID));
                    var subCatList = subCats.Where(o => bookSubCatList.Contains(o.ID));
                    value.catList = new ObservableCollection<Cat>(catList);
                    value.subCatList = new ObservableCollection<SubCat>(subCatList);
                }
                books = _books;
                paging = bookPaging.paging;
                ObservableCollection<int> _pageList = new ObservableCollection<int>();
                for (int i = 1; i <= paging.totalPage; i++)
                {
                    _pageList.Add(i);
                }
                pageList = _pageList;
            }
        }
        private async Task GetBookRef()
        {
            IEnumerable<bookCat> bookCat = await bookRepo.GetbookCat();
            IEnumerable<bookSubCat> bookSubCat = await bookRepo.GetbookSubCat();
            if (bookCat != null)
            {
                bookCats = new ObservableCollection<bookCat>(bookCat);
            }
            if (bookSubCat != null)
            {
                bookSubCats = new ObservableCollection<bookSubCat>(bookSubCat);
            }
        }
        public void ChangePage(int page)
        {      
            if (page > 0)
            {
                LoadData(false, page);
            }         
        }
        public void LoadCat_SubCat(Book book, ListBoxEdit catListBox, ListBoxEdit subCatListBox)
        {

            if (book != null)
            {
                catListBox.SelectedItem = null;
                subCatListBox.SelectedItem = null;
                catNum = book.catList.Count;
                subCatNum = book.subCatList.Count;
                foreach(Cat value in book.catList)
                {
                    catListBox.SelectedItems.Add(value);
                }
                foreach (SubCat value in book.subCatList)
                {
                    subCatListBox.SelectedItems.Add(value);
                }
            }
        }
        public void ClearSelected(GridControl grid, ListBoxEdit catListBox, ListBoxEdit SubCatListBox)
        {
            grid.SelectedItem = null;
            catListBox.SelectedItem = null;
            SubCatListBox.SelectedItem = null;
            catNum = 0;
            subCatNum = 0;
            message = MessageHelper.Get("+");
        }
        public async void AddOrUpdate(int index, int? id, string name, string author, int price, int quantity, string summary, 
            MyImageEdit img, ObservableCollection<object> listCat, ObservableCollection<object> listSubCat)
        {
            var catId = listCat.Cast<Cat>().Select(o => o.ID).ToList();
            var subCatId = listSubCat.Cast<SubCat>().Select(o => o.ID).ToList();
            var checkImg = img.Source as BitmapFrame;
            if (id != null) // Update
            {
                Book value = new Book(id, name, author, summary, null, price, quantity);
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
                ObservableCollection<bookCat> bookCat = new ObservableCollection<bookCat>();
                ObservableCollection<bookSubCat> bookSubCat = new ObservableCollection<bookSubCat>();
                foreach (int i in catId)
                {
                    bookCat.Add(new bookCat((int)id, i));
                }
                foreach (int i in subCatId)
                {
                    bookSubCat.Add(new bookSubCat((int)id, i));
                }
                value.bookCat = bookCat;
                value.bookSubCat = bookSubCat;

                bool result = await bookRepo.Update((int)id, value);
                if(result)
                {
                    value.catList = new ObservableCollection<Cat>(listCat.Cast<Cat>());
                    value.subCatList = new ObservableCollection<SubCat>(listSubCat.Cast<SubCat>());
                    await GetBookRef();
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
                Book value = new Book(null, name, author, summary, null, price, quantity);
                if(img.HasImage)
                {
                    byte[] imageByte = Helper.Image2Byte(img.ImagePath);
                    value.Image = Convert.ToBase64String(imageByte);
                }
                ObservableCollection<bookCat> bookCat = new ObservableCollection<bookCat>();
                ObservableCollection<bookSubCat> bookSubCat = new ObservableCollection<bookSubCat>();
                foreach(int i in catId)
                {
                    bookCat.Add(new bookCat(0, i));
                }
                foreach (int i in subCatId)
                {
                    bookSubCat.Add(new bookSubCat(0, i));
                }
                value.bookCat = bookCat;
                value.bookSubCat = bookSubCat;

                id = await bookRepo.Add(value);
                if (id != 0)
                {
                    value.ID = (int)id;
                    value.catList = new ObservableCollection<Cat>(listCat.Cast<Cat>());
                    value.subCatList = new ObservableCollection<SubCat>(listSubCat.Cast<SubCat>());
                    books.Add(value);
                    foreach(int i in catId)
                    {
                        bookCats.Add(new bookCat(value.ID, i));
                    }
                    foreach (int i in subCatId)
                    {
                        bookSubCats.Add(new bookSubCat(value.ID, i));
                    }
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
                        bookCats.Remove(o => o.BookID == id);
                        bookSubCats.Remove(o => o.BookID == id);
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
