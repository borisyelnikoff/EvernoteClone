using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Evernote.ViewModel.Commands
{
    public class RegisterCommand(LoginVM loginViewModel) : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public LoginVM LoginViewModel { get; set; } = loginViewModel;

        public bool CanExecute(object parameter)
        {
            var user = parameter as UserDto;

            return !string.IsNullOrWhiteSpace(user?.Username) &&
                !string.IsNullOrWhiteSpace(user?.Password) &&
                user.Password == user.ConfirmedPassword;
        }

        public async void Execute(object parameter)
        {
            await LoginViewModel.Register();
        }
    }
}
