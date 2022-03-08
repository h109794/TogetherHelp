using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entity
{
    [Table("comment")]
    public class Comment : Content
    {
        public List<Comment> Replys { get; set; }
        public string ReplyUsername { get; set; }
    }
}
