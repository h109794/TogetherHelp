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
        public ActionResult PublishComment(int articleId)
        {
            ICommentService commentService = new CommentService();
            CommentModel comment = commentService.Publish(articleId, CookieHelper.GetCurrentUserId(),
                                        Request.Form[Key.CommentContent], Request.Form[Key.ReplyUsername],
                                        Request.Form[Key.ReplyMainCommentId], Request.Form[Key.ReplyCommentId]);

            return PartialView(comment);
        }

        [NeedLoginFilter]
        public ActionResult Evaluate(int contentId)
        {
            IEvaluationService evaluationService = new EvaluationService();

            bool isAgree = bool.Parse(Request.Form[Key.IsAgree]);
            bool isArticle = bool.Parse(Request.Form[Key.IsArticle]);
            string jsonValue = evaluationService.Evaluate(contentId, CookieHelper.GetCurrentUserId(), isAgree, isArticle);

            return Json(jsonValue);
        }
    }
}
