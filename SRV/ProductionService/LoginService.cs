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
            User user = userRepository.GetByUsername(username);
            LoginModel loginModel = Mapper.Map<LoginModel>(user);
            // 用于判断当前用户是否有头像，从而决定导航栏是否需要从数据库加载头像
            loginModel.HasProfile = !(user.PersonalData.Profile is null);
            return loginModel;
        }
    }
}
