using WebDzivniekuPatversme.Services.Interfaces;
using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebDzivniekuPatversme.Services
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;

        public ContactsService(
            IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }
    }
}