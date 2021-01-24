using System;
using WebDzivniekuPatversme.Repository;

namespace WebPatversme.Models
{
    public class News
    {
        private WebShelterDbContext context;

        public int NewsID;

        public string Text;

        public DateTime DateCreated;

        public string ImagePath;

        public int FKUsersID;
    }
}