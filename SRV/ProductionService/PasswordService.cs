using BLL.Entity;
using BLL.Repository;
using Global;
using SRV.ServiceInterface;
using SRV.ViewModel;

namespace SRV.ProductionService
{
    public class PasswordService : BaseService, IPasswordService
    {
        private readonly UserRepository userRepository;

        public PasswordService() => userRepository = new UserRepository(DbContext);

        public bool ChangePassword(int userId, ChangePasswordModel pwdModel)
        {
            User user = userRepository.Find(userId);

            if (user.Password != Utility.MD5Encrypt(pwdModel.OriginalPwd))
            {
                return false;
            }
            else
            {
                user.Password = Utility.MD5Encrypt(pwdModel.NewPwd);
                return true;
            }
        }
    }
}
