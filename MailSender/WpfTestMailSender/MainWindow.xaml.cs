using System;
using System.Net;
using System.Net.Mail;
using System.Windows;
using WpfTestMailServer.Dialogs;

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
            EmailSendService email = new EmailSendService();

            try
            {
                email.SendEmail();

                SendEndWindow sendEndWindow = new SendEndWindow();
                sendEndWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                SendErrorWindow sendErrorWindow = new SendErrorWindow();
                sendErrorWindow.ShowDialog();
            }
        }
    }
}
