using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class SubCatRepository : GenericRepository<SubCat>, ISubCatRepository
    {
        public SubCatRepository(BookEntities db) : base(db) { }

        public IEnumerable<SubCat> SelectByCat(int id)
        {
            return SelectBy(o => o.CatID == id);
        }
    }
}
