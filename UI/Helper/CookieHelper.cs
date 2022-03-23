using Global;
using SRV.ProductionService;
using System;
using System.Web;

namespace UI.Helper
{
    public class CookieHelper
    {
        public static int GetCurrentUserId()
        {
            var registerService = new RegisterService();
            var userInfo = HttpContext.Current.Request.Cookies[Key.LoginInfo];

            if (userInfo is null)
            {
                throw new NullReferenceException("UserInfo cookie does not exist.");
            }
            else if (!int.TryParse(userInfo[Key.Id], out int id))
            {
                throw new ArgumentException();
            }
            else if (registerService.GetUserById(id) is null)
            {
                throw new ArgumentException("The user id does not exist.");
            }
            else if (userInfo[Key.Pwd] != registerService.GetUserById(id).Password)
            {
                throw new ArgumentException("Id and password dono't match.");
            }// else return

            return Convert.ToInt32(userInfo[Key.Id]);
        }
    }
}
