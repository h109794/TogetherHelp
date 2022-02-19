using BLL.Repository;
using Global;
using SRV.ServiceInterface;
using System.Web;

namespace SRV.ProductionService
{
    public class BaseService : IUserService
    {
        // 每个Action使用同一个DbContext
        protected SqlDbContext dbContext
        {
            get
            {
                if (HttpContext.Current.Items[Key.DbContext] is null)
                {
                    HttpContext.Current.Items.Add(Key.DbContext, new SqlDbContext());
                    ((SqlDbContext)HttpContext.Current.Items[Key.DbContext]).Database.BeginTransaction();
                }
                return (SqlDbContext)HttpContext.Current.Items[Key.DbContext];
            }
        }
    }
}
