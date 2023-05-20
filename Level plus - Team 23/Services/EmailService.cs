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
        private PdfService pdfService;

        public EmailService() {
        pdfService = new PdfService();
        }

        public async Task SendEmailAsync(EmailMessage emailMessage)
        {

            var attachment = pdfService.createPdfCertificateAttachment(emailMessage.course);

            EmailSettings settings = createEmailSettings();
            string subject = "Certificate Confirmation";
            string content = createEmailContent(emailMessage.course);

            var mimeMessage = new MimeMessage
            {
                Sender = new MailboxAddress(settings.SendersName, settings.SmtpUserName),
                Subject = subject
            };

            mimeMessage.From.Add(new MailboxAddress(settings.EmailDisplayName, settings.SmtpUserName));

            var builder = new BodyBuilder();

            builder.TextBody = content;

            builder.Attachments.Add(attachment);

            mimeMessage.Body = builder.ToMessageBody();

            mimeMessage.To.Add(new MailboxAddress(emailMessage.mailAddress, emailMessage.mailAddress));

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
                EmailDisplayName = "level_plus",
                SendersName = "level_plus"
            };
        }

        private string createEmailContent(Course course)
        {
            return $@"Dear {course.Student.Name},

We are pleased to inform you that you have successfully completed the {course.Title} course.

As a token of recognition for your hard work and dedication, we are delighted to attach your official certificate of completion to this email. The certificate acknowledges your commitment to enhancing your knowledge and skills in the field of {course.Title}, and serves as evidence of your accomplishment in completing this course.
We would like to extend our heartfelt congratulations to you for your remarkable achievement. Your dedication and effort in successfully completing the program are commendable, and we are confident that this accomplishment will greatly contribute to your personal and professional growth.

Thank you for your participation and dedication to the {course.Title} course. We wish you all the best in your future endeavors.
Sincerely,
Level Plus.";
        }
    }
}
