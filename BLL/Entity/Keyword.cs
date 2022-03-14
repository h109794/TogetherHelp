using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Entity
{
    [Table("keyword")]
    public class Keyword : BaseEntity
    {
        public string Text { get; set; }
        public int UseCount { get; set; }
        public Keyword UpperKeyword { get; set; }
        public List<Article> BelongTo { get; set; }
    }
}
