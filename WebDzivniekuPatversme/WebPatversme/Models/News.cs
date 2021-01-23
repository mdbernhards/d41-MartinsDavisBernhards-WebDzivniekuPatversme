using System;
using WebPatversme.Models.Database;

namespace WebPatversme.Models
{
    public class News
    {
        private ShelterRepository context;

        public int NewsID;

        public string Text;

        public DateTime DateCreated;

        public string ImagePath;

        public int FKUsersID;
    }
}