using System.ComponentModel.DataAnnotations.Schema;

namespace BLL.Entity
{
    [Table("user")]
    public class User : BaseEntity
    {
        public User Inviter { get; set; }
        public string InvitationCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Contact Contact { get; set; }
        public PersonalData PersonalData { get; set; }
    }
}
