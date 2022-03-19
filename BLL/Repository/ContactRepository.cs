using BLL.Entity;

namespace BLL.Repository
{
    public class ContactRepository : BaseRepository<Contact>
    {
        public ContactRepository(SqlDbContext sqlDbContext) : base(sqlDbContext) { }

        public Contact Get(int id)
        {
            return sqlDbContext.Contacts.Find(id);
        }
    }
}
