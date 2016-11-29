using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
