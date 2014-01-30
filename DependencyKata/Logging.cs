using System;
using System.IO;

namespace DependencyKata
{
    public interface ILogging
    {
        string LogMessage(string message);
    }

    public class DatabaseLogging : ILogging
    {
        public string LogMessage(string message)
        {
            try
            {
                Database.SaveToLog(message);
            }
            catch (Exception ex)
            {
                // If database write fails, write to file
                using (var writer = new StreamWriter("log.txt", true))
                {
                    message = message + "\nDatabase.SaveToLog Exception: " + ex.Message;
                    writer.WriteLine(message);
                }
            }
            return message;
        }
    }
}