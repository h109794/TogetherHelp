using DevTrends.MvcDonutCaching;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System.Collections.Generic;
using System.Web.Mvc;

namespace UI.Controllers
{
    public class KeywordController : Controller
    {
        const int keywordCount = 5;
        private readonly IKeywordService keywordService;

        public KeywordController(IKeywordService keywordService) => this.keywordService = keywordService;

        [ChildActionOnly]
        [DonutOutputCache(CacheProfile = "SidebarKeyword")]
        public ActionResult Index()
        {
            List<KeywordModel> keywords = keywordService.Select(keywordCount);
            return PartialView("_SidebarKeywords", keywords);
        }

        public ActionResult Change(int lastKeywordId)
        {
            List<KeywordModel> keywords = keywordService.Select(keywordCount, lastKeywordId);
            return Json(keywords, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Search(string keywordText)
        {
            int? keywordId = keywordService.GetKeywordId(keywordText);
            return Json(keywordId, JsonRequestBehavior.AllowGet);
        }
    }
}
