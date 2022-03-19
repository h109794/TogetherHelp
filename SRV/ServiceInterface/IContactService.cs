using SRV.ViewModel;

namespace SRV.ServiceInterface
{
    public interface IContactService
    {
        EmailModel GetEmail(int id);
        void BindEmail(int id, string emailAddress);
        bool ValidateEmailActivation(int id, string emailAddress);
    }
}
