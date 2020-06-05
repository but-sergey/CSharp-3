using MailSender.StaticClasses;
using System.Windows;

namespace MailSender
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbSenderSelect.ItemsSource = Variables.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";
        }

        private void miClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClock_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabPlanner;
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
