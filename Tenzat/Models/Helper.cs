using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Mail;

namespace Tenzat.Models
{
    public class Helper
    {
        public void SendEmail(string _Subject, string _To, string _MsgBody)
        {
            try
            {
                string email = "info@tenzat.com";
                string password = "Master2000";
                var loginInfo = new NetworkCredential(email, password);
                var msg = new MailMessage();
                var smtpClient = new SmtpClient("smtp.bizmail.yahoo.com", 587);
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                msg.From = new MailAddress(email);
                msg.To.Add(new MailAddress(_To));
                msg.Subject = _Subject;
                msg.Body = _MsgBody;
                msg.IsBodyHtml = true;
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = loginInfo;
                smtpClient.Send(msg);
            }
            catch (Exception)
            {

            }
        }
    }
}