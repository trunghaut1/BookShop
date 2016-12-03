using Repository.Model;
using Repository.ViewModel;
using System.Collections.Generic;

namespace Repository.ServerRepository
{
    public interface IEFBookRepository : IEFGenericRepository<Book>
    {
        IEnumerable<Book> GetByName(string name);
        ListPaging<Book> GetPage(int pageSize, int page);
    }
}
