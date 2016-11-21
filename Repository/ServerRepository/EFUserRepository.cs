using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

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
