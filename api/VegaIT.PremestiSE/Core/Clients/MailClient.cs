using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Core.Clients
{
    public interface IMailClient
    {
        void Send(string fromEmail, string message);
    }

    public class MailClient : IMailClient
    {
        public const string NewMessageOnPremestiSe = "New message on premesti.se";
        private readonly ISmtpClientFactory _smtpClientFactory;
        private readonly string _defaultEmail;
        
        public MailClient(ISmtpClientFactory smtpClientFactory, IConfiguration config)
        {
            _smtpClientFactory = smtpClientFactory;
            _defaultEmail = config.GetSection("DefaultEmail").Value;
        }

        public void Send(string fromEmail, string message)
        {
            var user = new
            {
                Mail = new MailAddress(SmtpClientFactory.DefaultUsername),
                Password = SmtpClientFactory.DefaultPassword
            };
            using (var smtpClient = _smtpClientFactory.CreateDefaultClient())
            {

                MailAddress receiverEmail = new MailAddress(_defaultEmail);

                string subject = NewMessageOnPremestiSe;
                string content = $"Message: {message}|from:{fromEmail}";


                using (MailMessage mail = new MailMessage(user.Mail, receiverEmail) { Subject = subject, Body = content })
                {
                    smtpClient.Send(mail);
                }
            }
        }
    }
}
