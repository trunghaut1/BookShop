using System;
using System.Collections.Generic;
using System.Linq;
using Repository.Model;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Migrations;

namespace Repository.ServerRepository
{
    public class EFGenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected BookEntities db { get; set; }
        protected DbSet<T> table;

        public EFGenericRepository(BookEntities db)
        {
            this.db = db;
            table = this.db.Set<T>();
        }

        public virtual IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public virtual IQueryable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return table.Where(predicate);
        }
        public virtual T GetByID(object id)
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
        public virtual bool Delete(object id)
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
