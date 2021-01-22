using WebPatversme.Models.Database;

namespace WebPatversme.Models
{
    public class AccountAccess
    {
        private ShelterDatabaseContext context;

        public int AccountAccessID;

        public int FKUsersID;

        public int FKAnimalSheltersID;
    }
}