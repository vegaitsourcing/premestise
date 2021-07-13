using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;

namespace Core.Clients
{
    public interface ISmtpClientFactory
    {
        ISmtpClientWrapper CreateDefaultClient();
    }

    public class SmtpClientFactory : ISmtpClientFactory
    {
        private readonly NetworkCredential Credential;

        public SmtpClientFactory(IConfiguration config)
        {
            string defaultUsername = config.GetSection("DefaultUsername").Value;
            string defaultPassword = config.GetSection("DefaultPassword").Value;
            Credential = new NetworkCredential(defaultUsername, defaultPassword);
        }

        public ISmtpClientWrapper CreateDefaultClient()
        {
            return new SmtpClientWrapper(
                "smtp.gmail.com",
                587,
                true, 
                SmtpDeliveryMethod.Network,
                false,
                Credential);
        }
    }
}