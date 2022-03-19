using BLL.Entity;
using System.Linq;
using System.Data.Entity;

namespace BLL.Repository
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(SqlDbContext sqlContext) : base(sqlContext) { }

        public User GetByUsername(string username)
        {
            return sqlDbContext.Users.Where(m => m.Username == username).Include(m => m.PersonalData).SingleOrDefault();
        }

        public User GetByNickname(string nicknama)
        {
            return sqlDbContext.Users.Where(m => m.PersonalData.Nickname == nicknama).SingleOrDefault();
        }

        public override User Find(int id)
        {
            return sqlDbContext.Users.Where(m => m.Id == id).Include(m => m.PersonalData).SingleOrDefault();
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
