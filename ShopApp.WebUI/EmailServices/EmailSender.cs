using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace ShopApp.WebUI.EmailServices
{
    public class EmailSender : IEmailSender
    {
        private const string SendGrindKey = "aalsdamsdlasd_Asdasd-qWE__DFSVSD_FSD_FSD_FS_DF_SDF_";

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(SendGrindKey, subject, htmlMessage, email);
        }

        private Task Execute(string sendGrindKey, string subject, string message, string email)
        {
            var client = new SendGridClient(sendGrindKey);

            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@shopapp.com", "Shop App"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(email));
            return client.SendEmailAsync(msg);
        }
    }
}
