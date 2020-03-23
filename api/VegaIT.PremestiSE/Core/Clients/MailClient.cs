﻿using System;
using Core.Interfaces.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.IO;
using System.Net.Mime;
using Util;
using Util.Enums;
using Persistence.Repositories;
using Persistence.Interfaces.Contracts;
using Core.Services.Mappers;
using Persistence.Interfaces.Entites;

namespace Core.Clients
{
    public interface IMailClient
    {
        void Send(string fromEmail, string message);
        void Send(string fromEmail, List<AlternateView> altViews);
        void SendVerificationMessage(RequestDto request, KindergardenDto fromKindergarden, IEnumerable<KindergardenDto> wishes);
        void SendFoundMatchMessage(RequestDto firstMatch, RequestDto secondMatch, KindergardenDto from, KindergardenDto to);
        void SendCircularMatchMessage(List<MatchedRequest> validChain);
    }

    public class MailClient : IMailClient
    {
        public const string Subject = "Nova poruka od premesti.se";

        private readonly string _defaultEmail;
        private readonly string _environment;
        private readonly ISmtpClientFactory _smtpClientFactory;
        private readonly IKindergardenRepository _kindergardenRepository;

        private const string _unmatchPageUrl = "placeholder";
        private const string _verificationPageUrl = "placeholder";
        private const string _infoNotValidPageUrl = "placeholder";
        private const string _confirmMatchPageUrl = "placeholder";

        // ovo negde u config ili nesto
        private readonly string _circularTemplatePath = $"{Directory.GetParent(Environment.CurrentDirectory)}\\Core\\Templates\\circular.htm";
        private readonly string _verifyTemplatePath = $"{Directory.GetParent(Environment.CurrentDirectory)}\\Core\\Templates\\verify.htm";
        private readonly string _matchTemplatePath = $"{Directory.GetParent(Environment.CurrentDirectory)}\\Core\\Templates\\index.htm";
        private readonly string _bannerPath = $"{Directory.GetParent(Environment.CurrentDirectory)}\\Core\\Templates\\images\\top-banner.jpg";
        private readonly string _footerPath = $"{Directory.GetParent(Environment.CurrentDirectory)}\\Core\\Templates\\images\\logo-footer.png";

