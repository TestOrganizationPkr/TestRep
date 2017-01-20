using Notification;

namespace BusinessLogic
{
    public class SmsBL : ISmsBL
    {
        /// <summary>
        /// This is to create an object for the Sms
        /// </summary>
        readonly ISms _sms;

        #pragma warning disable CS3001 // Argument type is not CLS-compliant
        /// <summary>
        /// Constructor which accepts the sms as a parameter which is a dependency.
        /// This dependency is configured in the UnityConfig file inside RegisterTypes function
        /// This is inside ServiceLocation folder
        /// </summary>
        /// <param name="sms"></param>
        public SmsBL(Sms sms)
        {
            _sms = sms;
        }

        public void SendSms(string toNumber, string smsBody)
        {
            _sms.SendSms(toNumber, smsBody);
        }
    }
}
