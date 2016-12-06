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
        private RecommendRepository recommendRepo;
        private ObservableCollection<Recommend> recommendList;
        public string message { get; set; }
        public bool IsPaging { get; set; } = true;
        public string txtSearchBook { get; set; }
        public int bookRecommend { get; set; }
        public Visibility IsUpdate { get; set; } = Visibility.Visible;
        public Visibility IsAdd { get; set; } = Visibility.Collapsed;
        public int bookAddRecommend { get; set; }
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
            recommendRepo = new RecommendRepository();
            books = new ObservableCollection<Book>();
            cats = new ObservableCollection<Cat>();
            subCats = new ObservableCollection<SubCat>();
            recommendList = new ObservableCollection<Recommend>();
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
                IsPaging = true;
            }
        }
        private async void SearchBook(int page)
        {
            if(!string.IsNullOrEmpty(txtSearchBook))
            {
                ListPaging<Book> bookPaging = await bookRepo.SearchPage(txtSearchBook, pageSize, page);
                //await GetCat_SubCat();
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
        }
        public async void btnSearchBook()
        {
            if (!string.IsNullOrEmpty(txtSearchBook))
            {
                int searchId;
                if (int.TryParse(txtSearchBook, out searchId))
                {
                    if (searchId > 0)
                    {
                        IsPaging = false;
                        books.Clear();
                        Book book = await bookRepo.GetByID(searchId);
                        if (book != null)
                            books.Add(book);
                    }
                }
                else
                {
                    IsPaging = true;
                    SearchBook(1);
                }
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
                if (!string.IsNullOrEmpty(txtSearchBook))
                {
                    SearchBook(page);
                }
                else
                {
                    LoadData(page);
                }
            }
        }
        public void ClearSelected(GridControl grid, ListBoxEdit catListBox, ListBoxEdit SubCatListBox)
        {
            IsAdd = Visibility.Visible;
            IsUpdate = Visibility.Collapsed;
            bookAddRecommend = 0;
            grid.SelectedItem = null;
            catListBox.SelectedItem = null;
            SubCatListBox.SelectedItem = null;
            message = MessageHelper.Get("+");
        }
        public void btnClearSearch()
        {
            LoadData(1);
            txtSearchBook = null;
        }
        private ObservableCollection<int> ToListInt(object list)
        {
            if(list != null)
            {
                if (list.GetType().Equals(typeof(ObservableCollection<int>)))
                {
                    return list as ObservableCollection<int>;
                }
                else
                {
                    var castList = list as IEnumerable<object>;
                    return castList != null ? new ObservableCollection<int>(castList.Cast<int>()) : null;
                }
            }
            return null;
        }
        public async void AddOrUpdate(int index, int? id, string name, string author, int price, int quantity, string summary, 
            MyImageEdit img, object catList, object subCatList)
        {
            var checkImg = img.Source as BitmapFrame;
            if (id != null) // Update
            {
                Book value = new Book(id, name, author, summary, null, price, quantity, ToListInt(catList), ToListInt(subCatList));
                if(checkImg == null)
                {
                    if(img.ImagePath != null & img.Source != null)
                    {
                        byte[] imageByte = Helper.Image2Byte(img.ImagePath);
                        value.Image = Convert.ToBase64String(imageByte);
                    }
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
                ObservableCollection<int> cat = ToListInt(catList); 
                ObservableCollection<int> subCat = ToListInt(subCatList);
                Book value = new Book(null, name, author, summary, null, price, quantity, cat, subCat);
                if(img.HasImage)
                {
                    byte[] imageByte = Helper.Image2Byte(img.ImagePath);
                    value.Image = Convert.ToBase64String(imageByte);
                }
                if(cat != null)
                    value.bookCat = new ObservableCollection<bookCat>(cat.Select(o => new bookCat(0, o)));
                if(subCat != null)
                    value.bookSubCat = new ObservableCollection<bookSubCat>(subCat.Select(o => new bookSubCat(0, o)));
                if (recommendList.Count() > 0)
                    value.Recommend = new ObservableCollection<Recommend>(recommendList.Select(o => 
                    new Recommend(o.FirstBookID, o.SecondBookID)));

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
                var window = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
                var result = WinUIMessageBox.Show(window,
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
        public async void CountRecommend(int? id, TableView grid)
        {
            bookRecommend = 0;
            if(grid.FocusedRowHandle >=0 && id != null)
            {
                IsUpdate = Visibility.Visible;
                IsAdd = Visibility.Collapsed;
                bookRecommend = await recommendRepo.CountRecommend((int)id);
            }
        }
        public void AddRecommend(TableView grid, int? id, Book book)
        {
            if(book == null)
            {
                book = new Book();
                if(recommendList != null)
                    book.Recommend = recommendList;
            }
            IWindowManager manager = new WindowManager();
            MainViewModel viewmodel = new MainViewModel(book);
            manager.ShowDialog(viewmodel, null, null);
            if (!viewmodel.IsActive)
            {
                if (id != null)
                    CountRecommend(id, grid);
                else
                {
                    var recommend = (viewmodel.ActiveItem as RecommendViewModel).book;
                    bookAddRecommend = recommend.Recommend.Count;
                    if (bookAddRecommend > 0)
                    {
                        recommendList = recommend.Recommend;
                    }
                }
            }
        }
    }
}
