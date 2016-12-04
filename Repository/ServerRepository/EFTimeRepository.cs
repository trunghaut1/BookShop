using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServerRepository
{
    public class EFTimeRepository : EFGenericRepository<TimeBased>, IEFTimeRepository
    {
        public EFTimeRepository(BookEntities db) : base(db)
        {
        }
    }
}
