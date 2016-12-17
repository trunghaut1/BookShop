using PropertyChanged;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repository.Model
{
    [ImplementPropertyChanged]
    public partial class Cat
    {
        public Cat(int? id, string name)
        {
            this.bookCat = new ObservableCollection<bookCat>();
            this.SubCat = new ObservableCollection<SubCat>();
            ID = id ?? 0;
            Name = name;
        }
        public Cat(Cat value)
        {
            this.bookCat = new ObservableCollection<bookCat>();
            this.SubCat = new ObservableCollection<SubCat>();
            ID = value.ID;
            Name = value.Name;
        }
        public Cat(Cat value, bool includeSubCat)
        {
            this.bookCat = new ObservableCollection<bookCat>();
            this.SubCat = new ObservableCollection<SubCat>();
            ID = value.ID;
            Name = value.Name;
            SubCat = new ObservableCollection<SubCat>(value.SubCat.Select(o => new SubCat(o.ID, o.Name, o.CatID)));
        }
    }
}
