using BLL.Entity;
using BLL.Repository;
using System;
using System.Collections.Generic;
using System.IO;

namespace QuickDb
{
    static class ArticleFactory
    {
        private static readonly SqlDbContext sqlDbContext;

        static ArticleFactory() => sqlDbContext = SqlDbContext.GetInstance();

        internal static Article Article1 { get; set; }
        internal static Article Article2 { get; set; }
        internal static Article Article3 { get; set; }

        internal static void GenerateArticles()
        {
            foreach (var item in GenerateArticleList())
            {
                sqlDbContext.Articles.Add(item);
            }
        }

        private static List<Article> GenerateArticleList()
        {
            List<Article> articles = new List<Article>();

            Article1 = CreateArticle("什么歌啊", GetArticleBody("BodyOne"), UserFactory.UserNumberOne);
            Article2 = CreateArticle("这是一首歌", GetArticleBody("BodyTwo"), UserFactory.UserNumberTwo);
            Article3 = CreateArticle("外键阴影属性", GetArticleBody("BodyThree"), UserFactory.UserNumberThree);

            Article2.Keywords.Add(KeywordFactory.DemoKeywordTwo);
            Article3.Keywords.Add(KeywordFactory.DemoKeywordTwo);
            KeywordFactory.DemoKeywordTwo.UseCount += 2;

            articles.Add(Article1);
            articles.Add(Article2);
            articles.Add(Article3);

            for (int i = 1; i < 61; i++)
            {
                articles.Add(CreateArticle($"标题{i}号", $"演示内容{i}号", UserFactory.Admin));
            }
            return articles;
        }

        private static Article CreateArticle(string title, string body, User author)
        {
            Article article = new Article
            {
                Title = title,
                Body = body,
                Author = author,
                PublishTime = DateTime.Now,
                Abstract = "测试自动摘要请使用发布功能",
                Keywords = new List<Keyword> { KeywordFactory.DemoKeywordOne },
            };
            KeywordFactory.DemoKeywordOne.UseCount++;

            return article;
        }

        private static string GetArticleBody(string filename)
        {
            // 获取应用程序的当前工作目录
            string path = Directory.GetCurrentDirectory();
            DirectoryInfo parentPath = Directory.GetParent(Directory.GetParent(path).ToString());
            return File.ReadAllText(parentPath.ToString() + $@"\ArticleBodys\{filename}.txt");
        }
    }
}
