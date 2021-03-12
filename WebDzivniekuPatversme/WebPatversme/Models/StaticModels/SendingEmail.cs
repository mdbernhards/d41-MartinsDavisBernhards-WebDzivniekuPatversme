using WebDzivniekuPatversme.Services.Other;

namespace WebDzivniekuPatversme.Models.StaticModels
{
    public static class SendingEmail
    {
        public static readonly string Username = "web.patversme@gmail.com";

        public static readonly string Password = Decryption.Decrypt(System.IO.File.ReadAllLines("FileData/key.txt")[0]);

        public static readonly string Host = "smtp.gmail.com";

        public static readonly int Port = 587;

        public static readonly bool SSL = true;
    }
}