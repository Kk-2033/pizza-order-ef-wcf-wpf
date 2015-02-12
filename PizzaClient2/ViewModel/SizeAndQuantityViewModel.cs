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
    public class SizeAndQuantityViewModel : ViewModelBase
    {
        private string quantity;
        private string size;
        public ICommand AdjustSizeCommand { get; set; }
        public ICommand AddToCartCommand { get; set; }
        public string Quantity
        {
            get
            {
                return quantity;
            }
            set
            {
                this.quantity = value;
                RaisePropertyChanged("Quantity");
                RaisePropertyChanged("PriceHuf"); // dependent property
                ((RelayCommand)AddToCartCommand).RaiseCanExecuteChanged();
            }
        }
        public string Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
                RaisePropertyChanged("Size");
                RaisePropertyChanged("PriceHuf"); // dependent property
            }
        }
        public Pizza Pizza { get; set; }

        public string PriceHuf
        {
            get
            {
                int n, q;
                bool isSizeNumeric = int.TryParse(Size, out n);
                bool isQuantityNumeric = int.TryParse(Quantity, out q);
                if (isSizeNumeric && isQuantityNumeric && q > 0)
                {
                    if (n == 32)
                    {
                        return "Total cost: " + Pizza.Price * q + " HUF";
                    }
                    return "Total cost: " + Pizza.FamilyPrice * q + " HUF";
                }
                else
                {
                    return "";
                }
            }
        }

        public SizeAndQuantityViewModel()
        {
            AdjustSizeCommand = new RelayCommand<string>(AdjustSize);
            AddToCartCommand = new RelayCommand(AddToCart, () =>
            {
                int q;
                bool isQuantityNumeric = int.TryParse(Quantity, out q);
                if (isQuantityNumeric)
                {
                    return (q > 0);
                }
                return false;
            });
        }

        private void AddToCart()
        {
            List<OrderedPizza> orderPart = new List<OrderedPizza>();
            int q;
            bool isQuantityNumeric = int.TryParse(Quantity, out q);
            for (int i = 0; i < q; i++)
            {
                int currentPrice = Size == "32" ? Pizza.Price : Pizza.FamilyPrice;
                orderPart.Add(new OrderedPizza
                {
                    Diameter = int.Parse(Size),
                    Name = this.Pizza.Name,
                    Price = currentPrice
                });
            }
            Messenger.Default.Send(new ViewModelMessage
            {
                Message = ViewModelMessage.Message_AddToCart,
                OrderPart = orderPart
            });
        }

        private void AdjustSize(string size)
        {
            Size = size;
        }
    }
}