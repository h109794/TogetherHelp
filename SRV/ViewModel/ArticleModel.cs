using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace SRV.ViewModel
{
    public class ArticleModel
    {
        public int Id { get; private set; }
        [Required(ErrorMessage = "* 标题不能为空")]
        [MaxLength(64, ErrorMessage = "* 标题长度不能超过64位")]
        public string Title { get; set; }
        [Required(ErrorMessage = "* 正文不能为空")]
        [AllowHtml]
        public string Body { get; set; }
        [MaxLength(256, ErrorMessage = "* 摘要长度不能超过256位")]
        public string Abstract { get; set; }
        public string AuthorName { get; set; }
        public DateTime PublishTime { get; set; }
        public List<int> AgreeUserIds { get; set; }
        public List<int> DisagreeUserIds { get; set; }
        public List<KeywordModel> Keywords { get; set; }
        public List<CommentModel> Comments { get; set; }
        // 用于接收前台传来的关键字文本，在后台解析成集合属性
        [Required(ErrorMessage = "* 关键不能为空")]
        public string KeywordsReceiver { get; set; }
    }
}
