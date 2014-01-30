using System;
using System.IO;

namespace DependencyKata
{
    public class DoItAll
    {
        public DoItAll(IOutputInputAdapter ioAdapter)
        {
            _ioAdapter = ioAdapter;
        }

        private readonly UserDetails _userDetails = new UserDetails();
        private IOutputInputAdapter _ioAdapter;

        public string Do()
        {
            _ioAdapter.SetOutput("Enter a username");
            _userDetails.Username = _ioAdapter.GetInput();
            _ioAdapter.SetOutput("Enter your full name");
            _userDetails.Fullname = _ioAdapter.GetInput();
            _ioAdapter.SetOutput("Enter your password");
            _userDetails.Password = _ioAdapter.GetInput();
            _ioAdapter.SetOutput("Re-enter your password");
            var confirmPassword = _ioAdapter.GetInput();

            if (_userDetails.Password != confirmPassword)
            {
                var errMessage = "The passwords don't match";
                _ioAdapter.SetOutput(errMessage);
                return errMessage;
            }

            var message = String.Format("Saving Details for User ({0}, {1}, {2})\n", 
                _userDetails.Username,
                _userDetails.Fullname, 
                _userDetails.PasswordEncrypted);

            _ioAdapter.SetOutput(message);

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
