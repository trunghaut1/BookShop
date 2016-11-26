using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
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
    }
}
