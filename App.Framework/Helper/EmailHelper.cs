using System.Configuration;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Threading.Tasks;

namespace App.Framework
{
    public static class EmailHelper
    {
        public static async Task SendAsync(string emailTo, string nameTo, string subject, string body, bool isHtml = true)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
            smtp.Host = smtpSection.Network.Host;
            smtp.EnableSsl = smtpSection.Network.EnableSsl;
            smtp.Port = smtpSection.Network.Port;
            smtp.DeliveryMethod = smtpSection.DeliveryMethod;

            MailAddress from = new MailAddress(smtpSection.Network.UserName, smtpSection.From, System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(emailTo, nameTo, System.Text.Encoding.UTF8);

            var message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            await smtp.SendMailAsync(message);
        }

        public static void Send(string emailTo, string nameTo, string subject, string body, bool isHtml = true)
        {
            var smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

            SmtpClient smtp = new SmtpClient();
            smtp.Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
            smtp.Host = smtpSection.Network.Host;
            smtp.EnableSsl = smtpSection.Network.EnableSsl;
            smtp.Port = smtpSection.Network.Port;
            smtp.DeliveryMethod = smtpSection.DeliveryMethod;

            MailAddress from = new MailAddress(smtpSection.Network.UserName, smtpSection.From, System.Text.Encoding.UTF8);
            MailAddress to = new MailAddress(emailTo, nameTo, System.Text.Encoding.UTF8);

            var message = new MailMessage(from, to);
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            smtp.Send(message);
        }


        public static async Task Send(NetworkCredential credential, string host, MailAddress emailTo, MailAddress emailFrom, string subject, string body, int port = 587, bool isHtml = true, bool isEnableSsl = false)
        {
            var message = new MailMessage();
            message.To.Add(emailTo);
            message.From = emailFrom;
            message.Subject = subject;
            message.Body = body;
            message.IsBodyHtml = isHtml;

            var smtp = new SmtpClient();

            smtp.Credentials = credential;
            smtp.Host = host;
            smtp.Port = port;
            smtp.EnableSsl = isEnableSsl;
            await smtp.SendMailAsync(message);

        }
    }
}
