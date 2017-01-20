using System;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Twilio;

namespace Notification
{
    public class Sms : ISms
    {
        #region Object Declaration

        public static readonly bool IsSmsOn = (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["IS_SMS_ON"]))
                                    && Convert.ToBoolean(ConfigurationManager.AppSettings["IS_SMS_ON"]);

        public static string FromNumber
        {
            get
            {
                return ConfigurationManager.AppSettings["TWILIO_FROM_NUMBER_PRODUCTION"];
            }
        }

        static readonly object _objectSendSms = new object();

        // Find your Account Sid and Auth Token at twilio.com/user/account 
        public static string AccountSid
        {
            get
            {
                return ConfigurationManager.AppSettings["TWILIO_ACCOUNT_SID_PRODUCTION"];
            }
        }

        public static string AuthToken
        {
            get
            {
                return ConfigurationManager.AppSettings["TWILIO_AUTH_TOKEN_PRODUCTION"];
            }
        }

        private readonly TwilioRestClient _twilio = null;

        #endregion

        #region Constructor

        public Sms()
        {
            _twilio = new TwilioRestClient(AccountSid, AuthToken);
        }

        #endregion


        public void SendSms(string toNumber, string smsBody)
        {
            //Send sms asynchronously using Twilio
            Task<bool> taskWithMethodAndState =
                Task.Factory.StartNew(() => Send(toNumber, smsBody));
            taskWithMethodAndState.ContinueWith(cnt =>
            {
                cnt.Dispose();
            });
        }

        /// <summary>
        /// This is to send the sms 
        /// </summary>
        /// <param name="toNumber"></param>
        /// <param name="smsBody"></param>
        /// <returns></returns>
        private bool Send(string toNumber, string smsBody)
        {
            bool status = false;
            //This is to check is the Sms is on or not
            if (IsSmsOn)
            {
                //Regular expression to validate phone number
                if (Regex.Match(toNumber, @"^\+?[0-9]+$").Success)
                {
                    //This is to lock the thread
                    lock (_objectSendSms)
                    {
                        var message = _twilio.SendMessage(FromNumber, toNumber, smsBody);
                        if (message.RestException != null)
                        {
                            var error = message.RestException.Message;
                        }
                        else
                        {
                            status = true;
                        }
                    }
                }
            }
            return status;
        }
    }
}