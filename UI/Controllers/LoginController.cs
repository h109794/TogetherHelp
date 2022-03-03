using Global;
using SRV.ProductionService;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UI.Filters;

namespace UI.Controllers
{
    [ModelValidationFilter]
    public class LoginController : Controller
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
        public ActionResult Index(LoginModel model)
        {
            ILoginService loginService = new LoginService();
            LoginModel loginModel = loginService.Authenticate(model.Username);

            if (loginModel.Username is null)
            {
                ModelState.AddModelError(nameof(model.Username), "* 用户名不存在");
            }
            else if (loginModel.Password != Utility.MD5Encrypt(model.Password))
            {
                ModelState.AddModelError(nameof(model.Password), "* 用户名或密码错误");
            }// else nothing

            if (model.Captcha.ToLower() != Session[Key.Captcha].ToString().ToLower())
            {
                ModelState.AddModelError(nameof(model.Captcha), "* 验证码错误");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HttpCookie loginCookie = new HttpCookie(Key.LoginInfo);
            HttpCookie usernameCookie = new HttpCookie(Key.Username, model.Username);
            loginCookie.Values.Add(Key.Id, loginModel.Id.ToString());
            loginCookie.Values.Add(Key.Pwd, loginModel.Password);
            usernameCookie.Expires = loginCookie.Expires = model.RememberMe is true ? DateTime.Now.AddDays(14) : default;
            Response.Cookies.Add(loginCookie);
            Response.Cookies.Add(usernameCookie);

            // 跳转到未登录时的目标访问页面
            if (Request.Cookies[Key.TargetPageURL] != null)
            {
                Response.Cookies[Key.TargetPageURL].Expires = DateTime.Now.AddDays(-1);
                return Redirect(Request.Cookies[Key.TargetPageURL].Value);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logoff()
        {
            Response.Cookies[Key.LoginInfo].Expires = DateTime.Now.AddDays(-1);
            Response.Cookies[Key.Username].Expires = DateTime.Now.AddDays(-1);

            return Redirect(Request.UrlReferrer.ToString() + "?logoff");
        }
    }
}
