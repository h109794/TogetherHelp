using SRV.ViewModel;

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
