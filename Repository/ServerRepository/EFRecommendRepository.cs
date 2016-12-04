using Repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServerRepository
{
    public class EFRecommendRepository : EFGenericRepository<Recommend>, IEFRecommendRepository
    {
        public EFRecommendRepository(BookEntities db) : base(db)
        {
        }
        public IEnumerable<Recommend> GetListByID(int id)
        {
            return GetBy(o => o.FirstBookID == id).AsEnumerable();
        }
        public int CountRecommend(int id)
        {
            return GetBy(o => o.FirstBookID == id).Count();
        }
        public bool UpdateList(int id, IEnumerable<Recommend> value)
        {
            try
            {
                var recommend = table.Where(o => o.FirstBookID == id).AsEnumerable();
                IEnumerable<Recommend> delList = recommend.Where(o => !value.Select(b => b.SecondBookID).Contains(o.SecondBookID));
                IEnumerable<Recommend> addList = value.Where(o => !recommend.Select(b => b.SecondBookID).Contains(o.SecondBookID));
                table.RemoveRange(delList);
                table.AddRange(addList);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
