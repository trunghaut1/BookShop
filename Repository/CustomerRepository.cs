using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(BookEntities db) : base(db) { }

        public Customer SelectByEmail(string value)
        {
            return SelectBy(o => o.Email == value).FirstOrDefault();
        }
    }
}
