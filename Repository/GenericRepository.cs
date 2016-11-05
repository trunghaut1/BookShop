using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Model;
using System.Data.Entity;

namespace Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected BookEntities _db { get; set; }
        protected DbSet<T> _table = null;

        public GenericRepository()
        {
            _db = new BookEntities();
            _table = _db.Set<T>();
        }
        public GenericRepository(BookEntities db)
        {
            _db = db;
            _table = _db.Set<T>();
        }

        public IEnumerable<T> SelectAll()
        {
            return _table.ToList();
        }

        public T SelectByID(object id)
        {
            try
            {
                return _table.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public T SelectByID(object id1, object id2)
        {
            try
            {
                return _table.Find(id1, id2);
            }
            catch
            {
                return null;
            }
        }

        public int Add(T obj)
        {
            try
            {
                _table.Add(obj);
                return _db.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public int Update(T obj)
        {
            try
            {
                _table.Attach(obj);
                _db.Entry(obj).State = EntityState.Modified;
                return _db.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }

        public int Delete(object id)
        {
            try
            {
                T existing = _table.Find(id);
                _table.Remove(existing);
                return _db.SaveChanges();
            }
            catch
            {
                return -1;
            }
        }
    }
}
