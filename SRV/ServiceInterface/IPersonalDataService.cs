using SRV.ViewModel;
using System.Web;

namespace SRV.ServiceInterface
{
    public interface IPersonalDataService
    {
        void ChangeProfile(int userId, HttpPostedFileBase profile);
        PersonalDataModel Get(int userId);
        void Save(int userId, PersonalDataModel personalData);
        bool ValidateNicknameExists(int userId, string nickname);
    }
}
