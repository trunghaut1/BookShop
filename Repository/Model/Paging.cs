using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public class Paging
    {
        public int totalItem { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }

        public int totalPage => pageSize == 0 ? 0 : (int)Math.Ceiling((decimal)totalItem / pageSize);
    }
}
