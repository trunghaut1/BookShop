using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Model
{
    [ImplementPropertyChanged]
    public partial class Recommend
    {
        public Recommend(int id1, int id2)
        {
            FirstBookID = id1;
            SecondBookID = id2;
        }
        public Recommend(int id1, int id2, Book book1)
        {
            FirstBookID = id1;
            SecondBookID = id2;
            Book1 = book1;
        }
        public Recommend()
        {

        }
        public Recommend(Recommend value)
        {
            FirstBookID = value.FirstBookID;
            SecondBookID = value.SecondBookID;

            Book1 = new Book(value.Book1, true);
        }
    }
}
