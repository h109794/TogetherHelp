using System.Collections.Generic;

namespace SRV.ViewModel
{
    public class ArticleListModel
    {
        public List<ArticleModel> Articles { get; set; }
        public int PageIndex { get; set; }
        public int PageCount { get; set; }
        public int CurrentUserId { get; set; }
        public string NavURLParam { get; set; }
    }
}
