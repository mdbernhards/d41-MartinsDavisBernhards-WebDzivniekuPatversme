using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Repositories.Interfaces;

namespace WebDzivniekuPatversme.Repositories
{
    public class ContactsRepository : IContactsRepository
    {
        private readonly WebShelterDbContext _dbcontext;

        public ContactsRepository(
            WebShelterDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
    }
}