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

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            HttpCookie cookie = new HttpCookie(Key.LoginInfo);
            cookie.Values.Add(Key.Id, loginModel.Id.ToString());
            cookie.Values.Add(Key.Pwd, loginModel.Password);
            cookie.Expires = model.RememberMe is true ? DateTime.Now.AddDays(14) : default;
            Response.Cookies.Add(cookie);

            return RedirectToAction("Index", "Home");
        }
    }
}
