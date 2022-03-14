using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Entity
{
    [Table("content")]
    public class Content : BaseEntity
    {
        public User Author { get; set; }
        public string Body { get; set; }
        public int Agree { get; set; }
        public int DisAgree { get; set; }
        public DateTime PublishTime { get; set; }
    }
}
