using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace BLL.Repository
{
    public class Repository<T> where T : Entity.Entity, new()
    {
        protected SqlDbContext sqlContext;
        protected DbSet<T> dbSet;

        protected Repository(SqlDbContext sqlContext)
        {
            this.sqlContext = sqlContext;
        }

        public int Save(T entity)
        {
            dbSet.Add(entity);
            return entity.Id;
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Find(int id)
        {
            throw new NotImplementedException();
        }
    }
}
