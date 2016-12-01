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
        public EFBookRepository(BookEntities db) : base(db) { }

        public ListPaging<Book> GetPage(int pageSize, int page)
        {
            Paging paging = new Paging()
            {
                totalItem = table.Count(),
                pageSize = pageSize,
                pageIndex = page
            };
            var book = table.OrderBy(o => o.ID).Skip(pageSize * (page - 1)).Take(pageSize).ToList()
                .Select(o => new Book(o));
            ListPaging<Book> bookPaging = new ListPaging<Book>()
            {
                list = book,
                paging = paging
            };
            return bookPaging;
        }
        public override bool Update(Book obj)
        {
            try
            {
                Book book = table.Find(obj.ID);
                table.AddOrUpdate(obj);
                var catDelete = book.bookCat.Where(o => !obj.bookCat.Any(b => b.CatID == o.CatID)).ToList();
                var subCatDelete = book.bookSubCat.Where(o => !obj.bookSubCat.Any(b => b.SubCatID == o.SubCatID)).ToList();
                var catAdd = obj.bookCat.Where(o => !book.bookCat.Any(b => b.CatID == o.CatID)).ToList();
                var subCatAdd = obj.bookSubCat.Where(o => !book.bookSubCat.Any(b => b.SubCatID == o.SubCatID)).ToList();
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
