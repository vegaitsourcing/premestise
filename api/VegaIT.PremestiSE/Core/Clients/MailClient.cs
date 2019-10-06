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

        public void Send(string toEmail, string message)
        {
            var user = new
            {
                Mail = new MailAddress(SmtpClientFactory.DefaultUsername),
                Password = SmtpClientFactory.DefaultPassword
            };
            using (var smtpClient = _smtpClientFactory.CreateDefaultClient())
            {

                MailAddress receiverEmail = new MailAddress(toEmail);

                string subject = NewMessageOnPremestiSe;
                string content = $"Message: {message}|from:{_defaultEmail}";


                using (MailMessage mail = new MailMessage(new MailAddress(_defaultEmail), receiverEmail))
                {
                    mail.Subject = subject;
                    mail.Body = content;
                    mail.IsBodyHtml = true;
                    smtpClient.Send(mail);
                }

                
            }
        }
    }
}
