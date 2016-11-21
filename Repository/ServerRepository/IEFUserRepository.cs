using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Repository.ServerRepository
{
    public interface IEFUserRepository : IEFGenericRepository<User>
    {
        User GetByEmail(string value);
    }
}
