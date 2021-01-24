using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebDzivniekuPatversme.Repository
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