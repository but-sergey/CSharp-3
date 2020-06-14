using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace MailSender.Services
{
    internal sealed class Scheduler
    {
        private DispatcherTimer timer = new DispatcherTimer();
        private EmailSendService emailSender;
        private DateTime dtSend;
        private IQueryable<Email> emails;

        public TimeSpan GetSendTime(string strSendTime)
        {
            TimeSpan tsSendTime = new TimeSpan();
            try
            {
                tsSendTime = TimeSpan.Parse(strSendTime);
            }
            catch { }
            return tsSendTime;
        }

        public void SendEmails(DateTime dtSend, EmailSendService emailSender, IQueryable<Email> emails)
        {
            this.emailSender = emailSender;
            this.dtSend = dtSend;
            this.emails = emails;
            timer.Tick += Timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            emailSender.SendMails(emails);
            timer.Stop();
            MessageBox.Show("Письма отправлены", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
