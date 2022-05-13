using SRV.ViewModel;
using System.Collections.Generic;

namespace SRV.ServiceInterface
{
    public interface IArticleService
    {
        List<ArticleModel> GetArticles(int pageIndex, int articleSize, out int articlesCount, int? keywordId);
        void Publish(ArticleModel article, int userId);
        ArticleModel FindById(int id);
    }
}
