using AutoMapper;
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
using System.Web.ModelBinding;

namespace SRV.ProductionService
{
    public class RegisterService : BaseService, IRegisterService
    {
        private readonly UserRepository userRepository;

        public RegisterService()
        {
            userRepository = new UserRepository(dbContext);
        }

        public bool ValidateUserIsExists(string inviterName)
        {
            return !(userRepository.GetByName(inviterName) is null);
        }

        public bool ValidateInviterIsExists(string inviterName, out string invitationCode)
        {
            User inviter = userRepository.GetByName(inviterName);
            invitationCode = inviter?.InvitationCode;
            return !(inviter is null);
        }

        public int Register(RegisterModel model)
        {
            User newUser = mapper.Map<User>(model);
            newUser.Password = Utility.MD5Encrypt(newUser.Password);
            newUser.Inviter = userRepository.GetByName(model.Inviter);
            newUser.InvitationCode = Utility.GenerateRandomString(4);
            return userRepository.Save(newUser);
        }

        internal User Find(int id)
        {
            return userRepository.Find(id);
        }

        public RegisterModel GetUserById(int id)
        {
            return mapper.Map<RegisterModel>(userRepository.Find(id));
        }
    }
}
