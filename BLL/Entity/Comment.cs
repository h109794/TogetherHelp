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
        public Comment Belong { get; set; }
    }
}
