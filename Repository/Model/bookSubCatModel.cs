using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [ImplementPropertyChanged]
    public partial class bookSubCat
    {
        public bookSubCat() { }
        public bookSubCat(int bookId, int subCatId)
        {
            BookID = bookId;
            SubCatID = subCatId;
        }
        public bookSubCat(bookSubCat value)
        {
            BookID = value.BookID;
            SubCatID = value.SubCatID;
        }
    }
}
