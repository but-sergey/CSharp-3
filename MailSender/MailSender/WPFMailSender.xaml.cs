using MailSender.Services;
using MailSender.StaticClasses;
using System;
using System.Linq;
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
            cbSenderSelect.SelectedIndex = 0;

            DB db = new DB();
            dgEmails.ItemsSource = db.Emails;
        }

        private void miClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClock_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabPlanner;
        }

        private void btnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            string strLogin = cbSenderSelect.Text;
            string strPassword = cbSenderSelect.SelectedValue.ToString();
            if (string.IsNullOrEmpty(strLogin))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }
            if (string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Укажите пароль отправителя");
                return;
            }

            EmailSendService emailSender = new EmailSendService(strLogin, strPassword);
            emailSender.SendMails((IQueryable<Email>)dgEmails.ItemsSource);
            MessageBox.Show("Письма отправлены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            Scheduler scheduler = new Scheduler();
            TimeSpan tsSendTime = scheduler.GetSendTime(tbTimePicker.Text);
            if(tsSendTime == new TimeSpan())
            {
                MessageBox.Show("Некорректный формат даты");
                return;
            }
            DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
            if(dtSendDateTime < DateTime.Now)
            {
                MessageBox.Show("Дата и время отправки писем не могут быть раньше, чем настоящее время");
                return;
            }
            EmailSendService emailSender = new EmailSendService(cbSenderSelect.Text, 
                                                        cbSenderSelect.SelectedValue.ToString());
            scheduler.SendEmails(dtSendDateTime, emailSender, (IQueryable<Email>)dgEmails.ItemsSource);
        }

        private void tscTabSwitcher_btnNextClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }
    }
}
