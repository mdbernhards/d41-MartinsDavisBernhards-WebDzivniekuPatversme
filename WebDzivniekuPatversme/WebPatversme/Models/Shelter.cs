using System;
using Microsoft.AspNetCore.Http;

namespace WebDzivniekuPatversme.Models
{
    public class Shelter
    {
        public string Id { set; get; }

        public string Name { set; get; }

        public string Email { set; get; }

        public string Description { set; get; }

        public string Address { set; get; }

        public string PhoneNumber { set; get; }

        public int AnimalCapacity { set; get; }

        public DateTime DateAdded { set; get; }

        public string ImagePath { set; get; }

        public IFormFile Image { set; get; }
    }
}