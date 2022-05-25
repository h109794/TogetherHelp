using Global;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UI.Filters;
using UI.Helper;

namespace UI.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;
        const int articleSize = 5;

        public ArticleController(IArticleService articleService) => this.articleService = articleService;

        public ActionResult Index(int? keywordId, int pageIndex = 1)
        {
            if (pageIndex == 0) return RedirectToRoute(new { pageIndex = 1 });

            List<ArticleModel> articles = articleService.GetArticles(pageIndex, articleSize, out int articlesCount, keywordId);

            if (articles.Count == 0) return PartialView("NotFound");

            ArticleListModel articleList = new ArticleListModel
            {
                Articles = articles,
                PageIndex = pageIndex,
                // 避免无法整除导致末尾页码丢失
                PageCount = (int)Math.Ceiling((double)articlesCount / articleSize),
                NavURLParam = keywordId is null ? null : $"?keywordId={keywordId}",
                CurrentUserId = (ViewData[Key.HasLogin] is null) ? 0 : CookieHelper.GetCurrentUserId(),
            };

            return View(articleList);
        }

        public ActionResult Single(int articleId)
        {
            ArticleModel article = articleService.FindAll(articleId);

            // 没有对应文章返回404页面
            if (article is null) return PartialView("NotFound");

            ViewBag.CurrentUserId = (ViewData[Key.HasLogin] is null) ? 0 : CookieHelper.GetCurrentUserId();
            return View(article);
        }

        [NeedLoginFilter]
        [ModelValidationFilter]
        public ActionResult Publish()
        {
            return View();
        }

        [HttpPost]
        [NeedLoginFilter]
        [ModelValidationFilter]
        public ActionResult Publish(ArticleModel newArticle)
        {
            articleService.Publish(newArticle, CookieHelper.GetCurrentUserId());
            return RedirectToAction("Index");
        }

        [NeedLoginFilter]
        public ActionResult My(int pageIndex = 1)
        {
            if (pageIndex == 0) return RedirectToRoute("MyArticles", new { pageIndex = 1 });

            int userId = CookieHelper.GetCurrentUserId();
            List<ArticleModel> articles = articleService.GetMyArticles(pageIndex, articleSize, out int articlesCount, userId);

            ArticleListModel articleList = new ArticleListModel
            {
                Articles = articles,
                PageIndex = pageIndex,
                // 避免无法整除导致末尾页码丢失
                PageCount = (int)Math.Ceiling((double)articlesCount / articleSize),
                CurrentUserId = userId,
            };

            return View("Index", articleList);
        }

        [NeedLoginFilter]
        public ActionResult Delete(int articleId, int routeValue)
        {
            articleService.Delete(articleId, CookieHelper.GetCurrentUserId());
            return RedirectToAction("My", new { pageIndex = routeValue });
        }

        [NeedLoginFilter]
        [ModelValidationFilter]
        public ActionResult Edit(int articleId)
        {
            ArticleModel article = articleService.Find(articleId);
            return View("Publish", article);
        }

        [HttpPost]
        [NeedLoginFilter]
        [ModelValidationFilter]
        public ActionResult Edit(int articleId, ArticleModel article)
        {
            articleService.Edit(articleId, article);
            return RedirectToAction("Single", new { articleId });
        }
    }
}
