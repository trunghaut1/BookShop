using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ClientRepository
{
    public class SubCatRepository : GenericRepository<SubCat>
    {
        public SubCatRepository()
        {
            url = "subcat";
        }
    }
}
