using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [ImplementPropertyChanged]
    public partial class TimeBased
    {
        public TimeBased()
        {
        }
        public TimeBased(int ruleId, int bookId)
        {
            RuleID = ruleId;
            BookID = bookId;
        }
        public TimeBased(int ruleId, Book book)
        {
            RuleID = ruleId;
            BookID = book.ID;
            Book = new Book(book, true);
        }
        public TimeBased(TimeBased value)
        {
            RuleID = value.RuleID;
            BookID = value.BookID;
            Book = new Book(value.Book, true);
        }
    }
}
