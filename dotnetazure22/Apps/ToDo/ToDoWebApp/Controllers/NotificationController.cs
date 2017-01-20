using BusinessLogic;
using System;
using System.Web.Mvc;
namespace ToDoWebApp.Controllers
{
    public class NotificationController : Controller
    {
        readonly ISmsBL _smsService;
		readonly IEmailBL _emailService;
        /// <summary>
/// Constructor which accepts the service as a parameter which is a dependency.
/// This dependency is configured in the UnityConfig file inside RegisterTypes function
/// This is in Service layer project inside ServiceLocation folder
/// This is a file called UnityMvcActivator which is responsible to Start and Shutdown the DI 
/// Start will called when the application start
/// Shutdown will called when the application stop
/// </summary>
/// <param name="smsservice"></param>
public NotificationController(ISmsBL smsservice, IEmailBL emailservice)
{
	_smsService = smsservice;
	_emailService = emailservice;
}
        /// <summary>
/// This is to send the sms
/// </summary>
/// <param name="toNumber"></param>
/// <param name="smsBody"></param>
/// <returns></returns>
[HttpPost]
public JsonResult SendSms(string toNumber, string smsBody)
{
	string result = string.Empty;
	try
	{
		if (!string.IsNullOrEmpty(toNumber) && !string.IsNullOrEmpty(smsBody))
		{
			_smsService.SendSms(toNumber, smsBody);
		}
	}
	catch (Exception ex)
	{
		Logger.Error("MessageController Unable to consume Send:" + ex.Message + ex.StackTrace);
		result = "Error";
	}
	finally
	{
		//This is to dispose the object
		Dispose();
	}
	return Json(result, JsonRequestBehavior.AllowGet);
}
		 /// <summary>
        /// This is to send the email using sendgrid
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="emailBody"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SendEmail(string toEmail, string emailBody)
        {
            string result = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(toEmail) && !string.IsNullOrEmpty(emailBody))
                {
                    _emailService.SendEmail(toEmail, emailBody);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("MessageController Unable to consume SendEmail:" + ex.Message + ex.StackTrace);
                result = "Error";
            }
            finally
            {
                //This is to dispose the object
                Dispose();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}