using System;
using Core.Interfaces.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Net.Mime;

namespace Core.Clients
{
    public interface IMailClient
    {
        void Send(string fromEmail, string message);
        void Send(string fromEmail, List<AlternateView> altViews);
        void SendVerificationMessage(RequestDto request, KindergardenDto fromKindergarden, IEnumerable<KindergardenDto> wishes);
        void SendFoundMatchMessage(RequestDto firstMatch, RequestDto secondMatch, KindergardenDto from, KindergardenDto to);
    }

    public class MailClient : IMailClient
    {
        public const string Subject = "Nova poruka od premesti.se";

        private readonly string _defaultEmail;
        private readonly ISmtpClientFactory _smtpClientFactory;

        private const string _unmatchPageUrl = "placeholder";
        private const string _verificationPageUrl = "placeholder";
        private const string _infoNotValidPageUrl = "placeholder";
        private const string _confirmMatchPageUrl = "placeholder";

        public MailClient(ISmtpClientFactory smtpClientFactory, IConfiguration config)
        {
            _smtpClientFactory = smtpClientFactory;
            _defaultEmail = config.GetSection("DefaultEmail").Value;
        }

        public void Send(string toEmail, string message)
        {
            using (var smtpClient = _smtpClientFactory.CreateDefaultClient())
            {
                MailAddress receiverEmail = new MailAddress(toEmail);

                using (MailMessage mail = new MailMessage(new MailAddress(_defaultEmail), receiverEmail))
                {
                    mail.Subject = Subject;
                    mail.Body = message;
                    mail.IsBodyHtml = true;
                    smtpClient.Send(mail);
                }
            }
        }

        public void Send(string toEmail, List<AlternateView> altViews)
        {
            using (var smtpClient = _smtpClientFactory.CreateDefaultClient())
            {
                MailAddress receiverEmail = new MailAddress(toEmail);

                using (MailMessage mail = new MailMessage(new MailAddress(_defaultEmail), receiverEmail))
                {
                    mail.Subject = Subject;
                    foreach (AlternateView altView in altViews)
                        mail.AlternateViews.Add(altView);
                    mail.IsBodyHtml = true;
                    smtpClient.Send(mail);
                }
            }
        }

        public void SendVerificationMessage(RequestDto request, KindergardenDto fromKindergarden, IEnumerable<KindergardenDto> wishes)
        {
            string verifyTemplatePath = $"{Directory.GetParent(System.Environment.CurrentDirectory)}\\Core\\Templates\\verify.htm";
            string bannerPath = $"{Directory.GetParent(Environment.CurrentDirectory)}\\Core\\Templates\\images\\top-banner.jpg";
            string footerPath = $"{Directory.GetParent(Environment.CurrentDirectory)}\\Core\\Templates\\images\\logo-footer.png";

            using (StreamReader reader = new StreamReader(verifyTemplatePath))
            {
                string mailText = reader.ReadToEnd();
                mailText = mailText.Replace("[[PARENT_NAME]]", request.ParentName);
                mailText = mailText.Replace("[[CHILD_NAME]]", request.ChildName);
                mailText = mailText.Replace("[[PHONE_NUMBER]]", request.PhoneNumber);
                mailText = mailText.Replace("[[FROM_KINDERGARDEN]]", $"- {fromKindergarden.Name}");

                StringBuilder toKindergardensBuilder = new StringBuilder();
                foreach (KindergardenDto wish in wishes)
                    toKindergardensBuilder.Append($"- {wish.Name}<br>");
                mailText = mailText.Replace("[[TO_KINDERGARDENS]]", toKindergardensBuilder.ToString());

                AlternateView bannerImageAltView = new AlternateView(bannerPath, MediaTypeNames.Image.Jpeg);
                AlternateView footerImageAltView = new AlternateView(footerPath, MediaTypeNames.Image.Jpeg);

                bannerImageAltView.TransferEncoding = TransferEncoding.Base64;
                footerImageAltView.TransferEncoding = TransferEncoding.Base64;

                mailText = mailText.Replace("[[TOP_BANNER_LOGO_SRC]]", $"cid:{bannerImageAltView.ContentId}");
                mailText = mailText.Replace("[[FOOTER_LOGO_SRC]]", $"cid:{footerImageAltView.ContentId}");

                AlternateView messageAltView = AlternateView.CreateAlternateViewFromString(mailText, null, MediaTypeNames.Text.Html);

                Send(request.Email, new List<AlternateView> { messageAltView, bannerImageAltView, footerImageAltView });
            }
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
