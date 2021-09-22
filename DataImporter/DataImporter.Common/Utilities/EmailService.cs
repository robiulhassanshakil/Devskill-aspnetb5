using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace DataImporter.Common.Utilities
{
    public class EmailService : IEmailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly bool _useSsl;
        private readonly string _from;

        public EmailService(string host,int port,string username,string password, bool useSsl,string from)
        {
            _host = host;
            _port = port;
            _username = username;
            _password = password;
            _useSsl = useSsl;
            _from = from;
        }
        public void SendEmail(string receiver, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_from, _from));
            message.To.Add(new MailboxAddress(receiver, receiver));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = body,
            };

            using var client = new SmtpClient();
            client.Timeout = 60000;
            client.Connect(_host, _port,_useSsl);
            client.Authenticate(_username, _password);
            client.Send(message);
            client.Disconnect(true);
        }
    }
}
