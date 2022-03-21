using System.ComponentModel.DataAnnotations;

namespace SRV.ViewModel
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "* 新密码不能为空")]
        [MinLength(4, ErrorMessage = "* 密码长度不能小于4位")]
        public string Password { get; set; }
        [Required(ErrorMessage = "* 确认密码不能为空")]
        [Compare(nameof(Password), ErrorMessage = "* 确认密码和新密码不一致")]
        public string ConfirmPassword { get; set; }
    }
}
