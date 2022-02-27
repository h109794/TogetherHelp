using Global;
using SRV.ProductionService;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    public class ArticleController : Controller
    {
        public ActionResult Index(int? id)
        {
            const int articleSize = 2;
            int pageIndex = (id is null) ? 1 : (int)id;

            ArticleService articleService = new ArticleService();
            List<ArticleModel> articles = articleService.GetArticles(pageIndex, articleSize);

            // 避免无法整除导致的末尾页码丢失
            int pageCount = (int)Math.Ceiling((double)articleService.GetArticlesCount() / articleSize);
            ViewBag.PageIndex = pageIndex;
            ViewBag.PageCount = pageCount;

            return View(articles);
        }
    }
}
