using PropertyChanged;

namespace Repository.Model
{
    [ImplementPropertyChanged]
    public partial class bookCat
    {
        public bookCat() { }
        public bookCat(int bookId, int catId)
        {
            BookID = bookId;
            CatID = catId;
        }
        public bookCat(bookCat value)
        {
            BookID = value.BookID;
            CatID = value.CatID;
        }
    }
}
