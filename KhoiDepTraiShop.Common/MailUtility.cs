using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KhoiDepTraiShop.Common
{
    public class MailUtility
    {
        public static  bool SendMail(string toMail,string subject, string content)
        {
            try
            {
                var host = ConfigUtility.GetByKey("SMTPHost");
                var port =int.Parse( ConfigUtility.GetByKey("Port"));
                var fromEmail = ConfigUtility.GetByKey("FromEmail");
                var fromEmailPassword = ConfigUtility.GetByKey("FromMailPassword");
                var fromName = ConfigUtility.GetByKey("FromName");

                var smtpClient = new SmtpClient(host, port)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(fromEmail, fromEmailPassword),
                    EnableSsl = true,
                    Timeout = 10000
                };

                var mail = new MailMessage
                {
                    Body = content,
                    Subject = subject,
                    From = new MailAddress(fromEmail, fromName)
                };

                mail.To.Add(new MailAddress(toMail));
                mail.BodyEncoding = Encoding.UTF8;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                smtpClient.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;  
            }
        }
    }
}
