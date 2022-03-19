using BLL.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace BLL.Repository
{
    public class ArticleRepository : BaseRepository<Article>
    {
        public ArticleRepository(SqlDbContext sqlDbContext) : base(sqlDbContext) { }

        public List<Article> GetArticles(int pageIndex, int articleSize)
        {
            return sqlDbContext.Articles.Include(a => a.Author.PersonalData).Include(a => a.Keywords).Include(a => a.Comments)
                        .OrderByDescending(a => a.Id).Skip((pageIndex - 1) * articleSize).Take(articleSize).ToList();
        }

        public Article FindById(int id)
        {
            return sqlDbContext.Articles.Where(a => a.Id == id).Include(a => a.Author.PersonalData)
                        .Include(a => a.Keywords).Include(a => a.Comments.Select(c => c.Author).Select(u => u.PersonalData))
                        .Include(a => a.Comments.Select(c => c.ReplyUser).Select(u => u.PersonalData)).SingleOrDefault();
        }

        public int GetArticlesCount()
        {
            return sqlDbContext.Articles.Count();
        }
    }
}
