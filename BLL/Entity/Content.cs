using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entity
{
    [Table("content")]
    public class Content : Entity
    {
        public User Author { get; set; }
        public string Body { get; set; }
        public int Agree { get; set; }
        public int DisAgree { get; set; }
        public DateTime PublishTime { get; set; }
    }
}
