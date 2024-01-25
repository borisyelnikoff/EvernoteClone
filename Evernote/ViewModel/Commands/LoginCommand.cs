using Evernote.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class LoginCommand(LoginVM loginViewModel) : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public LoginVM LoginViewModel { get; set; } = loginViewModel;

        public bool CanExecute(object parameter)
        {
            var user = (parameter as UserDto);

            return !string.IsNullOrWhiteSpace(user?.Username) &&
                !string.IsNullOrWhiteSpace(user?.Password);
        }

        public void Execute(object parameter)
        {
            LoginViewModel.Login();
        }
    }
}
