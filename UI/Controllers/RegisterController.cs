using Global;
using SRV.ProductionService;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Web;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    [ModelValidationFilter]
    public class RegisterController : Controller
    {
        public ActionResult Index()
        {
            // 如果已经登录重定向到主页
            if (ViewData[Key.HasLogin] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(RegisterModel model)
        {
            IRegisterService registerService = new RegisterService();

            if (!registerService.ValidateInviterIsExists(model.Inviter, out string invitationCode))
            {
                ModelState.AddModelError(nameof(model.Inviter), "* 邀请人不存在");
            }
            else if (invitationCode != model.InvitationCode)
            {
                ModelState.AddModelError(nameof(model.InvitationCode), "* 邀请码错误");
            }// else nothing

            if (registerService.ValidateUserIsExists(model.Username))
            {
                ModelState.AddModelError(nameof(model.Username), "* 用户名已存在");
            }

            if (model.Captcha.ToLower() != Session[Key.Captcha].ToString().ToLower())
            {
                ModelState.AddModelError(nameof(model.Captcha), "* 验证码错误");
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            int id = registerService.Register(model);
            HttpCookie cookie = new HttpCookie(Key.LoginInfo);
            cookie.Values.Add(Key.Id, id.ToString());
            cookie.Values.Add(Key.Pwd, Utility.MD5Encrypt(model.Password));
            Response.Cookies.Add(cookie);
            // 存放当前登录用户名
            Response.Cookies.Add(new HttpCookie(Key.Nickname, model.Username));

            // 跳转到未登录时的目标访问页面
            if (Request.Cookies[Key.TargetPageURL] != null)
            {
                Response.Cookies[Key.TargetPageURL].Expires = DateTime.Now.AddDays(-1);
                return Redirect(Request.Cookies[Key.TargetPageURL].Value);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
