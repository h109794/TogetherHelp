using Global;
using SRV.ProductionService;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.IO;
using System.Linq;
using System.Web;
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
            CommentModel comment = commentService.Publish(articleId, CookieHelper.GetCurrentUserId(), Request.Form[Key.CommentContent]);

            return View(comment);
        }
    }
}
