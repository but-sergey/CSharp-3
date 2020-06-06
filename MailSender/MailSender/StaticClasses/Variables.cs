using CodePasswordDLL;
using System.Collections.Generic;

namespace MailSender.StaticClasses
{
    public static class Variables
    {
        public static Dictionary<string, string> Senders { get { return dicSenders; } }
        public static Dictionary<string, int> SmptServers { get { return dicSmtpServers; } }

        private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
        {
            {"test-3005@list.ru", CodePassword.getPassword("uftuqbtt114") },
            {"but-sergey@bk.ru", "" }
        };

        private static Dictionary<string, int> dicSmtpServers = new Dictionary<string, int>()
        {
            {"smtp.mail.ru", 25 },
            {"smtp.yandex.ru", 25 }
        };
    }
}
