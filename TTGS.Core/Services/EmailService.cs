using TTGS.Shared.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net.Mail;
using System.Threading.Tasks;
using TTGS.Core.Interfaces;

namespace TTGS.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly IOptions<EmailSettings> _emailSettings;
        private readonly ILogger<EmailSettings> _logger;
        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailSettings> logger)
        {
            _emailSettings = emailSettings;
            _logger = logger;
        }

        public async Task<bool> SendEmailAsync(string fromEmail, string message, string fullName, string subject, string toEmail = null)
        {
            try
            {
                fromEmail = !string.IsNullOrWhiteSpace(fromEmail) ? fromEmail : _emailSettings.Value.ToEmail;
                toEmail = !string.IsNullOrWhiteSpace(toEmail) ? toEmail : _emailSettings.Value.ToEmail;

                using (var smtpClient = new SmtpClient(_emailSettings.Value.Server, _emailSettings.Value.Port))
                {
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(fromEmail)
                    };

                    mailMessage.Subject = subject;
                    mailMessage.To.Add(new MailAddress(toEmail, fullName));
                    mailMessage.Body = message;
                    mailMessage.IsBodyHtml = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential(_emailSettings.Value.Username, _emailSettings.Value.Password);
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot send email");
                return false;
            }

            return true;
        }
    }
}
