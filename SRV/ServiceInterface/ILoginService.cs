using SRV.ViewModel;

namespace SRV.ServiceInterface
{
    public interface ILoginService
    {
        LoginModel Authenticate(string username);
    }
}
