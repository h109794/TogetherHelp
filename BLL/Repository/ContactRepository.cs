using BLL.Entity;
using System.Linq;

namespace BLL.Repository
{
    public class ContactRepository : BaseRepository<Contact>
    {
        public ContactRepository(SqlDbContext sqlDbContext) : base(sqlDbContext) { }

        public Contact Get(int id)
        {
            return sqlDbContext.Contacts.Find(id);
        }

        public Contact GetByEmail(string email)
        {
            return sqlDbContext.Contacts.Where(c => c.EmailAddress == email).SingleOrDefault();
        }
    }
}
