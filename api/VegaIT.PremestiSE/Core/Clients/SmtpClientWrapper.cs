using System;
using System.Net;
using System.Net.Mail;

namespace Core.Clients
{
    public interface ISmtpClientWrapper : IDisposable
    {
        void Send(MailMessage mailMessage);
    }

    public class SmtpClientWrapper : ISmtpClientWrapper
    {
        private readonly SmtpClient _smtpClient;

        public SmtpClientWrapper(string host, int port, bool enableSsl, SmtpDeliveryMethod deliveryMethod,
            bool useDefaultCredentials, ICredentialsByHost credentials)
        {
            _smtpClient = new SmtpClient
            {
                Host = host,
                Port = port, 
                EnableSsl = enableSsl, 
                DeliveryMethod = deliveryMethod, 
                UseDefaultCredentials = useDefaultCredentials, 
                Credentials = credentials
            };
        }

        public void Send(MailMessage mailMessage)
        {
            _smtpClient.Send(mailMessage);
        }

        public void Dispose()
        {
            _smtpClient.Dispose();
        }
    }
}