using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ClientRepository
{
    public class TimeRepository : GenericRepository<TimeBased>
    {
        public TimeRepository()
        {
            url = "time";
        }
    }
}
