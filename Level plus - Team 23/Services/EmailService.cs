using static Level_plus___Team_23.Services.EmailService;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;
using Level_plus___Team_23.Models;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

namespace Level_plus___Team_23.Services
{
    public class EmailService
    {

        public EmailService() {}

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {
            EmailSettings settings = createEmailSettings();
            string subject = "subject placeholder";
            string content = "content placeholder";

            var mimeMessage = new MimeMessage
            {
                Sender = new MailboxAddress(settings.SendersName, settings.SmtpUserName),
                Subject = subject
            };

            mimeMessage.From.Add(new MailboxAddress(settings.EmailDisplayName, settings.SmtpUserName));

            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = content };

            mimeMessage.To.Add(new MailboxAddress(emailMessage.MailTo, emailMessage.MailTo));

            try
            {
                using (var smtp = new MailKit.Net.Smtp.SmtpClient())
                 {
                   var socketOptions = settings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.Auto;

                   await smtp.ConnectAsync(settings.SmtpServer, settings.SmtpServerPort, socketOptions);

                    if (!string.IsNullOrEmpty(settings.SmtpUserName))
                    {
                       await smtp.AuthenticateAsync(settings.SmtpUserName, settings.SmtpPassword);
                    }

                   await smtp.SendAsync(mimeMessage);

                   await smtp.DisconnectAsync(true);
                }
            }
            catch (SmtpException ex)
            {
                throw ex;
            }
        }

        private EmailSettings createEmailSettings()
        {
            return new EmailSettings
            {
                SmtpServer = "smtp.gmail.com",
                SmtpUserName = "level.plus.team23@gmail.com",
                SmtpPassword = "hxqommnlketduqhx",
                SmtpServerPort = 587,
                EnableSsl = true,
                EmailDisplayName = "test",
                SendersName = "test"
            };
        }
    }
}
