using Repository.Model;
using System.Collections.Generic;

namespace Repository.ServerRepository
{
    public interface IEFUserRepository : IEFGenericRepository<User>
    {
        User GetByEmail(string value);
    }
}
