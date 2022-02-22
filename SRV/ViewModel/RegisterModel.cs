using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SRV.ViewModel
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "* 邀请人不能为空")]
        public string Inviter { get; set; }
        [Required(ErrorMessage = "* 邀请码不能为空")]
        public string InvitationCode { get; set; }
        [Required(ErrorMessage = "* 用户名不能为空")]
        [MaxLength(32, ErrorMessage = "* 用户名长度不能超过32位")]
        public string Username { get; set; }
        [Required(ErrorMessage = "* 密码不能为空")]
        [MinLength(4, ErrorMessage = "* 密码长度不能小于4位")]
        public string Password { get; set; }
        [Required(ErrorMessage = "* 确认密码不能为空")]
        [Compare(nameof(Password), ErrorMessage = "* 确认密码和密码不一致")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "* 验证码不能为空")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "* 验证码格式错误")]
        public string Captcha { get; set; }
    }
}
