using System;
using System.Net;
using System.Net.Mail;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            MailMessage mailMessage = new MailMessage(Settings.FROM_MAIL, Settings.TO_MAIL);
            mailMessage.Subject = "Пробное письмо";
            mailMessage.Body = "Содержимое пробного письма";
            mailMessage.IsBodyHtml = false;

            SmtpClient client = new SmtpClient(Settings.SMTP_SERVER, Settings.SMTP_PORT);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            var password = Settings.SENDER_PASSWORD;
            if (password == string.Empty)
            {
                Console.Write("Пароль для доступа к аккаунту отправки писем: ");
                password = Console.ReadLine();
            }
            client.Credentials = new NetworkCredential(Settings.SENDER_NAME, password);

            try
            {
                client.Send(mailMessage);
                Console.WriteLine($"Письмо от {Settings.FROM_MAIL} на адрес {Settings.TO_MAIL} отправлено.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Невозможно отправить письмо ({ex.Message})");
            }

            Console.Write("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}
