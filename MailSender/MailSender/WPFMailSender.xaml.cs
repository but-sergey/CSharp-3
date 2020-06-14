using MailSender.Services;
using MailSender.StaticClasses;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Documents;

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

            cbSmtpSelect.ItemsSource = Variables.SmptServers;
            cbSmtpSelect.DisplayMemberPath = "Key";
            cbSmtpSelect.SelectedValuePath = "Value";
            cbSmtpSelect.SelectedIndex = 0;

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

        private EmailSettings? GetSettings()
        {
            EmailSettings emailSettings;
            emailSettings.strLogin = cbSenderSelect.Text;
            emailSettings.strPassword = cbSenderSelect.SelectedValue.ToString();
            emailSettings.strSmtp = cbSmtpSelect.Text;
            emailSettings.iSmtpPort = (int)cbSmtpSelect.SelectedValue;

            TextRange textRange = new TextRange(rchBody.Document.ContentStart, rchBody.Document.ContentEnd);
            emailSettings.strBody = textRange.Text;
            emailSettings.strSubject = txtSubject.Text;

            if (string.IsNullOrEmpty(emailSettings.strLogin))
            {
                MessageBox.Show("Выберите отправителя");
                tabControl.SelectedIndex = 0;
                return null;
            }
            if (string.IsNullOrEmpty(emailSettings.strPassword))
            {
                MessageBox.Show("Укажите пароль отправителя [Функция ещё не подключена]");
                return null;
            }
            if (string.IsNullOrEmpty(emailSettings.strSubject))
            {
                MessageBox.Show("Заполните тему письма");
                tabControl.SelectedIndex = 2;
                return null;
            }
            if (string.IsNullOrEmpty(emailSettings.strBody))
            {
                MessageBox.Show("Заполните тело письма");
                tabControl.SelectedIndex = 3;
                return null;
            }

            return emailSettings;
        }

        private void btnSendAtOnce_Click(object sender, RoutedEventArgs e)
        {
            EmailSettings? emailSettings = GetSettings();

            if (emailSettings == null) return;

            EmailSendService emailSender = new EmailSendService(emailSettings);
            emailSender.SendMails((IQueryable<Email>)dgEmails.ItemsSource);
            MessageBox.Show("Письма отправлены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            EmailSettings? emailSettings = GetSettings();

            if (emailSettings == null) return;

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

            EmailSendService emailSender = new EmailSendService(emailSettings);
            scheduler.SendEmails(dtSendDateTime, emailSender, (IQueryable<Email>)dgEmails.ItemsSource);
        }

        private void tscTabSwitcher_btnNextClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex = 1;
        }
    }
}
