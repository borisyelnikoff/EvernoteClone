using Evernote.Model;
using Evernote.ViewModel.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evernote.ViewModel
{
    public class LoginVM : INotifyPropertyChanged
    {
        private UserDto _user;
        private bool _isShowingRegisterView;

        public LoginVM()
        {
            RegisterCommand = new RegisterCommand(this);
            LoginCommand = new LoginCommand(this);
            _user = new UserDto();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public RegisterCommand RegisterCommand { get; set; }

        public LoginCommand LoginCommand { get; set; }

        public UserDto User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public bool IsRegisterVisible
        {
            get => _isShowingRegisterView;
            set
            {
                _isShowingRegisterView = value;
                OnPropertyChanged(nameof(IsRegisterVisible));
                OnPropertyChanged(nameof(IsLoginVisible));
            }
        }

        public string Username
        {
            get => User.Username;
            set
            {
                User = new UserDto()
                {
                    FirstName = User.FirstName, 
                    LastName = User.LastName,
                    Username = value,
                    Password = Password,
                    ConfirmedPassword = ConfirmedPassword
                };
                OnPropertyChanged(nameof(User));
            }
        }

        public string Password
        {
            get => User.Password;
            set
            {
                User = new UserDto()
                {
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Username = Username,
                    Password = value,
                    ConfirmedPassword = ConfirmedPassword
                };
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmedPassword
        {
            get => User.ConfirmedPassword;
            set
            {
                User = new UserDto()
                {
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Username = Username,
                    Password = Password,
                    ConfirmedPassword = value
                };
                OnPropertyChanged(nameof(ConfirmedPassword));
            }
        }

        public bool IsLoginVisible => !IsRegisterVisible;

        public void Login()
        {
        }

        public async Task Register()
        {
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void SwitchView() => IsRegisterVisible = !IsRegisterVisible;
    }
}
