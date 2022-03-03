using BLL.Entity;
using BLL.Repository;
using Global;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV.ProductionService
{
    public class PasswordService : BaseService, IPasswordService
    {
        private readonly UserRepository userRepository;

        public PasswordService()
        {
            userRepository = new UserRepository(dbContext);
        }

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
