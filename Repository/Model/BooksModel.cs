using PropertyChanged;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Repository.Model
{
    [ImplementPropertyChanged]
    public partial class Book
    {
        public ObservableCollection<int> catList { get; set; }
        public ObservableCollection<int> subCatList { get; set; }
        public Book(int? id, string name, string author, string sum, string img, int? price, int? quantity,
            ObservableCollection<int> catList, ObservableCollection<int> subCatList)
        {
            this.bookCat = new ObservableCollection<bookCat>();
            this.bookSubCat = new ObservableCollection<bookSubCat>();
            this.OrderDetail = new ObservableCollection<OrderDetail>();
            this.Recommend = new ObservableCollection<Recommend>();
            this.TimeBased = new ObservableCollection<TimeBased>();

            this.catList = catList ?? new ObservableCollection<int>();
            this.subCatList = subCatList ?? new ObservableCollection<int>();
            ID = id ?? 0;
            Name = name;
            Author = author;
            Summary = sum;
            Image = img;
            Price = price;
            Quantity = quantity;
        }
        public Book(Book value)
        {
            this.bookCat = new ObservableCollection<bookCat>();
            this.bookSubCat = new ObservableCollection<bookSubCat>();
            this.OrderDetail = new ObservableCollection<OrderDetail>();
            this.Recommend = new ObservableCollection<Recommend>();
            this.TimeBased = new ObservableCollection<TimeBased>();

            this.catList = value.bookCat != null ? new ObservableCollection<int>(value.bookCat.Select(o => o.CatID)) :
                new ObservableCollection<int>();
            this.subCatList = value.bookSubCat != null ? new ObservableCollection<int>(value.bookSubCat.Select(o => o.SubCatID)) :
                new ObservableCollection<int>();
            ID = value.ID;
            Name = value.Name;
            Author = value.Author;
            Summary = value.Summary;
            Image = value.Image;
            Price = value.Price;
            Quantity = value.Quantity;
        }
    }
}
