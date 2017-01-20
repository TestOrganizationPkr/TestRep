namespace BusinessLogic
{
    public interface ISmsBL
    {
        void SendSms(string toNumber, string smsBody);
    }
}
