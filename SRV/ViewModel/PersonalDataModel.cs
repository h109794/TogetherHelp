using System;
using System.ComponentModel.DataAnnotations;

namespace SRV.ViewModel
{
    public class PersonalDataModel
    {
        // true:man false:woman
        public bool? Gender { get; set; }
        [MaxLength(32, ErrorMessage = "* 用户名长度不能超过32位")]
        public string Nickname { get; set; }
        public string Birthday { get; set; }
        [MaxLength(256, ErrorMessage = "* 个人简介不能超过256位")]
        public string Description { get; set; }
    }
}
