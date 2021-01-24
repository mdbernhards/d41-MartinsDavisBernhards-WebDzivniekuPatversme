namespace WebPatversme.Models
{
    public class AccountAccess
    {
        public int AccountAccessID { set; get; }

        public int FKUsersID { set; get; }

        public int FKAnimalSheltersID { set; get; }
    }
}