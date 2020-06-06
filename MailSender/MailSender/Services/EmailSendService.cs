using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Windows;

namespace MailSender.Services
{
    internal sealed class EmailSendService
    {
        private string strLogin;
        private string strPassword;
        private string strSmtp;
        private int iSmtpPort;
        private string strBody;
        private string strSubject;

        public EmailSendService(string sLogin, string sPassword, string sSmtp, int iPort)
        {
            strLogin = sLogin;
            strPassword = sPassword;
            strSmtp = sSmtp;
            iSmtpPort = iPort;
        }

        private void SendEmail(string mail, string name)
        {
            using (MailMessage mailMessage = new MailMessage(strLogin, mail))
            {
                mailMessage.Subject = strSubject;
                mailMessage.Body = "Hello World!";
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
