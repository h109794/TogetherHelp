using SRV.ProductionService;
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
    [ModelValidationFilter]
    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController()
        {
            articleService = new ArticleService();
        }

        public ActionResult Index(int? id)
        {
            const int articleSize = 5;
            int pageIndex = (id is null) ? 1 : (int)id;
            List<ArticleModel> articles = articleService.GetArticles(pageIndex, articleSize);

            // 避免无法整除导致末尾页码丢失
            int pageCount = (int)Math.Ceiling((double)articleService.GetArticlesCount() / articleSize);
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageCount = pageCount;

            return View(articles);
        }

        public ActionResult Single(int id)
        {
            ArticleModel article = articleService.FindById(id);

            if (article is null)
            {
                return Redirect("~/Shared/Error");
            }
            return View(article);
        }

        [NeedLoginFilter]
        public ActionResult Publish()
        {
            return View();
        }

        [HttpPost]
        [NeedLoginFilter]
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
