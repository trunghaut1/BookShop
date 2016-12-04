using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServerRepository
{
    public interface IEFRecommendRepository : IEFGenericRepository<Recommend>
    {
        IEnumerable<Recommend> GetListByID(int id);
        int CountRecommend(int id);
        bool UpdateList(int id, IEnumerable<Recommend> value);
    }
}
