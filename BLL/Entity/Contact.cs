using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Entity
{
    [Table("contact")]
    public class Contact : BaseEntity
    {
        public User User { get; set; }
        public string EmailAddress { get; set; }
    }
}
