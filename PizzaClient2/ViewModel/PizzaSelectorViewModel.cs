using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PizzaClient2.PizzaOrder;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace PizzaClient2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PizzaSelectorViewModel : ViewModelBase
    {
        private ObservableCollection<PizzaViewModel> pizzaList;
        public ICommand ProceedOrderCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public Guid Guid
        {
            get
            {
                return this.guid;
            }
            set
            {
                this.guid = value;
                RaisePropertyChanged("Guid");
            }
        }

        private Guid guid;
        private List<OrderedPizza> cart;

        public ObservableCollection<PizzaViewModel> PizzaList
        {
            get
            {
                return pizzaList;
            }
        }

        public string CartIndicator { get; set; }

        public PizzaSelectorViewModel()
        {

            pizzaList = new ObservableCollection<PizzaViewModel>();
            cart = new List<OrderedPizza>();
            CartIndicator = "Check Cart (0) / Proceed Order";
            Guid = new Guid();

            Messenger.Default.Register<ViewModelMessage>(this, OnReceiveMessage);

            ProceedOrderCommand = new RelayCommand(ProceedOrder, () =>
            {
                return this.cart.Count > 0;
            });
            LogoutCommand = new RelayCommand(Logout);
        }

        private async void Logout()
        {
            PizzaOrderClient client = new PizzaOrderClient();
            var success = await client.LogoutAsync(Guid);
            client.Close();
            if (success)
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_Navigate,
                    NavigateTo = ViewModelMessage.Navigation_Login
                });
            }
        }

        private void ProceedOrder()
        {
            Messenger.Default.Send(new ViewModelMessage
            {
                Message = ViewModelMessage.Message_Navigate,
                NavigateTo = ViewModelMessage.Navigation_ProceedOrder,
                Guid = this.guid,
                Orders = cart
            });
        }

        private void OnReceiveMessage(ViewModelMessage msg)
        {
            if (msg.Message == ViewModelMessage.Message_LoadPizzaList)
            {
                this.Guid = msg.Guid;
                LoadPizzaList();
            }
            else if (msg.Message == ViewModelMessage.Message_AddToCart)
            {
                foreach (var item in msg.OrderPart)
                {
                    this.cart.Add(item);
                }
                CartIndicator = "Check Cart (" + cart.Count + ") / Proceed Order";
                RaisePropertyChanged("CartIndicator");
                ((RelayCommand)ProceedOrderCommand).RaiseCanExecuteChanged();
            }
            else if (msg.Message == ViewModelMessage.Message_RemoveFromCart)
            {
                this.cart.RemoveByNameAndSize(msg.OrderedPizza.Name, msg.OrderedPizza.Diameter);
                CartIndicator = "Check Cart (" + cart.Count + ") / Proceed Order";
                RaisePropertyChanged("CartIndicator");
                ((RelayCommand)ProceedOrderCommand).RaiseCanExecuteChanged();
            }
            else if (msg.Message == ViewModelMessage.Message_ClearCart)
            {
                cart.Clear();
                CartIndicator = "Check Cart (0) / Proceed Order";
                RaisePropertyChanged("CartIndicator");
                ((RelayCommand)ProceedOrderCommand).RaiseCanExecuteChanged();
            }
        }

        public async void LoadPizzaList()
        {
            // do not load list if we already have it
            if (pizzaList.Count == 0)
            {
                PizzaOrderClient client = new PizzaOrderClient();
                var pizzas = await client.GetAllPizzasAsync(this.guid);
                client.Close();
                foreach (var item in pizzas)
                {
                    pizzaList.Add(new PizzaViewModel(item));
                }

                // NYI Guest Pizza
                /*
                pizzaList.Add(new PizzaViewModel(new Pizza
                {
                    Name = PizzaViewModel.GuestChoicePizza,
                    Toppings = new List<Topping>(){
                    new Topping{
                        Name = "Click on the 'Add to Cart' to assemble your toppings."
                    }
                }
                
                }));*/
            }
        }
    }
}