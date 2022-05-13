using Global;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
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
            if (pageIndex == 0) { return RedirectToRoute(new { id = 1 }); }

            List<ArticleModel> articles = articleService.GetArticles(pageIndex, articleSize, out int articlesCount, keywordId);

            if (articles.Count == 0)
            {
                Response.StatusCode = 404;
                Response.WriteFile("~/Views/Shared/NotFound.html");
                return new EmptyResult();
            }

            // 避免无法整除导致末尾页码丢失
            int pageCount = (int)Math.Ceiling((double)articlesCount / articleSize);
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageCount = pageCount;
            ViewBag.URLParameter = keywordId is null ? null : $"?keywordId={keywordId}";
            ViewBag.CurrentUserId = (ViewData[Key.HasLogin] is null) ? 0 : CookieHelper.GetCurrentUserId();

            return View(articles);
        }

        public ActionResult Single(int id)
        {
            ArticleModel article = articleService.FindById(id);

            // 没有对应文章返回404页面
            if (article is null)
            {
                Response.StatusCode = 404;
                Response.WriteFile("~/Views/Shared/NotFound.html");
                return new EmptyResult();
            }

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
            // 防止用户添加关键字后再删光导致Receiver内有','使文章正常发布(匹配只有','的情况)
            if (Regex.IsMatch(newArticle.KeywordsReceiver, "^,+$"))
            {
                ModelState.AddModelError(nameof(newArticle.KeywordsReceiver), "* 关键字不能为空");
                return View();
            }

            articleService.Publish(newArticle, CookieHelper.GetCurrentUserId());
            return RedirectToAction("Index");
        }
    }
}
