using System;

namespace DependencyKata
{
    public class DoItAll
    {
        public DoItAll(IOutputInputAdapter ioAdapter, ILogging logging)
        {
            _ioAdapter = ioAdapter;
            _logging = logging;
        }

        private readonly UserDetails _userDetails = new UserDetails();
        private IOutputInputAdapter _ioAdapter;
        private readonly ILogging _logging;

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

            message = _logging.LogMessage(message);
            return message;
        }
    }
}
