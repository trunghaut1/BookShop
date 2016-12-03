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
        public IEnumerable<Book> GetByName(string name)
        {
            return GetBy(o => o.Name.Contains(name)).AsEnumerable();
        }
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
                var catDelete = book.bookCat.Where(o => !obj.catList.Contains(o.CatID)).ToList();
                var subCatDelete = book.bookSubCat.Where(o => !obj.subCatList.Contains(o.SubCatID)).ToList();
                var catAdd = obj.catList.Where(o => !book.bookCat.Any(c => c.CatID == o)).ToList();
                var subCatAdd = obj.subCatList.Where(o => !book.bookSubCat.Any(c => c.SubCatID == o)).ToList();
                foreach(bookCat value in catDelete)
                {
                    book.bookCat.Remove(value);
                }
                foreach (bookSubCat value in subCatDelete)
                {
                    book.bookSubCat.Remove(value);
                }
                foreach (int value in catAdd)
                {
                    book.bookCat.Add(new bookCat(obj.ID, value));
                }
                foreach (int value in subCatAdd)
                {
                    book.bookSubCat.Add(new bookSubCat(obj.ID, value));
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
