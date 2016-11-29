using BookShop.Admin.Converter;
using Caliburn.Micro;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid;
using PropertyChanged;
using Repository.ClientRepository;
using Repository.Helper;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
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
        public ObservableCollection<SubCat> subcats { get; set; }
        public ObservableCollection<bookCat> bookcats { get; set; }
        public ObservableCollection<bookSubCat> booksubcats { get; set; }
        public Paging paging { get; set; }
        public ObservableCollection<int> pageList { get; set; }

        public BookViewModel()
        {
            bookRepo = new BookRepository();
            catRepo = new CatRepository();
            subCatRepo = new SubCatRepository();
            books = new ObservableCollection<Book>();
            cats = new ObservableCollection<Cat>();
            subcats = new ObservableCollection<SubCat>();
            bookcats = new ObservableCollection<bookCat>();
            booksubcats = new ObservableCollection<bookSubCat>();
            paging = new Paging();
            pageList = new ObservableCollection<int>();
            LoadData(true, 1);
        }

        private async void LoadData(bool firstLoad, int page)
        {
            var book = await bookRepo.GetPage(pageSize, page);
            if(firstLoad)
            {
                var cat = await catRepo.GetAll();
                var subcat = await subCatRepo.GetAll();
                var bookcat = await bookRepo.GetbookCat();
                var booksubcat = await bookRepo.GetbookSubCat();
                if (cat != null)
                {
                    cats = new ObservableCollection<Cat>(cat);
                }
                if(subcat != null)
                {
                    subcats = new ObservableCollection<SubCat>(subcat);
                }
                if (bookcat != null)
                {
                    bookcats = new ObservableCollection<bookCat>(bookcat);
                }
                if (booksubcat != null)
                {
                    booksubcats = new ObservableCollection<bookSubCat>(booksubcat);
                }
            }
            if (book?.book != null)
            {
                books = new ObservableCollection<Book>(book.book);
                foreach(var value in books)
                {
                    var bookCatList = bookcats.Where(o => o.BookID == value.ID);
                    var bookSubCatList = booksubcats.Where(o => o.BookID == value.ID);
                    value.bookCat = new ObservableCollection<bookCat>(bookCatList);
                    value.bookSubCat = new ObservableCollection<bookSubCat>(bookSubCatList);
                }
                paging = book.paging;
                var _pageList = new ObservableCollection<int>();
                for (int i = 1; i <= paging.totalPage; i++)
                {
                    _pageList.Add(i);
                }
                pageList = _pageList;
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
                var catID = bookcats.Where(o => o.BookID == book.ID).Select(c => c.CatID).ToList();
                var subCatID = booksubcats.Where(o => o.BookID == book.ID).Select(c => c.SubCatID).ToList();
                var catList = cats.Where(o => catID.Contains(o.ID)).ToList();
                var subCatList = subcats.Where(o => subCatID.Contains(o.ID)).ToList();
                catNum = catList.Count;
                subCatNum = subCatList.Count;
                foreach(var value in catList)
                {
                    catListBox.SelectedItems.Add(value);
                }
                foreach (var value in subCatList)
                {
                    subCatListBox.SelectedItems.Add(value);
                }
            }
        }
        public void btnAdd(GridControl grid, ListBoxEdit catListBox, ListBoxEdit SubCatListBox)
        {
            grid.SelectedItem = null;
            catListBox.SelectedItem = null;
            SubCatListBox.SelectedItem = null;
            catNum = 0;
            subCatNum = 0;
            message = MessageHelper.Get("+");
        }
        public async void btnUpdate(int index, int? id, string name, string author, int price, int quantity, string summary, 
            MyImageEdit img, ObservableCollection<object> listCat, ObservableCollection<object> listSubCat)
        {
            var catId = listCat.Cast<Cat>().Select(o => o.ID).ToList();
            var subCatId = listSubCat.Cast<SubCat>().Select(o => o.ID).ToList();
            var t = img.Source as BitmapFrame;
            var m = t?.Decoder;
            if (id != null) // Update
            {

            }
            else // Add
            {
                var value = new Book(null, name, author, summary, null, price, quantity);
                if(img.HasImage)
                {
                    var imageByte = Helper.Image2Byte(img.ImagePath);
                    value.Image = Convert.ToBase64String(imageByte);
                    var bookCat = bookcats.Where(o => catId.Contains(o.CatID));
                }
            }
        }
    }
}
