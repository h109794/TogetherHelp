using System.ComponentModel.DataAnnotations;

namespace SRV.ViewModel
{
    public class EmailModel
    {
        [Required(ErrorMessage = "* 邮箱地址不能为空")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "* 验证码不能为空")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "* 验证码错误")]
        public string VerificationCode { get; set; }
    }
}
