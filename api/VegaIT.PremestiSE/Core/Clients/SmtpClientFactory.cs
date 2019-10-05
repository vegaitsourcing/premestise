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
        public const string DefaultUsername = "premestisecfc3@gmail.com";
        public const string DefaultPassword = "codeforacause3";
        private static readonly NetworkCredential Credential =
            new NetworkCredential(DefaultUsername, DefaultPassword);

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