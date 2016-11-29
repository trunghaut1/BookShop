using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;
using System.Data.Entity;
using Repository.ViewModel;

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
        public IEFGenericRepository<bookCat> bookCat => _bookCat;

        public IEFGenericRepository<bookSubCat> bookSubCat => _bookSubCat;

        public IEnumerable<Book> GetByCat(int id)
        {
            return GetBy(o => o.bookCat.Any(c => c.CatID == id));
        }

        public IEnumerable<Book> GetBySubCat(int id)
        {
            return GetBy(o => o.bookSubCat.Any(c => c.SubCatID == id));
        }
        public BookPaging GetPage(int pageSize, int page)
        {
            var paging = new Paging()
            {
                totalItem = table.Count(),
                pageSize = pageSize,
                pageIndex = page
            };
            var book = table.OrderBy(o => o.ID).Skip(pageSize * (page - 1)).Take(pageSize)
                .Select(o => new { o.ID, o.Name, o.Author, o.Summary, o.Image, o.Price, o.Quantity}).AsEnumerable()
                .Select(o => new Book(o.ID, o.Name, o.Author, o.Summary, o.Image, o.Price, o.Quantity))
                .AsEnumerable();
            var bookPaging = new BookPaging()
            {
                book = book,
                paging = paging
            };
            return bookPaging;
        }
        public IEnumerable<bookCat> GetbookCat()
        {
            return _bookCat.GetAll();
        }
        public IEnumerable<bookSubCat> GetbookSubCat()
        {
            return _bookSubCat.GetAll();
        }
    }
}
