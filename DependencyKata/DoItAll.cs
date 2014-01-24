using System;
using System.IO;

namespace DependencyKata
{
    public class DoItAll
    {
        private readonly UserDetails _userDetails = new UserDetails();

        public void Do()
        {
            Console.WriteLine("Enter a username");
            _userDetails.Username = Console.ReadLine();
            Console.WriteLine("Enter your full name");
            var fullName = Console.ReadLine();
            Console.WriteLine("Enter your password");
            _userDetails.Password = Console.ReadLine();
            Console.WriteLine("Re-enter your password");
            var confirmPassword = Console.ReadLine();

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
