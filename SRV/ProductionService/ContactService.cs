using BLL.Entity;
using BLL.Repository;
using SRV.ServiceInterface;
using SRV.ViewModel;

namespace SRV.ProductionService
{
    public class ContactService : BaseService, IContactService
    {
        private readonly ContactRepository contactRepository;

        public ContactService() => contactRepository = new ContactRepository(DbContext);

        public EmailModel GetEmail(int id)
        {
            Contact contact = contactRepository.Get(id);
            return Mapper.Map<EmailModel>(contact);
        }

        public void BindEmail(int id, string emailAddress)
        {
            Contact contact = contactRepository.Get(id);

            if (contact is null)
            {
                contact = new Contact() { Id = id, EmailAddress = emailAddress };
                contactRepository.Save(contact);
            }
            else { contact.EmailAddress = emailAddress; }
        }

        public bool ValidateEmailActivation(int id, string emailAddress)
        {
            string email = contactRepository.Get(id)?.EmailAddress;
            return email == emailAddress;
        }
    }
}
