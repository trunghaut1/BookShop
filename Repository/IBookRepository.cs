using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        IEnumerable<Book> SelectByCat(int id);
        IEnumerable<Book> SelectBySubCat(int id);
        IGenericRepository<bookCat> bookCat();
        IGenericRepository<bookSubCat> bookSubCat();
    }
}
