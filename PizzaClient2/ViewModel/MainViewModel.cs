using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace PizzaClient2.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        private ViewModelBase _currentViewModel;

        readonly static ViewModelLocator _locator = new ViewModelLocator();
        readonly static LoginViewModel _loginViewModel = _locator.LoginView;
        readonly static PizzaSelectorViewModel _pizzaSelectorViewModel = _locator.PizzaSelectorView;
        readonly static OrderSummaryViewModel _orderSummaryViewModel = _locator.OrderSummaryView;
        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                {
                    return;
                }
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public MainViewModel()
        {
            CurrentViewModel = MainViewModel._loginViewModel;
            Messenger.Default.Register<ViewModelMessage>(this, OnReceiveMessage);
        }

        private void OnReceiveMessage(ViewModelMessage msg)
        {
            if (msg.Message == ViewModelMessage.Message_Navigate)
            {
                if (msg.NavigateTo == ViewModelMessage.Navigation_PizzaSelector)
                {
                    CurrentViewModel = MainViewModel._pizzaSelectorViewModel;
                    Messenger.Default.Send(new ViewModelMessage
                    {
                        Message = ViewModelMessage.Message_LoadPizzaList,
                        Guid = msg.Guid
                    });
                }
                else if (msg.NavigateTo == ViewModelMessage.Navigation_ProceedOrder)
                {
                    CurrentViewModel = MainViewModel._orderSummaryViewModel;
                    Messenger.Default.Send(new ViewModelMessage
                    {
                        Message = ViewModelMessage.Message_LoadOrderSummary,
                        Guid = msg.Guid,
                        Orders = msg.Orders
                    });
                }
                else if (msg.NavigateTo == ViewModelMessage.Navigation_Login)
                {
                    CurrentViewModel = MainViewModel._loginViewModel;
                }
            }
        }
    }
}