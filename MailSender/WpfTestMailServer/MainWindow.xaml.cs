using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfTestMailServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
