using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using MailSender.StaticClasses;

namespace MailSender.Services
{
    internal sealed class EmailSendService
    {
        private string strLogin;
        private string strPassword;
        private string strSmtp;
        private int iSmtpPort;
        private string strSubject;
        private string strBody;

        public EmailSendService(EmailSettings? emailSettings)
        {
            strLogin = emailSettings?.strLogin;
            strPassword = emailSettings?.strPassword;
            strSmtp = emailSettings?.strSmtp;
            iSmtpPort = emailSettings?.iSmtpPort??0;
            strSubject = emailSettings?.strSubject;
            strBody = emailSettings?.strBody;
        }

        private void SendEmail(string mail, string name)
        {
            using (MailMessage mailMessage = new MailMessage(strLogin, mail))
            {
                mailMessage.Subject = strSubject;
                mailMessage.Body = strBody;
                mailMessage.IsBodyHtml = false;

                using (SmtpClient client = new SmtpClient(strSmtp, iSmtpPort))
                {
                    client.EnableSsl = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(strLogin, strPassword);

                    try
                    {
                        client.Send(mailMessage);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Невозможно отправить письмо " + ex.ToString(), "Ошибка отправки!",
                                        MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        public void SendMails(IQueryable<Email> emails)
        {
            foreach (Email email in emails)
            {
                SendEmail(email.Value, email.Name);
            }
        }
    }
}
