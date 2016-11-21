using Repository.Model;
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
        IEFGenericRepository<bookCat> bookCat();
        IEFGenericRepository<bookSubCat> bookSubCat();
    }
}
