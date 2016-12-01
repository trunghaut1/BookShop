using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using System.Data.Entity;
using Repository.ViewModel;
using System.Data.Entity.Migrations;

namespace Repository.ServerRepository
{
    public class EFBookRepository : EFGenericRepository<Book>, IEFBookRepository
    {
        protected DbSet<bookCat> bookCat = null;
        protected DbSet<bookSubCat> bookSubCat = null;
        public EFBookRepository(BookEntities db) : base(db)
        {
            bookCat = this.db.Set<bookCat>();
            bookSubCat = this.db.Set<bookSubCat>();
        }

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
            return db.bookCat.AsEnumerable();
        }
        public IEnumerable<bookSubCat> GetbookSubCat()
        {
            return db.bookSubCat.AsEnumerable();
        }
        public override bool Update(Book obj)
        {
            try
            {
                Book book = table.Find(obj.ID);
                table.AddOrUpdate(obj);
                var catDelete = book.bookCat.Where(o => !obj.bookCat.Any(b => b.CatID == o.CatID)).ToList();
                var subCatDelete = book.bookSubCat.Where(o => !obj.bookSubCat.Any(b => b.SubCatID == o.SubCatID)).ToList();
                var catAdd = obj.bookCat.Where(o => book.bookCat.Any(b => b.CatID == o.CatID));
                var subCatAdd = obj.bookSubCat.Where(o => obj.bookSubCat.Any(b => b.SubCatID == o.SubCatID));
                foreach(bookCat value in catDelete)
                {
                    book.bookCat.Remove(value);
                }
                foreach (bookSubCat value in subCatDelete)
                {
                    book.bookSubCat.Remove(value);
                }
                foreach (bookCat value in catAdd)
                {
                    book.bookCat.Add(value);
                }
                foreach (bookSubCat value in subCatAdd)
                {
                    book.bookSubCat.Add(value);
                }
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
