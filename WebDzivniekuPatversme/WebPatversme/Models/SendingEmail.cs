using WebDzivniekuPatversme.Services;

namespace WebDzivniekuPatversme.Models
{
    public class SendingEmail
    {
        public static string Username = "web.patversme@gmail.com";

        public static string Password = Decryption.Decrypt(System.IO.File.ReadAllLines("FileData/key.txt")[0]);

        public static string Host = "smtp.gmail.com";

        public static int Port = 587;

        public static bool SSL = true;
    }
}