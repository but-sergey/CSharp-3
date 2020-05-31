using System;
using System.Net;
using System.Net.Mail;
using WpfTestMailServer.StaticClasses;

namespace WpfTestMailServer
{
    internal sealed class EmailSendService
    {
        public void SendEmail()
        {
            MailMessage mailMessage = new MailMessage(Settings.FromMail, Settings.ToMail);
            mailMessage.Subject = "Пробное письмо";
            mailMessage.Body = "Содержимое пробного письма";
            mailMessage.IsBodyHtml = false;

            SmtpClient client = new SmtpClient(Settings.SmtpServer, Settings.SmtpPort);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Settings.SenderName, Settings.SenderPassword);
        }
    }
}
