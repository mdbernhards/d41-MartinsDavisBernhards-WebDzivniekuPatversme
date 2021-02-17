using System;
using Microsoft.AspNetCore.Http;

namespace WebDzivniekuPatversme.Models
{
    public class News
    {
        public string NewsID { set; get; }

        public string Text { set; get; }

        public DateTime DateAdded { set; get; }

        public string ImagePath { set; get; }

        public IFormFile Image { set; get; }

        public string Title { set; get; }

        public string UserID { set; get; }
    }
}