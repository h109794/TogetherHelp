namespace BLL.Repository
{
    public class BaseRepository<T> where T : Entity.BaseEntity, new()
    {
        protected SqlDbContext sqlDbContext;

        protected BaseRepository(SqlDbContext sqlDbContext)
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

        public virtual T Find(int id)
        {
            return sqlDbContext.Set<T>().Find(id);
        }
    }
}
