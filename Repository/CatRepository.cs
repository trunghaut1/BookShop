using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Repository
{
    public class CatRepository: GenericRepository<Cat>, ICatRepository
    {
        public CatRepository(BookEntities db) : base(db) { }
    }
}
