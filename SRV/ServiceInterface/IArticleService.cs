using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV.ServiceInterface
{
    public interface IArticleService
    {
        List<ArticleModel> GetArticles(int pageIndex, int articleSize);
        int GetArticlesCount();
        void Publish(ArticleModel article, int userId);
        ArticleModel FindById(int id);
    }
}
