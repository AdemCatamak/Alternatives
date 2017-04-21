using System;
using System.Net;
using System.Net.Mail;

namespace AdemCatamak.Utilities
{
    public class EmailHelper
    {
        public void SendEmail(string toEmail, string header, string content, string fromEmail, string fromPassword,
                              string host = "smtp.gmail.com", int port = 587)
        {
            SmtpClient client = new SmtpClient
                                {
                                    Host = host,
                                    Port = port,
                                    EnableSsl = true,
                                    DeliveryMethod = SmtpDeliveryMethod.Network,
                                    UseDefaultCredentials = false,
                                    Credentials = new NetworkCredential(fromEmail, fromPassword),
                                    Timeout = 10000
                                };
            MailMessage mm = new MailMessage(fromEmail, toEmail, header, content.Replace("\\n", Environment.NewLine));
            client.Send(mm);
        }
    }
}