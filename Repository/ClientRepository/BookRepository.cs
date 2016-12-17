using Repository.Helper;
using Repository.Model;
using Repository.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.ClientRepository
{
    public class BookRepository : GenericRepository<Book>
    {
        public BookRepository()
        {
            url = "book";
        }

        public async Task<ListPaging<Book>> GetPage(int pageSize, int page)
        {
            string json = await APIHelper.Get($"{url}/page/{pageSize}/{page}");
            if(!string.IsNullOrEmpty(json))
            {
                return JsonHelper.Json2Object<ListPaging<Book>>(json);
            }
            return null;
        }
        public async Task<ListPaging<Book>> SearchPage(string name, int pageSize, int page)
        {
            string json = await APIHelper.Get($"{url}/name/{name}/page/{pageSize}/{page}");
            if (!string.IsNullOrEmpty(json))
            {
                return JsonHelper.Json2Object<ListPaging<Book>>(json);
            }
            return null;
        }
        public async Task<IEnumerable<Book>> tGetByNumber(int number)
        {
            string json = await APIHelper.Get($"{url}/time/getbynumber/{number}");
            if (!string.IsNullOrEmpty(json))
            {
                return JsonHelper.Json2List<Book>(json);
            }
            return null;
        }
        public async Task<Book> tGetByID(int id)
        {
            string json = await APIHelper.Get($"{url}/time/getbyid/{id}");
            if (!string.IsNullOrEmpty(json))
            {
                return JsonHelper.Json2Object<Book>(json);
            }
            return null;
        }
    }
}
