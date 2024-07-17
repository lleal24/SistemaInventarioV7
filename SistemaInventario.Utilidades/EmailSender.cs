

using Castle.Core.Smtp;
using System.Net.Mail;

namespace SistemaInventario.Utilidades
{
    public class EmailSender : IEmailSender
    {
        public void Send(string from, string to, string subject, string messageText)
        {
            throw new NotImplementedException();
        }

        public void Send(MailMessage message)
        {
            throw new NotImplementedException();
        }

        public void Send(IEnumerable<MailMessage> messages)
        {
            throw new NotImplementedException();
        }
    }

}
