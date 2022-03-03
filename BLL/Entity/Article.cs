using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entity
{
    [Table("article")]
    public class Article : Content
    {
        public Article()
        {
            Keywords = new List<Keyword>();
            Comments = new List<Comment>();
        }

        public string Title { get; set; }
        public string Abstract { get; set; }
        public List<Keyword> Keywords { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
