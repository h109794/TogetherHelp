using SRV.ViewModel;

namespace SRV.ServiceInterface
{
    public interface IPersonalDataService
    {
        PersonalDataModel Get(int userId);
        void Save(int userId, PersonalDataModel personalData);
    }
}
