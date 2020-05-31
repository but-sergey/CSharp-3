using System;
using System.Net;
using System.Net.Mail;
using System.Windows;


namespace WpfTestMailServer
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSendMail_Click(object sender, RoutedEventArgs e)
        {
            MailMessage mailMessage = new MailMessage(Settings.FromMail, Settings.ToMail);
            mailMessage.Subject = "Пробное письмо";
            mailMessage.Body = "Содержимое пробного письма";
            mailMessage.IsBodyHtml = false;

            SmtpClient client = new SmtpClient(Settings.SmtpServer, Settings.SmtpPort);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Settings.SenderName, passwordBox.Password);

            try
            {
                client.Send(mailMessage);
                MessageBox.Show($"Письмо отправлено", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Невозможно отправить письмо ({ex.Message})", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
