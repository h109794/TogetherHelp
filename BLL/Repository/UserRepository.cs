using BLL.Entity;
using System.Linq;

namespace BLL.Repository
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(SqlDbContext sqlContext) : base(sqlContext) { }

        public User GetByName(string name)
        {
            return sqlDbContext.Users.Where(m => m.Username == name).SingleOrDefault();
        }

        public PersonalData GetPersonalData(int id)
        {
            return sqlDbContext.PersonalDatas.Find(id);
        }

        public void SavePersonalData(PersonalData newPersonalData)
        {
            PersonalData personalData = sqlDbContext.PersonalDatas.Find(newPersonalData.User.Id);

            personalData.Nickname = newPersonalData.Nickname;
            personalData.Gender = newPersonalData.Gender;
            personalData.Birthday = newPersonalData.Birthday;
            personalData.Description = newPersonalData.Description;
        }
    }
}
