using System.Windows.Documents;

namespace MailSender.StaticClasses
{
    public struct EmailSettings
    {
        public string strLogin;
        public string strPassword;
        public string strSmtp;
        public int iSmtpPort;
        public string strSubject;
        public string strBody;
    }
}
