using Repository.Model;
using System.Collections.Generic;

namespace Repository.ServerRepository
{
    public interface IUserRepository : IGenericRepository<User>
    {
        User GetByEmail(string value);
        IEnumerable<User> GetByName(string name);
    }
}
