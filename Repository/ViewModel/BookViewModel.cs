using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;

namespace Repository.ViewModel
{
    public class BookViewModel
    {
        public Book book { get; set; }
        public IEnumerable<bookCat> bookCat { get; set; }
        public IEnumerable<bookSubCat> bookSubCat { get; set; }
    }
}
