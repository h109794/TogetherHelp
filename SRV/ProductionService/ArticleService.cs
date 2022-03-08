﻿using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SRV.ProductionService
{
    public class ArticleService : BaseService, IArticleService
    {
        private readonly ArticleRepository articleRepository;

        public ArticleService()
        {
            articleRepository = new ArticleRepository(DbContext);
        }

        public List<ArticleModel> GetArticles(int pageIndex, int articleSize)
        {
            List<Article> articles = articleRepository.GetArticles(pageIndex, articleSize);
            List<ArticleModel> articleModels = new List<ArticleModel>();
            Mapper.Map(articles, articleModels);

            return articleModels;
        }

        public ArticleModel FindById(int id)
        {
            Article article = articleRepository.FindById(id); ;
            return Mapper.Map<ArticleModel>(article);
        }

        public int GetArticlesCount()
        {
            return articleRepository.GetArticlesCount();
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
                        newArticle.Keywords.Add(new Keyword() { Text = k, UseCount = 1 });
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
