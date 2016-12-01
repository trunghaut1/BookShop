using System.Linq;
using Repository.Model;
using System.Collections.Generic;

namespace Repository.ServerRepository
{
    public class EFUserRepository : EFGenericRepository<User>, IEFUserRepository
    {
        public EFUserRepository(BookEntities db) : base(db) { }

        public User GetByEmail(string value)
        {
            return GetBy(o => o.Email == value).FirstOrDefault();
        }
    }
}
