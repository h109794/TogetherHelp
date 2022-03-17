using BLL.Entity;
using BLL.Repository;
using Global;
using SRV.ServiceInterface;
using SRV.ViewModel;

namespace SRV.ProductionService
{
    public class RegisterService : BaseService, IRegisterService
    {
        private readonly UserRepository userRepository;

        public RegisterService() => userRepository = new UserRepository(DbContext);

        public bool ValidateUserIsExists(string inviterName)
        {
            return !(userRepository.GetByUsername(inviterName) is null);
        }

        public bool ValidateInviterIsExists(string inviterName, out string invitationCode)
        {
            User inviter = userRepository.GetByNickname(inviterName);
            invitationCode = inviter?.InvitationCode;
            return !(inviter is null);
        }

        public int Register(RegisterModel model)
        {
            User newUser = Mapper.Map<User>(model);
            newUser.Password = Utility.MD5Encrypt(newUser.Password);
            newUser.Inviter = userRepository.GetByNickname(model.Inviter);
            newUser.InvitationCode = Utility.GenerateRandomString(4);
            newUser.PersonalData = new PersonalData() { Nickname = newUser.Username };
            return userRepository.Save(newUser);
        }

        internal User Find(int id)
        {
            return userRepository.Find(id);
        }

        public RegisterModel GetUserById(int id)
        {
            return Mapper.Map<RegisterModel>(userRepository.Find(id));
        }
    }
}
