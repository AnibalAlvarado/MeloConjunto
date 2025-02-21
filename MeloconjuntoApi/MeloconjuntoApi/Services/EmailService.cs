using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using MailKit.Security;

namespace MeloconjuntoApi.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task EnviarEmailAsync(string correo, string subjectemail, string bodyemail)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_configuration["EmailSettings:From"]));
            email.To.Add(MailboxAddress.Parse(correo));
            email.Subject = subjectemail;

            var builder = new BodyBuilder();
            builder.TextBody = bodyemail;

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration["EmailSettings:SmtpServer"],
                                    int.Parse(_configuration["EmailSettings:SmtpPort"]),
                                    SecureSocketOptions.StartTls);

            await smtp.AuthenticateAsync(_configuration["EmailSettings:SmtpUsername"],
                                         _configuration["EmailSettings:SmtpPassword"]);

            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

    }
}
