using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public partial class BookEntities : DbContext
    {
        public BookEntities(string connectString)
            : base(connectString)
        {
        }
    }
}
