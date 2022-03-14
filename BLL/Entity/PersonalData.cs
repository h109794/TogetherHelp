using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Entity
{
    [Table("personal_data")]
    public class PersonalData : BaseEntity
    {
        public User User { get; set; }
        // true:man false:woman
        public bool? Gender { get; set; }
        public string Nickname { get; set; }
        public DateTime? Birthday { get; set; }
        public string Description { get; set; }
    }
}
