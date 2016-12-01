using PropertyChanged;
using System.Collections.ObjectModel;

namespace Repository.Model
{
    [ImplementPropertyChanged]
    public partial class SubCat
    {
        public SubCat(int? id, string name, int catId)
        {
            this.bookSubCat = new ObservableCollection<bookSubCat>();

            ID = id ?? 0;
            Name = name;
            CatID = catId;
        }
        public SubCat(SubCat value)
        {
            this.bookSubCat = new ObservableCollection<bookSubCat>();

            ID = value.ID;
            Name = value.Name;
            CatID = value.CatID;

            Cat = new Cat(value.Cat);
        }
        public SubCat(SubCat value, bool noCat)
        {
            this.bookSubCat = new ObservableCollection<bookSubCat>();

            ID = value.ID;
            Name = value.Name;
            CatID = value.CatID;
        }
    }
}
