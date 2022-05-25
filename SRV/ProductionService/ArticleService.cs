using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace SRV.ProductionService
{
    public class ArticleService : BaseService, IArticleService
    {
        private readonly ArticleRepository articleRepository;

        public ArticleService() => articleRepository = new ArticleRepository(DbContext);

        public ArticleModel Find(int articleId)
        {
            return Mapper.Map<ArticleModel>(articleRepository.FindById(articleId));
        }

        public ArticleModel FindAll(int articleId)
        {
            ArticleModel articleModel = Mapper.Map<ArticleModel>(articleRepository.FindAll(articleId));

            if (articleModel != null)
            {
                // 文章列表页是按发布时间倒序呈现，因此用户视角上下篇与与数据库内排序相反
                articleModel.PreviousArticle = Mapper.Map<ArticleModel>(articleRepository.GetNextArticle(articleId));
                articleModel.NextArticle = Mapper.Map<ArticleModel>(articleRepository.GetPreviousArticle(articleId));
            }
            return articleModel;
        }

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
                Expression<Func<Article, bool>> predicate = a => a.Keywords.Select(k => k.Id).Contains((int)keywordId);
                articles = articleRepository.GetArticles(pageIndex, articleSize, predicate);
                articlesCount = articleRepository.GetArticlesCount(predicate);
            }
            return Mapper.Map<List<ArticleModel>>(articles);
        }

        public List<ArticleModel> GetMyArticles(int pageIndex, int articleSize, out int articlesCount, int userId)
        {
            List<Article> articles = articleRepository.GetArticles(pageIndex, articleSize, a => a.Author.Id == userId);
            articlesCount = articleRepository.GetArticlesCount(a => a.Author.Id == userId);
            return Mapper.Map<List<ArticleModel>>(articles);
        }

        public void Publish(ArticleModel article, int userId)
        {
            Article newArticle = new Article();
            Mapper.Map(article, newArticle);

            if (newArticle.Abstract is null)
            {
                newArticle.Abstract = GenerateAbstract(newArticle.Body);
            }
            AddKeywords(article.KeywordsReceiver, newArticle.Keywords);
            newArticle.Author = new RegisterService().Find(userId);
            newArticle.PublishTime = DateTime.Now;
            articleRepository.Save(newArticle);
        }

        public void Delete(int articleId, int userId)
        {
            Article article = articleRepository.FindAll(articleId);
            if (article.Author.Id != userId)
            {
                throw new ArgumentException("The current user is not the article author.");
            }

            // 考虑使用触发器/存储过程删除评论评价
            var evaluationRepository = new EvaluationRepository(DbContext);
            if (article.Comments.Count > 0)
            {
                var commentRepository = new CommentRepository(DbContext);
                var comments = article.Comments;

                for (int i = comments.Count - 1; i >= 0; i--)
                {
                    for (int j = comments[i].Evaluations.Count - 1; j >= 0; j--)
                    {
                        evaluationRepository.Delete(comments[i].Evaluations[j]);
                    }
                    commentRepository.Delete(comments[i]);
                }
            }

            for (int i = article.Evaluations.Count - 1; i >= 0; i--)
            {
                evaluationRepository.Delete(article.Evaluations[i]);
            }

            RemoveKeyword(article.Keywords);
            articleRepository.Delete(article);
        }

        public void Edit(int articleId, ArticleModel article)
        {
            Article originalArticle = articleRepository.FindById(articleId);
            originalArticle.Title = article.Title;
            originalArticle.Body = article.Body;
            originalArticle.Abstract = article.Abstract ?? GenerateAbstract(article.Body);
            // 先删除原关键字再添加新关键字
            RemoveKeyword(originalArticle.Keywords);
            // 防止同一关键字被删除后再添加引发的异常
            DbContext.SaveChanges();
            AddKeywords(article.KeywordsReceiver, originalArticle.Keywords);
        }

        private void AddKeywords(string KeywordsStr, List<Keyword> keywords)
        {
            KeywordRepository keywordRepository = new KeywordRepository(DbContext);
            foreach (var k in KeywordsStr.Split(' '))
            {
                if (string.IsNullOrWhiteSpace(k)) continue;
                // 判断关键字是否已存在
                Keyword keyword = keywordRepository.GetByText(k);
                if (keyword != null)
                {
                    keywords.Add(keyword);
                    keyword.UseCount++;
                }
                else { keywords.Add(new Keyword { Text = k, UseCount = 1 }); }
            }
        }

        private void RemoveKeyword(List<Keyword> keywords)
        {
            KeywordRepository keywordRepository = new KeywordRepository(DbContext);
            for (int i = keywords.Count - 1; i >= 0; i--)
            {
                keywords[i].UseCount--;
                if (keywords[i].UseCount <= 0)
                {
                    keywordRepository.Delete(keywords[i]);
                }
            }
        }

        private string GenerateAbstract(string body)
        {
            string htmlTagRegex = "<[^>]*>";
            string spanceTagRegex = "</p><p>|(&nbsp;)+";
            // 先去掉多余的换行、空格再去除所有Html标签
            string filterHtmlTagBody = Regex.Replace(body, spanceTagRegex, " ");
            string filterSpaceBody = Regex.Replace(filterHtmlTagBody, htmlTagRegex, string.Empty);
            // 避免正文字数不满256引发的下标越界异常
            return filterSpaceBody.Length > 256 ? filterSpaceBody.Substring(0, 255) : filterSpaceBody;
        }
    }
}
