using WebDzivniekuPatversme.Data;
using WebDzivniekuPatversme.Repositories.Interfaces;

namespace WebDzivniekuPatversme.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly WebShelterDbContext _dbcontext;

        public HomeRepository(
            WebShelterDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
    }
}