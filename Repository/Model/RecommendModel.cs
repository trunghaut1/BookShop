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
        public Recommend()
        {

        }
        public Recommend(Recommend value)
        {
            FirstBookID = value.FirstBookID;
            SecondBookID = value.SecondBookID;
        }
    }
}
