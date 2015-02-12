using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using PizzaClient2.PizzaOrder;
using System.Windows.Input;

namespace PizzaClient2.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class OrderedPizzaViewModel : ViewModelBase
    {
        public ICommand RemovePizzaCommand { get; set; }
        public OrderedPizza OrderedPizza { get; set; }

        public string Name
        {
            get
            {
                return OrderedPizza.Name;
            }
        }

        public string Size
        {
            get
            {
                return OrderedPizza.Diameter.ToString() + " cm";
            }
        }

        public string Price {
            get
            {
                return OrderedPizza.Price.ToString() + " HUF";
            }
        }

        public OrderedPizzaViewModel()
        {
            RemovePizzaCommand = new RelayCommand(RemovePizza);
        }

        private void RemovePizza()
        {
            Messenger.Default.Send(new ViewModelMessage
            {
                Message = ViewModelMessage.Message_RemoveFromCart,
                OrderedPizza = this.OrderedPizza
            });
        }
    }
}