namespace Notification
{
    public interface ISms
    {
        void SendSms(string toNumber, string smsBody);
    }
}
