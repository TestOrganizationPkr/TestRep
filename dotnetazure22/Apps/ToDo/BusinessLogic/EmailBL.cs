using Notification;

namespace BusinessLogic
{
    public class EmailBL : IEmailBL
    {
        /// <summary>
        /// This is to create an object for the Sms
        /// </summary>
        readonly IEmail _email;

#pragma warning disable CS3001 // Argument type is not CLS-compliant
        /// <summary>
        /// Constructor which accepts the sms as a parameter which is a dependency.
        /// This dependency is configured in the UnityConfig file inside RegisterTypes function
        /// This is inside ServiceLocation folder
        /// </summary>
        /// <param name="sms"></param>
        public EmailBL(Email email)
        {
            _email = email;
        }
        

        public void SendEmail(string toEmail, string emailBody)
        {
            _email.SendEmail(toEmail, emailBody);
        }
    }
}
