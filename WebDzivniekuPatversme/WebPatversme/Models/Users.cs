using System;

namespace WebPatversme.Models
{
    public class Users
    {
        public int UserID { set; get; }

        public string Name { set; get; }

        public string Surname { set; get; }

        public string Password { set; get; }

        public string Email { set; get; }

        public int AccountType { set; get; }

        public string PhoneNumber { set; get; }

        public string ImagePath { set; get; }

        public DateTime DateAdded { set; get; }
    }
}