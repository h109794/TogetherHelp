using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Entity
{
    [Table("comment")]
    public class Comment : Content
    {
        public Comment() => Replys = new List<Comment>();

        public List<Comment> Replys { get; set; }
        public string ReplyUsername { get; set; }
    }
}
