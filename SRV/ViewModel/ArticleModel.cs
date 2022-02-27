using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV.ViewModel
{
    public class ArticleModel
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string Abstract { get { return (Body.Length > 256) ? Body.Substring(0, 255) : Body; } set { } }
        public RegisterModel Author { get; set; }
        public DateTime PublishTime { get; set; }
        public int Agree { get; set; }
        public int DisAgree { get; set; }
        public List<KeywordModel> Keywords { get; set; }
        public List<CommentModel> Comments { get; set; }
    }
}
