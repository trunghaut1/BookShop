using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq.Expressions;

namespace Repository
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> SelectAll();
        IEnumerable<T> SelectBy(Expression<Func<T, bool>> predicate);
        T SelectByID(object id);
        T SelectByID(object id1, object id2);
        int Add(T obj);
        int Update(T obj);
        int Delete(object id);
    }
}
