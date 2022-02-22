using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV.ViewModel
{
    public class LoginModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "* 用户名不能为空")]
        [MaxLength(32, ErrorMessage = "* 用户名长度不能超过32位")]
        public string Username { get; set; }
        [Required(ErrorMessage = "* 密码不能为空")]
        [MinLength(4, ErrorMessage = "* 密码长度不能小于4位")]
        public string Password { get; set; }
        [Required(ErrorMessage = "* 验证码不能为空")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "* 验证码格式错误")]
        public string Captcha { get; set; }
        public bool RememberMe { get; set; }
    }
}
