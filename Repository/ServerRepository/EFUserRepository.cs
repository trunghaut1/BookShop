using System.Linq;
using Repository.Model;
using System.Collections.Generic;

namespace Repository.ServerRepository
{
    public class EFUserRepository : EFGenericRepository<User>, IUserRepository
    {
        public EFUserRepository(BookEntities db) : base(db) { }

        public User GetByEmail(string value)
        {
            return GetBy(o => o.Email == value).FirstOrDefault();
        }
        public IEnumerable<User> GetByName(string name)
        {
            return GetBy(o => o.Name.Contains(name)).AsEnumerable();
        }
    }
}
