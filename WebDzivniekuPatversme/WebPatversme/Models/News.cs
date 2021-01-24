using System;

namespace WebPatversme.Models
{
    public class News
    {
        public int NewsID { set; get; }

        public string Text { set; get; }

        public DateTime DateCreated { set; get; }

        public string ImagePath { set; get; }

        public int FKUsersID { set; get; }
    }
}