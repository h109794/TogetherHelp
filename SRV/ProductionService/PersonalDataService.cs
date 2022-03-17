using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;
using System;

namespace SRV.ProductionService
{
    public class PersonalDataService : BaseService, IPersonalDataService
    {
        private readonly UserRepository userRepository;

        public PersonalDataService() => userRepository = new UserRepository(DbContext);

        public PersonalDataModel Get(int userId)
        {
            User user = userRepository.Find(userId);
            PersonalDataModel personalDataModel = Mapper.Map<PersonalDataModel>(user.PersonalData);

            personalDataModel.Username = user.Username;
            personalDataModel.Birthday = user.PersonalData.Birthday is null ? default :
                ((DateTime)user.PersonalData.Birthday).ToString("yyyy-MM-dd");

            return personalDataModel;
        }

        public bool ValidateNicknameExists(int userId, string nickname)
        {
            User user = userRepository.GetByNickname(nickname);
            return (user != null && user.Id != userId);
        }

        public void Save(int userId, PersonalDataModel personalData)
        {
            PersonalData newPersonalData = Mapper.Map<PersonalData>(personalData);
            User user = userRepository.Find(userId);
            newPersonalData.User = user;
            userRepository.SavePersonalData(newPersonalData);
        }
    }
}
