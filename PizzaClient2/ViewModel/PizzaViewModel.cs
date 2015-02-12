using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PizzaClient2.PizzaOrder;
using System.Collections.Generic;
using System.Windows.Input;

namespace PizzaClient2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class PizzaViewModel : ViewModelBase
    {
        private Pizza pizza;
        public static string GuestChoicePizza = "Guest Choice Pizza";
        public ICommand AddToCartCommand { get; set; }

        public string Name
        {
            get
            {
                return pizza.Name;
            }
        }

        public ICollection<Topping> Toppings
        {
            get
            {
                return pizza.Toppings;
            }
        }

        public int Price
        {
            get
            {
                return pizza.Price;
            }
        }

        public string PriceHuf
        {
            get
            {
                if (pizza.Name == GuestChoicePizza)
                {
                    return "";
                }
                return pizza.Price + " HUF";
            }
        }

        public int FamilyPrice
        {
            get
            {
                return pizza.FamilyPrice;
            }
        }

        public string ToppingsAsText
        {
            get
            {
                List<string> toppingsAsText = new List<string>();
                foreach (var topping in pizza.Toppings)
                {
                    toppingsAsText.Add(topping.Name);
                }
                return "(" + string.Join(", ", toppingsAsText) + ")";
            }
        }

        /// <summary>
        /// Initializes a new instance of the PizzaViewModel class.
        /// </summary>
        public PizzaViewModel(Pizza p)
        {
            pizza = p;
            AddToCartCommand = new RelayCommand<string>(AddToCart);
        }

        // pops up dialog to gather quantity and size
        private void AddToCart(string name)
        {
            Messenger.Default.Send(new ViewModelMessage
            {
                Message = ViewModelMessage.Message_OpenDialog,
                Dialog = ViewModelMessage.Dialog_SelectQuantityAndSize,
                Pizza = this.pizza
            });
        }

        public Pizza ToPizza()
        {
            return pizza;
        }
    }
}