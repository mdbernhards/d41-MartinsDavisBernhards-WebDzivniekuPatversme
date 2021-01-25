using WebDzivniekuPatversme.Repository.Interfaces;

namespace WebDzivniekuPatversme.Repository
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