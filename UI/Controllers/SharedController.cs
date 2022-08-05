using Global;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
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
        private readonly IPersonalDataService personalDataService;

        public SharedController(
            ICommentService commentService,
            IEvaluationService evaluationService,
            IPersonalDataService personalDataService)
        {
            this.commentService = commentService;
            this.evaluationService = evaluationService;
            this.personalDataService = personalDataService;
        }

        public ActionResult GenerateCaptcha()
        {
            MemoryStream captchaStream = Utility.Captcha.GenerateCaptcha(out string code);
            Session.Add(Key.Captcha, code);

            return File(captchaStream, "image/jpge");
        }

        public ActionResult GetCurrentUserProfile()
        {
            int userId = CookieHelper.GetCurrentUserId();
            string cacheKey = Key.NavProfile + userId;

            if (HttpContext.Cache[cacheKey] is null)
            {
                HttpContext.Cache.Insert(
                    cacheKey,
                    personalDataService.Get(userId).Profile,
                    null,
                    DateTime.MaxValue,
                    new TimeSpan(0, 15, 0));
            }

            return File((byte[])HttpContext.Cache[cacheKey], "image");
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
    }
}
