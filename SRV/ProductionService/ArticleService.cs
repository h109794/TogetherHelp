using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SRV.ProductionService
{
    public class ArticleService : BaseService, IArticleService
    {
        private readonly ArticleRepository articleRepository;

        public ArticleService() => articleRepository = new ArticleRepository(DbContext);

        public List<ArticleModel> GetArticles(int pageIndex, int articleSize, out int articlesCount, int? keywordId)
        {
            List<Article> articles;

            if (keywordId is null)
            {
                articles = articleRepository.GetArticles(pageIndex, articleSize);
                articlesCount = articleRepository.GetArticlesCount();
            }
            else
            {
                articles = articleRepository.GetArticlesByKeyword(pageIndex, articleSize, (int)keywordId);
                articlesCount = articleRepository.GetArticlesCountByKeyword((int)keywordId);
            }
            return Mapper.Map<List<ArticleModel>>(articles);
        }

        public ArticleModel FindById(int id)
        {
            Article article = articleRepository.FindById(id);
            ArticleModel articleModel = Mapper.Map<ArticleModel>(article);

            if (articleModel != null)
            {
                // 文章列表页是按发布时间倒序呈现，因此用户视角上下篇与与数据库内排序相反
                articleModel.PreviousArticle = Mapper.Map<ArticleModel>(articleRepository.GetNextArticle(article.Id));
                articleModel.NextArticle = Mapper.Map<ArticleModel>(articleRepository.GetPreviousArticle(article.Id));
            }
            return articleModel;
        }

        public void Publish(ArticleModel article, int userId)
        {
            Article newArticle = new Article();
            Mapper.Map(article, newArticle);

            if (article.KeywordsReceiver != null)
            {
                KeywordRepository keywordRepository = new KeywordRepository(DbContext);

                foreach (var k in article.KeywordsReceiver.Split(','))
                {
                    if (string.IsNullOrWhiteSpace(k)) continue;

                    // 判断关键字是否已存在
                    Keyword keyword = keywordRepository.GetByText(k);
                    if (keyword is null)
                    {
                        newArticle.Keywords.Add(new Keyword { Text = k, UseCount = 1 });
                    }
                    else
                    {
                        newArticle.Keywords.Add(keyword);
                        keyword.UseCount++;
                    }
                }
            }
            if (newArticle.Abstract is null)
            {
                string htmlTagRegex = "<[^>]*>";
                string spanceTagRegex = "</p><p>|[&nbsp;]+";
                // 先去掉多余的换行、空格再去除所有Html标签
                string filterHtmlTagBody = Regex.Replace(newArticle.Body, spanceTagRegex, " ");
                string filterSpaceBody = Regex.Replace(filterHtmlTagBody, htmlTagRegex, string.Empty);
                // 避免正文字数不满256引发的下标越界异常
                newArticle.Abstract = (filterSpaceBody.Length > 256) ? filterSpaceBody.Substring(0, 255) : filterSpaceBody;
            }

            newArticle.Author = new RegisterService().Find(userId);
            newArticle.PublishTime = DateTime.Now;
            articleRepository.Save(newArticle);
        }
    }
}
