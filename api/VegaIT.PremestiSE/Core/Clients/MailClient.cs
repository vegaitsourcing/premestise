using System;
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
        void SendFoundMatchMessage(RequestDto firstMatch, RequestDto secondMatch, KindergardenDto from, KindergardenDto to);
    }

    public class MailClient : IMailClient
    {
        public const string Subject = "Nova poruka od premesti.se";
        private readonly ISmtpClientFactory _smtpClientFactory;
        private readonly string _defaultEmail;
        private const string _verificationPageUrl = "placeholder";
        private const string _infoNotValidPageUrl = "placeholder";
        private const string _confirmMatchPageUrl = "placeholder";
        private const string _unmatchPageUrl = "placeholder";

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

            builder.Append($"Poštovani, {request.ParentName} <br>");
            builder.Append($"Klikom na potvrdi ulazite u sistem za spajanje roditelja radi zamene vrtića. <br>");
            builder.Append($"Informacije: <br>");
            builder.Append($"Ime deteta: {request.ChildName} <br>");
            builder.Append($"Kontakt telefon: {request.PhoneNumber} <br>");
            builder.Append($"Iz vrtića: {fromKindergarden.Name} <br>" );
            builder.Append($"U vrtiće: <br>");

            foreach(var wish in wishes) 
                builder.AppendLine($"\t {wish.Name} <br>");

            builder.AppendLine($"<strong><a href=\"{_confirmMatchPageUrl}\"> Potvrdi </a></strong> <br>");
            builder.AppendLine($"ili <br>");
            builder.AppendLine($"<strong><a href=\"{_unmatchPageUrl}\"> Prijavi netacne informacije </a></strong> <br>");

            Send(request.Email, builder.ToString());
        }

        public void SendFoundMatchMessage(RequestDto firstMatch, RequestDto secondMatch, KindergardenDto from, KindergardenDto to)
        {
            /*
             * First parent email
             */
            StringBuilder firstMessageBuilder = new StringBuilder();



            firstMessageBuilder.Append($"Poštovani, {firstMatch.ParentName} <br/>");
            firstMessageBuilder.Append($"Pronašli smo potencijalni premeštaj za vaše dete. ${Environment.NewLine}");

            firstMessageBuilder.Append($"<table>");

            firstMessageBuilder.Append($"<tr>");
            firstMessageBuilder.Append($"<td> Ime deteta: </td>");
            firstMessageBuilder.Append($"<td> {firstMatch.ChildName} </td>");
            firstMessageBuilder.Append($"<td> {secondMatch.ChildName} </td>");
            firstMessageBuilder.Append($"</tr>");

            firstMessageBuilder.Append($"<tr>");
            firstMessageBuilder.Append($"<td> Vrtić: </td>");
            firstMessageBuilder.Append($"<td> {from.Name} </td>");
            firstMessageBuilder.Append($"<td> {to.Name} </td>");
            firstMessageBuilder.Append($"</tr>");
            firstMessageBuilder.Append($"</table> <br>");

            firstMessageBuilder.Append($"Kontakt informacije drugog roditelja: <br>");
            firstMessageBuilder.Append($"Ime: {secondMatch.ParentName} <br>");
            firstMessageBuilder.Append($"Telefon: {secondMatch.PhoneNumber} <br>");
            firstMessageBuilder.Append($"Email: {secondMatch.Email} <br>");


            firstMessageBuilder.AppendLine($"<strong><a href=\"{_verificationPageUrl}\"> Uspešno </a></strong> <br>");
            firstMessageBuilder.AppendLine($"ili <br>");
            firstMessageBuilder.AppendLine($"<strong><a href=\"{_infoNotValidPageUrl}\"> Vrati me u red čekanja </a></strong> <br>");

            /*
             * Second parent email
             */
            StringBuilder secondMessageBuilder = new StringBuilder();
            secondMessageBuilder.Append($"Poštovani, {secondMatch.ParentName} <br>");
            secondMessageBuilder.Append($"Pronašli smo potencijalni premeštaj za vaše dete. <br>");

            secondMessageBuilder.Append($"<table>");

            secondMessageBuilder.Append($"<tr>");
            secondMessageBuilder.Append($"<td> Ime deteta: </td>");
            secondMessageBuilder.Append($"<td> {secondMatch.ChildName} </td>");
            secondMessageBuilder.Append($"<td> {firstMatch.ChildName} </td>");
            secondMessageBuilder.Append($"</tr>");

            secondMessageBuilder.Append($"<tr>");
            secondMessageBuilder.Append($"<td> Vrtić: </td>");
            secondMessageBuilder.Append($"<td> {to.Name} </td>");
            secondMessageBuilder.Append($"<td> {from.Name} </td>");
            secondMessageBuilder.Append($"</tr>");
            secondMessageBuilder.Append($"</table> <br>");

            secondMessageBuilder.Append($"Kontakt informacije drugog roditelja: <br>");
            secondMessageBuilder.Append($"Ime: {firstMatch.ParentName} <br>");
            secondMessageBuilder.Append($"Telefon: {firstMatch.PhoneNumber} <br>");
            secondMessageBuilder.Append($"Email: {firstMatch.Email} <br>");

            secondMessageBuilder.AppendLine($"<strong><a href=\"{_verificationPageUrl}\"> Uspešno </a></strong> <br>");
            secondMessageBuilder.AppendLine($"ili <br>");
            secondMessageBuilder.AppendLine($"<strong><a href=\"{_infoNotValidPageUrl}\"> Vrati me u red čekanja </a></strong> <br>");

            Send(firstMatch.Email, firstMessageBuilder.ToString());
            Send(secondMatch.Email, secondMessageBuilder.ToString());
        }
    }
}
