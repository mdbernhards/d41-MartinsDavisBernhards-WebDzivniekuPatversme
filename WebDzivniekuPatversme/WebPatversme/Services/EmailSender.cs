using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using WebDzivniekuPatversme.Models.StaticModels;

namespace WebDzivniekuPatversme.Services
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string body)
        {
            return Execute(subject, body, email);
        }

        public async Task Execute(string subject, string body, string email)
        {
            SmtpClient smtp = new SmtpClient
            {
                Host = SendingEmail.Host,
                Port = SendingEmail.Port,
                EnableSsl = SendingEmail.SSL,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(SendingEmail.Username, SendingEmail.Password),
            };

            using var message = new MailMessage(SendingEmail.Username, email)
            {
                Subject = subject,
                Body = body,
            };

            message.IsBodyHtml = true;

            smtp.Send(message);
            smtp.Dispose();
        }
    }
}