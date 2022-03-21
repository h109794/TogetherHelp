using SRV.ViewModel;

namespace SRV.ServiceInterface
{
    public interface IContactService
    {
        EmailModel GetEmail(int id);
        void BindEmail(int id, string emailAddress);
        bool ValidateEmailExists(string emailAddress);
        string GetEmailUserNickname(string emailAddress);
    }
}