        public MailClient(ISmtpClientFactory smtpClientFactory, IConfiguration config, IKindergardenRepository kindergardenRepository)
        {
            _smtpClientFactory = smtpClientFactory;
            _kindergardenRepository = kindergardenRepository;
            _defaultEmail = config.GetSection("DefaultEmail").Value;
            _environment = config.GetSection("env").Value;
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

        /// <summary>
        /// Sends email for verification with given request information
        /// </summary>
        /// <param name="request">Parent RequestDto object</param>
        /// <param name="fromKindergarden">KindergardenDto of the current kindergarden</param>
        /// <param name="wishes">List of requested kindergarden wishes</param>
        public void SendVerificationMessage(RequestDto request, KindergardenDto fromKindergarden, IEnumerable<KindergardenDto> wishes)
        {
            // Ako postoji drugi nacin da se slika stavi u email - izmenite
            // pokusao sam img src="http://localhost:50800/assets/images/..." ali nece u mail da stavi

            using (StreamReader reader = new StreamReader(_verifyTemplatePath))
            {
                string mailText = reader.ReadToEnd();
                var groupMapper = new AgeGroupMapper();
                mailText = mailText.Replace("[[PARENT_NAME]]", request.ParentName);
                mailText = mailText.Replace("[[CHILD_GROUP]]", groupMapper.mapGroupToText(request.Group));
                mailText = mailText.Replace("[[PHONE_NUMBER]]", request.PhoneNumber);
                mailText = mailText.Replace("[[URL_ENV]]", _environment);
                mailText = mailText.Replace("[[FROM_KINDERGARDEN]]", $"- {fromKindergarden.Name}");
               
                mailText = mailText.Replace("[[HASHED_ID]]", request.Id);

                StringBuilder toKindergardensBuilder = new StringBuilder();
                foreach (KindergardenDto wish in wishes)
                    toKindergardensBuilder.Append($"- {wish.Name}<br>");
                mailText = mailText.Replace("[[TO_KINDERGARDENS]]", toKindergardensBuilder.ToString());

                AlternateView bannerImageAltView = new AlternateView(_bannerPath, MediaTypeNames.Image.Jpeg);
                AlternateView footerImageAltView = new AlternateView(_footerPath, MediaTypeNames.Image.Jpeg);
                bannerImageAltView.TransferEncoding = TransferEncoding.Base64;
                footerImageAltView.TransferEncoding = TransferEncoding.Base64;

                mailText = mailText.Replace("[[TOP_BANNER_LOGO_SRC]]", $"cid:{bannerImageAltView.ContentId}");
                mailText = mailText.Replace("[[FOOTER_LOGO_SRC]]", $"cid:{footerImageAltView.ContentId}");

                AlternateView messageAltView = AlternateView.CreateAlternateViewFromString(mailText, null, MediaTypeNames.Text.Html);

                Send(request.Email, new List<AlternateView> { messageAltView, bannerImageAltView, footerImageAltView });
            }
        }

        /// <summary>
        /// Sends email to both parents with given match information
        /// </summary>
        /// <param name="firstMatch">RequestDto object of first matched parent</param>
        /// <param name="secondMatch">RequestDto object of second matched parent</param>
        /// <param name="from">KindergardentDto object of first matched parent</param>
        /// <param name="to">KindergardenDto object of second matched parent</param>
        public void SendFoundMatchMessage(RequestDto firstMatch, RequestDto secondMatch, KindergardenDto from, KindergardenDto to)
        {
            using (StreamReader reader = new StreamReader(_matchTemplatePath))
            {
                string mailText = reader.ReadToEnd();

                List<AlternateView> firstParentMailViews = CreateMatchMail(mailText, firstMatch, secondMatch, from, to);
                List<AlternateView> secondParentMailViews = CreateMatchMail(mailText, secondMatch, firstMatch, to, from);

                Send(firstMatch.Email, firstParentMailViews);
                Send(secondMatch.Email, secondParentMailViews);
            }
        }

        // DRY Helper Method
        private List<AlternateView> CreateMatchMail(string mail, RequestDto firstMatch, RequestDto secondMatch, KindergardenDto from, KindergardenDto to)
        {
            // images
            AlternateView bannerImageAltView = new AlternateView(_bannerPath, MediaTypeNames.Image.Jpeg);
            AlternateView footerImageAltView = new AlternateView(_footerPath, MediaTypeNames.Image.Jpeg);
            bannerImageAltView.TransferEncoding = TransferEncoding.Base64;
            footerImageAltView.TransferEncoding = TransferEncoding.Base64;

            // mail
            mail = mail.Replace("[[PARENT_NAME]]", firstMatch.ParentName);
            mail = mail.Replace("[[FROM_KINDERGARDEN]]", from.Name);
            mail = mail.Replace("[[TO_KINDERGARDEN]]", to.Name);
            mail = mail.Replace("[[MATCH_PARENT_NAME]]", secondMatch.ParentName);
            mail = mail.Replace("[[MATCH_PHONE]]", secondMatch.PhoneNumber);
            mail = mail.Replace("[[MATCH_EMAIL]]", secondMatch.Email);
            mail = mail.Replace("[[TOP_BANNER_LOGO_SRC]]", $"cid:{bannerImageAltView.ContentId}");
            mail = mail.Replace("[[FOOTER_LOGO_SRC]]", $"cid:{footerImageAltView.ContentId}");
            mail = mail.Replace("[[MATCHED_REQUEST_ID]]", firstMatch.Id);
            mail = mail.Replace("[[URL_ENV]]", _environment);



            List<AlternateView> mailViews = new List<AlternateView>()
            {
                // image alternate views
                bannerImageAltView,
                footerImageAltView,

                // mail alternate view
                AlternateView.CreateAlternateViewFromString(mail, null, MediaTypeNames.Text.Html)
            };

            return mailViews;
        }

        public void SendCircularMatchMessage(List<MatchedRequest> validChain)
        {
            List<Kindergarden> fromRequestsKindergardens = new List<Kindergarden>(validChain.Count);
            //popuni listu, iz svakog zahteva iz lanca izvuci odakle se zeli premestaj sto ce biti dovoljno za email
            foreach (MatchedRequest request in validChain)
            {
                fromRequestsKindergardens.Add(
                        _kindergardenRepository.GetById(request.FromKindergardenId));
            }
            var groupMapper = new AgeGroupMapper();
            string ageGroup = groupMapper.mapGroupToText(validChain[0].Group);

            for (var i = 0; i < validChain.Count; i++)
            {
                using (StreamReader reader = new StreamReader(_circularTemplatePath))
                {
                    string mailText = reader.ReadToEnd();
                   
                    mailText = mailText.Replace("[[CHAIN_LENGTH]]", validChain.Count.ToString());
                    mailText = mailText.Replace("[[CHILD_GROUP]]", ageGroup);
                    mailText = mailText.Replace("[[HASHED_ID]]", HashId.Encode(validChain[i].Id));
                    mailText = mailText.Replace("[[URL_ENV]]", _environment);

                    mailText = mailText.Replace("[[PERSON_1_NAME]]", validChain[0].ParentName);
                    mailText = mailText.Replace("[[PERSON_1_EMAIL]]", validChain[0].ParentEmail);
                    mailText = mailText.Replace("[[PERSON_1_PHONE]]", validChain[0].ParentPhoneNumber);

                    mailText = mailText.Replace("[[PERSON_2_NAME]]", validChain[1].ParentName);
                    mailText = mailText.Replace("[[PERSON_2_EMAIL]]", validChain[1].ParentEmail);
                    mailText = mailText.Replace("[[PERSON_2_PHONE]]", validChain[1].ParentPhoneNumber);

                    mailText = mailText.Replace("[[PERSON_3_NAME]]", validChain[2].ParentName);
                    mailText = mailText.Replace("[[PERSON_3_EMAIL]]", validChain[2].ParentEmail);
                    mailText = mailText.Replace("[[PERSON_3_PHONE]]", validChain[2].ParentPhoneNumber);

                    mailText = mailText.Replace("[[FROM_KINDERGARDEN_1]]", fromRequestsKindergardens[0].Name);
                    mailText = mailText.Replace("[[FROM_KINDERGARDEN_2]]", fromRequestsKindergardens[1].Name);
                    mailText = mailText.Replace("[[FROM_KINDERGARDEN_3]]", fromRequestsKindergardens[2].Name);


                    AlternateView bannerImageAltView = new AlternateView(_bannerPath, MediaTypeNames.Image.Jpeg);
                    AlternateView footerImageAltView = new AlternateView(_footerPath, MediaTypeNames.Image.Jpeg);
                    bannerImageAltView.TransferEncoding = TransferEncoding.Base64;
                    footerImageAltView.TransferEncoding = TransferEncoding.Base64;

                    mailText = mailText.Replace("[[TOP_BANNER_LOGO_SRC]]", $"cid:{bannerImageAltView.ContentId}");
                    mailText = mailText.Replace("[[FOOTER_LOGO_SRC]]", $"cid:{footerImageAltView.ContentId}");

                    AlternateView messageAltView = AlternateView.CreateAlternateViewFromString(mailText, null, MediaTypeNames.Text.Html);


                    Send(validChain[i].ParentEmail, new List<AlternateView> { messageAltView, bannerImageAltView, footerImageAltView });
                }
            }
            
        }
    }
}
