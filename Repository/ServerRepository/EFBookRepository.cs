using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;
using System.Data.Entity;

namespace Repository.ServerRepository
{
    public class EFBookRepository : EFGenericRepository<Book>, IEFBookRepository
    {
        private IEFGenericRepository<bookCat> _bookCat;
        private IEFGenericRepository<bookSubCat> _bookSubCat;
        public EFBookRepository(BookEntities db, IEFGenericRepository<bookCat> bookCat, IEFGenericRepository<bookSubCat> bookSubCat) : base(db)
        {
            _bookCat = bookCat;
            _bookSubCat = bookSubCat;
        }

        public IEnumerable<Book> GetByCat(int id)
        {
            return GetBy(o => o.bookCat.Any(c => c.CatID == id));
        }

        public IEnumerable<Book> GetBySubCat(int id)
        {
            return GetBy(o => o.bookSubCat.Any(c => c.SubCatID == id));
        }

        public IEFGenericRepository<bookCat> bookCat() => _bookCat;

        public IEFGenericRepository<bookSubCat> bookSubCat() => _bookSubCat;
    }
}
