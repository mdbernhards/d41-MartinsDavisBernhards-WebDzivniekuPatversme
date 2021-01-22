using WebPatversme.Models.Database;

namespace WebPatversme.Models
{
    public class News
    {
        private ShelterDatabaseContext context;

        public int NewsID;

        public string Text;

        public string DateCreated; //DateTime

        //public int image; ??

        public int FKUsersID;
    }
}