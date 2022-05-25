using SRV.ViewModel;
using System.Collections.Generic;

namespace SRV.ServiceInterface
{
    public interface IArticleService
    {
        List<ArticleModel> GetArticles(int pageIndex, int articleSize, out int articlesCount, int? keywordId);
        List<ArticleModel> GetMyArticles(int pageIndex, int articleSize, out int articlesCount, int userId);
        /// <summary>
        /// 获取文章本体及其关键字
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        ArticleModel Find(int articleId);
        /// <summary>
        /// 获取包括评论、评价在内的所有文章信息
        /// </summary>
        /// <param name="articleId"></param>
        /// <returns></returns>
        ArticleModel FindAll(int articleId);
        void Publish(ArticleModel article, int userId);
        void Delete(int articleId, int userId);
        void Edit(int articleId, ArticleModel article);
    }
}
