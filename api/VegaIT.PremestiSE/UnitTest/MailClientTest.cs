using System.Net.Mail;
using Core.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace UnitTest
{
    public class MailClientTest
    {
        private readonly MailClient _mailClient;
        private readonly Mock<ISmtpClientWrapper> _client;
        private const string ToEmail = "default@e.mail";
        private MailMessage _sentEmail;

        public MailClientTest()
        {
            var factory = new Mock<ISmtpClientFactory>();
            _client = new Mock<ISmtpClientWrapper>();
            _client.Setup(x => x.Send(It.IsAny<MailMessage>())).Callback<MailMessage>(mail => _sentEmail = mail);
            factory.Setup(x => x.CreateDefaultClient()).Returns(_client.Object);
            var section = new Mock<IConfigurationSection>();
            section.Setup(x => x.Value).Returns(ToEmail);
            var config = new Mock<IConfiguration>();
            config.Setup(x => x.GetSection(It.IsAny<string>())).Returns(section.Object);
            var logger = new Mock<ILogger<MailClient>>();
            _mailClient = new MailClient(factory.Object, config.Object, null);
        }

        //[Fact]
        //public void GivenDefaultClientFactory_WhenSendCalled_ShouldSendSmtpEmail()
        //{
        //    var fromEMail = "test@e.mail";
        //    var message = "test_message";
        //    string finalBody = $"Message: {message}|from:{fromEMail}";
            
        //    _mailClient.Send(fromEMail, message);
            
        //    _client.Verify(client => client.Send(It.IsAny<MailMessage>()));
        //    //Assert.Equal(MailClient.NewMessageOnPremestiSe, _sentEmail.Subject);
        //    Assert.Equal(finalBody, _sentEmail.Body);
        //    Assert.Equal(SmtpClientFactory.DefaultUsername, _sentEmail.From.Address);
        //    Assert.Equal(ToEmail, _sentEmail.To.ToString());
        //}
    }
}