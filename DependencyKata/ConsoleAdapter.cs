using System;

namespace DependencyKata
{
    public interface IOutputInputAdapter
    {
        string GetInput();
        void SetOutput(string output);
    }

    public class ConsoleAdapter : IOutputInputAdapter
    {
        public string GetInput()
        {
            return Console.ReadLine();
        }

        public void SetOutput(string output)
        {
            Console.WriteLine(output);
        }
    }
}