using Global;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System.Web.Mvc;
using UI.Filters;
using UI.Helper;

namespace UI.Controllers
{
    [ModelValidationFilter]
    public class PasswordController : Controller
    {
        private readonly IPasswordService passwordService;

        public PasswordController(IPasswordService passwordService) => this.passwordService = passwordService;

        [NeedLoginFilter]
        public ActionResult Change()
        {
            return View();
        }

        [HttpPost]
        [NeedLoginFilter]
        public ActionResult Change(ChangePasswordModel changePwdModel)
        {
            if (changePwdModel.OriginalPwd == changePwdModel.NewPwd)
            {
                ModelState.AddModelError(nameof(changePwdModel.NewPwd), "* 新密码不能和原密码相同");
                return View();
            }
            if (!passwordService.ChangePassword(CookieHelper.GetCurrentUserId(), changePwdModel))
            {
                ModelState.AddModelError(nameof(changePwdModel.OriginalPwd), "* 原密码错误");
                return View();
            }// else password changed successfully

            return Redirect("~/Login/Logoff");
        }

        public ActionResult Verify()
        {
            // 已有账号登录重定向至主页
            if (ViewData[Key.HasLogin] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Verify(EmailModel emailModel)
        {
            if (emailModel.VerificationCode != Session[Key.Captcha]?.ToString())
            {
                ModelState.AddModelError(nameof(emailModel.VerificationCode), "* 验证码错误");
                return View();
            }
            else
            {
                // 将email存入session，在reset中读取
                Session.Add(Key.EmailAddress, emailModel.EmailAddress);
                return RedirectToAction("reset");
            }
        }

        public ActionResult Reset()
        {
            string Urlreferrer = Request.UrlReferrer?.LocalPath.ToLower();

            // Model验证PRG，显示错误信息
            if (Urlreferrer == "/password/reset") { return View(); }
            if (Urlreferrer != "/password/verify" || Session[Key.EmailAddress] is null)
            {
                return RedirectToAction("verify");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Reset(ResetPasswordModel resetPwdModel)
        {
            if (!passwordService.ResetPassword(Session[Key.EmailAddress].ToString(), resetPwdModel.Password))
            {
                ModelState.AddModelError(nameof(resetPwdModel.Password), "* 新密码不能和原密码相同");
                return View();
            }
            else { Session.Remove(Key.EmailAddress); }

            return RedirectToAction("Index", "login");
        }
    }
}
