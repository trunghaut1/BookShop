using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Repository
{
    public interface ICustomerRepository : IGenericRepository<Customer>
    {
        Customer SelectByEmail(string value);
    }
}
