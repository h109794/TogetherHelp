using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV.ProductionService
{
    public class ArticleService : BaseService, IArticleService
    {
        private readonly ArticleRepository articleRepository;

        public ArticleService()
        {
            articleRepository = new ArticleRepository(dbContext);
        }

        public List<ArticleModel> GetArticles(int pageIndex, int articleSize)
        {
            List<Article> articles = articleRepository.GetArticles(pageIndex, articleSize);
            List<ArticleModel> articleModels = new List<ArticleModel>();
            mapper.Map(articles, articleModels);

            return articleModels;
        }

        public int GetArticlesCount()
        {
            return articleRepository.GetArticlesCount();
        }
    }
}
