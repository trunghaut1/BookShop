using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ViewModel
{
    public class BookPaging
    {
        public IEnumerable<Book> book { get; set; }
        public Paging paging { get; set; }
    }
}
