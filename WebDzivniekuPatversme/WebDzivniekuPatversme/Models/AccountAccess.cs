using WebDzivniekuPatversme.Models.Database;

namespace WebDzivniekuPatversme.Models
{
    public class AccountAccess
    {
        private ShelterDatabaseContext context;

        public int AccountAccessID;

        public int FKUsersID;

        public int FKAnimalSheltersID;
    }
}