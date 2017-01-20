using BusinessLogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Notification;
using System;
using System.Web.Mvc;
using ToDoWebApp.Controllers;

namespace ToDoWebApp.Tests
{
    [TestClass]
    public class NotificationControllerTest
    {
        /*$$WebTest_Razor_Notification_ControllerTest_Sms_Step1$$*/
		[TestMethod]
public void SendSmsAccurateData()
{
	// Arrange
	NotificationController controller = new NotificationController(new SmsBL(new Sms()),new EmailBL(new Email()));
	// Act
	var result = controller.SendSms("+919876543219", "test data");

	//Assert
	Assert.IsTrue(string.IsNullOrEmpty(Convert.ToString(result.Data)), "Send successfully");
 }

[TestMethod]
public void SendSmsException()
{
	// Arrange
	NotificationController controller = new NotificationController(new SmsMokeService(),new EmailMokeService());
	// Act
	// Act
	var result = controller.SendSms("+919876543219", "test data");

	//Assert
	Assert.IsTrue(result.Data.ToString().ToLower() == "error", "Since the model is wrong so throwing error.");
}
		/*$$WebTest_Razor_Notification_ControllerTest_Email_Step1$$*/  
		[TestMethod]
public void SendEmailAccurateData()
{
	// Arrange
	NotificationController controller = new NotificationController(new SmsBL(new Sms()),new EmailBL(new Email()));
	// Act
	var result = controller.SendEmail("abc@gmail.com", "test data");

	//Assert
	Assert.IsTrue(string.IsNullOrEmpty(Convert.ToString(result.Data)), "Send successfully");
}

[TestMethod]
public void SendEmailException()
{
	// Arrange
	NotificationController controller = new NotificationController(new SmsMokeService(),new EmailMokeService());
	// Act
	// Act
	var result = controller.SendEmail("abc@gmail.com", "test data");

	//Assert
	Assert.IsTrue(result.Data.ToString().ToLower() == "error", "Since the model is wrong so throwing error.");
}  
    }
}
