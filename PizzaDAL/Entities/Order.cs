using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaDAL.Entities
{
    public class Order
    {
        public static string StatusCreated = "Created";
        public static string StatusProcessing = "Processing";
        public static string StatusEnroute = "Enroute";
        public static string StatusDelivered = "Delivered";

        public int Id { get; set; }
        public string Status { get; set; }
        public virtual ICollection<OrderedPizza> Pizzas { get; set; }
        public User User { get; set; }
        public int TotalPrice
        {
            get
            {
                int totalPrice = 0;
                foreach (var item in Pizzas)
                {
                    totalPrice += item.Price;
                }
                return totalPrice;
            }
        }

        public Order()
        {
            Pizzas = new List<OrderedPizza>();
        }
    }
}
