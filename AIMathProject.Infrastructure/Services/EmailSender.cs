using AIMathProject.Application.Abstracts;
using AIMathProject.Application.Dto;
using AIMathProject.Infrastructure.Options;
using System.Net;
using System.Net.Mail;

namespace AIMathProject.Infrastructure.Services
{
    public class EmailSender : IEmailHelper
    {
        private readonly EmailConfiguration _emailConfig;


        public EmailSender(EmailConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public async Task SendEmailAsync(EmailRequest emailRequest, CancellationToken cancellationToken)
        {
            try
            {
                using var smtpClient = new SmtpClient(_emailConfig.Provider, _emailConfig.Port)
                {
                    Credentials = new NetworkCredential(_emailConfig.DefaultSender, _emailConfig.Password),
                    EnableSsl = true
                };

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(_emailConfig.DefaultSender),
                    Subject = emailRequest.Subject,
                    Body = emailRequest.Content,
                    IsBodyHtml = true 
                };
                mailMessage.To.Add(emailRequest.To);

                if (emailRequest.AttachmentFilePaths != null && emailRequest.AttachmentFilePaths.Length > 0)
                {
                    foreach (var path in emailRequest.AttachmentFilePaths)
                    {
                        if (!string.IsNullOrEmpty(path))
                        {
                            var attachment = new Attachment(path);
                            mailMessage.Attachments.Add(attachment);
                        }
                    }
                }

                await smtpClient.SendMailAsync(mailMessage, cancellationToken);
            }
            catch (SmtpException ex)
            {
                throw new Exception($"SMTP error occurred: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while sending email: {ex.Message}", ex);
            }
        }
    }
}
