using BLL.Entity;
using BLL.Repository;

namespace QuickDb
{
    static class ContactFactory
    {
        private static readonly SqlDbContext sqlDbContext;

        static ContactFactory() => sqlDbContext = SqlDbContext.GetInstance();

        internal static void GenerateContacts()
        {
            Contact contact = new Contact
            {
                User = UserFactory.Admin,
                EmailAddress = "979482093@qq.com",
            };
            sqlDbContext.Contacts.Add(contact);
        }
    }
}
