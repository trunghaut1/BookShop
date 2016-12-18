using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using System.Data.Entity;
using Repository.ViewModel;
using System.Data.Entity.Migrations;
using System;

namespace Repository.ServerRepository
{
    public class EFBookRepository : EFGenericRepository<Book>, IBookRepository
    {
        public EFBookRepository(BookEntities db) : base(db) { }
        public IEnumerable<Book> GetByName(string name)
        {
            return GetBy(o => o.Name.Contains(name)).AsEnumerable();
        }
        public ListPaging<Book> SearchPage(string name,int pageSize, int page)
        {
            Paging paging = new Paging()
            {
                totalItem = 0,
                pageSize = pageSize,
                pageIndex = page
            };
            var book = GetByName(name).OrderBy(o => o.ID).Skip(pageSize * (page - 1)).Take(pageSize).ToList()
                .Select(o => new Book(o));
            paging.totalItem = book.Count();
            ListPaging<Book> bookPaging = new ListPaging<Book>()
            {
                list = book,
                paging = paging
            };
            return bookPaging;
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
        private List<int> GetTimeBased()
        {
            DateTime now = DateTime.Now;
            string week = ((int)now.DayOfWeek).ToString();
            var hide = db.TimeRule.Where(o => o.Status == false && now.Date >= o.FromDate && now.Date <= o.ToDate)
            .Where(o => now.TimeOfDay >= o.FromHour && now.TimeOfDay <= o.ToHour)
            .Where(o => o.Week.Contains(week)).Select(o => o.ID).ToList();
            var itemHide = db.TimeBased.Where(o => hide.Contains(o.RuleID)).Select(o => o.BookID).ToList();
            var hide2 = db.TimeRule.Where(o => o.Status == true)
                .Where(o => !(now.Date >= o.FromDate && now.Date <= o.ToDate))
                .Where(o => !(now.TimeOfDay >= o.FromHour && now.TimeOfDay <= o.ToHour))
                .Where(o => !o.Week.Contains(week)).Select(o => o.ID).ToList();
            var itemHide2 = db.TimeBased.Where(o => hide2.Contains(o.RuleID)).Select(o => o.BookID).ToList();
            return itemHide.Union(itemHide2).ToList();
        }
        public IEnumerable<Book> tGetByNumber(int number)
        {
            List<int> itemHide = GetTimeBased();
            IEnumerable<Book> books = table.Where(o => !itemHide.Contains(o.ID)).Take(number)
                .OrderByDescending(o => o.ID).Take(number);
            return books;
        }
        public Book tGetByID(int id)
        {
            List<int> itemHide = GetTimeBased();
            Book book = table.Find(id);
            if (!itemHide.Contains(book.ID))
                return book;
            else
                return null;
        }
        public IEnumerable<Book> tGetRelated(int id)
        {
            List<int> itemHide = GetTimeBased();
            var related = db.Recommend.Where(o => o.FirstBookID == id).Select(o => o.SecondBookID);
            var book = table.Where(o => related.Contains(o.ID) && !itemHide.Contains(o.ID));
            return book.Count() > 4 ? book.Take(4) : book;
        }
        public ListPaging<Book> tGetByCatPage(int id, int pageSize, int page)
        {
            List<int> itemHide = GetTimeBased();
            var value = table.Where(o => !itemHide.Contains(o.ID) && o.bookCat.Select(a => a.CatID).Contains(id));
            var book = value.OrderBy(o => o.ID).Skip(pageSize * (page - 1)).Take(pageSize).ToList()
                .Select(o => new Book(o,true));
            Paging paging = new Paging()
            {
                totalItem = value.Count(),
                pageSize = pageSize,
                pageIndex = page
            };
            ListPaging<Book> bookPaging = new ListPaging<Book>()
            {
                list = book,
                paging = paging
            };
            return bookPaging;
        }
        public ListPaging<Book> tGetBySubCatPage(int id, int pageSize, int page)
        {
            List<int> itemHide = GetTimeBased();
            var value = table.Where(o => !itemHide.Contains(o.ID) && o.bookSubCat.Select(a => a.SubCatID).Contains(id));
            var book = value.OrderBy(o => o.ID).Skip(pageSize * (page - 1)).Take(pageSize).ToList()
                .Select(o => new Book(o, true));
            Paging paging = new Paging()
            {
                totalItem = value.Count(),
                pageSize = pageSize,
                pageIndex = page
            };
            ListPaging<Book> bookPaging = new ListPaging<Book>()
            {
                list = book,
                paging = paging
            };
            return bookPaging;
        }
        public ListPaging<Book> tGetPage(int pageSize, int page)
        {
            List<int> itemHide = GetTimeBased();
            var value = table.Where(o => !itemHide.Contains(o.ID));
            var book = value.OrderBy(o => o.ID).Skip(pageSize * (page - 1)).Take(pageSize).ToList()
                .Select(o => new Book(o, true));
            Paging paging = new Paging()
            {
                totalItem = value.Count(),
                pageSize = pageSize,
                pageIndex = page
            };
            ListPaging<Book> bookPaging = new ListPaging<Book>()
            {
                list = book,
                paging = paging
            };
            return bookPaging;
        }
    }
}
