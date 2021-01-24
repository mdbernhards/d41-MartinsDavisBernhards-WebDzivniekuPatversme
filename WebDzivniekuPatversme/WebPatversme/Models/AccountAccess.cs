using WebDzivniekuPatversme.Repository;

namespace WebPatversme.Models
{
    public class AccountAccess
    {
        private WebShelterDbContext context;

        public int AccountAccessID;

        public int FKUsersID;

        public int FKAnimalSheltersID;
    }
}