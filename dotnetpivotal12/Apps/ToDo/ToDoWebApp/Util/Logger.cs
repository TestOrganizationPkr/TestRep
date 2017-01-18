using System;

namespace ToDoWebApp
{
    /// <summary>
    /// You can views your log in Logs tabs in PCF
    /// </summary>
    public static class Logger
    {
        public static void Information(string logStatement)
        {
            Console.WriteLine("Info : " + logStatement);
        }
        public static void Error(string logStatement)
        {
            Console.WriteLine("Error : " + logStatement);
        }
        public static void Warning(string logStatement)
        {
            Console.WriteLine("Warning : " + logStatement);
        }
    }
}