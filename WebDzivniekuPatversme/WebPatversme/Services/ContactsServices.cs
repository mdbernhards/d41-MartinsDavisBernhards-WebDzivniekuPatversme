using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebPatversme.Models.Database
{
    public class ContactsServices : IContactsServices
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsServices(
            IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }
    }
}