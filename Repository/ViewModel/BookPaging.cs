using Repository.Model;
using System.Collections.Generic;

namespace Repository.ViewModel
{
    public class BookPaging
    {
        public IEnumerable<Book> book { get; set; }
        public Paging paging { get; set; }
    }
}
