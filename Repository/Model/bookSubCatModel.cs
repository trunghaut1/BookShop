using PropertyChanged;

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
