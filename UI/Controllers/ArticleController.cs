using Global;
using SRV.ProductionService;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;
using UI.Helper;

namespace UI.Controllers
{
    [ModelValidationFilter]
    public class ArticleController : Controller
    {
        private readonly ArticleService articleService;

        public ArticleController()
        {
            articleService = new ArticleService();
        }

        public ActionResult Index(int? id)
        {
            const int articleSize = 2;
            int pageIndex = (id is null) ? 1 : (int)id;
            List<ArticleModel> articles = articleService.GetArticles(pageIndex, articleSize);

            // 避免无法整除导致末尾页码丢失
            int pageCount = (int)Math.Ceiling((double)articleService.GetArticlesCount() / articleSize);
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageCount = pageCount;

            return View(articles);
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
            articleService.Publish(newArticle, CookieHelper.GetCurrentUserId());
            return RedirectToAction("Index");
        }
    }
}
