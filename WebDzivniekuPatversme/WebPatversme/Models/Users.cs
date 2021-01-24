using System;
using WebDzivniekuPatversme.Repository;

namespace WebPatversme.Models
{
    public class Users
    {
        private WebShelterDbContext context;

        public int UserID;

        public string Name;

        public string Surname;

        public string Password;

        public string Email;

        public int AccountType;

        public string PhoneNumber;

        public string ImagePath;

        public DateTime DateAdded;
    }
}