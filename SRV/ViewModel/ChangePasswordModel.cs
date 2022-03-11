using System.ComponentModel.DataAnnotations;

namespace SRV.ViewModel
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "* 原密码不能为空")]
        [MinLength(4, ErrorMessage = "* 原密码错误")]
        public string OriginalPwd { get; set; }
        [Required(ErrorMessage = "* 新密码不能为空")]
        [MinLength(4, ErrorMessage = "* 密码长度不能小于4位")]
        public string NewPwd { get; set; }
        [Required(ErrorMessage = "* 确认密码不能为空")]
        [Compare(nameof(NewPwd), ErrorMessage = "* 确认密码和新密码不一致")]
        public string ConfirmNewPwd { get; set; }
    }
}
