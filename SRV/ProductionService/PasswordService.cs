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

        public bool ChangePassword(int userId, ChangePasswordModel changePwdModel)
        {
            User user = userRepository.Find(userId);

            if (user.Password != Utility.MD5Encrypt(changePwdModel.OriginalPwd))
            {
                return false;
            }
            else
            {
                user.Password = Utility.MD5Encrypt(changePwdModel.NewPwd);
                return true;
            }
        }

        public bool ResetPassword(string email, string password)
        {
            int userId = new ContactRepository(DbContext).GetByEmail(email).Id;
            User user = userRepository.Find(userId);

            if (user.Password == Utility.MD5Encrypt(password))
            {
                return false;
            }
            else
            {
                user.Password = Utility.MD5Encrypt(password);
                return true;
            }
        }
    }
}
