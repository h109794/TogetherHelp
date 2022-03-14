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
            PersonalData personalData = userRepository.GetPersonalData(userId);
            PersonalDataModel personalDataModel = Mapper.Map<PersonalDataModel>(personalData);

            personalDataModel.Birthday = personalData.Birthday is null ? default :
                ((DateTime)personalData.Birthday).ToString("yyyy-MM-dd");

            return personalDataModel;
        }

        public void Save(int userId, PersonalDataModel personalData)
        {
            PersonalData newPersonalData = Mapper.Map<PersonalData>(personalData);
            newPersonalData.User = userRepository.Find(userId);
            userRepository.SavePersonalData(newPersonalData);
        }
    }
}
