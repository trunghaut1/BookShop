using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Migrations;

namespace Repository.ServerRepository
{
    public class EFGenericRepository<T> : IEFGenericRepository<T> where T : class
    {
        protected BookEntities db { get; set; }
        protected DbSet<T> table = null;

        public EFGenericRepository()
        {
            db = new BookEntities();
            table = db.Set<T>();
        }
        public EFGenericRepository(BookEntities db)
        {
            this.db = db;
            table = this.db.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return table.Where(predicate);
        }

        public T GetByID(object id)
        {
            try
            {
                return table.Find(id);
            }
            catch
            {
                return null;
            }
        }

        public T GetByID(object id1, object id2)
        {
            try
            {
                return table.Find(id1, id2);
            }
            catch
            {
                return null;
            }
        }

        public virtual bool Add(T obj)
        {
            try
            {
                table.Add(obj);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public virtual bool Update(T obj)
        {
            try
            {
                table.AddOrUpdate(obj);
                db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(object id)
        {
            try
            {
                T existing = table.Find(id);
                table.Remove(existing);
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
