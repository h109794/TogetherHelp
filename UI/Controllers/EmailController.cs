using Global;
using SRV.ProductionService;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Web.Mvc;
using UI.Filters;
using UI.Helper;

namespace UI.Controllers
{
    [NeedLoginFilter]
    [ModelValidationFilter]
    public class EmailController : Controller
    {
        private readonly IContactService contactService;

        public EmailController() => contactService = new ContactService();

        public ActionResult Index()
        {
            EmailModel email = contactService.GetEmail(CookieHelper.GetCurrentUserId());
            return View(email);
        }

        [HttpPost]
        public ActionResult Index(EmailModel email)
        {
            if (email.VerificationCode.ToLower() == Session[Key.Captcha].ToString().ToLower())
            {
                contactService.BindEmail(CookieHelper.GetCurrentUserId(), email.EmailAddress);
            }
            else
            {
                ModelState.AddModelError(nameof(email.VerificationCode), "* 验证码错误");
                return View();
            }

            return RedirectToAction("Index");
        }

        public JsonResult Send()
        {
            string emailAddress = Request.Form[Key.EmailAddress];
            if (contactService.ValidateEmailActivation(CookieHelper.GetCurrentUserId(), emailAddress))
            { return Json(false); }

            string captcha = new Random().Next(999999).ToString().PadLeft(6, '0');
            string emailContent = "<div style=\"padding:1rem\">" +
                                      "<p>感谢使用一起帮！</p>" +
                                      $"<p>您的邮箱验证码为：<b>{captcha}</b></p>" +
                                      "<p>20分钟内有效。</p>" +
                                      "<p>如非本人操作，请忽略此邮件。</p>" +
                                  "</div>";

            Utility.SendEmail(emailAddress, "一起帮邮箱验证", emailContent);
            Session.Add(Key.Captcha, captcha);
            return Json(true);
        }
    }
}
