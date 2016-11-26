using Caliburn.Micro;
using PropertyChanged;
using Repository.ClientRepository;
using Repository.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Admin.ViewModels
{
    [ImplementPropertyChanged]
    public class BookViewModel : Screen
    {
        private BookRepository bookRepo;
        public string message { get; set; }
        public ObservableCollection<Book> books { get; set; }

        public BookViewModel()
        {
            bookRepo = new BookRepository();
            LoadData();
        }

        private async void LoadData()
        {
            int pageSize = 10;
            var book = await bookRepo.GetPage(pageSize, 1);
            if (book?.book != null)
            {
                books = new ObservableCollection<Book>(book.book);
            }
            else
            {
                books = new ObservableCollection<Book>();
            }
        }
    }
}
