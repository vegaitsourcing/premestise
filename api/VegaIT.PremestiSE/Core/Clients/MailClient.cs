using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Core.Clients
{
    public class MailClient
    {
        public static void Send(string receiver, string message)
        {
            var user = new
            {
                Mail = new MailAddress("premestisecfc3@gmail.com"),
                Password = "codeforacause3"
            };
            using (SmtpClient smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(user.Mail.Address, user.Password)
            })
            {

                MailAddress receiverEmail = new MailAddress(receiver);

                string subject = "New message on premesti.se";
                string content = $"MessageL {message}";


                using (MailMessage mail = new MailMessage(user.Mail, receiverEmail) { Subject = subject, Body = content })
                {
                    smtp.Send(mail);
                }
            }
        }
    }
}
