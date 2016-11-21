using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
