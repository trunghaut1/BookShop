using Repository.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.ServerRepository
{
    public class EFTimeRepository : EFGenericRepository<TimeRule>, ITimeRepository
    {
        public EFTimeRepository(BookEntities db) : base(db)
        {
        }

        public override bool Update(TimeRule obj)
        {
            try
            {
                TimeRule rule = table.Find(obj.ID);
                var del = rule.TimeBased.Where(o => !obj.TimeBased.Select(t => t.BookID).Contains(o.BookID));
                var add = obj.TimeBased.Where(o => !rule.TimeBased.Select(t => t.BookID).Contains(o.BookID));
                table.AddOrUpdate(obj);
                db.TimeBased.RemoveRange(del);
                db.TimeBased.AddRange(add);
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
