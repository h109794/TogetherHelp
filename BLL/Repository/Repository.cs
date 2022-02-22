using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace BLL.Repository
{
    public class Repository<T> where T : Entity.Entity, new()
    {
        protected SqlDbContext sqlDbContext;

        protected Repository(SqlDbContext sqlDbContext)
        {
            this.sqlDbContext = sqlDbContext;
        }

        public int Save(T entity)
        {
            sqlDbContext.Set<T>().Add(entity);
            // 获取存入对象的Id值
            sqlDbContext.SaveChanges();
            return entity.Id;
        }

        public void Delete(T entity)
        {
            sqlDbContext.Set<T>().Remove(entity);
        }

        public T Find(int id)
        {
            return sqlDbContext.Set<T>().Find(id);
        }
    }
}
