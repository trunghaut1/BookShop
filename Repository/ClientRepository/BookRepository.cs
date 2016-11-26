using Repository.Helper;
using Repository.Model;
using Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ClientRepository
{
    public class BookRepository : GenericRepository<Book>
    {
        public BookRepository()
        {
            url = "book";
        }

        public async Task<BookPaging> GetPage(int pageSize, int page)
        {
            string json = await APIHelper.Get($"{url}/page/{pageSize}/{page}");
            if(!string.IsNullOrEmpty(json))
            {
                return JsonHelper.Json2Object<BookPaging>(json);
            }
            return null;
        }
    }
}
