using System.Linq;

namespace MailSender.Services
{
    public sealed class DB
    {
        private EmailsDataContext emails = new EmailsDataContext();
        
        public IQueryable<Email> Emails
        {
            get
            {
                return from c in emails.Emails select c;
            }
        }
    }
}
