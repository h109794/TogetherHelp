using System.ComponentModel.DataAnnotations;

namespace SRV.ViewModel
{
    public class PersonalDataModel
    {
        public byte[] Profile { get; set; }
        public string Username { get; set; }
        // true:man, false:woman
        public bool? Gender { get; set; }
        [Required(ErrorMessage = "* 昵称不能为空")]
        [StringLength(16, MinimumLength = 2, ErrorMessage = "* 昵称长度限制为2-16个字符")]
        public string Nickname { get; set; }
        public string Birthday { get; set; }
        [MaxLength(256, ErrorMessage = "* 个人简介不能超过256位")]
        public string Description { get; set; }
    }
}
