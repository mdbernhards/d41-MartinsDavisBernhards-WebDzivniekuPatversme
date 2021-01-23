using WebPatversme.Models.Database;

namespace WebPatversme.Models
{
    public class AccountAccess
    {
        private ShelterServices context;

        public int AccountAccessID;

        public int FKUsersID;

        public int FKAnimalSheltersID;
    }
}