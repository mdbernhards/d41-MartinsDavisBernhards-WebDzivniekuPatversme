using System;

namespace WebDzivniekuPatversme.Models
{
    public class News
    {
        public string NewsID { set; get; }

        public string Text { set; get; }

        public DateTime DateCreated { set; get; }

        public string ImagePath { set; get; }
    }
}