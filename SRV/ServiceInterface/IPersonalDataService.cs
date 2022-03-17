using SRV.ViewModel;

namespace SRV.ServiceInterface
{
    public interface IPersonalDataService
    {
        PersonalDataModel Get(int userId);
        bool ValidateNicknameExists(int userId, string nickname);
        void Save(int userId, PersonalDataModel personalData);
    }
}
