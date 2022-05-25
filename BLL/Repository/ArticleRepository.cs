using BLL.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System;
using System.Linq.Expressions;

namespace BLL.Repository
{
    public class ArticleRepository : BaseRepository<Article>
    {
        public ArticleRepository(SqlDbContext sqlDbContext) : base(sqlDbContext) { }

        public Article FindAll(int id)
        {
            return sqlDbContext.Articles.Where(a => a.Id == id)
                    .Include(a => a.Author.PersonalData).Include(a => a.Keywords).Include(a => a.Evaluations)
                    .Include(a => a.Comments.Select(c => c.Author).Select(u => u.PersonalData))
                    .Include(a => a.Comments.Select(c => c.ReplyUser).Select(u => u.PersonalData))
                    .Include(a => a.Comments.Select(c => c.Evaluations)).SingleOrDefault();
        }

        public Article FindById(int id)
        {
            return sqlDbContext.Articles.Where(a => a.Id == id).Include(a => a.Keywords).Single();
        }

        public List<Article> GetArticles(int pageIndex, int articleSize)
        {
            return sqlDbContext.Articles
                    .Include(a => a.Author.PersonalData).Include(a => a.Keywords)
                    .Include(a => a.Comments).Include(a => a.Evaluations)
                    .OrderByDescending(a => a.Id).Skip((pageIndex - 1) * articleSize).Take(articleSize).ToList();
        }

        public List<Article> GetArticles(int pageIndex, int articleSize, Expression<Func<Article, bool>> predicate)
        {
            return sqlDbContext.Articles.Where(predicate)
                    .Include(a => a.Author.PersonalData).Include(a => a.Keywords)
                    .Include(a => a.Comments).Include(a => a.Evaluations)
                    .OrderByDescending(a => a.Id).Skip((pageIndex - 1) * articleSize).Take(articleSize).ToList();
        }

        public Article GetArticleAndEvaluation(int id)
        {
            return sqlDbContext.Articles.Where(a => a.Id == id).Include(a => a.Evaluations).Single();
        }

        public Article GetPreviousArticle(int id)
        {
            return sqlDbContext.Articles.Where(a => a.Id < id).OrderByDescending(a => a.Id).FirstOrDefault();
        }

        public Article GetNextArticle(int id)
        {
            return sqlDbContext.Articles.Where(a => a.Id > id).OrderBy(a => a.Id).FirstOrDefault();
        }

        public int GetArticlesCount()
        {
            return sqlDbContext.Articles.Count();
        }

        public int GetArticlesCount(Expression<Func<Article, bool>> predicate)
        {
            return sqlDbContext.Articles.Where(predicate).Count();
        }
    }
}
