using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entity
{
    [Table("keyword")]
    public class Keyword : Entity
    {
        public string Text { get; set; }
        public int UseCount { get; set; }
        public Keyword UpperKeyword { get; set; }
        public List<Article> BelongTo { get; set; }
    }
}
