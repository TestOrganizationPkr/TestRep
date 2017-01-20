namespace BusinessLogic
{
    public interface IEmailBL
    {
        void SendEmail(string toEmail, string emailBody);
    }
}
