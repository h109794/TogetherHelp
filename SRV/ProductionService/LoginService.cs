using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRV.ProductionService
{
    public class LoginService : BaseService, ILoginService
    {
        private readonly UserRepository userRepository;

        public LoginService()
        {
            userRepository = new UserRepository(dbContext);
        }

        public LoginModel Authenticate(string username)
        {
            User user = userRepository.GetByName(username);
            return mapper.Map<LoginModel>(user);
        }
    }
}
