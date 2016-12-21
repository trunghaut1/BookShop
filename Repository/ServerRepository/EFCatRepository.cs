using Repository.Model;
using System.Collections.Generic;
using System.Linq;

namespace Repository.ServerRepository
{
    public class EFCatRepository: EFGenericRepository<Cat>, ICatRepository
    {
        public EFCatRepository(BookEntities db) : base(db) { }
        public IEnumerable<Cat> GetByBook(int id)
        {
            return table.Where(o => o.bookCat.Select(a => a.BookID).Contains(id));
        }
    }
}
