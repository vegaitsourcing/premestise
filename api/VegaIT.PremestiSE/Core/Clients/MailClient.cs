using Core.Interfaces.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Core.Clients
{
    public interface IMailClient
    {
        void Send(string fromEmail, string message);
        void SendVerificationMessage(RequestDto request, KindergardenDto fromKindergarden, IEnumerable<KindergardenDto> wishes);
    }

    public class MailClient : IMailClient
    {
        public const string Subject = "Nova poruka od premesti.se";
        private readonly ISmtpClientFactory _smtpClientFactory;
        private readonly string _defaultEmail;
        private const string _verificationPageUrl = "placeholder";
        private const string _infoNotValidPageUrl = "placeholder";

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
                string content = $"{message}";

                using (MailMessage mail = new MailMessage(new MailAddress(_defaultEmail), receiverEmail))
                {
                    mail.Subject = Subject;
                    mail.Body = content;
                    mail.IsBodyHtml = true;
                    smtpClient.Send(mail);
                }
            }
        }

        public void SendVerificationMessage(RequestDto request, KindergardenDto fromKindergarden, IEnumerable<KindergardenDto> wishes)
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine($"Poštovani, {request.ParentName} ");
            builder.AppendLine($"Klikom na potvrdi ulazite u sistem za spajanje roditelja radi zamene vrtića.");
            builder.AppendLine($"Informacije:");
            builder.AppendLine($"Ime deteta: {request.ChildName}");
            builder.AppendLine($"Kontakt telefon: {request.PhoneNumber}");
            builder.AppendLine($"Iz vrtića: {fromKindergarden.Name}");
            builder.AppendLine($"U vrtiće:");
            foreach(var wish in wishes) 
                builder.AppendLine($"\t {wish.Name}");

            builder.AppendLine($"<strong><a href="{_verificationPageUrl}"> Potvrdi </a></strong>");
            builder.AppendLine($"ili");
            builder.AppendLine($"<strong><a href="{_infoNotValidPageUrl}"> Prijavi netacne informacije </a></strong>");

            Send(request.Email, builder.ToString());
        }


    }
}
