using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Repository.ServerRepository
{
    public class EFCatRepository: EFGenericRepository<Cat>, IEFCatRepository
    {
        public EFCatRepository(BookEntities db) : base(db) { }
    }
}
