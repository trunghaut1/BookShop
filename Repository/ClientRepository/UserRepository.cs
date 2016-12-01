using Repository.Helper;
using Repository.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.ClientRepository
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository()
        {
            url = "user";
        }
    }
}
