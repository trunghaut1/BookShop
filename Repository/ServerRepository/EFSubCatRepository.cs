using Repository.Model;
using System.Collections.Generic;

namespace Repository.ServerRepository
{
    public class EFSubCatRepository : EFGenericRepository<SubCat>, IEFSubCatRepository
    {
        public EFSubCatRepository(BookEntities db) : base(db) { }

        public IEnumerable<SubCat> GetByCat(int id)
        {
            return GetBy(o => o.CatID == id);
        }
    }
}
