using System;

namespace DependencyKata
{
    public interface IConsoleAdapter
    {
        string GetInput();
    }

    public class ConsoleAdapter : IConsoleAdapter
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }
    }
}