using SRV.ViewModel;

namespace SRV.ServiceInterface
{
    public interface IPasswordService
    {
        bool ChangePassword(int userId, ChangePasswordModel pwdModel);
        bool ResetPassword(string email, string password);
    }
}
