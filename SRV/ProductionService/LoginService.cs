using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;

namespace SRV.ProductionService
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly UserRepository userRepository;

        public LoginService() => userRepository = new UserRepository(DbContext);

        public LoginModel Authenticate(string username)
        {
            User user = userRepository.GetByName(username);
            return Mapper.Map<LoginModel>(user);
        }
    }
}
