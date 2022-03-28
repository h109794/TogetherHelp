using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Entity
{
    [Table("content")]
    public class Content : BaseEntity
    {
        public User Author { get; set; }
        public string Body { get; set; }
        public DateTime PublishTime { get; set; }
        public List<Evaluation> Evaluations { get; set; }
    }
}
