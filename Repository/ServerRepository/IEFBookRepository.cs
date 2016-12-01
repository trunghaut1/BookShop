using Repository.Model;
using Repository.ViewModel;
using System.Collections.Generic;

namespace Repository.ServerRepository
{
    public interface IEFBookRepository : IEFGenericRepository<Book>
    {
        ListPaging<Book> GetPage(int pageSize, int page);
    }
}
