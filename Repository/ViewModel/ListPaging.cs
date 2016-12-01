using Repository.Model;
using System.Collections.Generic;

namespace Repository.ViewModel
{
    public class ListPaging<T> where T : class
    {
        public IEnumerable<T> list { get; set; }
        public Paging paging { get; set; }
    }
}
