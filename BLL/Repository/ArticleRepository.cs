using BLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace BLL.Repository
{
    public class ArticleRepository : Repository<Article>
    {
        public ArticleRepository(SqlDbContext sqlDbContext) : base(sqlDbContext) { }

        public List<Article> GetArticles(int pageIndex, int articleSize)
        {
            return sqlDbContext.Articles.Include(a => a.Author).Include(a => a.Keywords)
                        .OrderBy(a => a.Id).Skip((pageIndex - 1) * articleSize).Take(articleSize).ToList();
        }

        public int GetArticlesCount()
        {
            return sqlDbContext.Articles.Count();
        }
    }
}
