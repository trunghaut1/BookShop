using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    public partial class Book
    {
        public Book(int? id, string name, string author, string sum, string img, int? price, int? quantity)
        {
            this.bookCat = new ObservableCollection<bookCat>();
            this.bookSubCat = new ObservableCollection<bookSubCat>();
            this.OrderDetail = new ObservableCollection<OrderDetail>();
            this.Recommend = new ObservableCollection<Recommend>();
            this.TimeBased = new ObservableCollection<TimeBased>();
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
