using Florentina_Infinion_Assessment.Application.DTOs;
using Florentina_Infinion_Assessment.Application.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using MailKit.Net.Smtp;
using System.Text;
using System.Threading.Tasks;

namespace Florentina_Infinion_Assessment.Application.Services.Implementation
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
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Infinion Products", _config["EmailSettings:Username"]));
                message.To.Add(new MailboxAddress("", emailDto.To));
                message.Subject = emailDto.Subject;
                message.Body = new TextPart("html")
                {
                    Text = emailDto.Body
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_config["EmailSettings:Host"], int.Parse(_config["EmailSettings:Port"]!), false);
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
    }
}
