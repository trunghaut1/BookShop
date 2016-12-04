using Caliburn.Micro;
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

namespace BookShop.Admin.ViewModels
{
    [ImplementPropertyChanged]
    public class RecommendViewModel : Screen
    {
        private RecommendRepository repo;
        private BookRepository bookRepo;

        public string message { get; set; }
        public string txtSearchBook { get; set; }
        public Book book { get; set; }
        public ObservableCollection<Book> bookSearch { get; set; }

        public RecommendViewModel(Book value)
        {
            repo = new RecommendRepository();
            bookRepo = new BookRepository();
            bookSearch = new ObservableCollection<Book>();
            if (value.ID > 0)
            {
                book = value;
                LoadRecommend(value.ID);
            }
            else
                book = value;
        }
        private async void LoadRecommend(int id)
        {
            IEnumerable<Recommend> value = await repo.GetListByID(id);
            if(value != null)
            {
                book.Recommend = new ObservableCollection<Recommend>(value);
            }
        }
        public async void SearchBook()
        {
            if (!string.IsNullOrEmpty(txtSearchBook))
            {
                bookSearch.Clear();
                int searchId;
                if (int.TryParse(txtSearchBook, out searchId))
                {
                    if (searchId > 0)
                    {
                        Book book = await bookRepo.GetByID(searchId);
                        if (book != null)
                            bookSearch.Add(book);
                    }
                }
                else
                {
                    IEnumerable<Book> book = await bookRepo.GetByName(txtSearchBook);
                    if (book != null)
                        bookSearch = new ObservableCollection<Book>(book);
                }
            }
        }
        public void btnClearSearch()
        {
            txtSearchBook = null;
            bookSearch.Clear();
        }
        public void AddBook(Book value)
        {
            if(value == null)
            {
                message = "Vui lòng chọn sách cần thêm!";
                return;
            }
            Recommend exitense = book.Recommend.Where(o => o.SecondBookID == value.ID).FirstOrDefault();
            if(exitense != null)
            {
                message = "Đã tồn tại gợi ý này!";
                return;
            }
            else
            {
                book.Recommend.Add(new Recommend(book.ID, value.ID, value));
            }
        }
        public void DelBook(int? id)
        {
            if(id != null)
            {
                Recommend recommend = book.Recommend.Where(o => o.SecondBookID == id).FirstOrDefault();
                if(recommend != null)
                {
                    book.Recommend.Remove(recommend);
                    message = "Đã xóa khỏi gợi ý!";
                }
            }
            else
            {
                message = "Vui lòng chọn gợi ý cần xóa!";
            }
        }
        public void btnClose()
        {
            (this.Parent as MainViewModel).TryClose();
        }
        public async void btnUpdate()
        {
            if(book.ID > 0)
            {
                var recommend = book.Recommend.Select(o => new Recommend(o.FirstBookID, o.SecondBookID)).AsEnumerable();
                bool result = await repo.UpdateList(book.ID, recommend);
                if(result)
                {
                    message = MessageHelper.Get("up");
                }
                else
                {
                    message = MessageHelper.Get("upErr");
                }
            }
            else
            {
                message = "Thoát và ấn Lưu sách để hoàn tất!";
            }
        }
    }
}
