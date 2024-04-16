using System.Net.Mail;
using System.Net;

namespace EcommerceBackend.Services
{
    public class EmailServices
    {
        public void Send(string email, string subject, string body)
        {
            var mailMessage = new MailMessage("ravirawat0@outlook.com", email, subject, body);
            var smtpClient = new SmtpClient("smtp-mail.outlook.com", 587)
            {
                Credentials = new NetworkCredential("ravirawat0@outlook.com", "Alpha@9859"),
                EnableSsl = true
            };
            smtpClient.Send(mailMessage);
        }
    }
}
