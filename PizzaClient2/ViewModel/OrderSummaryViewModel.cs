using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PizzaClient2.PizzaOrder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Windows;

namespace PizzaClient2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class OrderSummaryViewModel : ViewModelBase
    {
        private Guid guid;
        public ObservableCollection<OrderedPizzaViewModel> Orders { get; set; }
        public ICommand OrderPizzasCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public string TotalPrice { get; set; }

        /// <summary>
        /// Initializes a new instance of the OrderSummaryViewModel class.
        /// </summary>
        public OrderSummaryViewModel()
        {
            Orders = new ObservableCollection<OrderedPizzaViewModel>();
            Messenger.Default.Register<ViewModelMessage>(this, OnReceiveMessage);

            OrderPizzasCommand = new RelayCommand(OrderPizzas, () => true);
            BackCommand = new RelayCommand(Back, () => true);
            TotalPrice = "Total Price: 0 HUF";
        }

        private void Back()
        {
            Messenger.Default.Send(new ViewModelMessage
            {
                Message = ViewModelMessage.Message_Navigate,
                NavigateTo = ViewModelMessage.Navigation_PizzaSelector,
                Guid = this.guid
            });
        }

        private async void OrderPizzas()
        {
            PizzaOrderClient client = new PizzaOrderClient();
            List<OrderedPizza> ordersList = new List<OrderedPizza>();
            foreach (var item in Orders)
            {
                ordersList.Add(item.OrderedPizza);
            }
            var success = await client.ProceedOrderAsync(guid, ordersList);
            if (success)
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_OrderSuccess
                });
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_ClearCart
                });
            }
            else
            {
                Messenger.Default.Send(new ViewModelMessage
                {
                    Message = ViewModelMessage.Message_OpenDialog,
                    Dialog = ViewModelMessage.Dialog_OrderFailed
                });
            }

            client.Close();
        }

        private void OnReceiveMessage(ViewModelMessage msg)
        {
            if (msg.Message == ViewModelMessage.Message_LoadOrderSummary)
            {
                guid = msg.Guid;
                LoadOrderSummary(msg.Orders);
            }
            else if (msg.Message == ViewModelMessage.Message_RemoveFromCart)
            {
                Orders.RemoveByNameAndSize(msg.OrderedPizza.Name, msg.OrderedPizza.Diameter);
                TotalPrice = "Total Price: " + Orders.Sum(p =>
                {
                    // remove the " HUF" trailing and convert it to int
                    return int.Parse(p.Price.Substring(0, p.Price.Length - 4));
                }).ToString() + " HUF"; // put back " HUF"
                RaisePropertyChanged("TotalPrice");
            }
            else if (msg.Message == ViewModelMessage.Message_ClearCart)
            {
                Orders.Clear();
                TotalPrice = "Total Price: 0 HUF";
                RaisePropertyChanged("TotalPrice");
            }
        }

        private void LoadOrderSummary(List<OrderedPizza> list)
        {
            Orders.Clear();

            int totalPrice = 0;
            foreach (var item in list)
            {
                totalPrice += item.Price;
                this.Orders.Add(new OrderedPizzaViewModel
                {
                    OrderedPizza = item
                });
            }
            TotalPrice = "Total Price: " + totalPrice + " HUF";
            RaisePropertyChanged("TotalPrice");
        }
    }
}