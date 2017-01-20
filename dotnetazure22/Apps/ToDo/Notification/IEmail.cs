namespace Notification
{
    public interface IEmail
    {
        void SendEmail(string toEmail, string emailBody);
    }
}