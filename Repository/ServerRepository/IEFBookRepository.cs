using Repository.Model;
using Repository.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServerRepository
{
    public interface IEFBookRepository : IEFGenericRepository<Book>
    {
        IEnumerable<Book> GetByCat(int id);
        IEnumerable<Book> GetBySubCat(int id);
        BookPaging GetPage(int pageSize, int page);
    }
}
