using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entity
{
    public class User : Entity
    {
        public User Inviter { get; set; }
        public string InvitationCode { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
