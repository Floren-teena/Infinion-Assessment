using Florentina_Infinion_Assessment.Application.DTOs;
using Microsoft.Extensions.Configuration;
using MimeKit;
using Florentina_Infinion_Assessment.Application.Helpers.Interfaces;
using MailKit.Security;

namespace Florentina_Infinion_Assessment.Application.Helpers.Implementation
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public async Task RegistrationEmailAsync(EmailDto emailDto)
        {
            try
            {
                string body = PopulateRegisterEmail(emailDto.FirstName!);
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Infinion Products", _config["EmailSettings:Username"]));
                message.To.Add(new MailboxAddress("", emailDto.To));
                message.Subject = emailDto.Subject;

                var builder = new BodyBuilder();
                builder.HtmlBody = body;
                message.Body = builder.ToMessageBody();

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_config["EmailSettings:Host"], int.Parse(_config["EmailSettings:Port"]!), SecureSocketOptions.StartTls);
                    await client.AuthenticateAsync(_config["EmailSettings:Username"], _config["EmailSettings:Password"]);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while sending email: {ex.Message}");
            }
        }

        private string PopulateRegisterEmail(string FirstName)
        {
            string body = string.Empty;

            string filePath = Directory.GetCurrentDirectory() + @"\Templates\RegisterEmail.html";

            using (var reader = new StreamReader(filePath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{FirstName}", FirstName);

            return body;
        }

    }
}
