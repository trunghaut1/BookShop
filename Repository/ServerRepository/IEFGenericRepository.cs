using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Repository.ServerRepository
{
    public interface IEFGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IQueryable<T> GetBy(Expression<Func<T, bool>> predicate);
        T GetByID(object id);
        T GetByID(object id1, object id2);
        bool Add(T obj);
        bool Update(T obj);
        bool Delete(object id);
    }
}
