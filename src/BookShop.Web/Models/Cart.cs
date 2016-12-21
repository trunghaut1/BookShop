using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Web.Models
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
    }
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();
        public virtual void AddItem(Book book, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Book.ID == book.ID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    Book = book,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }
        public virtual int GetBookQuantity(int id)
        {
            return lineCollection.Where(o => o.Book.ID == id).FirstOrDefault()?.Quantity ?? 0;
        }

        public virtual void RemoveLine(Book book) =>
            lineCollection.RemoveAll(l => l.Book.ID == book.ID);

        public virtual int ComputeTotalValue() =>
            lineCollection.Sum(e => (int)e.Book.Price * e.Quantity);

        public virtual void Clear() => lineCollection.Clear();

        public virtual IEnumerable<CartLine> Lines => lineCollection;
    }
}
