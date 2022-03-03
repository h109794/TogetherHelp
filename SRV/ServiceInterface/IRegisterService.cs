using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV.ServiceInterface
{
    public interface IRegisterService
    {
        bool ValidateUserIsExists(string inviterName);
        bool ValidateInviterIsExists(string inviterName, out string invitationCode);
        int Register(RegisterModel model);
        RegisterModel GetUserById(int id);
    }
}
