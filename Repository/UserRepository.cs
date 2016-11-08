using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(BookEntities db) : base(db) { }

        public User SelectByEmail(string value)
        {
            return SelectBy(o => o.Email == value).FirstOrDefault();
        }
    }
}
