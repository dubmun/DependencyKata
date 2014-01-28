using System;
using System.IO;

namespace DependencyKata
{
    public class DoItAll
    {
        public DoItAll(IConsoleAdapter console)
        {
            _console = console;
        }

        private readonly UserDetails _userDetails = new UserDetails();
        private IConsoleAdapter _console;

        public void Do()
        {
            Console.WriteLine("Enter a username");
            _userDetails.Username = _console.GetInput();
            Console.WriteLine("Enter your full name");
            var fullName = _console.GetInput();
            Console.WriteLine("Enter your password");
            _userDetails.Password = _console.GetInput();
            Console.WriteLine("Re-enter your password");
            var confirmPassword = _console.GetInput();

            if (_userDetails.Password != confirmPassword)
            {
                Console.WriteLine("The passwords don't match");
                return;
            }

            var message = String.Format("Saving Details for User ({0}, {1}, {2})\n", _userDetails.Username,
                fullName, _userDetails.PasswordEncrypted);

            Console.Write(message);

            try
            {
                Database.SaveToLog(message);
            }
            catch (Exception ex)
            {
                // If database write fails, write to file
                using (var writer = new StreamWriter("log.txt", true))
                {
                    writer.WriteLine(message + "\nDatabase.SaveToLog Exception: " + ex.Message);
                }
            }
        }
    }
}
