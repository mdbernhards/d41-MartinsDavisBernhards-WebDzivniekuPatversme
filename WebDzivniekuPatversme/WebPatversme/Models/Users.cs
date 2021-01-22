using WebPatversme.Models.Database;

namespace WebPatversme.Models
{
    public class Users
    {
        private ShelterDatabaseContext context;

        public int UserID;

        public string Name;

        public string Surname;

        public string Password;

        public string Email;

        public int AccountType;

        public string PhoneNumber;

        //public int image; ??

        public int DateAdded; //DateTime
    }
}