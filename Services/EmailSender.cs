using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace JoyBoxPlatform.Services
{
    public class EmailSender : IEmailSender
    {
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(
                    Environment.GetEnvironmentVariable("EMAIL_USER"),
                    Environment.GetEnvironmentVariable("EMAIL_PASSWORD")
                ),
                EnableSsl = true
            };

            var mail = new MailMessage(
                Environment.GetEnvironmentVariable("EMAIL_USER")!,
                email,
                subject,
                htmlMessage
            )
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mail);
        }
    }
}
