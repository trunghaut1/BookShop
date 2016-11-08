using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;
using System.Data.Entity;

namespace Repository
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private IGenericRepository<bookCat> _bookCat;
        private IGenericRepository<bookSubCat> _bookSubCat;
        public BookRepository(BookEntities db, IGenericRepository<bookCat> bookCat, IGenericRepository<bookSubCat> bookSubCat) : base(db)
        {
            _bookCat = bookCat;
            _bookSubCat = bookSubCat;
        }

        public IEnumerable<Book> SelectByCat(int id)
        {
            return SelectBy(o => o.bookCat.Any(c => c.CatID == id));
        }

        public IEnumerable<Book> SelectBySubCat(int id)
        {
            return SelectBy(o => o.bookSubCat.Any(c => c.SubCatID == id));
        }

        public IGenericRepository<bookCat> bookCat() => _bookCat;

        public IGenericRepository<bookSubCat> bookSubCat() => _bookSubCat;
    }
}
