using Repository.Model;
using System.Collections.Generic;
using System.Linq;

namespace Repository.ServerRepository
{
    public class EFSubCatRepository : EFGenericRepository<SubCat>, ISubCatRepository
    {
        public EFSubCatRepository(BookEntities db) : base(db) { }
        public IEnumerable<SubCat> GetByBook(int id)
        {
            return table.Where(o => o.bookSubCat.Select(a => a.BookID).Contains(id));
        }
    }
}
