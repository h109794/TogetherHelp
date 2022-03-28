using Global;
using SRV.ProductionService;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System.IO;
using System.Web.Mvc;
using UI.Filters;
using UI.Helper;

namespace UI.Controllers
{
    public class SharedController : Controller
    {
        public ActionResult GenerateCaptcha()
        {
            MemoryStream captchaStream = Utility.Captcha.GenerateCaptcha(out string code);
            Session.Add(Key.Captcha, code);

            return File(captchaStream, "image/jpge");
        }

        [NeedLoginFilter]
        public ActionResult PublishComment()
        {
            // 匹配不同route从segments中获取contentId
            if (!int.TryParse(Request.UrlReferrer.Segments[2], out int articleId))
            {
                articleId = int.Parse(Request.UrlReferrer.Segments[3]);
            }
            ICommentService commentService = new CommentService();
            CommentModel comment = commentService.Publish(articleId, CookieHelper.GetCurrentUserId(), Request.Form[Key.CommentContent],
                Request.Form[Key.ReplyUsername], Request.Form[Key.ReplyMainCommentId], Request.Form[Key.ReplyCommentId]);

            return View(comment);
        }

        [NeedLoginFilter]
        public ActionResult Evaluate()
        {
            IEvaluationService evaluationService = new EvaluationService();

            if (!int.TryParse(Request.Form[Key.ContentId], out int contentId))
            {
                if (!int.TryParse(Request.UrlReferrer.Segments[2], out contentId))
                    contentId = int.Parse(Request.UrlReferrer.Segments[3]);
            }
            bool isAgree = bool.Parse(Request.Form[Key.IsAgree]);
            bool isArticle = bool.Parse(Request.Form[Key.IsArticle]);
            string jsonValue = evaluationService.Evaluate(contentId, CookieHelper.GetCurrentUserId(), isAgree, isArticle);

            return Json(jsonValue);
        }
    }
}
