using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;
using System.Web;

namespace SRV.ProductionService
{
    public class PersonalDataService : BaseService, IPersonalDataService
    {
        private readonly UserRepository userRepository;

        public PersonalDataService() => userRepository = new UserRepository(DbContext);

        public void ChangeProfile(int userId, HttpPostedFileBase profile)
        {
            User user = userRepository.Find(userId);
            user.PersonalData.Profile = new byte[profile.ContentLength];
            try
            {
                profile.InputStream.Read(user.PersonalData.Profile, 0, profile.ContentLength);
            }
            finally { profile.InputStream.Dispose(); }
        }

        public PersonalDataModel Get(int userId)
        {
            User user = userRepository.Find(userId);
            PersonalDataModel personalDataModel = Mapper.Map<PersonalDataModel>(user.PersonalData);

            personalDataModel.Username = user.Username;
            personalDataModel.Birthday = user.PersonalData.Birthday is null ? default :
                ((DateTime)user.PersonalData.Birthday).ToString("yyyy-MM-dd");

            return personalDataModel;
        }

        public void Save(int userId, PersonalDataModel personalData)
        {
            PersonalData newPersonalData = Mapper.Map<PersonalData>(personalData);
            User user = userRepository.Find(userId);
            newPersonalData.User = user;
            userRepository.SavePersonalData(newPersonalData);
        }

        public bool ValidateNicknameExists(int userId, string nickname)
        {
            User user = userRepository.GetByNickname(nickname);
            return (user != null && user.Id != userId);
        }
    }
}
