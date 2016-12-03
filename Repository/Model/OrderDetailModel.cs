using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [ImplementPropertyChanged]
    public partial class OrderDetail
    {
        public OrderDetail(int orderId, int bookId, int price, int quantity, Book book)
        {
            OrderID = orderId;
            BookID = bookId;
            Price = price;
            Quantity = quantity;
            Book = new Book(book, true);
        }
        public OrderDetail(OrderDetail value, bool noBook)
        {
            OrderID = value.BookID;
            BookID = value.BookID;
            Price = value.Price;
            Quantity = value.Quantity;
        }
        public OrderDetail() { }
    }
}
