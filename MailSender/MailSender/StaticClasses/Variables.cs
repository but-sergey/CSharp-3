using System.Collections.Generic;

namespace MailSender.StaticClasses
{
    public static class Variables
    {
        public static Dictionary<string, string> Senders
        {
            get { return dicSenders; }
        }

        private static Dictionary<string, string> dicSenders = new Dictionary<string, string>()
        {
            {"test-3005@list.ru", "" },
            {"but-sergey@bk.ru", "" }
        };
    }
}
