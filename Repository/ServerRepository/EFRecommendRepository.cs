using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServerRepository
{
    public class EFRecommendRepository : EFGenericRepository<Recommend>, IEFRecommendRepository
    {
        public EFRecommendRepository(BookEntities db) : base(db)
        {
        }
    }
}
