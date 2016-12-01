using Repository.Model;
using System.Collections.Generic;
using System.Linq;

namespace Repository.ServerRepository
{
    public class EFSubCatRepository : EFGenericRepository<SubCat>, IEFSubCatRepository
    {
        public EFSubCatRepository(BookEntities db) : base(db) { }
    }
}
