using System.Windows;
using PizzaClient2.ViewModel;
using GalaSoft.MvvmLight.Messaging;
using PizzaClient2;
using PizzaClient2.View;

namespace PizzaClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
            Messenger.Default.Register<ViewModelMessage>(this, OnReceiveMessage);
        }

        private void OnReceiveMessage(ViewModelMessage msg)
        {
            if (msg.Message == ViewModelMessage.Message_OpenDialog)
            {
                if (msg.Dialog == ViewModelMessage.Dialog_SelectQuantityAndSize)
                {
                    SizeAndQuantityViewModel vm = new ViewModelLocator().SizeAndQuantityView;

                    vm.Pizza = msg.Pizza;
                    vm.Quantity = "1";
                    vm.Size = "32";

                    SizeAndQuantityView view = new SizeAndQuantityView
                    {
                        DataContext = vm
                    };
                    view.Show();
                }
                else if (msg.Dialog == ViewModelMessage.Dialog_OrderSuccess)
                {
                    MessageBox.Show("Order successfully completed.");
                }
                else if (msg.Dialog == ViewModelMessage.Dialog_OrderFailed)
                {
                    MessageBox.Show("Failed to order, please try again later.");
                }
                else if (msg.Dialog == ViewModelMessage.Dialog_LoginFailed)
                {
                    MessageBox.Show("Login failed. Invalid Username or Password.");
                }
                else if (msg.Dialog == ViewModelMessage.Dialog_RegistrationSuccess)
                {
                    MessageBox.Show("Registration completed, you may now login.");
                }
                else if (msg.Dialog == ViewModelMessage.Dialog_RegistrationFailed)
                {
                    MessageBox.Show("Registration failed!");
                }
            }
        }
    }
}