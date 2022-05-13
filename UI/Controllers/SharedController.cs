using Global;
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
        private readonly ICommentService commentService;
        private readonly IEvaluationService evaluationService;

        public SharedController(
            ICommentService commentService,
            IEvaluationService evaluationService)
        {
            this.commentService = commentService;
            this.evaluationService = evaluationService;
        }

        public ActionResult GenerateCaptcha()
        {
            MemoryStream captchaStream = Utility.Captcha.GenerateCaptcha(out string code);
            Session.Add(Key.Captcha, code);

            return File(captchaStream, "image/jpge");
        }

        [NeedLoginFilter]
        public ActionResult PublishComment(int articleId)
        {
            CommentModel comment = commentService.Publish(articleId, CookieHelper.GetCurrentUserId(),
                                        Request.Form[Key.CommentContent], Request.Form[Key.ReplyUsername],
                                        Request.Form[Key.ReplyMainCommentId], Request.Form[Key.ReplyCommentId]);

            return PartialView(comment);
        }

        [NeedLoginFilter]
        public ActionResult Evaluate()
        {
            int contentId = int.Parse(Request.Form[Key.ContentId]);
            bool isAgree = bool.Parse(Request.Form[Key.IsAgree]);
            bool isArticle = bool.Parse(Request.Form[Key.IsArticle]);
            string jsonValue = evaluationService.Evaluate(contentId, CookieHelper.GetCurrentUserId(), isAgree, isArticle);

            return Json(jsonValue);
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}
