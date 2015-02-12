using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PizzaClient2.PizzaOrder;
using System;
using System.Windows.Input;

namespace PizzaClient2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class LoginViewModel : ViewModelBase
    {
        private User user;
        private Guid guid;

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }
        public string LoginLabel { get; set; }
        public string UserName
        {
            get
            {
                return user.UserName;
            }
            set
            {
                user.UserName = value;
                RaisePropertyChanged("UserName");
            }
        }

        public string Password
        {
            get
            {
                return user.Password;
            }
            set
            {
                user.Password = value;
                RaisePropertyChanged("Password");
            }
        }

        public LoginViewModel()
        {
            this.user = new User();

            LoginCommand = new RelayCommand(LoginUser, () => { return LoginLabel == "Login"; });
            RegisterCommand = new RelayCommand(RegisterUser);

            LoginLabel = "Login";
        }

        private async void RegisterUser()
        {
            PizzaOrderClient client = new PizzaOrderClient();
            var success = await client.RegisterAsync(UserName, Password, "NYI");
            client.Close();

            if (success)
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_RegistrationSuccess
                });
            }
            else
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_RegistrationFailed
                });
            }
        }

        private async void LoginUser()
        {
            LoginLabel = "Logging in...";
            RaisePropertyChanged("LoginLabel");
            ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();

            PizzaOrderClient client = new PizzaOrderClient();
            var loginGuid = await client.LoginAsync(user);
            guid = loginGuid;
            client.Close();

            LoginLabel = "Login";
            RaisePropertyChanged("LoginLabel");
            ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();

            // if login succeeded and we got a valid guid
            if (guid != new Guid())
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Guid = guid,
                    Message = ViewModelMessage.Message_Navigate,
                    NavigateTo = ViewModelMessage.Navigation_PizzaSelector
                });
                UserName = "";
                Password = "";
            }
            else
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_LoginFailed
                });
            }
        }
    }
}