using SendGrid;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace Notification
{
    public class Email : IEmail
    {
        public static string FromEmail
        {
            get
            {
                return ConfigurationManager.AppSettings["SENDGRID_FROMEMAIL"];
            }
        }

        public static string Username
        {
            get
            {
                return ConfigurationManager.AppSettings["SENDGRID_USERNAME"];
            }
        }

        public static string Password
        {
            get
            {
                return ConfigurationManager.AppSettings["SENDGRID_PASSWORD"];
            }
        }

        public void SendEmail(string toEmail, string emailBody)
        {
            // Create the email object first, then add the properties.
            var myMessage = new SendGridMessage();
            
            // Add the message properties.
            myMessage.From = new MailAddress(FromEmail);

            // Add multiple addresses to the To field.
            List<string> recipients = new List<string>();
            recipients.Add(toEmail);

            myMessage.AddTo(recipients);

            myMessage.Subject = "To Do Item";
            
            myMessage.Text = emailBody;
            
            var credentials = new NetworkCredential(Username, Password);
            
            // Create an Web transport for sending email.
            var transportWeb = new Web(credentials);

            // Send the email, which returns an awaitable task.
            transportWeb.DeliverAsync(myMessage);
        }
    }
}
